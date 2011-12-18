namespace PowerScada
{
    partial class frmBase
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBase));
            this.panelbutton = new DevExpress.XtraEditors.PanelControl();
            this.dbnav = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnYeni = new System.Windows.Forms.ToolStripButton();
            this.btnYenile = new System.Windows.Forms.ToolStripButton();
            this.btnKaydet = new System.Windows.Forms.ToolStripButton();
            this.btnIptal = new System.Windows.Forms.ToolStripButton();
            this.btnDuzenle = new System.Windows.Forms.ToolStripButton();
            this.btnVazgec = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGeri = new System.Windows.Forms.ToolStripButton();
            this.btnIleri = new System.Windows.Forms.ToolStripButton();
            this.BtnSistemBilgileri = new System.Windows.Forms.ToolStripButton();
            this.paneldetay = new DevExpress.XtraEditors.PanelControl();
            this.paneldetayfill = new DevExpress.XtraEditors.PanelControl();
            this.paneldetaybottom = new DevExpress.XtraEditors.PanelControl();
            this.paneldetaytop = new DevExpress.XtraEditors.PanelControl();
            this.paneldetayleft = new DevExpress.XtraEditors.PanelControl();
            this.sp = new DevExpress.XtraEditors.SplitterControl();
            this.formbs = new System.Windows.Forms.BindingSource(this.components);
            this.pnlgrd = new DevExpress.XtraEditors.PanelControl();
            this.grpgrd = new DevExpress.XtraEditors.GroupControl();
            this.grd = new DevExpress.XtraGrid.GridControl();
            this.grdv = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.panelbutton)).BeginInit();
            this.panelbutton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dbnav)).BeginInit();
            this.dbnav.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paneldetay)).BeginInit();
            this.paneldetay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paneldetayfill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paneldetaybottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paneldetaytop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paneldetayleft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.formbs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlgrd)).BeginInit();
            this.pnlgrd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpgrd)).BeginInit();
            this.grpgrd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdv)).BeginInit();
            this.SuspendLayout();
            // 
            // panelbutton
            // 
            this.panelbutton.Appearance.BackColor = System.Drawing.Color.White;
            this.panelbutton.Appearance.Options.UseBackColor = true;
            this.panelbutton.Controls.Add(this.dbnav);
            this.panelbutton.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelbutton.Location = new System.Drawing.Point(0, 0);
            this.panelbutton.Name = "panelbutton";
            this.panelbutton.Size = new System.Drawing.Size(947, 49);
            this.panelbutton.TabIndex = 6;
            // 
            // dbnav
            // 
            this.dbnav.AddNewItem = null;
            this.dbnav.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.dbnav.CountItem = this.bindingNavigatorCountItem;
            this.dbnav.DeleteItem = null;
            this.dbnav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbnav.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.btnYeni,
            this.btnYenile,
            this.btnKaydet,
            this.btnIptal,
            this.btnDuzenle,
            this.btnVazgec,
            this.toolStripSeparator1,
            this.btnGeri,
            this.btnIleri,
            this.BtnSistemBilgileri});
            this.dbnav.Location = new System.Drawing.Point(2, 2);
            this.dbnav.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.dbnav.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.dbnav.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.dbnav.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.dbnav.Name = "dbnav";
            this.dbnav.PositionItem = this.bindingNavigatorPositionItem;
            this.dbnav.Size = new System.Drawing.Size(943, 45);
            this.dbnav.TabIndex = 0;
            this.dbnav.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 42);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 42);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 42);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 45);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 21);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 45);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 42);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 42);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 45);
            // 
            // btnYeni
            // 
            this.btnYeni.AutoSize = false;
            this.btnYeni.Image = ((System.Drawing.Image)(resources.GetObject("btnYeni.Image")));
            this.btnYeni.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.btnYeni.Name = "btnYeni";
            this.btnYeni.RightToLeftAutoMirrorImage = true;
            this.btnYeni.Size = new System.Drawing.Size(70, 22);
            this.btnYeni.Text = "Yeni";
            this.btnYeni.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.btnYeni.ToolTipText = "Ctrl+Insert";
            // 
            // btnYenile
            // 
            this.btnYenile.AutoSize = false;
            this.btnYenile.Image = ((System.Drawing.Image)(resources.GetObject("btnYenile.Image")));
            this.btnYenile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnYenile.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.btnYenile.Name = "btnYenile";
            this.btnYenile.Size = new System.Drawing.Size(70, 22);
            this.btnYenile.Text = "Yenile";
            this.btnYenile.ToolTipText = "F5";
            // 
            // btnKaydet
            // 
            this.btnKaydet.AutoSize = false;
            this.btnKaydet.Image = ((System.Drawing.Image)(resources.GetObject("btnKaydet.Image")));
            this.btnKaydet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnKaydet.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(80, 22);
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.ToolTipText = "F2";
            // 
            // btnIptal
            // 
            this.btnIptal.AutoSize = false;
            this.btnIptal.Image = ((System.Drawing.Image)(resources.GetObject("btnIptal.Image")));
            this.btnIptal.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.RightToLeftAutoMirrorImage = true;
            this.btnIptal.Size = new System.Drawing.Size(70, 22);
            this.btnIptal.Text = "İptal";
            this.btnIptal.ToolTipText = "Ctrl+Delete";
            // 
            // btnDuzenle
            // 
            this.btnDuzenle.AutoSize = false;
            this.btnDuzenle.Image = ((System.Drawing.Image)(resources.GetObject("btnDuzenle.Image")));
            this.btnDuzenle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDuzenle.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.btnDuzenle.Name = "btnDuzenle";
            this.btnDuzenle.Size = new System.Drawing.Size(90, 22);
            this.btnDuzenle.Text = "Düzenle";
            this.btnDuzenle.ToolTipText = "F4";
            // 
            // btnVazgec
            // 
            this.btnVazgec.AutoSize = false;
            this.btnVazgec.Image = ((System.Drawing.Image)(resources.GetObject("btnVazgec.Image")));
            this.btnVazgec.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnVazgec.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.btnVazgec.Name = "btnVazgec";
            this.btnVazgec.Size = new System.Drawing.Size(80, 22);
            this.btnVazgec.Text = "Vazgeç";
            this.btnVazgec.ToolTipText = "Ctrl+Esc";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 45);
            // 
            // btnGeri
            // 
            this.btnGeri.AutoSize = false;
            this.btnGeri.Image = ((System.Drawing.Image)(resources.GetObject("btnGeri.Image")));
            this.btnGeri.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGeri.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGeri.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.btnGeri.Name = "btnGeri";
            this.btnGeri.Size = new System.Drawing.Size(60, 22);
            this.btnGeri.Text = "Geri";
            this.btnGeri.ToolTipText = "Ctrl+Sol";
            // 
            // btnIleri
            // 
            this.btnIleri.AutoSize = false;
            this.btnIleri.Image = ((System.Drawing.Image)(resources.GetObject("btnIleri.Image")));
            this.btnIleri.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIleri.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIleri.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.btnIleri.Name = "btnIleri";
            this.btnIleri.Size = new System.Drawing.Size(60, 22);
            this.btnIleri.Text = "İleri";
            this.btnIleri.ToolTipText = "Ctrl+Sağ";
            // 
            // BtnSistemBilgileri
            // 
            this.BtnSistemBilgileri.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BtnSistemBilgileri.Image = ((System.Drawing.Image)(resources.GetObject("BtnSistemBilgileri.Image")));
            this.BtnSistemBilgileri.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnSistemBilgileri.Name = "BtnSistemBilgileri";
            this.BtnSistemBilgileri.Size = new System.Drawing.Size(136, 42);
            this.BtnSistemBilgileri.Text = "Kaydın Bilgilerini Göster";
            // 
            // paneldetay
            // 
            this.paneldetay.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.paneldetay.Appearance.Options.UseBackColor = true;
            this.paneldetay.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.paneldetay.Controls.Add(this.paneldetayfill);
            this.paneldetay.Controls.Add(this.paneldetaybottom);
            this.paneldetay.Controls.Add(this.paneldetaytop);
            this.paneldetay.Controls.Add(this.paneldetayleft);
            this.paneldetay.Dock = System.Windows.Forms.DockStyle.Top;
            this.paneldetay.Location = new System.Drawing.Point(0, 49);
            this.paneldetay.Name = "paneldetay";
            this.paneldetay.Size = new System.Drawing.Size(947, 366);
            this.paneldetay.TabIndex = 7;
            // 
            // paneldetayfill
            // 
            this.paneldetayfill.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.paneldetayfill.Appearance.Options.UseBackColor = true;
            this.paneldetayfill.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.paneldetayfill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paneldetayfill.Location = new System.Drawing.Point(24, 23);
            this.paneldetayfill.Name = "paneldetayfill";
            this.paneldetayfill.Size = new System.Drawing.Size(923, 320);
            this.paneldetayfill.TabIndex = 2;
            // 
            // paneldetaybottom
            // 
            this.paneldetaybottom.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.paneldetaybottom.Appearance.Options.UseBackColor = true;
            this.paneldetaybottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.paneldetaybottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.paneldetaybottom.Location = new System.Drawing.Point(24, 343);
            this.paneldetaybottom.Name = "paneldetaybottom";
            this.paneldetaybottom.Size = new System.Drawing.Size(923, 23);
            this.paneldetaybottom.TabIndex = 3;
            // 
            // paneldetaytop
            // 
            this.paneldetaytop.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.paneldetaytop.Appearance.Options.UseBackColor = true;
            this.paneldetaytop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.paneldetaytop.Dock = System.Windows.Forms.DockStyle.Top;
            this.paneldetaytop.Location = new System.Drawing.Point(24, 0);
            this.paneldetaytop.Name = "paneldetaytop";
            this.paneldetaytop.Size = new System.Drawing.Size(923, 23);
            this.paneldetaytop.TabIndex = 1;
            // 
            // paneldetayleft
            // 
            this.paneldetayleft.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.paneldetayleft.Appearance.Options.UseBackColor = true;
            this.paneldetayleft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.paneldetayleft.Dock = System.Windows.Forms.DockStyle.Left;
            this.paneldetayleft.Location = new System.Drawing.Point(0, 0);
            this.paneldetayleft.Name = "paneldetayleft";
            this.paneldetayleft.Size = new System.Drawing.Size(24, 366);
            this.paneldetayleft.TabIndex = 0;
            // 
            // sp
            // 
            this.sp.Dock = System.Windows.Forms.DockStyle.Top;
            this.sp.Location = new System.Drawing.Point(0, 415);
            this.sp.Name = "sp";
            this.sp.Size = new System.Drawing.Size(947, 6);
            this.sp.TabIndex = 8;
            this.sp.TabStop = false;
            // 
            // pnlgrd
            // 
            this.pnlgrd.Controls.Add(this.grpgrd);
            this.pnlgrd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlgrd.Location = new System.Drawing.Point(0, 421);
            this.pnlgrd.Name = "pnlgrd";
            this.pnlgrd.Size = new System.Drawing.Size(947, 124);
            this.pnlgrd.TabIndex = 9;
            // 
            // grpgrd
            // 
            this.grpgrd.Controls.Add(this.grd);
            this.grpgrd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpgrd.Location = new System.Drawing.Point(2, 2);
            this.grpgrd.Name = "grpgrd";
            this.grpgrd.Size = new System.Drawing.Size(943, 120);
            this.grpgrd.TabIndex = 0;
            // 
            // grd
            // 
            this.grd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd.EmbeddedNavigator.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.grd.EmbeddedNavigator.Appearance.Options.UseFont = true;
            this.grd.EmbeddedNavigator.TextStringFormat = "Kayıt {0} / {1}";
            this.grd.Location = new System.Drawing.Point(2, 22);
            this.grd.MainView = this.grdv;
            this.grd.Name = "grd";
            this.grd.Size = new System.Drawing.Size(939, 96);
            this.grd.TabIndex = 0;
            this.grd.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdv});
            // 
            // grdv
            // 
            this.grdv.GridControl = this.grd;
            this.grdv.GroupPanelText = "Başlıkları Buraya Sürükleyerek Guruplayabilirsiniz";
            this.grdv.Name = "grdv";
            this.grdv.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.grdv.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.grdv.OptionsBehavior.AllowIncrementalSearch = true;
            this.grdv.OptionsBehavior.ReadOnly = true;
            this.grdv.OptionsNavigation.AutoFocusNewRow = true;
            this.grdv.OptionsSelection.InvertSelection = true;
            this.grdv.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.grdv.OptionsView.ShowAutoFilterRow = true;
            // 
            // frmBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 545);
            this.Controls.Add(this.pnlgrd);
            this.Controls.Add(this.sp);
            this.Controls.Add(this.paneldetay);
            this.Controls.Add(this.panelbutton);
            this.KeyPreview = true;
            this.Name = "frmBase";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.panelbutton)).EndInit();
            this.panelbutton.ResumeLayout(false);
            this.panelbutton.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dbnav)).EndInit();
            this.dbnav.ResumeLayout(false);
            this.dbnav.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paneldetay)).EndInit();
            this.paneldetay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.paneldetayfill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paneldetaybottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paneldetaytop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paneldetayleft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.formbs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlgrd)).EndInit();
            this.pnlgrd.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpgrd)).EndInit();
            this.grpgrd.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.PanelControl panelbutton;
        public DevExpress.XtraEditors.PanelControl paneldetay;
        public DevExpress.XtraEditors.SplitterControl sp;
        public System.Windows.Forms.BindingNavigator dbnav;
        public System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        public System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        public System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        public System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        public System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        public System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        public System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        public System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        public System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        public System.Windows.Forms.ToolStripButton btnYeni;
        public System.Windows.Forms.ToolStripButton btnYenile;
        public System.Windows.Forms.ToolStripButton btnKaydet;
        public System.Windows.Forms.ToolStripButton btnIptal;
        public System.Windows.Forms.ToolStripButton btnDuzenle;
        public System.Windows.Forms.ToolStripButton btnVazgec;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStripButton btnGeri;
        public System.Windows.Forms.ToolStripButton btnIleri;
        public System.Windows.Forms.BindingSource formbs;
        public DevExpress.XtraEditors.PanelControl paneldetayleft;
        public DevExpress.XtraEditors.PanelControl paneldetayfill;
        public DevExpress.XtraEditors.PanelControl paneldetaybottom;
        public DevExpress.XtraEditors.PanelControl paneldetaytop;
        public DevExpress.XtraGrid.GridControl grd;
        public DevExpress.XtraGrid.Views.Grid.GridView grdv;
        public DevExpress.XtraEditors.PanelControl pnlgrd;
        public DevExpress.XtraEditors.GroupControl grpgrd;
        private System.Windows.Forms.ToolStripButton BtnSistemBilgileri;

    }
}