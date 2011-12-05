namespace AHBS2010
{
    partial class frmHastaBul
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
            this.Ustpanel = new DevExpress.XtraEditors.PanelControl();
            this.superTextBoxSoyadi = new AHBS2010.SuperTextBox();
            this.superTextBoxAdi = new AHBS2010.SuperTextBox();
            this.superTextBoxTckimlikno = new AHBS2010.SuperTextBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlTckimlikNo = new DevExpress.XtraEditors.LabelControl();
            this.labelControlAdi = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.tumhastalarBtn = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.edtmisafir = new DevExpress.XtraEditors.CheckEdit();
            this.edtkayitli = new DevExpress.XtraEditors.CheckEdit();
            this.edtkayitdurumtum = new DevExpress.XtraEditors.CheckEdit();
            this.Altpanel = new DevExpress.XtraEditors.PanelControl();
            this.grdhasta = new DevExpress.XtraGrid.GridControl();
            this.gridViewHasta = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cardViewhasta = new DevExpress.XtraGrid.Views.Card.CardView();
            ((System.ComponentModel.ISupportInitialize)(this.Ustpanel)).BeginInit();
            this.Ustpanel.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtmisafir.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtkayitli.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtkayitdurumtum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Altpanel)).BeginInit();
            this.Altpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdhasta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewHasta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardViewhasta)).BeginInit();
            this.SuspendLayout();
            // 
            // Ustpanel
            // 
            this.Ustpanel.Controls.Add(this.superTextBoxSoyadi);
            this.Ustpanel.Controls.Add(this.superTextBoxAdi);
            this.Ustpanel.Controls.Add(this.superTextBoxTckimlikno);
            this.Ustpanel.Controls.Add(this.labelControl2);
            this.Ustpanel.Controls.Add(this.labelControlTckimlikNo);
            this.Ustpanel.Controls.Add(this.labelControlAdi);
            this.Ustpanel.Controls.Add(this.simpleButton2);
            this.Ustpanel.Controls.Add(this.tumhastalarBtn);
            this.Ustpanel.Controls.Add(this.simpleButton1);
            this.Ustpanel.Controls.Add(this.groupBox3);
            this.Ustpanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.Ustpanel.Location = new System.Drawing.Point(0, 0);
            this.Ustpanel.Name = "Ustpanel";
            this.Ustpanel.Size = new System.Drawing.Size(1004, 80);
            this.Ustpanel.TabIndex = 0;
            // 
            // superTextBoxSoyadi
            // 
            this.superTextBoxSoyadi.AutoSize = true;
            this.superTextBoxSoyadi.Format = AHBS2010.Formati.Text;
            this.superTextBoxSoyadi.GetFocusColor = System.Drawing.Color.Empty;
            this.superTextBoxSoyadi.GotFocusColor = System.Drawing.Color.Empty;
            this.superTextBoxSoyadi.Location = new System.Drawing.Point(446, 51);
            this.superTextBoxSoyadi.Name = "superTextBoxSoyadi";
            this.superTextBoxSoyadi.Size = new System.Drawing.Size(150, 20);
            this.superTextBoxSoyadi.TabIndex = 31;
            // 
            // superTextBoxAdi
            // 
            this.superTextBoxAdi.AutoSize = true;
            this.superTextBoxAdi.Format = AHBS2010.Formati.Text;
            this.superTextBoxAdi.GetFocusColor = System.Drawing.Color.Empty;
            this.superTextBoxAdi.GotFocusColor = System.Drawing.Color.Empty;
            this.superTextBoxAdi.Location = new System.Drawing.Point(250, 51);
            this.superTextBoxAdi.Name = "superTextBoxAdi";
            this.superTextBoxAdi.Size = new System.Drawing.Size(134, 20);
            this.superTextBoxAdi.TabIndex = 30;
            // 
            // superTextBoxTckimlikno
            // 
            this.superTextBoxTckimlikno.AutoSize = true;
            this.superTextBoxTckimlikno.Format = AHBS2010.Formati.Sayisal;
            this.superTextBoxTckimlikno.GetFocusColor = System.Drawing.Color.Empty;
            this.superTextBoxTckimlikno.GotFocusColor = System.Drawing.Color.Empty;
            this.superTextBoxTckimlikno.Location = new System.Drawing.Point(95, 51);
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
            this.labelControl2.Location = new System.Drawing.Point(396, 58);
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
            this.labelControlTckimlikNo.Location = new System.Drawing.Point(12, 58);
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
            this.labelControlAdi.Location = new System.Drawing.Point(220, 58);
            this.labelControlAdi.Name = "labelControlAdi";
            this.labelControlAdi.Size = new System.Drawing.Size(24, 13);
            this.labelControlAdi.TabIndex = 26;
            this.labelControlAdi.Text = "Adı :";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(814, 48);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(100, 23);
            this.simpleButton2.TabIndex = 8;
            this.simpleButton2.Text = "&Kapat";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // tumhastalarBtn
            // 
            this.tumhastalarBtn.Location = new System.Drawing.Point(602, 48);
            this.tumhastalarBtn.Name = "tumhastalarBtn";
            this.tumhastalarBtn.Size = new System.Drawing.Size(100, 23);
            this.tumhastalarBtn.TabIndex = 7;
            this.tumhastalarBtn.Text = "&Tüm Hastalar";
            this.tumhastalarBtn.Click += new System.EventHandler(this.tumhastalarBtn_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(708, 48);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(100, 23);
            this.simpleButton1.TabIndex = 6;
            this.simpleButton1.Text = "&Yeni Hasta";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.edtmisafir);
            this.groupBox3.Controls.Add(this.edtkayitli);
            this.groupBox3.Controls.Add(this.edtkayitdurumtum);
            this.groupBox3.Location = new System.Drawing.Point(12, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(980, 40);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Hasta Kayıt Durumu";
            // 
            // edtmisafir
            // 
            this.edtmisafir.Enabled = false;
            this.edtmisafir.Location = new System.Drawing.Point(126, 15);
            this.edtmisafir.Name = "edtmisafir";
            this.edtmisafir.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.edtmisafir.Properties.Appearance.Options.UseBackColor = true;
            this.edtmisafir.Properties.Caption = "Misafir";
            this.edtmisafir.Size = new System.Drawing.Size(54, 19);
            this.edtmisafir.TabIndex = 2;
            // 
            // edtkayitli
            // 
            this.edtkayitli.Enabled = false;
            this.edtkayitli.Location = new System.Drawing.Point(66, 16);
            this.edtkayitli.Name = "edtkayitli";
            this.edtkayitli.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.edtkayitli.Properties.Appearance.Options.UseBackColor = true;
            this.edtkayitli.Properties.Caption = "Kayıtlı";
            this.edtkayitli.Size = new System.Drawing.Size(78, 19);
            this.edtkayitli.TabIndex = 1;
            // 
            // edtkayitdurumtum
            // 
            this.edtkayitdurumtum.EditValue = true;
            this.edtkayitdurumtum.Location = new System.Drawing.Point(3, 16);
            this.edtkayitdurumtum.Name = "edtkayitdurumtum";
            this.edtkayitdurumtum.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.edtkayitdurumtum.Properties.Appearance.Options.UseBackColor = true;
            this.edtkayitdurumtum.Properties.Caption = "Tümü";
            this.edtkayitdurumtum.Size = new System.Drawing.Size(63, 19);
            this.edtkayitdurumtum.TabIndex = 0;
            this.edtkayitdurumtum.CheckedChanged += new System.EventHandler(this.edtkayitdurumtum_CheckedChanged);
            // 
            // Altpanel
            // 
            this.Altpanel.Controls.Add(this.grdhasta);
            this.Altpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Altpanel.Location = new System.Drawing.Point(0, 80);
            this.Altpanel.Name = "Altpanel";
            this.Altpanel.Size = new System.Drawing.Size(1004, 460);
            this.Altpanel.TabIndex = 1;
            // 
            // grdhasta
            // 
            this.grdhasta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdhasta.EmbeddedNavigator.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.grdhasta.EmbeddedNavigator.Appearance.Options.UseFont = true;
            this.grdhasta.EmbeddedNavigator.TextStringFormat = "Kayıt {0} / {1}";
            this.grdhasta.Location = new System.Drawing.Point(2, 2);
            this.grdhasta.MainView = this.gridViewHasta;
            this.grdhasta.Name = "grdhasta";
            this.grdhasta.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdhasta.Size = new System.Drawing.Size(1000, 456);
            this.grdhasta.TabIndex = 1;
            this.grdhasta.Tag = "Hasta";
            this.grdhasta.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewHasta,
            this.cardViewhasta});
            this.grdhasta.DoubleClick += new System.EventHandler(this.grdhasta_DoubleClick);
            // 
            // gridViewHasta
            // 
            this.gridViewHasta.GridControl = this.grdhasta;
            this.gridViewHasta.Name = "gridViewHasta";
            this.gridViewHasta.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewHasta.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewHasta.OptionsBehavior.Editable = false;
            this.gridViewHasta.OptionsBehavior.ReadOnly = true;
            this.gridViewHasta.OptionsView.ShowGroupPanel = false;
            this.gridViewHasta.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never;
            this.gridViewHasta.ViewCaption = "Hastalar";
            // 
            // cardViewhasta
            // 
            this.cardViewhasta.FocusedCardTopFieldIndex = 0;
            this.cardViewhasta.GridControl = this.grdhasta;
            this.cardViewhasta.Name = "cardViewhasta";
            this.cardViewhasta.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.cardViewhasta.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.cardViewhasta.OptionsBehavior.ReadOnly = true;
            this.cardViewhasta.Tag = "Hasta";
            this.cardViewhasta.ViewCaption = "Hastalar";
            // 
            // frmHastaBul
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 540);
            this.Controls.Add(this.Altpanel);
            this.Controls.Add(this.Ustpanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmHastaBul";
            this.Text = "Hasta Arama ";
            ((System.ComponentModel.ISupportInitialize)(this.Ustpanel)).EndInit();
            this.Ustpanel.ResumeLayout(false);
            this.Ustpanel.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.edtmisafir.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtkayitli.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtkayitdurumtum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Altpanel)).EndInit();
            this.Altpanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdhasta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewHasta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardViewhasta)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl Ustpanel;
        private DevExpress.XtraEditors.PanelControl Altpanel;
        public DevExpress.XtraGrid.GridControl grdhasta;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewHasta;
        public DevExpress.XtraGrid.Views.Card.CardView cardViewhasta;
        private System.Windows.Forms.GroupBox groupBox3;
        public DevExpress.XtraEditors.CheckEdit edtmisafir;
        public DevExpress.XtraEditors.CheckEdit edtkayitli;
        public DevExpress.XtraEditors.CheckEdit edtkayitdurumtum;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton tumhastalarBtn;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private SuperTextBox superTextBoxAdi;
        private SuperTextBox superTextBoxTckimlikno;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControlTckimlikNo;
        private DevExpress.XtraEditors.LabelControl labelControlAdi;
        private SuperTextBox superTextBoxSoyadi;
    }
}