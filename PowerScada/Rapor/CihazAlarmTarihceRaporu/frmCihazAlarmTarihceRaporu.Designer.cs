namespace PowerScada.Rapor
{
    partial class frmCihazAlarmTarihceRaporu
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
            this.pivotGrid = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.ClmnLokasyonKodu = new DevExpress.XtraPivotGrid.PivotGridField();
            this.clmnLokasyonAdi = new DevExpress.XtraPivotGrid.PivotGridField();
            this.clmnCihazKodu = new DevExpress.XtraPivotGrid.PivotGridField();
            this.clmnCihazAdi = new DevExpress.XtraPivotGrid.PivotGridField();
            this.clmnCihazTuru = new DevExpress.XtraPivotGrid.PivotGridField();
            this.clmnCihazModeli = new DevExpress.XtraPivotGrid.PivotGridField();
            this.clmnAlarmTipi = new DevExpress.XtraPivotGrid.PivotGridField();
            this.clmnAlarmMesaji = new DevExpress.XtraPivotGrid.PivotGridField();
            this.clmnEklemeTarihi = new DevExpress.XtraPivotGrid.PivotGridField();
            this.clmnEkleyenKullanici = new DevExpress.XtraPivotGrid.PivotGridField();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.editButtonCihazModeli = new PowerScada.EditButton();
            this.label3 = new System.Windows.Forms.Label();
            this.myCombo1 = new PowerScada.MyCombo();
            this.btnGotur = new System.Windows.Forms.Button();
            this.btnGetir = new System.Windows.Forms.Button();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dateEditBitTarih = new DevExpress.XtraEditors.DateEdit();
            this.dateEditBasTarih = new DevExpress.XtraEditors.DateEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.editButtonLokasyon = new PowerScada.EditButton();
            this.EditButtonCihaz = new PowerScada.EditButton();
            this.panelgridler = new System.Windows.Forms.Panel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.myComboAlarm = new PowerScada.MyCombo();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pivotGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBitTarih.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBitTarih.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBasTarih.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBasTarih.Properties)).BeginInit();
            this.panelgridler.SuspendLayout();
            this.SuspendLayout();
            // 
            // pivotGrid
            // 
            this.pivotGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pivotGrid.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.ClmnLokasyonKodu,
            this.clmnLokasyonAdi,
            this.clmnCihazKodu,
            this.clmnCihazAdi,
            this.clmnCihazTuru,
            this.clmnCihazModeli,
            this.clmnAlarmTipi,
            this.clmnAlarmMesaji,
            this.clmnEklemeTarihi,
            this.clmnEkleyenKullanici});
            this.pivotGrid.Location = new System.Drawing.Point(0, 0);
            this.pivotGrid.Name = "pivotGrid";
            this.pivotGrid.OptionsPrint.PageSettings.Landscape = true;
            this.pivotGrid.OptionsPrint.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.pivotGrid.OptionsView.ShowColumnGrandTotalHeader = false;
            this.pivotGrid.OptionsView.ShowColumnGrandTotals = false;
            this.pivotGrid.OptionsView.ShowColumnTotals = false;
            this.pivotGrid.OptionsView.ShowRowGrandTotals = false;
            this.pivotGrid.OptionsView.ShowRowTotals = false;
            this.pivotGrid.Size = new System.Drawing.Size(880, 298);
            this.pivotGrid.TabIndex = 0;
            // 
            // ClmnLokasyonKodu
            // 
            this.ClmnLokasyonKodu.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.ClmnLokasyonKodu.AreaIndex = 0;
            this.ClmnLokasyonKodu.Caption = "LokasyonKodu";
            this.ClmnLokasyonKodu.FieldName = "LokasyonKodu";
            this.ClmnLokasyonKodu.Name = "ClmnLokasyonKodu";
            this.ClmnLokasyonKodu.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Max;
            this.ClmnLokasyonKodu.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            // 
            // clmnLokasyonAdi
            // 
            this.clmnLokasyonAdi.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.clmnLokasyonAdi.AreaIndex = 1;
            this.clmnLokasyonAdi.Caption = "Lokasyon Adı";
            this.clmnLokasyonAdi.FieldName = "LokasyonAdi";
            this.clmnLokasyonAdi.Name = "clmnLokasyonAdi";
            this.clmnLokasyonAdi.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Max;
            this.clmnLokasyonAdi.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            // 
            // clmnCihazKodu
            // 
            this.clmnCihazKodu.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.clmnCihazKodu.AreaIndex = 2;
            this.clmnCihazKodu.Caption = "Cihaz Kodu";
            this.clmnCihazKodu.FieldName = "CihazKodu";
            this.clmnCihazKodu.Name = "clmnCihazKodu";
            this.clmnCihazKodu.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Max;
            this.clmnCihazKodu.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            // 
            // clmnCihazAdi
            // 
            this.clmnCihazAdi.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.clmnCihazAdi.AreaIndex = 3;
            this.clmnCihazAdi.Caption = "Cihaz Adı";
            this.clmnCihazAdi.FieldName = "CihazAdi";
            this.clmnCihazAdi.Name = "clmnCihazAdi";
            this.clmnCihazAdi.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Max;
            this.clmnCihazAdi.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            // 
            // clmnCihazTuru
            // 
            this.clmnCihazTuru.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.clmnCihazTuru.AreaIndex = 4;
            this.clmnCihazTuru.Caption = "Cihaz Türü";
            this.clmnCihazTuru.FieldName = "CihazTuru";
            this.clmnCihazTuru.Name = "clmnCihazTuru";
            this.clmnCihazTuru.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Max;
            this.clmnCihazTuru.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            // 
            // clmnCihazModeli
            // 
            this.clmnCihazModeli.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.clmnCihazModeli.AreaIndex = 5;
            this.clmnCihazModeli.Caption = "Cihaz Modeli";
            this.clmnCihazModeli.FieldName = "CihazModeli";
            this.clmnCihazModeli.Name = "clmnCihazModeli";
            this.clmnCihazModeli.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Max;
            this.clmnCihazModeli.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            // 
            // clmnAlarmTipi
            // 
            this.clmnAlarmTipi.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.clmnAlarmTipi.AreaIndex = 7;
            this.clmnAlarmTipi.Caption = "Alarm Tipi";
            this.clmnAlarmTipi.FieldName = "AlarmTipi";
            this.clmnAlarmTipi.Name = "clmnAlarmTipi";
            this.clmnAlarmTipi.Options.ShowTotals = false;
            this.clmnAlarmTipi.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Max;
            this.clmnAlarmTipi.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            // 
            // clmnAlarmMesaji
            // 
            this.clmnAlarmMesaji.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.clmnAlarmMesaji.AreaIndex = 1;
            this.clmnAlarmMesaji.Caption = "Alarm Mesajı";
            this.clmnAlarmMesaji.FieldName = "AlarmMesaji";
            this.clmnAlarmMesaji.Name = "clmnAlarmMesaji";
            this.clmnAlarmMesaji.Options.ShowTotals = false;
            this.clmnAlarmMesaji.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Max;
            this.clmnAlarmMesaji.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            // 
            // clmnEklemeTarihi
            // 
            this.clmnEklemeTarihi.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.clmnEklemeTarihi.AreaIndex = 6;
            this.clmnEklemeTarihi.Caption = "Kayıt Tarihi";
            this.clmnEklemeTarihi.FieldName = "EklemeTarihi";
            this.clmnEklemeTarihi.Name = "clmnEklemeTarihi";
            this.clmnEklemeTarihi.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Max;
            this.clmnEklemeTarihi.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            // 
            // clmnEkleyenKullanici
            // 
            this.clmnEkleyenKullanici.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.clmnEkleyenKullanici.AreaIndex = 2;
            this.clmnEkleyenKullanici.Caption = "Kaydeden Kullanici";
            this.clmnEkleyenKullanici.FieldName = "EkleyenKullanici";
            this.clmnEkleyenKullanici.Name = "clmnEkleyenKullanici";
            this.clmnEkleyenKullanici.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Max;
            this.clmnEkleyenKullanici.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.myComboAlarm);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.editButtonCihazModeli);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.myCombo1);
            this.groupBox1.Controls.Add(this.btnGotur);
            this.groupBox1.Controls.Add(this.btnGetir);
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Controls.Add(this.dateEditBitTarih);
            this.groupBox1.Controls.Add(this.dateEditBasTarih);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.editButtonLokasyon);
            this.groupBox1.Controls.Add(this.EditButtonCihaz);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(880, 151);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtreler";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(755, 83);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "Excele Gönder";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Cihaz Modeli";
            // 
            // editButtonCihazModeli
            // 
            this.editButtonCihazModeli.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editButtonCihazModeli.BackColor = System.Drawing.SystemColors.Window;
            this.editButtonCihazModeli.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editButtonCihazModeli.CommandName = "EditButtonCihazModeli";
            this.editButtonCihazModeli.Id = ((long)(0));
            this.editButtonCihazModeli.Location = new System.Drawing.Point(108, 80);
            this.editButtonCihazModeli.Name = "editButtonCihazModeli";
            this.editButtonCihazModeli.NewValue = "";
            this.editButtonCihazModeli.OldValue = "";
            this.editButtonCihazModeli.ReadOnly = false;
            this.editButtonCihazModeli.Size = new System.Drawing.Size(239, 21);
            this.editButtonCihazModeli.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Cihaz Türü";
            // 
            // myCombo1
            // 
            this.myCombo1.BindingTürü = PowerScada.MyCombo.Binding.Enum;
            this.myCombo1.DisplayField = "";
            this.myCombo1.EmptyMessage = "Cihaz Türü Seçiniz";
            this.myCombo1.EmptyRow = true;
            this.myCombo1.EntityName = "";
            this.myCombo1.EnumTipi = "CihazTuru";
            this.myCombo1.Id = 0;
            this.myCombo1.Location = new System.Drawing.Point(108, 107);
            this.myCombo1.Name = "myCombo1";
            this.myCombo1.OldId = 0;
            this.myCombo1.SelectedIndex = 0;
            this.myCombo1.Size = new System.Drawing.Size(239, 21);
            this.myCombo1.TabIndex = 14;
            this.myCombo1.WhereClause = null;
            // 
            // btnGotur
            // 
            this.btnGotur.Location = new System.Drawing.Point(755, 53);
            this.btnGotur.Name = "btnGotur";
            this.btnGotur.Size = new System.Drawing.Size(102, 23);
            this.btnGotur.TabIndex = 13;
            this.btnGotur.Text = "Görüntüle";
            this.btnGotur.UseVisualStyleBackColor = true;
            this.btnGotur.Click += new System.EventHandler(this.btnGotur_Click);
            // 
            // btnGetir
            // 
            this.btnGetir.Location = new System.Drawing.Point(755, 22);
            this.btnGetir.Name = "btnGetir";
            this.btnGetir.Size = new System.Drawing.Size(102, 23);
            this.btnGetir.TabIndex = 12;
            this.btnGetir.Text = "Getir";
            this.btnGetir.UseVisualStyleBackColor = true;
            this.btnGetir.Click += new System.EventHandler(this.btnGetir_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(376, 90);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 13);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "Bitiş Tarihi";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(376, 56);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(50, 13);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "Baş. Tarihi";
            // 
            // dateEditBitTarih
            // 
            this.dateEditBitTarih.EditValue = new System.DateTime(2011, 11, 13, 0, 0, 0, 0);
            this.dateEditBitTarih.Location = new System.Drawing.Point(456, 83);
            this.dateEditBitTarih.Name = "dateEditBitTarih";
            this.dateEditBitTarih.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditBitTarih.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditBitTarih.Size = new System.Drawing.Size(130, 20);
            this.dateEditBitTarih.TabIndex = 7;
            // 
            // dateEditBasTarih
            // 
            this.dateEditBasTarih.EditValue = new System.DateTime(2011, 11, 13, 0, 0, 0, 0);
            this.dateEditBasTarih.Location = new System.Drawing.Point(456, 49);
            this.dateEditBasTarih.Name = "dateEditBasTarih";
            this.dateEditBasTarih.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditBasTarih.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditBasTarih.Size = new System.Drawing.Size(130, 20);
            this.dateEditBasTarih.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Lokasyon Adı";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cihaz Adı";
            // 
            // editButtonLokasyon
            // 
            this.editButtonLokasyon.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editButtonLokasyon.BackColor = System.Drawing.SystemColors.Window;
            this.editButtonLokasyon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editButtonLokasyon.CommandName = "EditButtonLokasyon";
            this.editButtonLokasyon.Id = ((long)(0));
            this.editButtonLokasyon.Location = new System.Drawing.Point(108, 46);
            this.editButtonLokasyon.Name = "editButtonLokasyon";
            this.editButtonLokasyon.NewValue = "";
            this.editButtonLokasyon.OldValue = "";
            this.editButtonLokasyon.ReadOnly = false;
            this.editButtonLokasyon.Size = new System.Drawing.Size(239, 21);
            this.editButtonLokasyon.TabIndex = 3;
            // 
            // EditButtonCihaz
            // 
            this.EditButtonCihaz.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.EditButtonCihaz.BackColor = System.Drawing.SystemColors.Window;
            this.EditButtonCihaz.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EditButtonCihaz.CommandName = "EditButtonCihaz";
            this.EditButtonCihaz.Id = ((long)(0));
            this.EditButtonCihaz.Location = new System.Drawing.Point(108, 20);
            this.EditButtonCihaz.Name = "EditButtonCihaz";
            this.EditButtonCihaz.NewValue = "";
            this.EditButtonCihaz.OldValue = "";
            this.EditButtonCihaz.ReadOnly = false;
            this.EditButtonCihaz.Size = new System.Drawing.Size(239, 21);
            this.EditButtonCihaz.TabIndex = 1;
            // 
            // panelgridler
            // 
            this.panelgridler.Controls.Add(this.pivotGrid);
            this.panelgridler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelgridler.Location = new System.Drawing.Point(0, 151);
            this.panelgridler.Name = "panelgridler";
            this.panelgridler.Size = new System.Drawing.Size(880, 298);
            this.panelgridler.TabIndex = 1;
            // 
            // myComboAlarm
            // 
            this.myComboAlarm.BindingTürü = PowerScada.MyCombo.Binding.Enum;
            this.myComboAlarm.DisplayField = "";
            this.myComboAlarm.EmptyMessage = "Alarm Tipi Seçiniz";
            this.myComboAlarm.EmptyRow = true;
            this.myComboAlarm.EntityName = "";
            this.myComboAlarm.EnumTipi = "AlarmTipi";
            this.myComboAlarm.Id = 0;
            this.myComboAlarm.Location = new System.Drawing.Point(456, 22);
            this.myComboAlarm.Name = "myComboAlarm";
            this.myComboAlarm.OldId = 0;
            this.myComboAlarm.SelectedIndex = 0;
            this.myComboAlarm.Size = new System.Drawing.Size(239, 21);
            this.myComboAlarm.TabIndex = 19;
            this.myComboAlarm.WhereClause = null;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(376, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Alarm Tipi";
            // 
            // frmCihazAlarmTarihceRaporu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 449);
            this.Controls.Add(this.panelgridler);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmCihazAlarmTarihceRaporu";
            this.Text = "Cihaz Tarihce Raporu";
            ((System.ComponentModel.ISupportInitialize)(this.pivotGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBitTarih.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBitTarih.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBasTarih.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBasTarih.Properties)).EndInit();
            this.panelgridler.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private EditButton editButtonLokasyon;
        private EditButton EditButtonCihaz;
        private System.Windows.Forms.Panel panelgridler;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGotur;
        private System.Windows.Forms.Button btnGetir;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dateEditBitTarih;
        private DevExpress.XtraEditors.DateEdit dateEditBasTarih;
        private System.Windows.Forms.Label label4;
        private EditButton editButtonCihazModeli;
        private System.Windows.Forms.Label label3;
        private MyCombo myCombo1;
        private DevExpress.XtraPivotGrid.PivotGridField ClmnLokasyonKodu;
        private DevExpress.XtraPivotGrid.PivotGridField clmnLokasyonAdi;
        private DevExpress.XtraPivotGrid.PivotGridField clmnCihazKodu;
        private DevExpress.XtraPivotGrid.PivotGridField clmnCihazAdi;
        private DevExpress.XtraPivotGrid.PivotGridField clmnCihazTuru;
        private DevExpress.XtraPivotGrid.PivotGridField clmnCihazModeli;
        private DevExpress.XtraPivotGrid.PivotGridField clmnAlarmTipi;
        private DevExpress.XtraPivotGrid.PivotGridField clmnAlarmMesaji;
        private DevExpress.XtraPivotGrid.PivotGridField clmnEklemeTarihi;
        private DevExpress.XtraPivotGrid.PivotGridField clmnEkleyenKullanici;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private DevExpress.XtraPivotGrid.PivotGridControl pivotGrid;
        private System.Windows.Forms.Label label5;
        private MyCombo myComboAlarm;
    }
}