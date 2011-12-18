namespace PowerScada
{
    partial class frmCihazIzlem
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
            this.button2 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.ucModem5 = new PowerScada.UCModem();
            this.ucModem4 = new PowerScada.UCModem();
            this.ucModem3 = new PowerScada.UCModem();
            this.ucModem2 = new PowerScada.UCModem();
            this.ucModem1 = new PowerScada.UCModem();
            this.ucTermoKupul6 = new PowerScada.UCTermoKupul();
            this.ucTermoKupul5 = new PowerScada.UCTermoKupul();
            this.ucTermoKupul4 = new PowerScada.UCTermoKupul();
            this.ucTermoKupul3 = new PowerScada.UCTermoKupul();
            this.ucTermoKupul2 = new PowerScada.UCTermoKupul();
            this.ucTermoKupul1 = new PowerScada.UCTermoKupul();
            this.ucTermoKupul11 = new PowerScada.UCTermoKupul();
            this.panelAlt = new DevExpress.XtraEditors.PanelControl();
            this.panelAlarm = new DevExpress.XtraEditors.PanelControl();
            this.splitterControl3 = new DevExpress.XtraEditors.SplitterControl();
            this.gridAlarm = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splitterControl2 = new DevExpress.XtraEditors.SplitterControl();
            this.panelTarihce = new DevExpress.XtraEditors.PanelControl();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.gridTarihce = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelCihazlar = new DevExpress.XtraEditors.PanelControl();
            this.vScrollBar1 = new DevExpress.XtraEditors.VScrollBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.listBoxhatalilokasyonlar = new System.Windows.Forms.ListBox();
            this.PanelLokasyon = new System.Windows.Forms.Panel();
            this.treeListLokasyon = new DevExpress.XtraTreeList.TreeList();
            this.PanelMain = new DevExpress.XtraEditors.PanelControl();
            this.panelUst = new DevExpress.XtraEditors.PanelControl();
            this.splitterControl5 = new DevExpress.XtraEditors.SplitterControl();
            this.panelLeft = new DevExpress.XtraEditors.PanelControl();
            this.panelRight = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelAlt)).BeginInit();
            this.panelAlt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelAlarm)).BeginInit();
            this.panelAlarm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridAlarm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelTarihce)).BeginInit();
            this.panelTarihce.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTarihce)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelCihazlar)).BeginInit();
            this.panelCihazlar.SuspendLayout();
            this.PanelLokasyon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListLokasyon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanelMain)).BeginInit();
            this.PanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelUst)).BeginInit();
            this.panelUst.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelLeft)).BeginInit();
            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelRight)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Control;
            this.button2.Cursor = System.Windows.Forms.Cursors.Default;
            this.button2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button2.Location = new System.Drawing.Point(243, 167);
            this.button2.Name = "button2";
            this.button2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button2.Size = new System.Drawing.Size(85, 33);
            this.button2.TabIndex = 110;
            this.button2.Text = "Adresten Oku";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.SystemColors.Window;
            this.listBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.listBox1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.listBox1.ItemHeight = 14;
            this.listBox1.Location = new System.Drawing.Point(5, 168);
            this.listBox1.Name = "listBox1";
            this.listBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listBox1.Size = new System.Drawing.Size(232, 32);
            this.listBox1.TabIndex = 103;
            // 
            // ucModem5
            // 
            this.ucModem5.AlarmTarihce = null;
            this.ucModem5.ButtonWidth = 98;
            this.ucModem5.Cihaz = null;
            this.ucModem5.CihazAdi = "Hubı Resetle";
            this.ucModem5.Cursor = System.Windows.Forms.Cursors.PanWest;
            this.ucModem5.LabelText = "Cihaz Adi";
            this.ucModem5.LabelWidth = 51;
            this.ucModem5.Location = new System.Drawing.Point(223, 95);
            this.ucModem5.Name = "ucModem5";
            this.ucModem5.Opcmanager = null;
            this.ucModem5.Size = new System.Drawing.Size(178, 24);
            this.ucModem5.TabIndex = 122;
            this.ucModem5.Tarihce = null;
            // 
            // ucModem4
            // 
            this.ucModem4.AlarmTarihce = null;
            this.ucModem4.ButtonWidth = 98;
            this.ucModem4.Cihaz = null;
            this.ucModem4.CihazAdi = "Dvr Resetle";
            this.ucModem4.Cursor = System.Windows.Forms.Cursors.PanWest;
            this.ucModem4.LabelText = "Cihaz Adi";
            this.ucModem4.LabelWidth = 51;
            this.ucModem4.Location = new System.Drawing.Point(5, 95);
            this.ucModem4.Name = "ucModem4";
            this.ucModem4.Opcmanager = null;
            this.ucModem4.Size = new System.Drawing.Size(184, 24);
            this.ucModem4.TabIndex = 121;
            this.ucModem4.Tarihce = null;
            // 
            // ucModem3
            // 
            this.ucModem3.AlarmTarihce = null;
            this.ucModem3.ButtonWidth = 98;
            this.ucModem3.Cihaz = null;
            this.ucModem3.CihazAdi = "?? Resetle";
            this.ucModem3.Cursor = System.Windows.Forms.Cursors.PanWest;
            this.ucModem3.LabelText = "Cihaz Adi";
            this.ucModem3.LabelWidth = 51;
            this.ucModem3.Location = new System.Drawing.Point(5, 138);
            this.ucModem3.Name = "ucModem3";
            this.ucModem3.Opcmanager = null;
            this.ucModem3.Size = new System.Drawing.Size(184, 24);
            this.ucModem3.TabIndex = 120;
            this.ucModem3.Tarihce = null;
            // 
            // ucModem2
            // 
            this.ucModem2.AlarmTarihce = null;
            this.ucModem2.ButtonWidth = 98;
            this.ucModem2.Cihaz = null;
            this.ucModem2.CihazAdi = "Routerı Resetle";
            this.ucModem2.Cursor = System.Windows.Forms.Cursors.PanWest;
            this.ucModem2.LabelText = "Cihaz Adi";
            this.ucModem2.LabelWidth = 51;
            this.ucModem2.Location = new System.Drawing.Point(613, 95);
            this.ucModem2.Name = "ucModem2";
            this.ucModem2.Opcmanager = null;
            this.ucModem2.Size = new System.Drawing.Size(164, 24);
            this.ucModem2.TabIndex = 119;
            this.ucModem2.Tarihce = null;
            // 
            // ucModem1
            // 
            this.ucModem1.AlarmTarihce = null;
            this.ucModem1.ButtonWidth = 98;
            this.ucModem1.Cihaz = null;
            this.ucModem1.CihazAdi = "Modemi Resetle";
            this.ucModem1.Cursor = System.Windows.Forms.Cursors.PanWest;
            this.ucModem1.LabelText = "Cihaz Adi";
            this.ucModem1.LabelWidth = 51;
            this.ucModem1.Location = new System.Drawing.Point(420, 95);
            this.ucModem1.Name = "ucModem1";
            this.ucModem1.Opcmanager = null;
            this.ucModem1.Size = new System.Drawing.Size(170, 24);
            this.ucModem1.TabIndex = 118;
            this.ucModem1.Tarihce = null;
            // 
            // ucTermoKupul6
            // 
            this.ucTermoKupul6.AlarmTarihce = null;
            this.ucTermoKupul6.Cihaz = null;
            this.ucTermoKupul6.CihazAdi = "Frekans";
            this.ucTermoKupul6.Cursor = System.Windows.Forms.Cursors.PanWest;
            this.ucTermoKupul6.LabelText = "Cihaz Adı";
            this.ucTermoKupul6.LabelWidth = 51;
            this.ucTermoKupul6.Location = new System.Drawing.Point(613, 22);
            this.ucTermoKupul6.Name = "ucTermoKupul6";
            this.ucTermoKupul6.Opcmanager = null;
            this.ucTermoKupul6.SicaklikGoster = false;
            this.ucTermoKupul6.Size = new System.Drawing.Size(175, 24);
            this.ucTermoKupul6.TabIndex = 117;
            this.ucTermoKupul6.Tarihce = null;
            this.ucTermoKupul6.TextWidth = 70;
            // 
            // ucTermoKupul5
            // 
            this.ucTermoKupul5.AlarmTarihce = null;
            this.ucTermoKupul5.Cihaz = null;
            this.ucTermoKupul5.CihazAdi = "Toprak Notr Arası Voltaj";
            this.ucTermoKupul5.Cursor = System.Windows.Forms.Cursors.PanWest;
            this.ucTermoKupul5.LabelText = "Cihaz Adı";
            this.ucTermoKupul5.LabelWidth = 51;
            this.ucTermoKupul5.Location = new System.Drawing.Point(420, 22);
            this.ucTermoKupul5.Name = "ucTermoKupul5";
            this.ucTermoKupul5.Opcmanager = null;
            this.ucTermoKupul5.SicaklikGoster = false;
            this.ucTermoKupul5.Size = new System.Drawing.Size(170, 24);
            this.ucTermoKupul5.TabIndex = 116;
            this.ucTermoKupul5.Tarihce = null;
            this.ucTermoKupul5.TextWidth = 70;
            // 
            // ucTermoKupul4
            // 
            this.ucTermoKupul4.AlarmTarihce = null;
            this.ucTermoKupul4.Cihaz = null;
            this.ucTermoKupul4.CihazAdi = "Ups Voltajı";
            this.ucTermoKupul4.Cursor = System.Windows.Forms.Cursors.PanWest;
            this.ucTermoKupul4.LabelText = "Cihaz Adı";
            this.ucTermoKupul4.LabelWidth = 51;
            this.ucTermoKupul4.Location = new System.Drawing.Point(223, 22);
            this.ucTermoKupul4.Name = "ucTermoKupul4";
            this.ucTermoKupul4.Opcmanager = null;
            this.ucTermoKupul4.SicaklikGoster = false;
            this.ucTermoKupul4.Size = new System.Drawing.Size(178, 24);
            this.ucTermoKupul4.TabIndex = 115;
            this.ucTermoKupul4.Tarihce = null;
            this.ucTermoKupul4.TextWidth = 70;
            // 
            // ucTermoKupul3
            // 
            this.ucTermoKupul3.AlarmTarihce = null;
            this.ucTermoKupul3.Cihaz = null;
            this.ucTermoKupul3.CihazAdi = "Şebeke Voltajı";
            this.ucTermoKupul3.Cursor = System.Windows.Forms.Cursors.PanWest;
            this.ucTermoKupul3.LabelText = "Cihaz Adı";
            this.ucTermoKupul3.LabelWidth = 51;
            this.ucTermoKupul3.Location = new System.Drawing.Point(5, 22);
            this.ucTermoKupul3.Name = "ucTermoKupul3";
            this.ucTermoKupul3.Opcmanager = null;
            this.ucTermoKupul3.SicaklikGoster = false;
            this.ucTermoKupul3.Size = new System.Drawing.Size(190, 24);
            this.ucTermoKupul3.TabIndex = 114;
            this.ucTermoKupul3.Tarihce = null;
            this.ucTermoKupul3.TextWidth = 70;
            // 
            // ucTermoKupul2
            // 
            this.ucTermoKupul2.AlarmTarihce = null;
            this.ucTermoKupul2.Cihaz = null;
            this.ucTermoKupul2.CihazAdi = "Ortam Aydınlığı";
            this.ucTermoKupul2.Cursor = System.Windows.Forms.Cursors.PanWest;
            this.ucTermoKupul2.LabelText = "Cihaz Adı";
            this.ucTermoKupul2.LabelWidth = 51;
            this.ucTermoKupul2.Location = new System.Drawing.Point(420, 52);
            this.ucTermoKupul2.Name = "ucTermoKupul2";
            this.ucTermoKupul2.Opcmanager = null;
            this.ucTermoKupul2.SicaklikGoster = false;
            this.ucTermoKupul2.Size = new System.Drawing.Size(170, 24);
            this.ucTermoKupul2.TabIndex = 113;
            this.ucTermoKupul2.Tarihce = null;
            this.ucTermoKupul2.TextWidth = 70;
            // 
            // ucTermoKupul1
            // 
            this.ucTermoKupul1.AlarmTarihce = null;
            this.ucTermoKupul1.Cihaz = null;
            this.ucTermoKupul1.CihazAdi = "2. Isı Değeri";
            this.ucTermoKupul1.Cursor = System.Windows.Forms.Cursors.PanWest;
            this.ucTermoKupul1.LabelText = "Cihaz Adı";
            this.ucTermoKupul1.LabelWidth = 51;
            this.ucTermoKupul1.Location = new System.Drawing.Point(223, 52);
            this.ucTermoKupul1.Name = "ucTermoKupul1";
            this.ucTermoKupul1.Opcmanager = null;
            this.ucTermoKupul1.SicaklikGoster = true;
            this.ucTermoKupul1.Size = new System.Drawing.Size(178, 24);
            this.ucTermoKupul1.TabIndex = 112;
            this.ucTermoKupul1.Tarihce = null;
            this.ucTermoKupul1.TextWidth = 70;
            // 
            // ucTermoKupul11
            // 
            this.ucTermoKupul11.AlarmTarihce = null;
            this.ucTermoKupul11.Cihaz = null;
            this.ucTermoKupul11.CihazAdi = "1. Isı Değeri";
            this.ucTermoKupul11.Cursor = System.Windows.Forms.Cursors.PanWest;
            this.ucTermoKupul11.LabelText = "Cihaz Adı";
            this.ucTermoKupul11.LabelWidth = 51;
            this.ucTermoKupul11.Location = new System.Drawing.Point(5, 52);
            this.ucTermoKupul11.Name = "ucTermoKupul11";
            this.ucTermoKupul11.Opcmanager = null;
            this.ucTermoKupul11.SicaklikGoster = true;
            this.ucTermoKupul11.Size = new System.Drawing.Size(184, 24);
            this.ucTermoKupul11.TabIndex = 111;
            this.ucTermoKupul11.Tarihce = null;
            this.ucTermoKupul11.TextWidth = 70;
            // 
            // panelAlt
            // 
            this.panelAlt.Controls.Add(this.panelAlarm);
            this.panelAlt.Controls.Add(this.splitterControl2);
            this.panelAlt.Controls.Add(this.panelTarihce);
            this.panelAlt.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelAlt.Location = new System.Drawing.Point(0, 592);
            this.panelAlt.Name = "panelAlt";
            this.panelAlt.Size = new System.Drawing.Size(1028, 10);
            this.panelAlt.TabIndex = 123;
            // 
            // panelAlarm
            // 
            this.panelAlarm.Controls.Add(this.splitterControl3);
            this.panelAlarm.Controls.Add(this.gridAlarm);
            this.panelAlarm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelAlarm.Location = new System.Drawing.Point(8, -217);
            this.panelAlarm.Name = "panelAlarm";
            this.panelAlarm.Size = new System.Drawing.Size(1018, 225);
            this.panelAlarm.TabIndex = 1;
            // 
            // splitterControl3
            // 
            this.splitterControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterControl3.Location = new System.Drawing.Point(2, 2);
            this.splitterControl3.Name = "splitterControl3";
            this.splitterControl3.Size = new System.Drawing.Size(1014, 6);
            this.splitterControl3.TabIndex = 1;
            this.splitterControl3.TabStop = false;
            // 
            // gridAlarm
            // 
            this.gridAlarm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridAlarm.Location = new System.Drawing.Point(2, 2);
            this.gridAlarm.MainView = this.gridView2;
            this.gridAlarm.Name = "gridAlarm";
            this.gridAlarm.Size = new System.Drawing.Size(1014, 221);
            this.gridAlarm.TabIndex = 0;
            this.gridAlarm.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridAlarm;
            this.gridView2.Name = "gridView2";
            // 
            // splitterControl2
            // 
            this.splitterControl2.Location = new System.Drawing.Point(2, 2);
            this.splitterControl2.Name = "splitterControl2";
            this.splitterControl2.Size = new System.Drawing.Size(6, 6);
            this.splitterControl2.TabIndex = 2;
            this.splitterControl2.TabStop = false;
            // 
            // panelTarihce
            // 
            this.panelTarihce.Controls.Add(this.splitterControl1);
            this.panelTarihce.Controls.Add(this.gridTarihce);
            this.panelTarihce.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTarihce.Location = new System.Drawing.Point(2, 2);
            this.panelTarihce.Name = "panelTarihce";
            this.panelTarihce.Size = new System.Drawing.Size(1024, 6);
            this.panelTarihce.TabIndex = 0;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterControl1.Location = new System.Drawing.Point(2, -2);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(1020, 6);
            this.splitterControl1.TabIndex = 124;
            this.splitterControl1.TabStop = false;
            // 
            // gridTarihce
            // 
            this.gridTarihce.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTarihce.Location = new System.Drawing.Point(2, 2);
            this.gridTarihce.MainView = this.gridView1;
            this.gridTarihce.Name = "gridTarihce";
            this.gridTarihce.Size = new System.Drawing.Size(1020, 2);
            this.gridTarihce.TabIndex = 0;
            this.gridTarihce.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridTarihce.Visible = false;
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridTarihce;
            this.gridView1.Name = "gridView1";
            // 
            // panelCihazlar
            // 
            this.panelCihazlar.Controls.Add(this.vScrollBar1);
            this.panelCihazlar.Controls.Add(this.textBox1);
            this.panelCihazlar.Controls.Add(this.button1);
            this.panelCihazlar.Controls.Add(this.ucTermoKupul11);
            this.panelCihazlar.Controls.Add(this.listBox1);
            this.panelCihazlar.Controls.Add(this.button2);
            this.panelCihazlar.Controls.Add(this.ucModem3);
            this.panelCihazlar.Controls.Add(this.ucModem5);
            this.panelCihazlar.Controls.Add(this.ucTermoKupul1);
            this.panelCihazlar.Controls.Add(this.ucModem4);
            this.panelCihazlar.Controls.Add(this.ucTermoKupul2);
            this.panelCihazlar.Controls.Add(this.ucTermoKupul3);
            this.panelCihazlar.Controls.Add(this.ucModem2);
            this.panelCihazlar.Controls.Add(this.ucTermoKupul4);
            this.panelCihazlar.Controls.Add(this.ucModem1);
            this.panelCihazlar.Controls.Add(this.ucTermoKupul5);
            this.panelCihazlar.Controls.Add(this.ucTermoKupul6);
            this.panelCihazlar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelCihazlar.Location = new System.Drawing.Point(2, 271);
            this.panelCihazlar.Name = "panelCihazlar";
            this.panelCihazlar.Size = new System.Drawing.Size(812, 319);
            this.panelCihazlar.TabIndex = 125;
            this.panelCihazlar.Visible = false;
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar1.Location = new System.Drawing.Point(794, 2);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.ScrollBarAutoSize = true;
            this.vScrollBar1.Size = new System.Drawing.Size(16, 315);
            this.vScrollBar1.TabIndex = 126;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(386, 168);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(138, 20);
            this.textBox1.TabIndex = 125;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.Cursor = System.Windows.Forms.Cursors.Default;
            this.button1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button1.Location = new System.Drawing.Point(530, 167);
            this.button1.Name = "button1";
            this.button1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button1.Size = new System.Drawing.Size(91, 20);
            this.button1.TabIndex = 124;
            this.button1.Text = "Adrese Yaz";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBoxhatalilokasyonlar
            // 
            this.listBoxhatalilokasyonlar.BackColor = System.Drawing.SystemColors.Window;
            this.listBoxhatalilokasyonlar.Cursor = System.Windows.Forms.Cursors.Default;
            this.listBoxhatalilokasyonlar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxhatalilokasyonlar.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxhatalilokasyonlar.ForeColor = System.Drawing.SystemColors.WindowText;
            this.listBoxhatalilokasyonlar.ItemHeight = 14;
            this.listBoxhatalilokasyonlar.Location = new System.Drawing.Point(2, 2);
            this.listBoxhatalilokasyonlar.Name = "listBoxhatalilokasyonlar";
            this.listBoxhatalilokasyonlar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listBoxhatalilokasyonlar.Size = new System.Drawing.Size(132, 261);
            this.listBoxhatalilokasyonlar.TabIndex = 126;
            this.listBoxhatalilokasyonlar.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox2_MouseDoubleClick);
            // 
            // PanelLokasyon
            // 
            this.PanelLokasyon.Controls.Add(this.treeListLokasyon);
            this.PanelLokasyon.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelLokasyon.Location = new System.Drawing.Point(0, 0);
            this.PanelLokasyon.Name = "PanelLokasyon";
            this.PanelLokasyon.Size = new System.Drawing.Size(212, 592);
            this.PanelLokasyon.TabIndex = 126;
            // 
            // treeListLokasyon
            // 
            this.treeListLokasyon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListLokasyon.KeyFieldName = "Id";
            this.treeListLokasyon.Location = new System.Drawing.Point(0, 0);
            this.treeListLokasyon.Name = "treeListLokasyon";
            this.treeListLokasyon.OptionsBehavior.Editable = false;
            this.treeListLokasyon.ParentFieldName = "UstLokasyon_Id";
            this.treeListLokasyon.Size = new System.Drawing.Size(212, 592);
            this.treeListLokasyon.TabIndex = 0;
            this.treeListLokasyon.DoubleClick += new System.EventHandler(this.treeListLokasyon_DoubleClick);
            // 
            // PanelMain
            // 
            this.PanelMain.Controls.Add(this.panelUst);
            this.PanelMain.Controls.Add(this.panelCihazlar);
            this.PanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMain.Location = new System.Drawing.Point(212, 0);
            this.PanelMain.Name = "PanelMain";
            this.PanelMain.Size = new System.Drawing.Size(816, 592);
            this.PanelMain.TabIndex = 127;
            // 
            // panelUst
            // 
            this.panelUst.Controls.Add(this.splitterControl5);
            this.panelUst.Controls.Add(this.panelLeft);
            this.panelUst.Controls.Add(this.panelRight);
            this.panelUst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelUst.Location = new System.Drawing.Point(2, 2);
            this.panelUst.Name = "panelUst";
            this.panelUst.Size = new System.Drawing.Size(812, 269);
            this.panelUst.TabIndex = 130;
            // 
            // splitterControl5
            // 
            this.splitterControl5.Location = new System.Drawing.Point(138, 2);
            this.splitterControl5.Name = "splitterControl5";
            this.splitterControl5.Size = new System.Drawing.Size(6, 265);
            this.splitterControl5.TabIndex = 130;
            this.splitterControl5.TabStop = false;
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.listBoxhatalilokasyonlar);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(2, 2);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(136, 265);
            this.panelLeft.TabIndex = 127;
            // 
            // panelRight
            // 
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(2, 2);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(808, 265);
            this.panelRight.TabIndex = 129;
            this.panelRight.Paint += new System.Windows.Forms.PaintEventHandler(this.panelRight_Paint);
            // 
            // frmCihazIzlem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 602);
            this.Controls.Add(this.PanelMain);
            this.Controls.Add(this.PanelLokasyon);
            this.Controls.Add(this.panelAlt);
            this.Name = "frmCihazIzlem";
            this.Text = "frmCihazIzlem";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCihazIzlem_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.panelAlt)).EndInit();
            this.panelAlt.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelAlarm)).EndInit();
            this.panelAlarm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridAlarm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelTarihce)).EndInit();
            this.panelTarihce.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTarihce)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelCihazlar)).EndInit();
            this.panelCihazlar.ResumeLayout(false);
            this.panelCihazlar.PerformLayout();
            this.PanelLokasyon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListLokasyon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanelMain)).EndInit();
            this.PanelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelUst)).EndInit();
            this.panelUst.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelLeft)).EndInit();
            this.panelLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelRight)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.ListBox listBox1;
        private UCTermoKupul ucTermoKupul11;
        private UCTermoKupul ucTermoKupul1;
        private UCTermoKupul ucTermoKupul2;
        private UCTermoKupul ucTermoKupul3;
        private UCTermoKupul ucTermoKupul4;
        private UCTermoKupul ucTermoKupul5;
        private UCTermoKupul ucTermoKupul6;
        private UCModem ucModem1;
        private UCModem ucModem2;
        private UCModem ucModem3;
        private UCModem ucModem4;
        private UCModem ucModem5;
        private DevExpress.XtraEditors.PanelControl panelAlt;
        private DevExpress.XtraEditors.PanelControl panelCihazlar;
        private DevExpress.XtraEditors.SplitterControl splitterControl2;
        private DevExpress.XtraEditors.PanelControl panelAlarm;
        private DevExpress.XtraGrid.GridControl gridAlarm;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.PanelControl panelTarihce;
        private DevExpress.XtraEditors.SplitterControl splitterControl3;
        private DevExpress.XtraGrid.GridControl gridTarihce;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        public System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private System.Windows.Forms.Panel PanelLokasyon;
        private DevExpress.XtraTreeList.TreeList treeListLokasyon;
        public System.Windows.Forms.ListBox listBoxhatalilokasyonlar;
        private DevExpress.XtraEditors.PanelControl PanelMain;
        private DevExpress.XtraEditors.PanelControl panelRight;
        private DevExpress.XtraEditors.PanelControl panelLeft;
        private DevExpress.XtraEditors.VScrollBar vScrollBar1;
        private DevExpress.XtraEditors.PanelControl panelUst;
        private DevExpress.XtraEditors.SplitterControl splitterControl5;
      
    }
}