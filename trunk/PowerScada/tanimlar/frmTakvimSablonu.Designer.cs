namespace AHBS2010
{
    partial class frmTakvimSablonu
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.Grid = new System.Windows.Forms.DataGridView();
            this.panelust = new DevExpress.XtraEditors.PanelControl();
            this.labelControlSablonTuru = new DevExpress.XtraEditors.LabelControl();
            this.ucEnumGosterSablonTuru = new AHBS2010.UcEnumGoster();
            this.labelControlSablonAdi = new DevExpress.XtraEditors.LabelControl();
            this.textEditSablonAdi = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.SaveInformationexpando)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelust)).BeginInit();
            this.panelust.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditSablonAdi.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // SaveInformationexpando
            // 
            this.SaveInformationexpando.CustomHeaderSettings.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelControl1);
            this.panel1.Controls.Add(this.panelust);
            this.panel1.Size = new System.Drawing.Size(810, 545);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.Grid);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 151);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(810, 394);
            this.panelControl1.TabIndex = 1;
            // 
            // Grid
            // 
            this.Grid.AllowUserToOrderColumns = true;
            this.Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grid.Location = new System.Drawing.Point(2, 2);
            this.Grid.Name = "Grid";
            this.Grid.Size = new System.Drawing.Size(806, 390);
            this.Grid.TabIndex = 0;
            this.Grid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_RowEnter);
            this.Grid.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_RowLeave);
            this.Grid.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.Grid_RowValidating);
            // 
            // panelust
            // 
            this.panelust.Controls.Add(this.labelControlSablonTuru);
            this.panelust.Controls.Add(this.ucEnumGosterSablonTuru);
            this.panelust.Controls.Add(this.labelControlSablonAdi);
            this.panelust.Controls.Add(this.textEditSablonAdi);
            this.panelust.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelust.Location = new System.Drawing.Point(0, 0);
            this.panelust.Name = "panelust";
            this.panelust.Size = new System.Drawing.Size(810, 151);
            this.panelust.TabIndex = 2;
            // 
            // labelControlSablonTuru
            // 
            this.labelControlSablonTuru.Location = new System.Drawing.Point(65, 94);
            this.labelControlSablonTuru.Name = "labelControlSablonTuru";
            this.labelControlSablonTuru.Size = new System.Drawing.Size(57, 13);
            this.labelControlSablonTuru.TabIndex = 2;
            this.labelControlSablonTuru.Text = "Şablon Türü";
            // 
            // ucEnumGosterSablonTuru
            // 
            this.ucEnumGosterSablonTuru.Deger = 0;
            this.ucEnumGosterSablonTuru.EnumTuru = "IzlemTuru";
            this.ucEnumGosterSablonTuru.Location = new System.Drawing.Point(150, 87);
            this.ucEnumGosterSablonTuru.Name = "ucEnumGosterSablonTuru";
            this.ucEnumGosterSablonTuru.Size = new System.Drawing.Size(197, 20);
            this.ucEnumGosterSablonTuru.TabIndex = 3;
            // 
            // labelControlSablonAdi
            // 
            this.labelControlSablonAdi.Location = new System.Drawing.Point(65, 68);
            this.labelControlSablonAdi.Name = "labelControlSablonAdi";
            this.labelControlSablonAdi.Size = new System.Drawing.Size(50, 13);
            this.labelControlSablonAdi.TabIndex = 0;
            this.labelControlSablonAdi.Text = "Şablon Adı";
            // 
            // textEditSablonAdi
            // 
            this.textEditSablonAdi.Location = new System.Drawing.Point(150, 61);
            this.textEditSablonAdi.Name = "textEditSablonAdi";
            this.textEditSablonAdi.Size = new System.Drawing.Size(197, 20);
            this.textEditSablonAdi.TabIndex = 1;
            // 
            // frmTakvimSablonu
            // 
            this.ClientSize = new System.Drawing.Size(983, 545);
            this.Name = "frmTakvimSablonu";
            ((System.ComponentModel.ISupportInitialize)(this.SaveInformationexpando)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelust)).EndInit();
            this.panelust.ResumeLayout(false);
            this.panelust.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditSablonAdi.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelust;
        private DevExpress.XtraEditors.LabelControl labelControlSablonTuru;
        private UcEnumGoster ucEnumGosterSablonTuru;
        private DevExpress.XtraEditors.LabelControl labelControlSablonAdi;
        private DevExpress.XtraEditors.TextEdit textEditSablonAdi;
        private System.Windows.Forms.DataGridView Grid;

    }
}
