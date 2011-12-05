using PowerScada;
namespace PowerScada
{
    partial class frmCihazTanim
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
            this.panelust = new DevExpress.XtraEditors.PanelControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPageCihazAdres = new DevExpress.XtraTab.XtraTabPage();
            this.GridAdresler = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.xtraTabPageAlarm = new DevExpress.XtraTab.XtraTabPage();
            this.gridAlarmAdresler = new DevExpress.XtraGrid.GridControl();
            this.gridViewAlarmAdresler = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Ustpanel = new System.Windows.Forms.Panel();
            this.editButtonLokasyon = new PowerScada.EditButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.editButtonCihazTuru = new PowerScada.EditButton();
            this.labelControlSablonAdi = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.textEditAdi = new DevExpress.XtraEditors.TextEdit();
            this.labelControlSablonTuru = new DevExpress.XtraEditors.LabelControl();
            this.textEditkodu = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.memoEditAciklama = new DevExpress.XtraEditors.MemoEdit();
            this.myComboCihazTuru = new PowerScada.MyCombo();
            ((System.ComponentModel.ISupportInitialize)(this.SaveInformationexpando)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelust)).BeginInit();
            this.panelust.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPageCihazAdres.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridAdresler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.xtraTabPageAlarm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridAlarmAdresler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewAlarmAdresler)).BeginInit();
            this.Ustpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditAdi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditkodu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditAciklama.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // SaveInformationexpando
            // 
            this.SaveInformationexpando.CustomHeaderSettings.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelust);
            this.panel1.Size = new System.Drawing.Size(810, 545);
            // 
            // panelust
            // 
            this.panelust.Controls.Add(this.xtraTabControl1);
            this.panelust.Controls.Add(this.Ustpanel);
            this.panelust.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelust.Location = new System.Drawing.Point(0, 0);
            this.panelust.Name = "panelust";
            this.panelust.Size = new System.Drawing.Size(810, 545);
            this.panelust.TabIndex = 2;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(2, 256);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPageCihazAdres;
            this.xtraTabControl1.Size = new System.Drawing.Size(806, 287);
            this.xtraTabControl1.TabIndex = 25;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageCihazAdres,
            this.xtraTabPageAlarm});
            // 
            // xtraTabPageCihazAdres
            // 
            this.xtraTabPageCihazAdres.Controls.Add(this.GridAdresler);
            this.xtraTabPageCihazAdres.Name = "xtraTabPageCihazAdres";
            this.xtraTabPageCihazAdres.Size = new System.Drawing.Size(799, 258);
            this.xtraTabPageCihazAdres.Text = "Cihaz Adresleri";
            // 
            // GridAdresler
            // 
            this.GridAdresler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridAdresler.Location = new System.Drawing.Point(0, 0);
            this.GridAdresler.MainView = this.gridView1;
            this.GridAdresler.Name = "GridAdresler";
            this.GridAdresler.Size = new System.Drawing.Size(799, 258);
            this.GridAdresler.TabIndex = 24;
            this.GridAdresler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.GridAdresler;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gridView1.OptionsNavigation.AutoFocusNewRow = true;
            this.gridView1.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridView1_KeyDown);
            // 
            // xtraTabPageAlarm
            // 
            this.xtraTabPageAlarm.Controls.Add(this.gridAlarmAdresler);
            this.xtraTabPageAlarm.Name = "xtraTabPageAlarm";
            this.xtraTabPageAlarm.Size = new System.Drawing.Size(799, 258);
            this.xtraTabPageAlarm.Text = "Alarm Ayarlari";
            // 
            // gridAlarmAdresler
            // 
            this.gridAlarmAdresler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridAlarmAdresler.Location = new System.Drawing.Point(0, 0);
            this.gridAlarmAdresler.MainView = this.gridViewAlarmAdresler;
            this.gridAlarmAdresler.Name = "gridAlarmAdresler";
            this.gridAlarmAdresler.Size = new System.Drawing.Size(799, 258);
            this.gridAlarmAdresler.TabIndex = 25;
            this.gridAlarmAdresler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewAlarmAdresler});
            // 
            // gridViewAlarmAdresler
            // 
            this.gridViewAlarmAdresler.GridControl = this.gridAlarmAdresler;
            this.gridViewAlarmAdresler.Name = "gridViewAlarmAdresler";
            this.gridViewAlarmAdresler.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewAlarmAdresler.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewAlarmAdresler.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gridViewAlarmAdresler.OptionsNavigation.AutoFocusNewRow = true;
            this.gridViewAlarmAdresler.OptionsSelection.MultiSelect = true;
            this.gridViewAlarmAdresler.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gridViewAlarmAdresler.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridView1_KeyDown);
            // 
            // Ustpanel
            // 
            this.Ustpanel.Controls.Add(this.editButtonLokasyon);
            this.Ustpanel.Controls.Add(this.labelControl4);
            this.Ustpanel.Controls.Add(this.editButtonCihazTuru);
            this.Ustpanel.Controls.Add(this.labelControlSablonAdi);
            this.Ustpanel.Controls.Add(this.labelControl3);
            this.Ustpanel.Controls.Add(this.textEditAdi);
            this.Ustpanel.Controls.Add(this.labelControlSablonTuru);
            this.Ustpanel.Controls.Add(this.textEditkodu);
            this.Ustpanel.Controls.Add(this.labelControl1);
            this.Ustpanel.Controls.Add(this.labelControl2);
            this.Ustpanel.Controls.Add(this.memoEditAciklama);
            this.Ustpanel.Controls.Add(this.myComboCihazTuru);
            this.Ustpanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.Ustpanel.Location = new System.Drawing.Point(2, 2);
            this.Ustpanel.Name = "Ustpanel";
            this.Ustpanel.Size = new System.Drawing.Size(806, 254);
            this.Ustpanel.TabIndex = 23;
            // 
            // editButtonLokasyon
            // 
            this.editButtonLokasyon.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editButtonLokasyon.BackColor = System.Drawing.SystemColors.Window;
            this.editButtonLokasyon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editButtonLokasyon.CommandName = "EditButtonLokasyon";
            this.editButtonLokasyon.Id = ((long)(0));
            this.editButtonLokasyon.Location = new System.Drawing.Point(103, 148);
            this.editButtonLokasyon.Name = "editButtonLokasyon";
            this.editButtonLokasyon.NewValue = "";
            this.editButtonLokasyon.OldValue = "";
            this.editButtonLokasyon.ReadOnly = false;
            this.editButtonLokasyon.Size = new System.Drawing.Size(197, 21);
            this.editButtonLokasyon.TabIndex = 24;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(19, 153);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(45, 13);
            this.labelControl4.TabIndex = 23;
            this.labelControl4.Text = "Lokasyon";
            // 
            // editButtonCihazTuru
            // 
            this.editButtonCihazTuru.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editButtonCihazTuru.BackColor = System.Drawing.SystemColors.Window;
            this.editButtonCihazTuru.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editButtonCihazTuru.CommandName = "EditButtonCihazTuru";
            this.editButtonCihazTuru.Id = ((long)(0));
            this.editButtonCihazTuru.Location = new System.Drawing.Point(103, 118);
            this.editButtonCihazTuru.Name = "editButtonCihazTuru";
            this.editButtonCihazTuru.NewValue = "";
            this.editButtonCihazTuru.OldValue = "";
            this.editButtonCihazTuru.ReadOnly = false;
            this.editButtonCihazTuru.Size = new System.Drawing.Size(197, 21);
            this.editButtonCihazTuru.TabIndex = 1;
            // 
            // labelControlSablonAdi
            // 
            this.labelControlSablonAdi.Location = new System.Drawing.Point(19, 66);
            this.labelControlSablonAdi.Name = "labelControlSablonAdi";
            this.labelControlSablonAdi.Size = new System.Drawing.Size(15, 13);
            this.labelControlSablonAdi.TabIndex = 4;
            this.labelControlSablonAdi.Text = "Adı";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(19, 124);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(59, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Cihaz Modeli";
            // 
            // textEditAdi
            // 
            this.textEditAdi.Location = new System.Drawing.Point(103, 59);
            this.textEditAdi.Name = "textEditAdi";
            this.textEditAdi.Size = new System.Drawing.Size(197, 20);
            this.textEditAdi.TabIndex = 5;
            // 
            // labelControlSablonTuru
            // 
            this.labelControlSablonTuru.Location = new System.Drawing.Point(19, 37);
            this.labelControlSablonTuru.Name = "labelControlSablonTuru";
            this.labelControlSablonTuru.Size = new System.Drawing.Size(24, 13);
            this.labelControlSablonTuru.TabIndex = 2;
            this.labelControlSablonTuru.Text = "Kodu";
            // 
            // textEditkodu
            // 
            this.textEditkodu.Location = new System.Drawing.Point(103, 30);
            this.textEditkodu.Name = "textEditkodu";
            this.textEditkodu.Size = new System.Drawing.Size(197, 20);
            this.textEditkodu.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(19, 182);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(41, 13);
            this.labelControl1.TabIndex = 21;
            this.labelControl1.Text = "Açıklama";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(19, 95);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(45, 13);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "Cihaz Tipi";
            // 
            // memoEditAciklama
            // 
            this.memoEditAciklama.Location = new System.Drawing.Point(103, 178);
            this.memoEditAciklama.Name = "memoEditAciklama";
            this.memoEditAciklama.Size = new System.Drawing.Size(549, 67);
            this.memoEditAciklama.TabIndex = 22;
            // 
            // myComboCihazTuru
            // 
            this.myComboCihazTuru.BindingTürü = PowerScada.MyCombo.Binding.Enum;
            this.myComboCihazTuru.DisplayField = "Ad";
            this.myComboCihazTuru.EmptyMessage = "";
            this.myComboCihazTuru.EmptyRow = false;
            this.myComboCihazTuru.EntityName = "";
            this.myComboCihazTuru.EnumTipi = "CihazTuru";
            this.myComboCihazTuru.Id = 1;
            this.myComboCihazTuru.Location = new System.Drawing.Point(103, 88);
            this.myComboCihazTuru.Name = "myComboCihazTuru";
            this.myComboCihazTuru.OldId = 0;
            this.myComboCihazTuru.SelectedIndex = 0;
            this.myComboCihazTuru.Size = new System.Drawing.Size(197, 21);
            this.myComboCihazTuru.TabIndex = 7;
            this.myComboCihazTuru.WhereClause = null;
            // 
            // frmCihazTanim
            // 
            this.ClientSize = new System.Drawing.Size(983, 545);
            this.Name = "frmCihazTanim";
            ((System.ComponentModel.ISupportInitialize)(this.SaveInformationexpando)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelust)).EndInit();
            this.panelust.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPageCihazAdres.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridAdresler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.xtraTabPageAlarm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridAlarmAdresler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewAlarmAdresler)).EndInit();
            this.Ustpanel.ResumeLayout(false);
            this.Ustpanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditAdi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditkodu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditAciklama.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelust;
        private DevExpress.XtraEditors.LabelControl labelControlSablonTuru;
        private DevExpress.XtraEditors.LabelControl labelControlSablonAdi;
        private DevExpress.XtraEditors.TextEdit textEditAdi;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit textEditkodu;
        private DevExpress.XtraEditors.MemoEdit memoEditAciklama;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private MyCombo myComboCihazTuru;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private EditButton editButtonCihazTuru;
        private System.Windows.Forms.Panel Ustpanel;
        private DevExpress.XtraGrid.GridControl GridAdresler;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private EditButton editButtonLokasyon;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageCihazAdres;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageAlarm;
        private DevExpress.XtraGrid.GridControl gridAlarmAdresler;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewAlarmAdresler;

    }
}
