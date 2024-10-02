using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CarTraderApp
{
    public partial class DashBoard : Form
    {
        private readonly string connectionString = "Server=localhost;Database=car_trader;Uid=root;Pwd=Admin;";
        private readonly string _username;
        private readonly string _userType;
        private readonly string _email;
        private readonly byte[] _userImage;
        private readonly ToolTip toolTip = new ToolTip();

        public DashBoard(string userType, string username, string email, byte[] userImage)
        {
            InitializeComponent();
            _userType = userType;
            _username = username;
            _email = email;
            _userImage = userImage;

            // Event subscriptions
            this.Load += DashBoard_Load;
            this.FormBorderStyle = FormBorderStyle.None;
            btnClose.Click += btnClose_Click;
            btnMax.Click += btnMax_Click;

            InitializeUserInterface();
        }

        // Initializes the user interface components and sets the default state.
        private void InitializeUserInterface()
        {
            SetUserImage(_userImage);
            InitializeToolTip(btnView, "View User Profile");
            LoadNewDashboard();  // Load the new dashboard on start
        }

        // Loads the AdminDashboard user control into the main panel.
        private void LoadNewDashboard()
        {
            mainPanel.Controls.Clear();
            AdminDashboard newDashboard = new AdminDashboard { Dock = DockStyle.Fill };
            mainPanel.Controls.Add(newDashboard);
        }

        // Sets a tooltip for a given control.
        private void InitializeToolTip(Control control, string tooltipText)
        {
            // Customize the tooltip appearance
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 100;
            toolTip.ReshowDelay = 500;
            toolTip.BackColor = Color.LightYellow;
            toolTip.ForeColor = Color.Black;
            toolTip.ToolTipIcon = ToolTipIcon.Info;
            toolTip.IsBalloon = true;

            // Set the tooltip text for the given control
            toolTip.SetToolTip(control, tooltipText);
        }


        // Sets the user image in the picture box.
        private void SetUserImage(byte[] imageBytes)
        {
            if (imageBytes != null && imageBytes.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    pictureUser.Image = Image.FromStream(ms);
                    pictureUser.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            else
            {
                pictureUser.Image = Properties.Resources.icons8_user_64;
                pictureUser.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        // Updates the breadcrumb navigation label to show the current page.
        private void UpdateBreadcrumb(string currentPage)
        {
            labelNav.Text = $"Home / {currentPage}";
        }

        // Loads the dashboard and adjusts UI elements based on user type.
        private void DashBoard_Load(object sender, EventArgs e)
        {
            try
            {
                lblWelcome.Text = _userType == "Admin" ? $"Welcome back, Admin, {_username}!" : $"Welcome, {_username}!";

                if (_userType == "Customer")
                {
                    // Hide buttons and adjust text for Customer view
                    btnReports.Visible = false;
                    btnOrders.Visible = false;
                    btnCustomers.Text = "View Orders";
                    btnCarParts.Text = "Buy A Car Part";
                    btnCars.Text = "Buy A Car";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the dashboard: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Logs out the user and displays the login form.
        private void Logout_Click(object sender, EventArgs e)
        {
            this.Close(); // Close the dashboard form
            new Login().Show(); // Show the login form again
        }

        // Loads a user control into the main panel.
        private void LoadControl(UserControl control)
        {
            mainPanel.Controls.Clear();
            control.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(control);
        }

        // Gets the user ID by username from the database.
        private int GetUserIdByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return -1;

            int userId = -1;
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
                    MessageBox.Show($"An error occurred while retrieving user ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return userId;
        }

        // Minimizes the application window.
        private void btnAppMin_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;

        // Exits the application.
        private void btnAppClose_Click(object sender, EventArgs e) => Application.Exit();

        // Change button color on mouse enter for minimize button.
        private void btnAppMin_MouseEnter(object sender, EventArgs e) => btnAppMin.BackColor = Color.Blue;

        // Revert button color on mouse leave for minimize button.
        private void btnAppMin_MouseLeave(object sender, EventArgs e) => btnAppMin.BackColor = panel1.BackColor;

        // Change button color on mouse enter for close button.
        private void btnAppClose_MouseEnter(object sender, EventArgs e) => btnAppClose.BackColor = Color.Red;

        // Revert button color on mouse leave for close button.
        private void btnAppClose_MouseLeave(object sender, EventArgs e) => btnAppClose.BackColor = panel1.BackColor;

        // Loads the ManageCarParts control.
        private void btnCarParts_Click(object sender, EventArgs e)
        {
            LoadControl(new ManageCarPartsControl());
            UpdateBreadcrumb("Manage Car Parts");
        }

        // Loads the ManageCar control.
        private void btnCars_Click(object sender, EventArgs e)
        {
            LoadControl(new ManageCarControl());
            UpdateBreadcrumb("Manage Cars");
        }

        // Loads the ManageUser control.
        private void btnCustomers_Click(object sender, EventArgs e)
        {
            int userId = GetUserIdByUsername(_username);
            LoadControl(new ManageUserControl(userId));
            UpdateBreadcrumb("Manage Users");
        }

        // Loads the ManageOrders control.
        private void btnOrders_Click(object sender, EventArgs e)
        {
            int userId = GetUserIdByUsername(_username);
            LoadControl(new ManageOrdersControl(userId, false));
            UpdateBreadcrumb("Manage Orders");
        }

        // Loads the ManageReports control.
        private void btnReports_Click(object sender, EventArgs e)
        {
            LoadControl(new ManageReportsControl());
            UpdateBreadcrumb("Manage Reports");
        }

        // Closes the application.
        private void btnClose_Click(object sender, EventArgs e) => Application.Exit();

        // Toggles between maximized and normal window state.
        private void btnMax_Click(object sender, EventArgs e)
        {
            this.WindowState = this.WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
        }

        // Logs out the user and returns to the login screen.
        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Login().Show();
        }

        // Displays the user profile in a dialog.
        private void btnView_Click(object sender, EventArgs e)
        {
            Profile profile = new Profile(_username, _email, _userImage);
            profile.ShowDialog();
        }

        // Reloads the new dashboard.
        private void dashboardNav_Click(object sender, EventArgs e) => LoadNewDashboard();
    }
}
