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
using DevExpress.XtraTreeList.Nodes;

namespace AHBS2010
{
    public partial class frmTaniAta :frmDialogBase
    { 
        #region variables
        
        BindingSource bstumtani = new BindingSource();
        BindingSource bsdoktortani = new BindingSource();
        BindingSource bsfavoritani = new BindingSource();
        BindingSource bshastatani = new BindingSource();


        List<Teshis> teshisler = new List<Teshis>();
        List<MuayeneTeshis> muayeneteshislistesi = new List<MuayeneTeshis>();
        List<DoktorTeshis> doktorteshislistesi = new List<DoktorTeshis>();

        #endregion
        
        public frmTaniAta()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmTaniAta_Load);
            this.formState = mymodel.myenum.EditMode.emYeni;
            InitializeForm();
            
        }

        public frmTaniAta(long Id, mymodel.myenum.EditMode formstate)
        {
            InitializeComponent();
            this.formState = formstate;

            this.Load += new EventHandler(frmTaniAta_Load);
            InitializeForm(Id, formstate);
           
        }
     

        void frmTaniAta_Load(object sender, EventArgs e)
         {
                #region eski kodlar
             //bstumtani.DataSource =Transaction.Instance.ExecuteSql(
            //    "select Id,UstTeshis_Id,Adi Tani,Kodu from teshis where aktif=1 order by adi");
            //trltum.DataSource = bstumtani;
            //if (hastalikmi)
            //    bshastatani.DataSource = Transaction.Instance.ExecuteSql(
            //        "select t.Id,t.UstTeshis_Id,t.Adi Tani,t.Kodu,h.Alerjikmi Alerjik,h.Kronikmi Kronik " +
            //        "    from MuayeneTeshis h " +
            //        "    join muayene m on m.Id=h.muayene_Id" +
            //        "    join teshis t on t.Id=h.teshis_Id" +
            //        "    where h.aktif=1 and (h.Kronikmi=1 or h.Alerjikmi=1) and m.hasta_Id=" + hastaId);
            //else
            //    bshastatani.DataSource = Transaction.Instance.ExecuteSql(
            //    "select t.Id,t.UstTeshis_Id,t.Adi Tani,t.Kodu,h.Alerjikmi Alerjik,h.Kronikmi Kronik " +
            //    "    from MuayeneTeshis h " +
            //    "    join teshis t on t.Id=h.teshis_Id" +
            //    "    where h.aktif=1 and h.muayene_Id=" + muayeneId);
            #endregion

                #region Teshisler doluyor

                 Teshis[] teshislistesi=new Teshis[Current.aktifteshisler.Values.Count];
                 Current.aktifteshisler.Values.CopyTo(teshislistesi,0);
                 if (teshislistesi != null && teshislistesi.Length > 0)
                    teshisler.AddRange(teshislistesi);

                TeshisBind();

                #region GridStyle

                Utility.SetGridStyle(trltum);
                for (int i = 0; i < trltum.Columns.Count; i++)
                {
                    if (trltum.Columns[i].Name.Contains("AsiTanim") ||
                        trltum.Columns[i].Name.Contains("UstTeshis") ||
                        trltum.Columns[i].Name.Contains("BildirimiZorunlumu") ||
                         trltum.Columns[i].Name.Contains("Tasiyicimi") ||
                        trltum.Columns[i].Name.Contains("OlumNedenimi"))
                    {
                        trltum.Columns[i].Visible = false;
                    }
                }
                trltum.Columns[0].Width = 400;
                #endregion

              #endregion

                #region doktorteshisdoluyor

                DoktorTeshis[] doktorteshisleri = Persistence.ReadList<DoktorTeshis>("Select * from DoktorTeshis where Doktor_Id=@prm0 and aktif=1",new object[]{Current.AktifDoktorId});
                if (doktorteshisleri != null && doktorteshisleri.Length > 0)
                    doktorteshislistesi.AddRange(doktorteshisleri);

                foreach (DoktorTeshis dentity in doktorteshislistesi)
                {
                    dentity.Teshis =Current.GetTeshis(dentity.Teshis.Id);
                }

                DoktorTeshisBind();

                #region GridStyle
                Utility.SetGridStyle(treeListDoktorTeshis);
                for (int i = 0; i < treeListDoktorTeshis.Columns.Count; i++)
                {
                    if (treeListDoktorTeshis.Columns[i].Name.Contains("Doktor"))
                    {
                        treeListDoktorTeshis.Columns[i].Visible = false;
                    }
                }
                treeListDoktorTeshis.Columns[1].Width = 400;
                #endregion

                #endregion

                #region MuayeneTeshis

                MuayeneTeshis[] MuayeneTeshisleri = Persistence.ReadDetail<MuayeneTeshis>("Muayene_Id", Current.AktifMuayeneId);
                    if (MuayeneTeshisleri != null && MuayeneTeshisleri.Length > 0)
                    {
                        muayeneteshislistesi.AddRange(MuayeneTeshisleri);
                        foreach (MuayeneTeshis mentity in muayeneteshislistesi)
                        {
                            mentity.Teshis = Current.GetTeshis(mentity.Teshis.Id);
                        }
                    }


                    MuayeneTeshisBind();

                   #region GridStyle
                    Utility.SetGridStyle(trlhasta);
                    for (int i = 0; i < trlhasta.Columns.Count; i++)
                    {
                        if (!trlhasta.Columns[i].Name.Contains("colTeshis") &&
                            !trlhasta.Columns[i].Name.Contains("colTeshisKodu")
                           )
                        {
                            trlhasta.Columns[i].Visible = false;
                        }
                    }
                    trlhasta.Columns[0].Width = 250;
                   #endregion

                #endregion

                #region eventler

                    btnaktarnormal.Click += new EventHandler(btnaktar_Click);
            btnkronik.Click += new EventHandler(btnkronik_Click);
            btnalerjik.Click += new EventHandler(btnalerjik_Click);
            btniptal.Click += new EventHandler(btniptal_Click);
            btntamam.Click += new EventHandler(btntamam_Click);

            trltum.MouseDoubleClick += new MouseEventHandler(trltum_MouseDoubleClick);
            trlhasta.MouseDoubleClick += new MouseEventHandler(trlhasta_MouseDoubleClick);
            simpleButtondoktoraAta.Click += new EventHandler(simpleButtondoktoraAta_Click);
            simpleButtonDoktordancikar.Click += new EventHandler(simpleButtonDoktordancikar_Click);
            treeListDoktorTeshis.MouseDoubleClick += new MouseEventHandler(treeListDoktorTeshis_MouseDoubleClick);
            //if(this.formState==myenum.EditMode.emIncele)
                //simpleButtonDoktordancikar.Enabled = false;
            #endregion
            simpleButtondoktoraAta.Enabled = false;
         }

        void treeListDoktorTeshis_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            HastaTeshisAktar(null, TeshisAktarmaTipi.DoktorTeshisListesindenAktar);
        }

        void simpleButtonDoktordancikar_Click(object sender, EventArgs e)
        {
            DoktorTeshisCikar();
        }

        void simpleButtondoktoraAta_Click(object sender, EventArgs e)
        {
            DoktoraTeshisAktar();
        }

        private void TeshisBind()
        {
  
            bstumtani.DataSource = teshisler;
            trltum.DataSource = bstumtani;
            trltum.RefreshDataSource();
        }

        private void DoktorTeshisBind()
        {

            bsdoktortani.DataSource = doktorteshislistesi;
            treeListDoktorTeshis.DataSource = bsdoktortani;
            treeListDoktorTeshis.RefreshDataSource();
        }

        void trlhasta_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            HastaTeshisCikar();
        }

        void trltum_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            HastaTeshisAktar(null,TeshisAktarmaTipi.TeshisListesindenAktar);
        }

        void btntamam_Click(object sender, EventArgs e)
        {
         

            if (muayeneteshislistesi != null && muayeneteshislistesi.Count > 0)
            {
                
                //TODO:Aynı muayenede atanan hastalıklar silinse sıkıntı olurmu

                Transaction.Instance.Join(
                              delegate()
                              {
                                  try
                                  {
                                      int sonuc = Transaction.Instance.ExecuteNonQuery("Delete from MuayeneTeshis where Muayene_Id=" + Current.AktifMuayeneId);
                                  }
                                  catch (Exception ex)
                                  {

                                      throw new Exception("Hata:" + ex.Message);
                                  }
                                  foreach (MuayeneTeshis item in muayeneteshislistesi)
                                  {

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
                                      }

                                      if (Current.AktifRandevuId > 0)
                                      {
                                          item.Randevu.Id = Current.AktifRandevuId;
                                          item.Randevu = Current.AktifRandevu;
                                          if(item.Id==0)
                                              if (Convert.ToDateTime(item.EklemeTarihi.ToShortDateString()) < Current.AktifRandevu.BasTarih)
                                                  throw new Exception("İleri tarihli bir randevu işlem yapılamaz.");


                                      }

                                      if (Current.AktifMuayeneId > 0)
                                      {
                                          if (!item.Kronikmi && !item.Alerjikmi)
                                              Muayene.UpdateMuayenedurumu(Current.AktifMuayeneId, myenum.MuayeneDurumu.MuayeneEdildi);
                                      }
                                      item.Insert();

                                  }

                                     
                                  
                                    if (Current.AktifRandevuId > 0)
                                    {
                                        Takvim.UpdateTakvimDurumu(Current.AktifRandevuId, myenum.RandevuDurumu.Geldi);
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
                                     int sonuc = Transaction.Instance.ExecuteNonQuery("Delete from MuayeneTeshis where Muayene_Id=" + Current.AktifMuayeneId);
                                     Muayene.UpdateMuayenedurumu(Current.AktifMuayeneId, myenum.MuayeneDurumu.Bekliyor);
                                 }
                                 catch (Exception ex)
                                 {

                                     throw new Exception("Hata:" + ex.Message);
                                 }

                             }
                     );
            }
          

        }

        private void MuayeneTeshisBind()
        {
            bshastatani.DataSource = muayeneteshislistesi;
            trlhasta.DataSource = bshastatani;
            trlhasta.RefreshDataSource();
        }

        public override void formtamam()
        {

        }


        void btnalerjik_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage.Name == "tptumu")
            {
                HastaTeshisAktar(sender, TeshisAktarmaTipi.TeshisListesindenAktar);
            }
            else
                if (xtraTabControl1.SelectedTabPage.Name == "tpdoktor")
                {
                    HastaTeshisAktar(sender, TeshisAktarmaTipi.DoktorTeshisListesindenAktar);
                }
        }

        void btnkronik_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage.Name == "tptumu")
            {
                HastaTeshisAktar(sender, TeshisAktarmaTipi.TeshisListesindenAktar);
            }
            else
                if (xtraTabControl1.SelectedTabPage.Name == "tpdoktor")
                {
                    HastaTeshisAktar(sender, TeshisAktarmaTipi.DoktorTeshisListesindenAktar);
                }
        }

        
        void btniptal_Click(object sender, EventArgs e)
        {
            HastaTeshisCikar();
        }

        private void HastaTeshisCikar()
        {
            if (bshastatani.Current != null)
            {
                long teshisId = (bshastatani.Current as MuayeneTeshis).Teshis.Id;
                MuayeneTeshis aktarilacakteshis = muayeneteshislistesi.Find(delegate(MuayeneTeshis mteshis)
                {
                    return mteshis.Teshis.Id == teshisId;
                }
                );

                muayeneteshislistesi.Remove(aktarilacakteshis);

                HastaTeshisBindGrids();


            }
            else
                MessageBox.Show("Çıkarmak istediniz Teşhisi seçmediniz");
        }

        void btnaktar_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage.Name == "tptumu")
            {
                HastaTeshisAktar(sender,TeshisAktarmaTipi.TeshisListesindenAktar);
            }
              else
                if (xtraTabControl1.SelectedTabPage.Name == "tpdoktor")
                {
                    HastaTeshisAktar(sender, TeshisAktarmaTipi.DoktorTeshisListesindenAktar);

                }
        }

        private void HastaTeshisAktar(object gonderen,TeshisAktarmaTipi aktarimtipi)
        {
            Teshis aktarilacakteshis=null;
            if (aktarimtipi == TeshisAktarmaTipi.TeshisListesindenAktar)
            {
                if (bstumtani.Current == null)
                {
                    MessageBox.Show(" Teşhis seçmediniz !");
                    return;
                }


                long teshisId = (bstumtani.Current as Teshis).Id;
                aktarilacakteshis = teshisler.Find(delegate(Teshis teshis)
                {
                    return teshis.Id == teshisId;
                }
                );
                TeshisAktar(gonderen, aktarilacakteshis);
            }
            else
                if (aktarimtipi == TeshisAktarmaTipi.DoktorTeshisListesindenAktar)
                {
                   bool secerekaktarim=false;
                    long teshisId=0;
                    for (int i = 0; i < treeListDoktorTeshis.Nodes.Count; i++)
                    {
                        if (!treeListDoktorTeshis.Nodes[i].HasChildren)
                        {
                            if (treeListDoktorTeshis.Nodes[i].Checked)
                            {
                                secerekaktarim = true;
                                teshisId =((Teshis)(treeListDoktorTeshis.Nodes[i][1])).Id; //(bsdoktortani.Current as DoktorTeshis).Teshis.Id;
                                DoktorTeshis doktorteshis = doktorteshislistesi.Find(delegate(DoktorTeshis dteshis)
                                {
                                    return dteshis.Teshis.Id == teshisId;
                                }
                                );

                                aktarilacakteshis = doktorteshis.Teshis;
                                treeListDoktorTeshis.Nodes[i].Checked = false;
                                TeshisAktar(gonderen, aktarilacakteshis);
                            }
                        }
                    }

                    if (!secerekaktarim)
                    {
                        if (bsdoktortani.Current == null)
                        {
                            MessageBox.Show(" Teşhis seçmediniz !");
                            return;
                        }
                        teshisId = (bsdoktortani.Current as DoktorTeshis).Teshis.Id;
                        DoktorTeshis doktorteshis = doktorteshislistesi.Find(delegate(DoktorTeshis dteshis)
                        {
                            return dteshis.Teshis.Id == teshisId;
                        }
                        );

                        aktarilacakteshis = doktorteshis.Teshis;
                        TeshisAktar(gonderen, aktarilacakteshis);
                    }
                }

          

           
        }

        private void TeshisAktar(object gonderen, Teshis aktarilacakteshis)
        {
           //eskikod=if (aktarilacakteshis != null && aktarilacakteshis.Id > 0 && !aktarilacakteshis.Tasiyicimi
            if (aktarilacakteshis != null && aktarilacakteshis.Id > 0 && !aktarilacakteshis.Kodu.Contains("-"))
            {

                string aktarmatipi = string.Empty;

                if (gonderen != null)
                    aktarmatipi = ((DevExpress.XtraEditors.SimpleButton)(gonderen)).Text;


                MuayeneTeshis doktorteshis = new MuayeneTeshis();
                //doktorteshis.Aciklama = Convert.ToInt16(edtadet.Text);
                if (aktarmatipi == "Kronik Aktar")
                {
                    doktorteshis.Kronikmi = true;
                    doktorteshis.Alerjikmi = false;
                }
                else
                    if (aktarmatipi == "Alerjik Aktar")
                    {
                        doktorteshis.Alerjikmi = true;
                        doktorteshis.Kronikmi = false;
                    }
                    else
                    {
                        doktorteshis.Alerjikmi = false;
                        doktorteshis.Kronikmi = false;
                    }


                doktorteshis.Muayene.Id = Current.AktifMuayeneId;
                doktorteshis.Muayene = Current.AktifMuayene;
                doktorteshis.Teshis.Id = aktarilacakteshis.Id;
                doktorteshis.Teshis = aktarilacakteshis;
                doktorteshis.Aktif = true;

                ///aynı hasta Kronik ve normal alerjik olabilir bunların validasoyonunu kaldıralım.
                bool varmi = muayeneteshislistesi.Exists(delegate(MuayeneTeshis mteshis)
                {
                    if (mteshis.Teshis.Id == doktorteshis.Teshis.Id)
                    {
                        if (mteshis.Alerjikmi == doktorteshis.Alerjikmi && mteshis.Kronikmi == doktorteshis.Kronikmi)
                        {
                            return true;
                        }
                        else
                            return false;

                    }
                    else
                        return false;

                }

               );

                if (varmi)
                {
                    MessageBox.Show("Bu Teshis listede mevcut. Aynı teşhis birden fazla aktarılamaz.");
                    return;
                }


                muayeneteshislistesi.Add(doktorteshis);

                HastaTeshisBindGrids();
            }
            else
            {
                MessageBox.Show("Üst Başlıklar teşhis olarak aktarılamaz");

            }
        }

        private void HastaTeshisBindGrids()
        {
            bshastatani.DataSource = muayeneteshislistesi;
            trlhasta.DataSource = bshastatani;
            trlhasta.RefreshDataSource();

        }

        private void DoktorTeshisBindGrids()
        {
            bsdoktortani.DataSource = doktorteshislistesi;
            treeListDoktorTeshis.DataSource = bsdoktortani;
            treeListDoktorTeshis.RefreshDataSource();
        }

        private void textBoxAdi_TextChanged(object sender, EventArgs e)
        {
            filtrele(textBoxAdi.Text);
        }

        private void filtrele(string filter)
        {
            string kolonname=string.Empty;

            if(radioButtonAdi.Checked)
               kolonname="Adi";
            else
              if(radioButton1.Checked)
                   kolonname="Kodu";
           

            if (xtraTabControl1.SelectedTabPage.Name == "tptumu")
            {
                teshisler.Clear();
                Teshis[] teshislistesi = Persistence.ReadList<Teshis>("Select * from Teshis where " + kolonname + " like '%" + filter + "%' and Aktif=1");
                if (teshislistesi != null && teshislistesi.Length > 0)
                    teshisler.AddRange(teshislistesi);
                else
                    teshisler.Clear();

                TeshisBind();
            }
            else
                if (xtraTabControl1.SelectedTabPage.Name == "tpdoktor")
                {
                    doktorteshislistesi.Clear();
                    DoktorTeshis[] dteshislistesi = Persistence.ReadList<DoktorTeshis>(@"Select DoktorTeshis.* from DoktorTeshis 
                    inner join Teshis on  Teshis.Id=DoktorTeshis.Teshis_Id
                    where Teshis."+kolonname + " like '%" + filter +"%' and Teshis.Aktif=1");
                    if (dteshislistesi != null && dteshislistesi.Length > 0)
                        doktorteshislistesi.AddRange(dteshislistesi);
                    else
                        doktorteshislistesi.Clear();

                    foreach (DoktorTeshis dentity in doktorteshislistesi)
                    {
                        dentity.Teshis = Current.GetTeshis(dentity.Teshis.Id);
                    }

                    DoktorTeshisBindGrids();
                }
        }

        private void DoktoraTeshisAktar()
        {
            if (bstumtani.Current != null)
            {
                long teshisId = (bstumtani.Current as Teshis).Id;
                Teshis aktarilacakteshis = teshisler.Find(delegate(Teshis teshis)
                {
                    return teshis.Id == teshisId;
                }
                );

                //eski kod=if (aktarilacakteshis.Id > 0 && !aktarilacakteshis.Tasiyicimi)
                if (aktarilacakteshis.Id > 0 && !aktarilacakteshis.Kodu.Contains("-"))
                {
                    DoktorTeshis doktorteshis = new DoktorTeshis();
                    doktorteshis.Doktor.Id = Current.AktifDoktorId;
                    doktorteshis.Doktor = Current.AktifDoktor;
                    doktorteshis.Teshis.Id = aktarilacakteshis.Id;
                    doktorteshis.Teshis = aktarilacakteshis;
                    doktorteshis.Aktif = true;

                    bool varmi = doktorteshislistesi.Exists(delegate(DoktorTeshis dteshis)
                        {
                            if (dteshis.Teshis.Id == aktarilacakteshis.Id)
                            {
                                return true;
                            }
                            else
                                return false;

                        }
                    );

                    if (varmi)
                    {
                        MessageBox.Show("Bu Teshis listede mevcut.");
                        return;
                    }
                    try
                    {
                        doktorteshis.Insert();
                    }
                    catch (Exception ex)
                    {
                        
                        throw new Exception("Hata:"+ex.Message);
                    }
                
                    doktorteshislistesi.Add(doktorteshis);

                    DoktorTeshisBindGrids();
                }
                else
                {
                    MessageBox.Show("Üst Başlıklar teşhis olarak aktarılamaz");

                }

            }
            else
                MessageBox.Show("Teşhis ilaç aktarmak için ilaç seçmediniz");
        }

        private void DoktorTeshisCikar()
        {
            try
            {
                if (bsdoktortani.Current != null)
                {
                    long teshisId = (bsdoktortani.Current as DoktorTeshis).Teshis.Id;
                    DoktorTeshis aktarilacakteshis = doktorteshislistesi.Find(delegate(DoktorTeshis mteshis)
                    {
                        return mteshis.Teshis.Id == teshisId;
                    }
                    );

                    try
                    {
                        aktarilacakteshis.Delete();
                    }
                    catch (Exception ex)
                    {
                        
                         throw new Exception("Hata:"+ex.Message);
                    }

                    doktorteshislistesi.Remove(aktarilacakteshis);
                    DoktorTeshisBindGrids();
                }
                else
                    MessageBox.Show("Çıkarmak istediniz Teşhisi seçmediniz");
            }
            catch (Exception)
            {
                MessageBox.Show("Çıkarılacak teşhisi seçmelisiniz."); 
               
            }
          
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage.Name == "tptumu")
            {
                simpleButtonDoktordancikar.Enabled = false;
                simpleButtondoktoraAta.Enabled = true;
                simpleButtonDoktordancikar.Enabled = false;
            }
            else
            if (xtraTabControl1.SelectedTabPage.Name == "tpdoktor")
            {
                if (this.formState != myenum.EditMode.emIncele)
                    simpleButtonDoktordancikar.Enabled = true;
                simpleButtondoktoraAta.Enabled = false;
                simpleButtonDoktordancikar.Enabled = true;
            }

        }

        private enum TeshisAktarmaTipi
        {
            TeshisListesindenAktar=1,
            DoktorTeshisListesindenAktar=2

        }
    }
}