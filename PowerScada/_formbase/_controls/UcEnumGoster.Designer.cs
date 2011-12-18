namespace PowerScada
{
    partial class UcEnumGoster
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.edtCombobox = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.edtCombobox.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // edtCombobox
            // 
            this.edtCombobox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edtCombobox.Location = new System.Drawing.Point(0, 0);
            this.edtCombobox.Name = "edtCombobox";
            this.edtCombobox.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.edtCombobox.Properties.ImmediatePopup = true;
            this.edtCombobox.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.edtCombobox.Size = new System.Drawing.Size(169, 20);
            this.edtCombobox.TabIndex = 46;
            this.edtCombobox.SelectedValueChanged += new System.EventHandler(this.edtCombobox_SelectedValueChanged);
            // 
            // UcEnumGoster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.edtCombobox);
            this.Name = "UcEnumGoster";
            this.Size = new System.Drawing.Size(169, 20);
            ((System.ComponentModel.ISupportInitialize)(this.edtCombobox.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.ComboBoxEdit edtCombobox;



    }
}
