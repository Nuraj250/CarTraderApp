namespace CarTraderApp
{
    partial class ManageCarPartsControl
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
            lblBehaviour = new Label();
            txtPartName = new RichTextBox();
            txtPrice = new RichTextBox();
            txtManufacturer = new RichTextBox();
            txtDescription = new RichTextBox();
            btnCancle = new Button();
            label2 = new Label();
            panel1 = new Panel();
            btnUpload = new Button();
            picPartImage = new PictureBox();
            btnSave = new Button();
            dataGridViewCarparts = new DataGridView();
            txtSearch = new RichTextBox();
            pictureSearch = new PictureBox();
            openFileDialog1 = new OpenFileDialog();
            txtBuyingPrice = new RichTextBox();
            txtQuentity = new RichTextBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picPartImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCarparts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureSearch).BeginInit();
            SuspendLayout();
            // 
            // lblBehaviour
            // 
            lblBehaviour.AutoSize = true;
            lblBehaviour.BackColor = Color.FromArgb(0, 151, 167);
            lblBehaviour.Font = new Font("Microsoft Sans Serif", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBehaviour.ForeColor = Color.White;
            lblBehaviour.Location = new Point(81, 34);
            lblBehaviour.Name = "lblBehaviour";
            lblBehaviour.Size = new Size(188, 32);
            lblBehaviour.TabIndex = 9;
            lblBehaviour.Text = "Add Car Part";
            // 
            // txtPartName
            // 
            txtPartName.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPartName.Location = new Point(106, 107);
            txtPartName.Name = "txtPartName";
            txtPartName.Size = new Size(582, 38);
            txtPartName.TabIndex = 10;
            txtPartName.Text = "";
            // 
            // txtPrice
            // 
            txtPrice.Font = new Font("Microsoft Sans Serif", 9F);
            txtPrice.Location = new Point(760, 105);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(289, 38);
            txtPrice.TabIndex = 12;
            txtPrice.Text = "";
            // 
            // txtManufacturer
            // 
            txtManufacturer.Font = new Font("Microsoft Sans Serif", 9F);
            txtManufacturer.Location = new Point(106, 228);
            txtManufacturer.Name = "txtManufacturer";
            txtManufacturer.Size = new Size(582, 38);
            txtManufacturer.TabIndex = 13;
            txtManufacturer.Text = "";
            // 
            // txtDescription
            // 
            txtDescription.Font = new Font("Microsoft Sans Serif", 9F);
            txtDescription.Location = new Point(107, 166);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(582, 38);
            txtDescription.TabIndex = 14;
            txtDescription.Text = "";
            // 
            // btnCancle
            // 
            btnCancle.BackColor = Color.DarkGray;
            btnCancle.FlatStyle = FlatStyle.Popup;
            btnCancle.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            btnCancle.Location = new Point(106, 293);
            btnCancle.Margin = new Padding(3, 4, 3, 4);
            btnCancle.Name = "btnCancle";
            btnCancle.Size = new Size(582, 43);
            btnCancle.TabIndex = 15;
            btnCancle.Text = "Cancel";
            btnCancle.UseVisualStyleBackColor = false;
            btnCancle.Click += btnCancle_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 16.2F, FontStyle.Bold);
            label2.ForeColor = Color.White;
            label2.Location = new Point(81, 399);
            label2.Name = "label2";
            label2.Size = new Size(200, 32);
            label2.TabIndex = 18;
            label2.Text = "View Car Part";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(0, 151, 167);
            panel1.Controls.Add(txtQuentity);
            panel1.Controls.Add(btnUpload);
            panel1.Controls.Add(picPartImage);
            panel1.Controls.Add(lblBehaviour);
            panel1.Controls.Add(btnCancle);
            panel1.Controls.Add(btnSave);
            panel1.Controls.Add(txtDescription);
            panel1.Controls.Add(txtPrice);
            panel1.Controls.Add(txtManufacturer);
            panel1.Controls.Add(txtPartName);
            panel1.Controls.Add(dataGridViewCarparts);
            panel1.Controls.Add(txtSearch);
            panel1.Controls.Add(pictureSearch);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(36, 33);
            panel1.Margin = new Padding(5);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(3);
            panel1.Size = new Size(1502, 854);
            panel1.TabIndex = 19;
            // 
            // btnUpload
            // 
            btnUpload.BackColor = Color.ForestGreen;
            btnUpload.BackgroundImageLayout = ImageLayout.Center;
            btnUpload.FlatStyle = FlatStyle.Popup;
            btnUpload.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnUpload.ForeColor = SystemColors.ActiveCaptionText;
            btnUpload.Location = new Point(1231, 166);
            btnUpload.Margin = new Padding(3, 4, 3, 4);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new Size(95, 43);
            btnUpload.TabIndex = 22;
            btnUpload.Text = "Upload";
            btnUpload.UseVisualStyleBackColor = false;
            // 
            // picPartImage
            // 
            picPartImage.ErrorImage = Properties.Resources.icons8_shopping_cart_64;
            picPartImage.Location = new Point(1055, 166);
            picPartImage.Name = "picPartImage";
            picPartImage.Size = new Size(132, 100);
            picPartImage.TabIndex = 21;
            picPartImage.TabStop = false;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.ForestGreen;
            btnSave.BackgroundImageLayout = ImageLayout.Center;
            btnSave.FlatStyle = FlatStyle.Popup;
            btnSave.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            btnSave.ForeColor = SystemColors.ActiveCaptionText;
            btnSave.Location = new Point(760, 293);
            btnSave.Margin = new Padding(3, 4, 3, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(582, 43);
            btnSave.TabIndex = 19;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // dataGridViewCarparts
            // 
            dataGridViewCarparts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCarparts.Location = new Point(37, 534);
            dataGridViewCarparts.Name = "dataGridViewCarparts";
            dataGridViewCarparts.RowHeadersWidth = 51;
            dataGridViewCarparts.Size = new Size(1420, 278);
            dataGridViewCarparts.TabIndex = 17;
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtSearch.Location = new Point(84, 458);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(299, 47);
            txtSearch.TabIndex = 20;
            txtSearch.Text = "";
            // 
            // pictureSearch
            // 
            pictureSearch.BackgroundImage = Properties.Resources.icons8_search_64;
            pictureSearch.BackgroundImageLayout = ImageLayout.Center;
            pictureSearch.Location = new Point(406, 459);
            pictureSearch.Name = "pictureSearch";
            pictureSearch.Size = new Size(57, 46);
            pictureSearch.TabIndex = 20;
            pictureSearch.TabStop = false;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtBuyingPrice
            // 
            txtBuyingPrice.Font = new Font("Microsoft Sans Serif", 9F);
            txtBuyingPrice.Location = new Point(1091, 140);
            txtBuyingPrice.Name = "txtBuyingPrice";
            txtBuyingPrice.Size = new Size(287, 38);
            txtBuyingPrice.TabIndex = 23;
            txtBuyingPrice.Text = "";
            // 
            // txtQuentity
            // 
            txtQuentity.Font = new Font("Microsoft Sans Serif", 9F);
            txtQuentity.Location = new Point(760, 171);
            txtQuentity.Name = "txtQuentity";
            txtQuentity.Size = new Size(289, 38);
            txtQuentity.TabIndex = 23;
            txtQuentity.Text = "";
            // 
            // ManageCarPartsControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            Controls.Add(txtBuyingPrice);
            Controls.Add(panel1);
            Name = "ManageCarPartsControl";
            Size = new Size(1574, 919);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picPartImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCarparts).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureSearch).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button btnSave;
        private Label lblBehaviour;
        private RichTextBox txtPartName;
        private RichTextBox txtPrice;
        private RichTextBox txtManufacturer;
        private RichTextBox txtDescription;
        private Button btnCancle;
        private Label label2;
        private Panel panel1;
        private RichTextBox txtSearch;
        private PictureBox pictureSearch;
        private DataGridView dataGridViewCarparts;
        private Button btnUpload;
        private PictureBox picPartImage;
        private OpenFileDialog openFileDialog1;
        private RichTextBox txtBuyingPrice;
        private RichTextBox txtQuentity;
    }
}
