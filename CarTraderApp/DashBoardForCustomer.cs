using Microsoft.VisualBasic.ApplicationServices;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CarTraderApp
{
    public partial class DashBoardForCustomer : Form
    {
        private readonly string connectionString = "Server=localhost;Database=car_trader;Uid=root;Pwd=Admin;";
        private string _username;
        private string _userType;
        private string _email;
        private byte[] _userImage;

        public DashBoardForCustomer(string userType, string username, string email, byte[] userImage)
        {
            InitializeComponent();
            _userType = userType;
            _username = username;
            _email = email;
            _userImage = userImage;

            SetDefaultView();
        }

        private void DashBoardForCustomer_Load(object sender, EventArgs e)
        {
            SetDefaultView();
        }

        private void SetDefaultView()
        {
            int userId = GetUserIdByUsername(_username);
            // Load default control, for example, AddCarOrder
            LoadControl(new CustomerHomePage()); // Load the AddCarOrder control as default
        }

        private int GetUserIdByUsername(string username)
        {
            int userId = 0; // Default to -1 if not found
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT id FROM users WHERE username = @username";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            userId = Convert.ToInt32(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while retrieving user ID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return userId;
        }

        private void LoadControl(UserControl control)
        {
            // Clear the panel
            mainPanel.Controls.Clear();

            // Set up the control
            control.Dock = DockStyle.Fill;

            // Add the control to the panel
            mainPanel.Controls.Add(control);
        }

        private void btnCar_Click(object sender, EventArgs e)
        {
            int userId = GetUserIdByUsername(_username);
            LoadControl(new AddCarOrder(true, userId));
        }

        private void btnCarParts_Click(object sender, EventArgs e)
        {
            int userId = GetUserIdByUsername(_username);
            LoadControl(new AddCarOrder(false, userId));
        }

        private void btnCart_Click(object sender, EventArgs e)
        {
            int userId = GetUserIdByUsername(_username);
            LoadControl(new CartControl(userId));
        }

        private void btnPerchase_Click(object sender, EventArgs e)
        {
            int userId = GetUserIdByUsername(_username);
            LoadControl(new OrderHistoryControl(userId));
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            string email = _email;  // Assume you have a way to get the email address of the user
            byte[] imageBytes = _userImage;  // Assume you have a way to get the image bytes of the user

            Profile profile = new Profile(_username, email, imageBytes);
            profile.ShowDialog();
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            // Toggle between normal and maximized state
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal; // If maximized, restore to normal size
            }
            else
            {
                this.WindowState = FormWindowState.Maximized; // If normal, maximize the window
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //Application.Exit(); // Closes the entire application
            this.Hide();
            new Login().Show();
        }

        private void dashboardNav_Click(object sender, EventArgs e)
        {
            LoadControl(new CustomerHomePage());
        }
    }
}
