using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarTraderApp
{
    public partial class Form_ForgotPassword : Form
    {
        private Label _lblTitle;
        private Label _lblInstruction;
        private Label _lblEmail;
        private TextBox _txtEmail;
        private Button _btnSendOTP;
        private readonly string _connectionString = "Server=localhost;Database=car_trader;Uid=root;Pwd=Admin;";

        public Form_ForgotPassword()
        {
            InitializeComponent();
            InitializeForgotPasswordForm();
        }

        // Initializes the form controls for the forgot password functionality.
        private void InitializeForgotPasswordForm()
        {
            // Set Form properties
            Text = "Reset Your Password";
            Size = new Size(600, 400);
            StartPosition = FormStartPosition.CenterScreen;

            // Title Label
            _lblTitle = CreateLabel("Reset Your Password", new Font("Arial", 16, FontStyle.Bold), new Point(180, 50));
            Controls.Add(_lblTitle);

            // Instruction Label
            _lblInstruction = CreateLabel(
                "Please enter your registered email. We'll send a secure OTP allowing you to securely reset your password.",
                new Font("Arial", 10), new Point(50, 100), new Size(500, 50), ContentAlignment.TopCenter);
            Controls.Add(_lblInstruction);

            // Email Label
            _lblEmail = CreateLabel("Enter Your Email", new Font("Arial", 10), new Point(230, 170));
            Controls.Add(_lblEmail);

            // Email TextBox
            _txtEmail = CreateTextBox(new Point(170, 200), 250);
            Controls.Add(_txtEmail);

            // Send OTP Button
            _btnSendOTP = CreateButton("Send OTP", new Point(250, 250), Color.DarkSlateGray, Color.White);
            _btnSendOTP.Click += BtnSendOtp_Click;
            Controls.Add(_btnSendOTP);
        }

        // Creates a label with specified properties.
        private Label CreateLabel(string text, Font font, Point location, Size? size = null, ContentAlignment textAlign = ContentAlignment.TopLeft)
        {
            return new Label
            {
                Text = text,
                Font = font,
                Location = location,
                AutoSize = size == null,
                Size = size ?? new Size(0, 0),
                TextAlign = textAlign
            };
        }

        // Creates a TextBox with specified properties.
        private TextBox CreateTextBox(Point location, int width)
        {
            return new TextBox
            {
                Width = width,
                Location = location
            };
        }

        // Creates a button with specified properties.
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

        // Handles the Send OTP button click event to validate email and send an OTP.
        private async void BtnSendOtp_Click(object sender, EventArgs e)
        {
            string email = _txtEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(email))
            {
                ShowCustomMessageBox("Please enter your email address.", "Error", true);
                return;
            }

            try
            {
                await SendOtpAsync(email);
            }
            catch (Exception ex)
            {
                ShowCustomMessageBox($"An error occurred while processing your request: {ex.Message}", "Error", true);
            }
        }

        // Sends an OTP to the user's email if the email is found in the database.
        private async Task SendOtpAsync(string email)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                const string query = "SELECT * FROM users WHERE Email = @Email";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    using (var reader = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            int otp = GenerateOtp();
                            reader.Close();

                            await UpdateUserOtpAsync(connection, email, otp);
                            await SendOtpEmailAsync(email, otp);

                            ShowCustomMessageBox("An OTP has been sent to your email address.", "Success", true);
                            new Form_OTP(email).Show();
                            Hide();
                        }
                        else
                        {
                            ShowCustomMessageBox("The entered email address does not exist in our records.", "Error", true);
                        }
                    }
                }
            }
        }

        // Updates the user's OTP and expiry time in the database.
        private async Task UpdateUserOtpAsync(MySqlConnection connection, string email, int otp)
        {
            const string updateQuery = "UPDATE users SET VerificationCode = @Otp, OtpExpireTime = @ExpireTime WHERE Email = @Email";
            using (var updateCmd = new MySqlCommand(updateQuery, connection))
            {
                updateCmd.Parameters.AddWithValue("@Otp", otp);
                updateCmd.Parameters.AddWithValue("@ExpireTime", DateTime.Now.AddMinutes(5));
                updateCmd.Parameters.AddWithValue("@Email", email);
                await updateCmd.ExecuteNonQueryAsync();
            }
        }

        // Sends an OTP email asynchronously to the specified email address.
        private async Task SendOtpEmailAsync(string email, int otp)
        {
            try
            {
                using (var mail = new MailMessage("nurajshaminda93@gmail.com", email))
                {
                    mail.Subject = "Your OTP Code";
                    mail.Body = $"Your OTP code is {otp}. It will expire in 5 minutes.";
                    mail.IsBodyHtml = false;

                    using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new System.Net.NetworkCredential("nurajshaminda93@gmail.com", "ifzw ctvg ntft ihlm"); // Use the App Password here
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowCustomMessageBox($"Failed to send OTP email: {ex.Message}", "Error", true);
            }
        }

        // Generates a 6-digit OTP.
        private int GenerateOtp()
        {
            return new Random().Next(100000, 999999);
        }

        // Displays a custom message box with optional OK button hiding.
        private void ShowCustomMessageBox(string message, string title, bool hideOkButton)
        {
            var customMessageBox = new CustomMessageBox(title, message);
            if (hideOkButton) customMessageBox.HideOkButton();
            customMessageBox.ShowDialog();
        }
    }
}
