namespace AHBS2010.Rapor
{
    partial class frm1549YasKadinBildirimListesi
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
            this.dateEditBasTarih = new DevExpress.XtraEditors.DateEdit();
            this.dateEditBitTarih = new DevExpress.XtraEditors.DateEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBasTarih.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBasTarih.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBitTarih.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBitTarih.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dateEditBasTarih
            // 
            this.dateEditBasTarih.EditValue = null;
            this.dateEditBasTarih.Location = new System.Drawing.Point(121, 53);
            this.dateEditBasTarih.Name = "dateEditBasTarih";
            this.dateEditBasTarih.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditBasTarih.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditBasTarih.Size = new System.Drawing.Size(139, 20);
            this.dateEditBasTarih.TabIndex = 0;
            // 
            // dateEditBitTarih
            // 
            this.dateEditBitTarih.EditValue = null;
            this.dateEditBitTarih.Location = new System.Drawing.Point(121, 95);
            this.dateEditBitTarih.Name = "dateEditBitTarih";
            this.dateEditBitTarih.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditBitTarih.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditBitTarih.Size = new System.Drawing.Size(139, 20);
            this.dateEditBitTarih.TabIndex = 1;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(121, 146);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(139, 23);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "Raporu Görüntüle";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(32, 56);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(73, 13);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Başlangıç Tarihi";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(32, 102);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Bitiş Tarihi";
            // 
            // frm1549YasKadinBildirimListesi
            // 
            this.ClientSize = new System.Drawing.Size(603, 398);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.dateEditBitTarih);
            this.Controls.Add(this.dateEditBasTarih);
            this.Name = "frm1549YasKadinBildirimListesi";
            this.Text = "15 - 49 Yaş Kadın Bildirim Listesi";
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBasTarih.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBasTarih.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBitTarih.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBitTarih.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit dateEditBasTarih;
        private DevExpress.XtraEditors.DateEdit dateEditBitTarih;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;

    
    }
}