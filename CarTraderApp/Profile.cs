using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CarTraderApp
{
    public partial class Profile : Form
    {
        private readonly string _username;
        private readonly string _email;
        private readonly byte[] _imageBytes;

        public Profile(string currentUsername, string currentUserEmail, byte[] currentUserImage)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            _username = currentUsername;
            _email = currentUserEmail;
            _imageBytes = currentUserImage;

            DisplayUserDetails();
        }

        // Displays the user's details such as username, email, and profile image.
        private void DisplayUserDetails()
        {
            lblUsername.Text = _username;
            lblEmail.Text = _email;
            DisplayUserImage(_imageBytes);
        }

        // Displays the user's image or a default image if none is provided.
        private void DisplayUserImage(byte[] imageBytes)
        {
            if (imageBytes != null && imageBytes.Length > 0)
            {
                try
                {
                    using (var ms = new MemoryStream(imageBytes))
                    {
                        picUserImage.Image = Image.FromStream(ms);
                        picUserImage.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
                catch (Exception ex)
                {
                    ShowCustomMessageBox($"Error displaying image: {ex.Message}", "Error", true);
                    SetDefaultUserImage();
                }
            }
            else
            {
                SetDefaultUserImage();
            }
        }

        // Sets a default user image when no image is provided or if an error occurs.
        private void SetDefaultUserImage()
        {
            picUserImage.Image = Properties.Resources.icons8_user_64;
            picUserImage.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        // Handles the click event to close the profile form.
        private void btnCancle_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Displays a custom message box with an optional OK button hiding.
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
