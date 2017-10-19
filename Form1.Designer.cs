namespace ConverToTif
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtFrontal = new System.Windows.Forms.TextBox();
            this.txtDorso = new System.Windows.Forms.TextBox();
            this.btnFrontal = new System.Windows.Forms.Button();
            this.btnDorso = new System.Windows.Forms.Button();
            this.btnConvertir = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtFrontal
            // 
            this.txtFrontal.Location = new System.Drawing.Point(149, 31);
            this.txtFrontal.Name = "txtFrontal";
            this.txtFrontal.ReadOnly = true;
            this.txtFrontal.Size = new System.Drawing.Size(262, 20);
            this.txtFrontal.TabIndex = 3;
            // 
            // txtDorso
            // 
            this.txtDorso.Location = new System.Drawing.Point(149, 84);
            this.txtDorso.Name = "txtDorso";
            this.txtDorso.ReadOnly = true;
            this.txtDorso.Size = new System.Drawing.Size(262, 20);
            this.txtDorso.TabIndex = 4;
            // 
            // btnFrontal
            // 
            this.btnFrontal.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFrontal.Location = new System.Drawing.Point(10, 28);
            this.btnFrontal.Name = "btnFrontal";
            this.btnFrontal.Size = new System.Drawing.Size(112, 23);
            this.btnFrontal.TabIndex = 5;
            this.btnFrontal.Text = "Imagen Dorso";
            this.btnFrontal.UseVisualStyleBackColor = true;
            this.btnFrontal.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnDorso
            // 
            this.btnDorso.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDorso.Location = new System.Drawing.Point(10, 81);
            this.btnDorso.Name = "btnDorso";
            this.btnDorso.Size = new System.Drawing.Size(116, 23);
            this.btnDorso.TabIndex = 6;
            this.btnDorso.Text = "Imagen Frontal";
            this.btnDorso.UseVisualStyleBackColor = true;
            this.btnDorso.Click += new System.EventHandler(this.btnDorso_Click);
            // 
            // btnConvertir
            // 
            this.btnConvertir.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConvertir.Location = new System.Drawing.Point(149, 175);
            this.btnConvertir.Name = "btnConvertir";
            this.btnConvertir.Size = new System.Drawing.Size(194, 31);
            this.btnConvertir.TabIndex = 7;
            this.btnConvertir.Text = "Convertir";
            this.btnConvertir.UseVisualStyleBackColor = true;
            this.btnConvertir.Click += new System.EventHandler(this.btnConvertir_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(473, 245);
            this.Controls.Add(this.btnConvertir);
            this.Controls.Add(this.btnDorso);
            this.Controls.Add(this.btnFrontal);
            this.Controls.Add(this.txtDorso);
            this.Controls.Add(this.txtFrontal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Elegir Imagen";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtFrontal;
        private System.Windows.Forms.TextBox txtDorso;
        private System.Windows.Forms.Button btnFrontal;
        private System.Windows.Forms.Button btnDorso;
        private System.Windows.Forms.Button btnConvertir;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}

