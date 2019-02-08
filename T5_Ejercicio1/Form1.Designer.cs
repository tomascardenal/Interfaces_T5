namespace T5_Ejercicio1
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lboxA = new System.Windows.Forms.ListBox();
            this.lboxB = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnPassToB = new System.Windows.Forms.Button();
            this.btnPassToA = new System.Windows.Forms.Button();
            this.txbAdd = new System.Windows.Forms.TextBox();
            this.lbTotal = new System.Windows.Forms.Label();
            this.lbIndex = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.picboxBall = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timerBall = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picboxBall)).BeginInit();
            this.SuspendLayout();
            // 
            // lboxA
            // 
            this.lboxA.FormattingEnabled = true;
            this.lboxA.Location = new System.Drawing.Point(44, 27);
            this.lboxA.Name = "lboxA";
            this.lboxA.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lboxA.Size = new System.Drawing.Size(120, 95);
            this.lboxA.TabIndex = 0;
            this.toolTip1.SetToolTip(this.lboxA, "Lista principal");
            this.lboxA.SelectedIndexChanged += new System.EventHandler(this.lboxA_SelectedIndexChanged);
            // 
            // lboxB
            // 
            this.lboxB.FormattingEnabled = true;
            this.lboxB.Location = new System.Drawing.Point(269, 27);
            this.lboxB.Name = "lboxB";
            this.lboxB.Size = new System.Drawing.Size(120, 95);
            this.lboxB.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(44, 139);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Añadir";
            this.toolTip1.SetToolTip(this.btnAdd, "Añade el texto a la lista principal");
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnRemove.Location = new System.Drawing.Point(314, 137);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "Quitar";
            this.toolTip1.SetToolTip(this.btnRemove, "Elimina la selección de la lista principal");
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnPassToB
            // 
            this.btnPassToB.Location = new System.Drawing.Point(198, 38);
            this.btnPassToB.Name = "btnPassToB";
            this.btnPassToB.Size = new System.Drawing.Size(37, 23);
            this.btnPassToB.TabIndex = 4;
            this.btnPassToB.Text = ">>";
            this.toolTip1.SetToolTip(this.btnPassToB, "Mover selección a la lista secundaria");
            this.btnPassToB.UseVisualStyleBackColor = true;
            this.btnPassToB.Click += new System.EventHandler(this.btnPassToB_Click);
            // 
            // btnPassToA
            // 
            this.btnPassToA.Location = new System.Drawing.Point(198, 82);
            this.btnPassToA.Name = "btnPassToA";
            this.btnPassToA.Size = new System.Drawing.Size(37, 23);
            this.btnPassToA.TabIndex = 5;
            this.btnPassToA.Text = "<<";
            this.toolTip1.SetToolTip(this.btnPassToA, "Mover selección a la lista principal");
            this.btnPassToA.UseVisualStyleBackColor = true;
            this.btnPassToA.Click += new System.EventHandler(this.btnPassToA_Click);
            // 
            // txbAdd
            // 
            this.txbAdd.Location = new System.Drawing.Point(139, 139);
            this.txbAdd.Name = "txbAdd";
            this.txbAdd.Size = new System.Drawing.Size(154, 20);
            this.txbAdd.TabIndex = 6;
            this.toolTip1.SetToolTip(this.txbAdd, "Escribe aquí lo que quieras añadir a la lista principal");
            // 
            // lbTotal
            // 
            this.lbTotal.AutoSize = true;
            this.lbTotal.Location = new System.Drawing.Point(29, 47);
            this.lbTotal.Name = "lbTotal";
            this.lbTotal.Size = new System.Drawing.Size(140, 13);
            this.lbTotal.TabIndex = 7;
            this.lbTotal.Text = "Elementos en lista principal: ";
            // 
            // lbIndex
            // 
            this.lbIndex.AutoSize = true;
            this.lbIndex.Location = new System.Drawing.Point(29, 21);
            this.lbIndex.Name = "lbIndex";
            this.lbIndex.Size = new System.Drawing.Size(115, 13);
            this.lbIndex.TabIndex = 8;
            this.lbIndex.Text = "Índices seleccionados:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.lbIndex);
            this.groupBox1.Controls.Add(this.lbTotal);
            this.groupBox1.Location = new System.Drawing.Point(44, 178);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(345, 82);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Info";
            this.toolTip1.SetToolTip(this.groupBox1, "Información sobre la lista principal");
            // 
            // picboxBall
            // 
            this.picboxBall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picboxBall.Image = global::T5_Ejercicio1.Properties.Resources.ball;
            this.picboxBall.Location = new System.Drawing.Point(0, 0);
            this.picboxBall.Name = "picboxBall";
            this.picboxBall.Size = new System.Drawing.Size(20, 20);
            this.picboxBall.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picboxBall.TabIndex = 10;
            this.picboxBall.TabStop = false;
            this.toolTip1.SetToolTip(this.picboxBall, "Yo solo soy una bola de dragón");
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timerBall
            // 
            this.timerBall.Interval = 1;
            this.timerBall.Tick += new System.EventHandler(this.timerBall_Tick);
            // 
            // Form1
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnRemove;
            this.ClientSize = new System.Drawing.Size(433, 272);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnPassToB);
            this.Controls.Add(this.txbAdd);
            this.Controls.Add(this.btnPassToA);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lboxB);
            this.Controls.Add(this.lboxA);
            this.Controls.Add(this.picboxBall);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picboxBall)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lboxA;
        private System.Windows.Forms.ListBox lboxB;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnPassToB;
        private System.Windows.Forms.Button btnPassToA;
        private System.Windows.Forms.TextBox txbAdd;
        private System.Windows.Forms.Label lbTotal;
        private System.Windows.Forms.Label lbIndex;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timerBall;
        private System.Windows.Forms.PictureBox picboxBall;
    }
}

