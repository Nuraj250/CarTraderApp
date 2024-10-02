namespace CarTraderApp
{
    partial class AddCarOrder
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
            flowLayoutPanelViewCars = new FlowLayoutPanel();
            txtSearch = new RichTextBox();
            SuspendLayout();
            // 
            // flowLayoutPanelViewCars
            // 
            flowLayoutPanelViewCars.Location = new Point(4, 66);
            flowLayoutPanelViewCars.Margin = new Padding(0);
            flowLayoutPanelViewCars.Name = "flowLayoutPanelViewCars";
            flowLayoutPanelViewCars.Size = new Size(1873, 750);
            flowLayoutPanelViewCars.TabIndex = 0;
            // 
            // txtSearch
            // 
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtSearch.Location = new Point(8, 6);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(390, 50);
            txtSearch.TabIndex = 0;
            txtSearch.Text = "";
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // AddCarOrder
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Center;
            Controls.Add(txtSearch);
            Controls.Add(flowLayoutPanelViewCars);
            Name = "AddCarOrder";
            Size = new Size(1873, 814);
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanelViewCars;
        private RichTextBox txtSearch;
    }
}