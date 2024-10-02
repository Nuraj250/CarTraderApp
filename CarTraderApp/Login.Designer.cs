namespace CarTraderApp
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            label4 = new Label();
            lblForgot = new Label();
            BtnLogin = new Button();
            panel2 = new Panel();
            txtUserName = new TextBox();
            txtPassword = new TextBox();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.BackgroundImage = Properties.Resources.combi_car_flat_vector_icon_1200x850;
            panel1.BackgroundImageLayout = ImageLayout.Center;
            panel1.Controls.Add(label4);
            panel1.Controls.Add(lblForgot);
            panel1.Controls.Add(BtnLogin);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(-23, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(668, 874);
            panel1.TabIndex = 0;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            label4.Location = new Point(209, 658);
            label4.Name = "label4";
            label4.Size = new Size(295, 23);
            label4.TabIndex = 5;
            label4.Text = "Don't Have An Account ? Create One";
            label4.Click += label4_Click;
            // 
            // lblForgot
            // 
            lblForgot.AutoSize = true;
            lblForgot.BackColor = Color.Transparent;
            lblForgot.Cursor = Cursors.Hand;
            lblForgot.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            lblForgot.Location = new Point(414, 518);
            lblForgot.Name = "lblForgot";
            lblForgot.Size = new Size(171, 23);
            lblForgot.TabIndex = 13;
            lblForgot.Text = "Forgotten Password?";
            lblForgot.Click += lblForgot_Click;
            // 
            // BtnLogin
            // 
            BtnLogin.BackColor = Color.FromArgb(0, 123, 138);
            BtnLogin.Cursor = Cursors.Hand;
            BtnLogin.FlatAppearance.BorderSize = 0;
            BtnLogin.FlatStyle = FlatStyle.Flat;
            BtnLogin.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            BtnLogin.ForeColor = Color.White;
            BtnLogin.Location = new Point(206, 576);
            BtnLogin.Name = "BtnLogin";
            BtnLogin.Size = new Size(300, 58);
            BtnLogin.TabIndex = 4;
            BtnLogin.Text = "Login";
            BtnLogin.UseVisualStyleBackColor = false;
            BtnLogin.Click += BtnLogin_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Transparent;
            panel2.Controls.Add(txtUserName);
            panel2.Controls.Add(txtPassword);
            panel2.Location = new Point(85, 130);
            panel2.Name = "panel2";
            panel2.Size = new Size(554, 622);
            panel2.TabIndex = 14;
            // 
            // txtUserName
            // 
            txtUserName.BackColor = SystemColors.Control;
            txtUserName.BorderStyle = BorderStyle.None;
            txtUserName.Font = new Font("Times New Roman", 12F);
            txtUserName.ForeColor = Color.DodgerBlue;
            txtUserName.Location = new Point(89, 290);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(394, 23);
            txtUserName.TabIndex = 9;
            // 
            // txtPassword
            // 
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Font = new Font("Times New Roman", 12F);
            txtPassword.ForeColor = Color.DodgerBlue;
            txtPassword.Location = new Point(89, 344);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(394, 23);
            txtPassword.TabIndex = 12;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // Login
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(650, 884);
            Controls.Add(panel1);
            Font = new Font("Lucida Console", 12F, FontStyle.Bold);
            ForeColor = Color.DodgerBlue;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            Name = "Login";
            Padding = new Padding(5);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form_Login";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label lblForgot;
        private TextBox txtPassword;
        private Button BtnLogin;
        private Label label4;
        private TextBox txtUserName;
        public Panel panel1;
        private Panel panel2;
    }
}

