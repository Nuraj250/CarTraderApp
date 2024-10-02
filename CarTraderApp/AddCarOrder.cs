using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CarTraderApp
{
    public partial class AddCarOrder : UserControl
    {
        private readonly string connectionString = "Server=localhost;Database=car_trader;Uid=root;Pwd=Admin;";
        private readonly string[] placeholders = { "Search Here..." };
        private readonly float placeholderFontSize = 12f;
        private readonly float inputFontSize = 12f;
        private CartControl cartControl;
        private readonly int _userId;
        private Panel overlayPanel;
        private readonly Color customColor = Color.FromArgb(0, 140, 153);

        public AddCarOrder(bool isCar, int userId)
        {
            InitializeComponent();
            InitializePlaceholders();
            _userId = userId;
            cartControl = new CartControl(userId);
            this.Controls.Add(cartControl);
            cartControl.Location = new Point(this.Width - cartControl.Width, 0); // Adjust location
            cartControl.Visible = false;

            ConfigureFlowLayoutPanel();
            InitializeOverlayPanel();

            LoadItems(isCar);
        }

        private void InitializePlaceholders()
        {
            SetPlaceholder(txtSearch, placeholders[0]);
            txtSearch.Multiline = true; // Ensure multiline is enabled
            //txtSearch.Align = HorizontalAlignment.Center; // Align text horizontally if needed
            txtSearch.SelectionAlignment = HorizontalAlignment.Left; // Align text horizontally
            txtSearch.Padding = new Padding(0, txtSearch.Height - 20, 0, 0); // Add padding to push text to bottom
        }

        private void AttachEventHandlers()
        {
            txtSearch.TextChanged += txtSearch_TextChanged;
        }

        private void SetPlaceholder(RichTextBox textBox, string placeholderText)
        {
            textBox.Text = placeholderText;
            textBox.ForeColor = Color.Gray;
            textBox.Multiline = true; // Enable multiline to support padding
            textBox.Padding = new Padding(0, textBox.Height - 20, 0, 0); // Adjust the padding to move text to bottom
        }
        private void InitializeOverlayPanel()
        {
            overlayPanel = new Panel
            {
                Size = this.Size,
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(200, Color.Black), // Semi-transparent background
                Visible = false, // Initially hidden
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            this.Controls.Add(overlayPanel);
        }

        private void ShowBuyNowControl(string itemId, string itemType, string itemName, decimal price, int quantity)
        {

            BuyNowControl buyNowControl = new BuyNowControl(_userId, itemName, price, null, quantity);
            int xPos = (this.ClientSize.Width - buyNowControl.Width) / 2;
            int yPos = (this.ClientSize.Height - buyNowControl.Height) / 2;

            buyNowControl.Location = new Point(xPos, yPos);

            this.Controls.Add(buyNowControl);

            buyNowControl.BringToFront();
        }
        private void ConfigureFlowLayoutPanel()
        {
            flowLayoutPanelViewCars.AutoSize = false;
            flowLayoutPanelViewCars.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanelViewCars.WrapContents = true; // Enable wrapping to fit within the panel width
            flowLayoutPanelViewCars.Padding = new Padding(40, 0, 0, 0); // Initial padding, will be adjusted dynamically

            // Enable scrolling
            flowLayoutPanelViewCars.AutoScroll = true; // Enable scrolling
            flowLayoutPanelViewCars.VerticalScroll.Visible = true; // Make vertical scrollbar visible
            flowLayoutPanelViewCars.HorizontalScroll.Visible = false; // Hide horizontal scrollbar if not needed
        }

        private void LoadItems(bool isCar)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    if (isCar)
                    {
                        // Query to get cars
                        string carQuery = "SELECT id, 'Car' AS item_type, CONCAT(make, ' ', model, ' ', year) AS name, price, image FROM cars WHERE is_deleted = FALSE";
                        LoadItemsFromQuery(carQuery, connection);
                    }
                    else
                    {
                        // Query to get parts
                        string partQuery = "SELECT Id AS id, 'CarPart' AS item_type, PartName AS name, Price AS price, Image AS image FROM parts WHERE is_deleted = FALSE";
                        LoadItemsFromQuery(partQuery, connection);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading items: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadItemsFromQuery(string query, MySqlConnection connection)
        {
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (!reader.HasRows)
                {
                    MessageBox.Show("No items found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; // Exit if no data is returned
                }

                while (reader.Read())
                {
                    Panel itemPanel = CreateItemPanel(reader);
                    flowLayoutPanelViewCars.Controls.Add(itemPanel);
                }
            }
        }

        private Panel CreateItemPanel(MySqlDataReader reader)
        {
            Panel panel = new Panel
            {
                Width = 280,
                Height = 400,
                Margin = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Safe extraction of values
            string id = reader["id"]?.ToString() ?? "0";
            string itemType = reader["item_type"]?.ToString() ?? "Unknown";
            string name = reader["name"]?.ToString() ?? "No Name";
            decimal price = reader["price"] != DBNull.Value ? Convert.ToDecimal(reader["price"]) : 0;

            // Load the item image
            PictureBox pictureBox = new PictureBox
            {
                Width = 260,
                Height = 260,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = (reader["image"] != DBNull.Value) ? ByteArrayToImage((byte[])reader["image"]) : null
            };

            // Create a label for the name (under the image)
            Label lblName = new Label
            {
                Text = name,
                AutoSize = true,
                Font = new Font("Arial", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Create a label for the price
            Label lblPrice = new Label
            {
                Text = "LKR " + price.ToString(),
                AutoSize = true,
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = customColor,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Quantity selector for Car Parts (only visible for car parts)
            NumericUpDown quantitySelector = new NumericUpDown
            {
                Minimum = 1,
                Maximum = 100,
                Value = 1,
                Width = 60,
                Visible = itemType == "CarPart",
                TextAlign = HorizontalAlignment.Center,
                Font = new Font("Arial", 10)
            };

            pictureBox.Click += (sender, e) => Panel_Click(sender, e, id, itemType, name, price, 1); // Default quantity as 1 for images

            // Create the "Add to Cart" button
            Button btnAddToCart = new Button
            {
                Text = "Add to Cart",
                Width = 130,
                Height = 40,
                BackColor = Color.White,
                ForeColor = customColor,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            btnAddToCart.FlatAppearance.BorderSize = 1;
            btnAddToCart.FlatAppearance.BorderColor = customColor;
            btnAddToCart.Click += (sender, e) => AddToCart(id, itemType, name, price, (int)quantitySelector.Value);

            // Create the "Buy Now" button
            Button btnBuyNow = new Button
            {
                Text = "Buy Now",
                Width = 130,
                Height = 40,
                BackColor = customColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            btnBuyNow.FlatAppearance.BorderSize = 0;
            btnBuyNow.Click += (sender, e) =>
            {
                int quantity = 1; // Default to 1 if not specified

                // Check if the item is a car part and has a quantity selector
                if (itemType == "CarPart")
                {
                    quantity = (int)quantitySelector.Value;
                }

                BuyNow(id, itemType, name, price, quantity);
            };
            // Add controls to panel
            panel.Controls.Add(pictureBox);
            panel.Controls.Add(lblName);
            panel.Controls.Add(lblPrice);

            // Position the lblName under the picture box
            pictureBox.Location = new Point(10, 10);
            lblName.Location = new Point((panel.Width - lblName.Width) / 2, pictureBox.Bottom + 10);

            // Position the lblPrice under the name label
            lblPrice.Location = new Point((panel.Width - lblPrice.Width) / 2, lblName.Bottom + 5);

            // Only show quantity selector and position it if the item is a car part
            if (itemType == "CarPart")
            {
                panel.Height += 40; // Increase panel height by 40 pixels for car parts
                panel.Controls.Add(quantitySelector);
                quantitySelector.Location = new Point((panel.Width - quantitySelector.Width) / 2, lblPrice.Bottom + 10);
                btnAddToCart.Location = new Point((panel.Width - btnAddToCart.Width - btnBuyNow.Width - 10) / 2, quantitySelector.Bottom + 10);
            }
            else
            {
                btnAddToCart.Location = new Point((panel.Width - btnAddToCart.Width - btnBuyNow.Width - 10) / 2, lblPrice.Bottom + 10);
            }

            panel.Controls.Add(btnAddToCart);
            panel.Controls.Add(btnBuyNow);
            btnBuyNow.Location = new Point(btnAddToCart.Right + 10, btnAddToCart.Top);

            return panel;
        }

        private void Panel_Click(object sender, EventArgs e, string id, string itemType, string name, decimal price, int quantity)
        {
            // Fetch and display item details from the database
            var itemDetails = FetchItemDetailsFromDatabase(id, itemType);
            if (itemDetails != null)
            {
                if (itemType == "Car")
                {
                    // Display the fetched details for a car
                    string detailsMessage = $"Make: {itemDetails["Make"]}\n" +
                                            $"Model: {itemDetails["Model"]}\n" +
                                            $"Year: {itemDetails["Year"]}\n" +
                                            $"Price: LKR {itemDetails["Price"]}\n" +
                                            $"Mileage: {itemDetails["Mileage"]}\n" +
                                            $"Color: {itemDetails["Color"]}\n" +
                                            $"Transmission: {itemDetails["Transmission"]}";

                    MessageBox.Show(detailsMessage, "Car Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Display the fetched details for a car part
                    string detailsMessage = $"Name: {itemDetails["Name"]}\n" +
                                            $"Price: LKR {itemDetails["Price"]}\n" +
                                            $"Manufacturer: {itemDetails["Manufacturer"]}\n" +
                                            $"Description: {itemDetails["Description"]}";

                    MessageBox.Show(detailsMessage, "Car Part Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Unable to fetch item details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Dictionary<string, string> FetchItemDetailsFromDatabase(string itemId, string itemType)
        {
            Dictionary<string, string> itemDetails = new Dictionary<string, string>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query;

                    if (itemType == "Car")
                    {
                        query = @"SELECT id, make, model, year, price, mileage, color, transmission, is_deleted, created_at, buying_price
                          FROM cars 
                          WHERE id = @ItemId AND is_deleted = FALSE";
                    }
                    else
                    {
                        query = @"SELECT Id AS id, PartName AS name, Price AS price, Manufacturer AS manufacturer, Description AS description 
                          FROM parts 
                          WHERE Id = @ItemId AND is_deleted = FALSE";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@ItemId", itemId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Fill the dictionary with item details for a car
                                if (itemType == "Car")
                                {
                                    itemDetails["Id"] = reader["id"].ToString();
                                    itemDetails["Make"] = reader["make"].ToString();
                                    itemDetails["Model"] = reader["model"].ToString();
                                    itemDetails["Year"] = reader["year"].ToString();
                                    itemDetails["Price"] = reader["price"].ToString();
                                    itemDetails["Mileage"] = reader["mileage"].ToString();
                                    itemDetails["Color"] = reader["color"].ToString();
                                    itemDetails["Transmission"] = reader["transmission"].ToString();
                                    itemDetails["Created At"] = reader["created_at"].ToString();
                                    itemDetails["Buying Price"] = reader["buying_price"].ToString();
                                }
                                else // Fill the dictionary with item details for a car part
                                {
                                    itemDetails["Id"] = reader["id"].ToString();
                                    itemDetails["Name"] = reader["name"].ToString();
                                    itemDetails["Price"] = reader["price"].ToString();
                                    itemDetails["Manufacturer"] = reader["manufacturer"]?.ToString() ?? "N/A";
                                    itemDetails["Description"] = reader["description"]?.ToString() ?? "N/A";
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching item details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }

            return itemDetails;
        }

        private Image ByteArrayToImage(byte[] byteArray)
        {
            using (var ms = new System.IO.MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }

        private void AddToCart(string itemId, string itemType, string itemName, decimal price, int quantity)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO cart (user_id, item_id, item_type, quantity) VALUES (@UserId, @ItemId, @ItemType, @Quantity)";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserId", _userId); // Assuming you have userId available
                        cmd.Parameters.AddWithValue("@ItemId", itemId);
                        cmd.Parameters.AddWithValue("@ItemType", itemType);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show($"{itemType} '{itemName}' added to cart!", "Add to Cart", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding item to cart: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BuyNow(string itemId, string itemType, string itemName, decimal price, int quantity)
        {
            ShowBuyNowControl(itemId, itemType, itemName, price, quantity);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.ToLower();

            foreach (Panel itemPanel in flowLayoutPanelViewCars.Controls)
            {
                Label nameLabel = itemPanel.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.Font.Bold); // Assuming the name label is bold
                if (nameLabel != null)
                {
                    itemPanel.Visible = nameLabel.Text.ToLower().Contains(searchText);
                }
            }
        }

        private void RichTextBox_Enter(object sender, EventArgs e)
        {
            RichTextBox textBox = sender as RichTextBox;
            if (textBox != null && textBox.Text == placeholders[0])
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
            }
        }

        private void RichTextBox_Leave(object sender, EventArgs e)
        {
            RichTextBox textBox = sender as RichTextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = placeholders[0];
                textBox.ForeColor = Color.Gray;
            }
        }
    }
}
