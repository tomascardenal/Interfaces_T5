#define FORCEINTERNATIONAL

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace T5_Ejercicio2
{
    public partial class Form1 : Form
    {
        //Valid chars for the keyboard
        private readonly string keyboardChars = "123456789*0#";
        //Name tag for the keyboard buttons
        private readonly string KB_NAME = "keyboardBtn";
        //Coordinates and sizes
        private readonly int KEY_SIDE = 50,
            TOP_MARGIN = 90,
            LEFT_MARGIN = 30,
            FORM_WIDTH = 210,
            FORM_HEIGHT = 375;
        //Saves which keys have already changed the background
        private List<char> backChanged;
        //The savedialog for saving the numbers
        private SaveFileDialog saveDialog;


        public Form1()
        {   
            //You can force internationalization by changing the directive
#if FORCEINTERNATIONAL
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-EN");
#else
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentCulture;
#endif

            InitializeComponent();

            //Setting the form size properly
            this.ClientSize = new Size(FORM_WIDTH, FORM_HEIGHT);
            this.Text = Properties.strings.MAIN_TITLE;

            //Dynamic button creator
            int xCnt = 0;
            int yCnt = 0;
            for (int i = 0; i < keyboardChars.Length; i++)
            {
                Button btn = new Button();
                btn.Name = KB_NAME;
                btn.Text = keyboardChars[i].ToString();
                btn.Font = new Font("Arial Black", 20);
                if (i % 3 == 0 && i != 0)
                {
                    xCnt = 0;
                    yCnt++;
                }
                else if (i != 0)
                {
                    xCnt++;
                }
                btn.BackColor = Button.DefaultBackColor;
                btn.Location = new Point(LEFT_MARGIN + (KEY_SIDE * xCnt), TOP_MARGIN + (KEY_SIDE * yCnt));
                btn.Size = new Size(KEY_SIDE, KEY_SIDE);
                btn.Click += new EventHandler(btnNumbersClick);
                btn.MouseEnter += new EventHandler(btnNumbersMouseEnter);
                btn.MouseLeave += new EventHandler(btnNumbersMouseLeave);
                btn.Enabled = true;
                Controls.Add(btn);
            }

            //Adding text for international language support
            fileMenuItem.Text = Properties.strings.MENU_FILE;
            saveNumberMenuItem.Text = Properties.strings.MENU_SAVE;
            resetMenuItem.Text = Properties.strings.MENU_RESET;
            closeMenuItem.Text = Properties.strings.MENU_CLOSE;
            aboutMenuItem.Text = Properties.strings.MENU_ABOUT;
            backChanged = new List<char>();

            //Savedialog
            saveDialog = new SaveFileDialog();
            saveDialog.Title = Properties.strings.SAVE_TITLE;
            saveDialog.InitialDirectory = Environment.GetEnvironmentVariable("homepath");
            saveDialog.Filter = Properties.strings.SAVE_FILTER;
            saveDialog.ValidateNames = true;
            saveDialog.OverwritePrompt = false;

         
        }

        /// <summary>
        /// Checks if the password on FormPass was introduced correctly
        /// </summary>
        private void passCheck()
        {
            FormPass fPass = new FormPass();
            DialogResult dres = fPass.ShowDialog();

            if (dres == DialogResult.Cancel)
            {
                this.Dispose();
            }
        }

        /// <summary>
        /// Prepares everything neccesary when the form loads
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            passCheck();
        }

        /// <summary>
        /// Resets the program, as when btnReset is clicked
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnReset.PerformClick();
        }

        /// <summary>
        /// Closes this program without confirmation
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// Opens a SaveDialog to save a number
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void grabarNúmeroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txbNumbers.Text))
            {
                saveDialog.ShowDialog();
                using (StreamWriter s = new StreamWriter(saveDialog.FileName,true))
                {
                    s.WriteLine(this.txbNumbers.Text);
                }
            }
            else
            {
                MessageBox.Show(
                    Properties.strings.ERR_NOSAVE_TXT,
                    Properties.strings.ERR_NOSAVE_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Hand
                    );
            }
        }

        /// <summary>
        /// Shows info about this program
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                Properties.strings.ABOUT_TXT,
                Properties.strings.ABOUT_TITLE,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
                );
        }

        /// <summary>
        /// Checks for user confirmation when closing the program
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(Properties.strings.CLOSE_CONFIRM_TXT, Properties.strings.CLOSE_CONFIRM_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Checks for key presses
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (keyboardChars.Contains(e.KeyChar))
            {
                txbNumbers.AppendText(e.KeyChar.ToString());
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txbNumbers.Clear();
            foreach (Control c in Controls)
            {
                if (c.Name == KB_NAME) 
                {
                    c.BackColor = Button.DefaultBackColor;
                }
            }
            backChanged.Clear();
        }

        public void btnNumbersClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            txbNumbers.AppendText(btn.Text);
            btn.BackColor = Color.MediumVioletRed;
            backChanged.Add(btn.Text[0]);
        }

        public void btnNumbersMouseEnter(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.BackColor = Color.DarkTurquoise;
        }

        public void btnNumbersMouseLeave(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (backChanged.Contains(btn.Text[0]))
            {
                btn.BackColor = Color.MediumVioletRed;
            }
            else
            {
                btn.BackColor = Button.DefaultBackColor;
            }
        }
    }
}
