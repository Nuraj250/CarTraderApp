namespace CarTraderApp
{
    partial class ManageOrdersControl
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
            dataGridViewOrders = new DataGridView();
            txtSearchOrders = new RichTextBox();
            pictureSearch = new PictureBox();
            label2 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewOrders).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureSearch).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(0, 151, 167);
            panel1.Controls.Add(dataGridViewOrders);
            panel1.Controls.Add(txtSearchOrders);
            panel1.Controls.Add(pictureSearch);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(36, 34);
            panel1.Margin = new Padding(5);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(3);
            panel1.Size = new Size(1502, 854);
            panel1.TabIndex = 21;
            // 
            // dataGridViewOrders
            // 
            dataGridViewOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewOrders.Location = new Point(66, 165);
            dataGridViewOrders.Name = "dataGridViewOrders";
            dataGridViewOrders.RowHeadersWidth = 51;
            dataGridViewOrders.Size = new Size(1368, 645);
            dataGridViewOrders.TabIndex = 17;
            // 
            // txtSearchOrders
            // 
            txtSearchOrders.Location = new Point(51, 89);
            txtSearchOrders.Name = "txtSearchOrders";
            txtSearchOrders.Size = new Size(299, 47);
            txtSearchOrders.TabIndex = 20;
            txtSearchOrders.Text = "";
            // 
            // pictureSearch
            // 
            pictureSearch.BackgroundImage = Properties.Resources.icons8_search_32;
            pictureSearch.BackgroundImageLayout = ImageLayout.Center;
            pictureSearch.Location = new Point(370, 89);
            pictureSearch.Name = "pictureSearch";
            pictureSearch.Size = new Size(48, 46);
            pictureSearch.TabIndex = 20;
            pictureSearch.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(54, 36);
            label2.Name = "label2";
            label2.Size = new Size(214, 37);
            label2.TabIndex = 18;
            label2.Text = "Manage Orders";
            // 
            // ManageOrdersControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            Controls.Add(panel1);
            Name = "ManageOrdersControl";
            Size = new Size(1574, 919);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewOrders).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureSearch).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btnUpload;
        private PictureBox picPartImage;
        private Label lblBehaviour;
        private Button btnCancle;
        private Button btnSave;
        private RichTextBox txtDescription;
        private RichTextBox txtPrice;
        private RichTextBox txtManufacturer;
        private RichTextBox txtPartName;
        private DataGridView dataGridViewOrders;
        private RichTextBox txtSearchOrders;
        private PictureBox pictureSearch;
        private Label label2;
    }
}
