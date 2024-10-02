using CarTraderApp.Properties;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CarTraderApp
{
    public partial class CustomerHomePage : UserControl
    {
        public CustomerHomePage()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            Color tealColor = Color.FromArgb(0, 151, 167);
            // Hero Panel with Gradient Background
            Panel heroPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = this.Height,
                BackColor = Color.Transparent // Set to transparent to use background image
            };
            this.Controls.Add(heroPanel);

            // Set a Gradient Background
            heroPanel.Paint += (s, e) =>
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(heroPanel.ClientRectangle,
                     Color.White, tealColor, 45F))
                {
                    e.Graphics.FillRectangle(brush, heroPanel.ClientRectangle);
                }
            };

            // PictureBox for Car Image
            PictureBox carPictureBox = new PictureBox
            {
                Image = Properties.Resources.iStock_Car_Liquid_cosmin4000,
                SizeMode = PictureBoxSizeMode.CenterImage,
                Size = new Size(750, 750),
                Location = new Point(20, 50)
            };
            heroPanel.Controls.Add(carPictureBox);

            // Main Hero Text
            Label heroMainText = new Label
            {
                Text = "Discover Your Dream Ride and Keep It Running Smoothly!",
                ForeColor = Color.Black,
                Font = new Font("Montserrat", 18, FontStyle.Bold),
                Location = new Point(850, 240),
                AutoSize = true
            };
            heroPanel.Controls.Add(heroMainText);

            // Subheading Hero Text
            Label heroSubText = new Label
            {
                Text = "Explore our vast collection of cars and high-quality parts.\n" +
                       "Whether you're looking to buy a new vehicle or maintain your current one,\n" +
                       "we offer unbeatable deals and expert support to ensure your journey is always smooth.",
                ForeColor = Color.Black,
                Font = new Font("Montserrat", 12),
                Location = new Point(850, 310),
                AutoSize = true
            };
            heroPanel.Controls.Add(heroSubText);
        }
    }
}
