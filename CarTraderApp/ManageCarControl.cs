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

namespace CarTraderApp
{
    public partial class ManageCarControl : UserControl
    {
        private readonly string connectionString = "Server=localhost;Database=car_trader;Uid=root;Pwd=Admin;";
        private readonly string[] placeholders = { "Manufacturer", "Model", "Year", "Price", "Buying Price", "Mileage", "Color", "Search by Manufacturer..." };
        private readonly float placeholderFontSize = 12f;
        private readonly float inputFontSize = 12f;
        private bool isSaving = false;

        public ManageCarControl()
        {
            InitializeComponent();
            InitializePlaceholders();
            AttachEventHandlers();
            LoadCarsData();
            lblCarBehavior.Text = "Add Car";
            InitializeComboBox();
        }

        private void InitializeComboBox()
        {
            cmbTransmission.Width = 150;
            cmbTransmission.Font = new Font(cmbTransmission.Font.FontFamily, 10);
            cmbTransmission.DropDownHeight = 100;

            cmbTransmission.Items.Clear();
            cmbTransmission.Items.Add("Manual");
            cmbTransmission.Items.Add("Automatic");

            cmbTransmission.SelectedIndex = 0;
        }

        private void InitializePlaceholders()
        {
            SetPlaceholder(txtMake, placeholders[0]);
            SetPlaceholder(txtModel, placeholders[1]);
            SetPlaceholder(txtYear, placeholders[2]);
            SetPlaceholder(txtPrice, placeholders[3]);
            SetPlaceholder(txtBuyingPrice, placeholders[4]);
            SetPlaceholder(txtMileage, placeholders[5]);
            SetPlaceholder(txtColor, placeholders[6]);
            SetPlaceholder(txtSearch, placeholders[7]);
        }

        private void AttachEventHandlers()
        {
            txtMake.Enter += richTextBox_Enter;
            txtMake.Leave += richTextBox_Leave;
            txtModel.Enter += richTextBox_Enter;
            txtModel.Leave += richTextBox_Leave;
            txtYear.Enter += richTextBox_Enter;
            txtYear.Leave += richTextBox_Leave;
            txtPrice.Enter += richTextBox_Enter;
            txtPrice.Leave += richTextBox_Leave;
            txtBuyingPrice.Enter += richTextBox_Enter;
            txtBuyingPrice.Leave += richTextBox_Leave;
            txtMileage.Enter += richTextBox_Enter;
            txtMileage.Leave += richTextBox_Leave;
            txtColor.Enter += richTextBox_Enter;
            txtColor.Leave += richTextBox_Leave;
            txtSearch.Enter += richTextBox_Enter;
            txtSearch.Leave += richTextBox_Leave;

            dataGridViewCars.CellClick += dataGridViewCars_CellClick;
            txtSearch.TextChanged += txtSearch_TextChanged;
            btnUpload.Click += btnUpload_Click;
        }

        private void LoadCarsData()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT id, make, model, year, price, buying_price, mileage, color, transmission, image FROM cars ";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable carsTable = new DataTable();
                            adapter.Fill(carsTable);
                            dataGridViewCars.DataSource = carsTable;

                            if (!dataGridViewCars.Columns.Contains("Edit"))
                            {
                                DataGridViewImageColumn editColumn = new DataGridViewImageColumn
                                {
                                    Name = "Edit",
                                    HeaderText = "Edit",
                                    Image = Properties.Resources.edit,
                                    AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                                };
                                dataGridViewCars.Columns.Add(editColumn);
                            }

                            if (!dataGridViewCars.Columns.Contains("Remove"))
                            {
                                DataGridViewImageColumn removeColumn = new DataGridViewImageColumn
                                {
                                    Name = "Remove",
                                    HeaderText = "Remove",
                                    Image = Properties.Resources.delete,
                                    AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                                };
                                dataGridViewCars.Columns.Add(removeColumn);
                            }

                            ConfigureDataGridView();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowCustomMessageBox("An error occurred while retrieving data: " + ex.Message, "Error", true);
                }
            }
        }

        private void ConfigureDataGridView()
        {
            dataGridViewCars.ReadOnly = true;
            dataGridViewCars.RowHeadersVisible = false;
            dataGridViewCars.AutoGenerateColumns = true;
            dataGridViewCars.RowTemplate.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dataGridViewCars.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCars.ColumnHeadersHeight = 50;

            var columns = new[]
            {
                new { Name = "id", Header = "Car ID", Width = 120 },
                new { Name = "make", Header = "Make", Width = 110 },
                new { Name = "model", Header = "Model", Width = 120 },
                new { Name = "year", Header = "Year", Width = 120 },
                new { Name = "price", Header = "Selling Price", Width = 150 },
                new { Name = "buying_price", Header = "Buying Price", Width = 150 },
                new { Name = "mileage", Header = "Mileage", Width = 150 },
                new { Name = "color", Header = "Color", Width = 100 },
                new { Name = "transmission", Header = "Transmission", Width = 145 },
                new { Name = "image", Header = "Image", Width = 230 }
            };

            foreach (var col in columns)
            {
                dataGridViewCars.Columns[col.Name].HeaderText = col.Header;
                dataGridViewCars.Columns[col.Name].Width = col.Width;
                dataGridViewCars.Columns[col.Name].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridViewCars.Columns[col.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewCars.Columns[col.Name].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            DataGridViewImageColumn imageColumn = (DataGridViewImageColumn)dataGridViewCars.Columns["image"];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;

            dataGridViewCars.Columns["id"].Visible = false;
            dataGridViewCars.RowTemplate.Height = 75;

            if (dataGridViewCars.Columns.Contains("Edit"))
            {
                dataGridViewCars.Columns["Edit"].Width = 150;
            }
            if (dataGridViewCars.Columns.Contains("Remove"))
            {
                dataGridViewCars.Columns["Remove"].Width = 150;
            }

            dataGridViewCars.CellFormatting += (sender, e) =>
            {
                if (e.ColumnIndex == dataGridViewCars.Columns["Edit"].Index && e.RowIndex >= 0)
                {
                    DataGridViewButtonCell buttonCell = dataGridViewCars.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;
                    if (buttonCell != null)
                    {
                        dataGridViewCars.Rows[e.RowIndex].Height = 150;
                        dataGridViewCars.Columns[e.ColumnIndex].Width = 150;
                        dataGridViewCars.Cursor = Cursors.Hand;
                    }
                }
                if (e.ColumnIndex == dataGridViewCars.Columns["Remove"].Index && e.RowIndex >= 0)
                {
                    DataGridViewButtonCell buttonCell = dataGridViewCars.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;
                    if (buttonCell != null)
                    {
                        dataGridViewCars.Rows[e.RowIndex].Height = 150;
                        dataGridViewCars.Columns[e.ColumnIndex].Width = 150;
                        dataGridViewCars.Cursor = Cursors.Hand;
                    }
                }
            };
        }

        private void SetPlaceholder(RichTextBox richTextBox, string placeholderText)
        {
            richTextBox.Text = placeholderText;
            richTextBox.ForeColor = Color.Gray;
            richTextBox.Font = new Font(richTextBox.Font.FontFamily, placeholderFontSize, FontStyle.Bold);
        }

        private void RemovePlaceholder(RichTextBox richTextBox, string placeholderText)
        {
            if (richTextBox.Text == placeholderText)
            {
                richTextBox.Text = "";
                richTextBox.ForeColor = Color.Black;
                richTextBox.Font = new Font(richTextBox.Font.FontFamily, inputFontSize, FontStyle.Regular);
            }
        }

        private void richTextBox_Enter(object sender, EventArgs e)
        {
            RichTextBox richTextBox = sender as RichTextBox;
            if (richTextBox != null)
            {
                int index = Array.IndexOf(placeholders, richTextBox.Text);
                if (index >= 0) RemovePlaceholder(richTextBox, placeholders[index]);
            }
        }

        private void richTextBox_Leave(object sender, EventArgs e)
        {
            RichTextBox richTextBox = sender as RichTextBox;
            if (richTextBox != null && string.IsNullOrWhiteSpace(richTextBox.Text))
            {
                int index = Array.IndexOf(placeholders, richTextBox.Tag.ToString());
                if (index >= 0) SetPlaceholder(richTextBox, placeholders[index]);
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    picCarImage.Image = Image.FromFile(openFileDialog.FileName);
                    picCarImage.SizeMode = PictureBoxSizeMode.StretchImage;
                    picCarImage.BringToFront();
                }
            }
        }

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

        private void dataGridViewCars_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == dataGridViewCars.Columns["Edit"].Index)
                {
                    DataGridViewRow row = dataGridViewCars.Rows[e.RowIndex];
                    PopulateFormFieldsFromRow(row);
                }
                else if (e.ColumnIndex == dataGridViewCars.Columns["Remove"].Index)
                {
                    int id = Convert.ToInt32(dataGridViewCars.Rows[e.RowIndex].Cells["id"].Value);
                    DeleteRowFromDatabase(id);
                    LoadCarsData();
                }
            }
        }

        private void DeleteRowFromDatabase(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE cars SET is_deleted = TRUE WHERE id = @Id";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }
                    ShowCustomMessageBox("Car deleted successfully!", "Success", true);
                }
                catch (Exception ex)
                {
                    ShowCustomMessageBox("An error occurred while marking the car as deleted: " + ex.Message, "Error", true);
                }
            }
        }

        private void PopulateFormFieldsFromRow(DataGridViewRow row)
        {
            txtMake.Text = row.Cells["make"].Value.ToString();
            txtModel.Text = row.Cells["model"].Value.ToString();
            txtYear.Text = row.Cells["year"].Value.ToString();
            txtPrice.Text = row.Cells["price"].Value.ToString();
            txtBuyingPrice.Text = row.Cells["buying_price"].Value.ToString();
            txtMileage.Text = row.Cells["mileage"].Value.ToString();
            txtColor.Text = row.Cells["color"].Value.ToString();
            cmbTransmission.SelectedItem = row.Cells["transmission"].Value.ToString();

            if (row.Cells["image"].Value != DBNull.Value)
            {
                byte[] imgBytes = (byte[])row.Cells["image"].Value;
                using (MemoryStream ms = new MemoryStream(imgBytes))
                {
                    picCarImage.Image = Image.FromStream(ms);
                }
            }
            else
            {
                picCarImage.Image = null;
            }

            btnSave.Text = "Update";
            btnSave.BackColor = Color.Orange;
            lblCarBehavior.Text = "Update Car";
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchValue = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchValue))
            {
                LoadCarsData();
            }
            else
            {
                var carsTable = (DataTable)dataGridViewCars.DataSource;
                var dv = carsTable.DefaultView;
                dv.RowFilter = $"make LIKE '%{searchValue}%'";
                dataGridViewCars.DataSource = dv.ToTable();
            }
        }

        private void ClearInputFields()
        {
            txtMake.Clear();
            txtModel.Clear();
            txtYear.Clear();
            txtPrice.Clear();
            txtMileage.Clear();
            txtColor.Clear();
            txtSearch.Clear();
            txtBuyingPrice.Clear();

            SetPlaceholder(txtMake, placeholders[0]);
            SetPlaceholder(txtModel, placeholders[1]);
            SetPlaceholder(txtYear, placeholders[2]);
            SetPlaceholder(txtPrice, placeholders[3]);
            SetPlaceholder(txtBuyingPrice, placeholders[4]);
            SetPlaceholder(txtMileage, placeholders[5]);
            SetPlaceholder(txtColor, placeholders[6]);
            SetPlaceholder(txtSearch, placeholders[7]);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            lblCarBehavior.Text = "Add Car";
            ClearInputFields();
            LoadCarsData();
            btnSave.Text = "Save";
            btnSave.BackColor = Color.ForestGreen;
            picCarImage.Image = null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (isSaving) return;
            isSaving = true;
            try
            {
                string make = txtMake.Text.Trim();
                string model = txtModel.Text.Trim();
                int year = int.Parse(txtYear.Text.Trim());
                decimal price = decimal.Parse(txtPrice.Text.Trim());
                decimal buying_price = decimal.Parse(txtBuyingPrice.Text.Trim());
                int mileage = int.Parse(txtMileage.Text.Trim());
                string color = txtColor.Text.Trim();
                string transmission = cmbTransmission.SelectedItem.ToString();
                byte[] imageBytes = picCarImage.Image != null ? ImageToByteArray(picCarImage.Image) : null;

                // Validate if required fields are filled in
                if (string.IsNullOrWhiteSpace(make) ||
                    string.IsNullOrWhiteSpace(model) ||
                    string.IsNullOrWhiteSpace(color))
                {
                    ShowCustomMessageBox("Please fill in all fields with valid data.", "Input Error", true);
                    return;
                }

                // Validate year
                string yearPattern = @"^\d{4}$"; // Matches a 4-digit year
                if (!Regex.IsMatch(txtYear.Text.Trim(), yearPattern))
                {
                    ShowCustomMessageBox("Please enter a valid year (e.g., 2024).", "Input Error", true);
                    return;
                }

                // Validate price and buying price
                string pricePattern = @"^\d+(\.\d{1,2})?$"; // Matches a number with up to 2 decimal places
                if (!Regex.IsMatch(txtPrice.Text.Trim(), pricePattern) ||
                    !Regex.IsMatch(txtBuyingPrice.Text.Trim(), pricePattern))
                {
                    ShowCustomMessageBox("Please enter a valid price or buying price (e.g., 15000.00).", "Input Error", true);
                    return;
                }

                // Validate mileage
                string mileagePattern = @"^\d+$"; // Matches only digits
                if (!Regex.IsMatch(txtMileage.Text.Trim(), mileagePattern))
                {
                    ShowCustomMessageBox("Please enter a valid mileage (e.g., 12000).", "Input Error", true);
                    return;
                }

                using (var connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = btnSave.Text == "Update"
                            ? "UPDATE cars SET make = @Make, model = @Model, year = @Year, price = @Price, buying_price = @BuyingPrice, mileage = @Mileage, color = @Color, transmission = @Transmission, image = @Image, is_deleted = 0 WHERE id = @Id"
                            : "INSERT INTO cars (make, model, year, price, buying_price, mileage, color, transmission, image, is_deleted) VALUES (@Make, @Model, @Year, @Price, @BuyingPrice, @Mileage, @Color, @Transmission, @Image, 0)";

                        using (var cmd = new MySqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@Make", make);
                            cmd.Parameters.AddWithValue("@Model", model);
                            cmd.Parameters.AddWithValue("@Year", year);
                            cmd.Parameters.AddWithValue("@Price", price);
                            cmd.Parameters.AddWithValue("@BuyingPrice", buying_price);
                            cmd.Parameters.AddWithValue("@Mileage", mileage);
                            cmd.Parameters.AddWithValue("@Color", color);
                            cmd.Parameters.AddWithValue("@Transmission", transmission);
                            cmd.Parameters.AddWithValue("@Image", imageBytes);

                            if (btnSave.Text == "Update")
                            {
                                int id = Convert.ToInt32(dataGridViewCars.CurrentRow.Cells["id"].Value);
                                cmd.Parameters.AddWithValue("@Id", id);
                            }

                            cmd.ExecuteNonQuery();

                            ShowCustomMessageBox($"Car {(btnSave.Text == "Update" ? "updated" : "saved")} successfully!", "Success", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowCustomMessageBox($"An error occurred while saving/updating the car: {ex.Message}", "Error", true);
                    }
                }

                picCarImage.Image = null;
                ClearInputFields();
                LoadCarsData();
                lblCarBehavior.Text = "Add Car";
                btnSave.Text = "Save";
                btnSave.BackColor = Color.ForestGreen;
            }

            finally
            {
                isSaving = false;
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

    }
}