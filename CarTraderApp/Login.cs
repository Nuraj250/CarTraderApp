using System.Security.Cryptography;
using System.Collections.Generic;
using System.ComponentModel;
using MySqlConnector;


namespace CarTraderApp
{
    public partial class Login : Form
    {
        string connectionString = "Server=localhost;Database=car_trader;Uid=root;Pwd=Admin;";
        private readonly string[] placeholders = { "UserName ", "Password " };
        private readonly float placeholderFontSize = 12f;
        private readonly float inputFontSize = 12f;
        public Login()
        {
            InitializeComponent();
            InitializePlaceholders();
            panel1.BackColor = Color.FromArgb(217, 255, 255, 255);
            this.Opacity = 0.85;

        }

        private void InitializePlaceholders()
        {
            txtUserName.Enter += TextBox_Enter;
            txtPassword.Leave += TextBox_Leave;

            SetPlaceholder(txtUserName, placeholders[0]);
            SetPlaceholder(txtPassword, placeholders[1]);
        }

        private void SetPlaceholder(TextBox textBox, string placeholderText)
        {
            textBox.Text = placeholderText;
            textBox.ForeColor = Color.Gray;
            textBox.Font = new Font(textBox.Font.FontFamily, placeholderFontSize, FontStyle.Bold);
            textBox.Tag = placeholderText; // Set the Tag property to store the placeholder text
        }

        private void RemovePlaceholder(TextBox textBox, string placeholderText)
        {
            if (textBox.Text == placeholderText)
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
                textBox.Font = new Font(textBox.Font.FontFamily, inputFontSize, FontStyle.Regular);

                // For password fields, ensure the PasswordChar is reset when typing starts
                if (textBox == txtPassword)
                {
                    txtPassword.PasswordChar = '●'; // Set to a dot or desired password character
                }
            }
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                // Remove placeholder if the textbox has it
                if (textBox.Text == (string)textBox.Tag)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                    textBox.Font = new Font(textBox.Font.FontFamily, inputFontSize, FontStyle.Regular);

                    // If it's the password field, set the PasswordChar
                    if (textBox == txtPassword)
                    {
                        txtPassword.PasswordChar = '●'; // Or any other character you prefer
                    }
                }
            }
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                // Reset placeholder if the textbox is empty
                textBox.Text = (string)textBox.Tag;
                textBox.ForeColor = Color.Gray;
                textBox.Font = new Font(textBox.Font.FontFamily, placeholderFontSize, FontStyle.Italic);

                // If it's the password field, reset the PasswordChar
                if (textBox == txtPassword)
                {
                    txtPassword.PasswordChar = '\0'; // Reset to nothing when showing placeholder
                }
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Check if username or password fields are empty
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ShowCustomMessageBox("Username and password fields cannot be empty. Please enter valid credentials.", "Error", true);
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Define the query to check the user's credentials
                    string query = "SELECT password, type, email, contact_number, image FROM Users WHERE Username = @username AND is_deleted = FALSE";
                    using (MySqlCommand mySqlCommand = new MySqlCommand(query, connection))
                    {
                        // Add parameters to the query to avoid SQL injection
                        mySqlCommand.Parameters.AddWithValue("@username", username);

                        // Execute the query
                        using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedHash = reader["password"].ToString();
                                string userType = reader["type"].ToString();
                                string email = reader["email"].ToString();
                                string contactNumber = reader["contact_number"].ToString(); // Get contact number
                                byte[] imageBytes = reader["image"] as byte[];

                                if (PasswordHelper.VerifyPassword(password, storedHash))
                                {
                                    // Check if the userType is admin or customer
                                    if (userType == "Admin")
                                    {
                                        // Redirect to the admin dashboard and pass the relevant parameters
                                        DashBoard adminDashboard = new DashBoard(userType, username, email, imageBytes);
                                        adminDashboard.Show();
                                        this.Hide();
                                    }
                                    else if (userType == "Customer")
                                    {

                                        // Redirect to the customer dashboard and pass the relevant parameters
                                        DashBoardForCustomer customerDashboard = new DashBoardForCustomer(userType, username, email, imageBytes);
                                        customerDashboard.Show();
                                        this.Hide();
                                    }
                                }
                                else
                                {
                                    ShowCustomMessageBox("Invalid password. Please try again.", "Error", true);
                                }
                            }
                            else
                            {
                                ShowCustomMessageBox("Username not found. Please try again.", "Error", true);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during the connection
                    MessageBox.Show("An error occurred: " + ex.Message, "Error");
                }
            }
        }
        private void label4_Click(object sender, EventArgs e)
        {
            Register registerForm = new Register();
            this.Hide();
            registerForm.Show();
        }
        private void ShowCustomMessageBox(string message, string title, bool hideOkButton)
        {
            CustomMessageBox customMessageBox = new CustomMessageBox(title, message);
            if (hideOkButton)
            {
                customMessageBox.HideOkButton(); // Hide the OK button if there's a warning
            }
            customMessageBox.ShowDialog();

        }

        private void lblForgot_Click(object sender, EventArgs e)
        {
            Form_ForgotPassword form_ForgotPassword = new Form_ForgotPassword();
            this.Hide();
            form_ForgotPassword.Show();
        }
    }
}
