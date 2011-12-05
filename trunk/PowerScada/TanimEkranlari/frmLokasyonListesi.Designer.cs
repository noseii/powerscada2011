using PowerScada;
namespace PowerScada
{
    partial class frmLokasyonListesi
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
            this.TextEditAdi = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.ucEnumGosterSablonTuru = new PowerScada.UcEnumGoster();
            ((System.ComponentModel.ISupportInitialize)(this.Islemler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextEditAdi.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // Islemler
            // 
            this.Islemler.CustomHeaderSettings.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            // 
            // Gridpanel
            // 
            this.Gridpanel.Size = new System.Drawing.Size(834, 545);
            // 
            // TextEditAdi
            // 
            this.TextEditAdi.Location = new System.Drawing.Point(113, 16);
            this.TextEditAdi.Name = "TextEditAdi";
            this.TextEditAdi.Size = new System.Drawing.Size(350, 20);
            this.TextEditAdi.TabIndex = 5;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(6, 22);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(15, 13);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "Adı";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(6, 47);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(57, 13);
            this.labelControl8.TabIndex = 0;
            this.labelControl8.Text = "Şablon Türü";
            // 
            // ucEnumGosterSablonTuru
            // 
            this.ucEnumGosterSablonTuru.Deger = 0;
            this.ucEnumGosterSablonTuru.EnumTuru = "IzlemTuru";
            this.ucEnumGosterSablonTuru.Location = new System.Drawing.Point(113, 42);
            this.ucEnumGosterSablonTuru.Name = "ucEnumGosterSablonTuru";
            this.ucEnumGosterSablonTuru.Size = new System.Drawing.Size(237, 20);
            this.ucEnumGosterSablonTuru.TabIndex = 1;
            // 
            // frmLokasyonListesi
            // 
            this.ClientSize = new System.Drawing.Size(983, 545);
            this.Name = "frmLokasyonListesi";
            ((System.ComponentModel.ISupportInitialize)(this.Islemler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextEditAdi.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit TextEditAdi;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private UcEnumGoster ucEnumGosterSablonTuru;
    }
}
