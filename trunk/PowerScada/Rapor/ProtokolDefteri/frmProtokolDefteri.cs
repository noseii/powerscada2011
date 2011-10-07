using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mymodel;
using SharpBullet.OAL;

namespace AHBS2010.Rapor
{
    public partial class frmProtokolDefteri : Form
    {
        public frmProtokolDefteri()
        {
            InitializeComponent();

            dateEditBasTarih.DateTime = System.DateTime.Today;
            dateEditBitTarih.DateTime = System.DateTime.Today;
            simpleButtonGetir.Click += new EventHandler(simpleButtonGetir_Click);
            simpleButtonExceleAktar.Click += new EventHandler(simpleButtonExceleAktar_Click);
            simpleButtonKapat.Click += new EventHandler(simpleButtonKapat_Click);
            simpleButtonOnizleme.Click += new EventHandler(simpleButtonOnizleme_Click);

           
        }

        void simpleButtonOnizleme_Click(object sender, EventArgs e)
        {
            ReportProtokolDefteri rprprotokoldefteri = new ReportProtokolDefteri();
            rprprotokoldefteri.DataSource = Getir(dateEditBasTarih.DateTime, dateEditBitTarih.DateTime, editButtondoktor.Id); 
            rprprotokoldefteri.DataMember = "Table";
            rprprotokoldefteri.Parameters[1].Value = dateEditBasTarih.DateTime;
            rprprotokoldefteri.Parameters[0].Value = dateEditBitTarih.DateTime;
            rprprotokoldefteri.ShowPreview();
        }

        void simpleButtonKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void simpleButtonExceleAktar_Click(object sender, EventArgs e)
        {
            SaveFileDialog dvdialog=new SaveFileDialog();

            dvdialog.Filter = "Excel Dosyasu (*.xlsx)|*.xlsx";
            if(dvdialog.ShowDialog()==System.Windows.Forms.DialogResult.OK)
            {
                gridViewProtokolDefteri.BestFitColumns();
                gridControlProtokolDefteri.ExportToXlsx(dvdialog.FileName);
            }
        }
        bool yuklendi = false;
        void simpleButtonGetir_Click(object sender, EventArgs e)
        {
            Sonuc sonuc=Validasyon();
            if (!sonuc.HataVarMi)
            {
                gridControlProtokolDefteri.DataSource = Getir(dateEditBasTarih.DateTime, dateEditBitTarih.DateTime, editButtondoktor.Id);
                //if (!yuklendi)
                //{
                    
                    gridControlProtokolDefteri.SetGridStyle(
                     @" <Style>
                    <Column Name='SIRANO' HeaderText='Sıra No' Width='60' DisplayIndex='1'  />                    
                    <Column Name='MTARIHI' HeaderText='Muayene Tarihi' Width='100' DisplayIndex='2'  />
                    <Column Name='MURACATSAATI' HeaderText='Müracat Saati' Width='90' DisplayIndex='3' />
                    <Column Name='MUAYENESAATI' HeaderText='M.Saati' Width='100' DisplayIndex='4' />
                    <Column Name='PROTOKOLNO' HeaderText='Protokol No' Width='100' DisplayIndex='6' />
                    <Column Name='ADISOYADI' HeaderText='Hasta Adı Soyadı' Width='150' DisplayIndex='7'  />
                    <Column Name='CINSIYET' HeaderText='Cinsiyet' Width='70' DisplayIndex='8' />
                    <Column Name='DOGUMTARIHI' HeaderText='Doğum Tarihi' Width='90' DisplayIndex='9' />
                    <Column Name='KURUMTIPI' HeaderText='Geldiği Yer' Width='75' DisplayIndex='10' />
                    <Column Name='TANI' HeaderText='Teşhis' Width='450' DisplayIndex='11' />
                    <Column Name='MUAYENESONUCU' HeaderText='Muayene Sonucu' Width='450' DisplayIndex='12' />
                    <Column Name='DADISOYADI' HeaderText='Doktor' Width='150' DisplayIndex='13' />
                    </Style>");
                    //yuklendi = true;
                //}
            }
            else
                MessageBox.Show(sonuc.Mesaj);
        }

        private Sonuc Validasyon()
        {
            if (editButtondoktor.Id == 0)
                return new Sonuc(true, "Protokol defterini getirmek için doktor seçmelisiniz");

            return new Sonuc(false, "");
        }

        private DataTable Getir(DateTime bastarih, DateTime bittarih, long doktor)
        {
            return Current.GetProtokolDefteri(bastarih, bittarih, doktor);
        }
      
    }
}
