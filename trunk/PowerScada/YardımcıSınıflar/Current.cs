using System;
using System.Collections.Generic;
using mymodel;
using SharpBullet.OAL;
using PowerScada.Properties;
using System.Windows.Forms;
using System.Xml;

namespace PowerScada
{
    public static class Current
    {
        public static string globalresmessage;
        public static int globalressonuc;
        public static string constr = "";
        public static string masterconstr = "";
        public static string anaklasor="C:\\PowerScada";
        public static string dataklasor="C:\\PowerScada\\Data";
        public static string xmlklasor="C:\\PowerScada\\XML";
        public static string kodklasor="C:\\PowerScada\\Kodlar";
        public static string excelklasor="C:\\PowerScada\\ExcelRapor";
        public static string pdfklasor="C:\\PowerScada\\PDFTetkik";
        public static bool DBIsUzakMakine = false;

        public static string getdate()
        {
         string result=  "Getdate()";
            if (Configuration.GetValue("DbType") == "System.Data.Sqlite")
                result = "current_date";

            return result;
        }
        //private static Doktor aktifdoktor;
        //public static Doktor AktifDoktor
        //{
        //    get
        //    {
        //        if (aktifdoktor == null)
        //        {
        //            if (Current.User.Doktor.Id > 0)
        //            {
        //                aktifdoktor = Persistence.Read<Doktor>(Current.User.Doktor.Id);
        //                if (aktifdoktor.Lokasyonilce.Id>0)
        //                    aktifdoktor.Lokasyonilce = Persistence.Read<Lokasyon>(aktifdoktor.Lokasyonilce.Id);
        //                if (aktifdoktor.LokasyonSehir.Id > 0)
        //                    aktifdoktor.LokasyonSehir = Persistence.Read<Lokasyon>(aktifdoktor.LokasyonSehir.Id);
        //                Current.User.Doktor = aktifdoktor;
        //            }
        //        }
        //        return aktifdoktor;
        //    }
        //    set
        //    {
        //        aktifdoktor = value;
        //        if (aktifdoktor.Lokasyonilce.Id > 0)
        //            aktifdoktor.Lokasyonilce = Persistence.Read<Lokasyon>(aktifdoktor.Lokasyonilce.Id);
        //        if (aktifdoktor.LokasyonSehir.Id > 0)
        //            aktifdoktor.LokasyonSehir = Persistence.Read<Lokasyon>(aktifdoktor.LokasyonSehir.Id);
        //    }
        //}

        //private static long aktifdoktorid;
        //public static long AktifDoktorId
        //{
        //    get
        //    {
        //        long Id = 0;
        //        if (Current.AktifDoktor != null)
        //            Id= Current.AktifDoktor.Id;

        //        return Id;
        //    }
        //    set
        //    {
        //        aktifdoktorid = value;
        //    }
        //}



        //private static Hasta aktifhasta;
        //public static Hasta AktifHasta
        //{
        //    get
        //    {
        //        return aktifhasta;
        //    }
        //    set
        //    {
        //        aktifhasta = value;
        //    }
        //}

        //public static ProgramAyarlari PrgAyar
        //{
        //    get
        //    {
        //        return Persistence.Read<ProgramAyarlari>(1);
        //    }
        //}

        //public static long AktifHastaId
        //{
        //    get
        //    {
        //        long Id = 0;
        //        if (Current.AktifHasta != null)
        //            Id= Current.AktifHasta.Id;

        //        return Id;
        //    }
        //}


        //private static Muayene aktifmuayene;
        //public static Muayene AktifMuayene
        //{
        //    get
        //    {
        //        return aktifmuayene;
        //    }
        //    set
        //    {
        //        aktifmuayene = value;
        //    }
        //}

        //public static long AktifMuayeneId 
        //{ 
        //    get
        //    {
        //        long Id = 0;
        //        if (Current.AktifMuayene!=null)
        //          Id=Current.AktifMuayene.Id;

        //        return Id;
        //    } 
        //}


        //private static Takvim aktifrandevu;
        //public static Takvim AktifRandevu
        //{
        //    get
        //    {
        //        return aktifrandevu;
        //    }
        //    set
        //    {
        //        aktifrandevu = value;
        //    }
        //}

        //public static long AktifRandevuId
        //{
        //    get
        //    {
        //        long Id = 0;
        //        if (Current.AktifRandevu != null)
        //            Id= Current.AktifRandevu.Id;
        //        return Id;
        //    }
        //}

        public static Kullanici User;
        public static string tempuzaksunucu = "";
        public static bool Login(string username, string password, string connectionstring, bool benihatirla, string sqlservisadi)
        {
            SharpBullet.OAL.Configuration.SetValue("Connection", connectionstring);
            string tempsifre = password;
            //Şifreler veritabanında md5'li olarak saklanmalı
            password = SecurityHelper.GetMd5Hash(password);

            //--Users u = new Users() { Username = username, Password = password }; u.Save();

            Kullanici user = Persistence.Read<Kullanici>(
                new Condition("Login", Operator.Equal, username),
                new Condition("Sifre", Operator.Equal, password)) ?? new Kullanici();

            if (user.Exist())
            {
                Current.User = user;
                CurrentModel.User = user;
                Current.AktifKullaniciEkranHaklari();
              
                if (benihatirla)
                {
                    Settings.Default.Kullanici = Current.User.Login;
                    Settings.Default.Sifre = tempsifre;
                    Settings.Default.BeniHatirla = benihatirla;
                    Settings.Default.anamakina = sqlservisadi;
                }
                else
                    Properties.Settings.Default.Reset();
                
                Properties.Settings.Default.Save();
                return true;
            }

            Current.User = null;
            return false;
        }


        public static Dictionary<string, mymodel.RolEkranHakki> ekranlar = new Dictionary<string, mymodel.RolEkranHakki>();

        public static void AktifKullaniciEkranHaklari()
        {
            RolEkranHakki[] rolekranhakki;
            rolekranhakki=Persistence.ReadList<RolEkranHakki>("Select * from RolEkranHakki where Aktif=1 and Rol='"+Current.User.GorevTuru.ToString() +"'");

            foreach (RolEkranHakki rolhaklari in rolekranhakki)
            {
                ekranlar.Add(rolhaklari.EkranAdi.ToString(), rolhaklari);
            }

        }

        public static bool FormKullaniciGormeYetkisi(string formismi)
        {
            if (Current.User.GorevTuru == myenum.GorevTuru.Admin || Current.User.GorevTuru == myenum.GorevTuru.AileHekimi)
                return true;
            if (ekranlar.ContainsKey(formismi))
            {
                if (ekranlar[formismi].Izle || ekranlar[formismi].Sil || ekranlar[formismi].Degistir || ekranlar[formismi].Ekle)
                    return true;
                else
                    return false;

            }
            else
                return false;
        }

        public static bool FormKullaniciIzlemeYetkisi(string formismi)
        {

            if (ekranlar.ContainsKey( formismi))
            {
                return ekranlar[ formismi].Izle;

            }
            else
                return false;
        }
     
        public static bool FormKullaniciSilmeYetkisi(string formismi)
        {

            if (ekranlar.ContainsKey( formismi))
            {
                return ekranlar[ formismi].Sil;

            }
            else
                return false;
        }

        public static bool FormKullaniciEkleYetkisi(string formismi)
        {

            if (ekranlar.ContainsKey( formismi))
            {
                return ekranlar[ formismi].Ekle;

            }
            else
                return false;
        }

        public static bool FormKullaniciDegistirYetkisi(string formismi)
        {

            if (ekranlar.ContainsKey( formismi))
            {
                return ekranlar[ formismi].Degistir;

            }
            else
                return false;
        }

        #region Cachelemeler

        //#region Teshislerin Cachlenmesi

        //public static Dictionary<long, mymodel.Teshis> aktifteshisler = new Dictionary<long, mymodel.Teshis>();

        //public static void AktifTeshisler()
        //{
        //    Teshis[] Teshisler;
        //    Teshisler = Persistence.ReadList<Teshis>("Select * from Teshis where Aktif=1");

        //    foreach (Teshis teshis in Teshisler)
        //    {
        //        aktifteshisler.Add(teshis.Id, teshis);
        //    }

        //}

        //public static Teshis GetTeshis(long TeshidId)
        //{
        //    if (aktifteshisler.ContainsKey(TeshidId))
        //        return aktifteshisler[TeshidId];
        //    else
        //    {
        //        Teshis teshis = Persistence.Read<Teshis>(TeshidId);
        //        aktifteshisler.Add(teshis.Id, teshis);

        //        return teshis;
        //    }
        //}

        //#endregion

        //#region Hizmetlerin Cachlenmesi

        //public static Dictionary<long, mymodel.Hizmet> aktifhizmetler = new Dictionary<long, mymodel.Hizmet>();

        //public static void AktifHizmetler()
        //{
        //    Hizmet[] Hizmetler;
        //    Hizmetler = Persistence.ReadList<Hizmet>("Select * from Hizmet where Aktif=1");

        //    foreach (Hizmet hizmet in Hizmetler)
        //    {
        //        aktifhizmetler.Add(hizmet.Id, hizmet);
        //    }

        //}

        //public static Hizmet GetHizmet(long hizmetId)
        //{
        //    if (aktifhizmetler.ContainsKey(hizmetId))
        //        return aktifhizmetler[hizmetId];
        //    else
        //    {
        //        Hizmet hizmet = Persistence.Read<Hizmet>(hizmetId);
        //        aktifhizmetler.Add(hizmet.Id, hizmet);

        //        return hizmet;
        //    }
        //}

        //#endregion

        //#region Hizmet Türünün Cachlenmesi

        //public static Dictionary<long, mymodel.HizmetTur> aktifhizmetturleri = new Dictionary<long, mymodel.HizmetTur>();

        //public static void AktifHizmetTurleri()
        //{
        //    HizmetTur[] HizmetTurleri;
        //    HizmetTurleri = Persistence.ReadList<HizmetTur>("Select * from HizmetTur where Aktif=1");

        //    foreach (HizmetTur hizmettur in HizmetTurleri)
        //    {
        //        aktifhizmetturleri.Add(hizmettur.Id, hizmettur);
        //    }

        //}

        //public static HizmetTur GetHizmetTuru(long hizmetturuId)
        //{
        //    if (aktifhizmetturleri.ContainsKey(hizmetturuId))
        //        return aktifhizmetturleri[hizmetturuId];
        //    else
        //    {
        //        HizmetTur hizmetturu = Persistence.Read<HizmetTur>(hizmetturuId);
        //        aktifhizmetturleri.Add(hizmetturu.Id, hizmetturu);

        //        return hizmetturu;
        //    }
        //}

        //#endregion

        //#region Ilacların Cachlenmesi

        //public static Dictionary<long, mymodel.ilac> aktifilaclar = new Dictionary<long, mymodel.ilac>();

        //public static void AktifIlaclar()
        //{
        //    ilac[] ilaclar;
        //    ilaclar = Persistence.ReadList<ilac>("Select * from ilac where Aktif=1");

        //    foreach (ilac ilac in ilaclar)
        //    {
        //        aktifilaclar.Add(ilac.Id, ilac);
        //    }

        //}

        //public static ilac GetIlac(long ilacId)
        //{
        //    if (aktifilaclar.ContainsKey(ilacId))
        //        return aktifilaclar[ilacId];
        //    else
        //    {
        //        ilac ilacentity = Persistence.Read<ilac>(ilacId);
        //        if (ilacentity!=null)
        //        aktifilaclar.Add(ilacentity.Id, ilacentity);

        //        return ilacentity;
        //    }
        //}

        //#endregion
        #endregion



        #region GridStylar

        //public static string GetGridStyle(this DataGridView grid)
        //{
        //    string result = "";
        //    result += " <Style>" + Environment.NewLine;
        //    foreach (DataGridViewColumn column in grid.Columns)
        //    {
        //        result += string.Format(@"    <Column Name='{0}' HeaderText='{1}' Width='{2}' DisplayIndex='{3}' />" + Environment.NewLine,
        //            column.Name, column.HeaderText, column.Width, column.DisplayIndex);
        //    }
        //    result += " </Style>";

        //    return result;
        //}

        //public static void SetGridStyle(this DataGridView grid, string style)
        //{
        //    grid.AutoGenerateColumns = false;
        //    grid.Columns.Clear();

        //    XmlDocument doc = new XmlDocument();
        //    doc.LoadXml(style);

        //    XmlNodeList columns = doc.DocumentElement.GetElementsByTagName("Column");
        //    foreach (XmlNode columnNode in columns)
        //    {
        //        string name = columnNode.Attributes["Name"].InnerText;

        //        string type = columnNode.Attributes["Type"] != null ? columnNode.Attributes["Type"].InnerText : "";
        //        string text = columnNode.Attributes["Text"] != null ? columnNode.Attributes["Text"].InnerText : "";
        //        if (type == "CheckBox")
        //            grid.Columns.Add(new DataGridViewCheckBoxColumn() { Name = name, HeaderText = name });
        //        else if (type == "ComboBox")
        //            grid.Columns.Add(new DataGridViewComboBoxColumn() { Name = name, HeaderText = name });
        //        else if (type == "Button")
        //            grid.Columns.Add(new DataGridViewButtonColumn() { Name = name, HeaderText = name, Text = text, UseColumnTextForButtonValue = true });
        //        else
        //            grid.Columns.Add(name, name);
        //    }
        //    foreach (XmlNode columnNode in columns)
        //    {
        //        string name = columnNode.Attributes["Name"].InnerText;
        //        DataGridViewColumn column = grid.Columns[name];
        //        column.DataPropertyName = name;
        //        column.HeaderText = columnNode.Attributes["HeaderText"] != null ? columnNode.Attributes["HeaderText"].InnerText : "";
        //        column.Width = int.Parse(columnNode.Attributes["Width"].InnerText);
        //        column.ReadOnly = bool.Parse(columnNode.Attributes["ReadOnly"].InnerText);
        //        column.Visible = bool.Parse(columnNode.Attributes["Visible"].InnerText);
        //        column.DisplayIndex = int.Parse(columnNode.Attributes["DisplayIndex"].InnerText);
        //    }
        //}

        //public static void SetGridStyle(this DevExpress.XtraGrid.GridControl grid, string style)
        //{
        //    //grid.AutoGenerateColumns = false;
        //    GridView view = (GridView)grid.DefaultView;
        //    view.Columns.Clear();

        //    XmlDocument doc = new XmlDocument();
        //    doc.LoadXml(style);

        //    XmlNodeList columns = doc.DocumentElement.GetElementsByTagName("Column");
        //    foreach (XmlNode columnNode in columns)
        //    {
        //        string name = columnNode.Attributes["Name"].InnerText;

        //        string type = columnNode.Attributes["Type"] != null ? columnNode.Attributes["Type"].InnerText : "";
        //        string text = columnNode.Attributes["Text"] != null ? columnNode.Attributes["Text"].InnerText : "";
        //        if (type == "CheckBox")
        //        {
        //            int i = view.Columns.Add(new GridColumn() { Name = name, FieldName = name });
        //            view.Columns[i].ColumnEdit = new RepositoryItemCheckEdit();
        //        }
        //        else if (type == "ComboBox")
        //        {
        //            int i = view.Columns.Add(new GridColumn() { Name = name, FieldName = name });
        //            view.Columns[i].ColumnEdit = new RepositoryItemComboBox();
        //        }
        //        else if (type == "Button")
        //        {
        //            int i = view.Columns.Add(new GridColumn() { Name = name, FieldName = name });
        //            view.Columns[i].ColumnEdit = new RepositoryItemButtonEdit() { TextEditStyle = TextEditStyles.HideTextEditor };
        //            view.Columns[i].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
        //        }
        //        else
        //        {
        //            view.Columns.Add(new GridColumn() { Name = name, FieldName = name });
        //        }
        //    }
        //    foreach (XmlNode columnNode in columns)
        //    {
        //        string name = columnNode.Attributes["Name"].InnerText;
        //        GridColumn column = view.Columns[name];
        //        column.Caption = columnNode.Attributes["HeaderText"] != null ? columnNode.Attributes["HeaderText"].InnerText : "";
        //        column.Width = int.Parse(columnNode.Attributes["Width"].InnerText);
        //        column.VisibleIndex = int.Parse(columnNode.Attributes["DisplayIndex"].InnerText);
        //        try
        //        {
        //            column.Visible = bool.Parse(columnNode.Attributes["Visible"].InnerText);
        //        }
        //        catch (Exception)
        //        {
                    
                  
        //        }
                
        //    }
        //}

        //public static void SetCardGridStyle(this DevExpress.XtraGrid.GridControl grid, string style)
        //{
        //    //grid.AutoGenerateColumns = false;
        //    DevExpress.XtraGrid.Views.Card.CardView view = (DevExpress.XtraGrid.Views.Card.CardView)grid.DefaultView;
        //    view.Columns.Clear();

        //    XmlDocument doc = new XmlDocument();
        //    doc.LoadXml(style);

        //    XmlNodeList columns = doc.DocumentElement.GetElementsByTagName("Column");
        //    foreach (XmlNode columnNode in columns)
        //    {
        //        string name = columnNode.Attributes["Name"].InnerText;

        //        string type = columnNode.Attributes["Type"] != null ? columnNode.Attributes["Type"].InnerText : "";
        //        string text = columnNode.Attributes["Text"] != null ? columnNode.Attributes["Text"].InnerText : "";
        //        if (type == "CheckBox")
        //        {
        //            int i = view.Columns.Add(new GridColumn() { Name = name, FieldName = name });
        //            view.Columns[i].ColumnEdit = new RepositoryItemCheckEdit();
        //        }
        //        else if (type == "ComboBox")
        //        {
        //            int i = view.Columns.Add(new GridColumn() { Name = name, FieldName = name });
        //            view.Columns[i].ColumnEdit = new RepositoryItemComboBox();
        //        }
        //        else if (type == "Button")
        //        {
        //            int i = view.Columns.Add(new GridColumn() { Name = name, FieldName = name });
        //            view.Columns[i].ColumnEdit = new RepositoryItemButtonEdit() { TextEditStyle = TextEditStyles.HideTextEditor };
        //            view.Columns[i].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
        //        }
        //        else
        //        {
        //            view.Columns.Add(new GridColumn() { Name = name, FieldName = name });
        //        }
        //    }
        //    foreach (XmlNode columnNode in columns)
        //    {
        //        string name = columnNode.Attributes["Name"].InnerText;
        //        GridColumn column = view.Columns[name];
        //        column.Caption = columnNode.Attributes["HeaderText"] != null ? columnNode.Attributes["HeaderText"].InnerText : "";
        //        column.Width = int.Parse(columnNode.Attributes["Width"].InnerText);
        //        column.VisibleIndex = int.Parse(columnNode.Attributes["DisplayIndex"].InnerText);
        //    }
        //}




        #endregion

        //public static TakvimSablonu GetTakvimSablonu(myenum.IzlemTuru sablonturu)
        //{
        //    Condition[] con=new Condition[2];
        //    con[0].Field="SablonTuru";
        //    con[0].Operator=Operator.Equal;
        //    con[0].Value=sablonturu.ToString();

        //    con[1].Field="Aktif";
        //    con[1].Operator=Operator.Equal;
        //    con[1].Value=true;

        //    TakvimSablonu takvimsablonu = Persistence.Read<TakvimSablonu>(con);
        //    if (takvimsablonu != null)
        //    {
        //        return takvimsablonu;
        //    }
        //    else
        //        return null;
        //}


        ///// <summary>
       
        ///// </summary>
        ///// <param name="sablonturu"></param>
        ///// <param name="islemtarihi"></param>
        ///// <param name="hasta"></param>
        //public static Takvim TakvimOlustur(myenum.IzlemTuru sablonturu,Hasta hasta,DateTime tarih)
        //{
        //    Takvim takvim = null;
        //    if (hasta.KayitDurumu == myenum.KayitDurumu.Kayitli)
        //    {
        //        TakvimSablonu sablon = Current.GetTakvimSablonu(sablonturu);
               
        //        if (sablon != null)
        //        {

        //            switch (sablon.SablonTuru)
        //            {
        //                case myenum.IzlemTuru.Bebek_Izlemi:
        //                    takvim = Current.BebekRandevulariniOlustur(sablon, hasta, tarih);
        //                    break;
        //                case myenum.IzlemTuru.Cocuk_Izlemi:
        //                    takvim = Current.CocukRandevulariniOlustur(sablon, hasta, tarih);
        //                    break;
        //                case myenum.IzlemTuru.Obez_Izlemi:
        //                    takvim = Current.ObezRandevulariniOlustur(sablon, hasta, tarih);
        //                    break;
        //                case myenum.IzlemTuru.Kadin_Izlemi:
        //                    takvim = Current.KadınRandevulariniOlustur(sablon, hasta, tarih);
        //                    break;
        //                case myenum.IzlemTuru.Gebe_Izlemi:
        //                    takvim = Current.GebeRandevulariniOlustur(sablon, hasta, tarih);
        //                    break;
        //                case myenum.IzlemTuru.Lohusa_Izlemi:
        //                    takvim = Current.LohusaRandevulariniOlustur(sablon, hasta, tarih);
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }
        //    }
        //    return takvim;
        //}

      

//        /// <summary>
//        /// Bir sonraki randevuyu döndersin
//        /// </summary>
//        /// <param name="sablon"></param>
//        /// <param name="islemtarihi"></param>
//        /// <param name="hasta"></param>
//        /// <param name="birsonrakiizlemtarihi"></param>
//        /// <returns></returns>
//        public static Takvim BebekRandevulariniOlustur(TakvimSablonu sablon, Hasta hasta, DateTime dogumtarihi)
//        {
//            Takvim Ilkrandevu = null;
//            Takvim takvim = null;
                
//                if (sablon!=null) 
//                {
                    
//                    TakvimSablonSatiri[] sablonsatirlari = Persistence.ReadList<TakvimSablonSatiri>("Select * from TakvimSablonSatiri where TakvimSablonu_Id=@prm0 and TakvimSablonSatiri.Aktif=1 order by TakvimSablonSatiri.IzlemSıraNo asc", new object[] {sablon.Id });
//                    if (sablonsatirlari != null && sablonsatirlari.Length > 0)
//                    {
//                        DateTime bastarih =Convert.ToDateTime(dogumtarihi.ToShortDateString());
//                        foreach (TakvimSablonSatiri satir in sablonsatirlari)
//                        {

//                            bastarih = bastarih.AddDays(satir.IlkIzlemdenSonrakiSure);
//                            takvim = Utility.RandevuOlustur(hasta, bastarih, null, myenum.IslemTuru.Izlem, sablon.SablonTuru, "Hastaya " + sablon.Adi + "in " + satir.Adi + " yapılacaktır");
//                            if (System.DateTime.Today >= takvim.BasTarih)
//                                continue;
//                            if (takvim.Id > 0)
//                            {
//                                foreach (TakvimSatiri yakvimsatiri in takvim.TakvimSatirlari)
//                                {
//                                    yakvimsatiri.Takvim.Id = takvim.Id;
//                                    yakvimsatiri.Insert();
//                                }
//                            }
//                            else
//                            {
//                                takvim.Insert();
//                                foreach (TakvimSatiri yakvimsatiri in takvim.TakvimSatirlari)
//                                {
//                                    yakvimsatiri.Takvim.Id = takvim.Id;
//                                    yakvimsatiri.Insert();
//                                }

//                            }
//                            if (Ilkrandevu == null)
//                                Ilkrandevu = takvim;

//                        }
//                    }
//                }
//            return Ilkrandevu;
//        }

//        public static Takvim CocukRandevulariniOlustur(TakvimSablonu sablon, Hasta hasta, DateTime dogumtarihi)
//        {
//            Takvim Ilkrandevu = null;
//            Takvim takvim = null;
         
//            if (sablon != null)
//            {
//                TakvimSablonSatiri[] sablonsatirlari = Persistence.ReadList<TakvimSablonSatiri>("Select * from TakvimSablonSatiri where TakvimSablonu_Id=@prm0 and TakvimSablonSatiri.Aktif=1 order by TakvimSablonSatiri.IzlemSıraNo asc", new object[] { sablon.Id });
//                if (sablonsatirlari != null && sablonsatirlari.Length > 0)
//                {
//                    DateTime bastarih = Convert.ToDateTime(dogumtarihi.ToShortDateString());
//                    foreach (TakvimSablonSatiri satir in sablonsatirlari)
//                    {
//                        bastarih = bastarih.AddDays(satir.IlkIzlemdenSonrakiSure);
//                        takvim = Utility.RandevuOlustur(hasta, bastarih, null, myenum.IslemTuru.Izlem, sablon.SablonTuru, "Hastaya " + sablon.Adi + "in " + satir.Adi + " yapılacaktır");
//                        if (System.DateTime.Today >= takvim.BasTarih)
//                            continue;
//                        if (takvim.Id > 0)
//                        {
//                            foreach (TakvimSatiri yakvimsatiri in takvim.TakvimSatirlari)
//                            {
//                                yakvimsatiri.Takvim.Id = takvim.Id;
//                                yakvimsatiri.Insert();
//                            }
//                        }
//                        else
//                        {
//                            takvim.Insert();
//                            foreach (TakvimSatiri yakvimsatiri in takvim.TakvimSatirlari)
//                            {
//                                yakvimsatiri.Takvim.Id = takvim.Id;
//                                yakvimsatiri.Insert();
//                            }

//                        }
//                        if (Ilkrandevu == null)
//                            Ilkrandevu = takvim;

//                    }
//                }
//            }
//            return Ilkrandevu;
//        }

//        public static Takvim GebeRandevulariniOlustur(TakvimSablonu sablon, Hasta hasta, DateTime gebeliktarihi)
//        {
//            Takvim Ilkrandevu = null;
//            Takvim takvim = null;

//            if (sablon != null)
//            {
//                TakvimSablonSatiri[] sablonsatirlari = Persistence.ReadList<TakvimSablonSatiri>("Select * from TakvimSablonSatiri where TakvimSablonu_Id=@prm0 and TakvimSablonSatiri.Aktif=1 order by TakvimSablonSatiri.IzlemSıraNo asc", new object[] { sablon.Id });
//                if (sablonsatirlari != null && sablonsatirlari.Length > 0)
//                {
//                    DateTime bastarih = Convert.ToDateTime(gebeliktarihi.ToShortDateString());
//                    foreach (TakvimSablonSatiri satir in sablonsatirlari)
//                    {

//                        bastarih = bastarih.AddDays(satir.IlkIzlemdenSonrakiSure);
//                        takvim = Utility.RandevuOlustur(hasta, bastarih, null, myenum.IslemTuru.Izlem, sablon.SablonTuru, "Hastaya " + sablon.Adi + "in " + satir.Adi + " yapılacaktır");
//                        if (System.DateTime.Today >= takvim.BasTarih)
//                            continue;
//                        if (takvim.Id > 0)
//                        {
//                            foreach (TakvimSatiri yakvimsatiri in takvim.TakvimSatirlari)
//                            {
//                                yakvimsatiri.Takvim.Id = takvim.Id;
//                                yakvimsatiri.Insert();
//                            }
//                        }
//                        else
//                        {
//                            takvim.Insert();
//                            foreach (TakvimSatiri yakvimsatiri in takvim.TakvimSatirlari)
//                            {
//                                yakvimsatiri.Takvim.Id = takvim.Id;
//                                yakvimsatiri.Insert();
//                            }

//                        }
//                        if (Ilkrandevu == null)
//                            Ilkrandevu = takvim;

//                    }
//                }
//            }
//            return Ilkrandevu;
//        }

//        public static Takvim LohusaRandevulariniOlustur(TakvimSablonu sablon, Hasta hasta, DateTime gebelikbitistarihi)
//        {
//            Takvim Ilkrandevu = null;
//            Takvim takvim = null;
//            DateTime lohusalikbitistarihi = gebelikbitistarihi.AddDays(41);
//            if (sablon != null)
//            {
//                TakvimSablonSatiri[] sablonsatirlari = Persistence.ReadList<TakvimSablonSatiri>("Select * from TakvimSablonSatiri where TakvimSablonu_Id=@prm0 and TakvimSablonSatiri.Aktif=1 order by TakvimSablonSatiri.IzlemSıraNo asc", new object[] { sablon.Id });
//                if (sablonsatirlari != null && sablonsatirlari.Length > 0)
//                {
//                    DateTime bastarih =Convert.ToDateTime(gebelikbitistarihi.ToShortDateString());
//                    foreach (TakvimSablonSatiri satir in sablonsatirlari)
//                    {

//                        bastarih = bastarih.AddDays(satir.IlkIzlemdenSonrakiSure);
//                        takvim = Utility.RandevuOlustur(hasta, bastarih, null, myenum.IslemTuru.Izlem, sablon.SablonTuru, "Hastaya " + sablon.Adi + "in " + satir.Adi + " yapılacaktır");
//                        if (System.DateTime.Today >= takvim.BasTarih)
//                            continue;
//                        if (takvim.Id > 0)
//                        {
//                            foreach (TakvimSatiri yakvimsatiri in takvim.TakvimSatirlari)
//                            {
//                                yakvimsatiri.Takvim.Id = takvim.Id;
//                                yakvimsatiri.Insert();
//                            }
//                        }
//                        else
//                        {
//                            takvim.Insert();
//                            foreach (TakvimSatiri yakvimsatiri in takvim.TakvimSatirlari)
//                            {
//                                yakvimsatiri.Takvim.Id = takvim.Id;
//                                yakvimsatiri.Insert();
//                            }

//                        }
//                        if (Ilkrandevu == null)
//                            Ilkrandevu = takvim;

//                    }
//                }
//            }
//            return Ilkrandevu;
//        }

//        public static Takvim ObezRandevulariniOlustur(TakvimSablonu sablon, Hasta hasta, DateTime islemtarihi)
//        {
//            Takvim Ilkrandevu = null;
//            Takvim takvim = null;

//            if (sablon != null)
//            {
//                TakvimSablonSatiri[] sablonsatirlari = Persistence.ReadList<TakvimSablonSatiri>("Select * from TakvimSablonSatiri where TakvimSablonu_Id=@prm0 and TakvimSablonSatiri.Aktif=1 order by TakvimSablonSatiri.IzlemSıraNo asc", new object[] { sablon.Id });
//                if (sablonsatirlari != null && sablonsatirlari.Length > 0)
//                {
//                    DateTime bastarih = Convert.ToDateTime(islemtarihi.ToShortDateString());
//                    foreach (TakvimSablonSatiri satir in sablonsatirlari)
//                    {

//                        bastarih = bastarih.AddDays(satir.IlkIzlemdenSonrakiSure);
//                        takvim = Utility.RandevuOlustur(hasta, bastarih, null, myenum.IslemTuru.Izlem, sablon.SablonTuru, "Hastaya " + sablon.Adi + "in " + satir.Adi + " yapılacaktır");
//                        if (System.DateTime.Today >= takvim.BasTarih)
//                            continue;
//                        if (takvim.Id > 0)
//                        {
//                            foreach (TakvimSatiri yakvimsatiri in takvim.TakvimSatirlari)
//                            {
//                                yakvimsatiri.Takvim.Id = takvim.Id;
//                                yakvimsatiri.Insert();
//                            }
//                        }
//                        else
//                        {
//                            takvim.Insert();
//                            foreach (TakvimSatiri yakvimsatiri in takvim.TakvimSatirlari)
//                            {
//                                yakvimsatiri.Takvim.Id = takvim.Id;
//                                yakvimsatiri.Insert();
//                            }

//                        }
//                        if (Ilkrandevu == null)
//                            Ilkrandevu = takvim;

//                    }
//                }
//            }
//            return Ilkrandevu;
//        }

//        public static Takvim KadınRandevulariniOlustur(TakvimSablonu sablon, Hasta hasta, DateTime islemtarihi)
//        {
//            Takvim Ilkrandevu = null;
//            Takvim takvim = null;

//            if (sablon != null)
//            {
//                TakvimSablonSatiri[] sablonsatirlari = Persistence.ReadList<TakvimSablonSatiri>("Select * from TakvimSablonSatiri where TakvimSablonu_Id=@prm0 and TakvimSablonSatiri.Aktif=1 order by TakvimSablonSatiri.IzlemSıraNo asc", new object[] { sablon.Id });
//                if (sablonsatirlari != null && sablonsatirlari.Length > 0)
//                {
//                    DateTime bastarih = Convert.ToDateTime(islemtarihi.ToShortDateString());
//                    foreach (TakvimSablonSatiri satir in sablonsatirlari)
//                    {
//                        bastarih=bastarih.AddDays(satir.IlkIzlemdenSonrakiSure);
//                        takvim=Utility.RandevuOlustur(hasta, bastarih, null, myenum.IslemTuru.Izlem, sablon.SablonTuru,  "Hastaya " + sablon.Adi + "in " + satir.Adi+" yapılacaktır");
//                        if (System.DateTime.Today >= takvim.BasTarih)
//                            continue;
//                        if (takvim.Id > 0)
//                        {
//                            foreach (TakvimSatiri yakvimsatiri in takvim.TakvimSatirlari)
//                            {
//                                yakvimsatiri.Takvim.Id = takvim.Id;
//                                yakvimsatiri.Insert();
//                            }
//                        }
//                        else
//                        {
//                            takvim.Insert();
//                            foreach (TakvimSatiri yakvimsatiri in takvim.TakvimSatirlari)
//                            {
//                                yakvimsatiri.Takvim.Id = takvim.Id;
//                                yakvimsatiri.Insert();
//                            }
                               
//                        }
//                        if (Ilkrandevu == null)
//                            Ilkrandevu = takvim;
//                    }
//                }
//            }
//            return Ilkrandevu;
//        }

        

        

//        public static Takvim RandevuGuncelle(Takvim takvim,DateTime bastarih,myenum.IslemTuru islemturu,myenum.IzlemTuru izlemturu,AsiOzellikTanim asiozelliktanim)
//        {
//            if (takvim.RandevuDurumu == myenum.RandevuDurumu.İptalEdildi)
//            {
//                takvim.RandevuDurumu = myenum.RandevuDurumu.İptalEdildi;
//                TakvimSatiri[] eskirandevusatirlari = Persistence.ReadList<TakvimSatiri>("Select * from TakvimSatiri Where Takvim_Id=@prm0 and Aktif=1", new object[] { takvim.Id });
//                //randevu satırlarını da pasif yapalım.
//                takvim.TakvimSatirlari.AddRange(eskirandevusatirlari);
//                foreach (TakvimSatiri satir in takvim.TakvimSatirlari)
//                {
//                    satir.Aktif = false;
                    
//                    satir.Update();
//                }
//                takvim.ValidateYapma = true;
//                takvim.Update();
//                return takvim;
//            }
            
//            Takvim eskirandevu = takvim;
//            Takvim[] randevular = Utility.IsPlanlananTarihteHastaninRandevusuVar(takvim.Hasta, bastarih, takvim.Doktor, takvim.Id);

//            if (randevular != null && randevular.Length > 0)
//            {
//                ///Bu tarih ve saatte bu hastanın seçili doktordan randevusu var.
//                ///Randevu tarihini eski satırlarada yansıtıyoruz.
//                takvim = randevular[0];
//                TakvimSatiri[] eskirandevusatirlari = Persistence.ReadList<TakvimSatiri>("Select * from TakvimSatiri Where Takvim_Id=@prm0 and Aktif=1", new object[] { eskirandevu.Id });
//                //eski randevu satırlarını yeni randevuya ekleyeceğiz.
//                takvim.TakvimSatirlari.AddRange(eskirandevusatirlari);
//                foreach (TakvimSatiri satir in takvim.TakvimSatirlari)
//                {
//                    satir.Takvim.Id = takvim.Id;
//                    satir.PlanlananTarih = takvim.BasTarih;
//                    satir.Update();
//                }
//                //
//                eskirandevu.Read();
//                eskirandevu.RandevuDurumu = myenum.RandevuDurumu.İptalEdildi;
//                eskirandevu.Aciklama = "Başka bir randevu ile birleştirildi.";
//                eskirandevu.Aktif = false;
//                eskirandevu.ValidateYapma = true;
//                eskirandevu.Update();

//            }
//            else
//            {
//                ///Bu tarih ve saatte bu hastanın seçili doktordan randevusu yok.
//                takvim.BasTarih = bastarih;
//                Randevu randevubilgisi = Utility.GetRandevu(takvim.BasTarih, takvim.Doktor, takvim.Hasta.Id, 0);
//                takvim.Saat = randevubilgisi.Saat.ToString().Substring(0, 5);
//                takvim.SiraNo = randevubilgisi.SiraNo;
               

//                TakvimSatiri[] satirlar = Persistence.ReadList<TakvimSatiri>("Select * from TakvimSatiri Where Takvim_Id=@prm0 and Aktif=1", new object[] { takvim.Id });
//                takvim.TakvimSatirlari.AddRange(satirlar);
//                foreach (TakvimSatiri satir in takvim.TakvimSatirlari)
//                {  
//                    satir.PlanlananTarih = takvim.BasTarih;
//                    eskirandevu.ValidateYapma = true;
//                    satir.Update();
//                }
//                takvim.ValidateYapma = true;
//                takvim.Update();
//            }
//            return takvim;

//        }

//        public static void RandevuBilgisiGoster(Takvim randevu)
//        {
//            StringBuilder str = new StringBuilder();
//            str.Append("Randevu Tarihi  :" + randevu.BasTarih.ToShortDateString() + "\n");
//            str.Append("Randevu Sıra No :" + randevu.SiraNo + "\n");
//            str.Append("Randevu Saati   :" + randevu.Saat.Substring(0,5) + "\n");
//            MessageBox.Show(str.ToString(), "Sonraki Randevu Bilgileri", MessageBoxButtons.OK, MessageBoxIcon.Information);
//        }

//        public static int AktifMuayeneyiKapat()
//        {
//            int result = 0;
//            if (AktifMuayene.MuayeneDurumu == myenum.MuayeneDurumu.MuayeneEdildi || AktifMuayene.MuayeneDurumu == myenum.MuayeneDurumu.SevkEdildi)
//               result= Transaction.Instance.ExecuteNonQuery("Update Muayene Set MuayeneKapalimi=1,MuayeneKapamaTarihi=getdate() where MuayeneKapalimi=0 and Id=@prm0", new object[] { Current.AktifMuayeneId });
//            else
//                MessageBox.Show("Herhangibir işlem yapılmamış muayene kapatılamaz.");
//            return result;
//        }

//        public static int MuayeneleriKapat(DateTime tarih)
//        {
//            int updatesayisi = 0;
//            if (Current.AktifDoktorId > 0)
//            {
//                updatesayisi = Transaction.Instance.ExecuteNonQuery(
//                    "Update Muayene Set MuayeneDurumu='MuayeneEdildi', MuayeneKapalimi=1,MuayeneKapamaTarihi=getdate() where MuayeneKapalimi=0 and MuayeneTarihi=@prm0 and Doktor_Id=@prm1", new object[] { tarih, Current.AktifDoktorId });
//            }
//            return updatesayisi;
//        }
//        public static int SeciliMuayeneleriKapat(DateTime bastarih,DateTime bittarih)
//        {
//            int updatesayisi = 0;
//            if (Current.AktifDoktorId > 0)
//            {
//                updatesayisi = Transaction.Instance.ExecuteNonQuery(
//                    "Update Muayene Set MuayeneDurumu='MuayeneEdildi',MuayeneKapalimi=1,MuayeneKapamaTarihi=getdate() where MuayeneKapalimi=0 and MuayeneTarihi between @prm0 and @prm1+1 and Doktor_Id=@prm2", 
//                    new object[] { bastarih, bittarih,Current.AktifDoktorId });
//            }
//            return updatesayisi;
//        }

//        /// <summary>
//        /// Tc kimlik numarası olmayan ve Yabancı olmayan yani pasaportnumarası ve kimlik numarası olmayan hastalara bir numara vermek için
//        /// Kullanılıyor. Bu numara kimlik numarası olunca tüm tablolarda kimlik numarası ile update edilecek.
//        /// </summary>
//        /// <returns></returns>
//        public static long GetHastaId()
//        {
            
//            long Id = 0;


//            int kayitvarmi=Transaction.Instance.ExecuteScalarI("select count(Id )from HastaId");
//            if (kayitvarmi == 0)
//            {
//                HastaId hastaId = new HastaId();
//                hastaId.Id = 1;
//                hastaId.SonKullanilanId = 1000000000000;
//                hastaId.Insert();
//            }
            
            
//            if (Configuration.GetValue("DbType") != "System.Data.Sqlite")
//                Id = Transaction.Instance.ExecuteScalarL("select isnull(max(SonKullanilanId),0)+1 from HastaId");
//            else
//                Id = Transaction.Instance.ExecuteScalarL("select ifnull(max(SonKullanilanId),0)+1 from HastaId");
           
//           int update = Transaction.Instance.ExecuteNonQuery("Update HastaId set SonKullanilanId=" + Id + " where Id=" + 1);
//           if (0>=update)
//               throw new Exception("Id alınırken hata oluştu");
//            return Id;
//        }

//        /// <summary>
//        /// Hastaya verilen geçici Idnin Tckimlik no ile update için gerekli scriptler eğer yeni bir tablo eklenirse 
//        /// Onunda kodunu buraya eklemek gerekiyor.
//        /// Atilla abinin önerisi
//        /// </summary>
//        /// <param name="eskiId"></param>
//        /// <param name="yeniId"></param>
//        /// <returns></returns>
//        public static int HastaIdUpdate(long eskiId, long yeniId)
//        {
//            ///Kimlik yada pasaportnumarası değil; 
           

//            int updatesayisi = 0;
//            ///Eğer yeni bir tablo eklenirse için hasta olan mutlaka buradaki update eklenmelidir.
//            string updatescripti = @"
//            Update Anamnez set Hasta_Id=@prm1 
//            where Hasta_Id=@prm0
//            
//            Update BebekCocukBeslenme set Hasta_Id=@prm1 
//            where Hasta_Id=@prm0
//            
//            Update BebekCocukBilgi set Hasta_Id=@prm1 
//            where Hasta_Id=@prm0
//            
//            Update BebekIzleme set Hasta_Id=@prm1 
//            where Hasta_Id=@prm0
//            
//            Update GebeBaslangic set Hasta_Id=@prm1 
//            where Hasta_Id=@prm0
//            
//            Update GebeIzleme set Hasta_Id=@prm1 
//            where Hasta_Id=@prm0
//
//            Update GebeSonuc set Hasta_Id=@prm1 
//            where Hasta_Id=@prm0
//
//            Update Hasta set Id=@prm1 
//            where Id=@prm0
//
//            Update HastaAliskanlik set Hasta_Id=@prm1 
//            where Hasta_Id=@prm0
//
//            Update HastaOzluk set Hasta_Id=@prm1 
//            where Hasta_Id=@prm0
//
//            Update KadinIzleme set Hasta_Id=@prm1 
//            where Hasta_Id=@prm0
//
//            Update KadinSistemikHastaliklar set Hasta_Id=@prm1 
//            where Hasta_Id=@prm0
//
//            Update LohusaIzleme set Hasta_Id=@prm1
//            where Hasta_Id=@prm0
//
//            Update Muayene set Hasta_Id=@prm1 
//            where Hasta_Id=@prm0
//
//            Update MuayeneAsi set Hasta_Id=@prm1 
//            where Hasta_Id=@prm0
//
//            Update MuayeneHizmet set Hasta_Id=@prm1 
//            where Hasta_Id=@prm0
//
//            Update MuayeneSevk set Hasta_Id=@prm1 
//            where Hasta_Id=@prm0
//
//            Update MuayeneTeshis set Hasta_Id=@prm1 
//            where Hasta_Id=@prm0
//
//            Update MuayeneTetkik set Hasta_Id=@prm1 
//            where Hasta_Id=@prm0
//
//            Update MuayeneTetkikDosya set Hasta_Id=@prm1 
//            where Hasta_Id=@prm0
//
//            Update ObezIzleme set Hasta_Id=@prm1 
//            where Hasta_Id=@prm0
//
//            Update OlumBildirimi set Hasta_Id=@prm1 
//            where Hasta_Id=@prm0
//
//            Update Recete set Hasta_Id=@prm1 
//            where Hasta_Id=@prm0
//
//            Update Receteilac set Hasta_Id=@prm1 
//            where Hasta_Id=@prm0
//
//            Update SaglikIstirahat set Hasta_Id=@prm1 
//            where Hasta_Id=@prm0
//
//            Update Takvim set Hasta_Id=@prm1 
//            where Hasta_Id=@prm0 ";

//            updatescripti = updatescripti.Replace("@prm0", eskiId.ToString()).Replace("@prm1", yeniId.ToString());

//            updatesayisi=Transaction.Instance.ExecuteNonQuery(updatescripti);

//            return updatesayisi;
//        }


//        public static DataTable GetProtokolDefteri(DateTime bastarih,DateTime bittarih,long doktorId)
//        {
//            if (bastarih == System.DateTime.MinValue || bittarih == System.DateTime.MinValue)
//                throw new Exception("Tarih düzgün seçilmedi");
//            if(doktorId==0)
//                throw new Exception("Doktor düzgün seçilmedi");


             
//            DataTable dt = Transaction.Instance.ExecuteSql(@" set dateformat dmy;
//                                    SELECT
//	                                     M.SiraNo									AS SIRANO
//                                        ,M.MuayeneTarihi							AS MTARIHI
//                                        ,(SELECT SAAT FROM Takvim T WHERE T.ID=M.RANDEVU_ID) AS MURACATSAATI
//                                        ,convert(nvarchar(5),M.EklemeTarihi,108)	AS MUAYENESAATI
//                                        ,DATEDIFF(YY,H.DogumTarihi,GETDATE())		AS YASI
//                                        ,M.ProtokolNo								AS PROTOKOLNO
//                                        ,H.Adi+' '+H.Soyadi							AS ADISOYADI
//                                        ,H.Cinsiyeti								AS CINSIYET 
//                                        ,ISNULL(H.BeyanDogumTarihi,H.DogumTarihi)   AS DOGUMTARIHI
//                                        ,H.KurumTipi								AS KURUMTIPI
//                                        ,dbo.fn_MuayeneSonucu (M.Id)				AS MUAYENESONUCU
//                                        ,dbo.FN_GETTANI(M.Id)						AS TANI
//                                        ,dbo.FN_GETILAC(M.Id)						AS ILAC
//                                        ,dbo.FN_GETRAPOR(M.Id)						AS RAPOR
//                                        ,dbo.FN_GETSEVK(M.Id)						AS SEVK
//                                        ,D.Adi+' '+D.Soyadi							AS DADISOYADI
//                                        ,D.Diplomano								AS DIPLOMANO
//                                    FROM Muayene M
//                                    INNER JOIN Hasta  H ON H.Id=M.Hasta_Id 
//                                    INNER JOIN Doktor D ON D.ID=dbo.iszero(M.VekilDoktor_Id,M.Doktor_Id) 
//                                    WHERE
//                                    M.MuayeneTarihi between @prm0 and @prm1	
//                                    AND (M.Doktor_Id=@prm2 OR M.VekilDoktor_Id=@prm2)	
//                                    AND M.MuayeneKapalimi=1
//                                    AND M.Aktif=1 ", new object[] { bastarih, bittarih, doktorId });

//            return dt;
//        }


//        public static DataTable GetHastaDagilimlari()
//        {

//            DataTable dt = new DataTable();

//            if (Current.AktifDoktorId > 0)
//            {



//                dt = Transaction.Instance.ExecuteSql(@" 
// 
//                       	IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '#temptable') 
//						DROP TABLE #temptable;
//
//                        declare @BebekHastaSayisi		decimal
//                        declare @CocukHastaSayisi		decimal
//                        declare @GebeHastaSayisi		decimal
//                        declare @GeziciHastaSayisi		decimal
//                        declare @KadınHastaSayisi		decimal
//                        declare @KayitliHastaSayisi		decimal
//                        declare @KentselHastaSayisi		decimal
//                        declare @LohusaHastaSayisi		decimal
//						declare @MisafirHastaSayisi		decimal
//						declare @HaneSayisi		        int
//                    
//
//
//                        set @BebekHastaSayisi	=0	
//                        set	@CocukHastaSayisi	=0		
//                        set	@GebeHastaSayisi	=0		
//                        set	@GeziciHastaSayisi	=0		
//                        set	@KadınHastaSayisi	=0	
//                        set	@KayitliHastaSayisi	=0	
//                        set	@KentselHastaSayisi	=0	
//                        set	@LohusaHastaSayisi	=0
//						set	@MisafirHastaSayisi	=0
//						set	@HaneSayisi	=0
//                      
//	
//
//                        Select
//
//                         @BebekHastaSayisi=SUM(B.BebekHastaSayisi) 
//                        ,@CocukHastaSayisi=SUM(B.CocukHastaSayisi) 
//                        ,@GebeHastaSayisi=SUM(B.GebeHastaSayisi) 
//                        ,@GeziciHastaSayisi=SUM(B.GeziciHastaSayisi) 
//                        ,@KadınHastaSayisi=SUM(B.KadınHastaSayisi)
//                        ,@KayitliHastaSayisi=SUM(B.KayitliHastaSayisi)
//                        ,@KentselHastaSayisi=SUM(B.KentselHastaSayisi) 
//                        ,@LohusaHastaSayisi=SUM(B.LohusaHastaSayisi)
//						,@MisafirHastaSayisi=SUM(B.MisafirHastaSayisi)
//						,@HaneSayisi=count(distinct TUIKAdresNo)
//
//
//                        from 
//                        (
//                        Select 
//                         sum(Case when KayitDurumu='Kayitli' and 365>DATEDIFF(dd,DogumTarihi,getdate()) then 1 else 0  end) BebekHastaSayisi
//                        ,sum(Case when KayitDurumu='Kayitli' and DATEDIFF(dd,DogumTarihi,getdate()) between 365 and 1826 then 1 else 0  end) CocukHastaSayisi
//                        ,sum(Case when KayitDurumu='Kayitli' and Cinsiyeti='Kadın' and DATEDIFF(DD,DogumTarihi,getdate())/365 between 15 and 49 then 1 else 0 end) KadınHastaSayisi
//                        ,sum(case when KayitDurumu='Kayitli' and GeziciHizmetVerilenHasta=1 then 1 else 0 end) GeziciHastaSayisi
//                        ,sum(case when KayitDurumu='Kayitli' and GeziciHizmetVerilenHasta=0 then 1 else 0 end) KentselHastaSayisi
//                        ,sum(Case when KayitDurumu='Kayitli' then 1 else 0 end) as KayitliHastaSayisi
//                        ,sum(Case when KayitDurumu='Misafir' then 1 else 0 end) as MisafirHastaSayisi
//                        ,TUIKAdresNo
//                        ,
//
//                        (isnull(((( 
//                            Select  
//		                        count(Id)  
//                            from GebeBaslangic gb 
//	                        where gb.IsAutoImport=0 and
//                            gb.Hasta_Id=Hasta.Id 
//                            and gb.GebelikDurumu='Basladi' 
//                            and gb.Aktif=1
//                        ))),0)) as GebeHastaSayisi,
//                        (case when 
//			                        DATEDIFF(dd,
//				                        (
//					                        Select 
//						                        max(gb.GebelikSonuclanmaTarihi) 
//					                        from GebeBaslangic gb
//					                        where gb.IsAutoImport=0 and Hasta_Id=Hasta.Id 
//					                        and gb.GebelikDurumu='Bitti' 
//					                        and gb.Aktif=1)
//			                        ,GETDATE())<43 
//		                        then 1 else 0 end) as LohusaHastaSayisi
//
//                        from Hasta
//                        where
//	                        Hasta.Doktor_Id=@prm0
//	                        and Hasta.Aktif=1
//                        group by Hasta.Id,TUIKAdresNo
//                        ) B
//
//
//                        create table #temptable 
//                        (
//	                        HastaTuru varchar(100),
//	                        HastaAdedi decimal
//
//                        )
//
//                        insert into #temptable (HastaTuru,HastaAdedi) values ('Kesin Kayıtlı Sayısı',@KayitliHastaSayisi)
//                        insert into #temptable (HastaTuru,HastaAdedi) values ('Kesin Kayıtlı Kentsel Sayısı',@KentselHastaSayisi)
//                        insert into #temptable (HastaTuru,HastaAdedi) values ('Kesin Kayıtlı Gezici Sayısı',@GeziciHastaSayisi)
//                        insert into #temptable (HastaTuru,HastaAdedi) values ('Misafir Sayisi',@MisafirHastaSayisi)
//                        insert into #temptable (HastaTuru,HastaAdedi) values ('Kesin Kayıtlı 15-49 Kadın Sayısı',@KadınHastaSayisi)
//                        insert into #temptable (HastaTuru,HastaAdedi) values ('Kesin Kayıtlı Gebe Sayısı',@GebeHastaSayisi)
//                        insert into #temptable (HastaTuru,HastaAdedi) values ('Kesin Kayıtlı Lohusa Sayısı',@LohusaHastaSayisi)
//                        insert into #temptable (HastaTuru,HastaAdedi) values ('Kesin Kayıtlı Bebek Sayısı',@BebekHastaSayisi)
//                        insert into #temptable (HastaTuru,HastaAdedi) values ('Kesin Kayıtlı Çocuk Sayısı',@CocukHastaSayisi)
//                        insert into #temptable (HastaTuru,HastaAdedi) values ('TUIK Bilgisi Gelen Hane Adedi',@HaneSayisi)
//                       
//                       
//                     
//                   
//						
//
//                        select * from #temptable", new object[] { Current.AktifDoktorId });
//            }

//            return dt;
//        }





        public static bool HasRight (myenum.GorevTuru rol, myenum.Hak hak)
        {

            if (rol == myenum.GorevTuru.Admin || rol == myenum.GorevTuru.AileHekimi)
                return true;

            RolHakki[] rolhaklari = Persistence.ReadList<RolHakki>("Select * from RolHakki where Rol=@prm0 and Aktif=1 and Hak=@prm1", new object[] {rol.ToString(),hak.ToString() });
            if (rolhaklari != null && rolhaklari.Length > 0)
            {
                return true;
            }
            else
                return false;
        }
    }
}
