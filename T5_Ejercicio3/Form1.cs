using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T5_Ejercicio3
{
    public partial class Form1 : Form
    {
        //OpenDialog que gestiona la apertura de formularios
        private OpenFileDialog openDialog;

        //Botones de izquierda, derecha
        private Button btnLeft, btnRight;

        //Índice de la imagen a mostarar
        private int imageIndex;

        //El formulario secundario (dónde se muestra la imagen)
        private Form2 formSec;

        //Lista de imágenes seguras (Que se pueden abrir) en el directorio
        private List<string> safeImagesOnDir;

        //Bitmap que se muestra en el formulario segundario
        private Bitmap currentBmp;

        //Unidades válidas
        private readonly string[] units = { "bytes", "kb", "mb", "gb", "tb", "WTF" };

        //Extensiones válidas
        private readonly string[] extensions = { ".jpg", ".jpeg", ".jpe", ".jfif", ".png", ".bmp" };

        /// <summary>
        /// Constructor para el formulario principal
        /// Inicializa las funcionalidades principales del programa
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            //openDialog
            openDialog = new OpenFileDialog();
            openDialog.Title = Properties.Resources.OPENDIALOG_TITLE;
            openDialog.InitialDirectory = Environment.GetEnvironmentVariable("homepath");
            openDialog.Filter = Properties.Resources.OPENDIALOG_FILTERS;
            openDialog.ValidateNames = true;

            //btnLeft
            btnLeft = new Button();
            btnLeft.Text = "<-";
            btnLeft.Location = new Point(btnOpen.Left - 50, btnOpen.Top + 10);
            btnLeft.Size = new Size(30, 30);
            btnLeft.Click += new EventHandler(btnLeft_Click);
            toolTip1.SetToolTip(btnLeft, Properties.Resources.TOOLTIP_LEFT);
            btnLeft.Hide();
            Controls.Add(btnLeft);

            //btnRight
            btnRight = new Button();
            btnRight.Text = "->";
            btnRight.Location = new Point(btnOpen.Right + 20, btnOpen.Top + 10);
            btnRight.Size = new Size(30, 30);
            btnRight.Click += new EventHandler(btnRight_Click);
            toolTip1.SetToolTip(btnRight, Properties.Resources.TOOLTIP_RIGHT);
            btnRight.Hide();
            Controls.Add(btnRight);

            safeImagesOnDir = new List<string>();
        }

        /// <summary>
        /// Lanza el click de los botones izquierda o derecha, para realizar la misma acción que estos
        /// </summary>
        /// <param name="isLeft">true si se quiere pulsar el botón izquierdo, false para lo contrario</param>
        public void useLeftRightButtons(bool isLeft)
        {
            if (isLeft)
            {
                btnLeft.PerformClick();
            }
            else
            {
                btnRight.PerformClick();
            }
        }

        /// <summary>
        /// Procesa las teclas de comandos, en este caso izquierda y derecha
        /// </summary>
        /// <param name="msg">El mensaje del evento</param>
        /// <param name="keyData">Los datos de la tecla de comando pulsada</param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Right)
            {
                btnRight.PerformClick();
            }
            else if (keyData == Keys.Left)
            {
                btnLeft.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Gestiona los clicks del botón de apertura de imágen
        /// </summary>
        /// <param name="sender">El botón que manda el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            DialogResult dr = openDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                loadDirectoryImages();
            }
        }

        /// <summary>
        /// Carga las imágenes del directorio actual y comprueba que sean convertibles a Bitmap
        /// </summary>
        private void loadDirectoryImages()
        {
            //Si openDialog tiene un FileName válido guardado
            if (!string.IsNullOrWhiteSpace(openDialog.FileName))
            {
                //Nos posicionamos en el directorio de la imagen
                Environment.CurrentDirectory = Path.GetDirectoryName(openDialog.FileName);
                //Recogemos todos los nombres de imágen en el directorio
                string[] imagesOnDirectory = Directory.GetFiles(Environment.CurrentDirectory, "*.*"
                    ).Where
                    (
                        file => extensions.Any(ext => file.EndsWith(ext, StringComparison.OrdinalIgnoreCase))//LINQ es una maravilla
                    ).ToArray();

                //Vaciamos la lista de imágenes seguras
                safeImagesOnDir.Clear();

                //Si el total de imágenes no es nulo
                if (imagesOnDirectory != null)
                {
                    //Si hay mas de una imagen, las recorremos
                    if (imagesOnDirectory.Length > 0)
                    {
                        for (int i = 0; i < imagesOnDirectory.Length; i++)
                        {
                            try
                            {
                                //Si no es una imágen válida, saltará la excepción no añadiendola a la colección
                                new Bitmap(imagesOnDirectory[i]);
                                safeImagesOnDir.Add(imagesOnDirectory[i]);
                                GC.Collect();
                            }
                            catch (ArgumentException)//Igual no es el método más optimo para comprobar que el fichero sea válido como un bitmap
                            {
                            }
                        }
                    }
                    try
                    {
                        if (safeImagesOnDir.Contains(openDialog.FileName))//Si el fichero escogido se encuentra entre los detectados como bmp válidos
                        {
                            imageIndex = safeImagesOnDir.IndexOf(openDialog.FileName);
                        }
                        else
                        {
                            throw new ArgumentException();//Si no está, damos por hecho que ha fallado
                        }
                        showBitmap(safeImagesOnDir[imageIndex]);//Mostramos el bmp
                        updateInfo();//Actualizamos la información
                        arrowBtnVisible(true);//Mostramos los botones de cambio de imágen
                    }
                    catch (ArgumentException)
                    {
                        //Lanzamos error
                        MessageBox.Show(Properties.Resources.ERR_BADFILE_TXT, Properties.Resources.ERR_BADFILE_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //Y probamos de nuevo a escoger una imágen válida
                        btnOpen.PerformClick();
                    }
                }
            }
        }

        /// <summary>
        /// Gestiona los clicks del botón de cambio de imagen (a la izquierda)
        /// </summary>
        /// <param name="sender">El componente que manda el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void btnLeft_Click(object sender, EventArgs e)
        {
            arrowBtnActions(true);
        }

        /// <summary>
        /// Gestiona los clicks del botón de cambio de imagen (a la derecha)
        /// </summary>
        /// <param name="sender">El componente que manda el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void btnRight_Click(object sender, EventArgs e)
        {
            arrowBtnActions(false);
        }

        /// <summary>
        /// Gestiona y controla en común las acciones de los botones de cambio de imagen, mostrando los mensajes correspondientes
        /// </summary>
        /// <param name="leftBtn">Indica si se ha pulsado el botón de cambio hacia la izquierda</param>
        private void arrowBtnActions(bool leftBtn)
        {
            bool exception = false;
            string exMessage = "";
            if (safeImagesOnDir != null && safeImagesOnDir.Count > 1)
            {
                if (leftBtn)
                {
                    if (imageIndex > 0)
                    {
                        imageIndex--;
                    }
                    else
                    {
                        imageIndex = safeImagesOnDir.Count - 1;
                    }
                }
                else
                {
                    if (imageIndex < safeImagesOnDir.Count - 1)
                    {
                        imageIndex++;
                    }
                    else
                    {
                        imageIndex = 0;
                    }

                }
                try
                {
                    showBitmap(safeImagesOnDir[imageIndex]);
                    updateInfo();
                }
                catch (ArgumentException ex)
                {
                    exception = true;
                    exMessage = ex.Message;
                }
                catch (FileNotFoundException ex)
                {
                    exception = true;
                    exMessage = ex.Message;
                }
                if (exception)
                {
                    MessageBox.Show(string.Format(Properties.Resources.ERR_OPENING_TXT, exMessage), Properties.Resources.ERR_OPENING_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (leftBtn)
                    {
                        imageIndex++;
                    }
                    else
                    {
                        imageIndex--;
                    }
                    loadDirectoryImages();
                }
            }
        }

        /// <summary>
        /// Lanza el formulario secundario con el BMP a mostrar correspondiente
        /// </summary>
        /// <param name="path">La ruta a la imagen a mostrar como bitmap</param>
        private void showBitmap(string path)
        {
            Bitmap bmp = new Bitmap(path);
            if (formSec == null)
            {
                formSec = new Form2(this);
            }
            formSec.setImage(bmp);
            if (!formSec.Visible)
            {
                formSec.Show();
            }
            currentBmp = bmp;
        }

        /// <summary>
        /// Cambia la visibilidad de los botones de cambio de imagen
        /// </summary>
        /// <param name="visible">Indica si son o no visibles</param>
        private void arrowBtnVisible(bool visible)
        {
            if (visible)
            {
                btnLeft.Show();
                btnRight.Show();
            }
            else
            {
                btnLeft.Hide();
                btnRight.Hide();
            }
        }

        /// <summary>
        /// Controla las pulsaciones de teclas sobre el formulario (para funcionalidad conjunta con los botones de cambio de imagen)
        /// </summary>
        /// <param name="sender">El componente que manda el evento (una tecla)</param>
        /// <param name="e">Los argumentos del evento</param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                btnRight.PerformClick();
            }
            else if (e.KeyCode == Keys.Left)
            {
                btnLeft.PerformClick();
            }
        }

        /// <summary>
        /// Actualiza la información mostrada sobre la imágen
        /// </summary>
        private void updateInfo()
        {
            string currentImg = safeImagesOnDir[imageIndex];
            if (!string.IsNullOrWhiteSpace(currentImg))
            {
                FileInfo fInfo = new FileInfo(currentImg);
                string unit;
                int unitCount = 0;
                long fileLength = fInfo.Length;
                while (fileLength >= 1024)
                {
                    fileLength = fileLength / 1024;
                    unitCount++;
                }
                unit = units[unitCount];

                lbInfo.Text = string.Format(Properties.Resources.LB_INFO,
                    currentImg.Substring(currentImg.LastIndexOf(Path.DirectorySeparatorChar) + 1), fileLength, unit, currentBmp.Width, currentBmp.Height);

                string imgPath = safeImagesOnDir[imageIndex];
                this.Text = string.Format(Properties.Resources.TITLE, imgPath.Substring(imgPath.LastIndexOf(Path.DirectorySeparatorChar) + 1));
                lbDir.Text = Directory.GetCurrentDirectory();
            }
        }

        public void ResetInfo()
        {
            this.Text = Properties.Resources.TITLE_EMPTY;
            lbDir.Text = "";
            lbInfo.Text = "";
            btnLeft.Hide();
            btnRight.Hide();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(Properties.Resources.CLOSE_CONFIRM_TXT, Properties.Resources.CLOSE_CONFIRM_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        public void NotifyKeyDown(KeyEventArgs e)
        {
            OnKeyDown(e);
        }
    }
}
