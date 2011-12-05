namespace AHBS2010
{
    partial class frmSevk
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
            this.lbkurum = new DevExpress.XtraEditors.ListBoxControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lbbolum = new DevExpress.XtraEditors.ListBoxControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.edttarih = new DevExpress.XtraEditors.DateEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlbuttonss)).BeginInit();
            this.pnlbuttonss.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl112s)).BeginInit();
            this.panelControl112s.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lbkurum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbbolum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edttarih.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edttarih.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlbuttonss
            // 
            this.pnlbuttonss.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pnlbuttonss.Appearance.Options.UseBackColor = true;
            this.pnlbuttonss.Location = new System.Drawing.Point(0, 384);
            this.pnlbuttonss.Size = new System.Drawing.Size(778, 52);
            this.pnlbuttonss.TabIndex = 11;
            // 
            // panelControl112s
            // 
            this.panelControl112s.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControl112s.Appearance.Options.UseBackColor = true;
            this.panelControl112s.Size = new System.Drawing.Size(774, 48);
            // 
            // groupBoxHastaBilgileri
            // 
            this.groupBoxHastaBilgileri.Size = new System.Drawing.Size(778, 51);
            // 
            // lbkurum
            // 
            this.lbkurum.Location = new System.Drawing.Point(15, 115);
            this.lbkurum.Name = "lbkurum";
            this.lbkurum.Size = new System.Drawing.Size(369, 263);
            this.lbkurum.TabIndex = 8;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(15, 96);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(62, 13);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "Sevk Kurumu";
            // 
            // lbbolum
            // 
            this.lbbolum.Location = new System.Drawing.Point(390, 115);
            this.lbbolum.Name = "lbbolum";
            this.lbbolum.Size = new System.Drawing.Size(380, 263);
            this.lbbolum.TabIndex = 10;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(390, 96);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 13);
            this.labelControl2.TabIndex = 9;
            this.labelControl2.Text = "Sevk Bölümü";
            // 
            // edttarih
            // 
            this.edttarih.EditValue = null;
            this.edttarih.Location = new System.Drawing.Point(94, 57);
            this.edttarih.Name = "edttarih";
            this.edttarih.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.edttarih.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.edttarih.Size = new System.Drawing.Size(187, 20);
            this.edttarih.TabIndex = 6;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(25, 64);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(52, 13);
            this.labelControl5.TabIndex = 5;
            this.labelControl5.Text = "Sevk Tarihi";
            // 
            // frmSevk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 436);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.edttarih);
            this.Controls.Add(this.lbbolum);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.lbkurum);
            this.Controls.Add(this.labelControl1);
            this.Name = "frmSevk";
            this.Text = "frmSevk";
            this.Controls.SetChildIndex(this.groupBoxHastaBilgileri, 0);
            this.Controls.SetChildIndex(this.labelControl1, 0);
            this.Controls.SetChildIndex(this.lbkurum, 0);
            this.Controls.SetChildIndex(this.pnlbuttonss, 0);
            this.Controls.SetChildIndex(this.labelControl2, 0);
            this.Controls.SetChildIndex(this.lbbolum, 0);
            this.Controls.SetChildIndex(this.edttarih, 0);
            this.Controls.SetChildIndex(this.labelControl5, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pnlbuttonss)).EndInit();
            this.pnlbuttonss.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl112s)).EndInit();
            this.panelControl112s.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lbkurum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbbolum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edttarih.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edttarih.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ListBoxControl lbkurum;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ListBoxControl lbbolum;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit edttarih;
        private DevExpress.XtraEditors.LabelControl labelControl5;

    }
}