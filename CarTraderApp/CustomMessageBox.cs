using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarTraderApp
{
    public partial class CustomMessageBox : Form
    {
        public CustomMessageBox(string title, string message)
        {
            InitializeComponent();
            lblTitle.Text = title;
            lblMessage.Text = message;

            }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // Center the form on the top center of the screen
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, 0);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btncancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public void HideOkButton()
        {
            btncancle.Visible = false;  // Hide the Cancel button
            btnSave.Text = "OK";
        }
        private void SetDynamicLabelText(Label label, string text)
        {
            // Set AutoSize to false to control the label's size dynamically
            label.AutoSize = false;

            // Measure the string size to determine the needed width and height
            using (Graphics g = label.CreateGraphics())
            {
                SizeF size = g.MeasureString(text, label.Font, label.Width);

                // Check if the text is too long to fit within the label's width
                if (size.Width > label.Width)
                {
                    // Split the text into two columns if it's too wide
                    string[] words = text.Split(' ');
                    StringBuilder leftColumn = new StringBuilder();
                    StringBuilder rightColumn = new StringBuilder();

                    float currentWidth = 0;
                    foreach (string word in words)
                    {
                        SizeF wordSize = g.MeasureString(word + " ", label.Font);
                        if (currentWidth + wordSize.Width > label.Width / 2)
                        {
                            rightColumn.Append(word + " ");
                        }
                        else
                        {
                            leftColumn.Append(word + " ");
                            currentWidth += wordSize.Width;
                        }
                    }

                    label.Text = leftColumn.ToString() + Environment.NewLine + rightColumn.ToString();
                }
                else
                {
                    label.Text = text;
                }

                // Adjust label height to fit the text
                label.Height = (int)Math.Ceiling(size.Height);
            }
        }

    }
}
