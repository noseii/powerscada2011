using PowerScada;
namespace PowerScada
{
    partial class frmLookupTable
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
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.myComboTipi = new PowerScada.MyCombo();
            this.memoEditAciklama = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.textEditkodu = new DevExpress.XtraEditors.TextEdit();
            this.labelControlSablonTuru = new DevExpress.XtraEditors.LabelControl();
            this.labelControlSablonAdi = new DevExpress.XtraEditors.LabelControl();
            this.textEditAdi = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.SaveInformationexpando)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelust)).BeginInit();
            this.panelust.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditAciklama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditkodu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditAdi.Properties)).BeginInit();
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
            this.panelust.Controls.Add(this.labelControl2);
            this.panelust.Controls.Add(this.myComboTipi);
            this.panelust.Controls.Add(this.memoEditAciklama);
            this.panelust.Controls.Add(this.labelControl1);
            this.panelust.Controls.Add(this.textEditkodu);
            this.panelust.Controls.Add(this.labelControlSablonTuru);
            this.panelust.Controls.Add(this.labelControlSablonAdi);
            this.panelust.Controls.Add(this.textEditAdi);
            this.panelust.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelust.Location = new System.Drawing.Point(0, 0);
            this.panelust.Name = "panelust";
            this.panelust.Size = new System.Drawing.Size(810, 545);
            this.panelust.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(65, 111);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(14, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Tip";
            // 
            // myComboTipi
            // 
            this.myComboTipi.BindingTürü = PowerScada.MyCombo.Binding.Enum;
            this.myComboTipi.DisplayField = "";
            this.myComboTipi.EmptyMessage = "";
            this.myComboTipi.EmptyRow = false;
            this.myComboTipi.EntityName = "";
            this.myComboTipi.EnumTipi = "ParametreTipi";
            this.myComboTipi.Id = 0;
            this.myComboTipi.Location = new System.Drawing.Point(150, 103);
            this.myComboTipi.Name = "myComboTipi";
            this.myComboTipi.SelectedIndex = -1;
            this.myComboTipi.Size = new System.Drawing.Size(150, 21);
            this.myComboTipi.TabIndex = 5;
            this.myComboTipi.WhereClause = null;
            // 
            // memoEditAciklama
            // 
            this.memoEditAciklama.Location = new System.Drawing.Point(150, 137);
            this.memoEditAciklama.Name = "memoEditAciklama";
            this.memoEditAciklama.Size = new System.Drawing.Size(401, 150);
            this.memoEditAciklama.TabIndex = 7;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(65, 139);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(41, 13);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "Açıklama";
            // 
            // textEditkodu
            // 
            this.textEditkodu.Location = new System.Drawing.Point(150, 48);
            this.textEditkodu.Name = "textEditkodu";
            this.textEditkodu.Size = new System.Drawing.Size(197, 20);
            this.textEditkodu.TabIndex = 1;
            // 
            // labelControlSablonTuru
            // 
            this.labelControlSablonTuru.Location = new System.Drawing.Point(65, 55);
            this.labelControlSablonTuru.Name = "labelControlSablonTuru";
            this.labelControlSablonTuru.Size = new System.Drawing.Size(24, 13);
            this.labelControlSablonTuru.TabIndex = 0;
            this.labelControlSablonTuru.Text = "Kodu";
            // 
            // labelControlSablonAdi
            // 
            this.labelControlSablonAdi.Location = new System.Drawing.Point(65, 81);
            this.labelControlSablonAdi.Name = "labelControlSablonAdi";
            this.labelControlSablonAdi.Size = new System.Drawing.Size(15, 13);
            this.labelControlSablonAdi.TabIndex = 2;
            this.labelControlSablonAdi.Text = "Adı";
            // 
            // textEditAdi
            // 
            this.textEditAdi.Location = new System.Drawing.Point(150, 74);
            this.textEditAdi.Name = "textEditAdi";
            this.textEditAdi.Size = new System.Drawing.Size(197, 20);
            this.textEditAdi.TabIndex = 3;
            // 
            // frmLookupTable
            // 
            this.ClientSize = new System.Drawing.Size(983, 545);
            this.Name = "frmLookupTable";
            ((System.ComponentModel.ISupportInitialize)(this.SaveInformationexpando)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelust)).EndInit();
            this.panelust.ResumeLayout(false);
            this.panelust.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditAciklama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditkodu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditAdi.Properties)).EndInit();
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
        private MyCombo myComboTipi;

    }
}
