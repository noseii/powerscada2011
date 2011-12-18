namespace AHBS2010
{
    partial class frmRandevu
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
            this.label2 = new System.Windows.Forms.Label();
            this.labelHasta = new System.Windows.Forms.Label();
            this.DateEditBasTarih = new DevExpress.XtraEditors.DateEdit();
            this.labelBasTarih = new System.Windows.Forms.Label();
            this.TextBoxAciklama = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBoxVekildoktor = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labelSiraNo = new System.Windows.Forms.Label();
            this.labelKayitdurumu = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelIslemTuru = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TimeEditSaat = new AHBS2010.MyTimeEdit();
            this.ucEnumGosterIzlemTuru = new AHBS2010.UcEnumGoster();
            this.ucEnumGosterIslemTuru = new AHBS2010.UcEnumGoster();
            this.ucEnumGosterDurum = new AHBS2010.UcEnumGoster();
            this.editButtonDoktor = new AHBS2010.EditButton();
            this.editButtonHasta = new AHBS2010.EditButton();
            ((System.ComponentModel.ISupportInitialize)(this.DateEditBasTarih.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEditBasTarih.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeEditSaat.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(12, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Doktor";
            // 
            // labelHasta
            // 
            this.labelHasta.AutoSize = true;
            this.labelHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelHasta.Location = new System.Drawing.Point(12, 123);
            this.labelHasta.Name = "labelHasta";
            this.labelHasta.Size = new System.Drawing.Size(49, 16);
            this.labelHasta.TabIndex = 5;
            this.labelHasta.Text = "Hasta";
            // 
            // DateEditBasTarih
            // 
            this.DateEditBasTarih.EditValue = new System.DateTime(2011, 2, 4, 17, 5, 48, 184);
            this.DateEditBasTarih.Location = new System.Drawing.Point(100, 152);
            this.DateEditBasTarih.Name = "DateEditBasTarih";
            this.DateEditBasTarih.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DateEditBasTarih.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.DateEditBasTarih.Size = new System.Drawing.Size(100, 20);
            this.DateEditBasTarih.TabIndex = 13;
            this.DateEditBasTarih.EditValueChanged += new System.EventHandler(this.DateEditBasTarih_EditValueChanged);
            // 
            // labelBasTarih
            // 
            this.labelBasTarih.AutoSize = true;
            this.labelBasTarih.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelBasTarih.Location = new System.Drawing.Point(14, 151);
            this.labelBasTarih.Name = "labelBasTarih";
            this.labelBasTarih.Size = new System.Drawing.Size(44, 16);
            this.labelBasTarih.TabIndex = 12;
            this.labelBasTarih.Text = "Tarih";
            // 
            // TextBoxAciklama
            // 
            this.TextBoxAciklama.Location = new System.Drawing.Point(100, 236);
            this.TextBoxAciklama.Multiline = true;
            this.TextBoxAciklama.Name = "TextBoxAciklama";
            this.TextBoxAciklama.Size = new System.Drawing.Size(287, 134);
            this.TextBoxAciklama.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(14, 237);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "Konu";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(14, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 14;
            this.label3.Text = "Saat";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(102, 376);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 36);
            this.button1.TabIndex = 20;
            this.button1.Text = "Randevu Kayıt ( F2 )";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(244, 376);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 36);
            this.button2.TabIndex = 21;
            this.button2.Text = "Çıkış ( F10 )";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBoxVekildoktor
            // 
            this.checkBoxVekildoktor.AutoSize = true;
            this.checkBoxVekildoktor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.checkBoxVekildoktor.ForeColor = System.Drawing.Color.Red;
            this.checkBoxVekildoktor.Location = new System.Drawing.Point(334, 100);
            this.checkBoxVekildoktor.Name = "checkBoxVekildoktor";
            this.checkBoxVekildoktor.Size = new System.Drawing.Size(122, 17);
            this.checkBoxVekildoktor.TabIndex = 4;
            this.checkBoxVekildoktor.Text = "Vekil Doktor Seç";
            this.checkBoxVekildoktor.UseVisualStyleBackColor = true;
            this.checkBoxVekildoktor.CheckedChanged += new System.EventHandler(this.checkBoxVekildoktor_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(12, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Sıra No";
            // 
            // labelSiraNo
            // 
            this.labelSiraNo.AutoSize = true;
            this.labelSiraNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelSiraNo.ForeColor = System.Drawing.Color.Red;
            this.labelSiraNo.Location = new System.Drawing.Point(97, 52);
            this.labelSiraNo.Name = "labelSiraNo";
            this.labelSiraNo.Size = new System.Drawing.Size(0, 18);
            this.labelSiraNo.TabIndex = 1;
            // 
            // labelKayitdurumu
            // 
            this.labelKayitdurumu.AutoSize = true;
            this.labelKayitdurumu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelKayitdurumu.ForeColor = System.Drawing.Color.Red;
            this.labelKayitdurumu.Location = new System.Drawing.Point(334, 130);
            this.labelKayitdurumu.Name = "labelKayitdurumu";
            this.labelKayitdurumu.Size = new System.Drawing.Size(51, 16);
            this.labelKayitdurumu.TabIndex = 7;
            this.labelKayitdurumu.Text = "label5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(14, 211);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 16);
            this.label5.TabIndex = 16;
            this.label5.Text = "Durum";
            // 
            // labelIslemTuru
            // 
            this.labelIslemTuru.AutoSize = true;
            this.labelIslemTuru.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelIslemTuru.Location = new System.Drawing.Point(12, 2);
            this.labelIslemTuru.Name = "labelIslemTuru";
            this.labelIslemTuru.Size = new System.Drawing.Size(80, 16);
            this.labelIslemTuru.TabIndex = 8;
            this.labelIslemTuru.Text = "İşlem Türü";
            this.labelIslemTuru.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(12, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "İzlem Türü";
            this.label6.Visible = false;
            // 
            // TimeEditSaat
            // 
            this.TimeEditSaat.EditValue = new System.DateTime(2011, 3, 4, 0, 0, 0, 0);
            this.TimeEditSaat.Location = new System.Drawing.Point(100, 178);
            this.TimeEditSaat.Name = "TimeEditSaat";
            this.TimeEditSaat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.TimeEditSaat.Properties.DisplayFormat.FormatString = "HH.mm";
            this.TimeEditSaat.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.TimeEditSaat.Properties.EditFormat.FormatString = "HH.mm";
            this.TimeEditSaat.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.TimeEditSaat.Properties.Mask.EditMask = "HH.mm";
            this.TimeEditSaat.Size = new System.Drawing.Size(100, 20);
            this.TimeEditSaat.TabIndex = 15;
            // 
            // ucEnumGosterIzlemTuru
            // 
            this.ucEnumGosterIzlemTuru.Deger = 0;
            this.ucEnumGosterIzlemTuru.Enabled = false;
            this.ucEnumGosterIzlemTuru.EnumTuru = "IzlemTuru";
            this.ucEnumGosterIzlemTuru.Location = new System.Drawing.Point(100, 26);
            this.ucEnumGosterIzlemTuru.Name = "ucEnumGosterIzlemTuru";
            this.ucEnumGosterIzlemTuru.Size = new System.Drawing.Size(175, 21);
            this.ucEnumGosterIzlemTuru.TabIndex = 11;
            this.ucEnumGosterIzlemTuru.Visible = false;
            // 
            // ucEnumGosterIslemTuru
            // 
            this.ucEnumGosterIslemTuru.Deger = 0;
            this.ucEnumGosterIslemTuru.EnumTuru = "IslemTuru";
            this.ucEnumGosterIslemTuru.Location = new System.Drawing.Point(100, -1);
            this.ucEnumGosterIslemTuru.Name = "ucEnumGosterIslemTuru";
            this.ucEnumGosterIslemTuru.Size = new System.Drawing.Size(175, 21);
            this.ucEnumGosterIslemTuru.TabIndex = 9;
            this.ucEnumGosterIslemTuru.Visible = false;
            this.ucEnumGosterIslemTuru.ValueChanged += new System.EventHandler(this.ucEnumGosterIslemTuru_ValueChanged);
            // 
            // ucEnumGosterDurum
            // 
            this.ucEnumGosterDurum.Deger = 0;
            this.ucEnumGosterDurum.Enabled = false;
            this.ucEnumGosterDurum.EnumTuru = "RandevuDurumu";
            this.ucEnumGosterDurum.Location = new System.Drawing.Point(100, 208);
            this.ucEnumGosterDurum.Name = "ucEnumGosterDurum";
            this.ucEnumGosterDurum.Size = new System.Drawing.Size(175, 21);
            this.ucEnumGosterDurum.TabIndex = 17;
            // 
            // editButtonDoktor
            // 
            this.editButtonDoktor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editButtonDoktor.BackColor = System.Drawing.SystemColors.Window;
            this.editButtonDoktor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editButtonDoktor.CommandName = "EditButtonDoktorSec";
            this.editButtonDoktor.Enabled = false;
            this.editButtonDoktor.Id = ((long)(0));
            this.editButtonDoktor.Location = new System.Drawing.Point(100, 94);
            this.editButtonDoktor.Name = "editButtonDoktor";
            this.editButtonDoktor.NewValue = "";
            this.editButtonDoktor.OldValue = "";
            this.editButtonDoktor.ReadOnly = false;
            this.editButtonDoktor.Size = new System.Drawing.Size(228, 23);
            this.editButtonDoktor.TabIndex = 3;
            // 
            // editButtonHasta
            // 
            this.editButtonHasta.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editButtonHasta.BackColor = System.Drawing.SystemColors.Window;
            this.editButtonHasta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editButtonHasta.CommandName = "EditButtonHasta";
            this.editButtonHasta.Id = ((long)(0));
            this.editButtonHasta.Location = new System.Drawing.Point(100, 123);
            this.editButtonHasta.Name = "editButtonHasta";
            this.editButtonHasta.NewValue = "";
            this.editButtonHasta.OldValue = "";
            this.editButtonHasta.ReadOnly = false;
            this.editButtonHasta.Size = new System.Drawing.Size(228, 23);
            this.editButtonHasta.TabIndex = 6;
            // 
            // frmRandevu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 424);
            this.Controls.Add(this.TimeEditSaat);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ucEnumGosterIzlemTuru);
            this.Controls.Add(this.labelIslemTuru);
            this.Controls.Add(this.ucEnumGosterIslemTuru);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ucEnumGosterDurum);
            this.Controls.Add(this.labelKayitdurumu);
            this.Controls.Add(this.labelSiraNo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBoxVekildoktor);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DateEditBasTarih);
            this.Controls.Add(this.labelBasTarih);
            this.Controls.Add(this.TextBoxAciklama);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.editButtonDoktor);
            this.Controls.Add(this.labelHasta);
            this.Controls.Add(this.editButtonHasta);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "frmRandevu";
            this.Text = "Randevu Kayıt";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmRandevu_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.DateEditBasTarih.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEditBasTarih.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeEditSaat.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private EditButton editButtonDoktor;
        private System.Windows.Forms.Label labelHasta;
        private EditButton editButtonHasta;
        private DevExpress.XtraEditors.DateEdit DateEditBasTarih;
        private System.Windows.Forms.Label labelBasTarih;
        private System.Windows.Forms.TextBox TextBoxAciklama;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox checkBoxVekildoktor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelSiraNo;
        private System.Windows.Forms.Label labelKayitdurumu;
        private UcEnumGoster ucEnumGosterDurum;
        private System.Windows.Forms.Label label5;
        private UcEnumGoster ucEnumGosterIslemTuru;
        private System.Windows.Forms.Label labelIslemTuru;
        private System.Windows.Forms.Label label6;
        private UcEnumGoster ucEnumGosterIzlemTuru;
        private MyTimeEdit TimeEditSaat;
    }
}