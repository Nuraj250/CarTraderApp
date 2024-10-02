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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CarTraderApp
{
    public partial class Register : Form
    {

        string connectionString = "Server=localhost;Database=car_trader;Uid=root;Pwd=Admin;";
        private readonly string[] placeholders = { "Email ", "User Name ", "Password ", "Re-Enter Password " };
        private readonly float placeholderFontSize = 12f;
        private readonly float inputFontSize = 12f;

        public Register()
        {
            InitializeComponent();
            txtEmail.TextChanged += txtEmail_TextChanged;
            InitializePlaceholders();
        }

        private void InitializePlaceholders()
        {
            txtUserName.Enter += TextBox_Enter;
            txtUserName.Leave += TextBox_Leave;
            txtEmail.Enter += TextBox_Enter;
            txtEmail.Leave += TextBox_Leave;
            txtPassword.Enter += TextBox_Enter;
            txtPassword.Leave += TextBox_Leave;
            txtReEnterPassword.Enter += TextBox_Enter;
            txtReEnterPassword.Leave += TextBox_Leave;
            SetPlaceholder(txtEmail, placeholders[0]);
            SetPlaceholder(txtUserName, placeholders[1]);
            SetPlaceholder(txtPassword, placeholders[2]);
            SetPlaceholder(txtReEnterPassword, placeholders[3]);
        }

        private void SetPlaceholder(TextBox textBox, string placeholderText)
        {
            textBox.Text = placeholderText;
            textBox.ForeColor = Color.Gray;
            textBox.Font = new Font(textBox.Font.FontFamily, placeholderFontSize, FontStyle.Bold);
            textBox.Tag = placeholderText;
        }

        private void RemovePlaceholder(TextBox textBox, string placeholderText)
        {
            if (textBox.Text == placeholderText)
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
                textBox.Font = new Font(textBox.Font.FontFamily, inputFontSize, FontStyle.Regular);
            }
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                int index = Array.IndexOf(placeholders, textBox.Tag.ToString());
                if (index >= 0) RemovePlaceholder(textBox, placeholders[index]);
            }
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                int index = Array.IndexOf(placeholders, textBox.Tag.ToString());
                if (index >= 0) SetPlaceholder(textBox, placeholders[index]);
            }
        }
        private void BtnRegister_Click(object sender, EventArgs e)
        {

            string email = txtEmail.Text;
            string username = txtUserName.Text;
            string password = txtPassword.Text;
            string confirmPassword = txtReEnterPassword.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                ShowCustomMessageBox("All fields are required, including the role.", "Error", true);
                return;
            }

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                ShowCustomMessageBox("Please enter a valid email address.", "Error", true);
                return;
            }

            string usernamePattern = @"^[a-zA-Z0-9]{3,15}$";
            if (!Regex.IsMatch(username, usernamePattern))
            {
                ShowCustomMessageBox("Username must be 3-15 characters long\n and contain only letters and numbers.", "Error", true);
                return;
            }

            // Password regex pattern: at least one uppercase letter, one lowercase letter, one digit, one special character, and 8-20 characters long
            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$";
            if (!Regex.IsMatch(password, passwordPattern))
            {
                ShowCustomMessageBox("Password must be 8-20 characters long,\n contain at least one uppercase letter,\n one lowercase letter,\n one digit,\n and one special character.", "Error", true);
                return;
            }

            if (password != confirmPassword)
            {
                ShowCustomMessageBox("Passwords do not match.", "Error", true);
                return;
            }


            // Hash the password
            string hashedPassword = PasswordHelper.HashPassword(password);

            // Insert into the database
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "INSERT INTO users (email, username, password, type) VALUES (@email, @username, @password, 'Customer')";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", hashedPassword);

                        cmd.ExecuteNonQuery();
                    }

                    ShowCustomMessageBox("Registration successful!", "Success", false);
                    this.Close(); // Close the registration form after successful registration
                    new Login().Show();
                }
                catch (Exception ex)
                {
                    ShowCustomMessageBox("An error occurred: " + ex.Message, "Error", true);
                }
            }
        }

        private string GenerateUsernameFromEmail(string email)
        {
            string username = email.Split('@')[0];
            int suffix = 1;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                while (true)
                {
                    // Check if the username already exists
                    string query = "SELECT COUNT(*) FROM users WHERE username = @username";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        // If no user with the same username exists, exit the loop
                        if (count == 0)
                        {
                            break;
                        }

                        // If username exists, append a suffix and continue the loop
                        username = $"{email.Split('@')[0]}{suffix}";
                        suffix++;
                    }
                }
            }

            return username;
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            string email = txtEmail.Text;

            // Only generate the username if the email is not empty and contains an '@'
            if (!string.IsNullOrEmpty(email) && email.Contains("@"))
            {
                txtUserName.Text = GenerateUsernameFromEmail(email);
            }
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

        private void lblBackToLogin_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}