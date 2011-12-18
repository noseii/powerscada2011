namespace PowerScada
{
    partial class SimpleLookup
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
            this.grid = new System.Windows.Forms.DataGridView();
            this.Altpanel = new System.Windows.Forms.Panel();
            this.Ustpanel = new System.Windows.Forms.Panel();
            this.superTextBoxAdi = new PowerScada.SuperTextBox();
            this.superTextBoxSoyadi = new PowerScada.SuperTextBox();
            this.superTextBoxTckimlikno = new PowerScada.SuperTextBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlTckimlikNo = new DevExpress.XtraEditors.LabelControl();
            this.labelControlAdi = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.Altpanel.SuspendLayout();
            this.Ustpanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 0);
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.Size = new System.Drawing.Size(550, 299);
            this.grid.TabIndex = 0;
            this.grid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.grid_MouseDoubleClick);
            // 
            // Altpanel
            // 
            this.Altpanel.Controls.Add(this.grid);
            this.Altpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Altpanel.Location = new System.Drawing.Point(0, 54);
            this.Altpanel.Name = "Altpanel";
            this.Altpanel.Size = new System.Drawing.Size(550, 299);
            this.Altpanel.TabIndex = 1;
            // 
            // Ustpanel
            // 
            this.Ustpanel.Controls.Add(this.superTextBoxAdi);
            this.Ustpanel.Controls.Add(this.superTextBoxSoyadi);
            this.Ustpanel.Controls.Add(this.superTextBoxTckimlikno);
            this.Ustpanel.Controls.Add(this.labelControl2);
            this.Ustpanel.Controls.Add(this.labelControlTckimlikNo);
            this.Ustpanel.Controls.Add(this.labelControlAdi);
            this.Ustpanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.Ustpanel.Location = new System.Drawing.Point(0, 0);
            this.Ustpanel.Name = "Ustpanel";
            this.Ustpanel.Size = new System.Drawing.Size(550, 54);
            this.Ustpanel.TabIndex = 2;
            // 
            // superTextBoxAdi
            // 
            this.superTextBoxAdi.AutoSize = true;
            this.superTextBoxAdi.Format = PowerScada.Formati.Text;
            this.superTextBoxAdi.GetFocusColor = System.Drawing.Color.Empty;
            this.superTextBoxAdi.GotFocusColor = System.Drawing.Color.Empty;
            this.superTextBoxAdi.Location = new System.Drawing.Point(244, 23);
            this.superTextBoxAdi.Name = "superTextBoxAdi";
            this.superTextBoxAdi.Size = new System.Drawing.Size(107, 20);
            this.superTextBoxAdi.TabIndex = 31;
            // 
            // superTextBoxSoyadi
            // 
            this.superTextBoxSoyadi.AutoSize = true;
            this.superTextBoxSoyadi.Format = PowerScada.Formati.Text;
            this.superTextBoxSoyadi.GetFocusColor = System.Drawing.Color.Empty;
            this.superTextBoxSoyadi.GotFocusColor = System.Drawing.Color.Empty;
            this.superTextBoxSoyadi.Location = new System.Drawing.Point(418, 23);
            this.superTextBoxSoyadi.Name = "superTextBoxSoyadi";
            this.superTextBoxSoyadi.Size = new System.Drawing.Size(107, 20);
            this.superTextBoxSoyadi.TabIndex = 30;
            // 
            // superTextBoxTckimlikno
            // 
            this.superTextBoxTckimlikno.AutoSize = true;
            this.superTextBoxTckimlikno.Format = PowerScada.Formati.Sayisal;
            this.superTextBoxTckimlikno.GetFocusColor = System.Drawing.Color.Empty;
            this.superTextBoxTckimlikno.GotFocusColor = System.Drawing.Color.Empty;
            this.superTextBoxTckimlikno.Location = new System.Drawing.Point(88, 23);
            this.superTextBoxTckimlikno.Name = "superTextBoxTckimlikno";
            this.superTextBoxTckimlikno.Size = new System.Drawing.Size(107, 20);
            this.superTextBoxTckimlikno.TabIndex = 29;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(368, 30);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(44, 13);
            this.labelControl2.TabIndex = 28;
            this.labelControl2.Text = "Soyadı :";
            // 
            // labelControlTckimlikNo
            // 
            this.labelControlTckimlikNo.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControlTckimlikNo.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControlTckimlikNo.Appearance.Options.UseFont = true;
            this.labelControlTckimlikNo.Appearance.Options.UseForeColor = true;
            this.labelControlTckimlikNo.Location = new System.Drawing.Point(9, 30);
            this.labelControlTckimlikNo.Name = "labelControlTckimlikNo";
            this.labelControlTckimlikNo.Size = new System.Drawing.Size(73, 13);
            this.labelControlTckimlikNo.TabIndex = 27;
            this.labelControlTckimlikNo.Text = "Tc Kimlik No :";
            // 
            // labelControlAdi
            // 
            this.labelControlAdi.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControlAdi.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControlAdi.Appearance.Options.UseFont = true;
            this.labelControlAdi.Appearance.Options.UseForeColor = true;
            this.labelControlAdi.Location = new System.Drawing.Point(214, 30);
            this.labelControlAdi.Name = "labelControlAdi";
            this.labelControlAdi.Size = new System.Drawing.Size(24, 13);
            this.labelControlAdi.TabIndex = 26;
            this.labelControlAdi.Text = "Adı :";
            // 
            // SimpleLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 353);
            this.Controls.Add(this.Altpanel);
            this.Controls.Add(this.Ustpanel);
            this.KeyPreview = true;
            this.Name = "SimpleLookup";
            this.Text = "Seç:";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SimpleLookup_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.Altpanel.ResumeLayout(false);
            this.Ustpanel.ResumeLayout(false);
            this.Ustpanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Panel Altpanel;
        private System.Windows.Forms.Panel Ustpanel;
        private SuperTextBox superTextBoxAdi;
        private SuperTextBox superTextBoxSoyadi;
        private SuperTextBox superTextBoxTckimlikno;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControlTckimlikNo;
        private DevExpress.XtraEditors.LabelControl labelControlAdi;
    }
}