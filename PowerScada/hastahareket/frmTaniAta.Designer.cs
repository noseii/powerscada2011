namespace AHBS2010
{
    partial class frmTaniAta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTaniAta));
            this.panelControl11 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tptumu = new DevExpress.XtraTab.XtraTabPage();
            this.trltum = new DevExpress.XtraTreeList.TreeList();
            this.tpdoktor = new DevExpress.XtraTab.XtraTabPage();
            this.treeListDoktorTeshis = new DevExpress.XtraTreeList.TreeList();
            this.panelControlFiltre = new DevExpress.XtraEditors.PanelControl();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButtonAdi = new System.Windows.Forms.RadioButton();
            this.textBoxAdi = new System.Windows.Forms.TextBox();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButtonDoktordancikar = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtondoktoraAta = new DevExpress.XtraEditors.SimpleButton();
            this.btnalerjik = new DevExpress.XtraEditors.SimpleButton();
            this.btnkronik = new DevExpress.XtraEditors.SimpleButton();
            this.btniptal = new DevExpress.XtraEditors.SimpleButton();
            this.btnaktarnormal = new DevExpress.XtraEditors.SimpleButton();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.trlhasta = new DevExpress.XtraTreeList.TreeList();
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
            this.tpdoktor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListDoktorTeshis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFiltre)).BeginInit();
            this.panelControlFiltre.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trlhasta)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlbuttonss
            // 
            this.pnlbuttonss.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pnlbuttonss.Appearance.Options.UseBackColor = true;
            this.pnlbuttonss.Location = new System.Drawing.Point(0, 615);
            this.pnlbuttonss.Size = new System.Drawing.Size(1070, 52);
            this.pnlbuttonss.TabIndex = 1;
            // 
            // panelControl112s
            // 
            this.panelControl112s.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControl112s.Appearance.Options.UseBackColor = true;
            this.panelControl112s.Size = new System.Drawing.Size(1066, 48);
            // 
            // groupBoxHastaBilgileri
            // 
            this.groupBoxHastaBilgileri.Size = new System.Drawing.Size(1070, 51);
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
            this.panelControl11.Size = new System.Drawing.Size(1070, 564);
            this.panelControl11.TabIndex = 1;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.xtraTabControl1);
            this.groupControl1.Controls.Add(this.panelControlFiltre);
            this.groupControl1.Controls.Add(this.panelControl2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(2, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(678, 560);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Tanı ve Hastalık Kodları";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(2, 57);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tptumu;
            this.xtraTabControl1.Size = new System.Drawing.Size(549, 501);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tptumu,
            this.tpdoktor});
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
            // 
            // tptumu
            // 
            this.tptumu.Controls.Add(this.trltum);
            this.tptumu.Name = "tptumu";
            this.tptumu.Size = new System.Drawing.Size(542, 472);
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
            this.trltum.ParentFieldName = "UstTeshis_Id";
            this.trltum.Size = new System.Drawing.Size(542, 472);
            this.trltum.TabIndex = 0;
            this.trltum.Tag = "Teshis";
            // 
            // tpdoktor
            // 
            this.tpdoktor.Controls.Add(this.treeListDoktorTeshis);
            this.tpdoktor.Name = "tpdoktor";
            this.tpdoktor.Size = new System.Drawing.Size(542, 472);
            this.tpdoktor.Text = "Doktora Atanan Teşhisler";
            // 
            // treeListDoktorTeshis
            // 
            this.treeListDoktorTeshis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListDoktorTeshis.KeyFieldName = "Id";
            this.treeListDoktorTeshis.Location = new System.Drawing.Point(0, 0);
            this.treeListDoktorTeshis.Name = "treeListDoktorTeshis";
            this.treeListDoktorTeshis.OptionsBehavior.AllowIncrementalSearch = true;
            this.treeListDoktorTeshis.OptionsBehavior.Editable = false;
            this.treeListDoktorTeshis.OptionsBehavior.EnableFiltering = true;
            this.treeListDoktorTeshis.OptionsSelection.MultiSelect = true;
            this.treeListDoktorTeshis.OptionsView.AutoWidth = false;
            this.treeListDoktorTeshis.OptionsView.ShowCheckBoxes = true;
            this.treeListDoktorTeshis.ParentFieldName = "UstTeshis_Id";
            this.treeListDoktorTeshis.Size = new System.Drawing.Size(542, 472);
            this.treeListDoktorTeshis.TabIndex = 1;
            this.treeListDoktorTeshis.Tag = "DoktorTeshis";
            // 
            // panelControlFiltre
            // 
            this.panelControlFiltre.Controls.Add(this.radioButton1);
            this.panelControlFiltre.Controls.Add(this.radioButtonAdi);
            this.panelControlFiltre.Controls.Add(this.textBoxAdi);
            this.panelControlFiltre.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControlFiltre.Location = new System.Drawing.Point(2, 22);
            this.panelControlFiltre.Name = "panelControlFiltre";
            this.panelControlFiltre.Size = new System.Drawing.Size(549, 35);
            this.panelControlFiltre.TabIndex = 0;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(87, 10);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(82, 17);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Kodu ile Ara";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButtonAdi
            // 
            this.radioButtonAdi.AutoSize = true;
            this.radioButtonAdi.Location = new System.Drawing.Point(8, 11);
            this.radioButtonAdi.Name = "radioButtonAdi";
            this.radioButtonAdi.Size = new System.Drawing.Size(73, 17);
            this.radioButtonAdi.TabIndex = 0;
            this.radioButtonAdi.Text = "Adı ile Ara";
            this.radioButtonAdi.UseVisualStyleBackColor = true;
            // 
            // textBoxAdi
            // 
            this.textBoxAdi.Location = new System.Drawing.Point(175, 7);
            this.textBoxAdi.Name = "textBoxAdi";
            this.textBoxAdi.Size = new System.Drawing.Size(230, 21);
            this.textBoxAdi.TabIndex = 2;
            this.textBoxAdi.TextChanged += new System.EventHandler(this.textBoxAdi_TextChanged);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.simpleButtonDoktordancikar);
            this.panelControl2.Controls.Add(this.simpleButtondoktoraAta);
            this.panelControl2.Controls.Add(this.btnalerjik);
            this.panelControl2.Controls.Add(this.btnkronik);
            this.panelControl2.Controls.Add(this.btniptal);
            this.panelControl2.Controls.Add(this.btnaktarnormal);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl2.Location = new System.Drawing.Point(551, 22);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(125, 536);
            this.panelControl2.TabIndex = 2;
            // 
            // simpleButtonDoktordancikar
            // 
            this.simpleButtonDoktordancikar.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonDoktordancikar.Image")));
            this.simpleButtonDoktordancikar.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.simpleButtonDoktordancikar.Location = new System.Drawing.Point(9, 471);
            this.simpleButtonDoktordancikar.Name = "simpleButtonDoktordancikar";
            this.simpleButtonDoktordancikar.Size = new System.Drawing.Size(111, 57);
            this.simpleButtonDoktordancikar.TabIndex = 5;
            this.simpleButtonDoktordancikar.Text = "Doktordan Çıkar";
            // 
            // simpleButtondoktoraAta
            // 
            this.simpleButtondoktoraAta.Enabled = false;
            this.simpleButtondoktoraAta.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtondoktoraAta.Image")));
            this.simpleButtondoktoraAta.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.simpleButtondoktoraAta.Location = new System.Drawing.Point(9, 408);
            this.simpleButtondoktoraAta.Name = "simpleButtondoktoraAta";
            this.simpleButtondoktoraAta.Size = new System.Drawing.Size(111, 57);
            this.simpleButtondoktoraAta.TabIndex = 4;
            this.simpleButtondoktoraAta.Text = "Doktora Teşhis Ata";
            // 
            // btnalerjik
            // 
            this.btnalerjik.Image = ((System.Drawing.Image)(resources.GetObject("btnalerjik.Image")));
            this.btnalerjik.Location = new System.Drawing.Point(9, 242);
            this.btnalerjik.Name = "btnalerjik";
            this.btnalerjik.Size = new System.Drawing.Size(111, 57);
            this.btnalerjik.TabIndex = 2;
            this.btnalerjik.Text = "Alerjik Aktar";
            // 
            // btnkronik
            // 
            this.btnkronik.Image = ((System.Drawing.Image)(resources.GetObject("btnkronik.Image")));
            this.btnkronik.Location = new System.Drawing.Point(9, 179);
            this.btnkronik.Name = "btnkronik";
            this.btnkronik.Size = new System.Drawing.Size(111, 57);
            this.btnkronik.TabIndex = 1;
            this.btnkronik.Text = "Kronik Aktar";
            // 
            // btniptal
            // 
            this.btniptal.Image = ((System.Drawing.Image)(resources.GetObject("btniptal.Image")));
            this.btniptal.Location = new System.Drawing.Point(9, 305);
            this.btniptal.Name = "btniptal";
            this.btniptal.Size = new System.Drawing.Size(111, 57);
            this.btniptal.TabIndex = 3;
            this.btniptal.Text = "Hastadan Çıkar";
            // 
            // btnaktarnormal
            // 
            this.btnaktarnormal.Image = ((System.Drawing.Image)(resources.GetObject("btnaktarnormal.Image")));
            this.btnaktarnormal.Location = new System.Drawing.Point(9, 116);
            this.btnaktarnormal.Name = "btnaktarnormal";
            this.btnaktarnormal.Size = new System.Drawing.Size(111, 57);
            this.btnaktarnormal.TabIndex = 0;
            this.btnaktarnormal.Text = "Normal Aktar";
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitterControl1.Location = new System.Drawing.Point(680, 2);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(6, 560);
            this.splitterControl1.TabIndex = 1;
            this.splitterControl1.TabStop = false;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.trlhasta);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupControl2.Location = new System.Drawing.Point(686, 2);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(382, 560);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Hastaya Atanmış Hastalık ve Tanılar";
            // 
            // trlhasta
            // 
            this.trlhasta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trlhasta.KeyFieldName = "Id";
            this.trlhasta.Location = new System.Drawing.Point(2, 22);
            this.trlhasta.Name = "trlhasta";
            this.trlhasta.OptionsBehavior.AllowIncrementalSearch = true;
            this.trlhasta.OptionsBehavior.Editable = false;
            this.trlhasta.OptionsBehavior.EnableFiltering = true;
            this.trlhasta.OptionsSelection.MultiSelect = true;
            this.trlhasta.ParentFieldName = "UstTeshis_Id";
            this.trlhasta.Size = new System.Drawing.Size(378, 536);
            this.trlhasta.TabIndex = 0;
            // 
            // frmTaniAta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 667);
            this.Controls.Add(this.panelControl11);
            this.Name = "frmTaniAta";
            this.Text = "Tanı / Hastalık Ata";
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
            this.tpdoktor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListDoktorTeshis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFiltre)).EndInit();
            this.panelControlFiltre.ResumeLayout(false);
            this.panelControlFiltre.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trlhasta)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl11;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage tptumu;
        private DevExpress.XtraTab.XtraTabPage tpdoktor;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraTreeList.TreeList trltum;
        private DevExpress.XtraTreeList.TreeList trlhasta;
        private DevExpress.XtraEditors.SimpleButton btniptal;
        private DevExpress.XtraEditors.SimpleButton btnaktarnormal;
        private DevExpress.XtraEditors.SimpleButton btnalerjik;
        private DevExpress.XtraEditors.SimpleButton btnkronik;
        private DevExpress.XtraEditors.PanelControl panelControlFiltre;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButtonAdi;
        private System.Windows.Forms.TextBox textBoxAdi;
        private DevExpress.XtraEditors.SimpleButton simpleButtonDoktordancikar;
        private DevExpress.XtraEditors.SimpleButton simpleButtondoktoraAta;
        private DevExpress.XtraTreeList.TreeList treeListDoktorTeshis;
    }
}