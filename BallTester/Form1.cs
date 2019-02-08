using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BallTester
{
    public partial class Form1 : Form
    {
        int ballX = 0, ballY = 0, ballVelX, ballVelY, ballRadius = 45;

        public Form1()
        {
            InitializeComponent();
            Random rand = new Random();
            ballVelX = rand.Next(1, 4);
            ballVelY = rand.Next(1, 4);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ballX += ballVelX;
            ballY += ballVelY;
            if (ballX + ballRadius >= this.ClientSize.Width || ballX <= 0)
            {
                ballVelX = -ballVelX;
            }

            if (ballY + ballRadius >= this.ClientSize.Height || ballY <= 0)
            {
                ballVelY = -ballVelY;
            }
            pictureBox1.Location = new Point(ballX, ballY);
        }
    }
}
