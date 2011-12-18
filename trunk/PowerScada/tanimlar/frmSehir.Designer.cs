namespace PowerScada
{
    partial class frmSehir
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
            this.textEditAdi = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.textEditKodu = new DevExpress.XtraEditors.TextEdit();
            this.labelControlKodu = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelbutton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paneldetay)).BeginInit();
            this.paneldetay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.formbs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paneldetayleft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paneldetayfill)).BeginInit();
            this.paneldetayfill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paneldetaybottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paneldetaytop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlgrd)).BeginInit();
            this.pnlgrd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpgrd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditAdi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditKodu.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelbutton
            // 
            this.panelbutton.Appearance.BackColor = System.Drawing.Color.White;
            this.panelbutton.Appearance.Options.UseBackColor = true;
            this.panelbutton.Size = new System.Drawing.Size(613, 49);
            // 
            // paneldetay
            // 
            this.paneldetay.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.paneldetay.Appearance.Options.UseBackColor = true;
            this.paneldetay.Size = new System.Drawing.Size(613, 150);
            // 
            // sp
            // 
            this.sp.Location = new System.Drawing.Point(0, 199);
            this.sp.Size = new System.Drawing.Size(613, 6);
            // 
            // paneldetayleft
            // 
            this.paneldetayleft.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.paneldetayleft.Appearance.Options.UseBackColor = true;
            this.paneldetayleft.Size = new System.Drawing.Size(24, 150);
            // 
            // paneldetayfill
            // 
            this.paneldetayfill.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.paneldetayfill.Appearance.Options.UseBackColor = true;
            this.paneldetayfill.Controls.Add(this.textEditKodu);
            this.paneldetayfill.Controls.Add(this.labelControlKodu);
            this.paneldetayfill.Controls.Add(this.textEditAdi);
            this.paneldetayfill.Controls.Add(this.labelControl1);
            this.paneldetayfill.Size = new System.Drawing.Size(589, 104);
            // 
            // paneldetaybottom
            // 
            this.paneldetaybottom.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.paneldetaybottom.Appearance.Options.UseBackColor = true;
            this.paneldetaybottom.Location = new System.Drawing.Point(24, 127);
            this.paneldetaybottom.Size = new System.Drawing.Size(589, 23);
            // 
            // paneldetaytop
            // 
            this.paneldetaytop.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.paneldetaytop.Appearance.Options.UseBackColor = true;
            this.paneldetaytop.Size = new System.Drawing.Size(589, 23);
            // 
            // pnlgrd
            // 
            this.pnlgrd.Location = new System.Drawing.Point(0, 205);
            this.pnlgrd.Size = new System.Drawing.Size(613, 340);
            // 
            // grpgrd
            // 
            this.grpgrd.Size = new System.Drawing.Size(609, 336);
            // 
            // textEditAdi
            // 
            this.textEditAdi.Location = new System.Drawing.Point(114, 50);
            this.textEditAdi.Name = "textEditAdi";
            this.textEditAdi.Size = new System.Drawing.Size(200, 20);
            this.textEditAdi.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 52);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(42, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Şehir Adı";
            // 
            // textEditKodu
            // 
            this.textEditKodu.Location = new System.Drawing.Point(114, 28);
            this.textEditKodu.Name = "textEditKodu";
            this.textEditKodu.Size = new System.Drawing.Size(200, 20);
            this.textEditKodu.TabIndex = 3;
            // 
            // labelControlKodu
            // 
            this.labelControlKodu.Location = new System.Drawing.Point(8, 30);
            this.labelControlKodu.Name = "labelControlKodu";
            this.labelControlKodu.Size = new System.Drawing.Size(24, 13);
            this.labelControlKodu.TabIndex = 2;
            this.labelControlKodu.Text = "Kodu";
            // 
            // frmSehir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(613, 545);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmSehir";
            ((System.ComponentModel.ISupportInitialize)(this.panelbutton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paneldetay)).EndInit();
            this.paneldetay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.formbs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paneldetayleft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paneldetayfill)).EndInit();
            this.paneldetayfill.ResumeLayout(false);
            this.paneldetayfill.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paneldetaybottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paneldetaytop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlgrd)).EndInit();
            this.pnlgrd.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpgrd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditAdi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditKodu.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit textEditAdi;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit textEditKodu;
        private DevExpress.XtraEditors.LabelControl labelControlKodu;


    }
}
