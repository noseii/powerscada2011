using System.Diagnostics;
using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System;

namespace PowerScada
{
    namespace Forms
    {
        partial class frmSplash : System.Windows.Forms.Form
        {

            //Form overrides dispose to clean up the component list.
            [System.Diagnostics.DebuggerNonUserCode()]
            protected override void Dispose(bool disposing)
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }

            //Required by the Windows Form Designer
            private System.ComponentModel.Container components = null;

            //NOTE: The following procedure is required by the Windows Form Designer
            //It can be modified using the Windows Form Designer.
            //Do not modify it using the code editor.
            [System.Diagnostics.DebuggerStepThrough()]
            private void InitializeComponent()
            {
                this.lblVersion = new System.Windows.Forms.Label();
                this.lblLicencee = new System.Windows.Forms.Label();
                this.lblApplicationName = new System.Windows.Forms.Label();
                this.lblApplicationText = new System.Windows.Forms.Label();
                this.lblCopyright = new System.Windows.Forms.Label();
                this.pbApplicationImage = new System.Windows.Forms.PictureBox();
                ((System.ComponentModel.ISupportInitialize)(this.pbApplicationImage)).BeginInit();
                this.SuspendLayout();
                // 
                // lblVersion
                // 
                this.lblVersion.BackColor = System.Drawing.Color.Transparent;
                this.lblVersion.ForeColor = System.Drawing.Color.White;
                this.lblVersion.Location = new System.Drawing.Point(333, 338);
                this.lblVersion.Name = "lblVersion";
                this.lblVersion.Size = new System.Drawing.Size(182, 15);
                this.lblVersion.TabIndex = 3;
                this.lblVersion.TextAlign = System.Drawing.ContentAlignment.TopRight;
                // 
                // lblLicencee
                // 
                this.lblLicencee.BackColor = System.Drawing.Color.Transparent;
                this.lblLicencee.Location = new System.Drawing.Point(44, 16);
                this.lblLicencee.Name = "lblLicencee";
                this.lblLicencee.Size = new System.Drawing.Size(471, 18);
                this.lblLicencee.TabIndex = 4;
                this.lblLicencee.TextAlign = System.Drawing.ContentAlignment.TopRight;
                // 
                // lblApplicationName
                // 
                this.lblApplicationName.AutoSize = true;
                this.lblApplicationName.BackColor = System.Drawing.Color.Transparent;
                this.lblApplicationName.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.lblApplicationName.Location = new System.Drawing.Point(8, 260);
                this.lblApplicationName.Name = "lblApplicationName";
                this.lblApplicationName.Size = new System.Drawing.Size(0, 18);
                this.lblApplicationName.TabIndex = 5;
                // 
                // lblApplicationText
                // 
                this.lblApplicationText.AutoSize = true;
                this.lblApplicationText.BackColor = System.Drawing.Color.Transparent;
                this.lblApplicationText.Location = new System.Drawing.Point(8, 280);
                this.lblApplicationText.Name = "lblApplicationText";
                this.lblApplicationText.Size = new System.Drawing.Size(0, 13);
                this.lblApplicationText.TabIndex = 6;
                // 
                // lblCopyright
                // 
                this.lblCopyright.AutoSize = true;
                this.lblCopyright.BackColor = System.Drawing.Color.Transparent;
                this.lblCopyright.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.lblCopyright.ForeColor = System.Drawing.Color.Gray;
                this.lblCopyright.Location = new System.Drawing.Point(9, 340);
                this.lblCopyright.Name = "lblCopyright";
                this.lblCopyright.Size = new System.Drawing.Size(194, 11);
                this.lblCopyright.TabIndex = 7;
                this.lblCopyright.Text = "copyright © 2006, Simyaci technology limited";
                // 
                // pbApplicationImage
                // 
                this.pbApplicationImage.BackColor = System.Drawing.Color.Transparent;
                this.pbApplicationImage.Location = new System.Drawing.Point(0, 47);
                this.pbApplicationImage.Name = "pbApplicationImage";
                this.pbApplicationImage.Size = new System.Drawing.Size(519, 198);
                this.pbApplicationImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.pbApplicationImage.TabIndex = 8;
                this.pbApplicationImage.TabStop = false;
                // 
                // frmSplash
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.ClientSize = new System.Drawing.Size(520, 360);
                this.ControlBox = false;
                this.Controls.Add(this.pbApplicationImage);
                this.Controls.Add(this.lblCopyright);
                this.Controls.Add(this.lblApplicationText);
                this.Controls.Add(this.lblApplicationName);
                this.Controls.Add(this.lblLicencee);
                this.Controls.Add(this.lblVersion);
                this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.Name = "frmSplash";
                this.ShowIcon = false;
                this.ShowInTaskbar = false;
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                this.Text = "frmSplash";
                ((System.ComponentModel.ISupportInitialize)(this.pbApplicationImage)).EndInit();
                this.ResumeLayout(false);
                this.PerformLayout();

            }
            public System.Windows.Forms.Label lblVersion;
            public System.Windows.Forms.Label lblLicencee;
            public System.Windows.Forms.Label lblApplicationName;
            public System.Windows.Forms.Label lblApplicationText;
            public System.Windows.Forms.Label lblCopyright;
            public System.Windows.Forms.PictureBox pbApplicationImage;
        }
    }

}
