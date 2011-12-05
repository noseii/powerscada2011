namespace PowerScada
{
    partial class frmProgramDegisiklikler
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
            this.edtsql = new System.Windows.Forms.TextBox();
            this.grd = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
            this.SuspendLayout();
            // 
            // edtsql
            // 
            this.edtsql.BackColor = System.Drawing.SystemColors.Info;
            this.edtsql.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.edtsql.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.edtsql.Location = new System.Drawing.Point(0, 470);
            this.edtsql.Name = "edtsql";
            this.edtsql.Size = new System.Drawing.Size(836, 13);
            this.edtsql.TabIndex = 3;
            // 
            // grd
            // 
            this.grd.AllowUserToAddRows = false;
            this.grd.AllowUserToDeleteRows = false;
            this.grd.AllowUserToOrderColumns = true;
            this.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd.Location = new System.Drawing.Point(0, 0);
            this.grd.Name = "grd";
            this.grd.ReadOnly = true;
            this.grd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grd.Size = new System.Drawing.Size(836, 470);
            this.grd.TabIndex = 4;
            // 
            // frmProgramDegisiklikler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 483);
            this.Controls.Add(this.grd);
            this.Controls.Add(this.edtsql);
            this.Name = "frmProgramDegisiklikler";
            this.Text = "Programda yapılan yenilik ve değişiklikler";
            ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox edtsql;
        private System.Windows.Forms.DataGridView grd;
    }
}