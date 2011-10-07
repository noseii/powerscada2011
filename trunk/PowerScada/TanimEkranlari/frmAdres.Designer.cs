namespace PowerScada
{
    partial class frmAdres
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
            this.ucEnumGosterSablonTuru = new PowerScada.UcEnumGoster();
            this.labelControlAdi = new DevExpress.XtraEditors.LabelControl();
            this.textEditSablonAdi = new DevExpress.XtraEditors.TextEdit();
            this.labelControlKodu = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit3 = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.SaveInformationexpando)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelust)).BeginInit();
            this.panelust.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditSablonAdi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).BeginInit();
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
            this.panelControl1.Location = new System.Drawing.Point(0, 302);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(810, 243);
            this.panelControl1.TabIndex = 1;
            // 
            // Grid
            // 
            this.Grid.AllowUserToOrderColumns = true;
            this.Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grid.Location = new System.Drawing.Point(2, 2);
            this.Grid.Name = "Grid";
            this.Grid.Size = new System.Drawing.Size(806, 239);
            this.Grid.TabIndex = 0;
              // 
            // panelust
            // 
            this.panelust.Controls.Add(this.labelControl2);
            this.panelust.Controls.Add(this.textEdit2);
            this.panelust.Controls.Add(this.labelControl3);
            this.panelust.Controls.Add(this.textEdit3);
            this.panelust.Controls.Add(this.labelControlKodu);
            this.panelust.Controls.Add(this.textEdit1);
            this.panelust.Controls.Add(this.labelControlSablonTuru);
            this.panelust.Controls.Add(this.ucEnumGosterSablonTuru);
            this.panelust.Controls.Add(this.labelControlAdi);
            this.panelust.Controls.Add(this.textEditSablonAdi);
            this.panelust.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelust.Location = new System.Drawing.Point(0, 0);
            this.panelust.Name = "panelust";
            this.panelust.Size = new System.Drawing.Size(810, 302);
            this.panelust.TabIndex = 2;
            // 
            // labelControlSablonTuru
            // 
            this.labelControlSablonTuru.Location = new System.Drawing.Point(535, 51);
            this.labelControlSablonTuru.Name = "labelControlSablonTuru";
            this.labelControlSablonTuru.Size = new System.Drawing.Size(57, 13);
            this.labelControlSablonTuru.TabIndex = 2;
            this.labelControlSablonTuru.Text = "Şablon Türü";
            // 
            // ucEnumGosterSablonTuru
            // 
            this.ucEnumGosterSablonTuru.Deger = 0;
            this.ucEnumGosterSablonTuru.EnumTuru = "IzlemTuru";
            this.ucEnumGosterSablonTuru.Location = new System.Drawing.Point(620, 44);
            this.ucEnumGosterSablonTuru.Name = "ucEnumGosterSablonTuru";
            this.ucEnumGosterSablonTuru.Size = new System.Drawing.Size(197, 20);
            this.ucEnumGosterSablonTuru.TabIndex = 3;
            // 
            // labelControlAdi
            // 
            this.labelControlAdi.Location = new System.Drawing.Point(65, 68);
            this.labelControlAdi.Name = "labelControlAdi";
            this.labelControlAdi.Size = new System.Drawing.Size(15, 13);
            this.labelControlAdi.TabIndex = 0;
            this.labelControlAdi.Text = "Adı";
            // 
            // textEditSablonAdi
            // 
            this.textEditSablonAdi.Location = new System.Drawing.Point(150, 61);
            this.textEditSablonAdi.Name = "textEditSablonAdi";
            this.textEditSablonAdi.Size = new System.Drawing.Size(197, 20);
            this.textEditSablonAdi.TabIndex = 1;
            // 
            // labelControlKodu
            // 
            this.labelControlKodu.Location = new System.Drawing.Point(65, 94);
            this.labelControlKodu.Name = "labelControlKodu";
            this.labelControlKodu.Size = new System.Drawing.Size(24, 13);
            this.labelControlKodu.TabIndex = 4;
            this.labelControlKodu.Text = "Kodu";
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(150, 87);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(197, 20);
            this.textEdit1.TabIndex = 5;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(65, 147);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(50, 13);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "Şablon Adı";
            // 
            // textEdit2
            // 
            this.textEdit2.Location = new System.Drawing.Point(150, 140);
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Size = new System.Drawing.Size(197, 20);
            this.textEdit2.TabIndex = 9;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(65, 121);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(0, 13);
            this.labelControl3.TabIndex = 6;
            // 
            // textEdit3
            // 
            this.textEdit3.Location = new System.Drawing.Point(150, 114);
            this.textEdit3.Name = "textEdit3";
            this.textEdit3.Size = new System.Drawing.Size(197, 20);
            this.textEdit3.TabIndex = 7;
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
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelust;
        private DevExpress.XtraEditors.LabelControl labelControlSablonTuru;
        private UcEnumGoster ucEnumGosterSablonTuru;
        private DevExpress.XtraEditors.LabelControl labelControlAdi;
        private DevExpress.XtraEditors.TextEdit textEditSablonAdi;
        private System.Windows.Forms.DataGridView Grid;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit textEdit2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit textEdit3;
        private DevExpress.XtraEditors.LabelControl labelControlKodu;
        private DevExpress.XtraEditors.TextEdit textEdit1;

    }
}
