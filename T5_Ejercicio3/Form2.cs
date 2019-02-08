using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T5_Ejercicio3
{
    public partial class Form2 : Form
    {
        private Form1 parentForm;

        //Testeo de capacidad de mover la ventana sin bordes
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public Form2(Form1 parentForm)
        {
            InitializeComponent();
            picBox.AutoSize = true;
            this.parentForm = parentForm;
        }
        
        public void setImage(Bitmap bmp)
        {
            picBox.Image = bmp;
            picBox.Size = bmp.Size;
            this.Size = bmp.Size;
            this.Update();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.ClientSize = picBox.Size;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            parentForm.ResetInfo();
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            parentForm.NotifyKeyDown(e);
        }

        private void siguienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            parentForm.useLeftRightButtons(false);
        }

        private void anteriorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            parentForm.useLeftRightButtons(true);
        }

        private void cerrarImagenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void allComponents_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
