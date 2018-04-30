namespace WinForms.LectorHuella
{
    partial class Buscar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxAlta = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxId = new System.Windows.Forms.TextBox();
            this.lbFechaAlta = new System.Windows.Forms.Label();
            this.lbNombre = new System.Windows.Forms.Label();
            this.lbID = new System.Windows.Forms.Label();
            this.pictureBoxBuscar = new System.Windows.Forms.PictureBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBuscar)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxAlta);
            this.groupBox1.Controls.Add(this.textBoxName);
            this.groupBox1.Controls.Add(this.textBoxId);
            this.groupBox1.Controls.Add(this.lbFechaAlta);
            this.groupBox1.Controls.Add(this.lbNombre);
            this.groupBox1.Controls.Add(this.lbID);
            this.groupBox1.Controls.Add(this.pictureBoxBuscar);
            this.groupBox1.Location = new System.Drawing.Point(23, 89);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(305, 353);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Buscar Huella";
            // 
            // textBoxAlta
            // 
            this.textBoxAlta.Location = new System.Drawing.Point(97, 315);
            this.textBoxAlta.Name = "textBoxAlta";
            this.textBoxAlta.Size = new System.Drawing.Size(185, 20);
            this.textBoxAlta.TabIndex = 6;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(97, 286);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(185, 20);
            this.textBoxName.TabIndex = 5;
            // 
            // textBoxId
            // 
            this.textBoxId.Location = new System.Drawing.Point(97, 258);
            this.textBoxId.Name = "textBoxId";
            this.textBoxId.Size = new System.Drawing.Size(185, 20);
            this.textBoxId.TabIndex = 4;
            // 
            // lbFechaAlta
            // 
            this.lbFechaAlta.AutoSize = true;
            this.lbFechaAlta.Location = new System.Drawing.Point(23, 323);
            this.lbFechaAlta.Name = "lbFechaAlta";
            this.lbFechaAlta.Size = new System.Drawing.Size(58, 13);
            this.lbFechaAlta.TabIndex = 3;
            this.lbFechaAlta.Text = "FechaAlta:";
            // 
            // lbNombre
            // 
            this.lbNombre.AutoSize = true;
            this.lbNombre.Location = new System.Drawing.Point(23, 292);
            this.lbNombre.Name = "lbNombre";
            this.lbNombre.Size = new System.Drawing.Size(47, 13);
            this.lbNombre.TabIndex = 2;
            this.lbNombre.Text = "Nombre:";
            // 
            // lbID
            // 
            this.lbID.AutoSize = true;
            this.lbID.Location = new System.Drawing.Point(23, 260);
            this.lbID.Name = "lbID";
            this.lbID.Size = new System.Drawing.Size(24, 13);
            this.lbID.TabIndex = 1;
            this.lbID.Text = "ID: ";
            // 
            // pictureBoxBuscar
            // 
            this.pictureBoxBuscar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxBuscar.Location = new System.Drawing.Point(23, 34);
            this.pictureBoxBuscar.Name = "pictureBoxBuscar";
            this.pictureBoxBuscar.Size = new System.Drawing.Size(259, 197);
            this.pictureBoxBuscar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxBuscar.TabIndex = 0;
            this.pictureBoxBuscar.TabStop = false;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(23, 468);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 1;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(253, 468);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(75, 23);
            this.btnCerrar.TabIndex = 2;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // Buscar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 527);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.groupBox1);
            this.Name = "Buscar";
            this.Text = "Buscar";
            //this.Activated += new System.EventHandler(this.Buscar_Activated);
            //this.Deactivate += new System.EventHandler(this.Buscar_Deactivate);
            //this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Buscar_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Buscar_FormClosed);
            this.Load += new System.EventHandler(this.Buscar_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBuscar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBoxBuscar;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.TextBox textBoxAlta;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxId;
        private System.Windows.Forms.Label lbFechaAlta;
        private System.Windows.Forms.Label lbNombre;
        private System.Windows.Forms.Label lbID;
    }
}