namespace PowerScada
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.raporlarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tanımlarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kullanıcıBilgileriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rolEkranHakkiTanimiMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lokasyonTanımıToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ilTanımıToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ilçeTanımıToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cihazListesiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tanımListesiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adresListesiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yenidenOluşturToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kullaniciAyarlariToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.denemeFormuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opcİzlemeFormuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miniToolStrip = new System.Windows.Forms.StatusStrip();
            this.lbltarih = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblaktifkullanici = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbldoktorgorevililce = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlstatus = new System.Windows.Forms.Panel();
            this.cmbaktifdoktor = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tt = new System.Windows.Forms.Timer(this.components);
            this.cihazİzlemEkranıToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.pnlstatus.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.raporlarToolStripMenuItem,
            this.tanımlarToolStripMenuItem,
            this.sysToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1028, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // raporlarToolStripMenuItem
            // 
            this.raporlarToolStripMenuItem.Name = "raporlarToolStripMenuItem";
            this.raporlarToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.raporlarToolStripMenuItem.Text = "Raporlar";
            // 
            // tanımlarToolStripMenuItem
            // 
            this.tanımlarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kullanıcıBilgileriToolStripMenuItem,
            this.rolEkranHakkiTanimiMenuItem,
            this.lokasyonTanımıToolStripMenuItem,
            this.ilTanımıToolStripMenuItem,
            this.ilçeTanımıToolStripMenuItem,
            this.cihazListesiToolStripMenuItem,
            this.tanımListesiToolStripMenuItem,
            this.adresListesiToolStripMenuItem,
            this.cihazİzlemEkranıToolStripMenuItem});
            this.tanımlarToolStripMenuItem.Name = "tanımlarToolStripMenuItem";
            this.tanımlarToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.tanımlarToolStripMenuItem.Tag = "Tanimlar";
            this.tanımlarToolStripMenuItem.Text = "Tanımlar";
            // 
            // kullanıcıBilgileriToolStripMenuItem
            // 
            this.kullanıcıBilgileriToolStripMenuItem.Name = "kullanıcıBilgileriToolStripMenuItem";
            this.kullanıcıBilgileriToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.kullanıcıBilgileriToolStripMenuItem.Tag = "Kullanici";
            this.kullanıcıBilgileriToolStripMenuItem.Text = "Kullanıcı Bilgileri";
            // 
            // rolEkranHakkiTanimiMenuItem
            // 
            this.rolEkranHakkiTanimiMenuItem.Name = "rolEkranHakkiTanimiMenuItem";
            this.rolEkranHakkiTanimiMenuItem.Size = new System.Drawing.Size(191, 22);
            this.rolEkranHakkiTanimiMenuItem.Tag = "RolEkranHakki";
            this.rolEkranHakkiTanimiMenuItem.Text = "Rol Ekran Hakki Tanımı";
            // 
            // lokasyonTanımıToolStripMenuItem
            // 
            this.lokasyonTanımıToolStripMenuItem.Name = "lokasyonTanımıToolStripMenuItem";
            this.lokasyonTanımıToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.lokasyonTanımıToolStripMenuItem.Text = "Lokasyon Listesi";
            this.lokasyonTanımıToolStripMenuItem.Click += new System.EventHandler(this.lokasyonTanımıToolStripMenuItem_Click);
            // 
            // ilTanımıToolStripMenuItem
            // 
            this.ilTanımıToolStripMenuItem.Name = "ilTanımıToolStripMenuItem";
            this.ilTanımıToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.ilTanımıToolStripMenuItem.Text = "İl Listesi";
            // 
            // ilçeTanımıToolStripMenuItem
            // 
            this.ilçeTanımıToolStripMenuItem.Name = "ilçeTanımıToolStripMenuItem";
            this.ilçeTanımıToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.ilçeTanımıToolStripMenuItem.Text = "İlçe Tanımı";
            // 
            // cihazListesiToolStripMenuItem
            // 
            this.cihazListesiToolStripMenuItem.Name = "cihazListesiToolStripMenuItem";
            this.cihazListesiToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.cihazListesiToolStripMenuItem.Text = "Cihaz Listesi";
            this.cihazListesiToolStripMenuItem.Click += new System.EventHandler(this.cihazListesiToolStripMenuItem_Click);
            // 
            // tanımListesiToolStripMenuItem
            // 
            this.tanımListesiToolStripMenuItem.Name = "tanımListesiToolStripMenuItem";
            this.tanımListesiToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.tanımListesiToolStripMenuItem.Text = "Tanım Listesi";
            this.tanımListesiToolStripMenuItem.Click += new System.EventHandler(this.tanımListesiToolStripMenuItem_Click);
            // 
            // adresListesiToolStripMenuItem
            // 
            this.adresListesiToolStripMenuItem.Name = "adresListesiToolStripMenuItem";
            this.adresListesiToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.adresListesiToolStripMenuItem.Text = "Adres Listesi";
            this.adresListesiToolStripMenuItem.Click += new System.EventHandler(this.adresListesiToolStripMenuItem_Click);
            // 
            // sysToolStripMenuItem
            // 
            this.sysToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.yenidenOluşturToolStripMenuItem,
            this.kullaniciAyarlariToolStripMenuItem,
            this.denemeFormuToolStripMenuItem,
            this.opcİzlemeFormuToolStripMenuItem});
            this.sysToolStripMenuItem.Name = "sysToolStripMenuItem";
            this.sysToolStripMenuItem.Size = new System.Drawing.Size(36, 20);
            this.sysToolStripMenuItem.Text = "Sys";
            // 
            // yenidenOluşturToolStripMenuItem
            // 
            this.yenidenOluşturToolStripMenuItem.Name = "yenidenOluşturToolStripMenuItem";
            this.yenidenOluşturToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.yenidenOluşturToolStripMenuItem.Text = "Yedekle";
            this.yenidenOluşturToolStripMenuItem.Click += new System.EventHandler(this.yenidenOluşturToolStripMenuItem_Click);
            // 
            // kullaniciAyarlariToolStripMenuItem
            // 
            this.kullaniciAyarlariToolStripMenuItem.Name = "kullaniciAyarlariToolStripMenuItem";
            this.kullaniciAyarlariToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.kullaniciAyarlariToolStripMenuItem.Text = "Kullanıcı Ayarları";
            // 
            // denemeFormuToolStripMenuItem
            // 
            this.denemeFormuToolStripMenuItem.Name = "denemeFormuToolStripMenuItem";
            this.denemeFormuToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.denemeFormuToolStripMenuItem.Text = "Deneme Formu";
            this.denemeFormuToolStripMenuItem.Click += new System.EventHandler(this.denemeFormuToolStripMenuItem_Click);
            // 
            // opcİzlemeFormuToolStripMenuItem
            // 
            this.opcİzlemeFormuToolStripMenuItem.Name = "opcİzlemeFormuToolStripMenuItem";
            this.opcİzlemeFormuToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.opcİzlemeFormuToolStripMenuItem.Text = "Opc İzleme Formu";
            this.opcİzlemeFormuToolStripMenuItem.Click += new System.EventHandler(this.opcİzlemeFormuToolStripMenuItem_Click);
            // 
            // miniToolStrip
            // 
            this.miniToolStrip.AutoSize = false;
            this.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.miniToolStrip.Location = new System.Drawing.Point(785, 1);
            this.miniToolStrip.Name = "miniToolStrip";
            this.miniToolStrip.Size = new System.Drawing.Size(1192, 22);
            this.miniToolStrip.TabIndex = 10;
            // 
            // lbltarih
            // 
            this.lbltarih.AutoSize = false;
            this.lbltarih.Image = ((System.Drawing.Image)(resources.GetObject("lbltarih.Image")));
            this.lbltarih.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbltarih.Name = "lbltarih";
            this.lbltarih.Size = new System.Drawing.Size(150, 17);
            this.lbltarih.Text = "Tarih:";
            // 
            // lblaktifkullanici
            // 
            this.lblaktifkullanici.AutoSize = false;
            this.lblaktifkullanici.Image = ((System.Drawing.Image)(resources.GetObject("lblaktifkullanici.Image")));
            this.lblaktifkullanici.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblaktifkullanici.Name = "lblaktifkullanici";
            this.lblaktifkullanici.Size = new System.Drawing.Size(250, 17);
            this.lblaktifkullanici.Text = "Aktif Kullanıcı:";
            // 
            // lbldoktorgorevililce
            // 
            this.lbldoktorgorevililce.AutoSize = false;
            this.lbldoktorgorevililce.Image = ((System.Drawing.Image)(resources.GetObject("lbldoktorgorevililce.Image")));
            this.lbldoktorgorevililce.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbldoktorgorevililce.Name = "lbldoktorgorevililce";
            this.lbldoktorgorevililce.Size = new System.Drawing.Size(400, 17);
            this.lbldoktorgorevililce.Text = "Doktor Görev İl/İlçe:";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStatusLabel2.Image")));
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(84, 17);
            this.toolStripStatusLabel2.Text = "Aktif Doktor:";
            // 
            // pnlstatus
            // 
            this.pnlstatus.Controls.Add(this.cmbaktifdoktor);
            this.pnlstatus.Controls.Add(this.statusStrip1);
            this.pnlstatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlstatus.Location = new System.Drawing.Point(0, 504);
            this.pnlstatus.Name = "pnlstatus";
            this.pnlstatus.Size = new System.Drawing.Size(1028, 22);
            this.pnlstatus.TabIndex = 6;
            // 
            // cmbaktifdoktor
            // 
            this.cmbaktifdoktor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbaktifdoktor.FormattingEnabled = true;
            this.cmbaktifdoktor.Location = new System.Drawing.Point(884, 1);
            this.cmbaktifdoktor.Name = "cmbaktifdoktor";
            this.cmbaktifdoktor.Size = new System.Drawing.Size(274, 21);
            this.cmbaktifdoktor.TabIndex = 11;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbltarih,
            this.lblaktifkullanici,
            this.lbldoktorgorevililce,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1028, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // cihazİzlemEkranıToolStripMenuItem
            // 
            this.cihazİzlemEkranıToolStripMenuItem.Name = "cihazİzlemEkranıToolStripMenuItem";
            this.cihazİzlemEkranıToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.cihazİzlemEkranıToolStripMenuItem.Text = "Cihaz İzlem Ekranı";
            this.cihazİzlemEkranıToolStripMenuItem.Click += new System.EventHandler(this.cihazİzlemEkranıToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 526);
            this.Controls.Add(this.pnlstatus);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Power Scada";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlstatus.ResumeLayout(false);
            this.pnlstatus.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tanımlarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kullanıcıBilgileriToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem raporlarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yenidenOluşturToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rolEkranHakkiTanimiMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kullaniciAyarlariToolStripMenuItem;
        private System.Windows.Forms.StatusStrip miniToolStrip;
        private System.Windows.Forms.ToolStripStatusLabel lbltarih;
        private System.Windows.Forms.ToolStripStatusLabel lblaktifkullanici;
        private System.Windows.Forms.ToolStripStatusLabel lbldoktorgorevililce;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Panel pnlstatus;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ComboBox cmbaktifdoktor;
        public System.Windows.Forms.Timer tt;
        private System.Windows.Forms.ToolStripMenuItem lokasyonTanımıToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ilTanımıToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ilçeTanımıToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem denemeFormuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opcİzlemeFormuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cihazListesiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tanımListesiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adresListesiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cihazİzlemEkranıToolStripMenuItem;



    }
}

