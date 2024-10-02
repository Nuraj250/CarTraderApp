using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CarTraderApp
{
    public partial class Form_OTP : Form
    {
        private readonly string _userEmail;
        private readonly string _connStr = "Server=localhost;Database=car_trader;Uid=root;Pwd=Admin;";

        // TextBoxes for OTP input
        private TextBox[] _otpTextBoxes;
        private Button _btnVerify;
        private Label _labelEmail;

        public Form_OTP(string email)
        {
            InitializeComponent();
            _userEmail = email;
            InitializeOTPForm();
        }

        // Initializes the OTP form with all necessary components.
        private void InitializeOTPForm()
        {
            // Form properties
            Text = "Enter OTP";
            Size = new Size(600, 400);
            StartPosition = FormStartPosition.CenterScreen;

            // Email label
            _labelEmail = CreateLabel(_userEmail, new Font("Arial", 10), new Point(240, 80));
            Controls.Add(_labelEmail);

            // Initialize OTP text boxes
            InitializeOtpTextBoxes();

            // Verify button
            _btnVerify = CreateButton("Verify", new Point(250, 250));
            _btnVerify.Click += BtnVerify_Click;
            Controls.Add(_btnVerify);

            Load += Form_OTP_Load;
        }

        // Creates a label with specified properties.
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

        // Initializes the OTP input text boxes.
        private void InitializeOtpTextBoxes()
        {
            _otpTextBoxes = new TextBox[6];
            int startX = 150;
            int startY = 150;
            int gap = 10;
            int txtWidth = 40;

            for (int i = 0; i < _otpTextBoxes.Length; i++)
            {
                _otpTextBoxes[i] = CreateOtpTextBox(startX + i * (txtWidth + gap), startY);
                Controls.Add(_otpTextBoxes[i]);
            }
        }

        // Creates a text box for OTP input.
        private TextBox CreateOtpTextBox(int x, int y)
        {
            var txtBox = new TextBox
            {
                Width = 40,
                Height = 40,
                Location = new Point(x, y),
                Font = new Font("Arial", 20, FontStyle.Bold),
                TextAlign = HorizontalAlignment.Center,
                MaxLength = 1 // Only allow one character
            };
            txtBox.KeyPress += OtpTextBox_KeyPress;
            txtBox.TextChanged += OtpTextBox_TextChanged;
            return txtBox;
        }

        // Creates a button with specified properties.
        private Button CreateButton(string text, Point location)
        {
            return new Button
            {
                Text = text,
                Font = new Font("Arial", 10, FontStyle.Bold),
                Size = new Size(100, 40),
                Location = location,
                BackColor = Color.DarkSlateGray,
                ForeColor = Color.White
            };
        }

        // Handles the form load event to set initial focus.
        private void Form_OTP_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer { Interval = 100 }; // Delay in milliseconds
            timer.Tick += (s, args) =>
            {
                timer.Stop();
                _otpTextBoxes[0].Focus();
            };
            timer.Start();
        }

        // Validates each OTP text box input to ensure it's a single digit and moves to the next box if filled.
        private void OtpTextBox_TextChanged(object sender, EventArgs e)
        {
            var currentTextBox = sender as TextBox;
            if (currentTextBox.Text.Length > 1)
            {
                currentTextBox.Text = currentTextBox.Text.Substring(0, 1);
                currentTextBox.Select(currentTextBox.Text.Length, 0);
            }

            if (currentTextBox.Text.Length == 1)
            {
                MoveToNextTextBox(currentTextBox);
            }
        }

        // Moves focus to the next OTP text box.
        private void MoveToNextTextBox(TextBox currentTextBox)
        {
            int currentIndex = Array.IndexOf(_otpTextBoxes, currentTextBox);
            if (currentIndex >= 0 && currentIndex < _otpTextBoxes.Length - 1)
            {
                _otpTextBoxes[currentIndex + 1].Focus();
            }
        }

        // Restricts input to only digits in the OTP text boxes.
        private void OtpTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        // Handles the OTP verification process when the verify button is clicked.
        private void BtnVerify_Click(object sender, EventArgs e)
        {
            string enteredOtp = string.Concat(_otpTextBoxes.Select(txt => txt.Text));

            if (ValidateOtp(enteredOtp))
            {
                if (VerifyOtp(enteredOtp))
                {
                    ShowCustomMessageBox("Verification successful!", "Success", true);
                    new Form_NewPassword(_userEmail).Show();
                    Hide();
                }
            }
        }

        // Validates the OTP input for correct length.
        private bool ValidateOtp(string enteredOtp)
        {
            if (enteredOtp.Length != 6)
            {
                ShowCustomMessageBox("Please enter a complete 6-digit OTP.", "Error", true);
                return false;
            }
            return true;
        }

        // Verifies the entered OTP against the database record.
        private bool VerifyOtp(string enteredOtp)
        {
            const string query = "SELECT VerificationCode, OtpExpireTime FROM users WHERE Email = @Email";

            using (var connection = new MySqlConnection(_connStr))
            {
                try
                {
                    connection.Open();
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Email", _userEmail);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int storedOtp = Convert.ToInt32(reader["VerificationCode"]);
                                DateTime otpExpireTime = Convert.ToDateTime(reader["OtpExpireTime"]);

                                if (storedOtp == int.Parse(enteredOtp) && otpExpireTime >= DateTime.Now)
                                {
                                    return true;
                                }

                                ShowCustomMessageBox("Incorrect or expired verification code.", "Error", true);
                                return false;
                            }
                            else
                            {
                                ShowCustomMessageBox("User not found.", "Error", true);
                                return false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowCustomMessageBox($"An error occurred while verifying the OTP: {ex.Message}", "Error", true);
                    return false;
                }
            }
        }

        // Displays a custom message box.
        private void ShowCustomMessageBox(string message, string title, bool hideOkButton)
        {
            var customMessageBox = new CustomMessageBox(title, message);
            if (hideOkButton)
            {
                customMessageBox.HideOkButton();
            }
            customMessageBox.ShowDialog();
        }
    }
}
