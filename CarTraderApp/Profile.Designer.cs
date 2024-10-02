namespace CarTraderApp
{
    partial class Profile
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            picUserImage = new PictureBox();
            lblUsername = new Label();
            lblEmail = new Label();
            btnCancle = new Button();
            panel4 = new Panel();
            ((System.ComponentModel.ISupportInitialize)picUserImage).BeginInit();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // picUserImage
            // 
            picUserImage.ErrorImage = Properties.Resources.icons8_male_user_64;
            picUserImage.Location = new Point(70, 66);
            picUserImage.Name = "picUserImage";
            picUserImage.Size = new Size(199, 165);
            picUserImage.TabIndex = 22;
            picUserImage.TabStop = false;
            // 
            // lblUsername
            // 
            lblUsername.Anchor = AnchorStyles.Top;
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUsername.Location = new Point(70, 315);
            lblUsername.Margin = new Padding(0);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(149, 31);
            lblUsername.TabIndex = 23;
            lblUsername.Text = "lblUsername";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEmail.Location = new Point(70, 376);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(101, 31);
            lblEmail.TabIndex = 24;
            lblEmail.Text = "lblEmail";
            // 
            // btnCancle
            // 
            btnCancle.BackColor = Color.FromArgb(0, 151, 167);
            btnCancle.Cursor = Cursors.Hand;
            btnCancle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancle.ForeColor = Color.White;
            btnCancle.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancle.Location = new Point(-17, -4);
            btnCancle.Name = "btnCancle";
            btnCancle.Padding = new Padding(26, 5, 5, 5);
            btnCancle.Size = new Size(158, 58);
            btnCancle.TabIndex = 25;
            btnCancle.Text = "   Cancle";
            btnCancle.TextAlign = ContentAlignment.MiddleLeft;
            btnCancle.UseVisualStyleBackColor = false;
            btnCancle.Click += btnCancle_Click;
            // 
            // panel4
            // 
            panel4.Controls.Add(btnCancle);
            panel4.Location = new Point(349, 519);
            panel4.Margin = new Padding(0);
            panel4.Name = "panel4";
            panel4.Padding = new Padding(0, 30, 0, 0);
            panel4.Size = new Size(131, 49);
            panel4.TabIndex = 26;
            // 
            // Profile
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.FromArgb(200, 230, 230);
            ClientSize = new Size(524, 614);
            Controls.Add(lblEmail);
            Controls.Add(lblUsername);
            Controls.Add(picUserImage);
            Controls.Add(panel4);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Profile";
            Text = "Profile";
            ((System.ComponentModel.ISupportInitialize)picUserImage).EndInit();
            panel4.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox picUserImage;
        private Label lblUsername;
        private Label lblEmail;
        private Button btnCancle;
        private Panel panel4;
    }
}