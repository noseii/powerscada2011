namespace AHBS2010
{
    partial class frmRandevuKaydi
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
            this.label1 = new System.Windows.Forms.Label();
            this.txSubject = new System.Windows.Forms.TextBox();
            this.checkAllDay = new System.Windows.Forms.CheckBox();
            this.labelRandevudurumu = new System.Windows.Forms.Label();
            this.labelIslemTuru = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnRecurrence = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.edStatus = new DevExpress.XtraScheduler.UI.AppointmentStatusEdit();
            this.edLabel = new DevExpress.XtraScheduler.UI.AppointmentLabelEdit();
            this.labelBasTarih = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtStart = new DevExpress.XtraEditors.DateEdit();
            this.dtEnd = new DevExpress.XtraEditors.DateEdit();
            this.timeStart = new DevExpress.XtraEditors.TimeEdit();
            this.timeEnd = new DevExpress.XtraEditors.TimeEdit();
            this.labelHasta = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.editButtonDoktor = new AHBS2010.EditButton();
            this.editButton1 = new AHBS2010.EditButton();
            this.ucEnumGosterSourceIslemTuru = new AHBS2010.UcEnumGoster();
            this.ucEnumGosterCustomRandevuDurumu = new AHBS2010.UcEnumGoster();
            ((System.ComponentModel.ISupportInitialize)(this.edStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edLabel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEnd.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 175);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Konu";
            // 
            // txSubject
            // 
            this.txSubject.Location = new System.Drawing.Point(135, 167);
            this.txSubject.Name = "txSubject";
            this.txSubject.Size = new System.Drawing.Size(184, 21);
            this.txSubject.TabIndex = 9;
            // 
            // checkAllDay
            // 
            this.checkAllDay.AutoSize = true;
            this.checkAllDay.Location = new System.Drawing.Point(135, 279);
            this.checkAllDay.Name = "checkAllDay";
            this.checkAllDay.Size = new System.Drawing.Size(76, 17);
            this.checkAllDay.TabIndex = 16;
            this.checkAllDay.Text = "Bütün Gün";
            this.checkAllDay.UseVisualStyleBackColor = true;
            this.checkAllDay.CheckedChanged += new System.EventHandler(this.checkAllDay_CheckedChanged);
            // 
            // labelRandevudurumu
            // 
            this.labelRandevudurumu.AutoSize = true;
            this.labelRandevudurumu.Location = new System.Drawing.Point(33, 100);
            this.labelRandevudurumu.Name = "labelRandevudurumu";
            this.labelRandevudurumu.Size = new System.Drawing.Size(90, 13);
            this.labelRandevudurumu.TabIndex = 4;
            this.labelRandevudurumu.Text = "Randevu Durumu";
            // 
            // labelIslemTuru
            // 
            this.labelIslemTuru.AutoSize = true;
            this.labelIslemTuru.Location = new System.Drawing.Point(33, 137);
            this.labelIslemTuru.Name = "labelIslemTuru";
            this.labelIslemTuru.Size = new System.Drawing.Size(57, 13);
            this.labelIslemTuru.TabIndex = 6;
            this.labelIslemTuru.Text = "İşlem Türü";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(135, 401);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(77, 23);
            this.btnOk.TabIndex = 21;
            this.btnOk.Text = "Kaydet";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnRecurrence
            // 
            this.btnRecurrence.Location = new System.Drawing.Point(218, 401);
            this.btnRecurrence.Name = "btnRecurrence";
            this.btnRecurrence.Size = new System.Drawing.Size(75, 23);
            this.btnRecurrence.TabIndex = 22;
            this.btnRecurrence.Text = "Tekrar";
            this.btnRecurrence.UseVisualStyleBackColor = true;
            this.btnRecurrence.Click += new System.EventHandler(this.btnRecurrence_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(299, 401);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 23;
            this.button3.Text = "Vazgeç";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // edStatus
            // 
            this.edStatus.Location = new System.Drawing.Point(135, 313);
            this.edStatus.Name = "edStatus";
            this.edStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.edStatus.Size = new System.Drawing.Size(184, 20);
            this.edStatus.TabIndex = 18;
            // 
            // edLabel
            // 
            this.edLabel.Location = new System.Drawing.Point(135, 350);
            this.edLabel.Name = "edLabel";
            this.edLabel.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.edLabel.Size = new System.Drawing.Size(184, 20);
            this.edLabel.TabIndex = 20;
            // 
            // labelBasTarih
            // 
            this.labelBasTarih.AutoSize = true;
            this.labelBasTarih.Location = new System.Drawing.Point(36, 212);
            this.labelBasTarih.Name = "labelBasTarih";
            this.labelBasTarih.Size = new System.Drawing.Size(51, 13);
            this.labelBasTarih.TabIndex = 10;
            this.labelBasTarih.Text = "Başlangıç";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 249);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Bitiş";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 320);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Statu";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(36, 357);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Etiket";
            // 
            // dtStart
            // 
            this.dtStart.EditValue = new System.DateTime(2011, 2, 4, 17, 5, 48, 184);
            this.dtStart.Location = new System.Drawing.Point(135, 205);
            this.dtStart.Name = "dtStart";
            this.dtStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtStart.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtStart.Size = new System.Drawing.Size(100, 20);
            this.dtStart.TabIndex = 11;
            this.dtStart.EditValueChanged += new System.EventHandler(this.dtStart_EditValueChanged);
            // 
            // dtEnd
            // 
            this.dtEnd.EditValue = new System.DateTime(2011, 2, 4, 17, 5, 38, 786);
            this.dtEnd.Location = new System.Drawing.Point(135, 242);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtEnd.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtEnd.Size = new System.Drawing.Size(100, 20);
            this.dtEnd.TabIndex = 14;
            this.dtEnd.EditValueChanged += new System.EventHandler(this.dtEnd_EditValueChanged);
            // 
            // timeStart
            // 
            this.timeStart.EditValue = new System.DateTime(2011, 2, 4, 0, 0, 0, 0);
            this.timeStart.Location = new System.Drawing.Point(242, 205);
            this.timeStart.Name = "timeStart";
            this.timeStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.timeStart.Size = new System.Drawing.Size(78, 20);
            this.timeStart.TabIndex = 12;
            this.timeStart.EditValueChanged += new System.EventHandler(this.timeStart_EditValueChanged);
            // 
            // timeEnd
            // 
            this.timeEnd.EditValue = new System.DateTime(2011, 2, 4, 0, 0, 0, 0);
            this.timeEnd.Location = new System.Drawing.Point(243, 240);
            this.timeEnd.Name = "timeEnd";
            this.timeEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.timeEnd.Size = new System.Drawing.Size(78, 20);
            this.timeEnd.TabIndex = 15;
            this.timeEnd.EditValueChanged += new System.EventHandler(this.timeEnd_EditValueChanged);
            // 
            // labelHasta
            // 
            this.labelHasta.AutoSize = true;
            this.labelHasta.Location = new System.Drawing.Point(33, 63);
            this.labelHasta.Name = "labelHasta";
            this.labelHasta.Size = new System.Drawing.Size(88, 13);
            this.labelHasta.TabIndex = 2;
            this.labelHasta.Text = "Hasta Adı Soyadı";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Doktor";
            // 
            // editButtonDoktor
            // 
            this.editButtonDoktor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editButtonDoktor.BackColor = System.Drawing.SystemColors.Window;
            this.editButtonDoktor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editButtonDoktor.CommandName = "EditButtonDoktorSec";
            this.editButtonDoktor.Id = 0;
            this.editButtonDoktor.Location = new System.Drawing.Point(135, 24);
            this.editButtonDoktor.Name = "editButtonDoktor";
            this.editButtonDoktor.NewValue = "";
            this.editButtonDoktor.OldValue = "";
            this.editButtonDoktor.ReadOnly = false;
            this.editButtonDoktor.Size = new System.Drawing.Size(184, 23);
            this.editButtonDoktor.TabIndex = 1;
            // 
            // editButton1
            // 
            this.editButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editButton1.BackColor = System.Drawing.SystemColors.Window;
            this.editButton1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editButton1.CommandName = "EditButtonHasta";
            this.editButton1.Id = 0;
            this.editButton1.Location = new System.Drawing.Point(135, 53);
            this.editButton1.Name = "editButton1";
            this.editButton1.NewValue = "";
            this.editButton1.OldValue = "";
            this.editButton1.ReadOnly = false;
            this.editButton1.Size = new System.Drawing.Size(184, 23);
            this.editButton1.TabIndex = 3;
            // 
            // ucEnumGosterSourceIslemTuru
            // 
            this.ucEnumGosterSourceIslemTuru.Deger = 0;
            this.ucEnumGosterSourceIslemTuru.EnumTuru = "IslemTuru";
            this.ucEnumGosterSourceIslemTuru.Location = new System.Drawing.Point(135, 130);
            this.ucEnumGosterSourceIslemTuru.Name = "ucEnumGosterSourceIslemTuru";
            this.ucEnumGosterSourceIslemTuru.Size = new System.Drawing.Size(184, 20);
            this.ucEnumGosterSourceIslemTuru.TabIndex = 7;
            // 
            // ucEnumGosterCustomRandevuDurumu
            // 
            this.ucEnumGosterCustomRandevuDurumu.Deger = 0;
            this.ucEnumGosterCustomRandevuDurumu.EnumTuru = "RandevuDurumu";
            this.ucEnumGosterCustomRandevuDurumu.Location = new System.Drawing.Point(135, 93);
            this.ucEnumGosterCustomRandevuDurumu.Name = "ucEnumGosterCustomRandevuDurumu";
            this.ucEnumGosterCustomRandevuDurumu.Size = new System.Drawing.Size(184, 20);
            this.ucEnumGosterCustomRandevuDurumu.TabIndex = 5;
            // 
            // frmRandevuKaydi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 537);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.editButtonDoktor);
            this.Controls.Add(this.labelHasta);
            this.Controls.Add(this.editButton1);
            this.Controls.Add(this.ucEnumGosterSourceIslemTuru);
            this.Controls.Add(this.ucEnumGosterCustomRandevuDurumu);
            this.Controls.Add(this.timeEnd);
            this.Controls.Add(this.timeStart);
            this.Controls.Add(this.dtEnd);
            this.Controls.Add(this.dtStart);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelBasTarih);
            this.Controls.Add(this.edLabel);
            this.Controls.Add(this.edStatus);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnRecurrence);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.labelIslemTuru);
            this.Controls.Add(this.labelRandevudurumu);
            this.Controls.Add(this.checkAllDay);
            this.Controls.Add(this.txSubject);
            this.Controls.Add(this.label1);
            this.Name = "frmRandevuKaydi";
            this.Text = "Randevu Formu";
            this.Activated += new System.EventHandler(this.MyAppointmentEditForm_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.edStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edLabel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEnd.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txSubject;
        private System.Windows.Forms.CheckBox checkAllDay;
        private System.Windows.Forms.Label labelRandevudurumu;
        private System.Windows.Forms.Label labelIslemTuru;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnRecurrence;
        private System.Windows.Forms.Button button3;
        private DevExpress.XtraScheduler.UI.AppointmentStatusEdit edStatus;
        private DevExpress.XtraScheduler.UI.AppointmentLabelEdit edLabel;
        private System.Windows.Forms.Label labelBasTarih;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.DateEdit dtStart;
        private DevExpress.XtraEditors.DateEdit dtEnd;
        private DevExpress.XtraEditors.TimeEdit timeStart;
        private DevExpress.XtraEditors.TimeEdit timeEnd;
        private UcEnumGoster ucEnumGosterCustomRandevuDurumu;
        private UcEnumGoster ucEnumGosterSourceIslemTuru;
        private EditButton editButton1;
        private System.Windows.Forms.Label labelHasta;
        private System.Windows.Forms.Label label2;
        private EditButton editButtonDoktor;
    }
}