namespace PowerScada
{
    partial class UCModem
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
            this.Label = new System.Windows.Forms.Label();
            this.Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.player)).BeginInit();
            this.SuspendLayout();
            // 
            // player
            // 
            this.player.Location = new System.Drawing.Point(0, 3);
            this.player.Size = new System.Drawing.Size(37, 23);
            // 
            // Label
            // 
            this.Label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.Label.Location = new System.Drawing.Point(4, 11);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(51, 13);
            this.Label.TabIndex = 0;
            this.Label.Text = "Cihaz Adi";
            this.Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button
            // 
            this.Button.Dock = System.Windows.Forms.DockStyle.Right;
            this.Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Button.Location = new System.Drawing.Point(61, 0);
            this.Button.Name = "Button";
            this.Button.Size = new System.Drawing.Size(51, 26);
            this.Button.TabIndex = 1;
            this.Button.Text = "Kapat";
            this.Button.UseVisualStyleBackColor = true;
            this.Button.Click += new System.EventHandler(this.button1_Click);
            // 
            // UCModem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Button);
            this.Controls.Add(this.Label);
            this.Name = "UCModem";
            this.Size = new System.Drawing.Size(112, 26);
            this.Controls.SetChildIndex(this.player, 0);
            this.Controls.SetChildIndex(this.Label, 0);
            this.Controls.SetChildIndex(this.Button, 0);
            ((System.ComponentModel.ISupportInitialize)(this.player)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Label;
        public System.Windows.Forms.Button Button;

    }
}
