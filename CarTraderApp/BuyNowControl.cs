using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarTraderApp
{
    public partial class BuyNowControl : UserControl
    {
        private readonly string connectionString = "Server=localhost;Database=car_trader;Uid=root;Pwd=Admin;";
        private int _customerId;
        private string _productName;
        private decimal _price;
        private Image _productImage;
        private int _quantity;

        public BuyNowControl(int customerId, string productName, decimal price, Image productImage, int quantity)
        {
            InitializeComponent();
            _customerId = customerId;
            _productName = productName;
            _price = price;
            _productImage = productImage;
            _quantity = quantity;
            SetupUI();

        }
        private void SetupUI()
        {
            // Set the size of the control to match the desired layout
            this.Size = new Size(1000, 700);

            // Set the background color of the control
            this.BackColor = Color.Aqua; // Change Color.White to your desired color

            // Calculate dynamic sizes based on the control's size
            int panelWidth = this.Width / 2 - 30; // Leave space for padding between panels
            int panelHeight = this.Height - 40; // Leave some space for margins
            int panelPadding = 20; // Space between panels

            // Billing Information Panel
            Panel billingPanel = new Panel
            {
                Size = new Size(panelWidth, panelHeight),
                Location = new Point(panelPadding, panelPadding),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblFirstName = new Label { Text = "First name:", Location = new Point(10, 20), AutoSize = true, Font = new Font("Arial", 10, FontStyle.Regular) };
            TextBox txtFirstName = new TextBox { Location = new Point(200, 20), Width = 250, Font = new Font("Arial", 10, FontStyle.Regular) };
            Label lblLastName = new Label { Text = "Last name:", Location = new Point(10, 60), AutoSize = true, Font = new Font("Arial", 10, FontStyle.Regular) };
            TextBox txtLastName = new TextBox { Location = new Point(200, 60), Width = 250, Font = new Font("Arial", 10, FontStyle.Regular) };
            Label lblEmail = new Label { Text = "Email address:", Location = new Point(10, 100), AutoSize = true, Font = new Font("Arial", 10, FontStyle.Regular) };
            TextBox txtEmail = new TextBox { Location = new Point(200, 100), Width = 250 };
            Label lblAddress = new Label { Text = "Address:", Location = new Point(10, 140), AutoSize = true, Font = new Font("Arial", 10, FontStyle.Regular) };
            TextBox txtAddress = new TextBox { Location = new Point(200, 140), Width = 250 };

            // Add all controls to the billing panel
            billingPanel.Controls.Add(lblFirstName);
            billingPanel.Controls.Add(txtFirstName);
            billingPanel.Controls.Add(lblLastName);
            billingPanel.Controls.Add(txtLastName);
            billingPanel.Controls.Add(lblEmail);
            billingPanel.Controls.Add(txtEmail);
            billingPanel.Controls.Add(lblAddress);
            billingPanel.Controls.Add(txtAddress);

            // Order Summary Panel
            Panel orderSummaryPanel = new Panel
            {
                Size = new Size(panelWidth, panelHeight),
                Location = new Point(panelWidth + panelPadding * 2, panelPadding),
                BorderStyle = BorderStyle.FixedSingle,
            };

            Label lblOrderSummaryTitle = new Label
            {
                Text = "Your Order",
                Location = new Point(10, 10),
                AutoSize = true,
                Font = new Font("Arial", 12, FontStyle.Bold)
            };

            Label lblProductName = new Label
            {
                Text = _productName,
                Location = new Point(10, 40),
                AutoSize = true
            };

            Label lblProductPrice = new Label
            {
                Text = "Subtotal: LKR " + _price.ToString("F2"),
                Location = new Point(10, 70),
                AutoSize = true,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            // Add a label to show quantity
            Label lblQuantity = new Label
            {
                Text = "Quantity: " + _quantity.ToString(),
                Location = new Point(10, 100), // Position it below the product price
                AutoSize = true,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            decimal totalPrice = _price * _quantity;

            Label lblTotal = new Label
            {
                Text = "Total: LKR " + totalPrice.ToString("F2"),
                Location = new Point(10, 130),
                AutoSize = true,
                Font = new Font("Arial", 12, FontStyle.Bold)
            };

            // Payment Options
            GroupBox grpPayment = new GroupBox
            {
                Text = "Payment Options",
                Location = new Point(10, 160),
                Size = new Size(500, 100)
            };

            RadioButton rdoCashOnDelivery = new RadioButton
            {
                Text = "Cash on Delivery",
                Location = new Point(10, 20),
                Checked = true,
                Font = new Font("Arial", 10, FontStyle.Regular)
            };

            RadioButton rdoCardPayment = new RadioButton
            {
                Text = "Card Payment",
                Location = new Point(10, 50),
                Font = new Font("Arial", 10, FontStyle.Regular)
            };

            grpPayment.Controls.Add(rdoCashOnDelivery);
            grpPayment.Controls.Add(rdoCardPayment);

            // Card Payment Details (initially hidden)
            Label lblCardNumber = new Label { Text = "Card Number:", Location = new Point(10, 300), Visible = false, Font = new Font("Arial", 10, FontStyle.Regular) };
            TextBox txtCardNumber = new TextBox { Location = new Point(200, 300), Width = 250, Visible = false, Font = new Font("Arial", 10, FontStyle.Regular) };
            Label lblExpiry = new Label { Text = "Expiry Date:", Location = new Point(10, 330), Visible = false, Font = new Font("Arial", 10, FontStyle.Regular) };
            TextBox txtExpiry = new TextBox { Location = new Point(200, 330), Width = 250, Visible = false, Font = new Font("Arial", 10, FontStyle.Regular) };
            Label lblCVV = new Label { Text = "CVV:", Location = new Point(10, 360), Visible = false, Font = new Font("Arial", 10, FontStyle.Regular) };
            TextBox txtCVV = new TextBox { Location = new Point(200, 360), Width = 250, Visible = false, Font = new Font("Arial", 10, FontStyle.Regular) };

            rdoCardPayment.CheckedChanged += (s, e) =>
            {
                bool isCardPayment = rdoCardPayment.Checked;
                lblCardNumber.Visible = isCardPayment;
                txtCardNumber.Visible = isCardPayment;
                lblExpiry.Visible = isCardPayment;
                txtExpiry.Visible = isCardPayment;
                lblCVV.Visible = isCardPayment;
                txtCVV.Visible = isCardPayment;
            };

            // Place Order Button
            Button btnPlaceOrder = new Button
            {
                Text = "Place Order",
                Location = new Point(10, 450),
                Width = panelWidth - 20,
                Height = 40,
                BackColor = Color.FromArgb(0, 140, 153),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnPlaceOrder.Click += (s, e) => PlaceOrder(txtFirstName.Text, txtLastName.Text, txtAddress.Text, txtEmail.Text, rdoCardPayment.Checked, txtCardNumber.Text, txtExpiry.Text, txtCVV.Text);

            // Cancel Button
            Button btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(10, 500),
                Width = panelWidth - 20,
                Height = 40,
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.Click += (s, e) => this.Visible = false; // Hide the control on cancel


            // Add all controls to the order summary panel
            orderSummaryPanel.Controls.Add(lblOrderSummaryTitle);
            orderSummaryPanel.Controls.Add(lblProductName);
            orderSummaryPanel.Controls.Add(lblProductPrice);
            orderSummaryPanel.Controls.Add(lblQuantity); // Add quantity label to the panel
            orderSummaryPanel.Controls.Add(lblTotal);
            orderSummaryPanel.Controls.Add(grpPayment);
            orderSummaryPanel.Controls.Add(lblCardNumber);
            orderSummaryPanel.Controls.Add(txtCardNumber);
            orderSummaryPanel.Controls.Add(lblExpiry);
            orderSummaryPanel.Controls.Add(txtExpiry);
            orderSummaryPanel.Controls.Add(lblCVV);
            orderSummaryPanel.Controls.Add(txtCVV);
            orderSummaryPanel.Controls.Add(btnPlaceOrder);
            orderSummaryPanel.Controls.Add(btnCancel);

            // Add both panels to the UserControl
            this.Controls.Add(billingPanel);
            this.Controls.Add(orderSummaryPanel);
        }


        private void PlaceOrder(string firstName, string lastName, string country, string email, bool isCardPayment, string cardNumber, string expiryDate, string cvv)
        {
            // Validate input fields
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(country))
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

            if (isCardPayment)
            {
                // Ensure all card details are provided
                if (string.IsNullOrEmpty(cardNumber) || string.IsNullOrEmpty(expiryDate) || string.IsNullOrEmpty(cvv))
                {
                    MessageBox.Show("Please fill in all card details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

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

            // Simulate placing order
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Insert order details
                    string orderQuery = "INSERT INTO `orders` (order_number, customer_id, first_name, last_name, country, email, payment_method, order_date, status, total_amount, is_deleted) " +
                                        "VALUES (@OrderNumber, @CustomerId, @FirstName, @LastName, @Country, @Email, @PaymentMethod, CURRENT_TIMESTAMP, @Status, @TotalAmount, 0)";
                    using (MySqlCommand cmd = new MySqlCommand(orderQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@OrderNumber", Guid.NewGuid().ToString().Substring(0, 8)); // Random order number
                        cmd.Parameters.AddWithValue("@CustomerId", _customerId);
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@Country", country);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@PaymentMethod", isCardPayment ? "Card Payment" : "Cash on Delivery");
                        cmd.Parameters.AddWithValue("@Status", "Delivering");
                        cmd.Parameters.AddWithValue("@TotalAmount", _price);

                        cmd.ExecuteNonQuery();
                    }
                    // Step 2: Retrieve the last inserted order ID
                    long orderId;
                    using (MySqlCommand cmd = new MySqlCommand("SELECT LAST_INSERT_ID();", connection))
                    {
                        orderId = Convert.ToInt64(cmd.ExecuteScalar());
                    }

                    // Step 3: Insert item into `order_items` table using the retrieved `order_id`
                    string orderItemQuery = "INSERT INTO `order_items` (order_id, item_id, item_type, quantity, price) " +
                                            "VALUES (@OrderId, @ItemId, @ItemType, @Quantity, @Price)";
                    using (MySqlCommand cmd = new MySqlCommand(orderItemQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@OrderId", orderId); // Use the retrieved order ID
                        cmd.Parameters.AddWithValue("@ItemId", _customerId); // Replace with actual item ID
                        cmd.Parameters.AddWithValue("@ItemType", "CarPart"); // Replace with actual item type
                        cmd.Parameters.AddWithValue("@Quantity", _quantity); // Correct quantity
                        cmd.Parameters.AddWithValue("@Price", _price);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Order is on the way!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error placing order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Utility method to convert Image to byte array
        private byte[] ImageToByteArray(Image image)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }
    }
}
