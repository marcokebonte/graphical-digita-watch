using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphical_Digital_Clock
{
    public partial class Form1 : Form
    {
        // Path to the directory containing digit images
        string path = @"Digits\";

        // Timer to update the clock display
        Timer t = new Timer();

        // Form1 constructor
        public Form1()
        {
            InitializeComponent();
        }

        // Form1 load event handler
        private void Form1_Load(object sender, EventArgs e)
        {
            // Set timer interval to 1000 milliseconds (1 second)
            t.Interval = 1000;

            // Attach event handler to timer's Tick event
            t.Tick += new EventHandler(t_tick);

            // Start the timer
            t.Start();
        }

        // Event handler for timer's Tick event
        private void t_tick(object sender, EventArgs e)
        {
            // Get current time
            int h, m, s;
            h = DateTime.Now.Hour;
            m = DateTime.Now.Minute;
            s = DateTime.Now.Second;

            // Extract digits of current time
            int[] time = { h / 10, h % 10, m / 10, m % 10, s / 10, s % 10 };

            // Create a new bitmap for the clock display
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            // Create a Graphics object from the bitmap
            Graphics graphic = Graphics.FromImage(bmp);

            // Loop through each digit of the time array
            for (int i = 0; i < time.Length; i++)
            {
                // Calculate horizontal position of digit based on index
                int extra = 32 * i + i / 2 * 20;

                // Load the image for the current digit from the Digits directory
                Image img = Image.FromFile(path + time[i] + ".png");

                // Draw the digit image on the bitmap
                graphic.DrawImage(img, 10 + extra, 10, 32, 46);
            }

            // Create a Font object for the colon separator
            Font font = new Font("Arial", 30);

            // Create a SolidBrush object for the colon separator
            SolidBrush brush = new SolidBrush(Color.Black);

            // Create a PointF object for the position of the colon separator
            PointF point = new PointF(68.5f, 10);

            // Draw the first colon separator on the bitmap
            graphic.DrawString(":", font, brush, point);

            // Move the position of the colon separator to the right
            point.X = 154.5f;

            // Draw the second colon separator on the bitmap
            graphic.DrawString(":", font, brush, point);

            // Set the picture box's Image property to the new bitmap
            pictureBox1.Image = bmp;

            // Dispose of the Graphics object to release system resources
            graphic.Dispose();
        }

        
    }
}
