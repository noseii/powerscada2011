namespace PowerScada
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.lblVersion = new System.Windows.Forms.Label();
            this.edtsifre = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.edtkullanici = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblEnvironment = new System.Windows.Forms.Label();
            this.chbRemember = new System.Windows.Forms.CheckBox();
            this.plnMain = new System.Windows.Forms.Panel();
            this.lblyeni = new System.Windows.Forms.LinkLabel();
            this.grd = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.edtexp64 = new System.Windows.Forms.LinkLabel();
            this.edtman = new System.Windows.Forms.LinkLabel();
            this.edtexp = new System.Windows.Forms.LinkLabel();
            this.btnattach = new System.Windows.Forms.Button();
            this.btncreate = new System.Windows.Forms.Button();
            this.cmbsqls = new System.Windows.Forms.ComboBox();
            this.wb = new System.Windows.Forms.WebBrowser();
            this.dird = new System.Windows.Forms.FolderBrowserDialog();
            this.plnMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
            this.SuspendLayout();
            // 
            // lblVersion
            // 
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.White;
            this.lblVersion.Location = new System.Drawing.Point(360, 94);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(116, 16);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "version 1.00.01";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // edtsifre
            // 
            this.edtsifre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.edtsifre.Location = new System.Drawing.Point(89, 276);
            this.edtsifre.Name = "edtsifre";
            this.edtsifre.PasswordChar = '*';
            this.edtsifre.Size = new System.Drawing.Size(109, 20);
            this.edtsifre.TabIndex = 6;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.BackColor = System.Drawing.Color.Transparent;
            this.lblUsername.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.ForeColor = System.Drawing.Color.Black;
            this.lblUsername.Location = new System.Drawing.Point(3, 252);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(80, 13);
            this.lblUsername.TabIndex = 3;
            this.lblUsername.Text = "&Kullanici Adi :";
            // 
            // edtkullanici
            // 
            this.edtkullanici.Location = new System.Drawing.Point(89, 249);
            this.edtkullanici.Name = "edtkullanici";
            this.edtkullanici.Size = new System.Drawing.Size(109, 21);
            this.edtkullanici.TabIndex = 4;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblPassword.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = System.Drawing.Color.Black;
            this.lblPassword.Location = new System.Drawing.Point(3, 279);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(39, 13);
            this.lblPassword.TabIndex = 5;
            this.lblPassword.Text = "&Şifre :";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(89, 329);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 33);
            this.button1.TabIndex = 10;
            this.button1.Text = "&Tamam";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(189, 329);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 33);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "&Vazgeç";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // lblEnvironment
            // 
            this.lblEnvironment.AutoSize = true;
            this.lblEnvironment.BackColor = System.Drawing.Color.Transparent;
            this.lblEnvironment.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnvironment.ForeColor = System.Drawing.Color.Black;
            this.lblEnvironment.Location = new System.Drawing.Point(3, 305);
            this.lblEnvironment.Name = "lblEnvironment";
            this.lblEnvironment.Size = new System.Drawing.Size(77, 13);
            this.lblEnvironment.TabIndex = 7;
            this.lblEnvironment.Text = "Veri Kaynağı";
            // 
            // chbRemember
            // 
            this.chbRemember.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chbRemember.BackColor = System.Drawing.Color.Transparent;
            this.chbRemember.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbRemember.ForeColor = System.Drawing.Color.Black;
            this.chbRemember.Location = new System.Drawing.Point(285, 331);
            this.chbRemember.Name = "chbRemember";
            this.chbRemember.Size = new System.Drawing.Size(66, 30);
            this.chbRemember.TabIndex = 9;
            this.chbRemember.Text = "&Beni Hatırla";
            this.chbRemember.UseVisualStyleBackColor = false;
            // 
            // plnMain
            // 
            this.plnMain.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.plnMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.plnMain.Controls.Add(this.lblyeni);
            this.plnMain.Controls.Add(this.grd);
            this.plnMain.Controls.Add(this.label1);
            this.plnMain.Controls.Add(this.edtexp64);
            this.plnMain.Controls.Add(this.edtman);
            this.plnMain.Controls.Add(this.edtexp);
            this.plnMain.Controls.Add(this.btnattach);
            this.plnMain.Controls.Add(this.btncreate);
            this.plnMain.Controls.Add(this.cmbsqls);
            this.plnMain.Controls.Add(this.chbRemember);
            this.plnMain.Controls.Add(this.lblEnvironment);
            this.plnMain.Controls.Add(this.btnCancel);
            this.plnMain.Controls.Add(this.button1);
            this.plnMain.Controls.Add(this.lblPassword);
            this.plnMain.Controls.Add(this.edtkullanici);
            this.plnMain.Controls.Add(this.lblUsername);
            this.plnMain.Controls.Add(this.edtsifre);
            this.plnMain.Controls.Add(this.lblVersion);
            this.plnMain.Controls.Add(this.wb);
            this.plnMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plnMain.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.plnMain.Location = new System.Drawing.Point(0, 0);
            this.plnMain.Name = "plnMain";
            this.plnMain.Size = new System.Drawing.Size(750, 422);
            this.plnMain.TabIndex = 0;
            // 
            // lblyeni
            // 
            this.lblyeni.AutoSize = true;
            this.lblyeni.BackColor = System.Drawing.Color.Transparent;
            this.lblyeni.Font = new System.Drawing.Font("Tahoma", 7F);
            this.lblyeni.LinkColor = System.Drawing.Color.MidnightBlue;
            this.lblyeni.Location = new System.Drawing.Point(361, 112);
            this.lblyeni.Name = "lblyeni";
            this.lblyeni.Size = new System.Drawing.Size(127, 12);
            this.lblyeni.TabIndex = 25;
            this.lblyeni.TabStop = true;
            this.lblyeni.Tag = "";
            this.lblyeni.Text = "Bu versiyonda neler var>>";
            this.lblyeni.Visible = false;
            // 
            // grd
            // 
            this.grd.AllowUserToAddRows = false;
            this.grd.AllowUserToDeleteRows = false;
            this.grd.AllowUserToOrderColumns = true;
            this.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grd.Location = new System.Drawing.Point(363, 130);
            this.grd.Name = "grd";
            this.grd.ReadOnly = true;
            this.grd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grd.Size = new System.Drawing.Size(387, 292);
            this.grd.TabIndex = 24;
            this.grd.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(316, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(405, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Aşağıdaki linklere tıklayarak indirebilir (Yönetici olarak) kurabilirsiniz. :";
            // 
            // edtexp64
            // 
            this.edtexp64.AutoSize = true;
            this.edtexp64.BackColor = System.Drawing.Color.Transparent;
            this.edtexp64.LinkColor = System.Drawing.Color.MidnightBlue;
            this.edtexp64.Location = new System.Drawing.Point(316, 31);
            this.edtexp64.Name = "edtexp64";
            this.edtexp64.Size = new System.Drawing.Size(165, 13);
            this.edtexp64.TabIndex = 21;
            this.edtexp64.TabStop = true;
            this.edtexp64.Tag = "";
            this.edtexp64.Text = "Microsoft SqlExpress(64Bit) 2005";
            // 
            // edtman
            // 
            this.edtman.AutoSize = true;
            this.edtman.BackColor = System.Drawing.Color.Transparent;
            this.edtman.LinkColor = System.Drawing.Color.MidnightBlue;
            this.edtman.Location = new System.Drawing.Point(487, 18);
            this.edtman.Name = "edtman";
            this.edtman.Size = new System.Drawing.Size(134, 13);
            this.edtman.TabIndex = 19;
            this.edtman.TabStop = true;
            this.edtman.Tag = "";
            this.edtman.Text = "Sql Maneger Yönetim Aracı";
            // 
            // edtexp
            // 
            this.edtexp.AutoSize = true;
            this.edtexp.BackColor = System.Drawing.Color.Transparent;
            this.edtexp.LinkColor = System.Drawing.Color.MidnightBlue;
            this.edtexp.Location = new System.Drawing.Point(316, 18);
            this.edtexp.Name = "edtexp";
            this.edtexp.Size = new System.Drawing.Size(165, 13);
            this.edtexp.TabIndex = 17;
            this.edtexp.TabStop = true;
            this.edtexp.Tag = "";
            this.edtexp.Text = "Microsoft SqlExpress(32Bit) 2005";
            // 
            // btnattach
            // 
            this.btnattach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnattach.BackColor = System.Drawing.Color.Transparent;
            this.btnattach.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnattach.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnattach.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnattach.ForeColor = System.Drawing.Color.Black;
            this.btnattach.Image = ((System.Drawing.Image)(resources.GetObject("btnattach.Image")));
            this.btnattach.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnattach.Location = new System.Drawing.Point(89, 368);
            this.btnattach.Name = "btnattach";
            this.btnattach.Size = new System.Drawing.Size(256, 24);
            this.btnattach.TabIndex = 16;
            this.btnattach.Text = "&Mevcut Bir Veri Tabanına Bağlan";
            this.btnattach.UseVisualStyleBackColor = false;
            this.btnattach.Visible = false;
            // 
            // btncreate
            // 
            this.btncreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btncreate.BackColor = System.Drawing.Color.Transparent;
            this.btncreate.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btncreate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btncreate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btncreate.ForeColor = System.Drawing.Color.Black;
            this.btncreate.Image = ((System.Drawing.Image)(resources.GetObject("btncreate.Image")));
            this.btncreate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btncreate.Location = new System.Drawing.Point(89, 395);
            this.btncreate.Name = "btncreate";
            this.btncreate.Size = new System.Drawing.Size(256, 24);
            this.btncreate.TabIndex = 15;
            this.btncreate.Text = "&Yeni bir Veri Tabani Oluştur";
            this.btncreate.UseVisualStyleBackColor = false;
            this.btncreate.Visible = false;
            // 
            // cmbsqls
            // 
            this.cmbsqls.FormattingEnabled = true;
            this.cmbsqls.Location = new System.Drawing.Point(89, 302);
            this.cmbsqls.Name = "cmbsqls";
            this.cmbsqls.Size = new System.Drawing.Size(256, 21);
            this.cmbsqls.TabIndex = 14;
            // 
            // wb
            // 
            this.wb.Location = new System.Drawing.Point(755, 429);
            this.wb.MinimumSize = new System.Drawing.Size(20, 20);
            this.wb.Name = "wb";
            this.wb.Size = new System.Drawing.Size(122, 74);
            this.wb.TabIndex = 18;
            this.wb.Url = new System.Uri("", System.UriKind.Relative);
            // 
            // dird
            // 
            this.dird.Description = "PowerScada.mdf dosyasının olduğu dizini gösteriniz";
            // 
            // LoginForm
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(750, 422);
            this.ControlBox = false;
            this.Controls.Add(this.plnMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "LoginForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login......";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LoginForm_KeyPress);
            this.plnMain.ResumeLayout(false);
            this.plnMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.TextBox edtsifre;
        public System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox edtkullanici;
        public System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Label lblEnvironment;
        public System.Windows.Forms.CheckBox chbRemember;
        private System.Windows.Forms.Panel plnMain;
        public System.Windows.Forms.ComboBox cmbsqls;
        public System.Windows.Forms.Button btnattach;
        public System.Windows.Forms.Button btncreate;
        private System.Windows.Forms.LinkLabel edtexp;
        private System.Windows.Forms.WebBrowser wb;
        private System.Windows.Forms.LinkLabel edtman;
        private System.Windows.Forms.FolderBrowserDialog dird;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel edtexp64;
        private System.Windows.Forms.DataGridView grd;
        private System.Windows.Forms.LinkLabel lblyeni;

    }
}