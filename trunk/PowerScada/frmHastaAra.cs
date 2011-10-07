using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
         
using mymodel;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using wsAh30;
using SharpBullet.OAL;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using AHBS2010.Rapor;
using DevExpress.XtraReports.Parameters;
using wsAh30.rHastaBilgi;
using wsAh30.rMvs;
using wsAh30.rLaboratuvar;
using AHBS2010.LocalLab;



namespace AHBS2010
{

    public partial class frmHastaAra : DevExpress.XtraEditors.XtraForm
    {
        #region decs
        [Browsable(false)]
        public Hasta AktifHasta
        {
            get
            {
                if (bshasta.Current != null)
                {
                    long id = Convert.ToInt64((bshasta.Current as DataRowView)["HastaNo"]);
                    return Persistence.Read<Hasta>(id);
                }
                else
                    return new Hasta();

            }
        }
        public Muayene AktifMuayene
        {
            get
            {
                Muayene yenimauyene = null;

                if (xtraTabControlMuayene.SelectedTabPage == xtraTabPageBugunkuMuayeneler)
                {

                    if (bsbugunkumuayene.Current != null)
                    {
                        yenimauyene = Persistence.Read<Muayene>(Convert.ToInt64((bsbugunkumuayene.Current as DataRowView)["MuayeneNo"]));
                    }
                    else
                        if (bsbugunkumuayene.Count > 0)
                        {
                            bsbugunkumuayene.MoveFirst();
                            yenimauyene = Persistence.Read<Muayene>(Convert.ToInt64((bsbugunkumuayene.Current as DataRowView)["MuayeneNo"]));
                        }
                        else
                            yenimauyene = null;


                }
                else
                {
                    if (bsgecmismuayene.Current != null)
                    {
                        yenimauyene = Persistence.Read<Muayene>(Convert.ToInt64((bsgecmismuayene.Current as DataRowView)["MuayeneNo"]));
                    }
                    else
                        if (bsgecmismuayene.Count > 0)
                        {
                            bsgecmismuayene.MoveFirst();
                            yenimauyene = Persistence.Read<Muayene>(Convert.ToInt64((bsgecmismuayene.Current as DataRowView)["MuayeneNo"]));
                        }
                        else
                            yenimauyene = null;
                }

                return yenimauyene;
            }
        }

        //Randevu
        public Takvim AktifRandevu
        {
            get
            {
                if (bshasta.Current != null)
                {
                    long id = 0;
                    try
                    {
                        id = Convert.ToInt64((bshasta.Current as DataRowView)["TakvimId"]);
                        return Persistence.Read<Takvim>(id);
                    }
                    catch (Exception)
                    {
                        return new Takvim();
                    }

                }
                else
                    return new Takvim();

            }
        }
        private frmMonitor monitor;
        public Color FormColor = new Color();
        //public BaseModelSampleEski BaseModelSample = new BaseModelSampleEski();
        public BindingSource bshasta = new BindingSource();
        public BindingSource bsbugunkumuayene = new BindingSource();
        public BindingSource bsgecmismuayene = new BindingSource();

        public BindingSource bsrecete = new BindingSource();
        public BindingSource bstani = new BindingSource();
        public BindingSource bstetkik = new BindingSource();
        public BindingSource bssevk = new BindingSource();
        public BindingSource bsasi = new BindingSource();
        public BindingSource bsizlem = new BindingSource();
        public BindingSource bshastalik = new BindingSource();
        public BindingSource bsanamnez = new BindingSource();
        public BindingSource bskadinizle = new BindingSource();
        public BindingSource bsgebeizle = new BindingSource();
        public BindingSource bsgebebaslangic = new BindingSource();
        public BindingSource bsgebesonuc = new BindingSource();
        public BindingSource bslohusaizle = new BindingSource();
        public BindingSource bsbebekcocukizle = new BindingSource();
        public BindingSource bsbebekbeslenizle = new BindingSource();
        public BindingSource bsobeziteizlem = new BindingSource();
        public BindingSource bsdogum = new BindingSource();
        public BindingSource bsvefat = new BindingSource();
        public BindingSource bshizmet = new BindingSource();
        public BindingSource bsraporlar = new BindingSource();

        public string etiket = "";
        #endregion decs

        #region form sets

        public frmHastaAra()
        {
            InitializeComponent();
            DateEditBasTarih.DateTime = System.DateTime.Today;
            dateEditBitTar.DateTime = System.DateTime.Today;
            this.dateEditBaslangicTarihiProtokol.EditValueChanged -= new System.EventHandler(this.dateEditBaslangicTarihiProtokol_EditValueChanged);
            this.dateEditBitisTarihiProtokol.EditValueChanged -= new System.EventHandler(this.dateEditBitisTarihiProtokol_EditValueChanged);
            dateEditBaslangicTarihiProtokol.DateTime = System.DateTime.Today;
            dateEditBitisTarihiProtokol.DateTime = System.DateTime.Today;
            this.dateEditBaslangicTarihiProtokol.EditValueChanged += new System.EventHandler(this.dateEditBaslangicTarihiProtokol_EditValueChanged);
            this.dateEditBitisTarihiProtokol.EditValueChanged += new System.EventHandler(this.dateEditBitisTarihiProtokol_EditValueChanged);
            DateEditBasTarih.EditValueChanged += new EventHandler(DateEditBasTarih_EditValueChanged);
            dateEditBitTar.EditValueChanged += new EventHandler(DateEditBasTarih_EditValueChanged);
            Current.AktifMuayene = null;
            Current.AktifHasta = null;
            Current.AktifRandevu = null;

            myenum.DoktorOdasıGorunumu DoktorOdasiGorunumu = (myenum.DoktorOdasıGorunumu)Current.PrgAyar.DoktorPanelAyar;

            bool izlemyetkisi=Current.HasRight(Current.User.GorevTuru, myenum.Hak.IzlemAcabilmeHakki);
            bool muayeneyetkisi= Current.HasRight(Current.User.GorevTuru, myenum.Hak.MuayeneAcabilmeHakki);
            bool muayeneizlemgorebilir = (izlemyetkisi && muayeneyetkisi);

            radioButtonHepsi.Visible = muayeneizlemgorebilir;
            radioButtonIzlemGrubu.Visible = izlemyetkisi;
            radioButtonMuayeneGrubu.Visible = muayeneyetkisi;

            if (DoktorOdasiGorunumu == myenum.DoktorOdasıGorunumu.HerIkisi && muayeneizlemgorebilir)
            {
                radioButtonHepsi.Checked = true;
            }
            else
                if (DoktorOdasiGorunumu == myenum.DoktorOdasıGorunumu.Izlem && izlemyetkisi)
                {
                    radioButtonIzlemGrubu.Checked = true;
                }
                else
                    if (DoktorOdasiGorunumu == myenum.DoktorOdasıGorunumu.Muayene && muayeneyetkisi)
                    {
                        radioButtonMuayeneGrubu.Checked = true;
                    }



            if (Current.PrgAyar.GridGorunumuStandartmi)
            {

                #region Hasta gridi

                this.grdhasta.MainView = this.gridViewHasta;
                this.grdhasta.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
                this.gridViewHasta});



                grdhasta.SetGridStyle(
                    @" <Style>
                        <Column Name='KayitDurumu' HeaderText='K.Durumu' Width='65' DisplayIndex='1'  />                    
                        <Column Name='TckNo' HeaderText='Tc K.No' Width='65' DisplayIndex='2'  />
                        <Column Name='PasaportNo' HeaderText='Pas.No' Width='65' DisplayIndex='3'  />
                        <Column Name='AdiSoyadi' HeaderText='Adı Soyadı' Width='150' DisplayIndex='4' />
                        <Column Name='Cinsiyeti' HeaderText='Cinsiyeti' Width='50' DisplayIndex='5' />
                   
                     </Style>");
                gridViewHasta.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(gridViewHasta_RowStyle);
                gridViewHasta.ShowingEditor += new CancelEventHandler(gridViewHasta_ShowingEditor);
                gridViewHasta.OptionsBehavior.Editable = false;

                #endregion Hasta gridi

                #region Bugunku muayene gridi

                this.grdBugunkumuayene.MainView = this.gridViewBugunkuMuayene;
                this.grdBugunkumuayene.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
                this.gridViewBugunkuMuayene});
                gridViewBugunkuMuayene.OptionsBehavior.Editable = false;

                grdBugunkumuayene.SetGridStyle(
                          @" <Style>
                        <Column Name='randevusirano' HeaderText='Rndv.Sıra No' Width='65' DisplayIndex='1'  /> 
                        <Column Name='SiraNo' HeaderText='Sıra No' Width='65' DisplayIndex='2'  />                    
                        <Column Name='ProtokolNo' HeaderText='Protokol No' Width='100' DisplayIndex='3'  />
                        <Column Name='MuayeneDurumu' HeaderText='Durumu' Width='120' DisplayIndex='4'  />
                        <Column Name='MuayeneKapalimi' HeaderText='Kapali' Width='50'  Type='CheckBox' DisplayIndex='5' />
                     </Style>");



                #endregion

                #region gecmis muayene gridi
                this.gridGecmisMuayene.MainView = this.gridViewGecisMuayene;
                this.gridGecmisMuayene.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
                this.gridViewGecisMuayene});
                gridViewGecisMuayene.OptionsBehavior.Editable = false;

                gridGecmisMuayene.SetGridStyle(
                    @" <Style>
                        <Column Name='MuayeneTarihi' HeaderText='Muayene Tarihi' Width='65' DisplayIndex='1'  />  
                        <Column Name='SiraNo' HeaderText='Sıra' Width='65' DisplayIndex='2'  />  
                        <Column Name='ProtokolNo' HeaderText='Protokol No' Width='100' DisplayIndex='3'  />
                        <Column Name='MuayeneDurumu' HeaderText='Durumu' Width='120' DisplayIndex='4'  />
                        <Column Name='MuayeneKapalimi' HeaderText='Kapali' Width='50'  Type='CheckBox' DisplayIndex='5' />
                     </Style>");


                #endregion

            }
            else
            {

                #region HastaGrid

                this.grdhasta.MainView = this.cardViewhasta;
                this.grdhasta.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
                this.cardViewhasta});



                grdhasta.SetCardGridStyle(
                      @" <Style>
                        <Column Name='KayitDurumu' HeaderText='K.Durumu' Width='65' DisplayIndex='1'  />                    
                        <Column Name='TckNo' HeaderText='Tc Kimlik No' Width='65' DisplayIndex='2'  />
                        <Column Name='PasaportNo' HeaderText='Pas. No' Width='65' DisplayIndex='3'  />
                        <Column Name='AdiSoyadi' HeaderText='Adı Soyadı' Width='150' DisplayIndex='4' />
                        <Column Name='Cinsiyeti' HeaderText='Cinsiyeti' Width='50' DisplayIndex='5' />
                   
                     </Style>");
                gridViewHasta.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(gridViewHasta_RowStyle);
                gridViewHasta.ShowingEditor += new CancelEventHandler(gridViewHasta_ShowingEditor);
                gridViewHasta.OptionsBehavior.Editable = false;

                #endregion


                #region bugunkumuayene

                this.grdBugunkumuayene.MainView = this.cardViewmuayene;
                this.grdBugunkumuayene.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
                this.cardViewmuayene});
                //Utility.SetGridStyle(((DevExpress.XtraGrid.Views.Card.CardView)grdBugunkumuayene.Views[0]));
                cardViewmuayene.OptionsBehavior.Editable = false;
                grdBugunkumuayene.SetCardGridStyle(
                                @" 
                      <Style>
                        <Column Name='randevusirano' HeaderText='Rndv.Sıra No' Width='65' DisplayIndex='1'  /> 
                        <Column Name='SiraNo' HeaderText='Sıra' Width='65' DisplayIndex='2'  />                    
                        <Column Name='ProtokolNo' HeaderText='Protokol No' Width='100' DisplayIndex='3'  />
                        <Column Name='MuayeneDurumu' HeaderText='Durumu' Width='120' DisplayIndex='4'  />
                        <Column Name='MuayeneKapalimi' HeaderText='Kapali' Width='50'  Type='CheckBox' DisplayIndex='5' />
                      </Style>");

                #endregion

                #region gecmiskumuayene

                this.gridGecmisMuayene.MainView = this.cardViewmuayene;
                this.gridGecmisMuayene.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
                this.cardViewmuayene});
                //Utility.SetGridStyle(((DevExpress.XtraGrid.Views.Card.CardView)grdBugunkumuayene.Views[0]));
                cardViewmuayene.OptionsBehavior.Editable = false;
                gridGecmisMuayene.SetCardGridStyle(
                                @" <Style>
                        <Column Name='MuayeneTarihi' HeaderText='Muayene Tarihi' Width='65' DisplayIndex='1'  />  
                        <Column Name='SiraNo' HeaderText='Sıra' Width='65' DisplayIndex='2'  />                    
                        <Column Name='ProtokolNo' HeaderText='Protokol No' Width='100' DisplayIndex='3'  />
                        <Column Name='MuayeneDurumu' HeaderText='Durumu' Width='120' DisplayIndex='4'  />
                        <Column Name='MuayeneKapalimi' HeaderText='Kapali' Width='50'  Type='CheckBox' DisplayIndex='5' />
                      </Style>");

                #endregion

            }

            if (!Current.PrgAyar.AramaYontemiEntermi)
            {
                edtara.TextChanged += new EventHandler(edtara_TextChanged);
                //textEditHastaBul.TextChanged += new EventHandler(textEditHastaBul_TextChanged);
                superTextBoxAdi.textBox.TextChanged += new EventHandler(textEditHastaBul_TextChanged);
                superTextBoxSoyadi.textBox.TextChanged += new EventHandler(textEditHastaBul_TextChanged);
                superTextBoxTckimlikno.textBox.TextChanged += new EventHandler(textEditHastaBul_TextChanged);
                //btngetir.Enabled = fals
            }



        }

        void DateEditBasTarih_EditValueChanged(object sender, EventArgs e)
        {
            simpleButtonMuayeneKapat.Text = DateEditBasTarih.DateTime.ToString("dd.MM.yyyy") + "/" + dateEditBitTar.DateTime.ToString("dd.MM.yyyy") + " arası Muayeneleri Kapat";
        }

        
        void textEditHastaBul_TextChanged(object sender, EventArgs e)
        {
            //if (textEditHastaBul.Text.Trim().Length > 2)
            if (superTextBoxAdi.textBox.Text.Trim().Length > 2 || superTextBoxSoyadi.textBox.Text.Trim().Length > 2 || superTextBoxTckimlikno.textBox.Text.Trim().Length > 2)
            {
                string kontrolAdi="";
                if (sender is TextBox)
                {
                    kontrolAdi = (sender as TextBox).Parent.Name;

                }
                frmHastaBul frm = new frmHastaBul(superTextBoxTckimlikno.textBox.Text, superTextBoxAdi.textBox.Text, superTextBoxSoyadi.textBox.Text, kontrolAdi);
                superTextBoxAdi.textBox.TextChanged -= new EventHandler(textEditHastaBul_TextChanged);
                superTextBoxSoyadi.textBox.TextChanged -= new EventHandler(textEditHastaBul_TextChanged);
                superTextBoxTckimlikno.textBox.TextChanged -= new EventHandler(textEditHastaBul_TextChanged);
                //textEditHastaBul.TextChanged -= new EventHandler(textEditHastaBul_TextChanged);
                //textEditHastaBul.Text = "";
                superTextBoxAdi.textBox.Text = "";
                superTextBoxSoyadi.textBox.Text = "";
                superTextBoxTckimlikno.textBox.Text = "";
                //textEditHastaBul.TextChanged += new EventHandler(textEditHastaBul_TextChanged);
                superTextBoxAdi.textBox.TextChanged += new EventHandler(textEditHastaBul_TextChanged);
                superTextBoxSoyadi.textBox.TextChanged += new EventHandler(textEditHastaBul_TextChanged);
                superTextBoxTckimlikno.textBox.TextChanged += new EventHandler(textEditHastaBul_TextChanged);
                frm.ShowDialog();
                Current.AktifHasta = null;
                HastaGetir();
            }
        }

        void edtara_TextChanged(object sender, EventArgs e)
        {
            if (edtara.Text.Trim().Length > 2)
            {
                btngetir_Click(sender, e);
            }
        }

        private void frmHastaAra_Load(object sender, EventArgs e)
        {
            //groupControldetaylar.Height = (Screen.PrimaryScreen.WorkingArea.Height / 2) - 25;
            //panelControldetaylar.Width = (Screen.PrimaryScreen.WorkingArea.Width / 3) - 50;
            //groupControlizlemler.Width = Screen.PrimaryScreen.WorkingArea.Width - (panelControldetaylar.Width + edtara.Width + 118);
            //groupControlizlemler.Dock = DockStyle.Fill;
            //panelControldetaylar.Dock = DockStyle.Fill;

            panelSag.Width = ((Screen.PrimaryScreen.WorkingArea.Width / 3) * 2) - 90;
            //groupControlizlemler.Width = ((Screen.PrimaryScreen.WorkingArea.Width / 3) * 2) - 50;
            panel2.Width = (Screen.PrimaryScreen.WorkingArea.Width / 3) + edtara.Width;


            bshasta.CurrentItemChanged += new EventHandler(bshasta_CurrentItemChanged);
            bsbugunkumuayene.CurrentItemChanged += new EventHandler(bsmuayene_CurrentItemChanged);
            bsgecmismuayene.CurrentItemChanged += new EventHandler(bsgecmismuayene_CurrentItemChanged);
            btngetir.Click += new EventHandler(btngetir_Click);
            edtara.KeyPress += new KeyPressEventHandler(edtara_KeyPress);
            edtkayitdurumtum.CheckedChanged += new EventHandler(edtkayitdurumtum_CheckedChanged);
            edtmuayenedurumutumu.CheckedChanged += new EventHandler(edtmuayenedurumutumu_CheckedChanged);


            xtraTabControldetay.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(xtraTabControldetay_SelectedPageChanged);
            xtraTabControlgebe.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(xtraTabControlgebe_SelectedPageChanged);
            xtraTabControlizlemler.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(xtraTabControlizlemler_SelectedPageChanged);

            gridhizmet.Enter += new EventHandler(gridhizmet_Enter);
            grdBugunkumuayene.Enter += new EventHandler(grdmuayene_Enter);
            gridGecmisMuayene.Enter += new EventHandler(gridGecmisMuayene_Enter);
            grdrecete.Enter += new EventHandler(grdrecete_Enter);
            grdtani.Enter += new EventHandler(grdtani_Enter);
            grdtetkik.Enter += new EventHandler(grdtetkik_Enter);
            grdsevk.Enter += new EventHandler(grdsevk_Enter);
            grdasi.Enter += new EventHandler(grdasi_Enter);
            grdhastalik.Enter += new EventHandler(grdhastalik_Enter);
            grdhasta.Enter += new EventHandler(grdhasta_Enter);
            grdanamnez.Enter += new EventHandler(grdanemnez_Enter);
            grdkadinizlem.Enter += new EventHandler(grdkadinizlem_Enter);
            grdbebekbeslenmeizlem.Enter += new EventHandler(grdbebekbeslenmeizlem_Enter);
            grdbebekcocukizlem.Enter += new EventHandler(grdbebekcocukizlem_Enter);
            grdlohusaizlem.Enter += new EventHandler(grdlohusaizlem_Enter);
            grdgebeizlem.Enter += new EventHandler(grdgebeizlem_Enter);
            grdobezizlem.Enter += new EventHandler(grdobezizlem_Enter);
            grdgebebaslangic.Enter += new EventHandler(grdgebebaslangic_Enter);
            grdgebesonuc.Enter += new EventHandler(grdgebesonuc_Enter);
            grddogum.Enter += new EventHandler(grddogum_Enter);
            grdvefat.Enter += new EventHandler(grdvefat_Enter);
            gridRaporlar.Enter += new EventHandler(gridRaporlar_Enter);
            btnsola.Click += new EventHandler(btnsola_Click);
            btnsoldansaga.Click += new EventHandler(btnsoldansaga_Click);
            btnsaga.Click += new EventHandler(btnsaga_Click);
            btnsagdansola.Click += new EventHandler(btnsagdansola_Click);
            btnal.Click += new EventHandler(btnal_Click);
            #region doubleclick evenleri

            xtraTabControldetay.DoubleClick += new EventHandler(xtraTabControldetay_DoubleClick);
            gridhizmet.MouseDoubleClick += new MouseEventHandler(gridhizmet_MouseDoubleClick);
            grdBugunkumuayene.MouseDoubleClick += new MouseEventHandler(gridhizmet_MouseDoubleClick);
            gridGecmisMuayene.MouseDoubleClick += new MouseEventHandler(gridhizmet_MouseDoubleClick);
            grdrecete.MouseDoubleClick += new MouseEventHandler(gridhizmet_MouseDoubleClick);
            grdtani.MouseDoubleClick += new MouseEventHandler(gridhizmet_MouseDoubleClick);
            grdtetkik.MouseDoubleClick += new MouseEventHandler(gridhizmet_MouseDoubleClick);
            grdsevk.MouseDoubleClick += new MouseEventHandler(gridhizmet_MouseDoubleClick);
            grdasi.MouseDoubleClick += new MouseEventHandler(gridhizmet_MouseDoubleClick);
            grdhastalik.MouseDoubleClick += new MouseEventHandler(gridhizmet_MouseDoubleClick);
            grdhasta.MouseDoubleClick += new MouseEventHandler(grdhasta_MouseDoubleClick);
            grdanamnez.MouseDoubleClick += new MouseEventHandler(gridhizmet_MouseDoubleClick);
            grdkadinizlem.MouseDoubleClick += new MouseEventHandler(gridhizmet_MouseDoubleClick);
            grdbebekbeslenmeizlem.MouseDoubleClick += new MouseEventHandler(gridhizmet_MouseDoubleClick);
            grdbebekcocukizlem.MouseDoubleClick += new MouseEventHandler(gridhizmet_MouseDoubleClick);
            grdlohusaizlem.MouseDoubleClick += new MouseEventHandler(gridhizmet_MouseDoubleClick);
            grdgebeizlem.MouseDoubleClick += new MouseEventHandler(gridhizmet_MouseDoubleClick);
            grdobezizlem.MouseDoubleClick += new MouseEventHandler(gridhizmet_MouseDoubleClick);
            grdgebebaslangic.MouseDoubleClick += new MouseEventHandler(gridhizmet_MouseDoubleClick);
            grdgebesonuc.MouseDoubleClick += new MouseEventHandler(gridhizmet_MouseDoubleClick);
            grddogum.MouseDoubleClick += new MouseEventHandler(gridhizmet_MouseDoubleClick);
            grdvefat.MouseDoubleClick += new MouseEventHandler(gridhizmet_MouseDoubleClick);
            gridRaporlar.MouseDoubleClick += new MouseEventHandler(gridhizmet_MouseDoubleClick);
            #endregion



            btnYeni.Click += new EventHandler(btnYeni_Click);
            btnDuzenle.Click += new EventHandler(btnDuzenle_Click);
            btnIptal.Click += new EventHandler(btnIptal_Click);
            btnincele.Click += new EventHandler(btnincele_Click);
            btnYenile.Click += new EventHandler(btnYenile_Click);
            etiket = "Hasta";
            setbuttons(bshasta);

        }

        

        void xtraTabControldetay_DoubleClick(object sender, EventArgs e)
        {
            if (xtraTabControldetay.SelectedTabPage == tphastaliklar)
            {
                etiket = "Hastalık";
                setbuttons(bshastalik);
            }
            else
            {
                if (xtraTabControldetay.SelectedTabPage == tpasilar)
                {
                    etiket = "Aşı";
                    setbuttons(bsasi);
                }
                else
                    if (xtraTabControldetay.SelectedTabPage == tpanemnez)
                    {
                        etiket = "Anamnez";
                        setbuttons(bsanamnez);
                    }
                    else
                        if (xtraTabControldetay.SelectedTabPage == tpreceteler)
                        {
                            etiket = "Reçete";
                            setbuttons(bsrecete);
                        }
                        else
                            if (xtraTabControldetay.SelectedTabPage == tpsevkler)
                            {
                                etiket = "Sevk";
                                setbuttons(bssevk);
                            }
                            else
                                if (xtraTabControldetay.SelectedTabPage == tptanilar)
                                {
                                    etiket = "Tanı";
                                    setbuttons(bstani);
                                }
                                else
                                    if (xtraTabControldetay.SelectedTabPage == tptetkikler)
                                    {
                                        etiket = "Tetkik";
                                        setbuttons(bstetkik);
                                    }
                                    else
                                        if (xtraTabControldetay.SelectedTabPage == tphizmetler)
                                        {
                                            etiket = "Hizmet";
                                            setbuttons(bshizmet);
                                        }
                                        else
                                            if (xtraTabControldetay.SelectedTabPage == tpraporlar)
                                            {
                                                etiket = "Rapor";
                                                setbuttons(bsraporlar);
                                            }
            }
                btnYeni_Click(btnYeni,null);
        }

        void gridRaporlar_Enter(object sender, EventArgs e)
        {

            etiket = "Rapor";
            setbuttons(bsraporlar);

        }

        #region doubleclick event methodları

        void grdhasta_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //etiket = "Muayene";
            //yeni();
        }
        void gridhizmet_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (ActiveControl is DevExpress.XtraGrid.GridControl)
            {
                long Id = 0;
                GridView view = ((GridView)((DevExpress.XtraGrid.GridControl)ActiveControl).DefaultView);
                //if (view.FocusedRowHandle < 0) return;
                //long id = (int)view.GetRowCellValue(view.FocusedRowHandle, "Id");
                object rowHandle = view.GetRow(view.FocusedRowHandle);
                if (rowHandle != null)
                {
                    duzenle();
                }
                //if (Id>0)
                //    duzenle();


            }

        }

        #endregion
        void gridGecmisMuayene_Enter(object sender, EventArgs e)
        {
            etiket = "Muayene";
            setbuttons(bsgecmismuayene);
        }

        void bsgecmismuayene_CurrentItemChanged(object sender, EventArgs e)
        {
            Current.AktifMuayene = this.AktifMuayene;
            getrecete();
            gettani();
            gettetkik();
            getsevk();
            getasi();
            getanamnez();
            getkadinizlem();
            getgebeizle();
            getgebebaslangic();
            getgebesonuc();
            getlohusaizle();
            getbebekcocukizle();
            getbebekbeslenizle();
            getobeziteizlem();
            getdogum();
            gethizmet();
            gethastalik();
            getraporlar();
        }

        void btnYenile_Click(object sender, EventArgs e)
        {
            if (ActiveControl != null)
            {
                if (ActiveControl is DevExpress.XtraGrid.GridControl)
                {
                    Yenile(((DevExpress.XtraGrid.GridControl)ActiveControl).Tag.ToString());
                }
            }

        }


        void btnincele_Click(object sender, EventArgs e)
        {
            Incele();
        }

        void btnIptal_Click(object sender, EventArgs e)
        {
           
            if ((ActiveControl is DevExpress.XtraGrid.GridControl))
            {

                string Entityname = string.Empty;

                DevExpress.XtraGrid.GridControl grid = (DevExpress.XtraGrid.GridControl)ActiveControl;

                if (grid.Tag != null)
                    Entityname = grid.Tag.ToString();
                else
                {
                    MessageBox.Show("Gridin Tagını dolduralım..");
                }

                if (Entityname == "Muayene")
                {
                    if ((((grid.DataSource as BindingSource).Current as DataRowView)["MuayeneDurumu"]).ToString() != "Bekliyor")
                    {
                        MessageBox.Show("-Bekliyor- durumu dışındaki muyaneler silinemez ?");
                        return;
                    }

                    if ((((grid.DataSource as BindingSource).Current as DataRowView)["MuayeneKapalimi"]).ToString() == "True")
                    {
                        MessageBox.Show("Kapatılmış muyaneler silinemez ?");
                        return;
                    }
                }

                if ((((grid.DataSource as BindingSource).Current as DataRowView)["TransferDurumu"]).ToString() == "10"||
                    (((grid.DataSource as BindingSource).Current as DataRowView)["TransferDurumu"]).ToString() == "Gonderildi")
                {
                    MessageBox.Show("Bakanlığa Transferi başarıyla yapılmış bilgi silinemez. ?");
                    return;
                }

                long PasifEdilecekId = Convert.ToInt64((((grid.DataSource as BindingSource).Current as DataRowView)["Id"]));

                if (MessageBox.Show("Seçili Kaydı İptal Etmek İstediğinizden Eminmisiniz", "Uyarı", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        if (Entityname == "Rapor")
                            Entityname = "SaglikIstirahat";
                        int updatesayisi = Transaction.Instance.ExecuteNonQuery("Update " + Entityname + " set Aktif=@prm1 Where Id=@prm0", new object[] { PasifEdilecekId, false });

                        if (updatesayisi > 0)
                        {
                            MessageBox.Show("Seçtiğiniz kayıt iptal edildi.");
                            if (Entityname == "Muayene")
                            {
                                Transaction.Instance.ExecuteNonQuery("Update takvim set RandevuDurumu='İptalEdildi' Where Id=@prm0 and hasta_ID=@prm1", 
                                    new object[] { AktifRandevu.Id, AktifHasta.Id });
                                bshasta.Remove(bshasta.Current);
                            }
                            if (Entityname == "SaglikIstirahat")
                                Entityname = "Rapor";
                            Yenile(Entityname);
                        }
                        else
                            MessageBox.Show("Seçtiğiniz kayıt  iptal edilemedi");
                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Hata:" + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("İptal etmek için Bir kayıt seçmediniz");
            }
        }


        void btnsagdansola_Click(object sender, EventArgs e)
        {
            //if (groupControlizlemler.Width < Screen.PrimaryScreen.WorkingArea.Width - (panelControldetaylar.Width + edtara.Width + 118))
            //{
            //    groupControlizlemler.Width = Screen.PrimaryScreen.WorkingArea.Width - (panelControldetaylar.Width + edtara.Width + 118);
            //    btnsagdansola.Visible = false;
            //    btnsaga.Visible = true;
            //}
            if (radioButtonIzlemGrubu.Checked)
            {
                if (panelSag.Width < (Screen.PrimaryScreen.WorkingArea.Width / 3))
                {
                    //groupControlizlemler.Width = ((Screen.PrimaryScreen.WorkingArea.Width / 3) * 2) - 50;
                    panelSag.Width = ((Screen.PrimaryScreen.WorkingArea.Width / 3) * 2) - 50;
                    btnsagdansola.Visible = false;
                    btnsaga.Visible = true;
                }
            }
            else
            {
                if (panelControldetaylar.Width < Screen.PrimaryScreen.WorkingArea.Width / 3)
                {
                    //panelControldetaylar.Width = ((Screen.PrimaryScreen.WorkingArea.Width / 3) * 2) - 50; 
                    panelSag.Width = ((Screen.PrimaryScreen.WorkingArea.Width / 3) * 2) - 50;
                    btnsagdansola.Visible = false;
                    btnsaga.Visible = true;
                }
            }

        }

        void btnsaga_Click(object sender, EventArgs e)
        {

            if (radioButtonIzlemGrubu.Checked)
            {
                if (panelSag.Width >= 1)
                {
                    //groupControlizlemler.Dock = DockStyle.Left;
                    //groupControlizlemler.Width = 1;
                    panelSag.Width = 1;
                    btnsagdansola.Visible = true;
                    btnsaga.Visible = false;
                }
            }
            else
            {
                if (panelSag.Width >= 1)
                {
                    //panelControldetaylar.Dock = DockStyle.Left;
                    //panelControldetaylar.Width = 1;
                    panelSag.Width = 1;
                    btnsagdansola.Visible = true;
                    btnsaga.Visible = false;
                }
            }
        }

        void btnsoldansaga_Click(object sender, EventArgs e)
        {
            //if (panelControldetaylar.Width < Screen.PrimaryScreen.WorkingArea.Width / 3)
            //{
            //    panelControldetaylar.Width = (Screen.PrimaryScreen.WorkingArea.Width / 3)*2;
            //    btnsoldansaga.Visible = false;
            //    btnsola.Visible = true;
            //}
            //if (groupControlizlemler.Width >= Screen.PrimaryScreen.WorkingArea.Width - (panelControldetaylar.Width + edtara.Width + 118))
            //{
            //    groupControlizlemler.Width = Screen.PrimaryScreen.WorkingArea.Width - (panelControldetaylar.Width + edtara.Width + 118);
            //    btnsagdansola.Visible = false;
            //    btnsaga.Visible = true;
            //}


        }

        void btnsola_Click(object sender, EventArgs e)
        {
            //if (panel2.Width >= 1)
            //{
            //    panel2.Width = 1;

            //    btnsoldansaga.Visible = true;
            //    btnsola.Visible = false;
            //}
        }

        public void sethastaadi()
        {
            if (bshasta == null || bshasta.Count == 0)
            {
                groupControlmuayene.Text = "Muayeneler";
                groupControlizlemler.Text = "İzlemler";
                groupControldetaylar.Text = "Detaylar";
                tbhastalar.Text = "Hastalar";

            }
            else
            {
                groupControlmuayene.Text = AktifHasta.ToString() + " ile ilgili Muayeneler";
                groupControlizlemler.Text = AktifHasta.ToString() + " ile ilgili İzlem ve Bildirimler";
                groupControldetaylar.Text = AktifHasta.ToString() + " Seçili Muayenesine Ait";
                tbhastalar.Text = "Hasta : " + AktifHasta.ToString();
                //if (AktifHasta.Cinsiyeti == myenum.Cinsiyet.Erkek && AktifHasta.Yasi > 12)
                //    FormColor = Color.LightBlue;
                //else
                //    if (AktifHasta.Yasi < 12 && AktifHasta.Yasi > 3)
                //        FormColor = Color.LightGreen;
                //    else
                //        if (AktifHasta.Yasi <= 3)
                //            FormColor = Color.LightPink;
                //        else
                //            if (AktifHasta.Cinsiyeti == myenum.Cinsiyet.KADIN && AktifHasta.Yasi > 12)
                //                FormColor = Color.LightGoldenrodYellow;
                if (AktifHasta.Cinsiyeti == myenum.Cinsiyet.Erkek)
                    FormColor = Color.LightBlue;
                else
                    if (AktifHasta.Cinsiyeti == myenum.Cinsiyet.Kadın)
                        FormColor = Color.LightPink;


                pnlhastakayitdurum.BackColor = FormColor;
                panelhastagetir.BackColor = FormColor;
                dbnav.BackColor = FormColor;
                pnltur.BackColor = FormColor;
                panelControlhastabaslik.BackColor = FormColor;
                pnlmuayenedurum.BackColor = FormColor;
                panelControl3.BackColor = FormColor;
               

                if (Current.AktifHastaId > 0)
                {
                    labelHastaAdiSoyadiDegeri.Text = Current.AktifHasta.Adi + ' ' + Current.AktifHasta.Soyadi;
                    labelCinsiyetDegeri.Text = Current.AktifHasta.Cinsiyeti.ToString();
                    labelYasDegeri.Text = Current.AktifHasta.YasAy().ToString();
                    labelTckimlikNoDegeri.Text = Current.AktifHasta.TckNo.ToString();
                    labelBabaAdiDegeri.Text = Current.AktifHasta.NfBabaAd;
                    labelAnneAdiDegeri.Text = Current.AktifHasta.NfAnaAd;
                    labelDogumTarihiDegeri.Text = Current.AktifHasta.NfDogumTarih != null ? Current.AktifHasta.NfDogumTarih.ToString() : ""; ;
                    object o = Transaction.Instance.ExecuteScalar(@"DECLARE @result varchar(max)
                    select
                    @result = coalesce(@result + '','') +
                    (
                    CASe when h.Kronikmi=1 then 'Kronik ' else '' end +', '+  
                    CASe when h.Alerjikmi=1 then 'Alerjik ' else '' end + ' '+t.Adi)



                                        from MuayeneTeshis h join teshis t on t.Id=h.teshis_Id join muayene m on m.Id=h.muayene_Id
                                        where h.Aktif=1 and m.hasta_Id=@prm0  and (h.Kronikmi=1 or h.Alerjikmi=1)
              
                     
                    select @result ", new object[] { Current.AktifHastaId });


                    textBoxHastaliklar.Text ="HASTALIKLAR : "+ o.ToString();

                    if (Current.AktifHasta.Cinsiyeti == myenum.Cinsiyet.Kadın)
                    {

                        xtraTabControlgebe.Visible = true;
                        xtraTabControlizlemler.TabPages[0].PageVisible = true;
                        grdlohusaizlem.Visible = true;
                        xtraTabControlizlemler.SelectedTabPage = xtraTabControlizlemler.TabPages[0];


                    }
                    else
                        if (Current.AktifHasta.Cinsiyeti == myenum.Cinsiyet.Erkek)
                        {
                            xtraTabControlizlemler.TabPages[0].PageVisible = false;

                            xtraTabControlgebe.Visible = false;

                            grdlohusaizlem.Visible = false;

                        }
                        else
                            if (Current.AktifHasta.Cinsiyeti == myenum.Cinsiyet.Belirsiz)
                            {
                                xtraTabControlgebe.Visible = true;

                                grdlohusaizlem.Visible = true;
                                xtraTabControlizlemler.TabPages[0].PageVisible = true;
                            }
                }
            }
            xtraTabControldetay.SelectedTabPageIndex = 0;
            xtraTabControlMuayene.SelectedTabPageIndex = 0;
        }

        public void setbuttons(BindingSource bs)
        {

            dbnav.BindingSource = bs;
            Current.AktifHasta = this.AktifHasta;
            btnYeni.Text = " &Yeni " + etiket + " ";
            btnDuzenle.Text = " " + etiket + " &Düzenle ";
            btnincele.Text = " " + etiket + " &incele ";
            btnIptal.Text = " " + etiket + " i&ptal ";
            btnYenile.Text = " " + etiket + " Ye&nile ";


            btnDuzenle.Enabled = false;
            btnIptal.Enabled = false;
            btnYeni.Enabled = false;
            btnincele.Enabled = false;
            btnsecilimuayenekapat.Visible = false;
            btngonderal.Visible = true;
            btnIptal.Visible = true;
            if (etiket == "Hasta")
            {
                btnDuzenle.Enabled = bs.Count > 0;
                btnIptal.Enabled = bs.Count > 0;
                btnIptal.Visible = false;
                btnYeni.Enabled = true;
                btnincele.Enabled = bs.Current != null;
            }
            else
                if (etiket == "Muayene")
                {
                    btnDuzenle.Enabled = bs.Count > 0;
                    //btnYeni.Enabled = bshasta.Count > 0;
                    btnYeni.Enabled = bsbugunkumuayene.Count == 0;
                    btnincele.Enabled = bs.Current != null;
                    btnIptal.Enabled = bs.Count > 0;
                    if (Current.AktifMuayene!=null)
                        btnsecilimuayenekapat.Visible = !Current.AktifMuayene.MuayeneKapalimi;
                }
                else
                    if (xtraTabControlMuayene.SelectedTabPage == xtraTabPageBugunkuMuayeneler)
                    {
                        if (bsbugunkumuayene.Count > 0)
                        {
                            btnDuzenle.Enabled = bs.Count > 0 && !AktifMuayene.MuayeneKapalimi;
                            btnIptal.Enabled = bs.Count > 0 && !AktifMuayene.MuayeneKapalimi;
                            btnYeni.Enabled = !AktifMuayene.MuayeneKapalimi;
                            btnincele.Enabled = bs.Current != null;
                        }
                    }
                    else
                        if (xtraTabControlMuayene.SelectedTabPage == xtraTabPaGecmisMuayene)
                        {
                            if (bsgecmismuayene.Count > 0)
                            {
                                btnDuzenle.Enabled = bs.Count > 0 && !AktifMuayene.MuayeneKapalimi;
                                btnIptal.Enabled = bs.Count > 0 && !AktifMuayene.MuayeneKapalimi;
                                btnYeni.Enabled = !AktifMuayene.MuayeneKapalimi;
                                btnincele.Enabled = bs.Current != null;
                            }
                        }

            if (etiket == "Vefat Bildirimi" || etiket == "Doğum Bildirimi"
                || etiket == "Hastalık" || etiket == "Obezite İzlem" || etiket == "Gebe İzlem"
                || etiket == "Gebe Sonuç" || etiket == "Gebe Başlangıç" || etiket == "Gebe Başlangıç" || etiket == "Lohusa İzlem"
                || etiket == "Bebek/Çocuk İzlem" || etiket == "Bebek Beslenme İzlem" || etiket == "15-49 Kadın İzlem"
                 || etiket == "Vefat Bildirimi" || etiket == "Aşı")  
            {
                btnDuzenle.Enabled = bs.Count > 0;
                btnIptal.Enabled = bs.Count > 0;
                btnYeni.Enabled = true;
                btnincele.Enabled = bs.Current != null;
            }
            

        }
        #endregion form sets


        #region navigation button clicks

        void btnYeni_Click(object sender, EventArgs e)
        {
            yeni();
        }

        void btnDuzenle_Click(object sender, EventArgs e)
        {
            duzenle();
        }
        #endregion navigation button clicks

        #region navigation button metods

        public void duzenle()
        {
            if (etiket == "Aşı")
            {
                //if (Current.AktifMuayeneId != 0)
                //{
                    frmMuayeneAsi f = new frmMuayeneAsi();
                    f.formState = mymodel.myenum.EditMode.emDuzenle;
                    f.Text = etiket;
                    f.HastaBilgileri(Current.AktifHasta);
                    f.ShowDialog();
                    getasi();
                //}
                //else
                //{
                //    MessageBox.Show("Aşı yapmak için muayene seçmelisiniz.");
                //}
            }
            else
                if (etiket == "Hizmet")
                {
                    frmHizmetAta f = new frmHizmetAta(Utility.GetGridToInt((bshizmet.Current as DataRowView), "Id"), mymodel.myenum.EditMode.emDuzenle);
                    f.Text = etiket;
                    f.HastaBilgileri(Current.AktifHasta);
                    f.btntamam.Text = btnDuzenle.Text;
                    f.ShowDialog();
                    gethizmet();

                }
                else
                    if (etiket == "Muayene")
                    {
                        frmMuayene f = new frmMuayene(Current.AktifMuayeneId, mymodel.myenum.EditMode.emDuzenle);
                        f.Text = etiket;
                        f.HastaBilgileri(Current.AktifHasta);
                        f.btntamam.Text = btnDuzenle.Text;
                        f.ShowDialog();
                        getbugunkumuayene();
                        //getgecmismuayene();
                    }
                    else
                        if (etiket == "Hasta")
                        {
                            frmHasta f = new frmHasta(Utility.GetGridToInt((bshasta.Current as DataRowView), "HastaNo"), mymodel.myenum.EditMode.emDuzenle);
                            f.Text = etiket;
                            f.ShowDialog();
                            HastaGetir();
                        }
                        else
                            if (etiket == "Reçete")
                            {
                                frmRecete f = new frmRecete(Convert.ToInt64((bsrecete.Current as DataRowView)["Id"]), mymodel.myenum.EditMode.emDuzenle);
                                f.Text = etiket;
                                f.HastaBilgileri(Current.AktifHasta);
                                f.ShowDialog();
                                getrecete();
                            }
                            else
                                if (etiket == "Anamnez")
                                {
                                    frmAnemnez f = new frmAnemnez(Convert.ToInt64((bsanamnez.Current as DataRowView)["Id"]), myenum.EditMode.emDuzenle);
                                    f.Text = etiket;
                                    f.HastaBilgileri(Current.AktifHasta);
                                    f.ShowDialog();
                                    getanamnez();

                                }
                                else
                                    if (etiket == "Tanı")
                                    {
                                        frmTaniAta f = new frmTaniAta(Convert.ToInt64((bstani.Current as DataRowView)["Id"]), myenum.EditMode.emDuzenle);
                                        f.Text = etiket;
                                        f.HastaBilgileri(Current.AktifHasta);
                                        f.ShowDialog();
                                        gettani();
                                        if (xtraTabControlMuayene.SelectedTabPage == xtraTabPageBugunkuMuayeneler)
                                            getbugunkumuayene();
                                        else
                                            getgecmismuayene();
                                        gethastalik();

                                    }
                                    else
                                        if (etiket == "Rapor")
                                        {
                                            frmSaglikIstirahat f = new frmSaglikIstirahat(Convert.ToInt64((bsraporlar.Current as DataRowView)["Id"]), myenum.EditMode.emDuzenle);
                                            f.Text = etiket;
                                            f.HastaBilgileri(Current.AktifHasta);
                                            f.ShowDialog();
                                            getraporlar();


                                        }
                                        else
                                            if (etiket == "15-49 Kadın İzlem")
                                            {
                                                frm15_49KadinIzleme f = new frm15_49KadinIzleme(Utility.GetGridToInt((bskadinizle.Current as DataRowView), "Id"), mymodel.myenum.EditMode.emDuzenle);
                                                f.Text = etiket;
                                                f.HastaBilgileri(Current.AktifHasta);
                                                f.ShowDialog();
                                                getkadinizlem();
                                            }
                                            else
                                                if (etiket == "Gebe Başlangıç")
                                                {
                                                    frmGebeBaslangic f = new frmGebeBaslangic(Convert.ToInt64((bsgebebaslangic.Current as DataRowView)["Id"]), myenum.EditMode.emDuzenle);
                                                    f.Text = etiket;
                                                    f.HastaBilgileri(Current.AktifHasta);
                                                    f.ShowDialog();
                                                    getgebebaslangic();
                                                }
                                                else
                                                    if (etiket == "Gebe İzlem")
                                                    {
                                                        Sonuc sonuc = frmGebeIzlem.GebeIzlemKontrol();
                                                        if (sonuc.HataVarMi)
                                                        {
                                                            MessageBox.Show(sonuc.Mesaj);
                                                            return;
                                                        }
                                                        frmGebeIzlem f = new frmGebeIzlem(Convert.ToInt64((bsgebeizle.Current as DataRowView)["Id"]), myenum.EditMode.emDuzenle);
                                                        f.Text = etiket;
                                                        f.HastaBilgileri(Current.AktifHasta);
                                                        f.ShowDialog();
                                                        getgebeizle();
                                                    }
                                                    else
                                                        if (etiket == "Gebe Sonuç")
                                                        {

                                                            frmGebeSonuc f = new frmGebeSonuc(Convert.ToInt64((bsgebesonuc.Current as DataRowView)["Id"]), myenum.EditMode.emDuzenle);
                                                            f.Text = etiket;
                                                            f.HastaBilgileri(Current.AktifHasta);
                                                            f.ShowDialog();
                                                            getgebesonuc();
                                                        }
                                                        else
                                                            if (etiket == "Lohusa İzlem")
                                                            {
                                                                frmLohusaIzlem f = new frmLohusaIzlem(Convert.ToInt64((bslohusaizle.Current as DataRowView)["Id"]), myenum.EditMode.emDuzenle);
                                                                f.Text = etiket;
                                                                f.HastaBilgileri(Current.AktifHasta);
                                                                f.ShowDialog();
                                                                getlohusaizle();
                                                            }
                                                            else
                                                                if (etiket == "Obezite İzlem")
                                                                {
                                                                    frmObeziteIzlem f = new frmObeziteIzlem(Convert.ToInt64((bsobeziteizlem.Current as DataRowView)["Id"]), myenum.EditMode.emDuzenle);
                                                                    f.Text = etiket;
                                                                    f.HastaBilgileri(Current.AktifHasta);
                                                                    f.ShowDialog();
                                                                    getobeziteizlem();
                                                                }
                                                                else
                                                                    if (etiket == "Bebek Beslenme İzlem")
                                                                    {
                                                                        frmBebekCocukBeslenme f = new frmBebekCocukBeslenme(Utility.GetGridToInt((bsbebekbeslenizle.Current as DataRowView), "Id"), mymodel.myenum.EditMode.emDuzenle);
                                                                        f.Text = etiket;
                                                                        f.HastaBilgileri(Current.AktifHasta);
                                                                        f.ShowDialog();
                                                                        getbebekbeslenizle();
                                                                    }
                                                                    else
                                                                        if (etiket == "Bebek/Çocuk İzlem")
                                                                        {
                                                                            frmBebekIzleme f = new frmBebekIzleme(Convert.ToInt64((bsbebekcocukizle.Current as DataRowView)["Id"]), myenum.EditMode.emDuzenle);
                                                                            f.Text = etiket;
                                                                            f.HastaBilgileri(Current.AktifHasta);
                                                                            f.ShowDialog();
                                                                            getbebekcocukizle();
                                                                        }
                                                                        else
                                                                            if (etiket == "Doğum Bildirimi")
                                                                            {

                                                                                frmBebekDogumBildirim f = new frmBebekDogumBildirim(Convert.ToInt64((bsdogum.Current as DataRowView)["Id"]), mymodel.myenum.EditMode.emDuzenle);
                                                                                f.Text = etiket;
                                                                                f.HastaBilgileri(Current.AktifHasta);
                                                                                f.ShowDialog();
                                                                                getdogum();
                                                                            }
                                                                            else
                                                                                if (etiket == "Vefat Bildirimi")
                                                                                {
                                                                                    frmOlumBildirimi f = new frmOlumBildirimi(Convert.ToInt64((bsvefat.Current as DataRowView)["Id"]), mymodel.myenum.EditMode.emDuzenle);
                                                                                    f.Text = etiket;
                                                                                    f.HastaBilgileri(Current.AktifHasta);
                                                                                    f.ShowDialog();
                                                                                    getvefat();
                                                                                }
                                                                                else
                                                                                    if (etiket == "Hastalık")
                                                                                    {
                                                                                        frmTaniAta f = new frmTaniAta(Convert.ToInt64((bshastalik.Current as DataRowView)["Id"]), myenum.EditMode.emDuzenle);
                                                                                        f.Text = etiket;
                                                                                        f.HastaBilgileri(Current.AktifHasta);
                                                                                        f.ShowDialog();
                                                                                        gethastalik();
                                                                                    }
                                                                                    else
                                                                                        if (etiket == "Sevk")
                                                                                        {
                                                                                            frmSevk f = new frmSevk(Convert.ToInt64((bssevk.Current as DataRowView)["Id"]), myenum.EditMode.emDuzenle);
                                                                                            f.Text = etiket;
                                                                                            f.HastaBilgileri(Current.AktifHasta);
                                                                                            f.ShowDialog();
                                                                                            getsevk();
                                                                                        }



        }
        /// <summary>
        /// Hata var ise true deger dönderir ve form açılmaz.
        /// </summary>
        /// <returns></returns>
        private bool DogumBildirimValidate()
        {
            if (Current.AktifHastaId > 0)
            {
                if (Current.AktifHasta.KayitDurumu == myenum.KayitDurumu.Misafir)
                {
                    MessageBox.Show("Misafir Hastalar İçin Doğum Bildirimi Yapılamaz");
                    return true;
                }



                return false;
            }

            return false;
        }

        private bool SaglikBildirimValidate()
        {
            if (Current.AktifMuayeneId > 0)
            {
                int tanivarmi = Transaction.Instance.ExecuteScalarI("Select count(Id) from MuayeneTeshis where Muayene_Id=@prm0 and Hasta_Id=@prm1 and Aktif=1", new object[] { Current.AktifMuayeneId, Current.AktifHastaId });
                if (tanivarmi == 0)
                {
                    MessageBox.Show("Hastaya rapor verebilmek için tanı kaydı yapılması gerekir.Tanısız rapor verilemez");
                    return true;
                }
            }
            else
            {
                MessageBox.Show("Muayene olmadan rapor alınamaz");
                return true;
            }
            return false;
        }

        /// <summary>
        /// Hata var ise true deger dönderir ve form açılmaz.
        /// </summary>
        /// <returns></returns>
        private bool ReceteValidate()
        {
            if (Current.AktifMuayeneId == 0)
            {
                MessageBox.Show("Muayene olmadan reçete kaydı yapılamaz");
                return true;
            }

            int tanisayi = Transaction.Instance.ExecuteScalarI("Select Count(Id) from MuayeneTeshis where MuayeneTeshis.Kronikmi=0 and MuayeneTeshis.Alerjikmi=0 and MuayeneTeshis.Muayene_Id =" + Current.AktifMuayeneId);
            if (tanisayi == 0)
            {
                MessageBox.Show("Tanı olmadan reçete kaydı yapılamaz");
                return true;
            }

            int muayeneyeaitrecetesayisi = Transaction.Instance.ExecuteScalarI("Select Count(Id) from Recete where Recete.Muayene_Id =" + Current.AktifMuayeneId);
            if (muayeneyeaitrecetesayisi > 0)
            {
                MessageBox.Show("Bir muayeneye ait bir reçete girişi yapılabilir.\nMevcut reçetenizi düzenleyebiliriniz.");
                return true;
            }

            return false;
        }
        private bool TaniValidate()
        {
            if (Current.AktifMuayeneId == 0)
            {
                MessageBox.Show("Muayene olmadan tanı giremezsiniz");
                return true;
            }
            return false;
        }
        private bool TetkikValidate()
        {
            if (Current.AktifMuayeneId == 0)
            {
                MessageBox.Show("Muayene olmadan tetkik giremezsiniz");
                return true;
            }
            return false;
        }
        private bool AnemnezValidate()
        {
            if (Current.AktifMuayeneId == 0)
            {
                MessageBox.Show("Muayene olmadan anemnez giremezsiniz");
                return true;
            }
            return false;
        }
        private bool SevkValidate()
        {
            if (Current.AktifMuayeneId == 0)
            {
                MessageBox.Show("Muayene olmadan sevk giremezsiniz");
                return true;
            }
            return false;
        }
        private bool HizmetValidate()
        {
            if (Current.AktifMuayeneId == 0)
            {
                MessageBox.Show("Muayene olmadan hizmet giremezsiniz");
                return true;
            }
            return false;
        }

        /// <summary>
        /// Hata var ise true deger dönderir ve form açılmaz.
        /// </summary>
        /// <returns></returns>
        public bool MuayeneValidate()
        {
            if (Current.AktifRandevuId > 0)
            {
                if (System.DateTime.Today > Current.AktifRandevu.BasTarih)
                {
                    MessageBox.Show("Geçmişe yönelik randevu girişi yapılamaz.");
                    return true;
                }
                else
                    return false;
            }
            return false;
        }

        public void yeni()
        {
            if (Current.AktifRandevuId > 0)
            {
                if (System.DateTime.Now < Current.AktifRandevu.BasTarih)
                {
                    MessageBox.Show("İleri tarihli randevu işlemi yapılamaz.");
                    return;
                }
            }

            if (etiket == "Hizmet")
            {
                if (HizmetValidate())
                    return;

                frmHizmetAta f = new frmHizmetAta(Utility.GetGridToInt((bshizmet.Current as DataRowView), "Id"), mymodel.myenum.EditMode.emDuzenle);
                f.Text = etiket;
                f.btntamam.Text = btnDuzenle.Text;
                f.HastaBilgileri(Current.AktifHasta);
                f.ShowDialog();
                gethizmet();


            }
            else
                if (etiket == "Hasta")
                {
                    frmHasta f = new frmHasta();

                    f.formState = mymodel.myenum.EditMode.emYeni;
                    f.Text = etiket;
                    f.HastaBilgileri(Current.AktifHasta);
                    f.ShowDialog();
                    HastaGetir();
                }
                else
                    //if (etiket == "Hastalık")
                    //{
                    //    frmTaniAta f = new frmTaniAta();
                    //    f.Text = etiket;
                    //    f.formState =mymodel.myenum.EditMode.emYeni;
                    //    //f.AktifHasta = AktifHasta;
                    //    //f.AktifMuayene = AktifMuayene;
                    //    f.hastalikmi = true;
                    //    f.ShowDialog();
                    //    gethastalik();
                    //}
                    //else
                    if (etiket == "Doğum Bildirimi")
                    {
                        if (DogumBildirimValidate())
                            return;
                        frmBebekDogumBildirim f = new frmBebekDogumBildirim();
                        f.Text = etiket;
                        f.HastaBilgileri(Current.AktifHasta);
                        f.formState = mymodel.myenum.EditMode.emYeni;
                        //f.AktifHasta = AktifHasta;
                        //f.AktifMuayene = AktifMuayene;
                        f.ShowDialog();
                        getdogum();

                    }
                    else
                        if (etiket == "Vefat Bildirimi")
                        {
                            frmOlumBildirimi f = new frmOlumBildirimi();
                            f.Text = etiket;
                            f.HastaBilgileri(Current.AktifHasta);
                            f.formState = mymodel.myenum.EditMode.emYeni;
                            f.ShowDialog();
                            getvefat();

                        }
                        else
                            if (etiket == "Tanı")
                            {
                                if (TaniValidate())
                                    return;

                                frmTaniAta f = new frmTaniAta(Utility.GetGridToInt((bstani.Current as DataRowView), "Id"), mymodel.myenum.EditMode.emDuzenle);
                                f.formState = mymodel.myenum.EditMode.emYeni;
                                f.Text = etiket;
                                f.HastaBilgileri(Current.AktifHasta);
                                f.ShowDialog();
                                gettani();
                                if (xtraTabControlMuayene.SelectedTabPage == xtraTabPageBugunkuMuayeneler)
                                    getbugunkumuayene();
                                else
                                    getgecmismuayene();
                                gethastalik();
                                //HastaGetir();
                            }
                            else
                                if (etiket == "Rapor")
                                {

                                    if (SaglikBildirimValidate())
                                        return;
                                    frmSaglikIstirahat f = new frmSaglikIstirahat();
                                    f.Text = etiket;
                                    f.HastaBilgileri(Current.AktifHasta);
                                    f.ShowDialog();
                                    getraporlar();


                                }
                                else
                                    if (etiket == "Reçete")
                                    {
                                        if (ReceteValidate())
                                            return;

                                        frmRecete f = new frmRecete();
                                        f.formState = mymodel.myenum.EditMode.emYeni;
                                        f.Text = etiket;
                                        f.HastaBilgileri(Current.AktifHasta);
                                        f.ShowDialog();
                                        getrecete();
                                    }
                                    else
                                        if (etiket == "Sevk")
                                        {
                                            if (SevkValidate())
                                                return;

                                            frmSevk f = new frmSevk();
                                            f.formState = mymodel.myenum.EditMode.emYeni;
                                            f.Text = etiket;
                                            f.HastaBilgileri(Current.AktifHasta);
                                            f.ShowDialog();
                                            getsevk();
                                            //HastaGetir();
                                        }
                                        else
                                            if (etiket == "Tetkik")
                                            {
                                                if (TetkikValidate())
                                                    return;
                                                frmTetkikAta f = new frmTetkikAta();
                                                f.formState = mymodel.myenum.EditMode.emYeni;
                                                f.Text = etiket;
                                                f.HastaBilgileri(Current.AktifHasta);
                                                f.ShowDialog();
                                                gettetkik();
                                            }
                                            else
                                                if (etiket == "Aşı")
                                                {
                                                    frmMuayeneAsi f = new frmMuayeneAsi();
                                                    f.formState = mymodel.myenum.EditMode.emYeni;

                                                    f.Text = etiket;
                                                    f.HastaBilgileri(Current.AktifHasta);
                                                    f.ShowDialog();
                                                    getasi();

                                                }
                                                else
                                                    if (etiket == "Muayene")
                                                    {

                                                        //TODO:Randevu Idyi tutarsak bir randevu id ile bir muayene olabilr mesajı verebiliriz.
                                                        //int muayene = Transaction.Instance.ExecuteScalarI("Select Count(Id) from Muayene where Muayene.Id " + Current.AktifMuayeneId);
                                                        // if (muayene > 0)
                                                        // {
                                                        //     MessageBox.Show("Bir randevu ile ancak bir muayene yapılabilir.");
                                                        //     return;
                                                        // }
                                                        if (MuayeneValidate())
                                                            return;

                                                        Hastaliklar();

                                                        frmMuayene f = new frmMuayene();
                                                        f.formState = mymodel.myenum.EditMode.emYeni;
                                                        f.Text = etiket;
                                                        f.HastaBilgileri(Current.AktifHasta);
                                                        f.btntamam.Text = btnYeni.Text;
                                                        f.ShowDialog();
                                                        getbugunkumuayene();
                                                        //getgecmismuayene();
                                                    }
                                                    else
                                                        if (etiket == "Anamnez")
                                                        {
                                                            if (AnemnezValidate())
                                                                return;

                                                            frmAnemnez f = new frmAnemnez();
                                                            f.formState = mymodel.myenum.EditMode.emYeni;
                                                            f.Text = etiket;
                                                            //f.AktifHasta = AktifHasta;
                                                            //f.AktifMuayene = AktifMuayene;
                                                            f.HastaBilgileri(Current.AktifHasta);
                                                            f.ShowDialog();
                                                            getanamnez();

                                                        }
                                                        else

                                                            if (etiket == "Obezite İzlem")
                                                            {
                                                                frmObeziteIzlem f = new frmObeziteIzlem();
                                                                f.formState = mymodel.myenum.EditMode.emYeni;
                                                                //f.AktifHasta = AktifHasta;
                                                                //f.AktifMuayene = AktifMuayene;
                                                                f.Text = etiket;
                                                                f.HastaBilgileri(Current.AktifHasta);
                                                                f.ShowDialog();
                                                                getobeziteizlem();

                                                            }
                                                            else
                                                                if (etiket == "Gebe İzlem")
                                                                {
                                                                    Sonuc sonuc= frmGebeIzlem.GebeIzlemKontrol();
                                                                    if (sonuc.HataVarMi)
                                                                    {
                                                                        MessageBox.Show(sonuc.Mesaj);
                                                                        return;
                                                                    }
                                                                    frmGebeIzlem f = new frmGebeIzlem();
                                                                    f.formState = mymodel.myenum.EditMode.emYeni;
                                                                    //f.AktifHasta = AktifHasta;
                                                                    //f.AktifMuayene = AktifMuayene;
                                                                    f.Text = etiket;
                                                                    f.HastaBilgileri(Current.AktifHasta);
                                                                    f.ShowDialog();
                                                                    getgebeizle();

                                                                }
                                                                else
                                                                    if (etiket == "Gebe Sonuç")
                                                                    {
                                                                        frmGebeSonuc f = new frmGebeSonuc();
                                                                        f.formState = mymodel.myenum.EditMode.emYeni;
                                                                        //f.AktifHasta = AktifHasta;
                                                                        //f.AktifMuayene = AktifMuayene;
                                                                        f.Text = etiket;
                                                                        f.HastaBilgileri(Current.AktifHasta);
                                                                        f.ShowDialog();
                                                                        getgebesonuc();

                                                                    }
                                                                    else
                                                                        if (etiket == "Gebe Başlangıç")
                                                                        {

                                                                            frmGebeBaslangic f = new frmGebeBaslangic();
                                                                            f.formState = mymodel.myenum.EditMode.emYeni;
                                                                            f.Text = etiket;
                                                                            f.HastaBilgileri(Current.AktifHasta);
                                                                            f.ShowDialog();
                                                                            getgebebaslangic();


                                                                        }
                                                                        else
                                                                            if (etiket == "Lohusa İzlem")
                                                                            {
                                                                                frmLohusaIzlem f = new frmLohusaIzlem();
                                                                                f.formState = mymodel.myenum.EditMode.emYeni;
                                                                                f.Text = etiket;
                                                                                f.HastaBilgileri(Current.AktifHasta);
                                                                                f.ShowDialog();
                                                                                getlohusaizle();

                                                                            }
                                                                            else
                                                                                if (etiket == "Bebek/Çocuk İzlem")
                                                                                {
                                                                                    frmBebekIzleme f = new frmBebekIzleme();
                                                                                    f.formState = mymodel.myenum.EditMode.emYeni;
                                                                                    //f.AktifHasta = AktifHasta;
                                                                                    //f.AktifMuayene = AktifMuayene;
                                                                                    f.Text = etiket;
                                                                                    f.HastaBilgileri(Current.AktifHasta);
                                                                                    f.ShowDialog();
                                                                                    getbebekcocukizle();

                                                                                }
                                                                                else
                                                                                    if (etiket == "Bebek Beslenme İzlem")
                                                                                    {
                                                                                        frmBebekCocukBeslenme f = new frmBebekCocukBeslenme();
                                                                                        f.formState = mymodel.myenum.EditMode.emYeni;
                                                                                        //f.AktifHasta = AktifHasta;
                                                                                        //f.AktifMuayene = AktifMuayene;
                                                                                        f.Text = etiket;
                                                                                        f.HastaBilgileri(Current.AktifHasta);
                                                                                        f.ShowDialog();
                                                                                        getbebekbeslenizle();

                                                                                    }
                                                                                    else
                                                                                        if (etiket == "15-49 Kadın İzlem")
                                                                                        {
                                                                                            frm15_49KadinIzleme f = new frm15_49KadinIzleme();
                                                                                            f.formState = mymodel.myenum.EditMode.emYeni;
                                                                                            f.Text = etiket;
                                                                                            f.HastaBilgileri(Current.AktifHasta);
                                                                                            f.ShowDialog();
                                                                                            getkadinizlem();

                                                                                        }



        }

        public static void Hastaliklar()
        {
            DataTable dt = Transaction.Instance.ExecuteSql("select h.Id,t.Kodu,t.Adi Hastalik,h.Kronikmi Kronik,h.Alerjikmi Alerjik" +
             "\nfrom MuayeneTeshis h join teshis t on t.Id=h.teshis_Id join muayene m on m.Id=h.muayene_Id" +
             "\nwhere h.Aktif=1 and m.hasta_Id= " + Current.AktifHastaId + " and (h.Kronikmi=1 or h.Alerjikmi=1)" +
             "\norder by h.Id desc");
            if (dt.Rows.Count > 0)
            {
                frmHastaBilgilendirme frm = new frmHastaBilgilendirme(Current.AktifHasta, dt);
                frm.ShowDialog();
            }
        }

        public void Incele()
        {

            if (etiket == "Hizmet")
            {
                frmHizmetAta f = new frmHizmetAta(Utility.GetGridToInt((bshizmet.Current as DataRowView), "Id"), mymodel.myenum.EditMode.emIncele);
                f.Text = etiket;
                f.HastaBilgileri(Current.AktifHasta);
                f.btntamam.Text = btnDuzenle.Text;
                f.ShowDialog();
                gethizmet();

            }
            else
                if (etiket == "Hasta")
                {
                    frmHasta f = new frmHasta(AktifHasta.Id, myenum.EditMode.emIncele);
                    f.Text = etiket;
                    f.HastaBilgileri(Current.AktifHasta);
                    f.ShowDialog();
                    gethasta("select   h.Id,h.Id HastaNo,h.TckNo,h.PasaportNo,h.Adi+' '+h.Soyadi AdiSoyadi,h.DogumTarihi,h.Cinsiyeti,h.KayitDurumu from Hasta h where h.Aktif=1 order by h.Adi,h.Soyadi");

                }                
                else
                    if (etiket == "Doğum Bildirimi")
                    {
                        frmBebekDogumBildirim f = new frmBebekDogumBildirim(Convert.ToInt32((bsdogum.Current as DataRowView)["Id"]), mymodel.myenum.EditMode.emIncele);
                        f.Text = etiket;
                        f.HastaBilgileri(Current.AktifHasta);
                        f.ShowDialog();
                        getdogum();
                    }
                    else
                        if (etiket == "Vefat Bildirimi")
                        {
                            frmOlumBildirimi f = new frmOlumBildirimi(Convert.ToInt32((bsvefat.Current as DataRowView)["Id"]), mymodel.myenum.EditMode.emIncele);
                            f.Text = etiket;
                            f.HastaBilgileri(Current.AktifHasta);
                            f.ShowDialog();
                            getvefat();
                        }
                        else
                            if (etiket == "Tanı")
                            {

                                frmTaniAta f = new frmTaniAta(Current.AktifMuayeneId, myenum.EditMode.emIncele);
                                f.Text = etiket;
                                f.HastaBilgileri(Current.AktifHasta);
                                f.ShowDialog();
                                gettani();
                                if (xtraTabControlMuayene.SelectedTabPage == xtraTabPageBugunkuMuayeneler)
                                    getbugunkumuayene();
                                else
                                    getgecmismuayene();
                                gethastalik();

                            }
                            else
                                if (etiket == "Rapor")
                                {
                                    frmSaglikIstirahat f = new frmSaglikIstirahat(Convert.ToInt32((bsraporlar.Current as DataRowView)["Id"]), myenum.EditMode.emIncele);
                                    f.Text = etiket;
                                    f.HastaBilgileri(Current.AktifHasta);
                                    f.ShowDialog();
                                    getraporlar();


                                }
                                else
                                    if (etiket == "Reçete")
                                    {
                                        frmRecete f = new frmRecete(Convert.ToInt32((bsrecete.Current as DataRowView)["Id"]), mymodel.myenum.EditMode.emIncele);
                                        f.Text = etiket;
                                        f.HastaBilgileri(Current.AktifHasta);
                                        f.ShowDialog();
                                        getrecete();
                                    }
                                    else
                                        if (etiket == "Sevk")
                                        {
                                            frmSevk f = new frmSevk(Convert.ToInt32((bssevk.Current as DataRowView)["Id"]), mymodel.myenum.EditMode.emIncele);
                                            f.Text = etiket;
                                            f.HastaBilgileri(Current.AktifHasta);
                                            f.ShowDialog();
                                            getsevk();
                                        }
                                        else
                                            if (etiket == "Tetkik")
                                            {
                                                frmTetkikAta f = new frmTetkikAta();
                                                f.formState = mymodel.myenum.EditMode.emYeni;
                                                f.Text = etiket;
                                                f.HastaBilgileri(Current.AktifHasta);
                                                f.ShowDialog();
                                                gettetkik();
                                            }
                                            else
                                                if (etiket == "Aşı")
                                                {
                                                    frmMuayeneAsi f = new frmMuayeneAsi();
                                                    f.formState = mymodel.myenum.EditMode.emIncele;
                                                    f.Text = etiket;
                                                    f.HastaBilgileri(Current.AktifHasta);
                                                    f.ShowDialog();
                                                    getasi();
                                                }
                                                else
                                                    if (etiket == "Muayene")
                                                    {
                                                        frmMuayene f = new frmMuayene(Current.AktifMuayeneId, mymodel.myenum.EditMode.emIncele);
                                                        f.Text = etiket;
                                                        f.HastaBilgileri(Current.AktifHasta);
                                                        f.btntamam.Text = btnYeni.Text;
                                                        f.ShowDialog();
                                                        getbugunkumuayene();
                                                        //getgecmismuayene();
                                                    }
                                                    else
                                                        if (etiket == "Anamnez")
                                                        {
                                                            frmAnemnez f = new frmAnemnez(Convert.ToInt32((bsanamnez.Current as DataRowView)["Id"]), myenum.EditMode.emIncele);
                                                            f.Text = etiket;
                                                            f.HastaBilgileri(Current.AktifHasta);
                                                            f.ShowDialog();
                                                            getanamnez();

                                                        }
                                                        else

                                                            if (etiket == "Obezite İzlem")
                                                            {
                                                                frmObeziteIzlem f = new frmObeziteIzlem(Convert.ToInt32((bsobeziteizlem.Current as DataRowView)["Id"]), myenum.EditMode.emIncele);
                                                                f.Text = etiket;
                                                                f.HastaBilgileri(Current.AktifHasta);
                                                                f.ShowDialog();
                                                                getobeziteizlem();
                                                            }
                                                            else
                                                                if (etiket == "Gebe İzlem")
                                                                {
                                                                    Sonuc sonuc = frmGebeIzlem.GebeIzlemKontrol();
                                                                    if (sonuc.HataVarMi)
                                                                    {
                                                                        MessageBox.Show(sonuc.Mesaj);
                                                                        return;
                                                                    }
                                                                    frmGebeIzlem f = new frmGebeIzlem(Convert.ToInt32((bsgebeizle.Current as DataRowView)["Id"]), myenum.EditMode.emIncele);
                                                                    f.Text = etiket;
                                                                    f.HastaBilgileri(Current.AktifHasta);
                                                                    f.ShowDialog();
                                                                    getgebeizle();
                                                                }
                                                                else
                                                                    if (etiket == "Gebe Sonuç")
                                                                    {
                                                                        frmGebeSonuc f = new frmGebeSonuc(Convert.ToInt32((bsgebeizle.Current as DataRowView)["Id"]), myenum.EditMode.emIncele);
                                                                        f.Text = etiket;
                                                                        f.HastaBilgileri(Current.AktifHasta);
                                                                        f.ShowDialog();
                                                                        getgebesonuc();
                                                                    }
                                                                    else
                                                                        if (etiket == "Gebe Başlangıç")
                                                                        {
                                                                            frmGebeBaslangic f = new frmGebeBaslangic(Convert.ToInt32((bsgebebaslangic.Current as DataRowView)["Id"]), myenum.EditMode.emIncele);
                                                                            f.Text = etiket;
                                                                            f.HastaBilgileri(Current.AktifHasta);
                                                                            f.ShowDialog();
                                                                            getgebebaslangic();

                                                                        }
                                                                        else
                                                                            if (etiket == "Lohusa İzlem")
                                                                            {
                                                                                frmLohusaIzlem f = new frmLohusaIzlem(Convert.ToInt32((bslohusaizle.Current as DataRowView)["Id"]), myenum.EditMode.emIncele);
                                                                                f.Text = etiket;
                                                                                f.HastaBilgileri(Current.AktifHasta);
                                                                                f.ShowDialog();
                                                                                getlohusaizle();
                                                                            }
                                                                            else
                                                                                if (etiket == "Bebek/Çocuk İzlem")
                                                                                {
                                                                                    frmBebekIzleme f = new frmBebekIzleme(Convert.ToInt32((bsbebekcocukizle.Current as DataRowView)["Id"]), myenum.EditMode.emIncele);
                                                                                    f.Text = etiket;
                                                                                    f.HastaBilgileri(Current.AktifHasta);
                                                                                    f.ShowDialog();
                                                                                    getbebekcocukizle();
                                                                                }
                                                                                else
                                                                                    if (etiket == "Bebek Beslenme İzlem")
                                                                                    {
                                                                                        frmBebekCocukBeslenme f = new frmBebekCocukBeslenme(Utility.GetGridToInt((bsbebekbeslenizle.Current as DataRowView), "Id"), mymodel.myenum.EditMode.emIncele);
                                                                                        f.Text = etiket;
                                                                                        f.HastaBilgileri(Current.AktifHasta);
                                                                                        f.ShowDialog();
                                                                                        getbebekbeslenizle();
                                                                                    }
                                                                                    else
                                                                                        if (etiket == "15-49 Kadın İzlem")
                                                                                        {
                                                                                            frm15_49KadinIzleme f = new frm15_49KadinIzleme(Utility.GetGridToInt((bskadinizle.Current as DataRowView), "Id"), mymodel.myenum.EditMode.emIncele);
                                                                                            f.Text = etiket;
                                                                                            f.HastaBilgileri(Current.AktifHasta);
                                                                                            f.ShowDialog();
                                                                                            getkadinizlem();
                                                                                        }


        }

        public void Yenile(string EntityName)
        {
            if (Current.AktifHastaId > 0)
            {
                switch (EntityName)
                {
                    case "Muayene":
                        if (xtraTabControlMuayene.SelectedTabPage == xtraTabPageBugunkuMuayeneler)
                            getbugunkumuayene();
                        else
                            getgecmismuayene();
                        break;
                    case "MuayeneTetkik":
                        gettetkik();
                        break;
                    case "MuayeneSevk":
                        getsevk();
                        break;
                    case "MuayeneAsi":
                        getasi();
                        break;
                    case "MuayeneTeshis":
                        gettani();
                        if (xtraTabControlMuayene.SelectedTabPage == xtraTabPageBugunkuMuayeneler)
                            getbugunkumuayene();
                        else
                            getgecmismuayene();
                        gethastalik();
                        break;
                    case "Recete":
                        getrecete();
                        break;
                    case "Anamnez":
                        getanamnez();
                        break;
                    case "KadinIzleme":
                        getkadinizlem();
                        break;
                    case "GebeBaslangic":
                        getgebebaslangic();
                        break;
                    case "GebeIzleme":
                        getgebeizle();
                        break;
                    case "GebeSonuc":
                        getgebesonuc();
                        break;
                    case "LohusaIzleme":
                        getlohusaizle();
                        break;
                    case "ObezIzleme":
                        getobeziteizlem();
                        break;
                    case "BebekCocukBeslenme":
                        getbebekbeslenizle();
                        break;
                    case "BebekIzleme":
                        getbebekcocukizle();
                        break;
                    case "BebekCocukBilgi":
                        getdogum();
                        break;

                    case "OlumBildirimi":
                        getvefat();
                        break;
                    case "Hizmet":
                        gethizmet();
                        break;
                    case "Rapor":
                        getraporlar();
                        break;

                    default:
                        break;
                }
            }
            else
            {
                switch (EntityName)
                {
                    case "Hasta":
                        gethasta("select  h.Id,h.Id HastaNo,h.TckNo,h.Adi+' '+h.Soyadi AdiSoyadi,h.DogumTarihi,h.Cinsiyeti from Hasta h where h.Aktif=1 order by h.Adi,h.Soyadi");
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion navigation button metods

        #region grid enters

        void grdvefat_Enter(object sender, EventArgs e)
        {
            etiket = "Vefat Bildirimi";
            setbuttons(bsvefat);
        }

        void grddogum_Enter(object sender, EventArgs e)
        {
            etiket = "Doğum Bildirimi";
            setbuttons(bsdogum);
        }

        void grdhasta_Enter(object sender, EventArgs e)
        {
            etiket = "Hasta";
            setbuttons(bshasta);
        }


        void grdhastalik_Enter(object sender, EventArgs e)
        {
            etiket = "Hastalık";
            setbuttons(bshastalik);
            //TODO:Bu entitynin giriş ekranı yok tanı ekranından geliyor buradaki veriler:
            btnDuzenle.Enabled = false;
            btnIptal.Enabled = false;
            btnYeni.Enabled = false;
            btnincele.Enabled = false;
        }

        void grdasi_Enter(object sender, EventArgs e)
        {
            etiket = "Aşı";
            setbuttons(bsasi);
        }

        void grdsevk_Enter(object sender, EventArgs e)
        {
            etiket = "Sevk";
            setbuttons(bssevk);
        }

        void grdtetkik_Enter(object sender, EventArgs e)
        {
            etiket = "Tetkik";
            setbuttons(bstetkik);
        }

        void grdtani_Enter(object sender, EventArgs e)
        {
            etiket = "Tanı";
            setbuttons(bstani);
        }

        void grdrecete_Enter(object sender, EventArgs e)
        {
            etiket = "Reçete";
            setbuttons(bsrecete);
        }

        void grdmuayene_Enter(object sender, EventArgs e)
        {
            etiket = "Muayene";
            setbuttons(bsbugunkumuayene);
        }

        void grdanemnez_Enter(object sender, EventArgs e)
        {
            etiket = "Anamnez";
            setbuttons(bsanamnez);
        }

        void grdobezizlem_Enter(object sender, EventArgs e)
        {
            etiket = "Obezite İzlem";
            setbuttons(bsobeziteizlem);
        }

        void grdgebeizlem_Enter(object sender, EventArgs e)
        {
            etiket = "Gebe İzlem";
            setbuttons(bsgebeizle);
        }

        void grdgebesonuc_Enter(object sender, EventArgs e)
        {
            etiket = "Gebe Sonuç";
            setbuttons(bsgebeizle);
        }

        void grdgebebaslangic_Enter(object sender, EventArgs e)
        {
            etiket = "Gebe Başlangıç";
            setbuttons(bsgebeizle);
        }

        void grdlohusaizlem_Enter(object sender, EventArgs e)
        {
            etiket = "Lohusa İzlem";
            setbuttons(bslohusaizle);
        }

        void grdbebekcocukizlem_Enter(object sender, EventArgs e)
        {
            etiket = "Bebek/Çocuk İzlem";
            setbuttons(bsbebekcocukizle);
        }

        void grdbebekbeslenmeizlem_Enter(object sender, EventArgs e)
        {
            etiket = "Bebek Beslenme İzlem";
            setbuttons(bsbebekbeslenizle);
        }

        void grdkadinizlem_Enter(object sender, EventArgs e)
        {
            etiket = "15-49 Kadın İzlem";
            setbuttons(bskadinizle);
        }

        private void grdsevk_Enter_1(object sender, EventArgs e)
        {
            etiket = "Sevk";
            setbuttons(bssevk);
        }

        void gridhizmet_Enter(object sender, EventArgs e)
        {
            etiket = "Hizmet";
            setbuttons(bshizmet);
        }

        #endregion grid enters

        #region get sqls

        public void getgebeizle()
        {
            bsgebeizle.DataSource = null;
            

            string sql = string.Empty;           
            bsgebeizle.DataSource = Transaction.Instance.ExecuteSql(@"Select 
            * from GebeIzleme where GebeIzleme.Hasta_Id=" + Current.AktifHastaId + sql + " and Aktif=1 ");

            grdgebeizlem.DataSource = bsgebeizle;

            Utility.SetGridStyle(((DevExpress.XtraGrid.Views.Grid.GridView)grdgebeizlem.Views[0]));


            if (bsgebeizle.Count > 0)
                tbgebeizlem.Text = "Gebe İzlem:[" + bsgebeizle.Count + " Adet]";
            else
                tbgebeizlem.Text = "Gebe İzlem";
        }

        public void getgebesonuc()
        {
            bsgebesonuc.DataSource = null;
          
            string sql = string.Empty;         

            bsgebesonuc.DataSource = Transaction.Instance.ExecuteSql(@"Select 
            * from GebeSonuc where GebeSonuc.Hasta_Id=" + Current.AktifHastaId + sql + " and Aktif=1 ");


            grdgebesonuc.DataSource = bsgebesonuc;
            Utility.SetGridStyle(((DevExpress.XtraGrid.Views.Grid.GridView)grdgebesonuc.Views[0]));
            tpgebesonuc.Text = "Gebe Sonuç:[" + bsgebesonuc.Count + " Adet]";


            if (bsgebesonuc.Count > 0)
                tpgebesonuc.Text = "Gebe Sonuç:[" + bsgebesonuc.Count + " Adet]";
            else
                tpgebesonuc.Text = "Gebe Sonuç";
        }

        public void getgebebaslangic()
        {
            bsgebebaslangic.DataSource = null;
           

            string sql = string.Empty;

            bsgebebaslangic.DataSource = Transaction.Instance.ExecuteSql(@"Select 
            * from GebeBaslangic where GebeBaslangic.hasta_Id=" + Current.AktifHastaId + sql + " and Aktif=1 ");
            grdgebebaslangic.DataSource = bsgebebaslangic;
            Utility.SetGridStyle(((DevExpress.XtraGrid.Views.Grid.GridView)grdgebebaslangic.Views[0]));


            if (bsanamnez.Count > 0)
                tpgebebaslangic.Text = "Gebe Başlangıç:[" + bsanamnez.Count + " Adet]";
            else
                tpgebebaslangic.Text = "Gebe Başlangıç";
        }

        public void getlohusaizle()
        {
            bslohusaizle.DataSource = null;
           

            string sql = string.Empty;

            bslohusaizle.DataSource = Transaction.Instance.ExecuteSql(@"Select 
            * from LohusaIzleme where LohusaIzleme.hasta_Id=" + Current.AktifHastaId + sql + " and Aktif=1 ");
            grdlohusaizlem.DataSource = bslohusaizle;
            Utility.SetGridStyle(((DevExpress.XtraGrid.Views.Grid.GridView)grdlohusaizlem.Views[0]));
        }

        public void getbebekcocukizle()
        {
            bsbebekcocukizle.DataSource = null;
           
            string sql = string.Empty;

            bsbebekcocukizle.DataSource = Transaction.Instance.ExecuteSql(@"Select 
            * from BebekIzleme where BebekIzleme.hasta_Id=" + Current.AktifHastaId + sql + " and Aktif=1 ");
            grdbebekcocukizlem.DataSource = bsbebekcocukizle;

            grdbebekcocukizlem.SetGridStyle(
                 @" <Style>
                    <Column Name='Id' HeaderText='Id' Width='45' DisplayIndex='1'   Visible='false'/> 
                    <Column Name='Hasta_Id' HeaderText='Hasta_Id' Width='90' DisplayIndex='2'  Visible='false' />  
                    <Column Name='Muayene_Id' HeaderText='Muayene_Id' Width='50'    DisplayIndex='3'  Visible='false'/>                  
                    <Column Name='Randevu_Id Tarih' HeaderText='Randevu_Id' Width='80' DisplayIndex='4'   Visible='false'/>
                    <Column Name='Doktor_Id' HeaderText='Doktor_Id' Width='80' DisplayIndex='8'   Visible='false'/>
                    <Column Name='IzlemTarihi' HeaderText='IzlemTarihi' Width='80'   DisplayIndex='5' Visible='True' />
                    <Column Name='Agirligi' HeaderText='Ağırlığı' Width='90'  DisplayIndex='6'  Visible='True'/>                         
                    <Column Name='Boyu' HeaderText='Boyu' Width='90'   DisplayIndex='7'  Visible='True'/>
                    <Column Name='BasCevresi' HeaderText='Baş Çevresi' Width='90'  DisplayIndex='9'  Visible='True'/>
                    <Column Name='GogusCevresi' HeaderText='GogusCevresi' Width='90'  DisplayIndex='10'  Visible='True'/>
                    <Column Name='KolCevresi' HeaderText='Kol Çevresi' Width='110'  DisplayIndex='11'  Visible='True'/>
                    <Column Name='FontenalAcikmi' HeaderText='Fontenal Açıkmı' Width='100'  DisplayIndex='12'  Visible='True'/>
                    </Style>");

            if (bsbebekcocukizle.Count > 0)
                tpbebekcocuk.Text = "Bebek/Çocuk İzlemleri:[" + bsbebekcocukizle.Count + " Adet]";
            else
                tpbebekcocuk.Text = "Bebek/Çocuk İzlemleri";
        }

        public void getbebekbeslenizle()
        {
            bsbebekbeslenizle.DataSource = null;
            
            string sql = string.Empty;

            bsbebekbeslenizle.DataSource = Transaction.Instance.ExecuteSql(@"Select 
            * from BebekCocukBeslenme where BebekCocukBeslenme.hasta_Id=" + Current.AktifHastaId + sql + " and Aktif=1 ");
            grdbebekbeslenmeizlem.DataSource = bsbebekbeslenizle;

            grdbebekbeslenmeizlem.SetGridStyle(
                   @" <Style>
                    <Column Name='Id' HeaderText='Id' Width='45' DisplayIndex='1'   Visible='false'/> 
                    <Column Name='Hasta_Id' HeaderText='Hasta_Id' Width='90' DisplayIndex='2'  Visible='false' />  
                    <Column Name='Muayene_Id' HeaderText='Muayene_Id' Width='50'    DisplayIndex='3'  Visible='false'/>                  
                    <Column Name='Randevu_Id Tarih' HeaderText='Randevu_Id' Width='80' DisplayIndex='4'   Visible='false'/>
                    <Column Name='Doktor_Id' HeaderText='Doktor_Id' Width='80' DisplayIndex='8'   Visible='false'/>
                    <Column Name='IzlemTarihi' HeaderText='IzlemTarihi' Width='80'   DisplayIndex='5' Visible='True' />
                    <Column Name='AnneSutuKesilmeAyi' HeaderText='Anne Sütü Kesilme Ayı' Width='150'  DisplayIndex='6'  Visible='True'/>                         
                    <Column Name='SadeceAnneSutuSuresi' HeaderText='Sadece Anne SütüSüresi' Width='150'   DisplayIndex='7'  Visible='True'/>
                    <Column Name='ilkGidaAyi' HeaderText='ilk Gıda Ayı' Width='80'  DisplayIndex='9'  Visible='True'/>
         
                    </Style>");

        }

        public void getobeziteizlem()
        {
            bsobeziteizlem.DataSource = null;
          
            string sql = string.Empty;

            bsobeziteizlem.DataSource = Transaction.Instance.ExecuteSql(@"Select 
            * from ObezIzleme where ObezIzleme.hasta_Id=" + Current.AktifHastaId + sql + " and Aktif=1 ");
            grdobezizlem.DataSource = bsobeziteizlem;
            grdobezizlem.SetGridStyle(
                    @" <Style>
                    <Column Name='Id' HeaderText='Id' Width='45' DisplayIndex='1'   Visible='false'/> 
                    <Column Name='Hasta_Id' HeaderText='Hasta_Id' Width='90' DisplayIndex='2'  Visible='false' />  
                    <Column Name='Muayene_Id' HeaderText='Muayene_Id' Width='50'    DisplayIndex='3'  Visible='false'/>                  
                    <Column Name='Randevu_Id Tarih' HeaderText='Randevu_Id' Width='80' DisplayIndex='4'   Visible='false'/>
                    <Column Name='Doktor_Id' HeaderText='Doktor_Id' Width='80' DisplayIndex='5'   Visible='false'/>
                    <Column Name='IzlemTarihi' HeaderText='İzlem Tarihi' Width='100'  DisplayIndex='6'  Visible='True'/>                         
                    <Column Name='Agirligi' HeaderText='Agirligi' Width='80'   DisplayIndex='7'  Visible='True'/>
                    <Column Name='Boyu' HeaderText='Boyu' Width='80'   DisplayIndex='8' Visible='True' />
                    <Column Name='BelGenisligi' HeaderText='BelGenisligi' Width='80'  DisplayIndex='9'  Visible='True'/>
                    <Column Name='Basen' HeaderText='Basen' Width='80'  DisplayIndex='10'  Visible='True'/>
                    <Column Name='Sonucu' HeaderText='Sonucu' Width='80'  DisplayIndex='11'  Visible='True'/>
                    </Style>");

        }

        public void getkadinizlem()
        {
            bskadinizle.DataSource = null;
           

            string sql = string.Empty;

            bskadinizle.DataSource = Transaction.Instance.ExecuteSql(@"Select 
            * from KadinIzleme where KadinIzleme.hasta_Id=" + Current.AktifHastaId + sql + " and Aktif=1 ");


            grdkadinizlem.DataSource = bskadinizle;
            Utility.SetGridStyle(((DevExpress.XtraGrid.Views.Grid.GridView)grdkadinizlem.Views[0]));

            if (bskadinizle.Count > 0)
                tpkadin.Text = "15 - 49 Yaş Kadın İzlemleri:[" + bskadinizle.Count + " Adet]";
            else
                tpkadin.Text = "15 - 49 Yaş Kadın İzlemleri";


        }

        public void getanamnez()
        {
            bsanamnez.DataSource = null;
         
            string sql = string.Empty;
            if (Current.AktifMuayeneId > 0)
                sql += " and Anamnez.muayene_Id=" + Current.AktifMuayeneId;

            bsanamnez.DataSource = Transaction.Instance.ExecuteSql(@"Select 
            * from Anamnez where Anamnez.hasta_Id=" + Current.AktifHastaId + sql + " and Aktif=1 ");


            grdanamnez.DataSource = bsanamnez;
            Utility.SetGridStyle(((DevExpress.XtraGrid.Views.Grid.GridView)grdanamnez.Views[0]));

            if (bsanamnez.Count > 0)
                tpanemnez.Text = "Anemnez:[" + bsanamnez.Count + " Adet]";
            else
                tpanemnez.Text = "Anemnez";
        }

        public void getdogum()
        {
            bsdogum.DataSource = null;
       
            string sql = string.Empty;

            bsdogum.DataSource = Transaction.Instance.ExecuteSql(@"Select 
            * from BebekCocukBilgi where BebekCocukBilgi.hasta_Id=" + Current.AktifHastaId + sql + " and Aktif=1 ");

            grddogum.DataSource = bsdogum;
            grdvefat.SetGridStyle(
               @" <Style>
                    <Column Name='Id' HeaderText='Id' Width='45' DisplayIndex='1'   Visible='false'/> 
                    <Column Name='Hasta_Id' HeaderText='Hasta_Id' Width='90' DisplayIndex='2'  Visible='false' />  
                    <Column Name='Muayene_Id' HeaderText='Muayene_Id' Width='50'    DisplayIndex='3'  Visible='false'/>                  
                    <Column Name='Randevu_Id Tarih' HeaderText='Randevu_Id' Width='80' DisplayIndex='4'   Visible='false'/>
                    <Column Name='Doktor_Id' HeaderText='Doktor_Id' Width='80' DisplayIndex='8'   Visible='false'/>
                    <Column Name='IzlemTarihi' HeaderText='IzlemTarihi' Width='80'   DisplayIndex='5' Visible='True' />
                    <Column Name='DogumTarihi' HeaderText='Doğum Tarihi' Width='100'  DisplayIndex='6'  Visible='True'/>                         
                    <Column Name='DogumSirasi' HeaderText='Doğum Sırası' Width='100'   DisplayIndex='7'  Visible='True'/>
                    <Column Name='GebelikNo' HeaderText='Gebelik No' Width='150'  DisplayIndex='9'  Visible='True'/>
                    </Style>");

            if (bsdogum.Count > 0)
                tpdogumbildirimi.Text = "Doğum Bildirimi:[" + bsdogum.Count + " Adet]";
            else
                tpdogumbildirimi.Text = "Doğum Bildirimi";
        }

        public void getvefat()
        {
            bsvefat.DataSource = null;
      

           

            bsvefat.DataSource = Transaction.Instance.ExecuteSql(@" Select
                *,Teshis.Adi 
            from OlumBildirimi 
            inner join Teshis on Teshis.Id=OlumBildirimi.Teshis1_Id
            where 
            OlumBildirimi.Hasta_Id=" + Current.AktifHastaId + " and OlumBildirimi.Aktif=1 ");

            grdvefat.DataSource = bsvefat;
            //Utility.SetGridStyle(((DevExpress.XtraGrid.Views.Grid.GridView)grdvefat.Views[0]));
            grdvefat.SetGridStyle(
                @" <Style>
                    <Column Name='Id' HeaderText='Id' Width='45' DisplayIndex='1'   Visible='false'/> 
                    <Column Name='Hasta_Id' HeaderText='Hasta_Id' Width='90' DisplayIndex='2'  Visible='false' />  
                    <Column Name='Muayene_Id' HeaderText='Muayene_Id' Width='50'    DisplayIndex='3'  Visible='false'/>                  
                    <Column Name='Randevu_Id Tarih' HeaderText='Randevu_Id' Width='80' DisplayIndex='4'   Visible='false'/>
                    <Column Name='Doktor_Id' HeaderText='Doktor_Id' Width='80' DisplayIndex='8'   Visible='false'/>
                    <Column Name='IzlemTarihi' HeaderText='IzlemTarihi' Width='80'   DisplayIndex='5' Visible='True' />
                    <Column Name='Adi' HeaderText='Temel Ölüm Nedeni' Width='150'  DisplayIndex='6'  Visible='True'/>                         
                    <Column Name='OlumTarihi' HeaderText='Ölum Tarihi' Width='90'   DisplayIndex='7'  Visible='True'/>
                    <Column Name='OlumYeri' HeaderText='Ölum Yeri' Width='150'  DisplayIndex='9'  Visible='True'/>
                    </Style>");

            if (bsvefat.Count > 0)
                tpolumbildirimi.Text = "Vefat Bildirimi:[" + bsvefat.Count + " Adet]";
            else
                tpolumbildirimi.Text = "Vefat Bildirimi";
        }

        public void getrecete()
        {
            bsrecete.DataSource = null;
           
            string sql = string.Empty;
            if (Current.AktifMuayeneId > 0)
                sql += " and Recete.muayene_Id=" + Current.AktifMuayeneId;


            bsrecete.DataSource = Transaction.Instance.ExecuteSql(@"Select 
             Recete.Id
            ,Recete.Muayene_Id
            ,(Select Adi from ilac where ilac.Id=Receteilac.Ilac_Id) as IlacAdi
            ,Receteilac.Dozaj
            ,Receteilac.Adet
            ,Receteilac.KullanimSekli
            ,Receteilac.KullanimPeriyot
            ,receteilac.TransferDurumu
            from Recete 
            inner join Receteilac on Receteilac.Recete_Id=Recete.Id and Receteilac.Aktif=1
            where Recete.Hasta_Id=" + Current.AktifHastaId + sql + " and Recete.Aktif=1");
            grdrecete.DataSource = bsrecete;
            Utility.SetGridStyle(((DevExpress.XtraGrid.Views.Grid.GridView)grdrecete.Views[0]));


            if (bsrecete.Count > 0)
                tpreceteler.Text = "Reçeteler:[" + bsrecete.Count + " Adet]";
            else
                tpreceteler.Text = "Reçeteler";

        }
        
        public void gettani()
        {
            bstani.DataSource = null;
         

            string sql = string.Empty;
            if (Current.AktifMuayeneId > 0)
                sql += " and  mt.muayene_Id=" + Current.AktifMuayeneId;

            bstani.DataSource = Transaction.Instance.ExecuteSql(
                    "select mt.Id, t.Kodu,t.Adi Tani " +
                    " from MuayeneTeshis mt" +
                    " join Teshis t on t.Id=mt.teshis_Id" +
                    " where  mt.Hasta_Id = " + Current.AktifHastaId + " and mt.Kronikmi=0 and mt.Alerjikmi=0 and mt.Aktif=1 " + sql +
                    " order by t.Id desc"
                    );
            grdtani.DataSource = bstani;
            Utility.SetGridStyle(((DevExpress.XtraGrid.Views.Grid.GridView)grdtani.Views[0]));
            gridViewtani.Columns["Tani"].Width = 800;
            gridViewtani.Columns["Kodu"].Width = 80;

            if (bstani.Count > 0)
                tptanilar.Text = "Tanılar:[" + bstani.Count + " Adet]";
            else
                tptanilar.Text = "Tanılar:";
        }

        public void getraporlar()
        {
            bsraporlar.DataSource = null;
            //if (xtraTabControlMuayene.SelectedTabPage == xtraTabPageBugunkuMuayeneler)
            //{
            //    if (bsbugunkumuayene.Count == 0)
            //        return;
            //}
            //else
            //{
            //    if (bsgecmismuayene.Count == 0)
            //        return;
            //}

            //if (Current.AktifMuayene == null)
            //    return;

            string sql = string.Empty;
            if (Current.AktifMuayeneId > 0)
                sql += " and  S.muayene_Id=" + Current.AktifMuayeneId;

            bsraporlar.DataSource = Transaction.Instance.ExecuteSql(
                    @"Select 
                        Id,
                        RaporTuru,
                        replace(GunSayisi,'.000','.0') as GunSayisi,
                        RaporBasTarih
                     from SaglikIstirahat S
                     where  S.Hasta_Id = " + Current.AktifHastaId + " and S.Aktif=1 " + sql +
                    "\norder by S.Id"
                    );
            gridRaporlar.DataSource = bsraporlar;
            Utility.SetGridStyle(((DevExpress.XtraGrid.Views.Grid.GridView)gridRaporlar.Views[0]));


            if (bsraporlar.Count > 0)
                tpraporlar.Text = "Raporlar:[" + bsraporlar.Count + " Adet]";
            else
                tpraporlar.Text = "Raporlar:";
        }
        
        public void gettetkik()
        {
            bstetkik.DataSource = null;
            string sql = string.Empty;
            if (Current.AktifMuayeneId > 0)
                sql += " and  mt.muayene_Id=" + Current.AktifMuayeneId;


            bstetkik.DataSource = Transaction.Instance.ExecuteSql(
                    "select mt.Barkod,t.Kodu,t.Adi TetkikAdi,mt.AileHekimiAciklama,mt.TetkikAciklama,"+
                    " mt.GidisTarihi,mt.DonusTarihi,"+
                    " 'İstem'= case mt.transferdurumu when 10 then 'Gönderildi' when 11 then 'Başarısız Gönderim' when 12 then 'Hata Alındı' else '' end," +
                    " Sonuc"+
                    " from MuayeneTetkik mt" +
                    " join Tetkik t on t.Id=mt.tetkik_Id" +
                    " where mt.Aktif=1 and mt.hasta_Id= " + Current.AktifHastaId + sql +
                    " order by t.Id desc"
                    );
            grdtetkik.DataSource = bstetkik;
            Utility.SetGridStyle(((DevExpress.XtraGrid.Views.Grid.GridView)grdtetkik.Views[0]));


            if (bstetkik.Count > 0)
                tptetkikler.Text = "Tetkikler:[" + bstetkik.Count + " Adet]";
            else
                tptetkikler.Text = "Tetkikler:";
        }

        public void getsevk()
        {
            bssevk.DataSource = null;
            string sql = string.Empty;
            if (Current.AktifMuayeneId > 0)
                sql += " and  s.muayene_Id=" + Current.AktifMuayeneId;

            bssevk.DataSource = Transaction.Instance.ExecuteSql(
                     "select s.Id, s.SevkTarihi,s.SevkNedeni,k.Adi SevKurumu,b.Adi SevkBolumu" +
                     "\nfrom MuayeneSevk s" +
                     "\njoin SevkBolum b on b.Id=s.SevkBolum_Id" +
                     "\njoin SevkKurum k on k.Id=s.SevkKurum_Id" +
                     "\nwhere s.Aktif=1 and s.Hasta_Id= " + Current.AktifHastaId + sql +
                     "\norder by s.Id desc"
                     );
            grdsevk.DataSource = bssevk;
            Utility.SetGridStyle(((DevExpress.XtraGrid.Views.Grid.GridView)grdsevk.Views[0]));



            if (bssevk.Count > 0)
                tpsevkler.Text = "Sevkler:[" + bssevk.Count + " Adet]";
            else
                tpsevkler.Text = "Sevkler:";
        }

        public void getasi()
        {
            string sql = @"select 
            (Select at.adi from AsiTanim at where at.Id=MAsi.AsiTanim_Id) [Aşı Adı],
            MAsi.AsiSiraSi as [Sıra No],
            Ts.PlanlananTarih,
            MAsi.PlanlananTarih as [Yapıldığı Tarih],
            Ts.Durum
            from   MuayeneAsi MAsi
            left join TakvimSatiri Ts  on MAsi.AsiTanim_Id=Ts.Asi_Id and MAsi.AsiOzellikTanimId=Ts.AsiOzellikTanimId 
            where MAsi.Aktif=1 
            and MAsi.Hasta_Id=" + Current.AktifHastaId + " order by MAsi.Id";
            bsasi.DataSource = Transaction.Instance.ExecuteSql(sql);
            //bsasi.DataSource = Transaction.Instance.ExecuteSql(
            //         "select a.PlanlananTarih,at.Adi AsiAdi" +
            //         "\nfrom MuayeneAsi a" +
            //         "\n inner join AsiTanim at on at.Id=a.asitanim_Id" +
            //         "\nwhere a.Aktif=1 and a.Hasta_Id=" + Current.AktifHastaId + sql +
            //         "\norder by a.Id"
            //         );
            grdasi.DataSource = bsasi;
            Utility.SetGridStyle(((DevExpress.XtraGrid.Views.Grid.GridView)grdasi.Views[0]));

            if (bsasi.Count > 0)
                tpasilar.Text = "Aşılar:[" + bsasi.Count + " Adet]";
            else
                tpasilar.Text = "Aşılar:";
        }

        public void gethastalik()
        {
            bshastalik.DataSource = null;
            if (bshasta.Count == 0)
                return;
            bshastalik.DataSource = Transaction.Instance.ExecuteSql(
                    "select h.Id,t.Kodu,t.Adi Hastalik,h.Kronikmi Kronik,h.Alerjikmi Alerjik" +
                    "\nfrom MuayeneTeshis h join teshis t on t.Id=h.teshis_Id join muayene m on m.Id=h.muayene_Id" +
                    "\nwhere h.Aktif=1 and m.hasta_Id= " + Current.AktifHastaId + " and (h.Kronikmi=1 or h.Alerjikmi=1)" +
                    "\norder by h.Id desc"
                    );
            grdhastalik.DataSource = bshastalik;
            Utility.SetGridStyle(((DevExpress.XtraGrid.Views.Grid.GridView)grdhastalik.Views[0]));

            if (bshastalik.Count > 0)
                tphastaliklar.Text = "Hastalıklar:[" + bshastalik.Count + " Adet]";
            else
                tphastaliklar.Text = "Hastalıklar:";
        }

        public void getbugunkumuayene()
        {
            bsbugunkumuayene.DataSource = null;
            if (bshasta.Count == 0)
                return;
            Current.AktifHasta = this.AktifHasta;
            Current.AktifRandevu = this.AktifRandevu;

            bsbugunkumuayene.DataSource = Transaction.Instance.ExecuteSql(
                "select m.Id, m.hasta_Id HastaNo,m.Id MuayeneNo,m.MuayeneTarihi Tarih,d.Adi+' '+d.Soyadi Doktoru,MuayeneKapalimi  " +
                "\n,m.SiraNo,m.ProtokolNo,m.MuayeneDurumu,isnull((select SiraNo from Takvim where Takvim.Id=m.Randevu_Id and Takvim.Aktif=1),'') as randevusirano " +
                "\nfrom Muayene m" +
                "\njoin Doktor d on d.Id=m.doktor_Id" +
                "\nwhere m.Aktif=1 and  m.hasta_Id= " + Current.AktifHastaId + " and MuayeneTarihi>='" + System.DateTime.Today.ToString("yyyyMMdd") + "'" +
                "\norder by m.MuayeneTarihi desc"
                );

            grdBugunkumuayene.DataSource = bsbugunkumuayene;

            //if (bsbugunkumuayene.Count > 0)
            //{
            bsbugunkumuayene.MoveFirst();
            //}
            Current.AktifMuayene = this.AktifMuayene;

            if (bsbugunkumuayene.Count > 0)
                xtraTabPageBugunkuMuayeneler.Text = "Bugünkü Muayeneler:[" + bsbugunkumuayene.Count + " Adet]";
            else
                xtraTabPageBugunkuMuayeneler.Text = "Bugünkü Muayeneler:";
            //sethastaadi();
        }

        public void getgecmismuayene()
        {
            bsgecmismuayene.DataSource = null;
            if (bshasta.Count == 0)
                return;
            Current.AktifHasta = this.AktifHasta;
            Current.AktifRandevu = this.AktifRandevu;

            bsgecmismuayene.DataSource = Transaction.Instance.ExecuteSql(
                "set dateformat dmy; select m.Id, m.hasta_Id HastaNo,m.Id MuayeneNo,m.MuayeneTarihi Tarih,d.Adi+' '+d.Soyadi Doktoru,MuayeneKapalimi  " +
                "\n,m.SiraNo,m.ProtokolNo,m.MuayeneDurumu,m.MuayeneTarihi " +
                "\nfrom Muayene m" +
                "\njoin Doktor d on d.Id=m.doktor_Id " +
                "\nwhere m.hasta_Id= " + Current.AktifHastaId + " and MuayeneTarihi<'" + System.DateTime.Today.ToString("yyyyMMdd") + "' and m.Aktif=1  " +
                "\norder by m.MuayeneTarihi desc"
                );
            gridGecmisMuayene.DataSource = bsgecmismuayene;
            //gridGecmisMuayene.RefreshDataSource();
            //if (bsgecmismuayene.Count > 0)
            //{
            bsgecmismuayene.MoveFirst();
            //}
            Current.AktifMuayene = this.AktifMuayene;



            if (bsgecmismuayene.Count > 0)
                xtraTabPaGecmisMuayene.Text = "Geçmiş Muayeneler:[" + bsgecmismuayene.Count + " Adet]";
            else
                xtraTabPaGecmisMuayene.Text = "Geçmiş Muayeneler:[" + bsgecmismuayene.Count + " Adet]";
            //sethastaadi();
        }

        public void gethasta(string sql)
        {
            bshasta.DataSource = null;
            DataTable dt = SharpBullet.OAL.Transaction.Instance.ExecuteSql(sql);
            dt.AcceptChanges();
            bshasta.DataSource = dt;
            grdhasta.DataSource = bshasta;


            if (radioButtontumHastalar.Checked)
            {
                grdhasta.SetGridStyle(
                  @" <Style>
                        <Column Name='KayitDurumu' HeaderText='K.Durumu' Width='65' DisplayIndex='1'  />                    
                        <Column Name='TckNo' HeaderText='Tc Kimlik No' Width='65' DisplayIndex='2'  />
                        <Column Name='PasaportNo' HeaderText='Pas.No' Width='65' DisplayIndex='3'  />
                        <Column Name='AdiSoyadi' HeaderText='Adı Soyadı' Width='150' DisplayIndex='4' />
                        <Column Name='Cinsiyeti' HeaderText='Cinsiyeti' Width='50' DisplayIndex='5' />
                   
                     </Style>");
            }
            else
                if (radioButtonRandevuluHastalar.Checked)
                {
                    grdhasta.SetGridStyle(
                 @" <Style>
                        <Column Name='SiraNo' HeaderText='Sıra' Width='40' DisplayIndex='1'  />
                        <Column Name='Saat' HeaderText='R.Saati' Width='70' DisplayIndex='2'  />
                        <Column Name='BasTarih' HeaderText='R.Tarihi' Width='70' DisplayIndex='4' />
                        <Column Name='TckNo' HeaderText='Tc K.No' Width='55' DisplayIndex='5'  />
                        <Column Name='AdiSoyadi' HeaderText='Adı Soyadı' Width='100' DisplayIndex='6' />
                        <Column Name='KayitDurumu' HeaderText='K.Durumu' Width='63' DisplayIndex='7'  />
                        <Column Name='IslemTuru' HeaderText='IslemTuru' Width='75' DisplayIndex='8' Visible='False' />
                        <Column Name='Aciklama' HeaderText='Aciklama' Width='150' DisplayIndex='9'  />

                     </Style>");
                }


            //bshasta.MoveFirst();
            etiket = "Hasta";
            setbuttons(bshasta);
            sethastaadi();

        }





        public void gethizmet()
        {
            bshizmet.DataSource = null;
            //if (bshasta.Count == 0)
            //    return;

            //if (Current.AktifMuayene== null)
            //    return;
            string sql = string.Empty;
            if (Current.AktifMuayeneId > 0)
                sql += " and  mhizmet.muayene_Id=" + Current.AktifMuayeneId;

            bshizmet.DataSource = Transaction.Instance.ExecuteSql(@"
            SELECT 
                mhizmet.Id
                ,Hizmet.Kodu
                ,Hizmet.Adi
                ,(select Adi from HizmetTur where HizmetTur.Id=Hizmet.HizmetTur_Id) as HizmetTuru
                ,Hizmet.Puani
                ,Hizmet.Ucreti
               
            FROM MuayeneHizmet mhizmet 
            INNER JOIN Hizmet  on Hizmet.Id=mhizmet.Hizmet_Id 
            WHERE 
            mhizmet.Hasta_Id=" + Current.AktifHastaId + sql + " AND mhizmet.Aktif=1 ORDER BY mhizmet.Id desc");


            gridhizmet.DataSource = bshizmet;
            Utility.SetGridStyle(((DevExpress.XtraGrid.Views.Grid.GridView)gridhizmet.Views[0]));
            gridhizmet.SetGridStyle(
    @" <Style>
                        <Column Name='Kodu' HeaderText='Kodu' Width='75' DisplayIndex='1'  />                    
                        <Column Name='Adi' HeaderText='Adı' Width='150' DisplayIndex='2'  />
                        <Column Name='HizmetTuru' HeaderText='Türü' Width='100' DisplayIndex='3'  />
                        <Column Name='Puani' HeaderText='Puanı' Width='75' DisplayIndex='4' />
                        <Column Name='Ucreti' HeaderText='Ücreti' Width='75' DisplayIndex='5' />
                   
                     </Style>");



            if (bshizmet.Count > 0)
                tphizmetler.Text = "Hizmetler:[" + bshizmet.Count + " Adet]";
            else
                tphizmetler.Text = "Hizmetler:";
        }
        #endregion get sqls

        #region current changes

        void xtraTabControlizlemler_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            SetEskiTabBaslikRengi(e.PrevPage, e.Page);
            SetSeciliTabBaslikRengi(e.Page);
            if (xtraTabControlizlemler.SelectedTabPage == tpdogumbildirimi)
            {
                etiket = "Doğum Bildirimi";
                setbuttons(bsdogum);
            }
            else
                if (xtraTabControlizlemler.SelectedTabPage == tpolumbildirimi)
                {
                    etiket = "Vefat Bildirimi";
                    setbuttons(bsvefat);
                }
        }

        void xtraTabControlgebe_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            SetEskiTabBaslikRengi(e.PrevPage, e.Page);
            SetSeciliTabBaslikRengi(e.Page);
            if (xtraTabControlgebe.SelectedTabPage == tpgebebaslangic)
            {
                etiket = "Gebe Başlangıç";
                setbuttons(bsgebebaslangic);
            }
            else
                if (xtraTabControlgebe.SelectedTabPage == tpgebesonuc)
                {
                    etiket = "Gebe Sonuç";
                    setbuttons(bsgebesonuc);
                }
                else
                    if (xtraTabControlgebe.SelectedTabPage == tbgebeizlem)
                    {
                        etiket = "Gebe İzlem";
                        setbuttons(bsgebeizle);
                    }
        }

        void xtraTabControldetay_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            SetEskiTabBaslikRengi(e.PrevPage, e.Page);
            SetSeciliTabBaslikRengi(e.Page);
            if (xtraTabControldetay.SelectedTabPage == tphastaliklar)
            {
                etiket = "Hastalık";
                setbuttons(bshastalik);
            }
            else
            {
                if (xtraTabControldetay.SelectedTabPage == tpasilar)
                {
                    etiket = "Aşı";
                    setbuttons(bsasi);
                }
                else
                    if (xtraTabControldetay.SelectedTabPage == tpanemnez)
                    {
                        etiket = "Anamnez";
                        setbuttons(bsanamnez);
                    }
                    else
                        if (xtraTabControldetay.SelectedTabPage == tpreceteler)
                        {
                            etiket = "Reçete";
                            setbuttons(bsrecete);
                        }
                        else
                            if (xtraTabControldetay.SelectedTabPage == tpsevkler)
                            {
                                etiket = "Sevk";
                                setbuttons(bssevk);
                            }
                            else
                                if (xtraTabControldetay.SelectedTabPage == tptanilar)
                                {
                                    etiket = "Tanı";
                                    setbuttons(bstani);
                                }
                                else
                                    if (xtraTabControldetay.SelectedTabPage == tptetkikler)
                                    {
                                        etiket = "Tetkik";
                                        setbuttons(bstetkik);
                                    }
                                    else
                                        if (xtraTabControldetay.SelectedTabPage == tphizmetler)
                                        {
                                            etiket = "Hizmet";
                                            setbuttons(bshizmet);
                                        }
            }
        }

        private void xtraTabControlMuayene_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            SetEskiTabBaslikRengi(e.PrevPage, e.Page);
            SetSeciliTabBaslikRengi(e.Page);
            if (xtraTabControlMuayene.SelectedTabPage == xtraTabPageBugunkuMuayeneler)
            {
                getbugunkumuayene();
            }
            else
                getgecmismuayene();

            //Sonradan eklendi
            if (Current.AktifMuayene == null)
            {
                getrecete();
                gettani();
                gettetkik();
                getsevk();
                getasi();
                getanamnez();
                getkadinizlem();
                getgebeizle();
                getgebebaslangic();
                getgebesonuc();
                getlohusaizle();
                getbebekcocukizle();
                getbebekbeslenizle();
                getobeziteizlem();
                getdogum();
                gethizmet();
                gethastalik();
                getraporlar();
            }
            //Sonradan eklendi
        }

        void bsmuayene_CurrentItemChanged(object sender, EventArgs e)
        {
            Current.AktifMuayene = this.AktifMuayene;
            getrecete();
            gettani();
            gettetkik();
            getsevk();
            getasi();
            getanamnez();
            getkadinizlem();
            getgebeizle();
            getgebebaslangic();
            getgebesonuc();
            getlohusaizle();
            getbebekcocukizle();
            getbebekbeslenizle();
            getobeziteizlem();
            getdogum();
            gethizmet();
            gethastalik();
            getraporlar();
        }


        private void bshasta_CurrentItemChanged(object sender, EventArgs e)
        {
            //TODO:En Önemli soru hanig hareketlerde muayene olmaz ise olmaz çok  önemli
            getbugunkumuayene();
            getgecmismuayene();
            gethastalik();
            getvefat();
            sethastaadi();


            if (Current.AktifMuayene == null)
            {
                getrecete();
                gettani();
                gettetkik();
                getsevk();
                getasi();
                getanamnez();
                getkadinizlem();
                getgebeizle();
                getgebebaslangic();
                getgebesonuc();
                getlohusaizle();
                getbebekcocukizle();
                getbebekbeslenizle();
                getobeziteizlem();
                getdogum();
                gethizmet();
                gethastalik();
                getraporlar();
            }
        }

        #endregion current changes

        #region btngetir
        private void btngetir_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Current.AktifHasta = null;
            HastaGetir();

            if (radioButtonRandevuluHastalar.Checked)
            {
                Current.AktifRandevu = this.AktifRandevu;
            }
            else
                Current.AktifRandevu = new Takvim();
            Cursor.Current = Cursors.Default;
        }

        public void HastaGetir()
        {
            string strsql = "";
            if (!edtkayitdurumtum.Checked &&
                !(edtkayitli.Checked && edtmisafir.Checked) &&
                !(!edtkayitli.Checked && !edtmisafir.Checked) &&
                (edtkayitli.Checked || edtmisafir.Checked))
            {
                strsql += "\n and KayitDurumu in (";
                if (edtkayitli.Checked)
                    strsql += "'" + myenum.KayitDurumu.Kayitli.ToString() + "'" + ",";
                if (edtmisafir.Checked)
                    strsql += "'" + myenum.KayitDurumu.Misafir.ToString() + "'" + ",";

                strsql = strsql.Remove(strsql.Length - 1, 1);
                strsql += ")";

            }



            if (rbobez.Checked)
                strsql += "\n and Obezmi=1";
            else
                if (rbobezdegil.Checked)
                    strsql += "\n and Obezmi=0";

            if (!edtmuayenedurumutumu.Checked &&
                !(edtmuayeneedilen.Checked && edtbekleyen.Checked && edtsevkedilen.Checked && edttahlil.Checked) &&
                !(!edtmuayeneedilen.Checked && !edtbekleyen.Checked && !edtsevkedilen.Checked && !edttahlil.Checked) &&
                (edtmuayeneedilen.Checked || edtbekleyen.Checked || edtsevkedilen.Checked || edttahlil.Checked)
                )
            {
                strsql += "\n and exists (select m.Id from muayene m where m.aktif=1 and m.MuayeneDurumu in (";
                if (edtmuayeneedilen.Checked)
                    strsql += "'" + myenum.MuayeneDurumu.MuayeneEdildi + "'" + ",";
                if (edtbekleyen.Checked)
                    strsql += "'" + myenum.MuayeneDurumu.Bekliyor + "'" + ",";
                if (edtsevkedilen.Checked)
                    strsql += "'" + myenum.MuayeneDurumu.SevkEdildi + "'" + ",";
                if (edttahlil.Checked)
                    strsql += "'" + myenum.MuayeneDurumu.TahlilBekleniyor + "'" + ",";


                strsql = strsql.Remove(strsql.Length - 1, 1);
                strsql += "))";
            }
            if (edtara.Text.Length > 0)
                if (edtara.Text.Contains("1") ||
                    edtara.Text.Contains("2") ||
                    edtara.Text.Contains("3") ||
                    edtara.Text.Contains("4") ||
                    edtara.Text.Contains("5") ||
                    edtara.Text.Contains("6") ||
                    edtara.Text.Contains("7") ||
                    edtara.Text.Contains("8") ||
                    edtara.Text.Contains("9") ||
                    edtara.Text.Contains("0"))
                    strsql += "\n and ltrim((h.tckno)) like '" + edtara.Text + "%'";
                else
                    strsql += "\n and (h.adi+' '+h.soyadi) like '%" + edtara.Text + "%'";

            string sql = @"Select  
                                h.Id,
                                h.Id HastaNo,
                                h.TckNo,
                                h.PasaportNo,
                                h.Adi+' '+h.Soyadi AdiSoyadi,
                                h.DogumTarihi,
                                h.Cinsiyeti,
                                h.KayitDurumu 
                           From Hasta h
                           Where 1=1 " + strsql;
            if (Current.AktifDoktorId > 0)
            {
                sql += @"  and (h.Doktor_Id=" + Current.AktifDoktorId + @" or h.Doktor_Id in(
                    Select 
                         DoktorVekalet.VerenDoktor_Id
                    from DoktorVekalet  
                    where 
                    DoktorVekalet.AlanDoktor_Id=" + Current.AktifDoktorId
                 + " and BaslangicTarihi<='" + System.DateTime.Today.ToString("yyyyMMdd") + "' and BitisTarihi>='" + System.DateTime.Today.ToString("yyyyMMdd") + "'))";

            }
            sql += " order by h.Adi,h.Soyadi";

            if (radioButtontumHastalar.Checked)
            {
                sql = @"   Select  
                                h.Id,
                                h.Id HastaNo,
                                h.TckNo,
                                h.PasaportNo,
                                h.Adi+' '+h.Soyadi AdiSoyadi,
                                h.DogumTarihi,
                                h.Cinsiyeti,
                                h.KayitDurumu 
                           From Hasta h
                           Where 1=1 " + strsql;

                if (Current.AktifDoktorId > 0)
                {

                    sql += @"   and (h.Doktor_Id=" + Current.AktifDoktorId + @" or h.Doktor_Id in(

                    Select 
                         DoktorVekalet.VerenDoktor_Id
                    from DoktorVekalet  
                    where 
                    DoktorVekalet.AlanDoktor_Id=" + Current.AktifDoktorId
                   + " and BaslangicTarihi<='" + System.DateTime.Today.ToString("yyyyMMdd") + "' and BitisTarihi>='" + System.DateTime.Today.ToString("yyyyMMdd") + "'))";
                }
                sql += " order by h.Adi,h.Soyadi";
            }
            else
                if (radioButtonRandevuluHastalar.Checked)
                {

                    if (DateEditBasTarih.DateTime != System.DateTime.MinValue && dateEditBitTar.DateTime != System.DateTime.MinValue)
                    {
                        strsql += " and  Takvim.BasTarih between '" + DateEditBasTarih.DateTime.ToString("yyyyMMdd") + "' and '" + dateEditBitTar.DateTime.ToString("yyyyMMdd") + "'";
                    }
                    sql = @"   Select  
                                    max(h.Id) as Id,
                                     max(h.Id ) as HastaNo,
                                     max(h.TckNo) as TckNo,
                                     max(h.PasaportNo) PasaportNo,
                                     max(h.Adi+' '+h.Soyadi) AdiSoyadi,
                                     max(h.DogumTarihi) as DogumTarihi,
                                     max(h.Cinsiyeti)as Cinsiyeti,
                                     max(h.KayitDurumu) as KayitDurumu,
                                     max(Takvim.Saat) as Saat,
                                     max(Takvim.SiraNo) as SiraNo,
                                     max(Takvim.BasTarih) as BasTarih,
                                     (Select max(Muayene.MuayeneDurumu) from Muayene where Hasta_Id=h.Id and Muayene.MuayeneTarihi=Takvim.BasTarih 
                                     and Muayene.Aktif=1 and Takvim.Id=Muayene.Randevu_Id) as MuayeneDurumu,
                                     max(Takvim.Id) as TakvimId,
                                     max(dbo.FN_GETISLEMTURU(Takvim.Id)) as IslemTuru,
                                     max(Takvim.Aciklama) as Aciklama     
                               From Hasta h
                               Inner join Takvim on Takvim.Hasta_Id=h.Id and Takvim.RandevuDurumu!='İptalEdildi' and  Takvim.Aktif=1
                               Where 1=1 " + strsql;
                    if (Current.AktifDoktorId > 0)
                    {
                        sql += @" and (h.Doktor_Id=" + Current.AktifDoktorId + @" or h.Doktor_Id in(

                                SELECT 
                                    DoktorVekalet.VerenDoktor_Id
                                FROM DoktorVekalet  
                                WHERE 
                                DoktorVekalet.AlanDoktor_Id=" + Current.AktifDoktorId +
                                 @" and BaslangicTarihi<='" + DateEditBasTarih.DateTime.ToString("yyyyMMdd")
                                 + "' and BitisTarihi>='" + dateEditBitTar.DateTime.ToString("yyyyMMdd") + @"' )) ";
                    }

                    if (radioButtonIzlemGrubu.Checked)
                    {
                        sql += @" and Exists(	Select Id 
										    From Takvimsatiri 
										    where Takvimsatiri.Takvim_Id=Takvim.Id 
										    and Takvimsatiri.aktif=1 
										    and Takvimsatiri.IslemTuru in ('" + myenum.IslemTuru.Asi.ToString() + "','" + myenum.IslemTuru.Izlem.ToString() + "'))";
                    }
                    else
                        if (radioButtonMuayeneGrubu.Checked)
                        {
                            sql += @" and Exists(	Select Id 
										    From Takvimsatiri 
										    where Takvimsatiri.Takvim_Id=Takvim.Id 
										    and Takvimsatiri.aktif=1 
										    and Takvimsatiri.IslemTuru in ('" + myenum.IslemTuru.Muayene.ToString() + "'))"; 

                        }
                        else
                        if(radioButtonHepsi.Checked)
                        {
                            sql += @" and Exists(	Select Id 
										    From Takvimsatiri 
										    where Takvimsatiri.Takvim_Id=Takvim.Id 
										    and Takvimsatiri.aktif=1 
										    and Takvimsatiri.IslemTuru in ('" + myenum.IslemTuru.Asi.ToString() + "','" + myenum.IslemTuru.Izlem.ToString() + "','" + myenum.IslemTuru.Muayene.ToString() + "'))";
                        }



                    sql += "  Group by Takvim.Id,h.Id,Takvim.BasTarih,Takvim.SiraNo,h.Adi,h.Soyadi  order by Takvim.BasTarih,Takvim.SiraNo,h.Adi,h.Soyadi";
                }
            gethasta(sql);

        }

        private void edtara_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btngetir_Click(btngetir, null);
        }
        #endregion btngetir

        #region checked changes
        private void edtkayitdurumtum_CheckedChanged(object sender, EventArgs e)
        {
            edtkayitli.Enabled = !edtkayitdurumtum.Checked;
            edtmisafir.Enabled = !edtkayitdurumtum.Checked;
        }

        private void edtmuayenedurumutumu_CheckedChanged(object sender, EventArgs e)
        {
            edtbekleyen.Enabled = !edtmuayenedurumutumu.Checked;
            edtmuayeneedilen.Enabled = !edtmuayenedurumutumu.Checked;

            edtsevkedilen.Enabled = !edtmuayenedurumutumu.Checked;
            edttahlil.Enabled = !edtmuayenedurumutumu.Checked;
        }

        private void edthastaturutumu_CheckedChanged(object sender, EventArgs e)
        {

        }
        #endregion checked changes


        private void radioButtontumHastalar_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonRandevuluHastalar.Checked)
            {
                groupBoxRandevu.Enabled = true;
            }
            else
                if (radioButtontumHastalar.Checked)
                {
                    groupBoxRandevu.Enabled = false;
                }
        }

        private void gridViewHasta_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            DataRow row = view.GetDataRow(e.RowHandle);


            if (row != null)
            {
                object muayenedurumu;
                try
                {
                    muayenedurumu = row["MuayeneDurumu"];
                }
                catch (Exception)
                {

                    muayenedurumu = null;
                }

                if (muayenedurumu != null && muayenedurumu != System.DBNull.Value)
                {

                    myenum.MuayeneDurumu durum = (myenum.MuayeneDurumu)Enum.Parse(typeof(myenum.MuayeneDurumu), muayenedurumu.ToString());

                    switch (durum)
                    {
                        case myenum.MuayeneDurumu.Bekliyor:
                            e.Appearance.BackColor = System.Drawing.Color.White;
                            break;
                        case myenum.MuayeneDurumu.MuayeneEdildi:
                            e.Appearance.BackColor = System.Drawing.Color.LawnGreen;
                            break;
                        case myenum.MuayeneDurumu.SevkEdildi:
                        case myenum.MuayeneDurumu.TahlilBekleniyor:

                            e.Appearance.BackColor = System.Drawing.Color.Yellow;
                            break;
                        default:
                            break;


                    }
                }
            }

        }

        void gridViewHasta_ShowingEditor(object sender, CancelEventArgs e)
        {

        }

        private void radioButtonMuayeneGrubu_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonMuayeneGrubu.Checked)
            {
                groupControlizlemler.Visible = false;
                panelControldetaylar.Visible = true;
            }
            HastaGetir();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButtonIzlemGrubu.Checked)
            {
                groupControlizlemler.Visible = true;
                panelControldetaylar.Visible = false;
            }

            HastaGetir();
            
        }

        private void radioButtonHepsi_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonHepsi.Checked)
            {

                if (groupControlizlemler.Visible)
                {
                    groupControlizlemler.Width = groupControlizlemler.Width / 2;
                    panelControldetaylar.Width = groupControlizlemler.Width / 2;
                }
                else
                {
                    groupControlizlemler.Width = panelControldetaylar.Width / 2;
                    panelControldetaylar.Width = panelControldetaylar.Width / 2;
                }
                groupControlizlemler.Dock = DockStyle.Right;
                panelControldetaylar.Dock = DockStyle.Fill;
                groupControlizlemler.SendToBack();
                groupControlizlemler.Visible = true;
                panelControldetaylar.Visible = true;
            }
            else
            {
                groupControlizlemler.Dock = DockStyle.Fill;
                panelControldetaylar.Dock = DockStyle.Fill;
            }
            HastaGetir();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            SagTusMenuGorunumAyarla();
        }

        private void SagTusMenuGorunumAyarla()
        {
            bool aktifmuyanevar = false;

            if (Current.AktifMuayeneId > 0)
            {
                aktifmuyanevar = true;
            }

            if (Current.AktifHastaId > 0)
            {
                foreach (ToolStripMenuItem Menu in contextMenuStrip1.Items)
                {
                    Menu.Visible = true;

                    if (Menu.Name == "MenuItemMuayeneGrubu" || Menu.Name == "MenuItemIzlemGrubu")
                    {
                        Menu.Click -= new EventHandler(Menu_Click);
                        Menu.Click += new EventHandler(Menu_Click);
                    }

                    foreach (ToolStripItem menuitem in Menu.DropDownItems)
                    {

                        menuitem.Visible = true;
                        menuitem.Click -= new EventHandler(menuitem_Click);
                        menuitem.Click += new EventHandler(menuitem_Click);
                        if (aktifmuyanevar)
                        {
                            if (Current.AktifMuayene.MuayeneKapalimi)
                            {
                                if (menuitem.Name != "Muayene" || menuitem.Name.Contains("Izlem") || menuitem.Name != ("Asi") || menuitem.Name != "VefatBİldirimi")
                                    menuitem.Visible = false;
                            }
                        }
                        else
                        {
                            if (menuitem.Name=="frmMuayene" || menuitem.Name.Contains("Izlem")
                                || menuitem.Name.Contains("frmBebekDogumBildirim")
                                || menuitem.Name.Contains("frmOlumBildirimi")
                                || menuitem.Name.Contains("frmGebeBaslangic")
                                || menuitem.Name.Contains("frmGebeSonuc")
                                || menuitem.Name.Contains("frmBebekCocukBeslenme")
                                || menuitem.Name.Contains("frmHasta") || menuitem.Name.Contains("frmMuayeneAsi") || menuitem.Name.Contains("frmMuayeneAsi")) //Muayenesiz aşı girilmesin kodu eklersen muayeneside girilir. 
                                
                            {
                               
                                //if ((menuitem.Name == "MenuItemYenifrmMuayeneAsi" || menuitem.Name =="MenuItemDuzenlefrmMuayeneAsi") && aktifmuyanevar)
                                //{
                                //    menuitem.Visible = true;
                                //}
                                //else
                                //    if ((menuitem.Name == "MenuItemYenifrmMuayeneAsi" || menuitem.Name =="MenuItemDuzenlefrmMuayeneAsi") && !aktifmuyanevar)
                                //    {
                                //        menuitem.Visible = false;
                                //    }
                                //    else
                                        menuitem.Visible = true;
                            }
                            else
                                menuitem.Visible = false;
                        }

                        switch (Current.AktifHasta.Cinsiyeti)
                        {
                            case myenum.Cinsiyet.Erkek:
                                if (menuitem.Name.Contains("frm15_49KadinIzleme")
                                    || menuitem.Name.Contains("frmGebeBaslangic")
                                    || menuitem.Name.Contains("frmGebeIzlem")
                                    || menuitem.Name.Contains("frmGebeSonuc")
                                    || menuitem.Name.Contains("frmLohusaIzlem")
                                    )
                                    menuitem.Visible = false;

                                break;
                            case myenum.Cinsiyet.Kadın:
                                if (menuitem.Name.Contains("frm15_49KadinIzleme")
                                    || menuitem.Name.Contains("frmGebeBaslangic")
                                    || menuitem.Name.Contains("frmGebeIzlem")
                                    || menuitem.Name.Contains("frmGebeSonuc")
                                    || menuitem.Name.Contains("frmLohusaIzlem")
                                    )
                                    menuitem.Visible = true;


                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
            {
                foreach (ToolStripMenuItem Menu in contextMenuStrip1.Items)
                {
                    if (Menu.Name == "MenuItemDuzenle")
                    {
                        Menu.Visible = false;
                    }
                    else
                        if (Menu.Name == "MenuItemMuayeneGrubu" || Menu.Name == "MenuItemIzlemGrubu")
                        {
                            Menu.Click -= new EventHandler(Menu_Click);
                            Menu.Click += new EventHandler(Menu_Click);
                        }

                    foreach (ToolStripItem menuitem in Menu.DropDownItems)
                    {
                        menuitem.Click -= new EventHandler(menuitem_Click);
                        menuitem.Click += new EventHandler(menuitem_Click);
                        if (menuitem.Name != "MenuItemYenifrmHasta")
                            menuitem.Visible = false;
                    }

                }
            }

        }

        void Menu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuitem = ((ToolStripMenuItem)sender);
            if (menuitem.Name == "MenuItemMuayeneGrubu")
                radioButtonMuayeneGrubu.Checked = true;
            else
                if (menuitem.Name == "MenuItemIzlemGrubu")
                    radioButtonIzlemGrubu.Checked = true;

        }

        void menuitem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuitem = ((ToolStripMenuItem)sender);
            YeniForm(menuitem, e);
        }

        private void YeniForm(ToolStripMenuItem menuitem, EventArgs e)
        {

            if (menuitem != null && menuitem.Tag != null && menuitem.OwnerItem.Name == "MenuItemYeni")
            {
                string assemblyname = Assembly.GetEntryAssembly().GetName().Name.ToString();
                try
                {
                    if (menuitem.Tag.ToString() == "frmMuayene")
                    {

                        if (MuayeneValidate())
                            return;
                        Hastaliklar();
                    }
                    else
                        if (menuitem.Tag.ToString() == "frmRecete")
                        {

                            if (ReceteValidate())
                                return;
                        }
                        else
                            if (menuitem.Tag.ToString() == "frmBebekDogumBildirim")
                            {
                                if (DogumBildirimValidate())
                                    return;
                            }
                            else
                                if (menuitem.Tag.ToString() == "frmSaglikIstirahat")
                                {
                                    if (SaglikBildirimValidate())
                                        return;
                                }
                                else
                                    if (menuitem.Tag.ToString() == "frmGebeIzlem")
                                    {
                                        Sonuc sonuc = frmGebeIzlem.GebeIzlemKontrol();
                                        if (sonuc.HataVarMi)
                                        {
                                            MessageBox.Show(sonuc.Mesaj);
                                            return;
                                        }
                                    }
                                    

                    frmDialogBase form = (frmDialogBase)Activator.CreateInstance(assemblyname, assemblyname + "." + menuitem.Tag.ToString()).Unwrap();
                    form.formState = mymodel.myenum.EditMode.emYeni;
                    form.HastaBilgileri(Current.AktifHasta);
                    form.ShowDialog();

                    switch (form.Name)
                    {
                        case "frmHasta":
                            radioButtontumHastalar.Checked = true;
                            HastaGetir();
                            break;
                        case "frmMuayene":
                            xtraTabControlMuayene.SelectedTabPage = xtraTabPageBugunkuMuayeneler;
                            getbugunkumuayene();
                            break;
                        case "frmTaniAta":
                            xtraTabControldetay.SelectedTabPage = tptanilar;
                            gettani();
                            gethastalik();
                            if (xtraTabControlMuayene.SelectedTabPage == xtraTabPageBugunkuMuayeneler)
                                getbugunkumuayene();
                            else
                                getgecmismuayene();
                            break;
                        case "frmRecete":
                            xtraTabControldetay.SelectedTabPage = tpreceteler;
                            getrecete();
                            break;
                        case "frmTetkik":
                            xtraTabControldetay.SelectedTabPage = tptetkikler;
                            gettetkik();
                            break;
                        case "frmAnemnez":
                            xtraTabControldetay.SelectedTabPage = tpanemnez;
                            getanamnez();
                            break;
                        case "frmSevk":
                            xtraTabControldetay.SelectedTabPage = tpsevkler;
                            getsevk();
                            break;
                        case "frmHizmetAta":
                            xtraTabControldetay.SelectedTabPage = tphizmetler;
                            gethizmet();
                            break;
                        case "frmMuayeneAsi":
                            xtraTabControlizlemler.SelectedTabPage = tpasilar;
                            getasi();
                            break;
                        case "frmOlumBildirimi":
                            xtraTabControlizlemler.SelectedTabPage = tpolumbildirimi;
                            getvefat();
                            break;
                        case "frmObeziteIzlem":
                            getobeziteizlem();
                            break;
                        case "frm15_49KadinIzleme":
                            xtraTabControlizlemler.SelectedTabPage = tpkadin;
                            getkadinizlem();
                            break;
                        case "frmGebeBaslangic":
                            xtraTabControlgebe.SelectedTabPage = tpgebebaslangic;
                            getgebebaslangic();
                            break;
                        case "frmGebeIzlem":
                            
                            xtraTabControlgebe.SelectedTabPage = tbgebeizlem;
                            getgebeizle();
                            break;
                        case "frmGebeSonuc":
                            xtraTabControlgebe.SelectedTabPage = tpgebesonuc;
                            getgebesonuc();
                            break;
                        case "frmLohusaIzlem":
                            getlohusaizle();
                            break;
                        case "frmBebekIzleme":
                            xtraTabControlizlemler.SelectedTabPage = tpbebekcocuk;
                            getbebekcocukizle();
                            break;
                        case "frmBebekCocukBeslenme":
                            getbebekbeslenizle();
                            break;
                        case "frmBebekDogumBildirim":
                            xtraTabControlizlemler.SelectedTabPage = tpdogumbildirimi;
                            getdogum();
                            break;
                        case "frmSaglikIstirahat":
                            xtraTabControldetay.SelectedTabPage = tpraporlar;
                            getraporlar();
                            break;
                       
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Hata :" + ex.InnerException.Message.ToString());
                }

            }
            else
                if (menuitem != null && menuitem.Tag != null && menuitem.OwnerItem.Name == "MenuItemDuzenle")
                {
                    switch (menuitem.Tag.ToString())
                    {
                        case "frmHasta":
                            grdhasta.Focus();
                            break;
                        case "frmMuayene":
                            radioButtonMuayeneGrubu.Checked = true;
                            if (xtraTabControlMuayene.SelectedTabPage == xtraTabPageBugunkuMuayeneler)
                            {
                                xtraTabControlMuayene.SelectedTabPage = xtraTabPaGecmisMuayene;
                                xtraTabControlMuayene.SelectedTabPage = xtraTabPageBugunkuMuayeneler;
                                grdBugunkumuayene.Focus();
                            }
                            else
                            {
                                xtraTabControlMuayene.SelectedTabPage = xtraTabPageBugunkuMuayeneler;
                                xtraTabControlMuayene.SelectedTabPage = xtraTabPaGecmisMuayene;
                                gridGecmisMuayene.Focus();
                            }
                            break;
                        case "frmTaniAta":
                            radioButtonMuayeneGrubu.Checked = true;
                            xtraTabControldetay.SelectedTabPage = tptanilar;
                            grdtani.Focus();
                            break;
                        case "frmRecete":
                            radioButtonMuayeneGrubu.Checked = true;
                            xtraTabControldetay.SelectedTabPage = tpreceteler;

                            grdrecete.Focus();
                            break;
                        case "frmTetkik":
                            radioButtonMuayeneGrubu.Checked = true;
                            xtraTabControldetay.SelectedTabPage = tptetkikler;
                            grdtetkik.Focus();
                            break;
                        case "frmAnemnez":
                            radioButtonMuayeneGrubu.Checked = true;
                            xtraTabControldetay.SelectedTabPage = tpanemnez;
                            grdanamnez.Focus();
                            break;
                        case "frmSevk":
                            radioButtonMuayeneGrubu.Checked = true;
                            xtraTabControldetay.SelectedTabPage = tpsevkler;
                            grdsevk.Focus();
                            break;
                        case "frmSaglikIstirahat":
                            radioButtonMuayeneGrubu.Checked = true;
                            xtraTabControldetay.SelectedTabPage = tpraporlar;
                            gridRaporlar.Focus();
                            break;
                        case "frmHizmetAta":
                            radioButtonMuayeneGrubu.Checked = true;
                            xtraTabControldetay.SelectedTabPage = tphizmetler;
                            gridhizmet.Focus();
                            break;
                        case "frmMuayeneAsi":
                            radioButtonIzlemGrubu.Checked = true;
                            xtraTabControlizlemler.SelectedTabPage = tpasilar;
                            grdasi.Focus();
                            break;
                        case "frmOlumBildirimi":
                            radioButtonIzlemGrubu.Checked = true;
                            xtraTabControlizlemler.SelectedTabPage = tpolumbildirimi;
                            grdvefat.Focus();
                            break;
                        case "frmObeziteIzlem":
                            radioButtonIzlemGrubu.Checked = true;
                            grdobezizlem.Focus();
                            break;
                        case "frm15_49KadinIzleme":
                            radioButtonIzlemGrubu.Checked = true;
                            xtraTabControlizlemler.SelectedTabPage = tpkadin;
                            grdkadinizlem.Focus();
                            break;
                        case "frmGebeBaslangic":
                            radioButtonIzlemGrubu.Checked = true;
                            xtraTabControlgebe.SelectedTabPage = tpgebebaslangic;
                            grdgebebaslangic.Focus();
                            break;
                        case "frmGebeIzlem":
                            radioButtonIzlemGrubu.Checked = true;
                            xtraTabControlgebe.SelectedTabPage = tbgebeizlem;
                            grdgebeizlem.Focus();
                            break;
                        case "frmGebeSonuc":
                            radioButtonIzlemGrubu.Checked = true;
                            xtraTabControlgebe.SelectedTabPage = tpgebesonuc;
                            grdgebesonuc.Focus();
                            break;
                        case "frmLohusaIzlem":
                            radioButtonIzlemGrubu.Checked = true;
                            grdlohusaizlem.Focus();
                            break;
                        case "frmBebekIzleme":
                            radioButtonIzlemGrubu.Checked = true;
                            xtraTabControlizlemler.SelectedTabPage = tpbebekcocuk;
                            grdbebekcocukizlem.Focus();
                            break;
                        case "frmBebekCocukBeslenme":
                            radioButtonIzlemGrubu.Checked = true;
                            grdbebekbeslenmeizlem.Focus();

                            break;
                        case "frmBebekDogumBildirim":
                            radioButtonIzlemGrubu.Checked = true;
                            xtraTabControlizlemler.SelectedTabPage = tpdogumbildirimi;
                            grddogum.Focus();
                            break;

                        default:
                            break;
                    }
                }


        }

        public void SetEskiTabBaslikRengi(DevExpress.XtraTab.XtraTabPage eskitabpage, DevExpress.XtraTab.XtraTabPage yenitabpage)
        {
            eskitabpage.Appearance.Header.BorderColor = Color.Empty;
            eskitabpage.Appearance.Header.Font = Font = new Font("Tahoma", 8.25F, FontStyle.Regular); ;
            eskitabpage.Appearance.Header.ForeColor = Color.Empty; ;
        }

        public void SetSeciliTabBaslikRengi(DevExpress.XtraTab.XtraTabPage tabpage)
        {
            tabpage.Appearance.Header.BorderColor = Color.Red;
            tabpage.Appearance.Header.Font = new Font("Tahoma", 8, FontStyle.Bold);
            tabpage.Appearance.Header.ForeColor = Color.Red;
        }

        private void frmHastaAra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                btngetir_Click(sender, e);
        }

        private void xtraTabControlhasta_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControlhasta.SelectedTabPage == TpProtokolDefteri)
            {
                getprotokoldefteri();
            }
            else
                if (xtraTabControlhasta.SelectedTabPage == tbdagilim)
                {
                    grdadetler.DataSource = Current.GetHastaDagilimlari();
                }
        }

        private void getprotokoldefteri()
        {
            gridProtokolDefteri.DataSource = Current.GetProtokolDefteri(dateEditBaslangicTarihiProtokol.DateTime, dateEditBitisTarihiProtokol.DateTime, Current.AktifDoktorId);
        }

        private void dateEditBitisTarihiProtokol_EditValueChanged(object sender, EventArgs e)
        {
            getprotokoldefteri();
        }

        private void dateEditBaslangicTarihiProtokol_EditValueChanged(object sender, EventArgs e)
        {
            getprotokoldefteri();
        }

        private void Buttonprotokoldefterigetir_Click(object sender, EventArgs e)
        {

            ReportProtokolDefteri rprprotokoldefteri = new ReportProtokolDefteri();
            rprprotokoldefteri.DataSource = Current.GetProtokolDefteri(dateEditBaslangicTarihiProtokol.DateTime, dateEditBitisTarihiProtokol.DateTime, Current.AktifDoktorId);
            rprprotokoldefteri.DataMember = "Table";
            rprprotokoldefteri.Parameters[1].Value = dateEditBitisTarihiProtokol.DateTime;
            rprprotokoldefteri.Parameters[0].Value = dateEditBaslangicTarihiProtokol.DateTime;
            rprprotokoldefteri.ShowPreview();

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string saat = string.Empty;
            RandevuListesiniMonitoreDoldur(ref saat);

            if ((Current.AktifHastaId > 0))
            {
                this.monitor.labelControl2.Text = "Randevu Saati:" + (bshasta.Current as DataRowView)["Saat"].ToString() + "\r\nHasta Adı:" + Current.AktifHasta.ToString() + "\r\nLÜTFEN GİRİNİZ";
                this.monitor.Renklen = true;
                this.monitor.timer1.Enabled = true;
                this.monitor.MesajYayinla = false;
            }
        }

        private void MonitoruGoster()
        {
            if ((frmMonitor.monitorinstance == null) || frmMonitor.monitorinstance.IsDisposed)
            {
                Screen[] ekranlar = System.Windows.Forms.Screen.AllScreens;
                if (Screen.AllScreens.Length > 1)
                {
                    //yeni from
                    this.monitor = frmMonitor.MonitorInstance();
                    // Önemli !
                    this.monitor.StartPosition = FormStartPosition.Manual;
                    // Ikinci Monitörü tanimla
                    Screen screen = GetScreen();
                    // Ikinci formun location tanimla
                    this.monitor.Location = screen.WorkingArea.Location;
                    // fullscreen yap
                    this.monitor.Size = new Size(screen.WorkingArea.Width, screen.WorkingArea.Height);
                    // formu aç
                    this.monitor.Show(this);
                    this.monitor.listBoxRandevu.Items.Clear();
                }
                else
                {
                    this.monitor = frmMonitor.MonitorInstance();
                    this.monitor.StartPosition = FormStartPosition.Manual;
                    Screen screen = GetScreen();
                    this.monitor.Location = screen.WorkingArea.Location;
                    this.monitor.Size = new Size(screen.WorkingArea.Width, screen.WorkingArea.Height);
                    this.monitor.Show(this);
                    this.monitor.listBoxRandevu.Items.Clear();

                }

            }
        }

        public Screen GetScreen(myenum.EkranGetir ekran = myenum.EkranGetir.İkinciEkran)
        {
            if (Screen.AllScreens.Length == 1)
            {
                return Screen.PrimaryScreen;
            }
            foreach (Screen screen in Screen.AllScreens)
            {
                if (myenum.EkranGetir.BirinciEkran == ekran)
                {
                    if (screen.Primary == false)
                    {
                        return screen;
                    }
                }
                else
                    if (screen.Primary == true)
                    {
                        return screen;
                    }
            }
            return null;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            string saat = string.Empty;
            MonitoruGoster();
            RandevuListesiniMonitoreDoldur(ref saat);
            this.monitor.labelControl2.Text = "LÜTFEN BEKLEYİNİZ.\r\nİÇERDE HASTA VAR.";
            this.monitor.Renklen = true;
            this.monitor.timer1.Enabled = true;
            this.monitor.MesajYayinla = false;


        }

        private void btnMesajiYayinla_Click(object sender, EventArgs e)
        {
            string saat = string.Empty;
            MonitoruGoster();
            RandevuListesiniMonitoreDoldur(ref saat);
            this.monitor.labelControl2.Text = "LÜTFEN BEKLEYİNİZ";
            this.monitor.Renklen = true;
            this.monitor.timer1.Enabled = true;
            this.monitor.MesajYayinla = true;
            this.monitor.textBox1.Text = textBoxMonitorMesaji.Text;


        }

        private void RandevuListesiniMonitoreDoldur(ref string saat)
        {

            if (radioButtonRandevuluHastalar.Checked)
            {
                if (Current.AktifHastaId > 0)
                {
                    if (gridViewHasta.RowCount > 0)
                    {
                        MonitoruGoster();
                        this.monitor.listBoxRandevu.Items.Clear();
                        string randevusaati = string.Empty; ;
                        for (int i = 0; i < gridViewHasta.RowCount; i++)
                        {
                            DateTime tarih = Convert.ToDateTime(gridViewHasta.GetRowCellValue(i, "BasTarih"));
                            if (tarih != System.DateTime.Today)
                                continue;

                            try
                            {

                                randevusaati = string.Empty;
                                saat = randevusaati = gridViewHasta.GetRowCellValue(i, "Saat").ToString();
                            }
                            catch (Exception)
                            {
                            }
                            this.monitor.listBoxRandevu.Items.Add(randevusaati + " " + gridViewHasta.GetRowCellValue(i, "AdiSoyadi").ToString());
                        }

                    }
                    else
                    {
                        MessageBox.Show("Listede çağrılacak hasta yok.");
                    }

                }

            }
            else
                throw new Exception("Monitör Özelliği yanlızca randevulu hastalarda kullanılabilir.");
        }

        private void simpleButtonMuayeneKapat_Click(object sender, EventArgs e)
        {
                if (DialogResult.OK == MessageBox.Show(
                    "Muayene tarihi " + DateEditBasTarih.DateTime.ToString("dd.MM.yyyy") + " ile " + dateEditBitTar.DateTime.ToString("dd.MM.yyyy") +
                    " arasında olan hastalarınızın açık muayeneleri kapatılacak.\nİşleme devam etmek istiyor musunuz?", 
                    "Uyarı", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
            {
                int kapatilanmuayene = Current.SeciliMuayeneleriKapat(DateEditBasTarih.DateTime, dateEditBitTar.DateTime);

                if (kapatilanmuayene > 0)
                    MessageBox.Show(kapatilanmuayene + " adet muayene kapatilmistir.");
            }
        }
        
        private void btnsecilimuayenekapat_Click(object sender, EventArgs e)
        {
            if (Current.AktifHastaId > 0)
                if (etiket == "Muayene")
                {
                    if (DialogResult.OK == MessageBox.Show("("+Current.AktifHasta.Adi+" "+Current.AktifHasta.Soyadi+ ") Seçili Hastanıza ait seçili Muayene kapatılacak.\nİşleme devam etmek istiyor musunuz?", "Uyarı", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
                    {
                        Current.AktifMuayeneyiKapat();
                        if (xtraTabControlMuayene.SelectedTabPage == xtraTabPageBugunkuMuayeneler)
                            getbugunkumuayene();
                        else
                            getgecmismuayene();
                    }
                    return;
                }
        }

        private void simpleButtonAra_Click(object sender, EventArgs e)
        {
            //frmHastaBul frm = new frmHastaBul(textEditHastaBul.Text);
            frmHastaBul frm = new frmHastaBul(superTextBoxTckimlikno.textBox.Text, superTextBoxAdi.textBox.Text, superTextBoxSoyadi.textBox.Text,"");
            frm.ShowDialog();
            HastaGetir();
        }

        private void btnal_Click(object sender, EventArgs e)
        {
            if (Current.AktifHastaId > 0)
                if (DialogResult.OK ==
                    MessageBox.Show("(" + Current.AktifHasta.Adi + " " + Current.AktifHasta.Soyadi 
                    + ") Seçili Hastanıza ait bakanlıkta olupta sizde olmayan bilgiler çekilecek.\nİşleme devam etmek istiyor musunuz?", "Uyarı", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
                {
                    try
                    {
                        btngonderal.Text = "Bakanlık -Hasta Tetkik Bilgileri- alınıyor...";

                        
                        btngonderal.Text = "Bakanlık -Hasta Detay Bilgileri- alınıyor...";
                        Application.DoEvents();
                        HASTAKAYITBILGISI bh = WebUtil.getBakanlikHastaBilgiDetay("P", Current.AktifHasta.Id, Current.AktifHasta.Adi, Current.AktifHasta.Soyadi);
                        if (bh != null)
                         Current.AktifHasta= WebUtil.setBakanlikHastaToLocalHasta(bh, Current.AktifHasta, false);
              
                        btngonderal.Text = "Bakanlık -Geçmiş Muayene ve İzlem Bilgileri- alınıyor...";
                        Application.DoEvents();
                        Cursor.Current = Cursors.WaitCursor;
                        TOPLUMUAYENELISTE ml = WebUtil.getBakanlikHastaMuayeneIzlem("P", Current.AktifHasta.Id, Current.AktifHasta.Adi, Current.AktifHasta.Soyadi);

                        btngonderal.Text = "TUIK -Vatandaşlık Bilgileri- alınıyor...";
                        Application.DoEvents();
                        WebUtil.setHastaTuikBilgi(Current.AktifHasta);

                        Current.AktifHasta.Update();

                        Cursor.Current = Cursors.WaitCursor;
                        if (ml != null)
                        {
                            WebUtil.setBakanlikMuayeneIzlemToLocalMuayeneIzlem(ml, Current.AktifHasta);
                            etiket = "Hasta";
                            setbuttons(bshasta);
                            getgecmismuayene();
                        }
                    

                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                        btngonderal.Text = "Bakanlık Günder/Al";
                    }
                }

        }
    }
}