namespace CarTraderApp
{
    partial class Register
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
            components = new System.ComponentModel.Container();
            panel2 = new Panel();
            panel1 = new Panel();
            pictureUser = new PictureBox();
            lblBackToLogin = new Label();
            txtReEnterPassword = new TextBox();
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            panel3 = new Panel();
            txtUserName = new TextBox();
            panel4 = new Panel();
            panel6 = new Panel();
            BtnRegister = new Button();
            panel5 = new Panel();
            imageList1 = new ImageList(components);
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureUser).BeginInit();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackgroundImage = Properties.Resources.combi_car_flat_vector_icon_1200x850;
            panel2.BackgroundImageLayout = ImageLayout.Center;
            panel2.Controls.Add(panel1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(4, 4);
            panel2.Margin = new Padding(2);
            panel2.Name = "panel2";
            panel2.Size = new Size(598, 699);
            panel2.TabIndex = 1;  
            // 
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(pictureUser);
            panel1.Controls.Add(lblBackToLogin);
            panel1.Controls.Add(txtReEnterPassword);
            panel1.Controls.Add(txtEmail);
            panel1.Controls.Add(txtPassword);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(txtUserName);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(panel6);
            panel1.Controls.Add(BtnRegister);
            panel1.Controls.Add(panel5);
            panel1.Location = new Point(49, 29);
            panel1.Name = "panel1";
            panel1.Size = new Size(493, 622);
            panel1.TabIndex = 1;
            // 
            // pictureUser
            // 
            pictureUser.BackgroundImage = Properties.Resources.icons8_trash_641;
            pictureUser.BackgroundImageLayout = ImageLayout.Stretch;
            pictureUser.ErrorImage = Properties.Resources.icons8_male_user_64;
            pictureUser.Location = new Point(169, 16);
            pictureUser.Name = "pictureUser";
            pictureUser.Size = new Size(156, 142);
            pictureUser.TabIndex = 22;
            pictureUser.TabStop = false;
            // 
            // lblBackToLogin
            // 
            lblBackToLogin.AutoSize = true;
            lblBackToLogin.Font = new Font("Microsoft Sans Serif", 10F);
            lblBackToLogin.ForeColor = Color.FromArgb(0, 123, 138);
            lblBackToLogin.Location = new Point(145, 565);
            lblBackToLogin.Margin = new Padding(2, 0, 2, 0);
            lblBackToLogin.Name = "lblBackToLogin";
            lblBackToLogin.Size = new Size(214, 20);
            lblBackToLogin.TabIndex = 5;
            lblBackToLogin.Text = "Already Have An Account ?";
            lblBackToLogin.Click += lblBackToLogin_Click;
            // 
            // txtReEnterPassword
            // 
            txtReEnterPassword.BorderStyle = BorderStyle.None;
            txtReEnterPassword.Font = new Font("Times New Roman", 12F);
            txtReEnterPassword.ForeColor = Color.FromArgb(52, 73, 94);
            txtReEnterPassword.Location = new Point(67, 432);
            txtReEnterPassword.Margin = new Padding(2);
            txtReEnterPassword.Name = "txtReEnterPassword";
            txtReEnterPassword.Size = new Size(371, 23);
            txtReEnterPassword.TabIndex = 18;
            // 
            // txtEmail
            // 
            txtEmail.BorderStyle = BorderStyle.None;
            txtEmail.Font = new Font("Times New Roman", 12F);
            txtEmail.ForeColor = Color.FromArgb(52, 73, 94);
            txtEmail.Location = new Point(67, 219);
            txtEmail.Margin = new Padding(2);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(371, 23);
            txtEmail.TabIndex = 9;
            txtEmail.TextChanged += txtEmail_TextChanged;
            // 
            // txtPassword
            // 
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Font = new Font("Times New Roman", 12F);
            txtPassword.ForeColor = Color.FromArgb(52, 73, 94);
            txtPassword.Location = new Point(68, 362);
            txtPassword.Margin = new Padding(2);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(371, 23);
            txtPassword.TabIndex = 16;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(52, 73, 94);
            panel3.Location = new Point(67, 246);
            panel3.Margin = new Padding(2);
            panel3.Name = "panel3";
            panel3.Size = new Size(371, 4);
            panel3.TabIndex = 8;
            // 
            // txtUserName
            // 
            txtUserName.BorderStyle = BorderStyle.None;
            txtUserName.Font = new Font("Times New Roman", 12F);
            txtUserName.ForeColor = Color.FromArgb(52, 73, 94);
            txtUserName.Location = new Point(67, 289);
            txtUserName.Margin = new Padding(2);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(371, 23);
            txtUserName.TabIndex = 12;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(52, 73, 94);
            panel4.Location = new Point(67, 315);
            panel4.Margin = new Padding(2);
            panel4.Name = "panel4";
            panel4.Size = new Size(371, 4);
            panel4.TabIndex = 11;
            // 
            // panel6
            // 
            panel6.BackColor = Color.FromArgb(52, 73, 94);
            panel6.Location = new Point(67, 388);
            panel6.Margin = new Padding(2);
            panel6.Name = "panel6";
            panel6.Size = new Size(371, 4);
            panel6.TabIndex = 15;
            // 
            // BtnRegister
            // 
            BtnRegister.BackColor = Color.FromArgb(0, 123, 138);
            BtnRegister.FlatAppearance.BorderSize = 0;
            BtnRegister.FlatStyle = FlatStyle.Flat;
            BtnRegister.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
            BtnRegister.ForeColor = Color.White;
            BtnRegister.Location = new Point(67, 498);
            BtnRegister.Margin = new Padding(2);
            BtnRegister.Name = "BtnRegister";
            BtnRegister.Size = new Size(371, 44);
            BtnRegister.TabIndex = 4;
            BtnRegister.Text = "Register";
            BtnRegister.UseVisualStyleBackColor = false;
            BtnRegister.Click += BtnRegister_Click;
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(52, 73, 94);
            panel5.Location = new Point(67, 458);
            panel5.Margin = new Padding(2);
            panel5.Name = "panel5";
            panel5.Size = new Size(371, 4);
            panel5.TabIndex = 17;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageSize = new Size(16, 16);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // Register
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(606, 707);
            Controls.Add(panel2);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2);
            Name = "Register";
            Padding = new Padding(4);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Signup";
            panel2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureUser).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtReEnterPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button BtnRegister;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblBackToLogin;
        private Panel panel1;
        private PictureBox pictureUser;
        private ImageList imageList1;
    }
}