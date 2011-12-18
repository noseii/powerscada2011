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
            this.panelust = new DevExpress.XtraEditors.PanelControl();
            this.labelControlAdi = new DevExpress.XtraEditors.LabelControl();
            this.textEditSablonAdi = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.SaveInformationexpando)).BeginInit();
            this.panel1.SuspendLayout();
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
            this.panel1.Controls.Add(this.panelust);
            this.panel1.Size = new System.Drawing.Size(810, 545);
            // 
            // panelust
            // 
            this.panelust.Controls.Add(this.labelControlAdi);
            this.panelust.Controls.Add(this.textEditSablonAdi);
            this.panelust.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelust.Location = new System.Drawing.Point(0, 0);
            this.panelust.Name = "panelust";
            this.panelust.Size = new System.Drawing.Size(810, 545);
            this.panelust.TabIndex = 2;
            // 
            // labelControlAdi
            // 
            this.labelControlAdi.Location = new System.Drawing.Point(65, 68);
            this.labelControlAdi.Name = "labelControlAdi";
            this.labelControlAdi.Size = new System.Drawing.Size(51, 13);
            this.labelControlAdi.TabIndex = 0;
            this.labelControlAdi.Text = "Tag Adresi";
            // 
            // textEditSablonAdi
            // 
            this.textEditSablonAdi.Location = new System.Drawing.Point(150, 61);
            this.textEditSablonAdi.Name = "textEditSablonAdi";
            this.textEditSablonAdi.Size = new System.Drawing.Size(197, 20);
            this.textEditSablonAdi.TabIndex = 1;
            // 
            // frmAdres
            // 
            this.ClientSize = new System.Drawing.Size(983, 545);
            this.Name = "frmAdres";
            ((System.ComponentModel.ISupportInitialize)(this.SaveInformationexpando)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelust)).EndInit();
            this.panelust.ResumeLayout(false);
            this.panelust.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditSablonAdi.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelust;
        private DevExpress.XtraEditors.LabelControl labelControlAdi;
        private DevExpress.XtraEditors.TextEdit textEditSablonAdi;

    }
}
