using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace CarTraderApp
{
    public partial class ManageOrdersControl : UserControl
    {
        private readonly string _connStr = "Server=localhost;Database=car_trader;Uid=root;Pwd=Admin;";
        private readonly string _placeholder = "Search by order number...";
        private readonly int _userId;

        public ManageOrdersControl(int userId, bool isCustomer)
        {
            InitializeComponent();
            _userId = userId;
            InitializePlaceholders();
            AttachEventHandlers();
            LoadOrdersData();
        }

        // Initializes the placeholder text for search input.
        private void InitializePlaceholders()
        {
            SetPlaceholder(txtSearchOrders, _placeholder);
        }

        // Attaches event handlers for various UI elements.
        private void AttachEventHandlers()
        {
            txtSearchOrders.Enter += RichTextBox_Enter;
            txtSearchOrders.Leave += RichTextBox_Leave;
            txtSearchOrders.TextChanged += txtSearchOrders_TextChanged;
            dataGridViewOrders.CellClick += dataGridViewOrders_CellClick;
        }

        // Sets placeholder text in a RichTextBox.
        private void SetPlaceholder(RichTextBox textBox, string placeholderText)
        {
            textBox.Text = placeholderText;
            textBox.ForeColor = Color.Gray;
        }

        // Loads order data from the database and populates the DataGridView.
        private void LoadOrdersData()
        {
            using (MySqlConnection connection = new MySqlConnection(_connStr))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT id, order_number, customer_id, order_date, status, total_amount FROM orders WHERE is_deleted = 0";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable ordersTable = new DataTable();
                        adapter.Fill(ordersTable);
                        dataGridViewOrders.DataSource = ordersTable;

                        AddActionColumns();
                        ConfigureDataGridView();
                    }
                }
                catch (Exception ex)
                {
                    ShowCustomMessageBox($"Error loading orders: {ex.Message}", "Error", true);
                }
            }
        }

        // Adds action buttons (View, Delete, Update Status) to the DataGridView.
        private void AddActionColumns()
        {
            AddButtonColumn("View", "View");
            AddButtonColumn("Delete", "Delete");
            AddButtonColumn("Update Status", "Update Status");
        }

        // Adds a button column to the DataGridView.
        private void AddButtonColumn(string columnName, string buttonText)
        {
            if (!dataGridViewOrders.Columns.Contains(columnName))
            {
                DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn
                {
                    Name = columnName,
                    HeaderText = buttonText,
                    Text = buttonText,
                    UseColumnTextForButtonValue = true,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                };
                dataGridViewOrders.Columns.Add(buttonColumn);
            }
        }

        // Configures the DataGridView appearance and properties.
        private void ConfigureDataGridView()
        {
            dataGridViewOrders.ReadOnly = true;
            dataGridViewOrders.RowHeadersVisible = false;
            dataGridViewOrders.AutoGenerateColumns = true;
            dataGridViewOrders.RowTemplate.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dataGridViewOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewOrders.ColumnHeadersHeight = 50;

            var columns = new[]
            {
                new { Name = "id", Header = "Order ID", Width = 100 },
                new { Name = "order_number", Header = "Order Number", Width = 250 },
                new { Name = "customer_id", Header = "Customer ID", Width = 150 },
                new { Name = "order_date", Header = "Order Date", Width = 250 },
                new { Name = "status", Header = "Status", Width = 200 },
                new { Name = "total_amount", Header = "Total Amount", Width = 226 }
            };

            foreach (var col in columns)
            {
                dataGridViewOrders.Columns[col.Name].HeaderText = col.Header;
                dataGridViewOrders.Columns[col.Name].Width = col.Width;
                dataGridViewOrders.Columns[col.Name].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridViewOrders.Columns[col.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewOrders.Columns[col.Name].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            dataGridViewOrders.Columns["id"].Visible = false; // Hide ID column
            dataGridViewOrders.RowTemplate.Height = 60;
        }

        // Handles cell clicks in the DataGridView to perform actions like View, Delete, and Update Status.
        private void dataGridViewOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int orderId = Convert.ToInt32(dataGridViewOrders.Rows[e.RowIndex].Cells["id"].Value);
                string actionType = dataGridViewOrders.Columns[e.ColumnIndex].HeaderText;
                ExecuteAction(actionType, orderId);
            }
        }

        // Executes the specified action based on the action type.
        private void ExecuteAction(string actionType, int orderId)
        {
            if (actionType == "View")
            {
                ShowViewOrderPopup(orderId); // Show the popup for viewing order details.
            }
            else if (actionType == "Delete")
            {
                var confirmResult = MessageBox.Show("Are you sure to delete this order?", "Confirm Delete!", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    MarkOrderAsDeleted(orderId);
                    LoadOrdersData(); // Refresh data after deletion
                }
            }
            else if (actionType == "Update Status")
            {
                ShowStatusUpdatePopup(orderId); // Show the popup for updating the status.
            }
        }

        // Displays a popup with order details.
        private void ShowViewOrderPopup(int orderId)
        {
            Panel viewOrderPanel = new Panel
            {
                Size = new Size(350, 200),
                Location = new Point(this.Width / 2 - 175, this.Height / 2 - 100),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Padding = new Padding(20),
            };

            Label actionLabel = new Label
            {
                Text = "View Order",
                Location = new Point(10, 10),
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 50)
            };

            Button closeButton = new Button
            {
                Text = "Close",
                Location = new Point(160, 140),
                Width = 100,
                Height = 35,
                Font = new Font("Arial", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(200, 0, 0),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0, MouseOverBackColor = Color.FromArgb(170, 0, 0) }
            };
            closeButton.Click += (s, e) => this.Controls.Remove(viewOrderPanel);

            viewOrderPanel.Controls.Add(actionLabel);
            viewOrderPanel.Controls.Add(closeButton);

            StringBuilder orderDetails = GetOrderDetails(orderId);

            Label detailsLabel = new Label
            {
                Text = orderDetails.ToString(),
                Location = new Point(10, 50),
                AutoSize = true,
                Font = new Font("Arial", 10),
                ForeColor = Color.Black,
                MaximumSize = new Size(300, 0),
            };
            viewOrderPanel.Controls.Add(detailsLabel);

            viewOrderPanel.Height = detailsLabel.Bottom + 50;

            this.Controls.Add(viewOrderPanel);
            viewOrderPanel.BringToFront();
        }

        // Displays a popup for updating the order status.
        private void ShowStatusUpdatePopup(int orderId)
        {
            Panel statusUpdatePanel = new Panel
            {
                Size = new Size(300, 200),
                Location = new Point(this.Width / 2 - 150, this.Height / 2 - 100),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Padding = new Padding(20),
            };

            Label statusLabel = new Label
            {
                Text = "Select New Status:",
                Location = new Point(10, 10),
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 50)
            };

            ComboBox statusDropdown = new ComboBox
            {
                Location = new Point(10, 50),
                Width = 260,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Arial", 10),
                FlatStyle = FlatStyle.Flat
            };
            statusDropdown.Items.AddRange(new string[] { "Delivering", "Delivered", "Pending", "Cancelled" });

            Button updateButton = new Button
            {
                Text = "Update Status",
                Location = new Point(10, 90),
                Width = 120,
                Height = 35,
                Font = new Font("Arial", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleCenter,
                FlatAppearance = { BorderSize = 0, MouseOverBackColor = Color.FromArgb(0, 100, 190) }
            };
            updateButton.Click += (s, e) =>
            {
                if (statusDropdown.SelectedItem != null)
                {
                    UpdateOrderStatus(orderId, statusDropdown.SelectedItem.ToString());
                    this.Controls.Remove(statusUpdatePanel); // Remove panel after updating status
                }
                else
                {
                    MessageBox.Show("Please select a status.");
                }
            };

            Button closeButton = new Button
            {
                Text = "Close",
                Location = new Point(140, 90),
                Width = 100,
                Height = 35,
                Font = new Font("Arial", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(200, 0, 0),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0, MouseOverBackColor = Color.FromArgb(170, 0, 0) }
            };
            closeButton.Click += (s, e) => this.Controls.Remove(statusUpdatePanel);

            statusUpdatePanel.Controls.Add(statusLabel);
            statusUpdatePanel.Controls.Add(statusDropdown);
            statusUpdatePanel.Controls.Add(updateButton);
            statusUpdatePanel.Controls.Add(closeButton);

            this.Controls.Add(statusUpdatePanel);
            statusUpdatePanel.BringToFront();
        }

        // Retrieves the details of a specific order.
        private StringBuilder GetOrderDetails(int orderId)
        {
            StringBuilder orderDetails = new StringBuilder();

            using (MySqlConnection connection = new MySqlConnection(_connStr))
            {
                try
                {
                    connection.Open();
                    string query = @"
                    SELECT oi.item_type, oi.item_id, oi.quantity, oi.price, o.status
                    FROM order_items oi
                    INNER JOIN orders o ON o.id = oi.order_id
                    WHERE oi.order_id = @orderId AND o.is_deleted = 0";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@orderId", orderId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    orderDetails.AppendLine($"Item Type: {reader["item_type"]}");
                                    orderDetails.AppendLine($"Item ID: {reader["item_id"]}");
                                    orderDetails.AppendLine($"Quantity: {reader["quantity"]}");
                                    orderDetails.AppendLine($"Price: {reader["price"]}");
                                    orderDetails.AppendLine($"Status: {reader["status"]}");
                                    orderDetails.AppendLine(); // Add an empty line for separation
                                }
                            }
                            else
                            {
                                orderDetails.AppendLine("No order details found.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    orderDetails.AppendLine($"Error retrieving order details: {ex.Message}");
                }
            }

            return orderDetails;
        }

        // Marks an order as deleted and removes associated items.
        private void MarkOrderAsDeleted(int orderId)
        {
            using (MySqlConnection connection = new MySqlConnection(_connStr))
            {
                try
                {
                    connection.Open();
                    ExecuteQuery(connection, "DELETE FROM order_items WHERE order_id = @orderId", orderId);
                    ExecuteQuery(connection, "DELETE FROM orders WHERE id = @orderId", orderId);

                    ShowCustomMessageBox("Order and its items deleted successfully!", "Success", true);
                }
                catch (Exception ex)
                {
                    ShowCustomMessageBox($"Error deleting order: {ex.Message}", "Error", true);
                }
            }
        }

        // Executes a non-query command with a parameterized query.
        private void ExecuteQuery(MySqlConnection connection, string query, int orderId)
        {
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@orderId", orderId);
                cmd.ExecuteNonQuery();
            }
        }

        // Updates the status of the selected order.
        private void UpdateOrderStatus(int orderId, string newStatus)
        {
            if (string.IsNullOrEmpty(newStatus))
            {
                MessageBox.Show("Please select a status.");
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(_connStr))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE orders SET status = @status WHERE id = @orderId AND is_deleted = 0";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@status", newStatus);
                        cmd.Parameters.AddWithValue("@orderId", orderId);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            ShowCustomMessageBox("Order status updated successfully!", "Success", true);
                            LoadOrdersData(); // Refresh data after updating
                        }
                        else
                        {
                            ShowCustomMessageBox("No matching order found to update.", "Error", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowCustomMessageBox($"Error updating order status: {ex.Message}", "Error", true);
                }
            }
        }

        // Filters orders based on search input.
        private void txtSearchOrders_TextChanged(object sender, EventArgs e)
        {
            string searchValue = txtSearchOrders.Text.Trim();
            if (string.IsNullOrEmpty(searchValue))
            {
                LoadOrdersData();
            }
            else
            {
                var ordersTable = (DataTable)dataGridViewOrders.DataSource;
                var dv = ordersTable.DefaultView;
                dv.RowFilter = $"order_number LIKE '%{searchValue}%'";
                dataGridViewOrders.DataSource = dv.ToTable();
            }
        }

        // Handles the Enter event for RichTextBox to clear placeholder text.
        private void RichTextBox_Enter(object sender, EventArgs e)
        {
            RichTextBox textBox = sender as RichTextBox;
            if (textBox != null && textBox.Text == _placeholder)
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
            }
        }

        // Handles the Leave event for RichTextBox to set placeholder text.
        private void RichTextBox_Leave(object sender, EventArgs e)
        {
            RichTextBox textBox = sender as RichTextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = _placeholder;
                textBox.ForeColor = Color.Gray;
            }
        }

        // Displays a custom message box with optional OK button hiding.
        private void ShowCustomMessageBox(string message, string title, bool hideOkButton)
        {
            CustomMessageBox customMessageBox = new CustomMessageBox(title, message);
            if (hideOkButton)
            {
                customMessageBox.HideOkButton(); // Hide the OK button if there's a warning
            }
            customMessageBox.ShowDialog();
        }
    }
}
