namespace CarTraderApp
{
    partial class CustomMessageBox
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
            lblTitle = new Label();
            panel1 = new Panel();
            lblMessage = new Label();
            btnSave = new Button();
            btncancle = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.FlatStyle = FlatStyle.Popup;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.Black;
            lblTitle.Location = new Point(12, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(97, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "label1";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Location = new Point(0, -1);
            panel1.Name = "panel1";
            panel1.Size = new Size(569, 60);
            panel1.TabIndex = 2;
            // 
            // lblMessage
            // 
            lblMessage.AutoSize = true;
            lblMessage.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMessage.Location = new Point(16, 86);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(65, 28);
            lblMessage.TabIndex = 3;
            lblMessage.Text = "label3";
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(52, 73, 94);
            btnSave.Cursor = Cursors.Hand;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(430, 169);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(85, 41);
            btnSave.TabIndex = 6;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btncancle
            // 
            btncancle.BackColor = Color.FromArgb(52, 73, 94);
            btncancle.Cursor = Cursors.Hand;
            btncancle.FlatAppearance.BorderSize = 0;
            btncancle.FlatStyle = FlatStyle.Flat;
            btncancle.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btncancle.ForeColor = Color.White;
            btncancle.Location = new Point(322, 169);
            btncancle.Name = "btncancle";
            btncancle.Size = new Size(79, 41);
            btncancle.TabIndex = 7;
            btncancle.Text = "Cancle";
            btncancle.UseVisualStyleBackColor = false;
            btncancle.Click += btncancle_Click;
            // 
            // CustomMessageBox
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            ClientSize = new Size(535, 226);
            ControlBox = false;
            Controls.Add(btncancle);
            Controls.Add(btnSave);
            Controls.Add(lblMessage);
            Controls.Add(lblTitle);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Location = new Point(0, 1000);
            Name = "CustomMessageBox";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.Manual;
            Text = "CustomMessageBox";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Panel panel1;
        private Label lblMessage;
        private Button btnSave;
        private Button btncancle;
    }
}