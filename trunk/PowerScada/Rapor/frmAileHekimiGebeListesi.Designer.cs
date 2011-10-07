namespace AHBS2010.Rapor
{
    partial class frmAileHekimiGebeListesi
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
            this.rdbtnDetaysiz = new System.Windows.Forms.RadioButton();
            this.rdbtnDetayli = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdDogumTarihi = new System.Windows.Forms.RadioButton();
            this.rdSoyadı = new System.Windows.Forms.RadioButton();
            this.rdAdi = new System.Windows.Forms.RadioButton();
            this.rdtckimlikNo = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdazalan = new System.Windows.Forms.RadioButton();
            this.rdartan = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelUst)).BeginInit();
            this.panelUst.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditRaporTarihi.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditRaporTarihi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelAna)).BeginInit();
            this.panelAna.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelUst
            // 
            this.panelUst.Controls.Add(this.groupBox3);
            this.panelUst.Controls.Add(this.groupBox2);
            this.panelUst.Controls.Add(this.groupBox1);
            this.panelUst.Size = new System.Drawing.Size(767, 233);
            this.panelUst.Controls.SetChildIndex(this.labelRaporTarihi, 0);
            this.panelUst.Controls.SetChildIndex(this.dateEditRaporTarihi, 0);
            this.panelUst.Controls.SetChildIndex(this.simpleButtonGoruntule, 0);
            this.panelUst.Controls.SetChildIndex(this.simpleButtonExceleAktar, 0);
            this.panelUst.Controls.SetChildIndex(this.simpleButtonCikis, 0);
            this.panelUst.Controls.SetChildIndex(this.groupBox1, 0);
            this.panelUst.Controls.SetChildIndex(this.groupBox2, 0);
            this.panelUst.Controls.SetChildIndex(this.groupBox3, 0);
            // 
            // simpleButtonCikis
            // 
            this.simpleButtonCikis.Location = new System.Drawing.Point(279, 201);
            // 
            // simpleButtonExceleAktar
            // 
            this.simpleButtonExceleAktar.Location = new System.Drawing.Point(144, 201);
            // 
            // simpleButtonGoruntule
            // 
            this.simpleButtonGoruntule.Location = new System.Drawing.Point(10, 201);
            // 
            // dateEditRaporTarihi
            // 
            this.dateEditRaporTarihi.EditValue = null;
            this.dateEditRaporTarihi.Enabled = false;
            this.dateEditRaporTarihi.Location = new System.Drawing.Point(607, 8);
            this.dateEditRaporTarihi.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditRaporTarihi.Size = new System.Drawing.Size(155, 20);
            // 
            // labelRaporTarihi
            // 
            this.labelRaporTarihi.Location = new System.Drawing.Point(539, 12);
            // 
            // panelAna
            // 
            this.panelAna.Size = new System.Drawing.Size(771, 431);
            // 
            // panelControl1
            // 
            this.panelControl1.Location = new System.Drawing.Point(2, 235);
            this.panelControl1.Size = new System.Drawing.Size(767, 194);
            // 
            // rdbtnDetaysiz
            // 
            this.rdbtnDetaysiz.AutoSize = true;
            this.rdbtnDetaysiz.Checked = true;
            this.rdbtnDetaysiz.Location = new System.Drawing.Point(10, 13);
            this.rdbtnDetaysiz.Name = "rdbtnDetaysiz";
            this.rdbtnDetaysiz.Size = new System.Drawing.Size(93, 17);
            this.rdbtnDetaysiz.TabIndex = 7;
            this.rdbtnDetaysiz.TabStop = true;
            this.rdbtnDetaysiz.Text = "Özet Görünüm";
            this.rdbtnDetaysiz.UseVisualStyleBackColor = true;
            // 
            // rdbtnDetayli
            // 
            this.rdbtnDetayli.AutoSize = true;
            this.rdbtnDetayli.Location = new System.Drawing.Point(109, 13);
            this.rdbtnDetayli.Name = "rdbtnDetayli";
            this.rdbtnDetayli.Size = new System.Drawing.Size(103, 17);
            this.rdbtnDetayli.TabIndex = 8;
            this.rdbtnDetayli.TabStop = true;
            this.rdbtnDetayli.Text = "Detaylı Görünüm";
            this.rdbtnDetayli.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbtnDetayli);
            this.groupBox1.Controls.Add(this.rdbtnDetaysiz);
            this.groupBox1.Location = new System.Drawing.Point(11, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(223, 42);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rapor Görünümü";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdDogumTarihi);
            this.groupBox2.Controls.Add(this.rdSoyadı);
            this.groupBox2.Controls.Add(this.rdAdi);
            this.groupBox2.Controls.Add(this.rdtckimlikNo);
            this.groupBox2.Location = new System.Drawing.Point(10, 79);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(301, 42);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sıralama";
            // 
            // rdDogumTarihi
            // 
            this.rdDogumTarihi.AutoSize = true;
            this.rdDogumTarihi.Location = new System.Drawing.Point(206, 13);
            this.rdDogumTarihi.Name = "rdDogumTarihi";
            this.rdDogumTarihi.Size = new System.Drawing.Size(88, 17);
            this.rdDogumTarihi.TabIndex = 11;
            this.rdDogumTarihi.Text = "Doğum Tarihi";
            this.rdDogumTarihi.UseVisualStyleBackColor = true;
            // 
            // rdSoyadı
            // 
            this.rdSoyadı.AutoSize = true;
            this.rdSoyadı.Location = new System.Drawing.Point(143, 13);
            this.rdSoyadı.Name = "rdSoyadı";
            this.rdSoyadı.Size = new System.Drawing.Size(57, 17);
            this.rdSoyadı.TabIndex = 10;
            this.rdSoyadı.Text = "Soyadı";
            this.rdSoyadı.UseVisualStyleBackColor = true;
            // 
            // rdAdi
            // 
            this.rdAdi.AutoSize = true;
            this.rdAdi.Location = new System.Drawing.Point(97, 13);
            this.rdAdi.Name = "rdAdi";
            this.rdAdi.Size = new System.Drawing.Size(40, 17);
            this.rdAdi.TabIndex = 9;
            this.rdAdi.Text = "Adı";
            this.rdAdi.UseVisualStyleBackColor = true;
            // 
            // rdtckimlikNo
            // 
            this.rdtckimlikNo.AutoSize = true;
            this.rdtckimlikNo.Checked = true;
            this.rdtckimlikNo.Location = new System.Drawing.Point(6, 13);
            this.rdtckimlikNo.Name = "rdtckimlikNo";
            this.rdtckimlikNo.Size = new System.Drawing.Size(85, 17);
            this.rdtckimlikNo.TabIndex = 8;
            this.rdtckimlikNo.TabStop = true;
            this.rdtckimlikNo.Text = "Tc Kimlik No";
            this.rdtckimlikNo.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdazalan);
            this.groupBox3.Controls.Add(this.rdartan);
            this.groupBox3.Location = new System.Drawing.Point(320, 79);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(119, 42);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tipi";
            // 
            // rdazalan
            // 
            this.rdazalan.AutoSize = true;
            this.rdazalan.Location = new System.Drawing.Point(56, 13);
            this.rdazalan.Name = "rdazalan";
            this.rdazalan.Size = new System.Drawing.Size(57, 17);
            this.rdazalan.TabIndex = 10;
            this.rdazalan.Text = "Azalan";
            this.rdazalan.UseVisualStyleBackColor = true;
            // 
            // rdartan
            // 
            this.rdartan.AutoSize = true;
            this.rdartan.Checked = true;
            this.rdartan.Location = new System.Drawing.Point(6, 13);
            this.rdartan.Name = "rdartan";
            this.rdartan.Size = new System.Drawing.Size(50, 17);
            this.rdartan.TabIndex = 9;
            this.rdartan.TabStop = true;
            this.rdartan.Text = "Artan";
            this.rdartan.UseVisualStyleBackColor = true;
            // 
            // frmAileHekimiGebeListesi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 431);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmAileHekimiGebeListesi";
            this.Text = "Gebe Listesi";
            ((System.ComponentModel.ISupportInitialize)(this.panelUst)).EndInit();
            this.panelUst.ResumeLayout(false);
            this.panelUst.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditRaporTarihi.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditRaporTarihi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelAna)).EndInit();
            this.panelAna.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rdbtnDetaysiz;
        private System.Windows.Forms.RadioButton rdbtnDetayli;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdDogumTarihi;
        private System.Windows.Forms.RadioButton rdSoyadı;
        private System.Windows.Forms.RadioButton rdAdi;
        private System.Windows.Forms.RadioButton rdtckimlikNo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdazalan;
        private System.Windows.Forms.RadioButton rdartan;
    }
}