using PowerScada;
namespace PowerScada.Rapor
{
    partial class frmProtokolDefteri
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButtonKapat = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonExceleAktar = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonOnizleme = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonGetir = new DevExpress.XtraEditors.SimpleButton();
            this.labelControlBitTarih = new DevExpress.XtraEditors.LabelControl();
            this.dateEditBitTarih = new DevExpress.XtraEditors.DateEdit();
            this.labelControlBasTarih = new DevExpress.XtraEditors.LabelControl();
            this.editButtondoktor = new PowerScada.EditButton();
            this.dateEditBasTarih = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gridControlProtokolDefteri = new DevExpress.XtraGrid.GridControl();
            this.gridViewProtokolDefteri = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBitTarih.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBitTarih.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBasTarih.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBasTarih.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProtokolDefteri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewProtokolDefteri)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.simpleButtonKapat);
            this.panelControl1.Controls.Add(this.simpleButtonExceleAktar);
            this.panelControl1.Controls.Add(this.simpleButtonOnizleme);
            this.panelControl1.Controls.Add(this.simpleButtonGetir);
            this.panelControl1.Controls.Add(this.labelControlBitTarih);
            this.panelControl1.Controls.Add(this.dateEditBitTarih);
            this.panelControl1.Controls.Add(this.labelControlBasTarih);
            this.panelControl1.Controls.Add(this.editButtondoktor);
            this.panelControl1.Controls.Add(this.dateEditBasTarih);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1014, 65);
            this.panelControl1.TabIndex = 0;
            // 
            // simpleButtonKapat
            // 
            this.simpleButtonKapat.Location = new System.Drawing.Point(925, 23);
            this.simpleButtonKapat.Name = "simpleButtonKapat";
            this.simpleButtonKapat.Size = new System.Drawing.Size(80, 23);
            this.simpleButtonKapat.TabIndex = 9;
            this.simpleButtonKapat.Text = "Kapat";
            // 
            // simpleButtonExceleAktar
            // 
            this.simpleButtonExceleAktar.Location = new System.Drawing.Point(835, 23);
            this.simpleButtonExceleAktar.Name = "simpleButtonExceleAktar";
            this.simpleButtonExceleAktar.Size = new System.Drawing.Size(80, 23);
            this.simpleButtonExceleAktar.TabIndex = 8;
            this.simpleButtonExceleAktar.Text = "Excele Aktar";
            // 
            // simpleButtonOnizleme
            // 
            this.simpleButtonOnizleme.Location = new System.Drawing.Point(745, 23);
            this.simpleButtonOnizleme.Name = "simpleButtonOnizleme";
            this.simpleButtonOnizleme.Size = new System.Drawing.Size(80, 23);
            this.simpleButtonOnizleme.TabIndex = 7;
            this.simpleButtonOnizleme.Text = "Ön İzleme";
            // 
            // simpleButtonGetir
            // 
            this.simpleButtonGetir.Location = new System.Drawing.Point(655, 23);
            this.simpleButtonGetir.Name = "simpleButtonGetir";
            this.simpleButtonGetir.Size = new System.Drawing.Size(80, 23);
            this.simpleButtonGetir.TabIndex = 6;
            this.simpleButtonGetir.Text = "Getir";
            // 
            // labelControlBitTarih
            // 
            this.labelControlBitTarih.Location = new System.Drawing.Point(477, 33);
            this.labelControlBitTarih.Name = "labelControlBitTarih";
            this.labelControlBitTarih.Size = new System.Drawing.Size(55, 13);
            this.labelControlBitTarih.TabIndex = 5;
            this.labelControlBitTarih.Text = "Bitiş Tarihi :";
            // 
            // dateEditBitTarih
            // 
            this.dateEditBitTarih.EditValue = null;
            this.dateEditBitTarih.Location = new System.Drawing.Point(540, 26);
            this.dateEditBitTarih.Name = "dateEditBitTarih";
            this.dateEditBitTarih.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditBitTarih.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditBitTarih.Size = new System.Drawing.Size(100, 20);
            this.dateEditBitTarih.TabIndex = 4;
            // 
            // labelControlBasTarih
            // 
            this.labelControlBasTarih.Location = new System.Drawing.Point(267, 33);
            this.labelControlBasTarih.Name = "labelControlBasTarih";
            this.labelControlBasTarih.Size = new System.Drawing.Size(80, 13);
            this.labelControlBasTarih.TabIndex = 3;
            this.labelControlBasTarih.Text = "Başlangıç Tarihi :";
            // 
            // editButtondoktor
            // 
            this.editButtondoktor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editButtondoktor.BackColor = System.Drawing.SystemColors.Window;
            this.editButtondoktor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editButtondoktor.CommandName = "EditButtonDoktorSec";
            this.editButtondoktor.Id = ((long)(0));
            this.editButtondoktor.Location = new System.Drawing.Point(50, 23);
            this.editButtondoktor.Name = "editButtondoktor";
            this.editButtondoktor.NewValue = "";
            this.editButtondoktor.OldValue = "";
            this.editButtondoktor.ReadOnly = false;
            this.editButtondoktor.Size = new System.Drawing.Size(182, 23);
            this.editButtondoktor.TabIndex = 2;
            // 
            // dateEditBasTarih
            // 
            this.dateEditBasTarih.EditValue = null;
            this.dateEditBasTarih.Location = new System.Drawing.Point(355, 26);
            this.dateEditBasTarih.Name = "dateEditBasTarih";
            this.dateEditBasTarih.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditBasTarih.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditBasTarih.Size = new System.Drawing.Size(100, 20);
            this.dateEditBasTarih.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 33);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(39, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Doktor :";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.gridControlProtokolDefteri);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 65);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1014, 560);
            this.panelControl2.TabIndex = 1;
            // 
            // gridControlProtokolDefteri
            // 
            this.gridControlProtokolDefteri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlProtokolDefteri.Location = new System.Drawing.Point(2, 2);
            this.gridControlProtokolDefteri.MainView = this.gridViewProtokolDefteri;
            this.gridControlProtokolDefteri.Name = "gridControlProtokolDefteri";
            this.gridControlProtokolDefteri.Size = new System.Drawing.Size(1010, 556);
            this.gridControlProtokolDefteri.TabIndex = 0;
            this.gridControlProtokolDefteri.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewProtokolDefteri});
            // 
            // gridViewProtokolDefteri
            // 
            this.gridViewProtokolDefteri.GridControl = this.gridControlProtokolDefteri;
            this.gridViewProtokolDefteri.Name = "gridViewProtokolDefteri";
            this.gridViewProtokolDefteri.OptionsView.ColumnAutoWidth = false;
            this.gridViewProtokolDefteri.OptionsView.ShowGroupPanel = false;
            // 
            // frmProtokolDefteri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 625);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "frmProtokolDefteri";
            this.ShowIcon = false;
            this.Text = "Protokol Defteri";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBitTarih.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBitTarih.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBasTarih.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBasTarih.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProtokolDefteri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewProtokolDefteri)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.DateEdit dateEditBasTarih;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl labelControlBitTarih;
        private DevExpress.XtraEditors.DateEdit dateEditBitTarih;
        private DevExpress.XtraEditors.LabelControl labelControlBasTarih;
        private EditButton editButtondoktor;
        private DevExpress.XtraEditors.SimpleButton simpleButtonGetir;
        private DevExpress.XtraGrid.GridControl gridControlProtokolDefteri;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewProtokolDefteri;
        private DevExpress.XtraEditors.SimpleButton simpleButtonKapat;
        private DevExpress.XtraEditors.SimpleButton simpleButtonExceleAktar;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOnizleme;
    }
}