using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using Image = System.Drawing.Image;

namespace CarTraderApp
{
    public partial class CartControl : UserControl
    {
        private readonly int _userId;
        private readonly string connectionString = "Server=localhost;Database=car_trader;Uid=root;Pwd=Admin;";
        private Label lblTotalItems;
        private Label lblTotalPrice;
        private RadioButton rdoCardPayment;
        private TextBox txtCardNumber;
        private TextBox txtExpiryDate;
        private TextBox txtCsv;
        private TextBox txtFirstName;
        private TextBox txtLastName;
        private TextBox txtEmail;
        private TextBox txtAddress;
        private CheckBox chkSelectAll;
        private String itemId;
        private String itemType;

        public CartControl(int userId)
        {
            InitializeComponent();
            _userId = userId;

            // Initialize flowLayoutPanel for cart items
            flowLayoutPanelCartItems.Width = this.Width / 2; // Set to half of the control width
            flowLayoutPanelCartItems.Height = this.Height; // Full height of the control
            flowLayoutPanelCartItems.Dock = DockStyle.Left; // Align to the left side
            flowLayoutPanelCartItems.AutoScroll = true; // Enable scrolling if necessary

            LoadCartItems();
            CreateSummarySection();
        }

        private void LoadCartItems()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                    SELECT ci.item_id, ci.item_type, 
                           IF(ci.item_type = 'Car', CONCAT(c.make, ' ', c.model, ' ', c.year), p.PartName) AS name, 
                           IF(ci.item_type = 'Car', c.price, p.Price) AS price,
                           IF(ci.item_type = 'Car', c.image, p.Image) AS image
                    FROM cart ci
                    LEFT JOIN cars c ON ci.item_id = c.id AND ci.item_type = 'Car'
                    LEFT JOIN parts p ON ci.item_id = p.Id AND ci.item_type = 'CarPart'
                    WHERE ci.user_id = @UserId";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserId", _userId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Panel itemPanel = CreateCartItemPanel(reader);
                                flowLayoutPanelCartItems.Controls.Add(itemPanel);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading cart items: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private Panel CreateCartItemPanel(MySqlDataReader reader)
        {
            Panel panel = new Panel
            {
                Width = this.Width / 2 - 20,
                Height = 120,
                Margin = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle
            };
            // Extract item data
            itemId = reader["item_id"].ToString();
            itemType = reader["item_type"].ToString();

            CheckBox chkSelect = new CheckBox
            {
                Location = new Point(10, 10),
                Width = 30
            };
            chkSelect.CheckedChanged += (sender, e) => UpdateSummary();

            PictureBox pictureBox = new PictureBox
            {
                Width = 80,
                Height = 80,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = (reader["image"] != DBNull.Value) ? ByteArrayToImage((byte[])reader["image"]) : null,
                Location = new Point(40, 20)
            };

            Label lblName = new Label
            {
                Text = reader["name"].ToString(),
                AutoSize = true,
                Location = new Point(pictureBox.Right + 20, 20),
                Font = new System.Drawing.Font("Arial", 13, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label lblPrice = new Label
            {
                Text = "Price: LKR " + Convert.ToDecimal(reader["price"]).ToString("N2"),
                AutoSize = true,
                Location = new Point(pictureBox.Right + 20, lblName.Bottom + 10),
                Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };

            NumericUpDown numQuantity = new NumericUpDown
            {
                Minimum = 1,
                Maximum = 100,
                Value = 1,
                Width = 60,
                Location = new Point(panel.Width - 200, 30),
                Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold),
                TextAlign = HorizontalAlignment.Center
            };
            numQuantity.ValueChanged += (sender, e) => UpdateSummary();

            Button btnRemove = new Button
            {
                Text = "Remove",
                Width = 100,
                Height = 40,
                Location = new Point(panel.Width - 120, 30),
                Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnRemove.Click += (sender, e) =>
            {
                // Use the extracted item data
                RemoveCartItem(itemId, itemType);

                // Remove the item from the display
                flowLayoutPanelCartItems.Controls.Remove(panel);

                // Update the summary
                UpdateSummary();
            };

            panel.Controls.Add(chkSelect);
            panel.Controls.Add(pictureBox);
            panel.Controls.Add(lblName);
            panel.Controls.Add(lblPrice);
            panel.Controls.Add(numQuantity);
            panel.Controls.Add(btnRemove);

            return panel;
        }

        private void RemoveCartItem(string itemId, string itemType)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM cart WHERE user_id = @UserId AND item_id = @ItemId AND item_type = @ItemType";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserId", _userId);
                        cmd.Parameters.AddWithValue("@ItemId", itemId);
                        cmd.Parameters.AddWithValue("@ItemType", itemType);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error removing item from cart: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UpdateSummary()
        {
            decimal totalPrice = 0;
            int totalItems = 0;

            foreach (Control control in flowLayoutPanelCartItems.Controls)
            {
                if (control is Panel panel)
                {
                    CheckBox chkSelect = panel.Controls.OfType<CheckBox>().FirstOrDefault();
                    NumericUpDown numQuantity = panel.Controls.OfType<NumericUpDown>().FirstOrDefault();
                    Label lblPrice = panel.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.Text.StartsWith("Price: LKR"));

                    if (chkSelect != null && chkSelect.Checked && numQuantity != null && lblPrice != null)
                    {
                        decimal price = Convert.ToDecimal(lblPrice.Text.Replace("Price: LKR ", ""));
                        totalPrice += price * numQuantity.Value;
                        totalItems += (int)numQuantity.Value;
                    }
                }
            }

            lblTotalItems.Text = "ITEMS: " + totalItems;
            lblTotalPrice.Text = "TOTAL PRICE: LKR " + (totalPrice + 500); // Adjust the shipping cost if needed
        }

        private void CreateSummarySection()
        {
            Panel summaryPanel = new Panel
            {
                Width = this.Width / 2, // Set to half of the panel width
                Height = this.Height, // Full height of the control
                Margin = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(this.Width / 2, 0), // Positioned to the right half
                Anchor = AnchorStyles.Top | AnchorStyles.Right // Anchor it to the top-right of the container
            };

            // Add text boxes for user details

            lblTotalItems = new Label
            {
                Text = "ITEMS: 0",
                AutoSize = true,
                Location = new Point(10, 20),
                Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold)
            };

            Label lblShipping = new Label
            {
                Text = "SHIPPING: Standard Delivery - LKR 500.00",
                AutoSize = true,
                Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
                Location = new Point(10, lblTotalItems.Bottom + 10)
            };

            lblTotalPrice = new Label
            {
                Text = "TOTAL PRICE: LKR 0.00",
                AutoSize = true,
                Location = new Point(10, lblShipping.Bottom + 10),
                Font = new System.Drawing.Font("Arial", 14, FontStyle.Bold)
            };

            txtFirstName = new TextBox
            {
                PlaceholderText = "First Name",
                Width = summaryPanel.Width - 20,
                Location = new Point(10, lblTotalPrice.Bottom + 20),
                Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold)
            };

            txtLastName = new TextBox
            {
                PlaceholderText = "Last Name",
                Width = summaryPanel.Width - 20,
                Location = new Point(10, txtFirstName.Bottom + 10),
                Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold)
            };

            txtEmail = new TextBox
            {
                PlaceholderText = "Email",
                Width = summaryPanel.Width - 20,
                Location = new Point(10, txtLastName.Bottom + 10),
                Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold)
            };

            txtAddress = new TextBox
            {
                PlaceholderText = "Address",
                Width = summaryPanel.Width - 20,
                Location = new Point(10, txtEmail.Bottom + 10),
                Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold)
            };

            RadioButton rdoCashOnDelivery = new RadioButton
            {
                Text = "Cash on Delivery",
                AutoSize = true,
                Location = new Point(10, txtAddress.Bottom + 20),
                Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
                Checked = true
            };

            rdoCardPayment = new RadioButton
            {
                Text = "Card Payment",
                AutoSize = true,
                Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
                Location = new Point(10, rdoCashOnDelivery.Bottom + 10),
            };

            // Panel for card details
            Panel pnlCardDetails = new Panel
            {
                Width = summaryPanel.Width - 20,
                Height = 100,
                Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
                Location = new Point(10, rdoCardPayment.Bottom + 10),
                Visible = false // Hidden initially
            };

            txtCardNumber = new TextBox
            {
                Width = pnlCardDetails.Width - 20,
                Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
                Location = new Point(0, 0),
                PlaceholderText = "Card Number"
            };

            txtExpiryDate = new TextBox
            {
                Width = pnlCardDetails.Width - 20,
                Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
                Location = new Point(0, txtCardNumber.Bottom + 10),
                PlaceholderText = "Expiry Date (MM/YY)"
            };

            txtCsv = new TextBox
            {
                Width = pnlCardDetails.Width - 20,
                Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
                Location = new Point(0, txtExpiryDate.Bottom + 10),
                PlaceholderText = "CSV"
            };

            pnlCardDetails.Controls.Add(txtCardNumber);
            pnlCardDetails.Controls.Add(txtExpiryDate);
            pnlCardDetails.Controls.Add(txtCsv);

            // Toggle visibility of card details based on payment selection
            rdoCardPayment.CheckedChanged += (s, e) =>
            {
                pnlCardDetails.Visible = rdoCardPayment.Checked;
            };

            // Select all checkbox
            chkSelectAll = new CheckBox
            {
                Text = "Select All",
                Location = new Point(10, pnlCardDetails.Bottom + 10),
                Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
                AutoSize = true
            };

            chkSelectAll.CheckedChanged += (s, e) =>
            {
                foreach (Control control in flowLayoutPanelCartItems.Controls)
                {
                    if (control is Panel panel)
                    {
                        CheckBox chkSelect = panel.Controls.OfType<CheckBox>().FirstOrDefault();
                        if (chkSelect != null)
                        {
                            chkSelect.Checked = chkSelectAll.Checked;
                        }
                    }
                }
                UpdateSummary();
            };

            // Place buttons side by side
            Button btnCheckout = new Button
            {
                Text = "CHECKOUT",
                Width = 150,
                Height = 40,
                Location = new Point((summaryPanel.Width - 220) / 2, chkSelectAll.Bottom + 20), // Adjusted position for side-by-side layout
                BackColor = Color.Black,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Arial", 13, FontStyle.Bold),
            };

            Button btnCancel = new Button
            {
                Text = "CANCEL",
                Width = 150,
                Height = 40,
                Location = new Point(btnCheckout.Right + 20, chkSelectAll.Bottom + 20), // Right next to the CHECKOUT button
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Arial", 13, FontStyle.Bold),
            };

            btnCheckout.Click += (s, e) =>
            {
                if (rdoCardPayment.Checked && (string.IsNullOrWhiteSpace(txtCardNumber.Text) || string.IsNullOrWhiteSpace(txtExpiryDate.Text)))
                {
                    MessageBox.Show("Please fill in all card details.", "Card Payment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (lblTotalItems.Text == "ITEMS: 0" || lblTotalPrice.Text == "TOTAL PRICE: LKR 0.00")
                {
                    MessageBox.Show("Cart is empty. Please add items to your cart before checking out.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ProceedCheckout();
            };

            btnCancel.Click += (s, e) =>
            {
                // Reset selection and payment options
                foreach (Control control in flowLayoutPanelCartItems.Controls)
                {
                    if (control is Panel panel)
                    {
                        CheckBox chkSelect = panel.Controls.OfType<CheckBox>().FirstOrDefault();
                        NumericUpDown numQuantity = panel.Controls.OfType<NumericUpDown>().FirstOrDefault();

                        if (chkSelect != null)
                        {
                            chkSelect.Checked = false;
                        }

                        if (numQuantity != null)
                        {
                            numQuantity.Value = 1;
                        }
                    }
                }

                rdoCashOnDelivery.Checked = false;
                rdoCardPayment.Checked = false;
                txtCardNumber.Clear();
                txtExpiryDate.Clear();
                txtCsv.Clear();

                // Reset summary
                lblTotalItems.Text = "ITEMS: 0";
                lblTotalPrice.Text = "TOTAL PRICE: LKR 0.00";
            };

            summaryPanel.Controls.Add(lblTotalItems);
            summaryPanel.Controls.Add(lblShipping);
            summaryPanel.Controls.Add(lblTotalPrice);
            summaryPanel.Controls.Add(txtFirstName);
            summaryPanel.Controls.Add(txtLastName);
            summaryPanel.Controls.Add(txtAddress);
            summaryPanel.Controls.Add(txtEmail);
            summaryPanel.Controls.Add(rdoCashOnDelivery);
            summaryPanel.Controls.Add(rdoCardPayment);
            summaryPanel.Controls.Add(pnlCardDetails);
            summaryPanel.Controls.Add(chkSelectAll); // Add select all checkbox to the panel
            summaryPanel.Controls.Add(btnCheckout);
            summaryPanel.Controls.Add(btnCancel); // Add cancel button to the panel

            this.Controls.Add(summaryPanel);
        }

        private void ProceedCheckout()
        {
            // Retrieve user details from input fields if database retrieval fails
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string country = txtAddress.Text;
            string email = txtEmail.Text;

            // Ensure user details are provided
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(country) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Email validation using regex
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                MessageBox.Show("Please enter a valid email address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool isCardPayment = rdoCardPayment.Checked;
            string cardNumber = txtCardNumber.Text;
            string expiryDate = txtExpiryDate.Text;
            string cvv = txtCsv.Text;

            if (isCardPayment)
            {
                // Card number validation using regex (Visa, MasterCard, etc.)
                string cardNumberPattern = @"^\d{16}$"; // Matches exactly 16 digits
                if (!Regex.IsMatch(cardNumber, cardNumberPattern))
                {
                    MessageBox.Show("Please enter a valid 16-digit card number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Expiry date validation using regex (MM/YY format)
                string expiryDatePattern = @"^(0[1-9]|1[0-2])\/?([0-9]{2})$";
                if (!Regex.IsMatch(expiryDate, expiryDatePattern))
                {
                    MessageBox.Show("Please enter a valid expiry date in MM/YY format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // CVV validation using regex (3 or 4 digits)
                string cvvPattern = @"^\d{3,4}$";
                if (!Regex.IsMatch(cvv, cvvPattern))
                {
                    MessageBox.Show("Please enter a valid CVV (3 or 4 digits).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Prepare a list to hold items to be added to the order
            List<(string itemId, string itemType, int quantity, decimal price)> orderItems = new List<(string, string, int, decimal)>();

            decimal totalPrice = 0;

            // Iterate through the selected items
            foreach (Control control in flowLayoutPanelCartItems.Controls)
            {
                if (control is Panel panel)
                {
                    CheckBox chkSelect = panel.Controls.OfType<CheckBox>().FirstOrDefault();
                    NumericUpDown numQuantity = panel.Controls.OfType<NumericUpDown>().FirstOrDefault();
                    Label lblPrice = panel.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.Text.StartsWith("Price: LKR"));

                    if (chkSelect != null && chkSelect.Checked && numQuantity != null && lblPrice != null)
                    {
                        decimal itemPrice = Convert.ToDecimal(lblPrice.Text.Replace("Price: LKR ", ""));
                        totalPrice += itemPrice * numQuantity.Value;

                        // Add item details to the list
                        orderItems.Add((itemId, itemType, (int)numQuantity.Value, itemPrice));
                    }
                }
            }

            // Ensure there are items to checkout
            if (orderItems.Count == 0)
            {
                MessageBox.Show("No items selected for checkout.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create a single order and add all selected items to it
            PlaceOrder(firstName, lastName, country, email, isCardPayment, cardNumber, expiryDate, cvv, totalPrice, orderItems);

            ClearSelectedCartItems(orderItems);

        }


        private void PlaceOrder(string firstName, string lastName, string country, string email, bool isCardPayment, string cardNumber, string expiryDate, string cvv, decimal totalPrice, List<(string itemId, string itemType, int quantity, decimal price)> orderItems)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Insert a single order
                    string orderQuery = "INSERT INTO `orders` (order_number, customer_id, first_name, last_name, country, email, payment_method, order_date, status, total_amount, is_deleted) " +
                                        "VALUES (@OrderNumber, @CustomerId, @FirstName, @LastName, @Country, @Email, @PaymentMethod, CURRENT_TIMESTAMP, @Status, @TotalAmount, 0)";
                    using (MySqlCommand cmd = new MySqlCommand(orderQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@OrderNumber", Guid.NewGuid().ToString().Substring(0, 8)); // Random order number
                        cmd.Parameters.AddWithValue("@CustomerId", _userId);
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@Country", country);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@PaymentMethod", isCardPayment ? "Card Payment" : "Cash on Delivery");
                        cmd.Parameters.AddWithValue("@Status", "Pending");
                        cmd.Parameters.AddWithValue("@TotalAmount", totalPrice);

                        cmd.ExecuteNonQuery();
                    }

                    // Retrieve the last inserted order ID
                    long orderId;
                    using (MySqlCommand cmd = new MySqlCommand("SELECT LAST_INSERT_ID();", connection))
                    {
                        orderId = Convert.ToInt64(cmd.ExecuteScalar());
                    }

                    // Insert each item into the order_items table using the same order ID
                    foreach (var (itemId, itemType, quantity, price) in orderItems)
                    {
                        string orderItemQuery = "INSERT INTO `order_items` (order_id, item_id, item_type, quantity, price) " +
                                                "VALUES (@OrderId, @ItemId, @ItemType, @Quantity, @Price)";
                        using (MySqlCommand cmd = new MySqlCommand(orderItemQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@OrderId", orderId);
                            cmd.Parameters.AddWithValue("@ItemId", itemId);
                            cmd.Parameters.AddWithValue("@ItemType", itemType);
                            cmd.Parameters.AddWithValue("@Quantity", quantity);
                            cmd.Parameters.AddWithValue("@Price", price);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Order is on the way!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error placing order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private Image ByteArrayToImage(byte[] byteArray)
        {
            using (var ms = new System.IO.MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }

        private void ClearCartItems()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM cart WHERE user_id = @UserId";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserId", _userId);
                        cmd.ExecuteNonQuery();
                    }

                    // Clear items from the flowLayoutPanelCartItems
                    flowLayoutPanelCartItems.Controls.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error clearing cart items: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ClearSelectedCartItems(List<(string itemId, string itemType, int quantity, decimal price)> orderItems)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    foreach (var (itemId, itemType, _, _) in orderItems)
                    {
                        string query = "DELETE FROM cart WHERE user_id = @UserId AND item_id = @ItemId AND item_type = @ItemType";
                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@UserId", _userId);
                            cmd.Parameters.AddWithValue("@ItemId", itemId);
                            cmd.Parameters.AddWithValue("@ItemType", itemType);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Clear items from the flowLayoutPanelCartItems
                    foreach (Control control in flowLayoutPanelCartItems.Controls.OfType<Panel>().ToList())
                    {
                        CheckBox chkSelect = control.Controls.OfType<CheckBox>().FirstOrDefault();
                        if (chkSelect != null && chkSelect.Checked)
                        {
                            flowLayoutPanelCartItems.Controls.Remove(control);
                        }
                    }

                    UpdateSummary(); // Update the summary after clearing items
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error clearing selected cart items: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

