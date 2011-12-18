using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using mymodel;
using SharpBullet.OAL;
using SharpBullet.ActiveRecord;

namespace AHBS2010
{
    public partial class frmHizmetAta :frmDialogBase
    {
        BindingSource bstumhizmetler = new BindingSource();
        BindingSource bsmuayenehizmetler = new BindingSource();
        

        #region variables

        List<Hizmet> hizmetler = new List<Hizmet>();
        List<MuayeneHizmet> muayenehizmetlistesi = new List<MuayeneHizmet>();


        #endregion

        private MuayeneHizmet muayenehizmetentiy;

        public MuayeneHizmet MuayeneHizmetEntity
        {
            get
            {
                if (muayenehizmetentiy == null)
                    muayenehizmetentiy = (MuayeneHizmet)CommandNew();

                return (MuayeneHizmet)formEntity;
            }
            set
            {
                muayenehizmetentiy = value;
            }
        }



         public frmHizmetAta()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmTaniAta_Load);
            this.formState = mymodel.myenum.EditMode.emYeni;
            InitializeForm();
          
        }

         public frmHizmetAta(long Id, mymodel.myenum.EditMode formstate)
        {
            InitializeComponent();
            this.formState = formstate;

            this.Load += new EventHandler(frmTaniAta_Load);
            InitializeForm(Id, formstate);
            //InitDataControl();
         
        }

       
       

        void frmTaniAta_Load(object sender, EventArgs e)
        {
            
            #region Eski kodlar
            //bstumhizmetler.DataSource = mycommon.myUtil.OpenSqlIntoDataTable(
            //    "select Id,UstTeshis_Id,Adi Tani,Kodu from teshis where aktif=1 order by adi");
            //trltum.DataSource = bstumhizmetler;
            //if (hastalikmi)                
            //bshastatani.DataSource = mycommon.myUtil.OpenSqlIntoDataTable(
            //    "select t.Id,t.UstTeshis_Id,t.Adi Tani,t.Kodu,h.Alerjikmi Alerjik,h.Kronikmi Kronik " +
            //    "    from MuayeneHizmet h " +
            //    "    join muayene m on m.Id=h.muayene_Id"+
            //    "    join teshis t on t.Id=h.teshis_Id" +
            //    "    where h.aktif=1 and (h.Kronikmi=1 or h.Alerjikmi=1) and m.hasta_Id=" + hastaId);
            //else
            //    bshastatani.DataSource = mycommon.myUtil.OpenSqlIntoDataTable(
            //    "select t.Id,t.UstTeshis_Id,t.Adi Tani,t.Kodu,h.Alerjikmi Alerjik,h.Kronikmi Kronik " +
            //    "    from MuayeneHizmet h " +
            //    "    join teshis t on t.Id=h.teshis_Id" +
            //    "    where h.aktif=1 and h.muayene_Id=" + muayeneId);
            #endregion


            #region Hizmetler doluyor

            Hizmet[] hizmetlistesi = new Hizmet[Current.aktifhizmetler.Values.Count];
            Current.aktifhizmetler.Values.CopyTo(hizmetlistesi, 0);
            if (hizmetlistesi != null && hizmetlistesi.Length > 0)
                hizmetler.AddRange(hizmetlistesi);

            foreach (Hizmet hentity in hizmetlistesi)
            {
                if (hentity.HizmetTur.Id > 0)
                    hentity.HizmetTur = Current.GetHizmetTuru(hentity.HizmetTur.Id);
            }
            TumHizmetlerBind();

            #region GridStyle

            Utility.SetGridStyle(trltum);
            
            for (int i = 0; i < trltum.Columns.Count; i++)
            {
                if (trltum.Columns[i].Name.Contains("Puani") ||
                    trltum.Columns[i].Name.Contains("Tasiyicimi") ||
                    trltum.Columns[i].Name.Contains("Aciklama") ||
                    trltum.Columns[i].Name.Contains("Aciklama") ||
                    trltum.Columns[i].Name.Contains("UstHizmet"))
                {
                    trltum.Columns[i].Visible = false;
                }
                else
                if (trltum.Columns[i].Name.Contains("Kodu"))
                  
                {
                    trltum.Columns[i].Width = 25;
                }
                else
                if (trltum.Columns[i].Name.Contains("Adi"))
                {
                    trltum.Columns[i].Width = 420;
                }
                else
                    if (trltum.Columns[i].Name.Contains("HizmetTur"))
                    {
                        trltum.Columns[i].Width = 70;
                    }

            }
            #endregion

            #endregion

            #region Muayene Hizmetleri doluyor

            MuayeneHizmet[] Muayenehizmetleri = Persistence.ReadList<MuayeneHizmet>("Select * from MuayeneHizmet where Muayene_Id=@prm0 and aktif=1", new object[] { Current.AktifMuayeneId });
            if (Muayenehizmetleri != null && Muayenehizmetleri.Length > 0)
                muayenehizmetlistesi.AddRange(Muayenehizmetleri);
            foreach (MuayeneHizmet mentity in muayenehizmetlistesi)
            {
                mentity.Hizmet = Current.GetHizmet(mentity.Hizmet.Id);
                if (mentity.Hizmet.HizmetTur.Id > 0)
                    mentity.Hizmet.HizmetTur = Current.GetHizmetTuru(mentity.Hizmet.HizmetTur.Id);

            }
            MuayeneHizmetleriBind();

            #region GridStyle

            Utility.SetGridStyle(treeMuayeneHizmeti);
            for (int i = 0; i < treeMuayeneHizmeti.Columns.Count; i++)
            {
                if (treeMuayeneHizmeti.Columns[i].FieldName=="Doktor" ||
                    treeMuayeneHizmeti.Columns[i].FieldName == "UstHizmet" ||
                    treeMuayeneHizmeti.Columns[i].FieldName=="Muayene")
                {
                    treeMuayeneHizmeti.Columns[i].Visible = false;
                }
                else
                    if (treeMuayeneHizmeti.Columns[i].Name == "HizmetKodu")
                    {
                        treeMuayeneHizmeti.Columns[i].Width = 25;
                    }
                    else
                        if (treeMuayeneHizmeti.Columns[i].FieldName == "HizmetTuru")
                        {
                            treeMuayeneHizmeti.Columns[i].Width = 70;
                        }
                        else
                        if (treeMuayeneHizmeti.Columns[i].FieldName=="Hizmet")
                        {
                            treeMuayeneHizmeti.Columns[i].Width = 420;
                        }
                       
            }
            #endregion

            #endregion

            #region eventler

            btnaktarnormal.Click += new EventHandler(btnaktar_Click);
            btniptal.Click += new EventHandler(btniptal_Click);
            btntamam.Click += new EventHandler(btntamam_Click);
            trltum.MouseDoubleClick += new MouseEventHandler(trltum_MouseDoubleClick);
            treeMuayeneHizmeti.MouseDoubleClick += new MouseEventHandler(treeMuayeneHizmeti_MouseDoubleClick);
            #endregion
            btntamam.Text = "Kaydet";
        }

        void treeMuayeneHizmeti_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MuayenedenHizmetcikar();
        }

        void trltum_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MuayeneyeHizmetAktar();
        }

        private void MuayeneHizmetleriBind()
        {
            bsmuayenehizmetler.DataSource = muayenehizmetlistesi;
            treeMuayeneHizmeti.DataSource = bsmuayenehizmetler;
            treeMuayeneHizmeti.RefreshDataSource();
        }

        private void TumHizmetlerBind()
        {
            bstumhizmetler.DataSource = hizmetler;
            trltum.DataSource = bstumhizmetler;
            trltum.RefreshDataSource();
        }

        void btntamam_Click(object sender, EventArgs e)
        {


            if (muayenehizmetlistesi != null && muayenehizmetlistesi.Count > 0)
            {
                Transaction.Instance.Join(
                              delegate()
                              {
                                  try
                                  {
                                      int sonuc = Transaction.Instance.ExecuteNonQuery("Delete from MuayeneHizmet where Muayene_Id=" + muayenehizmetlistesi[0].Muayene.Id);
                                  }
                                  catch (Exception ex)
                                  {

                                      throw new Exception("Hata:" + ex.Message);
                                  }
                                  foreach (MuayeneHizmet item in muayenehizmetlistesi)
                                  {
                                      item.Id = 0;
                                      item.Hasta.Id = Current.AktifHastaId;
                                      item.Hasta = Current.AktifHasta;
                                      item.Doktor.Id = Current.AktifHasta.Doktor.Id;
                                      if (Current.AktifDoktorId != item.Doktor.Id)
                                      {
                                          item.VekilDoktor.Id = Current.AktifDoktorId;
                                          item.VekilDoktor = Current.AktifDoktor;
                                      }
                                      if (Current.AktifMuayeneId > 0)
                                      {
                                          item.Muayene.Id = Current.AktifMuayeneId;
                                          item.Muayene = Current.AktifMuayene;
                                          
                                          //Muayene.UpdateMuayenedurumu(Current.AktifMuayeneId, myenum.MuayeneDurumu.MuayeneEdildi);

                                      }
                                     


                                      if (Current.AktifRandevuId > 0)
                                      {
                                          if (Convert.ToDateTime(item.EklemeTarihi.ToShortDateString()) < Current.AktifRandevu.BasTarih)
                                              throw new Exception("İleri tarihli bir randevu işlem yapılamaz.");

                                          item.Randevu.Id = Current.AktifRandevuId;
                                          item.Randevu = Current.AktifRandevu;
                                          Takvim.UpdateTakvimDurumu(Current.AktifRandevuId, myenum.RandevuDurumu.Geldi);
                                      }
                                      item.Insert();

                                  }
                              }
                      );
            }
            else
            {
                Transaction.Instance.Join(
                             delegate()
                             {
                                 try
                                 {
                                     int sonuc = Transaction.Instance.ExecuteNonQuery("Delete from MuayeneHizmet where Muayene_Id=" + Current.AktifMuayeneId);
                                 }
                                 catch (Exception ex)
                                 {

                                     throw new Exception("Hata:" + ex.Message);
                                 }
                                
                             }
                     );

            }
          

        }

        public override void formtamam()
        {

        }
        
        void btniptal_Click(object sender, EventArgs e)
        {
            MuayenedenHizmetcikar();
        }

        private void MuayenedenHizmetcikar()
        {
            if (bsmuayenehizmetler.Current != null)
            {
                long HizmetId = (bsmuayenehizmetler.Current as MuayeneHizmet).Hizmet.Id;
                MuayeneHizmet akatarilacakhizmet = muayenehizmetlistesi.Find(delegate(MuayeneHizmet mteshis)
                {
                    return mteshis.Hizmet.Id == HizmetId;
                }
                );

                muayenehizmetlistesi.Remove(akatarilacakhizmet);
                MuayeneHizmetleriBind();
            }
            else
                MessageBox.Show("Muayeden çıkarmak istediniz hizmeti seçmediniz");
        }

        void btnaktar_Click(object sender, EventArgs e)
        {
            MuayeneyeHizmetAktar();
        }

        private void MuayeneyeHizmetAktar()
        {
            if (bstumhizmetler.Current != null)
            {
                long HizmetId = (bstumhizmetler.Current as Hizmet).Id;
                Hizmet aktarilacakhizmet = hizmetler.Find(delegate(Hizmet hizmet)
                {
                    return hizmet.Id == HizmetId;
                }
                );


                bool varmi = muayenehizmetlistesi.Exists(delegate(MuayeneHizmet mhizmet)
                        {
                            if (mhizmet.Hizmet.Id == aktarilacakhizmet.Id)
                                return true;
                            else
                                return false;
                        }
                   );

                if (varmi)
                {
                    MessageBox.Show("Bu hizmet listede mevcut.");
                    return;
                }
                else
                if (aktarilacakhizmet!=null && aktarilacakhizmet.Id > 0 && !aktarilacakhizmet.Tasiyicimi)
                {
                    MuayeneHizmet mueyenehizmet = new MuayeneHizmet();
                    mueyenehizmet.Hizmet = aktarilacakhizmet;
                    mueyenehizmet.Hizmet.Id = aktarilacakhizmet.Id;
                    mueyenehizmet.Muayene = Current.AktifMuayene;
                    mueyenehizmet.Muayene.Id = Current.AktifMuayeneId;
                    mueyenehizmet.Aktif = true;
                    muayenehizmetlistesi.Add(mueyenehizmet);
                    MuayeneHizmetleriBind();
                    
                }
                else
                {
                    MessageBox.Show("Üst Başlıklar hizmet olarak aktarılamaz");

                }

            }
            else
                MessageBox.Show("Muayeneye eklemek istediğiniz hizmeti seçmediniz");
        }

        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            filtrele(textBoxFilter.Text);
        }

        private void filtrele(string filter)
        {
            //TODO:Koduna göre filtrelediğin ağaç oluşmuyor..
            
            string kolonname = string.Empty;

            if (radioButtonAdi.Checked)
                kolonname = "Adi";
            else
                if (radioButtonKodu.Checked)
                    kolonname = "Kodu";

            
           
                hizmetler.Clear();
                Hizmet[] hizmetlistesi = Persistence.ReadList<Hizmet>("Select * from Hizmet where " + kolonname + " like '%" + filter + "%' and Aktif=1");
                if (hizmetlistesi != null && hizmetlistesi.Length > 0)
                    hizmetler.AddRange(hizmetlistesi);
                else
                    hizmetler.Clear();

                foreach (Hizmet hentity in hizmetler)
                {
                    if (hentity.HizmetTur.Id>0)
                        hentity.HizmetTur = Current.GetHizmetTuru(hentity.HizmetTur.Id);
                }


                TumHizmetlerBind();
          
        }
  
    }
}