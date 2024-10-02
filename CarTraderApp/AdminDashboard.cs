using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CarTraderApp
{
    public partial class AdminDashboard : UserControl
    {
        private readonly string connectionString = "Server=localhost;Database=car_trader;Uid=root;Pwd=Admin;";

        public AdminDashboard()
        {
            InitializeComponent();
            InitializeDashboard();
        }

        private void InitializeDashboard()
        {
            this.Dock = DockStyle.Fill; // Fill the parent container
            CreateHeroSection(); // Enhanced welcome section
            CreateSummaryCards();
        }

        private void CreateHeroSection()
        {
            Panel heroPanel = new Panel
            {
                Size = new Size(this.Width - 40, 150), // Adjust the width with some margin
                Location = new Point(20, 20), // Add margin to the left and top
                BackColor = Color.Teal
            };

            Label lblWelcome = new Label
            {
                Text = "Welcome to the Admin Dashboard!",
                Font = new Font("Arial", 28, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 20),
                AutoSize = true
            };

            Label lblOverview = new Label
            {
                Text = "Get an overview of your business performance and manage your inventory, orders, and users all in one place.",
                Font = new Font("Arial", 14, FontStyle.Regular),
                ForeColor = Color.White,
                Location = new Point(20, 80),
                AutoSize = true
            };

            heroPanel.Controls.Add(lblWelcome);
            heroPanel.Controls.Add(lblOverview);
            this.Controls.Add(heroPanel);
        }

        private void CreateSummaryCards()
        {
            // Fetch data from the database
            string totalSales = "LKR " + GetTotalSales().ToString("N2");
            string totalOrders = GetTotalOrders().ToString();
            string totalUsers = GetTotalUsers().ToString();
            string totalCars = GetTotalCars().ToString();
            string totalCarParts = GetTotalCarParts().ToString();

            // Adjust panel positions and sizes
            int panelWidth = (this.Width - 60) / 2; // Two panels per row, with margin
            int panelHeight = 150;

            // First row
            int firstRowY = 200; // Y position for the first row, below the hero section
            Panel panelSales = CreateSummaryPanel("Total Sales", totalSales, "This shows the total sales amount.", 20, firstRowY, panelWidth, panelHeight);
            Panel panelOrders = CreateSummaryPanel("Total Orders", totalOrders, "This represents the total number of orders.", 30 + panelWidth, firstRowY, panelWidth, panelHeight);

            // Second row
            int secondRowY = firstRowY + panelHeight + 20; // Y position for the second row
            Panel panelUsers = CreateSummaryPanel("Total Users", totalUsers, "This is the total count of registered users.", 20, secondRowY, panelWidth, panelHeight);
            Panel panelCars = CreateSummaryPanel("Total Cars", totalCars, "This indicates the total number of cars in inventory.", 30 + panelWidth, secondRowY, panelWidth, panelHeight);

            // Third row for remaining panel
            int thirdRowY = secondRowY + panelHeight + 20; // Y position for the third row
            Panel panelParts = CreateSummaryPanel("Total Car Parts", totalCarParts, "This shows the total number of car parts available.", 20, thirdRowY, panelWidth, panelHeight);

            this.Controls.Add(panelSales);
            this.Controls.Add(panelOrders);
            this.Controls.Add(panelUsers);
            this.Controls.Add(panelCars);
            this.Controls.Add(panelParts);
        }

        private Panel CreateSummaryPanel(string title, string value, string description, int x, int y, int width, int height)
        {
            Panel panel = new Panel
            {
                Size = new Size(width, height),
                Location = new Point(x, y),
                BackColor = Color.Teal,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblTitle = new Label
            {
                Text = title,
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(10, 10),
                AutoSize = true
            };

            Label lblValue = new Label
            {
                Text = value,
                Font = new Font("Arial", 20, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(10, 50),
                AutoSize = true
            };

            Label lblDescription = new Label
            {
                Text = description,
                Font = new Font("Arial", 10, FontStyle.Regular),
                ForeColor = Color.White,
                Location = new Point(10, 100),
                AutoSize = true
            };

            panel.Controls.Add(lblTitle);
            panel.Controls.Add(lblValue);
            panel.Controls.Add(lblDescription);

            return panel;
        }

        // Methods to fetch actual data from the database
        private decimal GetTotalSales()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT SUM(total_amount) FROM orders WHERE is_deleted = 0";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    object result = cmd.ExecuteScalar();
                    return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                }
            }
        }

        private int GetTotalOrders()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM orders WHERE is_deleted = 0";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        private int GetTotalUsers()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM users WHERE is_deleted = 0";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        private int GetTotalCars()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM cars WHERE is_deleted = 0";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        private int GetTotalCarParts()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM parts WHERE is_deleted = 0";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
    }
}
