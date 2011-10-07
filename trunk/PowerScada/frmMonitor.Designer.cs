namespace AHBS2010
{
    partial class frmMonitor
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelUst = new System.Windows.Forms.Panel();
            this.splitterControl2 = new DevExpress.XtraEditors.SplitterControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelUstSag = new System.Windows.Forms.Panel();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.panelSol = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panelSag = new System.Windows.Forms.Panel();
            this.listBoxRandevu = new AHBS2010.MyListBox();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.panelAlt = new System.Windows.Forms.Panel();
            this.labelControlSaat = new DevExpress.XtraEditors.LabelControl();
            this.labelControlTarih = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.splitterControl3 = new DevExpress.XtraEditors.SplitterControl();
            this.splitterControl4 = new DevExpress.XtraEditors.SplitterControl();
            this.panelUst.SuspendLayout();
            this.panelUstSag.SuspendLayout();
            this.panelSol.SuspendLayout();
            this.panelSag.SuspendLayout();
            this.panelAlt.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panelUst
            // 
            this.panelUst.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.panelUst.Controls.Add(this.splitterControl2);
            this.panelUst.Controls.Add(this.labelControl1);
            this.panelUst.Controls.Add(this.panelUstSag);
            this.panelUst.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelUst.Location = new System.Drawing.Point(0, 0);
            this.panelUst.Name = "panelUst";
            this.panelUst.Size = new System.Drawing.Size(884, 73);
            this.panelUst.TabIndex = 0;
            // 
            // splitterControl2
            // 
            this.splitterControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitterControl2.Location = new System.Drawing.Point(489, 0);
            this.splitterControl2.Name = "splitterControl2";
            this.splitterControl2.Size = new System.Drawing.Size(6, 73);
            this.splitterControl2.TabIndex = 1;
            this.splitterControl2.TabStop = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(21, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(238, 42);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "labelControl1";
            // 
            // panelUstSag
            // 
            this.panelUstSag.Controls.Add(this.labelControl3);
            this.panelUstSag.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelUstSag.Location = new System.Drawing.Point(495, 0);
            this.panelUstSag.Name = "panelUstSag";
            this.panelUstSag.Size = new System.Drawing.Size(389, 73);
            this.panelUstSag.TabIndex = 2;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(18, 21);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(359, 33);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "BEKLEYEN HASTA LİSTESİ";
            // 
            // panelSol
            // 
            this.panelSol.Controls.Add(this.textBox1);
            this.panelSol.Controls.Add(this.labelControl2);
            this.panelSol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSol.Location = new System.Drawing.Point(0, 79);
            this.panelSol.Name = "panelSol";
            this.panelSol.Size = new System.Drawing.Size(884, 594);
            this.panelSol.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(884, 594);
            this.textBox1.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(12, 200);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(216, 39);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "labelControl2";
            // 
            // panelSag
            // 
            this.panelSag.AutoScroll = true;
            this.panelSag.Controls.Add(this.listBoxRandevu);
            this.panelSag.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelSag.Location = new System.Drawing.Point(495, 79);
            this.panelSag.Name = "panelSag";
            this.panelSag.Size = new System.Drawing.Size(389, 588);
            this.panelSag.TabIndex = 2;
            // 
            // listBoxRandevu
            // 
            this.listBoxRandevu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxRandevu.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.listBoxRandevu.FormattingEnabled = true;
            this.listBoxRandevu.ItemHeight = 39;
            this.listBoxRandevu.Location = new System.Drawing.Point(0, 0);
            this.listBoxRandevu.Name = "listBoxRandevu";
            this.listBoxRandevu.Size = new System.Drawing.Size(389, 588);
            this.listBoxRandevu.TabIndex = 2;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterControl1.Location = new System.Drawing.Point(0, 73);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(884, 6);
            this.splitterControl1.TabIndex = 3;
            this.splitterControl1.TabStop = false;
            // 
            // panelAlt
            // 
            this.panelAlt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.panelAlt.Controls.Add(this.labelControlSaat);
            this.panelAlt.Controls.Add(this.labelControlTarih);
            this.panelAlt.Controls.Add(this.labelControl5);
            this.panelAlt.Controls.Add(this.labelControl4);
            this.panelAlt.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelAlt.Location = new System.Drawing.Point(0, 673);
            this.panelAlt.Name = "panelAlt";
            this.panelAlt.Size = new System.Drawing.Size(884, 60);
            this.panelAlt.TabIndex = 8;
            // 
            // labelControlSaat
            // 
            this.labelControlSaat.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControlSaat.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControlSaat.Appearance.Options.UseFont = true;
            this.labelControlSaat.Appearance.Options.UseForeColor = true;
            this.labelControlSaat.Location = new System.Drawing.Point(566, 15);
            this.labelControlSaat.Name = "labelControlSaat";
            this.labelControlSaat.Size = new System.Drawing.Size(0, 33);
            this.labelControlSaat.TabIndex = 4;
            // 
            // labelControlTarih
            // 
            this.labelControlTarih.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControlTarih.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControlTarih.Appearance.Options.UseFont = true;
            this.labelControlTarih.Appearance.Options.UseForeColor = true;
            this.labelControlTarih.Location = new System.Drawing.Point(170, 15);
            this.labelControlTarih.Name = "labelControlTarih";
            this.labelControlTarih.Size = new System.Drawing.Size(0, 33);
            this.labelControlTarih.TabIndex = 3;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Appearance.Options.UseForeColor = true;
            this.labelControl5.Location = new System.Drawing.Point(21, 15);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(107, 33);
            this.labelControl5.TabIndex = 2;
            this.labelControl5.Text = "TARİH :";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Appearance.Options.UseForeColor = true;
            this.labelControl4.Location = new System.Drawing.Point(480, 15);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(80, 33);
            this.labelControl4.TabIndex = 1;
            this.labelControl4.Text = "SAAT:";
            // 
            // splitterControl3
            // 
            this.splitterControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterControl3.Location = new System.Drawing.Point(0, 667);
            this.splitterControl3.Name = "splitterControl3";
            this.splitterControl3.Size = new System.Drawing.Size(884, 6);
            this.splitterControl3.TabIndex = 9;
            this.splitterControl3.TabStop = false;
            // 
            // splitterControl4
            // 
            this.splitterControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitterControl4.Location = new System.Drawing.Point(489, 79);
            this.splitterControl4.Name = "splitterControl4";
            this.splitterControl4.Size = new System.Drawing.Size(6, 588);
            this.splitterControl4.TabIndex = 10;
            this.splitterControl4.TabStop = false;
            // 
            // frmMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 733);
            this.Controls.Add(this.splitterControl4);
            this.Controls.Add(this.panelSag);
            this.Controls.Add(this.splitterControl3);
            this.Controls.Add(this.panelSol);
            this.Controls.Add(this.panelAlt);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.panelUst);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmMonitor";
            this.ShowIcon = false;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMonitor_FormClosed);
            this.Load += new System.EventHandler(this.frmMonitor_Load);
            this.panelUst.ResumeLayout(false);
            this.panelUst.PerformLayout();
            this.panelUstSag.ResumeLayout(false);
            this.panelUstSag.PerformLayout();
            this.panelSol.ResumeLayout(false);
            this.panelSol.PerformLayout();
            this.panelSag.ResumeLayout(false);
            this.panelAlt.ResumeLayout(false);
            this.panelAlt.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelUst;
        public DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.Panel panelSol;
        private System.Windows.Forms.Panel panelSag;
        public System.Windows.Forms.Timer timer1;
        public DevExpress.XtraEditors.LabelControl labelControl2;
        public System.Windows.Forms.TextBox textBox1;
        private DevExpress.XtraEditors.SplitterControl splitterControl2;
        private System.Windows.Forms.Panel panelUstSag;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private System.Windows.Forms.Panel panelAlt;
        private DevExpress.XtraEditors.SplitterControl splitterControl3;
        private DevExpress.XtraEditors.SplitterControl splitterControl4;
        private DevExpress.XtraEditors.LabelControl labelControlSaat;
        private DevExpress.XtraEditors.LabelControl labelControlTarih;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        public  AHBS2010.MyListBox listBoxRandevu;
    }
}