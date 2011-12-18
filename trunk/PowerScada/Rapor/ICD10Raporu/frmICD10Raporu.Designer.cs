namespace AHBS2010.Rapor
{
    partial class frmICD10Raporu
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
            this.btnGotur = new System.Windows.Forms.Button();
            this.btnGetir = new System.Windows.Forms.Button();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.dateEditDogumBitTarih = new DevExpress.XtraEditors.DateEdit();
            this.dateEditDogumBasTarih = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dateEditBitTarih = new DevExpress.XtraEditors.DateEdit();
            this.dateEditBasTarih = new DevExpress.XtraEditors.DateEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.editButtonTeshis = new AHBS2010.EditButton();
            this.editButtonkisi = new AHBS2010.EditButton();
            this.panelgridler = new System.Windows.Forms.Panel();
            this.gridControlHastaDetaylari = new DevExpress.XtraGrid.GridControl();
            this.gridViewhastadetay = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splitterControl3 = new DevExpress.XtraEditors.SplitterControl();
            this.gridControlHastaliklar = new DevExpress.XtraGrid.GridControl();
            this.gridViewhastaliklar = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDogumBitTarih.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDogumBitTarih.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDogumBasTarih.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDogumBasTarih.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBitTarih.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBitTarih.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBasTarih.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBasTarih.Properties)).BeginInit();
            this.panelgridler.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlHastaDetaylari)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewhastadetay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlHastaliklar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewhastaliklar)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGotur);
            this.groupBox1.Controls.Add(this.btnGetir);
            this.groupBox1.Controls.Add(this.labelControl3);
            this.groupBox1.Controls.Add(this.labelControl4);
            this.groupBox1.Controls.Add(this.dateEditDogumBitTarih);
            this.groupBox1.Controls.Add(this.dateEditDogumBasTarih);
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Controls.Add(this.dateEditBitTarih);
            this.groupBox1.Controls.Add(this.dateEditBasTarih);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.editButtonTeshis);
            this.groupBox1.Controls.Add(this.editButtonkisi);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(880, 146);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtreler";
            // 
            // btnGotur
            // 
            this.btnGotur.Location = new System.Drawing.Point(551, 53);
            this.btnGotur.Name = "btnGotur";
            this.btnGotur.Size = new System.Drawing.Size(102, 23);
            this.btnGotur.TabIndex = 13;
            this.btnGotur.Text = "Görüntüle";
            this.btnGotur.UseVisualStyleBackColor = true;
            this.btnGotur.Click += new System.EventHandler(this.btnGotur_Click);
            // 
            // btnGetir
            // 
            this.btnGetir.Location = new System.Drawing.Point(551, 22);
            this.btnGetir.Name = "btnGetir";
            this.btnGetir.Size = new System.Drawing.Size(102, 23);
            this.btnGetir.TabIndex = 12;
            this.btnGetir.Text = "Getir";
            this.btnGetir.UseVisualStyleBackColor = true;
            this.btnGetir.Click += new System.EventHandler(this.btnGetir_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(263, 119);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(84, 13);
            this.labelControl3.TabIndex = 10;
            this.labelControl3.Text = "Doğum Bitiş Tarihi";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(263, 89);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(86, 13);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "Doğum Baş. Tarihi";
            // 
            // dateEditDogumBitTarih
            // 
            this.dateEditDogumBitTarih.EditValue = null;
            this.dateEditDogumBitTarih.Location = new System.Drawing.Point(365, 113);
            this.dateEditDogumBitTarih.Name = "dateEditDogumBitTarih";
            this.dateEditDogumBitTarih.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditDogumBitTarih.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditDogumBitTarih.Size = new System.Drawing.Size(130, 20);
            this.dateEditDogumBitTarih.TabIndex = 11;
            // 
            // dateEditDogumBasTarih
            // 
            this.dateEditDogumBasTarih.EditValue = null;
            this.dateEditDogumBasTarih.Location = new System.Drawing.Point(365, 83);
            this.dateEditDogumBasTarih.Name = "dateEditDogumBasTarih";
            this.dateEditDogumBasTarih.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditDogumBasTarih.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditDogumBasTarih.Size = new System.Drawing.Size(130, 20);
            this.dateEditDogumBasTarih.TabIndex = 9;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(6, 120);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 13);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "Bitiş Tarihi";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(6, 90);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(50, 13);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "Baş. Tarihi";
            // 
            // dateEditBitTarih
            // 
            this.dateEditBitTarih.EditValue = new System.DateTime(2011, 8, 29, 0, 0, 0, 0);
            this.dateEditBitTarih.Location = new System.Drawing.Point(108, 114);
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
            this.dateEditBasTarih.EditValue = new System.DateTime(2011, 8, 29, 0, 0, 0, 0);
            this.dateEditBasTarih.Location = new System.Drawing.Point(108, 84);
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
            this.label2.Location = new System.Drawing.Point(6, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Hastalık Adı";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hasta Adı Soyadı";
            // 
            // editButtonTeshis
            // 
            this.editButtonTeshis.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editButtonTeshis.BackColor = System.Drawing.SystemColors.Window;
            this.editButtonTeshis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editButtonTeshis.CommandName = "EditButtonYetkili";
            this.editButtonTeshis.Id = ((long)(0));
            this.editButtonTeshis.Location = new System.Drawing.Point(108, 53);
            this.editButtonTeshis.Name = "editButtonTeshis";
            this.editButtonTeshis.NewValue = "";
            this.editButtonTeshis.OldValue = "";
            this.editButtonTeshis.ReadOnly = false;
            this.editButtonTeshis.Size = new System.Drawing.Size(387, 21);
            this.editButtonTeshis.TabIndex = 3;
            // 
            // editButtonkisi
            // 
            this.editButtonkisi.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editButtonkisi.BackColor = System.Drawing.SystemColors.Window;
            this.editButtonkisi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editButtonkisi.CommandName = "EditButtonHasta";
            this.editButtonkisi.Id = ((long)(0));
            this.editButtonkisi.Location = new System.Drawing.Point(108, 22);
            this.editButtonkisi.Name = "editButtonkisi";
            this.editButtonkisi.NewValue = "";
            this.editButtonkisi.OldValue = "";
            this.editButtonkisi.ReadOnly = false;
            this.editButtonkisi.Size = new System.Drawing.Size(387, 21);
            this.editButtonkisi.TabIndex = 1;
            // 
            // panelgridler
            // 
            this.panelgridler.Controls.Add(this.gridControlHastaDetaylari);
            this.panelgridler.Controls.Add(this.splitterControl3);
            this.panelgridler.Controls.Add(this.gridControlHastaliklar);
            this.panelgridler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelgridler.Location = new System.Drawing.Point(0, 146);
            this.panelgridler.Name = "panelgridler";
            this.panelgridler.Size = new System.Drawing.Size(880, 303);
            this.panelgridler.TabIndex = 1;
            // 
            // gridControlHastaDetaylari
            // 
            this.gridControlHastaDetaylari.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlHastaDetaylari.Location = new System.Drawing.Point(443, 0);
            this.gridControlHastaDetaylari.MainView = this.gridViewhastadetay;
            this.gridControlHastaDetaylari.Name = "gridControlHastaDetaylari";
            this.gridControlHastaDetaylari.Size = new System.Drawing.Size(437, 303);
            this.gridControlHastaDetaylari.TabIndex = 1;
            this.gridControlHastaDetaylari.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewhastadetay});
            // 
            // gridViewhastadetay
            // 
            this.gridViewhastadetay.GridControl = this.gridControlHastaDetaylari;
            this.gridViewhastadetay.Name = "gridViewhastadetay";
            this.gridViewhastadetay.OptionsView.ColumnAutoWidth = false;
            this.gridViewhastadetay.OptionsView.ShowViewCaption = true;
            this.gridViewhastadetay.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gridViewhastadetay.ViewCaption = "Hasta Detayları";
            // 
            // splitterControl3
            // 
            this.splitterControl3.Location = new System.Drawing.Point(437, 0);
            this.splitterControl3.Name = "splitterControl3";
            this.splitterControl3.Size = new System.Drawing.Size(6, 303);
            this.splitterControl3.TabIndex = 13;
            this.splitterControl3.TabStop = false;
            // 
            // gridControlHastaliklar
            // 
            this.gridControlHastaliklar.Dock = System.Windows.Forms.DockStyle.Left;
            this.gridControlHastaliklar.Location = new System.Drawing.Point(0, 0);
            this.gridControlHastaliklar.MainView = this.gridViewhastaliklar;
            this.gridControlHastaliklar.Name = "gridControlHastaliklar";
            this.gridControlHastaliklar.Size = new System.Drawing.Size(437, 303);
            this.gridControlHastaliklar.TabIndex = 0;
            this.gridControlHastaliklar.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewhastaliklar});
            // 
            // gridViewhastaliklar
            // 
            this.gridViewhastaliklar.GridControl = this.gridControlHastaliklar;
            this.gridViewhastaliklar.Name = "gridViewhastaliklar";
            this.gridViewhastaliklar.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewhastaliklar.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewhastaliklar.OptionsBehavior.Editable = false;
            this.gridViewhastaliklar.OptionsBehavior.ReadOnly = true;
            this.gridViewhastaliklar.OptionsView.ColumnAutoWidth = false;
            this.gridViewhastaliklar.OptionsView.ShowViewCaption = true;
            this.gridViewhastaliklar.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gridViewhastaliklar.ViewCaption = "ICD10 Genel Toplam";
            this.gridViewhastaliklar.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewhastaliklar_FocusedRowChanged);
            // 
            // frmICD10Raporu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 449);
            this.Controls.Add(this.panelgridler);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmICD10Raporu";
            this.Text = "ICD10 Raporlaması";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDogumBitTarih.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDogumBitTarih.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDogumBasTarih.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDogumBasTarih.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBitTarih.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBitTarih.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBasTarih.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBasTarih.Properties)).EndInit();
            this.panelgridler.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlHastaDetaylari)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewhastadetay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlHastaliklar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewhastaliklar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private EditButton editButtonTeshis;
        private EditButton editButtonkisi;
        private System.Windows.Forms.Panel panelgridler;
        private DevExpress.XtraGrid.GridControl gridControlHastaDetaylari;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewhastadetay;
        private DevExpress.XtraGrid.GridControl gridControlHastaliklar;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewhastaliklar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGotur;
        private System.Windows.Forms.Button btnGetir;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.DateEdit dateEditDogumBitTarih;
        private DevExpress.XtraEditors.DateEdit dateEditDogumBasTarih;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dateEditBitTarih;
        private DevExpress.XtraEditors.DateEdit dateEditBasTarih;
        private DevExpress.XtraEditors.SplitterControl splitterControl3;
    }
}