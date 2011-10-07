using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpBullet.OAL;
using mymodel;
using System.Xml.Linq;



namespace PowerScada
{
    public partial class frmProgramDegisiklikler : Form
    {
        BindingSource bs = new BindingSource();
        public frmProgramDegisiklikler()
        {
            InitializeComponent();
            grd.DoubleClick += new EventHandler(grd_DoubleClick);
            this.Load += new EventHandler(frmProgramDegisiklikler_Load);
        }

        void frmProgramDegisiklikler_Load(object sender, EventArgs e)
        {
            XDocument doc = XDocument.Load("ProgramDegisiklikleri.xml");
            var kk=  from p in doc.Elements("Isler").Elements("Kayit")
                         select new
                         {
                             Versiyon = p.Element("Versiyon").Value,
                             Aciklama = p.Element("Aciklama").Value,
                             Tur = p.Element("Tur").Value,
                             Tarih = p.Element("Tarih").Value
                         };

            grd.DataSource = kk.ToList();
            grd.Columns[1].Width = 500;
        }

        void grd_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Transaction.Instance.ExecuteNonQuery(edtsql.Text);
            }
            catch
            { }
        }

      
       
    }
}
