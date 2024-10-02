namespace CarTraderApp
{
    partial class ManageUserControl
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
            txtPassword = new RichTextBox();
            cmbType = new ComboBox();
            btnUpload = new Button();
            picUserImage = new PictureBox();
            lblUserBehavior = new Label();
            btnCancle = new Button();
            btnSave = new Button();
            txtContactNumber = new RichTextBox();
            txtEmail = new RichTextBox();
            txtUsername = new RichTextBox();
            dataGridViewUsers = new DataGridView();
            txtSearch = new RichTextBox();
            pictureSearch = new PictureBox();
            label2 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picUserImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewUsers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureSearch).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(0, 140, 153);
            panel1.Controls.Add(txtPassword);
            panel1.Controls.Add(cmbType);
            panel1.Controls.Add(btnUpload);
            panel1.Controls.Add(picUserImage);
            panel1.Controls.Add(lblUserBehavior);
            panel1.Controls.Add(btnCancle);
            panel1.Controls.Add(btnSave);
            panel1.Controls.Add(txtContactNumber);
            panel1.Controls.Add(txtEmail);
            panel1.Controls.Add(txtUsername);
            panel1.Controls.Add(dataGridViewUsers);
            panel1.Controls.Add(txtSearch);
            panel1.Controls.Add(pictureSearch);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(36, 32);
            panel1.Margin = new Padding(5);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(3);
            panel1.Size = new Size(1502, 854);
            panel1.TabIndex = 22;
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Microsoft Sans Serif", 9F);
            txtPassword.Location = new Point(106, 232);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(582, 38);
            txtPassword.TabIndex = 24;
            txtPassword.Text = "";
            // 
            // cmbType
            // 
            cmbType.DropDownHeight = 500;
            cmbType.DropDownWidth = 500;
            cmbType.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbType.FormattingEnabled = true;
            cmbType.IntegralHeight = false;
            cmbType.Location = new Point(1056, 112);
            cmbType.Margin = new Padding(10);
            cmbType.Name = "cmbType";
            cmbType.Size = new Size(286, 33);
            cmbType.TabIndex = 23;
            // 
            // btnUpload
            // 
            btnUpload.BackColor = Color.ForestGreen;
            btnUpload.BackgroundImageLayout = ImageLayout.Center;
            btnUpload.FlatStyle = FlatStyle.Popup;
            btnUpload.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnUpload.ForeColor = SystemColors.ActiveCaptionText;
            btnUpload.Location = new Point(937, 167);
            btnUpload.Margin = new Padding(3, 4, 3, 4);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new Size(95, 43);
            btnUpload.TabIndex = 22;
            btnUpload.Text = "Upload";
            btnUpload.UseVisualStyleBackColor = false;
            // 
            // picUserImage
            // 
            picUserImage.Location = new Point(761, 169);
            picUserImage.Name = "picUserImage";
            picUserImage.Size = new Size(130, 101);
            picUserImage.TabIndex = 21;
            picUserImage.TabStop = false;
            // 
            // lblUserBehavior
            // 
            lblUserBehavior.AutoSize = true;
            lblUserBehavior.BackColor = Color.FromArgb(0, 140, 153);
            lblUserBehavior.Font = new Font("Microsoft Sans Serif", 16.2F, FontStyle.Bold);
            lblUserBehavior.ForeColor = Color.White;
            lblUserBehavior.Location = new Point(81, 34);
            lblUserBehavior.Name = "lblUserBehavior";
            lblUserBehavior.Size = new Size(139, 32);
            lblUserBehavior.TabIndex = 9;
            lblUserBehavior.Text = "Add User";
            // 
            // btnCancle
            // 
            btnCancle.BackColor = Color.DarkGray;
            btnCancle.FlatStyle = FlatStyle.Popup;
            btnCancle.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancle.Location = new Point(106, 293);
            btnCancle.Margin = new Padding(3, 4, 3, 4);
            btnCancle.Name = "btnCancle";
            btnCancle.Size = new Size(582, 43);
            btnCancle.TabIndex = 15;
            btnCancle.Text = "Cancel";
            btnCancle.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.ForestGreen;
            btnSave.BackgroundImageLayout = ImageLayout.Center;
            btnSave.FlatStyle = FlatStyle.Popup;
            btnSave.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSave.ForeColor = SystemColors.ActiveCaptionText;
            btnSave.Location = new Point(760, 293);
            btnSave.Margin = new Padding(3, 4, 3, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(582, 43);
            btnSave.TabIndex = 19;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // txtContactNumber
            // 
            txtContactNumber.Font = new Font("Microsoft Sans Serif", 9F);
            txtContactNumber.Location = new Point(760, 107);
            txtContactNumber.Name = "txtContactNumber";
            txtContactNumber.Size = new Size(271, 38);
            txtContactNumber.TabIndex = 12;
            txtContactNumber.Text = "";
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Microsoft Sans Serif", 9F);
            txtEmail.Location = new Point(106, 165);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(582, 38);
            txtEmail.TabIndex = 13;
            txtEmail.Text = "";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtUsername.Location = new Point(106, 107);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(582, 38);
            txtUsername.TabIndex = 10;
            txtUsername.Text = "";
            // 
            // dataGridViewUsers
            // 
            dataGridViewUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewUsers.Location = new Point(117, 534);
            dataGridViewUsers.Name = "dataGridViewUsers";
            dataGridViewUsers.RowHeadersWidth = 51;
            dataGridViewUsers.Size = new Size(1264, 278);
            dataGridViewUsers.TabIndex = 17;
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Microsoft Sans Serif", 9F);
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
            pictureSearch.Location = new Point(398, 458);
            pictureSearch.Name = "pictureSearch";
            pictureSearch.Size = new Size(49, 44);
            pictureSearch.TabIndex = 20;
            pictureSearch.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 16.2F, FontStyle.Bold);
            label2.ForeColor = Color.White;
            label2.Location = new Point(81, 399);
            label2.Name = "label2";
            label2.Size = new Size(151, 32);
            label2.TabIndex = 18;
            label2.Text = "View User";
            // 
            // ManageUserControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            Controls.Add(panel1);
            Name = "ManageUserControl";
            Size = new Size(1574, 919);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picUserImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewUsers).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureSearch).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btnUpload;
        private PictureBox picUserImage;
        private Label lblUserBehavior;
        private Button btnCancle;
        private Button btnSave;
        private RichTextBox txtContactNumber;
        private RichTextBox txtEmail;
        private RichTextBox txtUsername;
        private DataGridView dataGridViewUsers;
        private RichTextBox txtSearch;
        private PictureBox pictureSearch;
        private Label label2;
        private ComboBox cmbType;
        private RichTextBox txtPassword;
    }
}
