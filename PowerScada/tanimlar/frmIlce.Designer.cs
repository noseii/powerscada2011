namespace PowerScada
{
    partial class frmIlce
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
            this.textEditAdi = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.editButtonIl = new PowerScada.EditButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelbutton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paneldetay)).BeginInit();
            this.paneldetay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.formbs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paneldetayleft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paneldetayfill)).BeginInit();
            this.paneldetayfill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paneldetaybottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paneldetaytop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlgrd)).BeginInit();
            this.pnlgrd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpgrd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditAdi.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelbutton
            // 
            this.panelbutton.Appearance.BackColor = System.Drawing.Color.White;
            this.panelbutton.Appearance.Options.UseBackColor = true;
            this.panelbutton.Size = new System.Drawing.Size(692, 49);
            // 
            // paneldetay
            // 
            this.paneldetay.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.paneldetay.Appearance.Options.UseBackColor = true;
            this.paneldetay.Size = new System.Drawing.Size(692, 154);
            // 
            // sp
            // 
            this.sp.Location = new System.Drawing.Point(0, 203);
            this.sp.Size = new System.Drawing.Size(692, 6);
            // 
            // paneldetayleft
            // 
            this.paneldetayleft.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.paneldetayleft.Appearance.Options.UseBackColor = true;
            this.paneldetayleft.Size = new System.Drawing.Size(24, 154);
            // 
            // paneldetayfill
            // 
            this.paneldetayfill.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.paneldetayfill.Appearance.Options.UseBackColor = true;
            this.paneldetayfill.Controls.Add(this.editButtonIl);
            this.paneldetayfill.Controls.Add(this.labelControl2);
            this.paneldetayfill.Controls.Add(this.textEditAdi);
            this.paneldetayfill.Controls.Add(this.labelControl1);
            this.paneldetayfill.Size = new System.Drawing.Size(668, 108);
            // 
            // paneldetaybottom
            // 
            this.paneldetaybottom.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.paneldetaybottom.Appearance.Options.UseBackColor = true;
            this.paneldetaybottom.Location = new System.Drawing.Point(24, 131);
            this.paneldetaybottom.Size = new System.Drawing.Size(668, 23);
            // 
            // paneldetaytop
            // 
            this.paneldetaytop.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.paneldetaytop.Appearance.Options.UseBackColor = true;
            this.paneldetaytop.Size = new System.Drawing.Size(668, 23);
            // 
            // pnlgrd
            // 
            this.pnlgrd.Location = new System.Drawing.Point(0, 209);
            this.pnlgrd.Size = new System.Drawing.Size(692, 336);
            // 
            // grpgrd
            // 
            this.grpgrd.Size = new System.Drawing.Size(688, 332);
            // 
            // textEditAdi
            // 
            this.textEditAdi.Location = new System.Drawing.Point(122, 46);
            this.textEditAdi.Name = "textEditAdi";
            this.textEditAdi.Size = new System.Drawing.Size(437, 20);
            this.textEditAdi.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(10, 53);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(35, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "İlçe Adı";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(10, 22);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(86, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Bağlı Olduğu Şehir";
            // 
            // editButtonIl
            // 
            this.editButtonIl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editButtonIl.BackColor = System.Drawing.SystemColors.Window;
            this.editButtonIl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editButtonIl.CommandName = "EditButtonIlSec";
            this.editButtonIl.Id = ((long)(0));
            this.editButtonIl.Location = new System.Drawing.Point(122, 14);
            this.editButtonIl.Name = "editButtonIl";
            this.editButtonIl.NewValue = "";
            this.editButtonIl.OldValue = "";
            this.editButtonIl.ReadOnly = false;
            this.editButtonIl.Size = new System.Drawing.Size(275, 23);
            this.editButtonIl.TabIndex = 3;
            // 
            // frmIlce
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(692, 545);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmIlce";
            ((System.ComponentModel.ISupportInitialize)(this.panelbutton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paneldetay)).EndInit();
            this.paneldetay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.formbs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paneldetayleft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paneldetayfill)).EndInit();
            this.paneldetayfill.ResumeLayout(false);
            this.paneldetayfill.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paneldetaybottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paneldetaytop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlgrd)).EndInit();
            this.pnlgrd.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpgrd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditAdi.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit textEditAdi;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private EditButton editButtonIl;
    }
}
