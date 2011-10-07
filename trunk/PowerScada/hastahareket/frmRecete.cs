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
using AHBS2010.Rapor;

namespace AHBS2010
{
    public partial class frmRecete : frmDialogBase
    {

        //TODO:Gridde direk rowu seçme aktif hale getirilmeliki çift tıklanabilsin. şuan kenarından yapılabiliyor.
        BindingSource bsfavoriilac = new BindingSource();
        BindingSource bshastailac = new BindingSource();
        BindingSource bsdoktorilac = new BindingSource();
        BindingSource bstumilac = new BindingSource();

        BindingSource bsrecetelac = new BindingSource();

        #region variables
        
        List<ilac> IlacListesi = new List<ilac>();
        List<Receteilac> receteilaclistesi = new List<Receteilac>();
        List<DoktorIlac> doktorilaclistesi = new List<DoktorIlac>();

        #endregion

        private Recete recete;

        public Recete ReceteEntity
        {
            get
            {
                if (recete == null)
                    recete = (Recete)CommandNew();

                return (Recete)formEntity;
            }
            set
            {
                recete = value;
            }
        }

        public frmRecete()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmRecete_Load);
            this.formState = mymodel.myenum.EditMode.emYeni;
            InitializeForm();
            
        }

        public frmRecete(long Id, mymodel.myenum.EditMode formstate)
        {
            InitializeComponent();
            this.formState = formstate;
          
            this.Load += new EventHandler(frmRecete_Load);
            InitializeForm(Id, formstate);
            InitDataControl();

            btnvazgecc.Text = "Kapat";
        }

        void frmRecete_Load(object sender, EventArgs e)
        {
            edtadet.Properties.MinValue = 0;
            edtadet.Properties.MaxValue = 100;
            #region eski satırlar
            //bstumilac.DataSource = mycommon.myUtil.OpenSqlIntoDataTable("select Id,Barkod,Adi from ilac where aktif=1 order by adi");
            //grd.DataSource = bstumilac;
            //grdv.Columns["Id"].Visible = false;

            //bshastailac.DataSource = mycommon.myUtil.OpenSqlIntoDataTable(
            //    "select i.Adi ilac,r.Adet,r.Dozaj,r.KullanimSekli,r.KullanimPeriyot " +
            //        "\nfrom Receteilac r" +
            //        "\njoin Recete rt on rt.Id=r.recete_Id" +
            //        "\njoin ilac i on i.Id=r.Ilac_Id" +
            //        "\nwhere r.Aktif=1 and rt.muayene_Id= " + muayeneId +
            //        "\norder by r.Id,i.Id");
            //grdrecete.DataSource = bshastailac;
            //grdvrecete.Columns["Id"].Visible = false;



            //bsrecetelac
            //    Condition[] con = new Condition[1];
            //con[0] = new Condition("Aktif", Operator.Equal, 1);

            //ilac[] ilaclar = Persistence.ReadList<ilac>(new string[] { "Id", "Barkod", "Adi" }, con, new string[] { "Adi" }, 0);
            //if (ilaclar != null && ilaclar.Length > 0)
            //    IlacListesi.AddRange(ilaclar);


            //bstumilac.DataSource = IlacListesi;
            //grd.DataSource = bstumilac;

            #endregion

            #region Ilaclar Yukleniyor

            ilac[] ilaclar = new ilac[Current.aktifilaclar.Values.Count];
            Current.aktifilaclar.Values.CopyTo(ilaclar, 0);
            if (ilaclar != null && ilaclar.Length > 0)
                IlacListesi.AddRange(ilaclar);


            IlaclarBind();

            #region GridStyle


            AsiGridStyle(grdv);
            #endregion

            #endregion 

            #region Doktorun Ilaclari yukleniyor

            DoktorIlac[] doktorilaclari = Persistence.ReadList<DoktorIlac>("Select * from DoktorIlac where Doktor_Id=@prm0 and aktif=1", new object[] { Current.AktifDoktorId });
            if (doktorilaclari != null && doktorilaclari.Length > 0)
                doktorilaclistesi.AddRange(doktorilaclari);

            foreach (DoktorIlac ilacentity in doktorilaclistesi)
            {
                ilacentity.Ilac = Current.GetIlac(ilacentity.Ilac.Id);
            }

            DoktorIlacBind();

            #region GridStyle
            AsiGridStyle(gridViewdoktorilac);
            #endregion

            #endregion

            #region ReceteIlaclari Yukleniyor

            ReceteIlaclariniGetir();

            ReceteIlacBind();
            ReceteAsiGridStyle(grdvrecete);
            #endregion

            #region eventler

                btnaktar.Click += new EventHandler(btnaktar_Click);
                btniptal.Click += new EventHandler(btniptal_Click);
                btntamam.Click += new EventHandler(btntamam_Click);
                simpleButtonDoktordancikar.Click += new EventHandler(simpleButtonDoktordancikar_Click);
                simpleButtondoktoraAta.Click += new EventHandler(simpleButtondoktoraAta_Click);
                grdilac.MouseDoubleClick += new MouseEventHandler(grdilac_MouseDoubleClick);
                grdrecete.MouseDoubleClick += new MouseEventHandler(grdrecete_MouseDoubleClick);
                griddoktorilac.MouseDoubleClick += new MouseEventHandler(griddoktorilac_MouseDoubleClick);
                
            #endregion
        }

        private void ReceteIlaclariniGetir()
        {
            receteilaclistesi.Clear();
            Receteilac[] receteilaclari = Persistence.ReadList<Receteilac>("Select *  from Receteilac Where   Receteilac.Recete_Id=@prm0", ReceteEntity.Id);
            if (receteilaclari != null && receteilaclari.Length > 0)
                receteilaclistesi.AddRange(receteilaclari);

            foreach (Receteilac receteentity in receteilaclistesi)
            {
                receteentity.Ilac = Current.GetIlac(receteentity.Ilac.Id);
            }
        }

        private void AsiGridStyle(DevExpress.XtraGrid.Views.Grid.GridView view)
        {   //<Column Name='AzamiDozaj' HeaderText='AzamiDozaj' Width='40' DisplayIndex='3'  />
        
            griddoktorilac.SetGridStyle(
                 @" <Style>
                        <Column Name='Adi' HeaderText='Adi' Width='130' DisplayIndex='1'  />                    
                        <Column Name='Barkod' HeaderText='Barkod' Width='47' DisplayIndex='2'  />
                       
                      </Style>");
            grdilac.SetGridStyle(
                 @" <Style>
                        <Column Name='Adi' HeaderText='Adi' Width='130' DisplayIndex='1'  />                    
                        <Column Name='Barkod' HeaderText='Barkod' Width='47' DisplayIndex='2'  />
                      
                      </Style>");
        }

        private void ReceteAsiGridStyle(DevExpress.XtraGrid.Views.Grid.GridView view)
        {
            //<Column Name='AzamiDozaj' HeaderText='Azami Dozaj' Width='30' DisplayIndex='3'  />
            //<Column Name='KullanimSekliAciklama' HeaderText='Açıklama' Width='65' DisplayIndex='5'  />
            //<Column Name='ilacDozAciklama' HeaderText='Doz Açıklama' Width='65' DisplayIndex='7'  />
            grdrecete.SetGridStyle(
                 @" <Style>
                        <Column Name='Adet' HeaderText='Adet' Width='35' DisplayIndex='0'  />
                        <Column Name='Adi' HeaderText='Adi' Width='110' DisplayIndex='1'  />                    
                        <Column Name='KullanimSekli' HeaderText='K.Şekli' Width='65' DisplayIndex='4'  />
                        
                        <Column Name='KullanimPeriyot' HeaderText='K.Periyot' Width='50' DisplayIndex='6'  />
                        
                        
                      </Style>");
        }

        #region Cesitli Eventler

        void griddoktorilac_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ReceteyeIlacAktar(IlacAktarmaTipi.DoktorIlacListesindenAktar);
        }

        void grdrecete_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RecetedenIlacCikar();
        }

        void grdilac_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ReceteyeIlacAktar(IlacAktarmaTipi.IlacListesindenAktar);
        }

        void simpleButtondoktoraAta_Click(object sender, EventArgs e)
        {
            DoktoraIlacAktar();
        }

        void simpleButtonDoktordancikar_Click(object sender, EventArgs e)
        {
            DoktorIlacCikar();
        }

        void btntamam_Click(object sender, EventArgs e)
        {

            updatedata();
            List<ActiveRecordBase> childlist;
            childlist = new List<SharpBullet.ActiveRecord.ActiveRecordBase>();
            foreach (Receteilac item in receteilaclistesi)
            {

                item.Hasta.Id = Current.AktifHastaId;
                item.Hasta = Current.AktifHasta;
               
                if (Current.AktifMuayeneId > 0)
                {
                    item.MuayeneId = Current.AktifMuayeneId;
                }
                
                if (Current.AktifRandevuId > 0)
                {
                    item.Randevu.Id = Current.AktifRandevuId;
                    item.Randevu = Current.AktifRandevu;
                }
                
                
                childlist.Add((ActiveRecordBase)item);
            }
            ReceteEntity.SetChilds(childlist);

            ///TODO:Reçete ilaçsız kaydedilemez
            bool recetesilindi = false;
            if (ReceteEntity.Id > 0)
            {
                ///İlaç yoksa reçete de silinsin.
                if (receteilaclistesi.Count == 0)
                {
                    Transaction.Instance.Join(
                                delegate()
                                {
                                    ReceteEntity.DeleteAllChilds();
                                    ReceteEntity.Delete();
                                });
                    recetesilindi = true;
                    btnvazgecc.Text = "Kapat";
                }
                else
                {
                    Transaction.Instance.Join(
                                  delegate()
                                  {


                                      ReceteEntity.Hasta.Id = Current.AktifHastaId;
                                      ReceteEntity.Hasta = Current.AktifHasta;
                                      ReceteEntity.Doktor.Id = Current.AktifHasta.Doktor.Id;
                                      if (Current.AktifDoktorId != ReceteEntity.Doktor.Id)
                                      {
                                          ReceteEntity.VekilDoktor.Id = Current.AktifDoktorId;
                                          ReceteEntity.VekilDoktor = Current.AktifDoktor;
                                      }
                                      if (Current.AktifMuayeneId > 0)
                                      {
                                          ReceteEntity.Muayene.Id = Current.AktifMuayeneId;
                                          ReceteEntity.Muayene = Current.AktifMuayene;
                                          //Muayene.UpdateMuayenedurumu(Current.AktifMuayeneId, myenum.MuayeneDurumu.MuayeneEdildi);

                                      }
                                     
                                      if (Current.AktifRandevuId > 0)
                                      {
                                            if (Convert.ToDateTime(ReceteEntity.EklemeTarihi.ToShortDateString())<Current.AktifRandevu.BasTarih)
                                                throw new Exception("İleri tarihli bir randevu işlem yapılamaz.");
                                             
                                            ReceteEntity.Randevu.Id = Current.AktifRandevuId;
                                            ReceteEntity.Randevu = Current.AktifRandevu;
                                            Takvim.UpdateTakvimDurumu(Current.AktifRandevuId, myenum.RandevuDurumu.Geldi);
                                      }
                                      

                                      ReceteEntity.Update();
                                  }
                          );
                }
            }
            else
            if (ReceteEntity.Id == 0)
            {
                //ilaç yoksa reçete kaydı yapamasın.
                if (receteilaclistesi.Count == 0)
                {
                    MessageBox.Show("İlaç girişi yapılmadan reçete kaydı yapamazsınız");
                    return;
                }

                Transaction.Instance.Join(
                              delegate()
                              {

                                  ReceteEntity.Hasta.Id = Current.AktifHastaId;
                                  ReceteEntity.Hasta = Current.AktifHasta;
                                  ReceteEntity.Doktor.Id = Current.AktifHasta.Doktor.Id;
                                  if (Current.AktifDoktorId != ReceteEntity.Doktor.Id)
                                  {
                                      ReceteEntity.VekilDoktor.Id = Current.AktifDoktorId;
                                      ReceteEntity.VekilDoktor = Current.AktifDoktor;
                                  }
                                  if (Current.AktifMuayeneId > 0)
                                  {
                                      ReceteEntity.Muayene.Id = Current.AktifMuayeneId;
                                      ReceteEntity.Muayene = Current.AktifMuayene;
                                      Muayene.UpdateMuayenedurumu(Current.AktifMuayeneId, myenum.MuayeneDurumu.MuayeneEdildi);

                                  }
                                 
                                if (Current.AktifRandevuId > 0)
                                {
                                    if (Convert.ToDateTime(ReceteEntity.EklemeTarihi.ToShortDateString())<Current.AktifRandevu.BasTarih)
                                        throw new Exception("İleri tarihli bir randevu işlem yapılamaz.");

                                    ReceteEntity.Randevu.Id = Current.AktifRandevuId;
                                    ReceteEntity.Randevu = Current.AktifRandevu;
                                    Takvim.UpdateTakvimDurumu(Current.AktifRandevuId, myenum.RandevuDurumu.Geldi);
                                }
                                ReceteEntity.Insert();
                                  
                              }
                      );
            }

            ReceteIlaclariniGetir();
            ReceteIlacBind();
            ReceteAsiGridStyle(grdvrecete);
            if (!recetesilindi)
            {
                if (MessageBox.Show("Reçete kaydı Yapıldı.\nReçete dökümü almak istermisiniz ?", "Reçete Döküm istermisiniz ?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                {
                    simpleButtonReceteDokum_Click(sender, e);
                    btnvazgecc.Text = "Kapat";
                }
            }
            else
            {
                MessageBox.Show("İlgili Reçete sistemden silindi.");
            }
           this.DialogResult=DialogResult.None;

            
        }

        #endregion

        #region Grid Binding islemleri

        private void IlaclarBind()
        {
            bstumilac.DataSource = IlacListesi;
            grdilac.DataSource = bstumilac;
            grdilac.RefreshDataSource();
        }

        private void ReceteIlacBind()
        {
            bsrecetelac.DataSource = receteilaclistesi;
            grdrecete.DataSource = bsrecetelac;
            grdrecete.RefreshDataSource();
        }

        private void DoktorIlacBind()
        {
            bsdoktorilac.DataSource = doktorilaclistesi;
            griddoktorilac.DataSource = bsdoktorilac;
            griddoktorilac.RefreshDataSource();
        }

        #endregion

        public override void formtamam()
        {
           
        }

        public override void showdata()
        {
            //TODO:bu bilgileri nerden alacağız ya da nerde girdireceğiz.
            //ReceteEntity = new Recete();
            //ReceteEntity.Aciklama;
            ReceteEntity.Muayene = Current.AktifMuayene;
            ReceteEntity.No = "";
            ReceteEntity.RaporKurumu = myenum.SosyalGuvenlikKurumTipi.BagKur;
            ReceteEntity.RaporNo = "";
            ReceteEntity.Tipi = myenum.ReceteTur.Normal;
            
        }

        public override void updatedata()
        {
            ReceteEntity.Muayene = Current.AktifMuayene;
            ReceteEntity.No = "";
            ReceteEntity.RaporKurumu = Current.AktifHasta.KurumTipi;
            ReceteEntity.RaporNo = "";
            ReceteEntity.Tipi = myenum.ReceteTur.Normal;
            ReceteEntity.Aktif = true;
        }

        protected override Entity CommandNew()
        {
            return new Recete();
        }

        protected override void CommandRead(long objId)
        {
            formEntity = SharpBullet.OAL.Persistence.Read<Recete>(objId);
        }

        void btniptal_Click(object sender, EventArgs e)
        {  
            RecetedenIlacCikar();
        }

        private void RecetedenIlacCikar()
        {

            


                if (bsrecetelac.Current != null)
                {
                    if (MessageBox.Show("Seçili ilacı listeden çıkarmak istediğinize eminmisiniz","Bilgi",MessageBoxButtons.OKCancel) ==System.Windows.Forms.DialogResult.OK)
                    {
                        long IlacId = (bsrecetelac.Current as Receteilac).Ilac.Id;
                        Receteilac aktarilacakilac = receteilaclistesi.Find(delegate(Receteilac receteilac)
                        {
                            return receteilac.Ilac.Id == IlacId;
                        }
                        );

                        receteilaclistesi.Remove(aktarilacakilac);
                        ReceteIlacBind();
                    }
                }
                else
                    MessageBox.Show("Reçeteden çıkarmak istediniz ilacı seçmediniz");
            
        }

        void btnaktar_Click(object sender, EventArgs e)
        {
             if (xtraTabControl1.SelectedTabPage.Name == "tptumu")
            {
                ReceteyeIlacAktar(IlacAktarmaTipi.IlacListesindenAktar);
            }
              else
                if (xtraTabControl1.SelectedTabPage.Name == "tpdoktor")
                {
                    ReceteyeIlacAktar(IlacAktarmaTipi.DoktorIlacListesindenAktar);
                }
          
        }

        private void ReceteyeIlacAktar(IlacAktarmaTipi aktarimtipi)
        {
            ilac aktarilacakilac = null;
            if (aktarimtipi == IlacAktarmaTipi.IlacListesindenAktar)
            {
                if (bstumilac.Current == null)
                {
                    MessageBox.Show(" İlaç seçmediniz !");
                    return;
                }
                long IlacId = (bstumilac.Current as ilac).Id;
                aktarilacakilac = IlacListesi.Find(delegate(ilac ilc)
                {
                    return ilc.Id == IlacId;
                }
                );
            }
            else
                if (aktarimtipi == IlacAktarmaTipi.DoktorIlacListesindenAktar)
                {
                    if (bsdoktorilac.Current == null)
                    {
                        MessageBox.Show(" İlaç seçmediniz !");
                        return;
                    }

                    long IlacId = (bsdoktorilac.Current as DoktorIlac).Ilac.Id;
                    DoktorIlac doktorilac = doktorilaclistesi.Find(delegate(DoktorIlac dilac)
                    {
                        return dilac.Ilac.Id == IlacId;
                    }
                    );

                    aktarilacakilac = doktorilac.Ilac;
                }

                if (aktarilacakilac.Id > 0)
                {

                    bool varmi = receteilaclistesi.Exists(delegate(Receteilac receteilacentity)
                    {
                        if (receteilacentity.Ilac.Id == aktarilacakilac.Id)
                        {
                            return true;
                        }
                        else
                            return false;

                    });

                    if (varmi)
                    {
                        MessageBox.Show("Bu İlaç listede mevcut ");
                        return;
                    }



                    Receteilac receteilac = new Receteilac();
                    receteilac.Adet = Convert.ToInt16(edtadet.Text);
                    receteilac.KullanimPeriyot = (myenum.ilacKullanimPeriyot)ucilacdozaj1.Deger;
                    receteilac.KullanimSekli = (myenum.ilacKullanimSekli)ucilacKullanimSekli1.Deger;
                    receteilac.Ilac.Id = aktarilacakilac.Id;
                    receteilac.Ilac = aktarilacakilac;
                    receteilac.Aktif = true;
                    receteilac.MuayeneId = Current.AktifMuayeneId;
                    receteilac.KullanimSekliAciklama = textBoxKullanimSekliAciklama.Text;
                    receteilac.ilacDozAciklama = textBoxDozAciklama.Text;
                    receteilac.Validate();
                    

                    receteilaclistesi.Add(receteilac);
                    ReceteIlacBind();


                    ucilacKullanimSekli1.Deger = 0;
                    ucilacdozaj1.Deger = 0;
                    edtadet.Value = 0;
                    textBoxDozAciklama.Clear();
                    textBoxKullanimSekliAciklama.Clear();

                }

        }

        private void DoktorIlacCikar()
        {
            try
            {
                if (bsdoktorilac.Current != null)
                {
                    long IlacId = (bsdoktorilac.Current as DoktorIlac).Ilac.Id;
                    DoktorIlac aktarilacakilac = doktorilaclistesi.Find(delegate(DoktorIlac doktorilac)
                    {
                        return doktorilac.Ilac.Id == IlacId;
                    }
                    );

                    try
                    {
                        aktarilacakilac.Delete();
                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Hata:" + ex.Message);
                    }

                    doktorilaclistesi.Remove(aktarilacakilac);
                    DoktorIlacBind();
                }
                else
                    MessageBox.Show("Listeden İlaç Seçmediniz");
            }
            catch (Exception)
            {
                MessageBox.Show("Çıkarılacak İlacı seçmelisiniz.");

            }

        }

        private void DoktoraIlacAktar()
        {
            if (bstumilac.Current != null)
            {
                long IlacId = (bstumilac.Current as ilac).Id;
                ilac aktarilacakilac = IlacListesi.Find(delegate(ilac ilacentity)
                {
                    return ilacentity.Id == IlacId;
                }
                );

                if (aktarilacakilac.Id > 0 )
                {
                    DoktorIlac doktorilac = new DoktorIlac();
                    doktorilac.Doktor.Id = Current.AktifDoktorId;
                    doktorilac.Doktor = Current.AktifDoktor;
                    doktorilac.Ilac.Id = aktarilacakilac.Id;
                    doktorilac.Ilac = aktarilacakilac;
                    doktorilac.Aktif = true;

                    bool varmi = doktorilaclistesi.Exists(delegate(DoktorIlac dilac)
                    {
                        if (dilac.Ilac.Id == aktarilacakilac.Id)
                        {
                            return true;
                        }
                        else
                            return false;

                    }
                    );

                    if (varmi)
                    {
                        MessageBox.Show("Bu ilac listede mevcut.");
                        return;
                    }
                    try
                    {
                        doktorilac.Insert();
                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Hata:" + ex.Message);
                    }

                    doktorilaclistesi.Add(doktorilac);

                    DoktorIlacBind();
                }
               

            }
            else
                MessageBox.Show("Doktor İlaç Listesine ilaç aktarmak için ilaç seçmediniz");
        }

        private enum IlacAktarmaTipi
        {
            IlacListesindenAktar = 1,
            DoktorIlacListesindenAktar = 2

        }

        private void simpleButtonReceteDokum_Click(object sender, EventArgs e)
        {
            ReportRecete receterpr = new ReportRecete();
             
            receterpr.DataSource = Transaction.Instance.ExecuteSql(@"SELECT
            Muayene.Id as MuayeneId
            ,Hasta.Adi+' '+Hasta.Adi as AdiSoyadi
            ,Hasta.TckNo
            ,Hasta.Cinsiyeti
            ,Hasta.KurumTipi
            ,Hasta.DogumTarihi
            ,GETDATE() as ReceteTarihi
            ,Muayene.ProtokolNo as ProtokolNo
            ,'Bilmiyoruz sorulacak' as TabibinKurumu
            ,(select Doktor.Diplomano+' '+Unvan+' '+Doktor.Adi+Doktor.Soyadi from Doktor where Doktor.Id=Hasta.Doktor_Id) as DoktorBilgileri ,"+
            ReceteEntity.Id+@" as receteId      
            
            FROM Hasta 
             inner join Muayene on Muayene.Hasta_Id=Hasta.Id  
            where
            Muayene.Id=" + Current.AktifMuayeneId + " and Hasta.Id=" + Current.AktifHastaId);

            receterpr.ShowPreview();

        }
    }
}