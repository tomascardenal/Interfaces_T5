using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T5_Ejercicio1
{
    public partial class Form1 : Form
    {
        private Icon[] icons = {
            Properties.Resources.icon1,
            Properties.Resources.icon2,
            Properties.Resources.icon3 };
        int currentIconIndex = 0;
        int currentTitleIndex = Properties.Resources.MAIN_TITLE.Length;
        bool iconChange;

        int ballX = 0, ballY = 0, ballVelX, ballVelY, ballRadius = 20;

        public Form1()
        {
            InitializeComponent();
            updateInfo();
            lbIndex.Text = Properties.Resources.LBINDEX_MSG + Properties.Resources.LBINDEX_NONE;
            Random rand = new Random();
            ballVelX = rand.Next(1, 6);
            ballVelY = rand.Next(1, 6);
            groupBox1.BackColor = Color.FromArgb(23, Control.DefaultBackColor);
            groupBox1.SendToBack();
            timer1.Start();
            timerBall.Start();
            iconChange = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txbAdd.Text))
            {
                lboxA.Items.Add(txbAdd.Text);
                txbAdd.Clear();
                updateInfo();
            }
            else
            {
                MessageBox.Show(
                    Properties.Resources.ERR_NOTEXT_MSG,
                    Properties.Resources.ERR_NOTEXT_TITLE,
                    MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lboxA.SelectedIndices.Count > 0)
            {
                for (int i = lboxA.SelectedIndices.Count - 1; i >= 0; i--)
                {
                    lboxA.Items.RemoveAt(lboxA.SelectedIndices[i]);
                }

                //while (lboxA.SelectedIndices.Count > 0)
                //    lboxA.Items.RemoveAt(lboxA.SelectedIndices[0]);
                updateInfo();
            }
            else
            {
                MessageBox.Show(
                    Properties.Resources.ERR_NOSELECTION_MSG,
                    Properties.Resources.ERR_NOSELECTION_TITLE,
                    MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void btnPassToB_Click(object sender, EventArgs e)
        {
            btnPassActions(lboxA, lboxB);
        }

        private void btnPassToA_Click(object sender, EventArgs e)
        {
            btnPassActions(lboxB, lboxA);
        }

        private void btnPassActions(ListBox origin, ListBox destiny)
        {
            if (origin.SelectedIndices.Count > 0)
            {
                int currentIndex;
                for (int i = origin.SelectedIndices.Count - 1; i >= 0; i--)
                {
                    currentIndex = origin.SelectedIndices[i];
                    destiny.Items.Insert(0, origin.Items[currentIndex]);
                    origin.Items.RemoveAt(currentIndex);
                }
                updateInfo();
            }
            else
            {
                MessageBox.Show(
                    Properties.Resources.ERR_CANTMOVE_MSG,
                    Properties.Resources.ERR_CANTMOVE_TITLE,
                    MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void timerBall_Tick(object sender, EventArgs e)
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
            picboxBall.Location = new Point(ballX, ballY);

        }

        private void lboxA_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbIndex.Text = Properties.Resources.LBINDEX_MSG;
            if (lboxA.SelectedIndices.Count > 0)
            {
                for (int i = 0; i < lboxA.SelectedIndices.Count; i++)
                {
                    lbIndex.Text += string.Format("{0},", lboxA.SelectedIndices[i]);
                }
            }
            else
            {
                lbIndex.Text += Properties.Resources.LBINDEX_NONE;
            }
        }

        private void updateInfo()
        {
            lbTotal.Text = String.Format(Properties.Resources.LBTOTAL_MSG, lboxA.Items.Count);
            toolTip1.SetToolTip(lboxB, string.Format(Properties.Resources.TOOLTIP_LBOXB, lboxB.Items.Count));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (currentIconIndex >= icons.Length)
            {
                currentIconIndex = 0;
            }

            if (currentTitleIndex < 0)
            {
                currentTitleIndex = Properties.Resources.MAIN_TITLE.Length;
            }

            if (iconChange)
            {
                this.Icon = icons[currentIconIndex];
                currentIconIndex++;
            }

            iconChange = !iconChange;

            this.Text = Properties.Resources.MAIN_TITLE.Substring(currentTitleIndex);
            currentTitleIndex--;
        }
    }
}
