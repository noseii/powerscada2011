namespace PowerScada
{

    partial class frmRolIsHakki
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Rolgrid = new System.Windows.Forms.DataGridView();
            this.ustpanel = new System.Windows.Forms.Panel();
            this.labelRol = new System.Windows.Forms.Label();
            this.txtbxRolAdi = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.IsHaklarigrid = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelEkranIsmi = new System.Windows.Forms.Label();
            this.textBoxEkranIsmi = new System.Windows.Forms.TextBox();
            this.btnkaydet = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Hak = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Var = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Aktif = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Rol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Rolgrid)).BeginInit();
            this.ustpanel.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IsHaklarigrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Rolgrid);
            this.splitContainer1.Panel1.Controls.Add(this.ustpanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1056, 744);
            this.splitContainer1.SplitterDistance = 352;
            this.splitContainer1.TabIndex = 0;
            // 
            // Rolgrid
            // 
            this.Rolgrid.AllowUserToAddRows = false;
            this.Rolgrid.AllowUserToDeleteRows = false;
            this.Rolgrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Rolgrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Rol,
            this.Id});
            this.Rolgrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Rolgrid.Location = new System.Drawing.Point(0, 84);
            this.Rolgrid.Name = "Rolgrid";
            this.Rolgrid.Size = new System.Drawing.Size(352, 660);
            this.Rolgrid.TabIndex = 1;
            this.Rolgrid.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.Rolgrid_RowLeave);
            this.Rolgrid.SelectionChanged += new System.EventHandler(this.Rolgrid_SelectionChanged);
            // 
            // ustpanel
            // 
            this.ustpanel.Controls.Add(this.labelRol);
            this.ustpanel.Controls.Add(this.txtbxRolAdi);
            this.ustpanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ustpanel.Location = new System.Drawing.Point(0, 0);
            this.ustpanel.Name = "ustpanel";
            this.ustpanel.Size = new System.Drawing.Size(352, 84);
            this.ustpanel.TabIndex = 0;
            // 
            // labelRol
            // 
            this.labelRol.AutoSize = true;
            this.labelRol.Location = new System.Drawing.Point(10, 51);
            this.labelRol.Name = "labelRol";
            this.labelRol.Size = new System.Drawing.Size(44, 13);
            this.labelRol.TabIndex = 1;
            this.labelRol.Text = "Rol İsmi";
            // 
            // txtbxRolAdi
            // 
            this.txtbxRolAdi.Location = new System.Drawing.Point(75, 44);
            this.txtbxRolAdi.Name = "txtbxRolAdi";
            this.txtbxRolAdi.Size = new System.Drawing.Size(189, 20);
            this.txtbxRolAdi.TabIndex = 0;
            this.txtbxRolAdi.TextChanged += new System.EventHandler(this.txtbxRolAdi_TextChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 84);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(700, 660);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.IsHaklarigrid);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(692, 634);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "İş Hakları";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // IsHaklarigrid
            // 
            this.IsHaklarigrid.AllowUserToAddRows = false;
            this.IsHaklarigrid.AllowUserToDeleteRows = false;
            this.IsHaklarigrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.IsHaklarigrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.Hak,
            this.Var,
            this.Aktif});
            this.IsHaklarigrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IsHaklarigrid.Location = new System.Drawing.Point(3, 3);
            this.IsHaklarigrid.Name = "IsHaklarigrid";
            this.IsHaklarigrid.Size = new System.Drawing.Size(686, 628);
            this.IsHaklarigrid.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelEkranIsmi);
            this.panel1.Controls.Add(this.textBoxEkranIsmi);
            this.panel1.Controls.Add(this.btnkaydet);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(700, 84);
            this.panel1.TabIndex = 1;
            // 
            // labelEkranIsmi
            // 
            this.labelEkranIsmi.AutoSize = true;
            this.labelEkranIsmi.Location = new System.Drawing.Point(19, 51);
            this.labelEkranIsmi.Name = "labelEkranIsmi";
            this.labelEkranIsmi.Size = new System.Drawing.Size(48, 13);
            this.labelEkranIsmi.TabIndex = 3;
            this.labelEkranIsmi.Text = "Hak İsmi";
            // 
            // textBoxEkranIsmi
            // 
            this.textBoxEkranIsmi.Location = new System.Drawing.Point(84, 44);
            this.textBoxEkranIsmi.Name = "textBoxEkranIsmi";
            this.textBoxEkranIsmi.Size = new System.Drawing.Size(211, 20);
            this.textBoxEkranIsmi.TabIndex = 2;
            this.textBoxEkranIsmi.TextChanged += new System.EventHandler(this.textBoxEkranIsmi_TextChanged);
            // 
            // btnkaydet
            // 
            this.btnkaydet.Location = new System.Drawing.Point(444, 44);
            this.btnkaydet.Name = "btnkaydet";
            this.btnkaydet.Size = new System.Drawing.Size(115, 23);
            this.btnkaydet.TabIndex = 0;
            this.btnkaydet.Text = "Kaydet";
            this.btnkaydet.UseVisualStyleBackColor = true;
            this.btnkaydet.Click += new System.EventHandler(this.btnkaydet_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // Hak
            // 
            this.Hak.DataPropertyName = "Hak";
            this.Hak.HeaderText = "Hak";
            this.Hak.Name = "Hak";
            this.Hak.ReadOnly = true;
            this.Hak.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Hak.Width = 250;
            // 
            // Var
            // 
            this.Var.DataPropertyName = "Var";
            this.Var.HeaderText = "Var";
            this.Var.Name = "Var";
            this.Var.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Var.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Aktif
            // 
            this.Aktif.DataPropertyName = "Aktif";
            this.Aktif.HeaderText = "Aktif";
            this.Aktif.Name = "Aktif";
            // 
            // Rol
            // 
            this.Rol.DataPropertyName = "Rol";
            this.Rol.HeaderText = "Rol Adi";
            this.Rol.Name = "Rol";
            this.Rol.ReadOnly = true;
            this.Rol.Width = 250;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // frmRolIsHakki
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 744);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmRolIsHakki";
            this.Text = "Rol Hakkı";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Rolgrid)).EndInit();
            this.ustpanel.ResumeLayout(false);
            this.ustpanel.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.IsHaklarigrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView Rolgrid;
        private System.Windows.Forms.Panel ustpanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtbxRolAdi;
        private System.Windows.Forms.Button btnkaydet;
        private System.Windows.Forms.Label labelRol;
        private System.Windows.Forms.Label labelEkranIsmi;
        private System.Windows.Forms.TextBox textBoxEkranIsmi;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView IsHaklarigrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rol;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Hak;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Var;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Aktif;
    }
}
