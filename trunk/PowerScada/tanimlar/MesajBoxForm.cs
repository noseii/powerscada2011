using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AHBS2010
{
	/// <summary>
	/// Mesaj göstermek için kullanlır
	/// </summary>
	public class MesajBoxForm : System.Windows.Forms.Form
	{
		private System.ComponentModel.Container components = null;
        private System.Windows.Forms.Panel panoPanel;
        public Label lblMesaj;
        public CheckBox chkBxBirdahSorma;
        public Button btnHayir;
        public Button btnTamam;
        public Button btnEvet;
        public MesajFormMode Mode = MesajFormMode.SadeceMesaj; 


        public enum MesajFormMode
        {
            SadeceMesaj = 1,
            Ok,
            EvetHayir,
        }

        public MesajBoxForm(string panoBasligi, string mesaj)
            : this(panoBasligi, mesaj, MesajFormMode.SadeceMesaj, false)
        {
            
        }

        public MesajBoxForm(string baslik, string mesaj, MesajFormMode mod, bool withCheckBox)
		{
			InitializeComponent();

            resetAnswer();

            Mode = mod;

            this.Text = baslik.Trim().Length > 0 ? baslik : "";

            chkBxBirdahSorma.Visible = withCheckBox;
            lblMesaj.Text = mesaj;
            lblMesaj.Visible = true;
            switch (Mode)
            {
                case MesajFormMode.SadeceMesaj:
                    chkBxBirdahSorma.Visible = false;
                    lblMesaj.Visible = false;
                    btnTamam.Visible = false;
                    btnEvet.Visible = false;
                    btnHayir.Visible = false;

                    break;
                case MesajFormMode.Ok:
                    btnTamam.Visible = true;
                    btnEvet.Visible = false;
                    btnHayir.Visible = false;
                    
                    break;
                case MesajFormMode.EvetHayir:
                    btnTamam.Visible = false;
                    btnEvet.Visible = true;
                    btnHayir.Visible = true;
                    break;
                default:
                    break;
            }

			this.Focus();

            //WinFormLib.ExtendedProvider.Style.InitializeStyle(panoPanel, "Info");
			
		}

        public bool tamamSecildi = false, evetSecildi = false, hayirsecildi = false;

        void resetAnswer()
        {
            tamamSecildi = false;
            evetSecildi = false;
            hayirsecildi = false;

        }

		private void MesajBoxForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			
            if( e.KeyCode == Keys.Escape)
				this.Close();

		}

        private void btnEvet_Click_1(object sender, EventArgs e)
        {
            resetAnswer();
            evetSecildi = true;
            this.Close();
        }

        private void btnHayir_Click_1(object sender, EventArgs e)
        {
            resetAnswer();
            hayirsecildi = true;
            this.Close();
        }

        private void btnTamam_Click_1(object sender, EventArgs e)
        {
            resetAnswer();
            tamamSecildi = true;
            this.Close();
        }

        private void MesajBoxForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (!tamamSecildi && !evetSecildi)
            //    hayirsecildi = true;

        }


		#region Designer Codes
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.panoPanel = new System.Windows.Forms.Panel();
            this.btnTamam = new System.Windows.Forms.Button();
            this.lblMesaj = new System.Windows.Forms.Label();
            this.chkBxBirdahSorma = new System.Windows.Forms.CheckBox();
            this.btnHayir = new System.Windows.Forms.Button();
            this.btnEvet = new System.Windows.Forms.Button();
            this.panoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panoPanel
            // 
            this.panoPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panoPanel.Controls.Add(this.btnTamam);
            this.panoPanel.Controls.Add(this.lblMesaj);
            this.panoPanel.Controls.Add(this.chkBxBirdahSorma);
            this.panoPanel.Controls.Add(this.btnHayir);
            this.panoPanel.Controls.Add(this.btnEvet);
            this.panoPanel.Location = new System.Drawing.Point(0, 0);
            this.panoPanel.Name = "panoPanel";
            this.panoPanel.Size = new System.Drawing.Size(363, 133);
            this.panoPanel.TabIndex = 1;
            // 
            // btnTamam
            // 
            this.btnTamam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTamam.Location = new System.Drawing.Point(114, 95);
            this.btnTamam.Name = "btnTamam";
            this.btnTamam.Size = new System.Drawing.Size(118, 25);
            this.btnTamam.TabIndex = 11;
            this.btnTamam.Text = "Tamam";
            this.btnTamam.UseVisualStyleBackColor = true;
            this.btnTamam.Click += new System.EventHandler(this.btnTamam_Click_1);
            // 
            // lblMesaj
            // 
            this.lblMesaj.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMesaj.Location = new System.Drawing.Point(3, 0);
            this.lblMesaj.Name = "lblMesaj";
            this.lblMesaj.Size = new System.Drawing.Size(357, 58);
            this.lblMesaj.TabIndex = 15;
            this.lblMesaj.Text = "mesaj";
            this.lblMesaj.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // chkBxBirdahSorma
            // 
            this.chkBxBirdahSorma.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkBxBirdahSorma.AutoSize = true;
            this.chkBxBirdahSorma.Location = new System.Drawing.Point(128, 72);
            this.chkBxBirdahSorma.Name = "chkBxBirdahSorma";
            this.chkBxBirdahSorma.Size = new System.Drawing.Size(114, 17);
            this.chkBxBirdahSorma.TabIndex = 14;
            this.chkBxBirdahSorma.Text = "Bir daha sorma";
            this.chkBxBirdahSorma.UseVisualStyleBackColor = true;
            // 
            // btnHayir
            // 
            this.btnHayir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHayir.Location = new System.Drawing.Point(206, 95);
            this.btnHayir.Name = "btnHayir";
            this.btnHayir.Size = new System.Drawing.Size(118, 25);
            this.btnHayir.TabIndex = 13;
            this.btnHayir.Text = "Hayir";
            this.btnHayir.UseVisualStyleBackColor = true;
            this.btnHayir.Click += new System.EventHandler(this.btnHayir_Click_1);
            // 
            // btnEvet
            // 
            this.btnEvet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEvet.Location = new System.Drawing.Point(24, 95);
            this.btnEvet.Name = "btnEvet";
            this.btnEvet.Size = new System.Drawing.Size(118, 25);
            this.btnEvet.TabIndex = 12;
            this.btnEvet.Text = "Evet";
            this.btnEvet.UseVisualStyleBackColor = true;
            this.btnEvet.Click += new System.EventHandler(this.btnEvet_Click_1);
            // 
            // MesajBoxForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(362, 132);
            this.Controls.Add(this.panoPanel);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "MesajBoxForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MesajBoxForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MesajBoxForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MesajBoxForm_KeyDown);
            this.panoPanel.ResumeLayout(false);
            this.panoPanel.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

        

		#endregion

       
       


	}
}
