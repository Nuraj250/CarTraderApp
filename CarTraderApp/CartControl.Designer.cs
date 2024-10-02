namespace CarTraderApp
{
    partial class CartControl
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
            flowLayoutPanelCartItems = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // flowLayoutPanelCartItems
            // 
            flowLayoutPanelCartItems.Location = new Point(0, 0);
            flowLayoutPanelCartItems.Name = "flowLayoutPanelCartItems";
            flowLayoutPanelCartItems.Size = new Size(1873, 814);
            flowLayoutPanelCartItems.TabIndex = 0;
            // 
            // CartControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1855, 767);
            Controls.Add(flowLayoutPanelCartItems);
            Name = "CartControl";
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanelCartItems;
    }
}
