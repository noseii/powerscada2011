namespace AHBS2010
{
    partial class frmSaglikIstirahat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSaglikIstirahat));
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.SpinEditGunSayisi = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.ucEnumGoster1 = new AHBS2010.UcEnumGoster();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.DateEditRaporBasTarih = new DevExpress.XtraEditors.DateEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlbuttonss)).BeginInit();
            this.pnlbuttonss.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl112s)).BeginInit();
            this.panelControl112s.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SpinEditGunSayisi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEditRaporBasTarih.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEditRaporBasTarih.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlbuttonss
            // 
            this.pnlbuttonss.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pnlbuttonss.Appearance.Options.UseBackColor = true;
            this.pnlbuttonss.Location = new System.Drawing.Point(0, 204);
            this.pnlbuttonss.Size = new System.Drawing.Size(643, 52);
            this.pnlbuttonss.TabIndex = 2;
            // 
            // panelControl112s
            // 
            this.panelControl112s.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControl112s.Appearance.Options.UseBackColor = true;
            this.panelControl112s.Controls.Add(this.simpleButton1);
            this.panelControl112s.Size = new System.Drawing.Size(639, 48);
            this.panelControl112s.Controls.SetChildIndex(this.simpleButtonsistemBilgisiniGoster, 0);
            this.panelControl112s.Controls.SetChildIndex(this.btntamam, 0);
            this.panelControl112s.Controls.SetChildIndex(this.btnvazgecc, 0);
            this.panelControl112s.Controls.SetChildIndex(this.simpleButton1, 0);
            // 
            // btnvazgecc
            // 
            this.btnvazgecc.Location = new System.Drawing.Point(199, 7);
            // 
            // btntamam
            // 
            this.btntamam.Location = new System.Drawing.Point(328, 7);
            // 
            // groupBoxHastaBilgileri
            // 
            this.groupBoxHastaBilgileri.Size = new System.Drawing.Size(643, 51);
            // 
            // simpleButtonsistemBilgisiniGoster
            // 
            this.simpleButtonsistemBilgisiniGoster.Location = new System.Drawing.Point(26, 7);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(457, 7);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(138, 36);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "Rapor Döküm";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // SpinEditGunSayisi
            // 
            this.SpinEditGunSayisi.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SpinEditGunSayisi.Location = new System.Drawing.Point(146, 143);
            this.SpinEditGunSayisi.Name = "SpinEditGunSayisi";
            this.SpinEditGunSayisi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.SpinEditGunSayisi.Size = new System.Drawing.Size(54, 20);
            this.SpinEditGunSayisi.TabIndex = 5;
            this.SpinEditGunSayisi.Visible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(34, 150);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(49, 13);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "Gün Sayısı";
            this.labelControl1.Visible = false;
            // 
            // ucEnumGoster1
            // 
            this.ucEnumGoster1.Deger = 0;
            this.ucEnumGoster1.EnumTuru = "RaporTuru";
            this.ucEnumGoster1.Location = new System.Drawing.Point(146, 75);
            this.ucEnumGoster1.Name = "ucEnumGoster1";
            this.ucEnumGoster1.Size = new System.Drawing.Size(169, 20);
            this.ucEnumGoster1.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(34, 82);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(54, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Rapor Türü";
            // 
            // DateEditRaporBasTarih
            // 
            this.DateEditRaporBasTarih.EditValue = new System.DateTime(2011, 3, 2, 21, 29, 36, 0);
            this.DateEditRaporBasTarih.Location = new System.Drawing.Point(146, 109);
            this.DateEditRaporBasTarih.Name = "DateEditRaporBasTarih";
            this.DateEditRaporBasTarih.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.DateEditRaporBasTarih.Properties.Appearance.Options.UseFont = true;
            this.DateEditRaporBasTarih.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DateEditRaporBasTarih.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.DateEditRaporBasTarih.Size = new System.Drawing.Size(98, 20);
            this.DateEditRaporBasTarih.TabIndex = 3;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(34, 116);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(105, 13);
            this.labelControl6.TabIndex = 2;
            this.labelControl6.Text = "Rapor Başlangıç Tarihi";
            // 
            // frmSaglikIstirahat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 256);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.DateEditRaporBasTarih);
            this.Controls.Add(this.ucEnumGoster1);
            this.Controls.Add(this.SpinEditGunSayisi);
            this.Name = "frmSaglikIstirahat";
            this.Text = "Rapor";
            this.Controls.SetChildIndex(this.SpinEditGunSayisi, 0);
            this.Controls.SetChildIndex(this.ucEnumGoster1, 0);
            this.Controls.SetChildIndex(this.DateEditRaporBasTarih, 0);
            this.Controls.SetChildIndex(this.labelControl1, 0);
            this.Controls.SetChildIndex(this.labelControl6, 0);
            this.Controls.SetChildIndex(this.labelControl2, 0);
            this.Controls.SetChildIndex(this.groupBoxHastaBilgileri, 0);
            this.Controls.SetChildIndex(this.pnlbuttonss, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pnlbuttonss)).EndInit();
            this.pnlbuttonss.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl112s)).EndInit();
            this.panelControl112s.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SpinEditGunSayisi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEditRaporBasTarih.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEditRaporBasTarih.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SpinEdit SpinEditGunSayisi;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private UcEnumGoster ucEnumGoster1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit DateEditRaporBasTarih;
        private DevExpress.XtraEditors.LabelControl labelControl6;
    }
}