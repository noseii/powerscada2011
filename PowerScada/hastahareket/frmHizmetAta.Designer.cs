namespace AHBS2010
{
    partial class frmHizmetAta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHizmetAta));
            this.panelControl11 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tptumu = new DevExpress.XtraTab.XtraTabPage();
            this.trltum = new DevExpress.XtraTreeList.TreeList();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btniptal = new DevExpress.XtraEditors.SimpleButton();
            this.btnaktarnormal = new DevExpress.XtraEditors.SimpleButton();
            this.panelControlFiltre = new DevExpress.XtraEditors.PanelControl();
            this.radioButtonKodu = new System.Windows.Forms.RadioButton();
            this.radioButtonAdi = new System.Windows.Forms.RadioButton();
            this.textBoxFilter = new System.Windows.Forms.TextBox();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.treeMuayeneHizmeti = new DevExpress.XtraTreeList.TreeList();
            ((System.ComponentModel.ISupportInitialize)(this.pnlbuttonss)).BeginInit();
            this.pnlbuttonss.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl112s)).BeginInit();
            this.panelControl112s.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl11)).BeginInit();
            this.panelControl11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tptumu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trltum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFiltre)).BeginInit();
            this.panelControlFiltre.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeMuayeneHizmeti)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlbuttonss
            // 
            this.pnlbuttonss.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pnlbuttonss.Appearance.Options.UseBackColor = true;
            this.pnlbuttonss.Location = new System.Drawing.Point(0, 596);
            this.pnlbuttonss.Size = new System.Drawing.Size(971, 52);
            this.pnlbuttonss.TabIndex = 2;
            // 
            // panelControl112s
            // 
            this.panelControl112s.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControl112s.Appearance.Options.UseBackColor = true;
            this.panelControl112s.Size = new System.Drawing.Size(967, 48);
            // 
            // groupBoxHastaBilgileri
            // 
            this.groupBoxHastaBilgileri.Size = new System.Drawing.Size(971, 51);
            // 
            // panelControl11
            // 
            this.panelControl11.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControl11.Appearance.Options.UseBackColor = true;
            this.panelControl11.Controls.Add(this.groupControl1);
            this.panelControl11.Controls.Add(this.splitterControl1);
            this.panelControl11.Controls.Add(this.groupControl2);
            this.panelControl11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl11.Location = new System.Drawing.Point(0, 51);
            this.panelControl11.Name = "panelControl11";
            this.panelControl11.Size = new System.Drawing.Size(971, 545);
            this.panelControl11.TabIndex = 1;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.xtraTabControl1);
            this.groupControl1.Controls.Add(this.panelControl2);
            this.groupControl1.Controls.Add(this.panelControlFiltre);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(2, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(540, 541);
            this.groupControl1.TabIndex = 0;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(2, 57);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tptumu;
            this.xtraTabControl1.Size = new System.Drawing.Size(411, 482);
            this.xtraTabControl1.TabIndex = 2;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tptumu});
            // 
            // tptumu
            // 
            this.tptumu.Controls.Add(this.trltum);
            this.tptumu.Name = "tptumu";
            this.tptumu.Size = new System.Drawing.Size(404, 453);
            this.tptumu.Text = "Tümü";
            // 
            // trltum
            // 
            this.trltum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trltum.KeyFieldName = "Id";
            this.trltum.Location = new System.Drawing.Point(0, 0);
            this.trltum.Name = "trltum";
            this.trltum.OptionsBehavior.AllowIncrementalSearch = true;
            this.trltum.OptionsBehavior.Editable = false;
            this.trltum.OptionsBehavior.EnableFiltering = true;
            this.trltum.OptionsSelection.MultiSelect = true;
            this.trltum.OptionsView.AutoWidth = false;
            this.trltum.ParentFieldName = "UstHizmet_Id";
            this.trltum.Size = new System.Drawing.Size(404, 453);
            this.trltum.TabIndex = 0;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btniptal);
            this.panelControl2.Controls.Add(this.btnaktarnormal);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl2.Location = new System.Drawing.Point(413, 57);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(125, 482);
            this.panelControl2.TabIndex = 2;
            // 
            // btniptal
            // 
            this.btniptal.Image = ((System.Drawing.Image)(resources.GetObject("btniptal.Image")));
            this.btniptal.Location = new System.Drawing.Point(8, 305);
            this.btniptal.Name = "btniptal";
            this.btniptal.Size = new System.Drawing.Size(111, 57);
            this.btniptal.TabIndex = 1;
            this.btniptal.Text = "Hizmet Çıkar";
            // 
            // btnaktarnormal
            // 
            this.btnaktarnormal.Image = ((System.Drawing.Image)(resources.GetObject("btnaktarnormal.Image")));
            this.btnaktarnormal.Location = new System.Drawing.Point(9, 242);
            this.btnaktarnormal.Name = "btnaktarnormal";
            this.btnaktarnormal.Size = new System.Drawing.Size(111, 57);
            this.btnaktarnormal.TabIndex = 0;
            this.btnaktarnormal.Text = "Hizmet Aktar";
            // 
            // panelControlFiltre
            // 
            this.panelControlFiltre.Controls.Add(this.radioButtonKodu);
            this.panelControlFiltre.Controls.Add(this.radioButtonAdi);
            this.panelControlFiltre.Controls.Add(this.textBoxFilter);
            this.panelControlFiltre.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControlFiltre.Location = new System.Drawing.Point(2, 22);
            this.panelControlFiltre.Name = "panelControlFiltre";
            this.panelControlFiltre.Size = new System.Drawing.Size(536, 35);
            this.panelControlFiltre.TabIndex = 0;
            // 
            // radioButtonKodu
            // 
            this.radioButtonKodu.AutoSize = true;
            this.radioButtonKodu.Location = new System.Drawing.Point(87, 10);
            this.radioButtonKodu.Name = "radioButtonKodu";
            this.radioButtonKodu.Size = new System.Drawing.Size(82, 17);
            this.radioButtonKodu.TabIndex = 1;
            this.radioButtonKodu.Text = "Kodu ile Ara";
            this.radioButtonKodu.UseVisualStyleBackColor = true;
            // 
            // radioButtonAdi
            // 
            this.radioButtonAdi.AutoSize = true;
            this.radioButtonAdi.Checked = true;
            this.radioButtonAdi.Location = new System.Drawing.Point(8, 11);
            this.radioButtonAdi.Name = "radioButtonAdi";
            this.radioButtonAdi.Size = new System.Drawing.Size(73, 17);
            this.radioButtonAdi.TabIndex = 0;
            this.radioButtonAdi.TabStop = true;
            this.radioButtonAdi.Text = "Adı ile Ara";
            this.radioButtonAdi.UseVisualStyleBackColor = true;
            // 
            // textBoxFilter
            // 
            this.textBoxFilter.Location = new System.Drawing.Point(175, 7);
            this.textBoxFilter.Name = "textBoxFilter";
            this.textBoxFilter.Size = new System.Drawing.Size(230, 21);
            this.textBoxFilter.TabIndex = 2;
            this.textBoxFilter.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitterControl1.Location = new System.Drawing.Point(542, 2);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(6, 541);
            this.splitterControl1.TabIndex = 0;
            this.splitterControl1.TabStop = false;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.treeMuayeneHizmeti);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupControl2.Location = new System.Drawing.Point(548, 2);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(421, 541);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Muayenede Kullanılan Hizmetler";
            // 
            // treeMuayeneHizmeti
            // 
            this.treeMuayeneHizmeti.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeMuayeneHizmeti.KeyFieldName = "Id";
            this.treeMuayeneHizmeti.Location = new System.Drawing.Point(2, 22);
            this.treeMuayeneHizmeti.Name = "treeMuayeneHizmeti";
            this.treeMuayeneHizmeti.OptionsBehavior.AllowIncrementalSearch = true;
            this.treeMuayeneHizmeti.OptionsBehavior.Editable = false;
            this.treeMuayeneHizmeti.OptionsBehavior.EnableFiltering = true;
            this.treeMuayeneHizmeti.OptionsSelection.MultiSelect = true;
            this.treeMuayeneHizmeti.OptionsView.AutoWidth = false;
            this.treeMuayeneHizmeti.OptionsView.ShowRoot = false;
            this.treeMuayeneHizmeti.ParentFieldName = "UstTeshis_Id";
            this.treeMuayeneHizmeti.Size = new System.Drawing.Size(417, 517);
            this.treeMuayeneHizmeti.TabIndex = 0;
            // 
            // frmHizmetAta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 648);
            this.Controls.Add(this.panelControl11);
            this.Name = "frmHizmetAta";
            this.Text = "Muayeneye Hizmet Ata";
            this.Controls.SetChildIndex(this.groupBoxHastaBilgileri, 0);
            this.Controls.SetChildIndex(this.pnlbuttonss, 0);
            this.Controls.SetChildIndex(this.panelControl11, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pnlbuttonss)).EndInit();
            this.pnlbuttonss.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl112s)).EndInit();
            this.panelControl112s.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl11)).EndInit();
            this.panelControl11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tptumu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trltum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFiltre)).EndInit();
            this.panelControlFiltre.ResumeLayout(false);
            this.panelControlFiltre.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeMuayeneHizmeti)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl11;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage tptumu;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraTreeList.TreeList trltum;
        private DevExpress.XtraTreeList.TreeList treeMuayeneHizmeti;
        private DevExpress.XtraEditors.SimpleButton btniptal;
        private DevExpress.XtraEditors.SimpleButton btnaktarnormal;
        private DevExpress.XtraEditors.PanelControl panelControlFiltre;
        private System.Windows.Forms.RadioButton radioButtonKodu;
        private System.Windows.Forms.RadioButton radioButtonAdi;
        private System.Windows.Forms.TextBox textBoxFilter;
    }
}