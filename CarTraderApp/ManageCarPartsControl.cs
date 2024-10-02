using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Engines;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CarTraderApp
{
    public partial class ManageCarPartsControl : UserControl
    {
        private readonly string _connStr = "Server=localhost;Database=car_trader;Uid=root;Pwd=Admin;";
        private readonly string[] _placeholders = { "Part Name", "Description", "Manufacturer", "Price", "Buying Price", "Quantity", "Enter your part name here..." };

        private readonly float _placeholderFontSize = 12f;
        private readonly float _inputFontSize = 12f;

        public ManageCarPartsControl()
        {
            InitializeComponent();
            InitializePlaceholders();
            AttachEventHandlers();
            LoadPartsData();
            lblBehaviour.Text = "Add Car Part";
        }

        // Initializes placeholder text for input fields.
        private void InitializePlaceholders()
        {
            SetPlaceholder(txtPartName, _placeholders[0]);
            SetPlaceholder(txtDescription, _placeholders[1]);
            SetPlaceholder(txtManufacturer, _placeholders[2]);
            SetPlaceholder(txtPrice, _placeholders[3]);
            SetPlaceholder(txtBuyingPrice, _placeholders[4]);
            SetPlaceholder(txtQuentity, _placeholders[5]);  // New line added
            SetPlaceholder(txtSearch, _placeholders[6]);
        }

        // Attaches event handlers to UI elements.
        private void AttachEventHandlers()
        {
            AttachPlaceholderEvents(txtPartName);
            AttachPlaceholderEvents(txtDescription);
            AttachPlaceholderEvents(txtManufacturer);
            AttachPlaceholderEvents(txtPrice);
            AttachPlaceholderEvents(txtBuyingPrice);
            AttachPlaceholderEvents(txtSearch);
            AttachPlaceholderEvents(txtQuentity);

            dataGridViewCarparts.CellClick += dataGridViewCarParts_CellClick;
            txtSearch.TextChanged += txtSearch_TextChanged;
            btnUpload.Click += btnUpload_Click;
            btnCancle.Click += btnCancle_Click;
        }

        // Attaches Enter and Leave events to manage placeholder text.
        private void AttachPlaceholderEvents(RichTextBox textBox)
        {
            textBox.Enter += richTextBox_Enter;
            textBox.Leave += richTextBox_Leave;
        }

        // Sets placeholder text in a RichTextBox.
        private void SetPlaceholder(RichTextBox richTextBox, string placeholderText)
        {
            richTextBox.Text = placeholderText;
            richTextBox.ForeColor = Color.Gray;
            richTextBox.Font = new Font(richTextBox.Font.FontFamily, _placeholderFontSize, FontStyle.Bold);
        }

        // Removes placeholder text when the RichTextBox is focused.
        private void RemovePlaceholder(RichTextBox richTextBox, string placeholderText)
        {
            if (richTextBox.Text == placeholderText)
            {
                richTextBox.Text = "";
                richTextBox.ForeColor = Color.Black;
                richTextBox.Font = new Font(richTextBox.Font.FontFamily, _inputFontSize, FontStyle.Regular);
            }
        }

        // Handles the Enter event for RichTextBox to clear placeholder text.
        private void richTextBox_Enter(object sender, EventArgs e)
        {
            RichTextBox richTextBox = sender as RichTextBox;
            if (richTextBox != null)
            {
                int index = Array.IndexOf(_placeholders, richTextBox.Text);
                if (index >= 0) RemovePlaceholder(richTextBox, _placeholders[index]);
            }
        }

        // Handles the Leave event for RichTextBox to set placeholder text.
        private void richTextBox_Leave(object sender, EventArgs e)
        {
            RichTextBox richTextBox = sender as RichTextBox;
            if (richTextBox != null && string.IsNullOrWhiteSpace(richTextBox.Text))
            {
                int index = Array.IndexOf(_placeholders, richTextBox.Tag.ToString());
                if (index >= 0) SetPlaceholder(richTextBox, _placeholders[index]);
            }
        }

        // Loads parts data from the database and populates the DataGridView
        private void LoadPartsData()
        {
            using (MySqlConnection connection = new MySqlConnection(_connStr))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Id, PartName, Description, Manufacturer, Price, Buying_Price , quantity, Image FROM parts";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable partsTable = new DataTable();
                        adapter.Fill(partsTable);

                        dataGridViewCarparts.DataSource = partsTable;

                        AddActionColumns();
                        ConfigureDataGridView();
                    }
                }
                catch (Exception ex)
                {
                    ShowCustomMessageBox($"Error loading parts: {ex.Message}", "Error", true);
                }
            }
        }

        // Adds Edit and Remove action columns to the DataGridView.
        private void AddActionColumns()
        {
            AddImageColumn("Edit", Properties.Resources.edit);
            AddImageColumn("Remove", Properties.Resources.delete);
        }

        // Adds an image column to the DataGridView.
        private void AddImageColumn(string columnName, Image image)
        {
            if (!dataGridViewCarparts.Columns.Contains(columnName))
            {
                DataGridViewImageColumn imageColumn = new DataGridViewImageColumn
                {
                    Name = columnName,
                    HeaderText = columnName,
                    Image = image,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                };
                dataGridViewCarparts.Columns.Add(imageColumn);
            }
        }

        // Configures the DataGridView appearance and properties.
        private void ConfigureDataGridView()
        {
            dataGridViewCarparts.ReadOnly = true;
            dataGridViewCarparts.RowHeadersVisible = false;
            dataGridViewCarparts.AutoGenerateColumns = true;
            dataGridViewCarparts.RowTemplate.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dataGridViewCarparts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCarparts.ColumnHeadersHeight = 50;

            var columns = new[]
            {
                new { Name = "Id", Header = "Part ID", Width = 150 },
                new { Name = "PartName", Header = "Part Name", Width = 250 },
                new { Name = "Description", Header = "Description", Width = 300 },
                new { Name = "quantity", Header = "Qty", Width = 65 },
                new { Name = "Manufacturer", Header = "Manufacturer", Width = 190 },
                new { Name = "Buying_Price", Header = "Buying Price", Width = 150 },
                new { Name = "Price", Header = "selling Price", Width = 150 },
                new { Name = "Image", Header = "Image", Width = 170 }
            };

            foreach (var col in columns)
            {
                dataGridViewCarparts.Columns[col.Name].HeaderText = col.Header;
                dataGridViewCarparts.Columns[col.Name].Width = col.Width;
                dataGridViewCarparts.Columns[col.Name].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridViewCarparts.Columns[col.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewCarparts.Columns[col.Name].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            DataGridViewImageColumn imageColumn = (DataGridViewImageColumn)dataGridViewCarparts.Columns["Image"];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;

            dataGridViewCarparts.Columns["Id"].Visible = false; // Hide ID column
            dataGridViewCarparts.RowTemplate.Height = 75;
        }

        // Handles cell clicks in the DataGridView to perform actions like Edit and Remove.
        private void dataGridViewCarParts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == dataGridViewCarparts.Columns["Edit"].Index)
                {
                    DataGridViewRow row = dataGridViewCarparts.Rows[e.RowIndex];
                    PopulateFormFieldsFromRow(row);
                }
                else if (e.ColumnIndex == dataGridViewCarparts.Columns["Remove"].Index)
                {
                    int id = Convert.ToInt32(dataGridViewCarparts.Rows[e.RowIndex].Cells["Id"].Value);
                    DeleteRowFromDatabase(id);
                    LoadPartsData(); // Refresh data after deletion
                }
            }
        }

        // Populates form fields from the selected DataGridView row.
        private void PopulateFormFieldsFromRow(DataGridViewRow row)
        {
            txtPartName.Text = row.Cells["PartName"].Value.ToString();
            txtDescription.Text = row.Cells["Description"].Value.ToString();
            txtManufacturer.Text = row.Cells["Manufacturer"].Value.ToString();
            txtPrice.Text = row.Cells["Price"].Value.ToString();
            txtQuentity.Text = row.Cells["quantity"].Value.ToString();
            txtBuyingPrice.Text = row.Cells["Buying_Price"].Value.ToString();

            if (row.Cells["Image"].Value != DBNull.Value)
            {
                byte[] imgBytes = (byte[])row.Cells["Image"].Value;
                using (MemoryStream ms = new MemoryStream(imgBytes))
                {
                    picPartImage.Image = Image.FromStream(ms);
                }
            }
            else
            {
                picPartImage.Image = null;
            }

            btnSave.Text = "Update";
            btnSave.BackColor = Color.Orange;
            lblBehaviour.Text = "Update Car Part";
        }

        // Converts an Image to a byte array.
        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Bitmap bmp = new Bitmap(image))
                {
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                return ms.ToArray();
            }
        }

        // Handles the Upload button click event to select and display an image.
        private void btnUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    picPartImage.Image = Image.FromFile(openFileDialog.FileName);
                    picPartImage.SizeMode = PictureBoxSizeMode.StretchImage;
                    picPartImage.BringToFront();
                }
            }
        }

        // Deletes a car part from the database.
        private void DeleteRowFromDatabase(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connStr))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM parts WHERE Id = @Id";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }
                    ShowCustomMessageBox("Car part deleted successfully!", "Success", true);
                }
                catch (Exception ex)
                {
                    ShowCustomMessageBox($"Error deleting car part: {ex.Message}", "Error", true);
                }
            }
        }

        // Clears all input fields and resets placeholders.
        private void ClearInputFields()
        {
            txtPartName.Clear();
            txtDescription.Clear();
            txtManufacturer.Clear();
            txtPrice.Clear();
            txtBuyingPrice.Clear();
            txtSearch.Clear();
            txtQuentity.Clear();

            InitializePlaceholders(); // Reset placeholders
        }

        // Shows a custom message box with optional hiding of the OK button.
        private void ShowCustomMessageBox(string message, string title, bool hideOkButton)
        {
            CustomMessageBox customMessageBox = new CustomMessageBox(title, message);
            if (hideOkButton)
            {
                customMessageBox.HideOkButton();
            }
            customMessageBox.ShowDialog();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            lblBehaviour.Text = "Add Car Part";
            ClearInputFields();
            LoadPartsData();
            btnSave.Text = "Save";
            btnSave.BackColor = Color.ForestGreen;
            picPartImage.Image = null; // Clear the image after canceling
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchValue = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchValue))
            {
                LoadPartsData();
            }
            else
            {
                var partsTable = (DataTable)dataGridViewCarparts.DataSource;
                var dv = partsTable.DefaultView;
                dv.RowFilter = $"PartName LIKE '%{searchValue}%'";
                dataGridViewCarparts.DataSource = dv.ToTable();
            }
        }

        private bool isSaving = false;

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (isSaving) return; // Prevent re-entry
            isSaving = true;

            try
            {
                string partName = txtPartName.Text.Trim();
                string description = txtDescription.Text.Trim();
                string manufacturer = txtManufacturer.Text.Trim();
                string priceText = txtPrice.Text.Trim();
                string buying_price = txtBuyingPrice.Text.Trim();
                string quantityText = txtQuentity.Text.Trim();  // New line added

                // Validate that quantity is a valid number
                if (string.IsNullOrWhiteSpace(quantityText) || !int.TryParse(quantityText, out int quantity))
                {
                    ShowCustomMessageBox("Please enter a valid quantity.", "Input Error", true);
                    return;
                }

                // Validate that the price and buying price are numeric values
                string pricePattern = @"^\d+(\.\d{1,2})?$"; // Matches a number with up to 2 decimal places
                if (string.IsNullOrWhiteSpace(priceText) || !Regex.IsMatch(priceText, pricePattern) || !decimal.TryParse(priceText, out decimal price))
                {
                    ShowCustomMessageBox("Please enter a valid price (e.g., 15000.00).", "Input Error", true);
                    return;
                }

                if (string.IsNullOrWhiteSpace(buying_price) || !Regex.IsMatch(buying_price, pricePattern) || !decimal.TryParse(buying_price, out decimal buyingPrice))
                {
                    ShowCustomMessageBox("Please enter a valid buying price (e.g., 12000.00).", "Input Error", true);
                    return;
                }

                // Validate that required text fields are not empty
                if (string.IsNullOrWhiteSpace(partName) ||
                    string.IsNullOrWhiteSpace(description) ||
                    string.IsNullOrWhiteSpace(manufacturer))
                {
                    ShowCustomMessageBox("Please fill in all fields with valid data.", "Input Error", true);
                    return;
                }

                byte[] imageBytes = picPartImage.Image != null ? ImageToByteArray(picPartImage.Image) : null;

                using (var connection = new MySqlConnection(_connStr))
                {
                    try
                    {
                        connection.Open();
                        string query = btnSave.Text == "Update"
                     ? "UPDATE parts SET PartName = @PartName, Description = @Description, Manufacturer = @Manufacturer, Price = @Price, Buying_Price = @BuyingPrice, quantity = @Quantity, Image = @Image WHERE Id = @Id"  
                     : "INSERT INTO parts (PartName, Description, Manufacturer, Price, Buying_price, quantity, Image) VALUES (@PartName, @Description, @Manufacturer, @Price, @BuyingPrice, @Quantity, @Image)";

                        using (var cmd = new MySqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@PartName", partName);
                            cmd.Parameters.AddWithValue("@Description", description);
                            cmd.Parameters.AddWithValue("@Manufacturer", manufacturer);
                            cmd.Parameters.AddWithValue("@Price", price);
                            cmd.Parameters.AddWithValue("@BuyingPrice", buying_price);
                            cmd.Parameters.AddWithValue("@Quantity", quantity);
                            cmd.Parameters.AddWithValue("@Image", imageBytes);

                            if (btnSave.Text == "Update")
                            {
                                int id = Convert.ToInt32(dataGridViewCarparts.CurrentRow.Cells["Id"].Value);
                                cmd.Parameters.AddWithValue("@Id", id);
                            }

                            cmd.ExecuteNonQuery();
                            ShowCustomMessageBox($"Car part {(btnSave.Text == "Update" ? "updated" : "saved")} successfully!", "Success", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowCustomMessageBox($"An error occurred while saving/updating the car part: {ex.Message}", "Error", true);
                    }
                }
                // Clear image after saving
                picPartImage.Image = null;

                // Reload data and reset the form
                ClearInputFields();
                LoadPartsData();
                lblBehaviour.Text = "Add Car Part";
                btnSave.Text = "Save";
                btnSave.BackColor = Color.ForestGreen;
            }
            finally
            {
                isSaving = false; // Reset the flag
            }
        }

    }
}
