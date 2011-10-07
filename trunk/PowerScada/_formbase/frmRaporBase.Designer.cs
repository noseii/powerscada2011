namespace PowerScada
{
    partial class frmRaporBase
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
            this.panelAna = new DevExpress.XtraEditors.PanelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.grid = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelUst = new DevExpress.XtraEditors.PanelControl();
            this.simpleButtonCikis = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonExceleAktar = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonGoruntule = new DevExpress.XtraEditors.SimpleButton();
            this.dateEditRaporTarihi = new DevExpress.XtraEditors.DateEdit();
            this.labelRaporTarihi = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelAna)).BeginInit();
            this.panelAna.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelUst)).BeginInit();
            this.panelUst.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditRaporTarihi.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditRaporTarihi.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelAna
            // 
            this.panelAna.Controls.Add(this.panelControl1);
            this.panelAna.Controls.Add(this.panelUst);
            this.panelAna.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAna.Location = new System.Drawing.Point(0, 0);
            this.panelAna.Name = "panelAna";
            this.panelAna.Size = new System.Drawing.Size(690, 431);
            this.panelAna.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.grid);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(2, 135);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(686, 294);
            this.panelControl1.TabIndex = 1;
            // 
            // grid
            // 
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(2, 2);
            this.grid.MainView = this.gridView;
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(682, 290);
            this.grid.TabIndex = 0;
            this.grid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.GridControl = this.grid;
            this.gridView.GroupPanelText = "Başlıklara Tıklayarak sıralamayı,Buraya sürükleyerek gruplamayı yapabilirsiniz ";
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView.OptionsBehavior.AllowIncrementalSearch = true;
            this.gridView.OptionsBehavior.ReadOnly = true;
            this.gridView.OptionsSelection.InvertSelection = true;
            this.gridView.OptionsView.ColumnAutoWidth = false;
            this.gridView.OptionsView.ShowViewCaption = true;
            this.gridView.ViewCaption = "..";
            // 
            // panelUst
            // 
            this.panelUst.Controls.Add(this.simpleButtonCikis);
            this.panelUst.Controls.Add(this.simpleButtonExceleAktar);
            this.panelUst.Controls.Add(this.simpleButtonGoruntule);
            this.panelUst.Controls.Add(this.dateEditRaporTarihi);
            this.panelUst.Controls.Add(this.labelRaporTarihi);
            this.panelUst.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelUst.Location = new System.Drawing.Point(2, 2);
            this.panelUst.Name = "panelUst";
            this.panelUst.Size = new System.Drawing.Size(686, 133);
            this.panelUst.TabIndex = 0;
            // 
            // simpleButtonCikis
            // 
            this.simpleButtonCikis.Location = new System.Drawing.Point(281, 103);
            this.simpleButtonCikis.Name = "simpleButtonCikis";
            this.simpleButtonCikis.Size = new System.Drawing.Size(119, 23);
            this.simpleButtonCikis.TabIndex = 6;
            this.simpleButtonCikis.Text = "Çıkış";
            // 
            // simpleButtonExceleAktar
            // 
            this.simpleButtonExceleAktar.Location = new System.Drawing.Point(146, 103);
            this.simpleButtonExceleAktar.Name = "simpleButtonExceleAktar";
            this.simpleButtonExceleAktar.Size = new System.Drawing.Size(119, 23);
            this.simpleButtonExceleAktar.TabIndex = 5;
            this.simpleButtonExceleAktar.Text = "Excele Aktar";
            // 
            // simpleButtonGoruntule
            // 
            this.simpleButtonGoruntule.Location = new System.Drawing.Point(11, 103);
            this.simpleButtonGoruntule.Name = "simpleButtonGoruntule";
            this.simpleButtonGoruntule.Size = new System.Drawing.Size(119, 23);
            this.simpleButtonGoruntule.TabIndex = 4;
            this.simpleButtonGoruntule.Text = "Görüntüle";
            // 
            // dateEditRaporTarihi
            // 
            this.dateEditRaporTarihi.EditValue = null;
            this.dateEditRaporTarihi.Location = new System.Drawing.Point(77, 10);
            this.dateEditRaporTarihi.Name = "dateEditRaporTarihi";
            this.dateEditRaporTarihi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditRaporTarihi.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditRaporTarihi.Size = new System.Drawing.Size(129, 20);
            this.dateEditRaporTarihi.TabIndex = 3;
            // 
            // labelRaporTarihi
            // 
            this.labelRaporTarihi.Location = new System.Drawing.Point(9, 14);
            this.labelRaporTarihi.Name = "labelRaporTarihi";
            this.labelRaporTarihi.Size = new System.Drawing.Size(58, 13);
            this.labelRaporTarihi.TabIndex = 2;
            this.labelRaporTarihi.Text = "Rapor Tarihi";
            // 
            // frmRaporBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 431);
            this.Controls.Add(this.panelAna);
            this.Name = "frmRaporBase";
            this.ShowIcon = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.panelAna)).EndInit();
            this.panelAna.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelUst)).EndInit();
            this.panelUst.ResumeLayout(false);
            this.panelUst.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditRaporTarihi.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditRaporTarihi.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.PanelControl panelAna;
        public DevExpress.XtraEditors.PanelControl panelControl1;
        public DevExpress.XtraEditors.PanelControl panelUst;
        public DevExpress.XtraGrid.GridControl grid;
        public DevExpress.XtraEditors.SimpleButton simpleButtonCikis;
        public DevExpress.XtraEditors.SimpleButton simpleButtonExceleAktar;
        public DevExpress.XtraEditors.SimpleButton simpleButtonGoruntule;
        public DevExpress.XtraEditors.DateEdit dateEditRaporTarihi;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView;
        public DevExpress.XtraEditors.LabelControl labelRaporTarihi;
    }
}