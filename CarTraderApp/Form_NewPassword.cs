using MySql.Data.MySqlClient;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CarTraderApp
{
    public partial class Form_NewPassword : Form
    {
        private Label _lblTitle;
        private Label _lblEmail;
        private Label _lblNewPassword;
        private Label _lblConfirmPassword;
        private TextBox _txtEmail;
        private TextBox _txtNewPassword;
        private TextBox _txtConfirmPassword;
        private Button _btnConfirm;
        private Button _btnCancel;
        private readonly string _userEmail;
        string connectionString = "Server=localhost;Database=car_trader;Uid=root;Pwd=Admin;";


        public Form_NewPassword(string userEmail)
        {
            InitializeComponent();
            _userEmail = userEmail;
            InitializeNewPasswordForm();
        }

        // Initializes the form controls for setting a new password.
        private void InitializeNewPasswordForm()
        {
            // Set Form properties
            Text = "Enter New Password";
            Size = new Size(600, 400);
            StartPosition = FormStartPosition.CenterScreen;

            // Title Label
            _lblTitle = CreateLabel("Enter New Password", new Font("Arial", 16, FontStyle.Bold), new Point(200, 50));
            Controls.Add(_lblTitle);

            // Email Label
            _lblEmail = CreateLabel("Email", new Font("Arial", 10), new Point(180, 100));
            Controls.Add(_lblEmail);

            // Email TextBox
            _txtEmail = CreateTextBox(new Point(180, 130), _userEmail, false);
            Controls.Add(_txtEmail);

            // New Password Label
            _lblNewPassword = CreateLabel("Password", new Font("Arial", 10), new Point(180, 170));
            Controls.Add(_lblNewPassword);

            // New Password TextBox
            _txtNewPassword = CreatePasswordTextBox(new Point(180, 200));
            Controls.Add(_txtNewPassword);

            // Confirm Password Label
            _lblConfirmPassword = CreateLabel("Confirm Password", new Font("Arial", 10), new Point(180, 240));
            Controls.Add(_lblConfirmPassword);

            // Confirm Password TextBox
            _txtConfirmPassword = CreatePasswordTextBox(new Point(180, 270));
            Controls.Add(_txtConfirmPassword);

            // Confirm Button
            _btnConfirm = CreateButton("Confirm", new Point(280, 320), Color.DarkSlateGray, Color.White);
            _btnConfirm.Click += BtnConfirm_Click;
            Controls.Add(_btnConfirm);

            // Cancel Button
            _btnCancel = CreateButton("Cancel", new Point(150, 320), Color.Red, Color.White);
            _btnCancel.Click += BtnCancel_Click;
            Controls.Add(_btnCancel);
        }

        // Creates a label with the specified properties.
        private Label CreateLabel(string text, Font font, Point location)
        {
            return new Label
            {
                Text = text,
                Font = font,
                AutoSize = true,
                Location = location
            };
        }

        // Creates a standard TextBox.
        private TextBox CreateTextBox(Point location, string text = "", bool enabled = true)
        {
            return new TextBox
            {
                Width = 250,
                Location = location,
                Text = text,
                Enabled = enabled
            };
        }

        // Creates a TextBox for password input.
        private TextBox CreatePasswordTextBox(Point location)
        {
            return new TextBox
            {
                Width = 250,
                Location = location,
                PasswordChar = '*'
            };
        }

        // Creates a button with the specified properties.
        private Button CreateButton(string text, Point location, Color backColor, Color foreColor)
        {
            return new Button
            {
                Text = text,
                Font = new Font("Arial", 10, FontStyle.Bold),
                Size = new Size(100, 40),
                Location = location,
                BackColor = backColor,
                ForeColor = foreColor
            };
        }

        // Handles the Confirm button click event to update the user's password.
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            string newPassword = _txtNewPassword.Text;
            string confirmPassword = _txtConfirmPassword.Text;

            // Password regex pattern: at least one uppercase letter, one lowercase letter, one digit, one special character, and 8-20 characters long
            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$";
            if (!Regex.IsMatch(newPassword, passwordPattern))
            {
                ShowCustomMessageBox("Password must be 8-20 characters long,\n contain at least one uppercase letter,\n one lowercase letter,\n one digit,\n and one special character.", "Error", true);
                return;
            }

            if (newPassword != confirmPassword)
            {
                ShowCustomMessageBox("Passwords do not match. Please try again.", "Error", true);
                return;
            }

            bool isUpdated = UpdateUserPassword(_userEmail, newPassword);

            if (isUpdated)
            {
                ShowCustomMessageBox("Your password has been updated successfully.", "Success", true);
                Close();
                new Login().Show();
            }
            else
            {
                ShowCustomMessageBox("Failed to update password. Please try again.", "Error", true);
            }
        }

        // Handles the Cancel button click event to close the form.
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Updates the user's password in the database.
        private bool UpdateUserPassword(string email, string newPassword)
        {
            string hashedPassword = PasswordHelper.HashPassword(newPassword);

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "UPDATE Users SET Password = @password WHERE Email = @email";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@password", hashedPassword);

                        cmd.ExecuteNonQuery();
                    }

                    ShowCustomMessageBox("Registration successful!", "Success", false);
                    return true;
                }
                catch (Exception ex)
                {
                    ShowCustomMessageBox("An error occurred: " + ex.Message, "Error", true);
                    return false;
                }
            }
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
