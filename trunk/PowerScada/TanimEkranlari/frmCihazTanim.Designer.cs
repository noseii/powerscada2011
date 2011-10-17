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
            this.GridAdresler = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Ustpanel = new System.Windows.Forms.Panel();
            this.editButtonCihazTuru = new PowerScada.EditButton();
            this.labelControlSablonAdi = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.textEditAdi = new DevExpress.XtraEditors.TextEdit();
            this.labelControlSablonTuru = new DevExpress.XtraEditors.LabelControl();
            this.textEditkodu = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.memoEditAciklama = new DevExpress.XtraEditors.MemoEdit();
            this.myComboDavranis = new PowerScada.MyCombo();
            ((System.ComponentModel.ISupportInitialize)(this.SaveInformationexpando)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelust)).BeginInit();
            this.panelust.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridAdresler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
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
            this.panelust.Controls.Add(this.GridAdresler);
            this.panelust.Controls.Add(this.Ustpanel);
            this.panelust.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelust.Location = new System.Drawing.Point(0, 0);
            this.panelust.Name = "panelust";
            this.panelust.Size = new System.Drawing.Size(810, 545);
            this.panelust.TabIndex = 2;
            // 
            // GridAdresler
            // 
            this.GridAdresler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridAdresler.Location = new System.Drawing.Point(2, 252);
            this.GridAdresler.MainView = this.gridView1;
            this.GridAdresler.Name = "GridAdresler";
            this.GridAdresler.Size = new System.Drawing.Size(806, 291);
            this.GridAdresler.TabIndex = 24;
            this.GridAdresler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.GridAdresler.Validating += new System.ComponentModel.CancelEventHandler(this.GridAdresler_Validating);
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
            // Ustpanel
            // 
            this.Ustpanel.Controls.Add(this.editButtonCihazTuru);
            this.Ustpanel.Controls.Add(this.labelControlSablonAdi);
            this.Ustpanel.Controls.Add(this.labelControl3);
            this.Ustpanel.Controls.Add(this.textEditAdi);
            this.Ustpanel.Controls.Add(this.labelControlSablonTuru);
            this.Ustpanel.Controls.Add(this.textEditkodu);
            this.Ustpanel.Controls.Add(this.labelControl1);
            this.Ustpanel.Controls.Add(this.labelControl2);
            this.Ustpanel.Controls.Add(this.memoEditAciklama);
            this.Ustpanel.Controls.Add(this.myComboDavranis);
            this.Ustpanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.Ustpanel.Location = new System.Drawing.Point(2, 2);
            this.Ustpanel.Name = "Ustpanel";
            this.Ustpanel.Size = new System.Drawing.Size(806, 250);
            this.Ustpanel.TabIndex = 23;
            // 
            // editButtonCihazTuru
            // 
            this.editButtonCihazTuru.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editButtonCihazTuru.BackColor = System.Drawing.SystemColors.Window;
            this.editButtonCihazTuru.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editButtonCihazTuru.CommandName = "EditButtonCihazTuru";
            this.editButtonCihazTuru.Id = ((long)(0));
            this.editButtonCihazTuru.Location = new System.Drawing.Point(103, 10);
            this.editButtonCihazTuru.Name = "editButtonCihazTuru";
            this.editButtonCihazTuru.NewValue = "";
            this.editButtonCihazTuru.OldValue = "";
            this.editButtonCihazTuru.ReadOnly = false;
            this.editButtonCihazTuru.Size = new System.Drawing.Size(202, 21);
            this.editButtonCihazTuru.TabIndex = 1;
            // 
            // labelControlSablonAdi
            // 
            this.labelControlSablonAdi.Location = new System.Drawing.Point(19, 59);
            this.labelControlSablonAdi.Name = "labelControlSablonAdi";
            this.labelControlSablonAdi.Size = new System.Drawing.Size(15, 13);
            this.labelControlSablonAdi.TabIndex = 4;
            this.labelControlSablonAdi.Text = "Adı";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(18, 18);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(51, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Cihaz Türü";
            // 
            // textEditAdi
            // 
            this.textEditAdi.Location = new System.Drawing.Point(103, 63);
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
            this.textEditkodu.Location = new System.Drawing.Point(103, 37);
            this.textEditkodu.Name = "textEditkodu";
            this.textEditkodu.Size = new System.Drawing.Size(197, 20);
            this.textEditkodu.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(18, 127);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(41, 13);
            this.labelControl1.TabIndex = 21;
            this.labelControl1.Text = "Açıklama";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(18, 95);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(42, 13);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "Davranış";
            // 
            // memoEditAciklama
            // 
            this.memoEditAciklama.Location = new System.Drawing.Point(103, 121);
            this.memoEditAciklama.Name = "memoEditAciklama";
            this.memoEditAciklama.Size = new System.Drawing.Size(549, 76);
            this.memoEditAciklama.TabIndex = 22;
            // 
            // myComboDavranis
            // 
            this.myComboDavranis.BindingTürü = PowerScada.MyCombo.Binding.Enum;
            this.myComboDavranis.DisplayField = "Ad";
            this.myComboDavranis.EmptyMessage = "";
            this.myComboDavranis.EmptyRow = false;
            this.myComboDavranis.EntityName = "";
            this.myComboDavranis.EnumTipi = "Davranis";
            this.myComboDavranis.Id = 0;
            this.myComboDavranis.Location = new System.Drawing.Point(103, 89);
            this.myComboDavranis.Name = "myComboDavranis";
            this.myComboDavranis.SelectedIndex = 0;
            this.myComboDavranis.Size = new System.Drawing.Size(197, 21);
            this.myComboDavranis.TabIndex = 7;
            this.myComboDavranis.WhereClause = null;
            // 
            // frmCihazTanim
            // 
            this.ClientSize = new System.Drawing.Size(983, 545);
            this.Name = "frmCihazTanim";
            ((System.ComponentModel.ISupportInitialize)(this.SaveInformationexpando)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelust)).EndInit();
            this.panelust.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridAdresler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
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
        private MyCombo myComboDavranis;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private EditButton editButtonCihazTuru;
        private System.Windows.Forms.Panel Ustpanel;
        private DevExpress.XtraGrid.GridControl GridAdresler;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;

    }
}
