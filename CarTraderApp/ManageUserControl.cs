using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CarTraderApp
{
    public partial class ManageUserControl : UserControl
    {
        private readonly string _connStr = "Server=localhost;Database=car_trader;Uid=root;Pwd=Admin;";
        private readonly string[] _placeholders = { "Username", "Email", "Contact Number", "Password", "Search by username..." };
        private readonly float _placeholderFontSize = 12f;
        private readonly float _inputFontSize = 12f;
        private readonly int _userId;
        private bool _isSaving = false;

        public ManageUserControl(int userId)
        {
            InitializeComponent();
            _userId = userId;
            InitializePlaceholders();
            AttachEventHandlers();
            LoadUsersData();
            lblUserBehavior.Text = "Add User";
            InitializeComboBox();
        }

        // Initializes the ComboBox with user types and settings.
        private void InitializeComboBox()
        {
            cmbType.Width = 301;
            cmbType.Font = new Font(cmbType.Font.FontFamily, 10);
            cmbType.DropDownHeight = 100;
            cmbType.Items.Clear();
            cmbType.Items.AddRange(new[] { "Admin", "Customer", "User" });
            cmbType.SelectedIndex = 0;
        }

        // Initializes the placeholder text for input fields.
        private void InitializePlaceholders()
        {
            SetPlaceholder(txtUsername, _placeholders[0]);
            SetPlaceholder(txtEmail, _placeholders[1]);
            SetPlaceholder(txtContactNumber, _placeholders[2]);
            SetPlaceholder(txtPassword, _placeholders[3]);
            SetPlaceholder(txtSearch, _placeholders[4]);
        }

        // Attaches event handlers to controls for user interaction.
        private void AttachEventHandlers()
        {
            txtUsername.Enter += RichTextBox_Enter;
            txtUsername.Leave += RichTextBox_Leave;
            txtEmail.Enter += RichTextBox_Enter;
            txtEmail.Leave += RichTextBox_Leave;
            txtContactNumber.Enter += RichTextBox_Enter;
            txtContactNumber.Leave += RichTextBox_Leave;
            txtPassword.Enter += RichTextBox_Enter;
            txtPassword.Leave += RichTextBox_Leave;
            txtSearch.Enter += RichTextBox_Enter;
            txtSearch.Leave += RichTextBox_Leave;

            dataGridViewUsers.CellClick += DataGridViewUsers_CellClick;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            btnUpload.Click += BtnUpload_Click;
            btnCancle.Click += BtnCancel_Click;
            btnSave.Click += BtnSave_Click;
        }

        // Loads user data into the DataGridView.
        private void LoadUsersData()
        {
            using (var connection = new MySqlConnection(_connStr))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Id, Username, Email, contact_number AS ContactNumber, Type, Image FROM users WHERE is_deleted = FALSE";
                    using (var cmd = new MySqlCommand(query, connection))
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable usersTable = new DataTable();
                        adapter.Fill(usersTable);
                        dataGridViewUsers.DataSource = usersTable;

                        AddEditRemoveButtons();
                        ConfigureDataGridView();
                    }
                }
                catch (Exception ex)
                {
                    ShowCustomMessageBox("An error occurred while retrieving data: " + ex.Message, "Error", true);
                }
            }
        }

        // Configures the DataGridView layout and appearance.
        private void ConfigureDataGridView()
        {
            dataGridViewUsers.ReadOnly = true;
            dataGridViewUsers.RowHeadersVisible = false;
            dataGridViewUsers.AutoGenerateColumns = true;
            dataGridViewUsers.RowTemplate.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dataGridViewUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewUsers.ColumnHeadersHeight = 50;
            dataGridViewUsers.RowTemplate.Height = 75;

            var columnSettings = new[]
            {
                new { Name = "Id", Header = "User ID", Width = 200, Visible = false },
                new { Name = "Username", Header = "Username", Width = 200, Visible = true },
                new { Name = "Email", Header = "Email", Width = 300, Visible = true },
                new { Name = "ContactNumber", Header = "Contact Number", Width = 200, Visible = true },
                new { Name = "Type", Header = "User Type", Width = 200, Visible = true },
                new { Name = "Image", Header = "Image", Width = 220, Visible = true }
            };

            foreach (var col in columnSettings)
            {
                var column = dataGridViewUsers.Columns[col.Name];
                if (column != null)
                {
                    column.HeaderText = col.Header;
                    column.Width = col.Width;
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    column.Visible = col.Visible;
                }
            }

            if (dataGridViewUsers.Columns.Contains("Image"))
            {
                DataGridViewImageColumn imageColumn = (DataGridViewImageColumn)dataGridViewUsers.Columns["Image"];
                imageColumn.ImageLayout = DataGridViewImageCellLayout.Normal;
            }

            ConfigureEditRemoveButtons();
        }

        // Adds Edit and Remove buttons to the DataGridView.
        private void AddEditRemoveButtons()
        {
            if (!dataGridViewUsers.Columns.Contains("Edit"))
            {
                DataGridViewImageColumn editColumn = new DataGridViewImageColumn
                {
                    Name = "Edit",
                    HeaderText = "Edit",
                    Image = Properties.Resources.edit,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                };
                dataGridViewUsers.Columns.Add(editColumn);
            }

            if (!dataGridViewUsers.Columns.Contains("Remove"))
            {
                DataGridViewImageColumn removeColumn = new DataGridViewImageColumn
                {
                    Name = "Remove",
                    HeaderText = "Remove",
                    Image = Properties.Resources.delete,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                };
                dataGridViewUsers.Columns.Add(removeColumn);
            }

            // Check and remove Password column if it exists
            if (dataGridViewUsers.Columns.Contains("Password"))
            {
                dataGridViewUsers.Columns.Remove("Password");
            }
        }

        // Configures the Edit and Remove buttons in the DataGridView.
        private void ConfigureEditRemoveButtons()
        {
            dataGridViewUsers.CellFormatting += (sender, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == dataGridViewUsers.Columns["Edit"].Index ||
                        e.ColumnIndex == dataGridViewUsers.Columns["Remove"].Index)
                    {
                        dataGridViewUsers.Rows[e.RowIndex].Height = 70;
                        dataGridViewUsers.Columns[e.ColumnIndex].Width = 100;
                        dataGridViewUsers.Cursor = Cursors.Hand;
                    }
                }
            };
        }

        // Sets placeholder text for a RichTextBox.
        private void SetPlaceholder(RichTextBox richTextBox, string placeholderText)
        {
            richTextBox.Text = placeholderText;
            richTextBox.ForeColor = Color.Gray;
            richTextBox.Font = new Font(richTextBox.Font.FontFamily, _placeholderFontSize, FontStyle.Bold);
        }

        // Removes placeholder text from a RichTextBox.
        private void RemovePlaceholder(RichTextBox richTextBox, string placeholderText)
        {
            if (richTextBox.Text == placeholderText)
            {
                richTextBox.Text = "";
                richTextBox.ForeColor = Color.Black;
                richTextBox.Font = new Font(richTextBox.Font.FontFamily, _inputFontSize, FontStyle.Regular);
            }
        }

        // Event handler for entering a RichTextBox to remove placeholder text.
        private void RichTextBox_Enter(object sender, EventArgs e)
        {
            if (sender is RichTextBox richTextBox)
            {
                int index = Array.IndexOf(_placeholders, richTextBox.Text);
                if (index >= 0) RemovePlaceholder(richTextBox, _placeholders[index]);
            }
        }

        // Event handler for leaving a RichTextBox to set placeholder text.
        private void RichTextBox_Leave(object sender, EventArgs e)
        {
            if (sender is RichTextBox richTextBox && string.IsNullOrWhiteSpace(richTextBox.Text))
            {
                int index = Array.IndexOf(_placeholders, richTextBox.Tag?.ToString());
                if (index >= 0) SetPlaceholder(richTextBox, _placeholders[index]);
            }
        }

        // Handles the click event to upload a user image.
        private void BtnUpload_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog { Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    picUserImage.Image = Image.FromFile(openFileDialog.FileName);
                    picUserImage.SizeMode = PictureBoxSizeMode.StretchImage;
                    picUserImage.BringToFront();
                }
            }
        }

        // Converts an Image to a byte array.
        private byte[] ImageToByteArray(Image image)
        {
            using (var ms = new MemoryStream())
            {
                using (var bmp = new Bitmap(image))
                {
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                return ms.ToArray();
            }
        }

        // Handles cell click events in the DataGridView for editing or removing users.
        private void DataGridViewUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == dataGridViewUsers.Columns["Edit"].Index)
                {
                    PopulateFormFieldsFromRow(dataGridViewUsers.Rows[e.RowIndex]);
                }
                else if (e.ColumnIndex == dataGridViewUsers.Columns["Remove"].Index)
                {
                    int id = Convert.ToInt32(dataGridViewUsers.Rows[e.RowIndex].Cells["Id"].Value);
                    DeleteRowFromDatabase(id);
                    LoadUsersData();
                }
            }
        }

        // Marks a user as deleted in the database.
        private void DeleteRowFromDatabase(int id)
        {
            using (var connection = new MySqlConnection(_connStr))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE users SET is_deleted = TRUE WHERE Id = @Id";
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }
                    ShowCustomMessageBox("User deleted successfully!", "Success", true);
                }
                catch (Exception ex)
                {
                    ShowCustomMessageBox("An error occurred while marking the user as deleted: " + ex.Message, "Error", true);
                }
            }
        }

        // Populates form fields with data from a selected DataGridView row.
        private void PopulateFormFieldsFromRow(DataGridViewRow row)
        {
            txtUsername.Text = row.Cells["Username"].Value.ToString();
            txtEmail.Text = row.Cells["Email"].Value.ToString();
            txtContactNumber.Text = row.Cells["ContactNumber"].Value.ToString();
            txtPassword.Text = string.Empty;
            txtPassword.Enabled = false;
            txtPassword.ReadOnly = true;
            cmbType.SelectedItem = row.Cells["Type"].Value.ToString();

            if (row.Cells["Image"].Value != DBNull.Value)
            {
                byte[] imgBytes = (byte[])row.Cells["Image"].Value;
                using (MemoryStream ms = new MemoryStream(imgBytes))
                {
                    picUserImage.Image = Image.FromStream(ms);
                }
            }
            else
            {
                picUserImage.Image = null;
            }

            btnSave.Text = "Update";
            btnSave.BackColor = Color.Orange;
            lblUserBehavior.Text = "Update User";
        }

        // Handles the save or update operation for user data.
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (_isSaving) return;
            _isSaving = true;
            try
            {
                if (!ValidateUserInput(out string username, out string email, out string contactNumber, out string userType, out string password, out byte[] imageBytes, out bool isUpdatingPassword, out string hashedPassword))
                {
                    return;
                }

                using (var connection = new MySqlConnection(_connStr))
                {
                    try
                    {
                        connection.Open();
                        string query = BuildUserSaveOrUpdateQuery(isUpdatingPassword);
                        using (var cmd = new MySqlCommand(query, connection))
                        {
                            SetUserSaveOrUpdateParameters(cmd, username, email, contactNumber, userType, imageBytes, isUpdatingPassword, hashedPassword);
                            cmd.ExecuteNonQuery();
                            ShowCustomMessageBox($"User {(btnSave.Text == "Update" ? "updated" : "saved")} successfully!", "Success", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowCustomMessageBox($"An error occurred while saving/updating the user: {ex.Message}", "Error", true);
                    }
                }

                ClearInputFields();
                LoadUsersData();
                ResetForm();
            }
            finally
            {
                _isSaving = false;
            }
        }

        // Builds the SQL query for saving or updating a user.
        private string BuildUserSaveOrUpdateQuery(bool isUpdatingPassword)
        {
            if (btnSave.Text == "Update")
            {
                string query = "UPDATE Users SET Username = @Username, Email = @Email, contact_number = @ContactNumber, type = @UserType, Image = @Image, is_deleted = 0";
                if (isUpdatingPassword) query += ", Password = @Password";
                query += " WHERE id = @Id";
                return query;
            }
            else
            {
                return "INSERT INTO Users (Username, Email, contact_number, type, Password, Image, is_deleted) VALUES (@Username, @Email, @ContactNumber, @UserType, @Password, @Image, 0)";
            }
        }

        // Sets the parameters for the user save or update operation.
        private void SetUserSaveOrUpdateParameters(MySqlCommand cmd, string username, string email, string contactNumber, string userType, byte[] imageBytes, bool isUpdatingPassword, string hashedPassword)
        {
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@ContactNumber", contactNumber);
            cmd.Parameters.AddWithValue("@UserType", userType);
            cmd.Parameters.AddWithValue("@Image", imageBytes);

            if (btnSave.Text == "Update")
            {
                int id = Convert.ToInt32(dataGridViewUsers.CurrentRow.Cells["Id"].Value);
                cmd.Parameters.AddWithValue("@Id", id);
                if (isUpdatingPassword)
                {
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);
                }
            }
            else
            {
                cmd.Parameters.AddWithValue("@Password", hashedPassword);
            }
        }

        // Validates the user input before saving or updating.
        private bool ValidateUserInput(out string username, out string email, out string contactNumber, out string userType, out string password, out byte[] imageBytes, out bool isUpdatingPassword, out string hashedPassword)
        {
            username = txtUsername.Text.Trim();
            email = txtEmail.Text.Trim();
            contactNumber = txtContactNumber.Text.Trim();
            userType = cmbType.SelectedItem?.ToString();
            password = txtPassword.Text.Trim();
            imageBytes = picUserImage.Image != null ? ImageToByteArray(picUserImage.Image) : null;
            hashedPassword = string.Empty;
            isUpdatingPassword = !string.IsNullOrWhiteSpace(password);

            // Validate required fields
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(contactNumber))
            {
                ShowCustomMessageBox("Please fill in all fields with valid data.", "Input Error", true);
                return false;
            }

            if (string.IsNullOrWhiteSpace(userType))
            {
                ShowCustomMessageBox("Please select a user type.", "Input Error", true);
                return false;
            }

            // Validate email using regex
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                ShowCustomMessageBox("Please enter a valid email address.", "Input Error", true);
                return false;
            }

            // Validate contact number (10 digits)
            string contactNumberPattern = @"^\d{10}$";
            if (!Regex.IsMatch(contactNumber, contactNumberPattern))
            {
                ShowCustomMessageBox("Please enter a valid 10-digit contact number.", "Input Error", true);
                return false;
            }

            // Validate password if updating
            if (isUpdatingPassword)
            {
                // Password regex pattern: at least one uppercase letter, one lowercase letter, one digit, one special character, and 8-20 characters long
                string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$";
                if (!Regex.IsMatch(password, passwordPattern))
                {
                    ShowCustomMessageBox("Password must be 8-20 characters long, contain at least one uppercase letter, one lowercase letter, one digit, and one special character.", "Input Error", true);
                    return false;
                }

                hashedPassword = PasswordHelper.HashPassword(password);
            }

            return true;
        }


        // Resets the form to its default state after a save or cancel operation.
        private void ResetForm()
        {
            lblUserBehavior.Text = "Add User";
            btnSave.Text = "Save";
            btnSave.BackColor = Color.ForestGreen;
            picUserImage.Image = null;
        }

        // Handles the cancel operation, resetting the form and reloading user data.
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            ResetForm();
            ClearInputFields();
            LoadUsersData();
        }

        // Handles the search operation by filtering user data in the DataGridView.
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchValue = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchValue))
            {
                LoadUsersData();
            }
            else
            {
                var usersTable = (DataTable)dataGridViewUsers.DataSource;
                var dv = usersTable.DefaultView;
                dv.RowFilter = $"Username LIKE '%{searchValue}%'";
                dataGridViewUsers.DataSource = dv.ToTable();
            }
        }

        // Clears all input fields and resets placeholders.
        private void ClearInputFields()
        {
            txtUsername.Clear();
            txtEmail.Clear();
            txtContactNumber.Clear();
            txtPassword.Clear();
            txtSearch.Clear();

            SetPlaceholder(txtUsername, _placeholders[0]);
            SetPlaceholder(txtEmail, _placeholders[1]);
            SetPlaceholder(txtContactNumber, _placeholders[2]);
            SetPlaceholder(txtPassword, _placeholders[3]);
            SetPlaceholder(txtSearch, _placeholders[4]);
        }

        // Displays a custom message box with an optional OK button.
        private void ShowCustomMessageBox(string message, string title, bool hideOkButton)
        {
            CustomMessageBox customMessageBox = new CustomMessageBox(title, message);
            if (hideOkButton)
            {
                customMessageBox.HideOkButton();
            }
            customMessageBox.ShowDialog();
        }
    }
}
