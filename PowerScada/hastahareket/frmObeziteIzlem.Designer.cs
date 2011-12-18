namespace AHBS2010
{
    partial class frmObeziteIzlem
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
            this.edtboyu = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.edtagirligi = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.edtbelgenisligi = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dateEditbirsonrakiIzlemTarihi = new DevExpress.XtraEditors.DateEdit();
            this.labelSablonBilgisi = new System.Windows.Forms.Label();
            this.checkBoxBirSonrakiIzlemTarihi = new System.Windows.Forms.CheckBox();
            this.labelIzlemSaati = new DevExpress.XtraEditors.LabelControl();
            this.labelIzlemSaatilabel = new DevExpress.XtraEditors.LabelControl();
            this.labelIzlemTarihi = new DevExpress.XtraEditors.LabelControl();
            this.DateEditIzlemTarihi = new DevExpress.XtraEditors.DateEdit();
            this.spinEditBasen = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.ucEnumGosterBKISonucu = new AHBS2010.UcEnumGoster();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButtonRandevuDuzenle = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlbuttonss)).BeginInit();
            this.pnlbuttonss.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl112s)).BeginInit();
            this.panelControl112s.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtboyu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtagirligi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtbelgenisligi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditbirsonrakiIzlemTarihi.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditbirsonrakiIzlemTarihi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEditIzlemTarihi.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEditIzlemTarihi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditBasen.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlbuttonss
            // 
            this.pnlbuttonss.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pnlbuttonss.Appearance.Options.UseBackColor = true;
            this.pnlbuttonss.Location = new System.Drawing.Point(0, 294);
            this.pnlbuttonss.Size = new System.Drawing.Size(628, 52);
            this.pnlbuttonss.TabIndex = 6;
            // 
            // panelControl112s
            // 
            this.panelControl112s.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControl112s.Appearance.Options.UseBackColor = true;
            this.panelControl112s.Size = new System.Drawing.Size(624, 48);
            // 
            // btnvazgecc
            // 
            this.btnvazgecc.Location = new System.Drawing.Point(336, 7);
            // 
            // btntamam
            // 
            this.btntamam.Location = new System.Drawing.Point(472, 7);
            // 
            // groupBoxHastaBilgileri
            // 
            this.groupBoxHastaBilgileri.Size = new System.Drawing.Size(628, 51);
            // 
            // simpleButtonsistemBilgisiniGoster
            // 
            this.simpleButtonsistemBilgisiniGoster.Location = new System.Drawing.Point(156, 7);
            // 
            // edtboyu
            // 
            this.edtboyu.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.edtboyu.Location = new System.Drawing.Point(155, 141);
            this.edtboyu.Name = "edtboyu";
            this.edtboyu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.edtboyu.Size = new System.Drawing.Size(54, 20);
            this.edtboyu.TabIndex = 6;
            this.edtboyu.EditValueChanged += new System.EventHandler(this.edtboyu_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 144);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(45, 13);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "Boyu(cm)";
            // 
            // edtagirligi
            // 
            this.edtagirligi.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.edtagirligi.Location = new System.Drawing.Point(155, 115);
            this.edtagirligi.Name = "edtagirligi";
            this.edtagirligi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.edtagirligi.Size = new System.Drawing.Size(54, 20);
            this.edtagirligi.TabIndex = 4;
            this.edtagirligi.EditValueChanged += new System.EventHandler(this.edtagirligi_EditValueChanged);
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(8, 118);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(31, 13);
            this.labelControl12.TabIndex = 3;
            this.labelControl12.Text = "Ağırlığı";
            // 
            // edtbelgenisligi
            // 
            this.edtbelgenisligi.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.edtbelgenisligi.Location = new System.Drawing.Point(155, 167);
            this.edtbelgenisligi.Name = "edtbelgenisligi";
            this.edtbelgenisligi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.edtbelgenisligi.Size = new System.Drawing.Size(54, 20);
            this.edtbelgenisligi.TabIndex = 8;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(8, 170);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(76, 13);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "Bel Genişliği(cm)";
            // 
            // dateEditbirsonrakiIzlemTarihi
            // 
            this.dateEditbirsonrakiIzlemTarihi.EditValue = new System.DateTime(2011, 2, 26, 0, 0, 0, 0);
            this.dateEditbirsonrakiIzlemTarihi.Location = new System.Drawing.Point(485, 83);
            this.dateEditbirsonrakiIzlemTarihi.Name = "dateEditbirsonrakiIzlemTarihi";
            this.dateEditbirsonrakiIzlemTarihi.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dateEditbirsonrakiIzlemTarihi.Properties.Appearance.Options.UseFont = true;
            this.dateEditbirsonrakiIzlemTarihi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditbirsonrakiIzlemTarihi.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditbirsonrakiIzlemTarihi.Size = new System.Drawing.Size(100, 20);
            this.dateEditbirsonrakiIzlemTarihi.TabIndex = 10;
            this.dateEditbirsonrakiIzlemTarihi.Visible = false;
            // 
            // labelSablonBilgisi
            // 
            this.labelSablonBilgisi.AutoSize = true;
            this.labelSablonBilgisi.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelSablonBilgisi.ForeColor = System.Drawing.Color.Red;
            this.labelSablonBilgisi.Location = new System.Drawing.Point(8, 54);
            this.labelSablonBilgisi.Name = "labelSablonBilgisi";
            this.labelSablonBilgisi.Size = new System.Drawing.Size(43, 14);
            this.labelSablonBilgisi.TabIndex = 0;
            this.labelSablonBilgisi.Text = "label2";
            this.labelSablonBilgisi.Visible = false;
            // 
            // checkBoxBirSonrakiIzlemTarihi
            // 
            this.checkBoxBirSonrakiIzlemTarihi.AutoSize = true;
            this.checkBoxBirSonrakiIzlemTarihi.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.checkBoxBirSonrakiIzlemTarihi.Location = new System.Drawing.Point(306, 86);
            this.checkBoxBirSonrakiIzlemTarihi.Name = "checkBoxBirSonrakiIzlemTarihi";
            this.checkBoxBirSonrakiIzlemTarihi.Size = new System.Drawing.Size(173, 20);
            this.checkBoxBirSonrakiIzlemTarihi.TabIndex = 9;
            this.checkBoxBirSonrakiIzlemTarihi.Text = "Bir Sonraki İzlem Tarihi";
            this.checkBoxBirSonrakiIzlemTarihi.UseVisualStyleBackColor = true;
            this.checkBoxBirSonrakiIzlemTarihi.CheckedChanged += new System.EventHandler(this.checkBoxBirSonrakiIzlemTarihi_CheckedChanged);
            // 
            // labelIzlemSaati
            // 
            this.labelIzlemSaati.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelIzlemSaati.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelIzlemSaati.Appearance.Options.UseFont = true;
            this.labelIzlemSaati.Appearance.Options.UseForeColor = true;
            this.labelIzlemSaati.Location = new System.Drawing.Point(485, 113);
            this.labelIzlemSaati.Name = "labelIzlemSaati";
            this.labelIzlemSaati.Size = new System.Drawing.Size(28, 14);
            this.labelIzlemSaati.TabIndex = 12;
            this.labelIzlemSaati.Text = "Saat";
            this.labelIzlemSaati.Visible = false;
            // 
            // labelIzlemSaatilabel
            // 
            this.labelIzlemSaatilabel.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelIzlemSaatilabel.Appearance.Options.UseFont = true;
            this.labelIzlemSaatilabel.Location = new System.Drawing.Point(306, 113);
            this.labelIzlemSaatilabel.Name = "labelIzlemSaatilabel";
            this.labelIzlemSaatilabel.Size = new System.Drawing.Size(144, 16);
            this.labelIzlemSaatilabel.TabIndex = 11;
            this.labelIzlemSaatilabel.Text = "Bir Sonraki İzlem Saati";
            this.labelIzlemSaatilabel.Visible = false;
            // 
            // labelIzlemTarihi
            // 
            this.labelIzlemTarihi.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelIzlemTarihi.Appearance.Options.UseFont = true;
            this.labelIzlemTarihi.Location = new System.Drawing.Point(8, 84);
            this.labelIzlemTarihi.Name = "labelIzlemTarihi";
            this.labelIzlemTarihi.Size = new System.Drawing.Size(73, 16);
            this.labelIzlemTarihi.TabIndex = 1;
            this.labelIzlemTarihi.Text = "İzlem Tarihi";
            // 
            // DateEditIzlemTarihi
            // 
            this.DateEditIzlemTarihi.EditValue = new System.DateTime(2011, 3, 2, 21, 29, 36, 0);
            this.DateEditIzlemTarihi.Location = new System.Drawing.Point(155, 81);
            this.DateEditIzlemTarihi.Name = "DateEditIzlemTarihi";
            this.DateEditIzlemTarihi.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.DateEditIzlemTarihi.Properties.Appearance.Options.UseFont = true;
            this.DateEditIzlemTarihi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DateEditIzlemTarihi.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.DateEditIzlemTarihi.Size = new System.Drawing.Size(104, 22);
            this.DateEditIzlemTarihi.TabIndex = 2;
            // 
            // spinEditBasen
            // 
            this.spinEditBasen.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditBasen.Location = new System.Drawing.Point(155, 192);
            this.spinEditBasen.Name = "spinEditBasen";
            this.spinEditBasen.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditBasen.Size = new System.Drawing.Size(54, 20);
            this.spinEditBasen.TabIndex = 14;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(8, 195);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(53, 13);
            this.labelControl3.TabIndex = 13;
            this.labelControl3.Text = "Basen (cm)";
            // 
            // ucEnumGosterBKISonucu
            // 
            this.ucEnumGosterBKISonucu.Deger = 0;
            this.ucEnumGosterBKISonucu.Enabled = false;
            this.ucEnumGosterBKISonucu.EnumTuru = "BKISonucu";
            this.ucEnumGosterBKISonucu.Location = new System.Drawing.Point(155, 218);
            this.ucEnumGosterBKISonucu.Name = "ucEnumGosterBKISonucu";
            this.ucEnumGosterBKISonucu.Size = new System.Drawing.Size(109, 19);
            this.ucEnumGosterBKISonucu.TabIndex = 15;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(8, 224);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(29, 13);
            this.labelControl4.TabIndex = 16;
            this.labelControl4.Text = "Sonuç";
            // 
            // simpleButtonRandevuDuzenle
            // 
            this.simpleButtonRandevuDuzenle.Location = new System.Drawing.Point(306, 144);
            this.simpleButtonRandevuDuzenle.Name = "simpleButtonRandevuDuzenle";
            this.simpleButtonRandevuDuzenle.Size = new System.Drawing.Size(279, 33);
            this.simpleButtonRandevuDuzenle.TabIndex = 26;
            this.simpleButtonRandevuDuzenle.Text = "Randevu Düzenle";
            this.simpleButtonRandevuDuzenle.Visible = false;
            this.simpleButtonRandevuDuzenle.Click += new System.EventHandler(this.simpleButtonRandevuDuzenle_Click);
            // 
            // frmObeziteIzlem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(628, 346);
            this.Controls.Add(this.simpleButtonRandevuDuzenle);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.ucEnumGosterBKISonucu);
            this.Controls.Add(this.spinEditBasen);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelIzlemTarihi);
            this.Controls.Add(this.DateEditIzlemTarihi);
            this.Controls.Add(this.labelIzlemSaati);
            this.Controls.Add(this.labelIzlemSaatilabel);
            this.Controls.Add(this.checkBoxBirSonrakiIzlemTarihi);
            this.Controls.Add(this.labelSablonBilgisi);
            this.Controls.Add(this.dateEditbirsonrakiIzlemTarihi);
            this.Controls.Add(this.edtbelgenisligi);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.edtboyu);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.edtagirligi);
            this.Controls.Add(this.labelControl12);
            this.Name = "frmObeziteIzlem";
            this.Controls.SetChildIndex(this.groupBoxHastaBilgileri, 0);
            this.Controls.SetChildIndex(this.pnlbuttonss, 0);
            this.Controls.SetChildIndex(this.labelControl12, 0);
            this.Controls.SetChildIndex(this.edtagirligi, 0);
            this.Controls.SetChildIndex(this.labelControl1, 0);
            this.Controls.SetChildIndex(this.edtboyu, 0);
            this.Controls.SetChildIndex(this.labelControl2, 0);
            this.Controls.SetChildIndex(this.edtbelgenisligi, 0);
            this.Controls.SetChildIndex(this.dateEditbirsonrakiIzlemTarihi, 0);
            this.Controls.SetChildIndex(this.labelSablonBilgisi, 0);
            this.Controls.SetChildIndex(this.checkBoxBirSonrakiIzlemTarihi, 0);
            this.Controls.SetChildIndex(this.labelIzlemSaatilabel, 0);
            this.Controls.SetChildIndex(this.labelIzlemSaati, 0);
            this.Controls.SetChildIndex(this.DateEditIzlemTarihi, 0);
            this.Controls.SetChildIndex(this.labelIzlemTarihi, 0);
            this.Controls.SetChildIndex(this.labelControl3, 0);
            this.Controls.SetChildIndex(this.spinEditBasen, 0);
            this.Controls.SetChildIndex(this.ucEnumGosterBKISonucu, 0);
            this.Controls.SetChildIndex(this.labelControl4, 0);
            this.Controls.SetChildIndex(this.simpleButtonRandevuDuzenle, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pnlbuttonss)).EndInit();
            this.pnlbuttonss.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl112s)).EndInit();
            this.panelControl112s.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.edtboyu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtagirligi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtbelgenisligi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditbirsonrakiIzlemTarihi.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditbirsonrakiIzlemTarihi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEditIzlemTarihi.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEditIzlemTarihi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditBasen.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SpinEdit edtboyu;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SpinEdit edtagirligi;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.SpinEdit edtbelgenisligi;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit dateEditbirsonrakiIzlemTarihi;
        private System.Windows.Forms.Label labelSablonBilgisi;
        private System.Windows.Forms.CheckBox checkBoxBirSonrakiIzlemTarihi;
        private DevExpress.XtraEditors.LabelControl labelIzlemSaati;
        private DevExpress.XtraEditors.LabelControl labelIzlemSaatilabel;
        private DevExpress.XtraEditors.LabelControl labelIzlemTarihi;
        private DevExpress.XtraEditors.DateEdit DateEditIzlemTarihi;
        private DevExpress.XtraEditors.SpinEdit spinEditBasen;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private UcEnumGoster ucEnumGosterBKISonucu;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton simpleButtonRandevuDuzenle;
    }
}
