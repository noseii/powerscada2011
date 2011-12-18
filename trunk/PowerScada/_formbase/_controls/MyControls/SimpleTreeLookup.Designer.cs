namespace PowerScada
{
    partial class SimpleTreeLookup
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
            this.GridTumTeshis = new DevExpress.XtraTreeList.TreeList();
            this.panelArama = new System.Windows.Forms.Panel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.labelkodu = new System.Windows.Forms.Label();
            this.textBoxAdi = new System.Windows.Forms.TextBox();
            this.LabelAdi = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.GridTumTeshis)).BeginInit();
            this.panelArama.SuspendLayout();
            this.SuspendLayout();
            // 
            // GridTumTeshis
            // 
            this.GridTumTeshis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridTumTeshis.KeyFieldName = "Id";
            this.GridTumTeshis.Location = new System.Drawing.Point(0, 43);
            this.GridTumTeshis.Name = "GridTumTeshis";
            this.GridTumTeshis.OptionsBehavior.AllowIncrementalSearch = true;
            this.GridTumTeshis.OptionsBehavior.Editable = false;
            this.GridTumTeshis.OptionsBehavior.EnableFiltering = true;
            this.GridTumTeshis.ParentFieldName = "UstTeshis_Id";
            this.GridTumTeshis.Size = new System.Drawing.Size(324, 310);
            this.GridTumTeshis.TabIndex = 1;
            this.GridTumTeshis.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GridTumTeshis_MouseDoubleClick);
            // 
            // panelArama
            // 
            this.panelArama.Controls.Add(this.textBox2);
            this.panelArama.Controls.Add(this.labelkodu);
            this.panelArama.Controls.Add(this.textBoxAdi);
            this.panelArama.Controls.Add(this.LabelAdi);
            this.panelArama.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelArama.Location = new System.Drawing.Point(0, 0);
            this.panelArama.Name = "panelArama";
            this.panelArama.Size = new System.Drawing.Size(324, 43);
            this.panelArama.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(209, 12);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 3;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // labelkodu
            // 
            this.labelkodu.AutoSize = true;
            this.labelkodu.Location = new System.Drawing.Point(167, 19);
            this.labelkodu.Name = "labelkodu";
            this.labelkodu.Size = new System.Drawing.Size(32, 13);
            this.labelkodu.TabIndex = 2;
            this.labelkodu.Text = "Kodu";
            // 
            // textBoxAdi
            // 
            this.textBoxAdi.Location = new System.Drawing.Point(52, 12);
            this.textBoxAdi.Name = "textBoxAdi";
            this.textBoxAdi.Size = new System.Drawing.Size(100, 20);
            this.textBoxAdi.TabIndex = 1;
            this.textBoxAdi.TextChanged += new System.EventHandler(this.textBoxAdi_TextChanged);
            // 
            // LabelAdi
            // 
            this.LabelAdi.AutoSize = true;
            this.LabelAdi.Location = new System.Drawing.Point(10, 19);
            this.LabelAdi.Name = "LabelAdi";
            this.LabelAdi.Size = new System.Drawing.Size(22, 13);
            this.LabelAdi.TabIndex = 0;
            this.LabelAdi.Text = "Adi";
            // 
            // SimpleTreeLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 353);
            this.Controls.Add(this.GridTumTeshis);
            this.Controls.Add(this.panelArama);
            this.KeyPreview = true;
            this.Name = "SimpleTreeLookup";
            this.Text = "Seç:";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SimpleLookup_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.GridTumTeshis)).EndInit();
            this.panelArama.ResumeLayout(false);
            this.panelArama.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList GridTumTeshis;
        private System.Windows.Forms.Panel panelArama;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label labelkodu;
        private System.Windows.Forms.TextBox textBoxAdi;
        private System.Windows.Forms.Label LabelAdi;

    }
}