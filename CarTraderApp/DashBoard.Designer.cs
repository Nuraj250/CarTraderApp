namespace CarTraderApp
{
    partial class DashBoard
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
            panel1 = new Panel();
            btnView = new Button();
            btnMax = new Button();
            lblWelcome = new Label();
            btnClose = new Button();
            btnAppClose = new Button();
            btnAppMin = new Button();
            sidebarPanel = new Panel();
            panel2 = new Panel();
            btnLogout = new Button();
            pictureUser = new PictureBox();
            reportContainer = new FlowLayoutPanel();
            panel8 = new Panel();
            btnReports = new Button();
            panel9 = new Panel();
            customerReportNav = new Button();
            panel10 = new Panel();
            orderReportNav = new Button();
            panelDashboard = new Panel();
            dashboardNav = new Button();
            panelCars = new Panel();
            btnCars = new Button();
            panelOrders = new Panel();
            btnOrders = new Button();
            panelCarParts = new Panel();
            btnCarParts = new Button();
            panelCustomers = new Panel();
            btnCustomers = new Button();
            panel3 = new Panel();
            labelNav = new Label();
            sidebarTransition = new System.Windows.Forms.Timer(components);
            reportTransition = new System.Windows.Forms.Timer(components);
            mainPanel = new Panel();
            panel1.SuspendLayout();
            sidebarPanel.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureUser).BeginInit();
            reportContainer.SuspendLayout();
            panel8.SuspendLayout();
            panel9.SuspendLayout();
            panel10.SuspendLayout();
            panelDashboard.SuspendLayout();
            panelCars.SuspendLayout();
            panelOrders.SuspendLayout();
            panelCarParts.SuspendLayout();
            panelCustomers.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top;
            panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel1.BackColor = Color.FromArgb(0, 140, 153);
            panel1.Controls.Add(btnView);
            panel1.Controls.Add(btnMax);
            panel1.Controls.Add(lblWelcome);
            panel1.Controls.Add(btnClose);
            panel1.Controls.Add(btnAppClose);
            panel1.Controls.Add(btnAppMin);
            panel1.Location = new Point(300, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1575, 62);
            panel1.TabIndex = 0;
            // 
            // btnView
            // 
            btnView.BackColor = Color.FromArgb(0, 151, 167);
            btnView.BackgroundImage = Properties.Resources.icons8_eye_32;
            btnView.BackgroundImageLayout = ImageLayout.Center;
            btnView.Cursor = Cursors.Hand;
            btnView.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnView.ForeColor = Color.White;
            btnView.ImageAlign = ContentAlignment.MiddleLeft;
            btnView.Location = new Point(1398, 5);
            btnView.Name = "btnView";
            btnView.Padding = new Padding(25, 0, 0, 0);
            btnView.Size = new Size(54, 51);
            btnView.TabIndex = 9;
            btnView.TextAlign = ContentAlignment.MiddleLeft;
            btnView.UseVisualStyleBackColor = false;
            btnView.Click += btnView_Click;
            // 
            // btnMax
            // 
            btnMax.BackColor = Color.FromArgb(0, 151, 167);
            btnMax.BackgroundImage = Properties.Resources.icons8_maximize_48__1_;
            btnMax.BackgroundImageLayout = ImageLayout.Center;
            btnMax.Cursor = Cursors.Hand;
            btnMax.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnMax.ForeColor = Color.White;
            btnMax.ImageAlign = ContentAlignment.MiddleLeft;
            btnMax.Location = new Point(1463, 5);
            btnMax.Name = "btnMax";
            btnMax.Padding = new Padding(25, 0, 0, 0);
            btnMax.Size = new Size(54, 51);
            btnMax.TabIndex = 6;
            btnMax.TextAlign = ContentAlignment.MiddleLeft;
            btnMax.UseVisualStyleBackColor = false;
            btnMax.Click += btnMax_Click;
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblWelcome.Location = new Point(869, 19);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.RightToLeft = RightToLeft.No;
            lblWelcome.Size = new Size(0, 29);
            lblWelcome.TabIndex = 3;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.FromArgb(0, 151, 167);
            btnClose.BackgroundImage = Properties.Resources.icons8_close_48;
            btnClose.BackgroundImageLayout = ImageLayout.Center;
            btnClose.Cursor = Cursors.Hand;
            btnClose.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClose.ForeColor = Color.White;
            btnClose.ImageAlign = ContentAlignment.MiddleRight;
            btnClose.Location = new Point(1520, 9);
            btnClose.Name = "btnClose";
            btnClose.Padding = new Padding(25, 0, 0, 0);
            btnClose.Size = new Size(46, 45);
            btnClose.TabIndex = 4;
            btnClose.TextAlign = ContentAlignment.MiddleLeft;
            btnClose.UseVisualStyleBackColor = false;
            // 
            // btnAppClose
            // 
            btnAppClose.BackColor = Color.Transparent;
            btnAppClose.BackgroundImageLayout = ImageLayout.Stretch;
            btnAppClose.FlatStyle = FlatStyle.Flat;
            btnAppClose.ForeColor = Color.DodgerBlue;
            btnAppClose.Location = new Point(1805, 12);
            btnAppClose.Name = "btnAppClose";
            btnAppClose.Size = new Size(59, 51);
            btnAppClose.TabIndex = 8;
            btnAppClose.UseVisualStyleBackColor = false;
            // 
            // btnAppMin
            // 
            btnAppMin.BackColor = Color.Transparent;
            btnAppMin.BackgroundImageLayout = ImageLayout.Stretch;
            btnAppMin.FlatStyle = FlatStyle.Flat;
            btnAppMin.ForeColor = Color.DodgerBlue;
            btnAppMin.Location = new Point(1740, 12);
            btnAppMin.Name = "btnAppMin";
            btnAppMin.Size = new Size(59, 51);
            btnAppMin.TabIndex = 7;
            btnAppMin.UseVisualStyleBackColor = false;
            // 
            // sidebarPanel
            // 
            sidebarPanel.BackColor = Color.FromArgb(0, 123, 138);
            sidebarPanel.Controls.Add(panel2);
            sidebarPanel.Controls.Add(pictureUser);
            sidebarPanel.Controls.Add(reportContainer);
            sidebarPanel.Controls.Add(panelDashboard);
            sidebarPanel.Controls.Add(panelCars);
            sidebarPanel.Controls.Add(panelOrders);
            sidebarPanel.Controls.Add(panelCarParts);
            sidebarPanel.Controls.Add(panelCustomers);
            sidebarPanel.Location = new Point(0, 0);
            sidebarPanel.Name = "sidebarPanel";
            sidebarPanel.Size = new Size(300, 1033);
            sidebarPanel.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnLogout);
            panel2.Location = new Point(0, 971);
            panel2.Margin = new Padding(0);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(0, 30, 0, 0);
            panel2.Size = new Size(300, 60);
            panel2.TabIndex = 5;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.FromArgb(0, 151, 167);
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLogout.ForeColor = Color.White;
            btnLogout.Image = Properties.Resources.icons8_logout_64;
            btnLogout.ImageAlign = ContentAlignment.MiddleLeft;
            btnLogout.Location = new Point(-13, -78);
            btnLogout.Name = "btnLogout";
            btnLogout.Padding = new Padding(25, 0, 0, 0);
            btnLogout.Size = new Size(354, 216);
            btnLogout.TabIndex = 1;
            btnLogout.Text = "                LogOut";
            btnLogout.TextAlign = ContentAlignment.MiddleLeft;
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            // 
            // pictureUser
            // 
            pictureUser.BackgroundImage = Properties.Resources.icons8_male_user_64;
            pictureUser.BackgroundImageLayout = ImageLayout.Stretch;
            pictureUser.ErrorImage = null;
            pictureUser.Location = new Point(51, 38);
            pictureUser.Name = "pictureUser";
            pictureUser.Size = new Size(189, 186);
            pictureUser.TabIndex = 21;
            pictureUser.TabStop = false;
            // 
            // reportContainer
            // 
            reportContainer.BackColor = Color.FromArgb(55, 64, 93);
            reportContainer.Controls.Add(panel8);
            reportContainer.Controls.Add(panel9);
            reportContainer.Controls.Add(panel10);
            reportContainer.Location = new Point(2, 704);
            reportContainer.Margin = new Padding(0);
            reportContainer.Name = "reportContainer";
            reportContainer.Size = new Size(297, 80);
            reportContainer.TabIndex = 5;
            // 
            // panel8
            // 
            panel8.Controls.Add(btnReports);
            panel8.Location = new Point(0, 0);
            panel8.Margin = new Padding(0);
            panel8.Name = "panel8";
            panel8.Padding = new Padding(0, 30, 0, 0);
            panel8.Size = new Size(297, 80);
            panel8.TabIndex = 4;
            // 
            // btnReports
            // 
            btnReports.BackColor = Color.FromArgb(0, 151, 167);
            btnReports.Cursor = Cursors.Hand;
            btnReports.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnReports.ForeColor = Color.White;
            btnReports.Image = Properties.Resources.icons8_report_60;
            btnReports.ImageAlign = ContentAlignment.MiddleLeft;
            btnReports.Location = new Point(-20, -18);
            btnReports.Name = "btnReports";
            btnReports.Padding = new Padding(25, 0, 0, 0);
            btnReports.Size = new Size(354, 121);
            btnReports.TabIndex = 3;
            btnReports.Text = "                Manage Reports";
            btnReports.TextAlign = ContentAlignment.MiddleLeft;
            btnReports.UseVisualStyleBackColor = false;
            btnReports.Click += btnReports_Click;
            // 
            // panel9
            // 
            panel9.BackColor = Color.FromArgb(55, 64, 93);
            panel9.Controls.Add(customerReportNav);
            panel9.Location = new Point(0, 80);
            panel9.Margin = new Padding(0);
            panel9.Name = "panel9";
            panel9.Padding = new Padding(0, 30, 0, 0);
            panel9.Size = new Size(297, 80);
            panel9.TabIndex = 4;
            // 
            // customerReportNav
            // 
            customerReportNav.BackColor = Color.FromArgb(55, 64, 93);
            customerReportNav.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            customerReportNav.ForeColor = Color.White;
            customerReportNav.ImageAlign = ContentAlignment.MiddleLeft;
            customerReportNav.Location = new Point(-5, -17);
            customerReportNav.Name = "customerReportNav";
            customerReportNav.Padding = new Padding(25, 0, 0, 0);
            customerReportNav.Size = new Size(354, 121);
            customerReportNav.TabIndex = 3;
            customerReportNav.Text = "         Customer Report";
            customerReportNav.TextAlign = ContentAlignment.MiddleLeft;
            customerReportNav.UseVisualStyleBackColor = false;
            // 
            // panel10
            // 
            panel10.BackColor = Color.FromArgb(55, 64, 93);
            panel10.Controls.Add(orderReportNav);
            panel10.Location = new Point(0, 160);
            panel10.Margin = new Padding(0);
            panel10.Name = "panel10";
            panel10.Padding = new Padding(0, 30, 0, 0);
            panel10.Size = new Size(297, 80);
            panel10.TabIndex = 4;
            // 
            // orderReportNav
            // 
            orderReportNav.BackColor = Color.FromArgb(55, 64, 93);
            orderReportNav.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            orderReportNav.ForeColor = Color.White;
            orderReportNav.ImageAlign = ContentAlignment.MiddleLeft;
            orderReportNav.Location = new Point(-5, -17);
            orderReportNav.Name = "orderReportNav";
            orderReportNav.Padding = new Padding(25, 0, 0, 0);
            orderReportNav.Size = new Size(354, 121);
            orderReportNav.TabIndex = 3;
            orderReportNav.Text = "         Order Reports";
            orderReportNav.TextAlign = ContentAlignment.MiddleLeft;
            orderReportNav.UseVisualStyleBackColor = false;
            // 
            // panelDashboard
            // 
            panelDashboard.Controls.Add(dashboardNav);
            panelDashboard.Location = new Point(0, 280);
            panelDashboard.Name = "panelDashboard";
            panelDashboard.Padding = new Padding(0, 30, 0, 0);
            panelDashboard.Size = new Size(297, 80);
            panelDashboard.TabIndex = 4;
            // 
            // dashboardNav
            // 
            dashboardNav.BackColor = Color.FromArgb(0, 151, 167);
            dashboardNav.Cursor = Cursors.Hand;
            dashboardNav.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dashboardNav.ForeColor = Color.White;
            dashboardNav.Image = Properties.Resources.icons8_dashboard_64__2_;
            dashboardNav.ImageAlign = ContentAlignment.MiddleLeft;
            dashboardNav.Location = new Point(-19, -21);
            dashboardNav.Name = "dashboardNav";
            dashboardNav.Padding = new Padding(25, 0, 0, 0);
            dashboardNav.Size = new Size(354, 121);
            dashboardNav.TabIndex = 3;
            dashboardNav.Text = "                Dashboard";
            dashboardNav.TextAlign = ContentAlignment.MiddleLeft;
            dashboardNav.UseVisualStyleBackColor = false;
            dashboardNav.Click += dashboardNav_Click;
            // 
            // panelCars
            // 
            panelCars.Controls.Add(btnCars);
            panelCars.Location = new Point(0, 364);
            panelCars.Name = "panelCars";
            panelCars.Padding = new Padding(0, 30, 0, 0);
            panelCars.Size = new Size(297, 80);
            panelCars.TabIndex = 4;
            // 
            // btnCars
            // 
            btnCars.BackColor = Color.FromArgb(0, 151, 167);
            btnCars.Cursor = Cursors.Hand;
            btnCars.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCars.ForeColor = Color.White;
            btnCars.Image = Properties.Resources.icons8_car_64;
            btnCars.ImageAlign = ContentAlignment.MiddleLeft;
            btnCars.Location = new Point(-13, -17);
            btnCars.Name = "btnCars";
            btnCars.Padding = new Padding(25, 0, 0, 0);
            btnCars.Size = new Size(354, 121);
            btnCars.TabIndex = 3;
            btnCars.Text = "               Manage Cars";
            btnCars.TextAlign = ContentAlignment.MiddleLeft;
            btnCars.UseVisualStyleBackColor = false;
            btnCars.Click += btnCars_Click;
            // 
            // panelOrders
            // 
            panelOrders.Controls.Add(btnOrders);
            panelOrders.Location = new Point(2, 619);
            panelOrders.Name = "panelOrders";
            panelOrders.Padding = new Padding(0, 30, 0, 0);
            panelOrders.Size = new Size(297, 80);
            panelOrders.TabIndex = 4;
            // 
            // btnOrders
            // 
            btnOrders.BackColor = Color.FromArgb(0, 151, 167);
            btnOrders.Cursor = Cursors.Hand;
            btnOrders.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnOrders.ForeColor = Color.White;
            btnOrders.Image = Properties.Resources.icons8_maximum_order_64;
            btnOrders.ImageAlign = ContentAlignment.MiddleLeft;
            btnOrders.Location = new Point(-18, -17);
            btnOrders.Name = "btnOrders";
            btnOrders.Padding = new Padding(25, 0, 0, 0);
            btnOrders.Size = new Size(354, 121);
            btnOrders.TabIndex = 3;
            btnOrders.Text = "               Manage Orders";
            btnOrders.TextAlign = ContentAlignment.MiddleLeft;
            btnOrders.UseVisualStyleBackColor = false;
            btnOrders.Click += btnOrders_Click;
            // 
            // panelCarParts
            // 
            panelCarParts.Controls.Add(btnCarParts);
            panelCarParts.Location = new Point(1, 448);
            panelCarParts.Name = "panelCarParts";
            panelCarParts.Padding = new Padding(0, 30, 0, 0);
            panelCarParts.Size = new Size(297, 80);
            panelCarParts.TabIndex = 4;
            // 
            // btnCarParts
            // 
            btnCarParts.BackColor = Color.FromArgb(0, 151, 167);
            btnCarParts.BackgroundImageLayout = ImageLayout.Center;
            btnCarParts.Cursor = Cursors.Hand;
            btnCarParts.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCarParts.ForeColor = Color.White;
            btnCarParts.Image = Properties.Resources.icons8_car_part_64;
            btnCarParts.ImageAlign = ContentAlignment.MiddleLeft;
            btnCarParts.Location = new Point(-9, -17);
            btnCarParts.Name = "btnCarParts";
            btnCarParts.Padding = new Padding(25, 0, 0, 0);
            btnCarParts.Size = new Size(354, 121);
            btnCarParts.TabIndex = 3;
            btnCarParts.Text = "              Manage Car Parts";
            btnCarParts.TextAlign = ContentAlignment.MiddleLeft;
            btnCarParts.UseVisualStyleBackColor = false;
            btnCarParts.Click += btnCarParts_Click;
            // 
            // panelCustomers
            // 
            panelCustomers.Controls.Add(btnCustomers);
            panelCustomers.Location = new Point(2, 533);
            panelCustomers.Name = "panelCustomers";
            panelCustomers.Padding = new Padding(0, 30, 0, 0);
            panelCustomers.Size = new Size(297, 80);
            panelCustomers.TabIndex = 4;
            // 
            // btnCustomers
            // 
            btnCustomers.BackColor = Color.FromArgb(0, 151, 167);
            btnCustomers.Cursor = Cursors.Hand;
            btnCustomers.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCustomers.ForeColor = Color.White;
            btnCustomers.Image = Properties.Resources.icons8_male_user_64;
            btnCustomers.ImageAlign = ContentAlignment.MiddleLeft;
            btnCustomers.Location = new Point(-16, -17);
            btnCustomers.Name = "btnCustomers";
            btnCustomers.Padding = new Padding(25, 0, 0, 0);
            btnCustomers.Size = new Size(354, 121);
            btnCustomers.TabIndex = 3;
            btnCustomers.Text = "               Manage Customers";
            btnCustomers.TextAlign = ContentAlignment.MiddleLeft;
            btnCustomers.UseVisualStyleBackColor = false;
            btnCustomers.Click += btnCustomers_Click;
            // 
            // panel3
            // 
            panel3.BackColor = Color.White;
            panel3.BackgroundImageLayout = ImageLayout.Center;
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(labelNav);
            panel3.Location = new Point(300, 63);
            panel3.Name = "panel3";
            panel3.Size = new Size(1575, 48);
            panel3.TabIndex = 2;
            // 
            // labelNav
            // 
            labelNav.AutoSize = true;
            labelNav.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelNav.Location = new Point(41, 11);
            labelNav.Name = "labelNav";
            labelNav.Size = new Size(176, 25);
            labelNav.TabIndex = 2;
            labelNav.Text = "Home / Dashboard";
            // 
            // sidebarTransition
            // 
            sidebarTransition.Interval = 10;
            // 
            // mainPanel
            // 
            mainPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            mainPanel.BackColor = Color.FromArgb(243, 244, 247);
            mainPanel.Location = new Point(300, 112);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(1574, 919);
            mainPanel.TabIndex = 4;
            // 
            // DashBoard
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1875, 1030);
            Controls.Add(sidebarPanel);
            Controls.Add(panel3);
            Controls.Add(panel1);
            Controls.Add(mainPanel);
            FormBorderStyle = FormBorderStyle.None;
            IsMdiContainer = true;
            Name = "DashBoard";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Dashboard";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            sidebarPanel.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureUser).EndInit();
            reportContainer.ResumeLayout(false);
            panel8.ResumeLayout(false);
            panel9.ResumeLayout(false);
            panel10.ResumeLayout(false);
            panelDashboard.ResumeLayout(false);
            panelCars.ResumeLayout(false);
            panelOrders.ResumeLayout(false);
            panelCarParts.ResumeLayout(false);
            panelCustomers.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel sidebarPanel;
        private Panel panel3;
        private Label labelHead;
        private System.Windows.Forms.Timer sidebarTransition;
        private Button btnCarParts;
        private Panel panelDashboard;
        private Panel panelCars;
        private Button btnCars;
        private Panel panelCarParts;
        private Panel panelCustomers;
        private Button btnCustomers;
        private Panel panelOrders;
        private Button btnOrders;
        private Panel panel8;
        private Button btnReports;
        private FlowLayoutPanel reportContainer;
        private Panel panel9;
        private Button customerReportNav;
        private Panel panel10;
        private Button orderReportNav;
        private System.Windows.Forms.Timer reportTransition;
        private Panel mainPanel;
        private Button btnAppClose;
        private Button btnAppMin;
        private Label labelNav;
        private Label lblWelcome;
        private Button dashboardNav;
        private PictureBox pictureUser;
        private Panel panel2;
        private Button btnClose;
        private Button btnMax;
        private Button btnLogout;
        private Button btnView;
    }
}