using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T5_Ejercicio2
{
    public partial class FormPass : Form
    {
        private int triesLeft;
        private readonly int PIN = 1234;

        public FormPass()
        {
            InitializeComponent();
            triesLeft = 3;
            lbTries.Text = string.Format(Properties.strings.TRIESLEFT_TXT, triesLeft);
            lbPass.Text = Properties.strings.PASS_TEXT;
            this.Text = Properties.strings.PASS_TITLE;
        }

        private void txbPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar))
            {
                txbPass.Text = txbPass.Text.Substring(0, txbPass.Text.Length - 1);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txbPass.Text == PIN.ToString())
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                if (triesLeft > 1)
                {
                    triesLeft--;
                    lbTries.Text = string.Format(Properties.strings.TRIESLEFT_TXT, triesLeft);
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                }
            }
        }
    }
}
