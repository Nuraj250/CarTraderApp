using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CarTraderApp
{
    public partial class OrderHistoryControl : UserControl
    {
        private readonly string _connectionString = "Server=localhost;Database=car_trader;Uid=root;Pwd=Admin;";
        private readonly int _userId;
        private DataGridView _dataGridViewOrders;
        private TextBox _txtSearch;
        private Button _btnDownloadAll;
        private Button _btnNewOrder;

        public OrderHistoryControl(int userId)
        {
            _userId = userId;
            InitializeComponent();
            InitializeComponents();
            LoadOrderHistory();
            _dataGridViewOrders.CellClick += DataGridViewOrders_CellClick;
        }

        // Displays the content of a specific order in a new form.
        private void ShowOrderContent(string orderNumber)
        {
            Form orderContentForm = new Form
            {
                Text = $"Order Content for {orderNumber}",
                Size = new Size(600, 400),
                StartPosition = FormStartPosition.CenterParent
            };

            DataGridView dataGridViewOrderContent = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false
            };

            orderContentForm.Controls.Add(dataGridViewOrderContent);
            LoadOrderContent(orderNumber, dataGridViewOrderContent);
            orderContentForm.ShowDialog();
        }

        // Loads the content of an order into a DataGridView.
        private void LoadOrderContent(string orderNumber, DataGridView dataGridView)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    const string query = @"
                        SELECT 
                            oi.item_id AS `Item ID`,
                            oi.item_type AS `Item Type`,
                            oi.quantity AS `Quantity`,
                            oi.price AS `Price`,
                            (oi.quantity * oi.price) AS `Total Price`
                        FROM order_items oi
                        INNER JOIN orders o ON oi.order_id = o.id
                        WHERE o.order_number = @OrderNumber";

                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@OrderNumber", orderNumber);
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dataGridView.DataSource = dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowCustomMessageBox($"Error loading order content: {ex.Message}", "Error", true);
                }
            }
        }

        // Handles the CellClick event for the DataGridView showing orders.
        private void DataGridViewOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string orderNumber = _dataGridViewOrders.Rows[e.RowIndex].Cells["Order Number"].Value.ToString();
                ShowOrderContent(orderNumber);
            }
        }

        // Initializes the design of components for the form.
        private void InitializeComponents()
        {
            _dataGridViewOrders = new DataGridView
            {
                Location = new Point(80, 120),
                Width = 1700,
                Height = 800,
                Margin = new Padding(20),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                ColumnHeadersHeight = 40,
                RowTemplate = { Height = 50 },
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                EnableHeadersVisualStyles = false,
                BackgroundColor = Color.White
            };

            ConfigureDataGridView();

            _txtSearch = new TextBox
            {
                Location = new Point(20, 20),
                Width = 200,
                Font = new Font("Arial", 10)
            };
            _txtSearch.TextChanged += TxtSearch_TextChanged;

            Controls.Add(_dataGridViewOrders);
            Controls.Add(_txtSearch);
        }

        // Configures styles for the DataGridView.
        private void ConfigureDataGridView()
        {
            _dataGridViewOrders.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            _dataGridViewOrders.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            _dataGridViewOrders.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            _dataGridViewOrders.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            _dataGridViewOrders.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            _dataGridViewOrders.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            _dataGridViewOrders.DefaultCellStyle.Font = new Font("Arial", 11);
            _dataGridViewOrders.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 200, 200);
            _dataGridViewOrders.DefaultCellStyle.SelectionForeColor = Color.Black;
            _dataGridViewOrders.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            _dataGridViewOrders.DefaultCellStyle.Padding = new Padding(10, 0, 10, 0);

            _dataGridViewOrders.GridColor = Color.LightGray;
        }

        // Loads the order history for the user
        private void LoadOrderHistory()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    const string query = @"
                        SELECT 
                            `orders`.`order_number` AS `Order Number`,
                            `orders`.`customer_id` AS `Customer ID`,
                            `orders`.`order_date` AS `Order Date`,
                            `orders`.`status` AS `Status`,
                            `orders`.`total_amount` AS `Total Amount`
                        FROM `car_trader`.`orders`
                        WHERE `orders`.`is_deleted` = 0 
                        AND `orders`.`customer_id` = @UserId";

                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserId", _userId);
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            _dataGridViewOrders.DataSource = dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowCustomMessageBox($"Error loading order history: {ex.Message}", "Error", true);
                }
            }
        }

        // Handles text changes in the search box to filter the DataGridView.
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = _txtSearch.Text;
            (_dataGridViewOrders.DataSource as DataTable).DefaultView.RowFilter = string.IsNullOrEmpty(searchText)
                ? string.Empty
                : $"[Order Number] LIKE '%{searchText}%'";
        }

        // Displays a custom message box.
        private void ShowCustomMessageBox(string message, string title, bool hideOkButton)
        {
            var customMessageBox = new CustomMessageBox(title, message);
            if (hideOkButton) customMessageBox.HideOkButton();
            customMessageBox.ShowDialog();
        }
    }
}
