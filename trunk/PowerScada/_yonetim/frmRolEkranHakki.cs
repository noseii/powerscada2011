using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using mymodel;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using SharpBullet.OAL;
using System.Reflection;

namespace PowerScada
{
    public partial class frmRolEkranHakki : frmBase
    {
        public frmRolEkranHakki()
        {
            InitializeComponent();
            //ucilce1.setcombo();
            ToolStripButton kullaniciHaklari=new ToolStripButton();
            kullaniciHaklari.Text="İş Hakları Formu";
            kullaniciHaklari.Click += new EventHandler(button1_Click);
            this.dbnav.Items.Add(kullaniciHaklari);
           
        }

        RolEkranHakki rolekranhakki;
        
        public RolEkranHakki RolEkranHakki 
        { 
            get
            {
                return ((RolEkranHakki)formEntity); ;   
            }
            set
            {
                rolekranhakki = value;
            }
            
        }

        public override void fillgrd()
        {
            BindingList<RolEkranHakki> rolekranhakkilistesi = new BindingList<RolEkranHakki>();
            RolEkranHakki[] rolekranhaklari = Persistence.ReadList<RolEkranHakki>();


            if (rolekranhaklari != null && rolekranhaklari.Length > 0)
            {
                foreach (Entity item in rolekranhaklari)
                {
                    rolekranhakkilistesi.Add((RolEkranHakki)item);
                }
            }
            formbs.DataSource = rolekranhakkilistesi;
            base.fillgrd();

            
        }

        public override void updatedata()
        {
            base.updatedata();
            RolEkranHakki.Rol = (myenum.GorevTuru)ucEnumGosterRol.Deger;
            RolEkranHakki.EkranAdi=edtCombobox.SelectedItem.ToString();
            RolEkranHakki.Degistir=checkBoxDegistir.Checked;
            RolEkranHakki.Ekle=checkBoxekle.Checked;
            RolEkranHakki.Izle=checkBoxIzle.Checked;
            RolEkranHakki.Sil=checkBoxSil.Checked;
          
        }

        public override void showdata()
        {
            base.showdata();

            ucEnumGosterRol.Deger = (myenum.GorevTuru)RolEkranHakki.Rol;
            edtCombobox.SelectedItem = RolEkranHakki.EkranAdi;
            checkBoxDegistir.Checked = RolEkranHakki.Degistir;
            checkBoxekle.Checked = RolEkranHakki.Ekle;
            checkBoxIzle.Checked = RolEkranHakki.Izle;
            checkBoxSil.Checked = RolEkranHakki.Sil;
       }


        public override void InitdataControls()
        {
            Assembly a = Assembly.GetEntryAssembly();

            foreach (Type item in a.GetTypes())
            {
                if (item.BaseType!=null &&
                    ((item.BaseType.ToString() == "PowerScada.frmBase") || (item.BaseType.ToString() == "PowerScada.frmDialogBase") || item.BaseType.ToString() == "DevExpress.XtraEditors.XtraForm"))
                {
                    if (item.Name != "MainForm" && item.Name != "SimpleTreeLookup"
                        && item.Name != "SimpleLookup" && item.Name != "SimpleTreeLookup" && item.Name != "frmSplash")
                    edtCombobox.Properties.Items.Add(item.Name.Replace("frm",""));
                }

            }
        }

        public override void SetGridStyle()
        {
            grdv.Columns["EklemeTarihi"].Visible = false;
            grdv.Columns["DegistirmeTarihi"].Visible = false;
            grdv.Columns["EkleyenMakAdres"].Visible = false;
            grdv.Columns["EkleyenKullanici"].Visible = false;
            grdv.Columns["DegistirenKullanici"].Visible = false;
            grdv.Columns["DegistirenMakAdres"].Visible = false;
            grdv.Columns["RowVersion"].Visible = false;
            grdv.Columns["Id"].Visible = false;
        
           
        }

        protected override Entity CommandNew()
        {
            return new mymodel.RolEkranHakki();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmRolIsHakki frm = new frmRolIsHakki();
            frm.ShowDialog();
            
        }

      
        

     
        
    }
}

