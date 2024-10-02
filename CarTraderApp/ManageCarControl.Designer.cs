namespace CarTraderApp
{
    partial class ManageCarControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            txtBuyingPrice = new RichTextBox();
            cmbTransmission = new ComboBox();
            txtColor = new RichTextBox();
            txtPrice = new RichTextBox();
            btnUpload = new Button();
            picCarImage = new PictureBox();
            lblCarBehavior = new Label();
            btnCancel = new Button();
            btnSave = new Button();
            txtModel = new RichTextBox();
            txtMileage = new RichTextBox();
            txtYear = new RichTextBox();
            txtMake = new RichTextBox();
            dataGridViewCars = new DataGridView();
            txtSearch = new RichTextBox();
            pictureSearch = new PictureBox();
            label2 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picCarImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCars).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureSearch).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(0, 140, 153);
            panel1.Controls.Add(txtBuyingPrice);
            panel1.Controls.Add(cmbTransmission);
            panel1.Controls.Add(txtColor);
            panel1.Controls.Add(txtPrice);
            panel1.Controls.Add(btnUpload);
            panel1.Controls.Add(picCarImage);
            panel1.Controls.Add(lblCarBehavior);
            panel1.Controls.Add(btnCancel);
            panel1.Controls.Add(btnSave);
            panel1.Controls.Add(txtModel);
            panel1.Controls.Add(txtMileage);
            panel1.Controls.Add(txtYear);
            panel1.Controls.Add(txtMake);
            panel1.Controls.Add(dataGridViewCars);
            panel1.Controls.Add(txtSearch);
            panel1.Controls.Add(pictureSearch);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(37, 36);
            panel1.Margin = new Padding(5);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(3);
            panel1.Size = new Size(1502, 854);
            panel1.TabIndex = 20;
            // 
            // txtBuyingPrice
            // 
            txtBuyingPrice.Location = new Point(432, 280);
            txtBuyingPrice.Name = "txtBuyingPrice";
            txtBuyingPrice.Size = new Size(257, 38);
            txtBuyingPrice.TabIndex = 27;
            txtBuyingPrice.Text = "";
            // 
            // cmbTransmission
            // 
            cmbTransmission.DropDownHeight = 500;
            cmbTransmission.DropDownWidth = 500;
            cmbTransmission.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbTransmission.FormattingEnabled = true;
            cmbTransmission.IntegralHeight = false;
            cmbTransmission.Location = new Point(759, 232);
            cmbTransmission.Margin = new Padding(10);
            cmbTransmission.Name = "cmbTransmission";
            cmbTransmission.Size = new Size(286, 33);
            cmbTransmission.TabIndex = 26;
            // 
            // txtColor
            // 
            txtColor.Location = new Point(760, 166);
            txtColor.Name = "txtColor";
            txtColor.Size = new Size(285, 38);
            txtColor.TabIndex = 24;
            txtColor.Text = "";
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(106, 280);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(302, 38);
            txtPrice.TabIndex = 23;
            txtPrice.Text = "";
            // 
            // btnUpload
            // 
            btnUpload.BackColor = Color.ForestGreen;
            btnUpload.BackgroundImageLayout = ImageLayout.Center;
            btnUpload.FlatStyle = FlatStyle.Popup;
            btnUpload.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnUpload.ForeColor = SystemColors.ActiveCaptionText;
            btnUpload.Location = new Point(1247, 169);
            btnUpload.Margin = new Padding(3, 4, 3, 4);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new Size(95, 43);
            btnUpload.TabIndex = 22;
            btnUpload.Text = "Upload";
            btnUpload.UseVisualStyleBackColor = false;
            // 
            // picCarImage
            // 
            picCarImage.Location = new Point(1077, 169);
            picCarImage.Name = "picCarImage";
            picCarImage.Size = new Size(153, 120);
            picCarImage.TabIndex = 21;
            picCarImage.TabStop = false;
            // 
            // lblCarBehavior
            // 
            lblCarBehavior.AutoSize = true;
            lblCarBehavior.BackColor = Color.FromArgb(0, 140, 153);
            lblCarBehavior.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCarBehavior.ForeColor = Color.WhiteSmoke;
            lblCarBehavior.Location = new Point(94, 32);
            lblCarBehavior.Name = "lblCarBehavior";
            lblCarBehavior.Size = new Size(120, 37);
            lblCarBehavior.TabIndex = 9;
            lblCarBehavior.Text = "Add Car";
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.DarkGray;
            btnCancel.FlatStyle = FlatStyle.Popup;
            btnCancel.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancel.Location = new Point(106, 337);
            btnCancel.Margin = new Padding(3, 4, 3, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(582, 43);
            btnCancel.TabIndex = 15;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.ForestGreen;
            btnSave.BackgroundImageLayout = ImageLayout.Center;
            btnSave.FlatStyle = FlatStyle.Popup;
            btnSave.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSave.ForeColor = SystemColors.ActiveCaptionText;
            btnSave.Location = new Point(760, 337);
            btnSave.Margin = new Padding(3, 4, 3, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(582, 43);
            btnSave.TabIndex = 19;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // txtModel
            // 
            txtModel.Location = new Point(107, 166);
            txtModel.Name = "txtModel";
            txtModel.Size = new Size(582, 38);
            txtModel.TabIndex = 14;
            txtModel.Text = "";
            // 
            // txtMileage
            // 
            txtMileage.Location = new Point(760, 107);
            txtMileage.Name = "txtMileage";
            txtMileage.Size = new Size(582, 38);
            txtMileage.TabIndex = 12;
            txtMileage.Text = "";
            // 
            // txtYear
            // 
            txtYear.Location = new Point(106, 227);
            txtYear.Name = "txtYear";
            txtYear.Size = new Size(582, 38);
            txtYear.TabIndex = 13;
            txtYear.Text = "";
            // 
            // txtMake
            // 
            txtMake.Location = new Point(106, 107);
            txtMake.Name = "txtMake";
            txtMake.Size = new Size(582, 38);
            txtMake.TabIndex = 10;
            txtMake.Text = "";
            // 
            // dataGridViewCars
            // 
            dataGridViewCars.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCars.Location = new Point(37, 534);
            dataGridViewCars.Name = "dataGridViewCars";
            dataGridViewCars.RowHeadersWidth = 51;
            dataGridViewCars.Size = new Size(1420, 278);
            dataGridViewCars.TabIndex = 17;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(84, 458);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(299, 47);
            txtSearch.TabIndex = 20;
            txtSearch.Text = "";
            // 
            // pictureSearch
            // 
            pictureSearch.BackgroundImage = Properties.Resources.icons8_search_32;
            pictureSearch.BackgroundImageLayout = ImageLayout.Center;
            pictureSearch.Location = new Point(406, 455);
            pictureSearch.Name = "pictureSearch";
            pictureSearch.Size = new Size(57, 46);
            pictureSearch.TabIndex = 20;
            pictureSearch.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.WhiteSmoke;
            label2.Location = new Point(84, 402);
            label2.Name = "label2";
            label2.Size = new Size(130, 37);
            label2.TabIndex = 18;
            label2.Text = "View Car";
            // 
            // ManageCarControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            Controls.Add(panel1);
            Name = "ManageCarControl";
            Size = new Size(1574, 919);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picCarImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCars).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureSearch).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btnUpload;
        private PictureBox picCarImage;
        private Label lblCarBehavior;
        private Button btnCancel;
        private Button btnSave;
        private RichTextBox txtModel;
        private RichTextBox txtMileage;
        private RichTextBox txtYear;
        private RichTextBox txtMake;
        private DataGridView dataGridViewCars;
        private RichTextBox txtSearch;
        private PictureBox pictureSearch;
        private Label label2;
        private RichTextBox txtPrice;
        private RichTextBox txtColor;
        private ComboBox cmbTransmission;
        private RichTextBox txtBuyingPrice;
    }
}
