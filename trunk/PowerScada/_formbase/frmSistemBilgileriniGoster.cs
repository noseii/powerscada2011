using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mymodel;

namespace PowerScada
{
    public partial class frmSistemBilgileriniGoster : Form
    {
        Entity entity;
        public frmSistemBilgileriniGoster(Entity baseentity)
        {
            InitializeComponent();

            entity = baseentity;
            ShowForm();
        }


        public void ShowForm()
        {
            textEdit1.Text = entity.EkleyenKullanici;
            textEdit2.Text = entity.DegistirenKullanici;
            
            if (entity.EklemeTarihi != System.DateTime.MinValue)
                dateEdit1.DateTime = entity.EklemeTarihi;

            if(entity.DegistirmeTarihi!=System.DateTime.MinValue)
                dateEdit2.DateTime = entity.DegistirmeTarihi;

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
