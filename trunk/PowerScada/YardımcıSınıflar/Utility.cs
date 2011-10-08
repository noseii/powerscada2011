using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SharpBullet.OAL;
using SharpBullet.DAL;
using mymodel;
using SharpBullet.UI;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Reflection;
using DevExpress.XtraEditors;
using PowerScada.Properties;
using System.Xml;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;

namespace PowerScada
{
    public  class Utility
    {
        //public static class DateTimeFormatter : IFormatProvider, ICustomFormatter
        //{
        //    public object GetFormat(Type formatType)
        //    {
        //        if (formatType == typeof(ICustomFormatter))
        //            return this;
        //        else
        //            return null;
        //    }

        //    public string Format(string format, object arg, IFormatProvider provider)
        //    {
        //        string inputDate = Convert.ToString(arg);
        //        return string.Format("{0}/{1}/{2} {3}:{4}:{5}",
        //            inputDate.Substring(4, 2),
        //            inputDate.Substring(6, 2),
        //            inputDate.Substring(0, 4),
        //            inputDate.Substring(8, 2),
        //            inputDate.Substring(10, 2),
        //            inputDate.Substring(12, 2));
        //    }

        //}
        public static long GetGridToInt(DataRowView currentrow,string columnname)
        {
            long Id = 0;
            if (currentrow != null)
                Id = Convert.ToInt64(currentrow[columnname]);
            return Id;
        }

        public static string GetMakAdres()
        {
            string makadres = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            makadres = makadres.Replace(@"\", "_");
            makadres = makadres.Insert(0, "_");
            makadres = makadres.Insert(0, System.Environment.MachineName);
            return makadres;
        }

        public static DataTable EnumToDataTable(Type enumtype )
        {
            DataTable dtenum=new DataTable();
            
            DataColumn column1 = new DataColumn("Ad", typeof(String));
            DataColumn column2 = new DataColumn("Id", typeof(Int16));
            dtenum.Columns.Add(column1);
            dtenum.Columns.Add(column2);


            Array list = Enum.GetValues(enumtype);

            for (int i = 0; i < list.Length; i++)
            {
                DataRow row = dtenum.NewRow();
                row["Id"] = list.GetValue(i);
                row["Ad"] = Enum.GetName(enumtype, list.GetValue(i));
            }
            return dtenum;
        }

        public static void Combodoldur(DevExpress.XtraEditors.ComboBoxEdit combobox,Type enumtipi,bool Isbosrow=false,string bosrowyazisi="")
        {
            if (Isbosrow)
            {
                combobox.Properties.Items.Add(bosrowyazisi);
                combobox.EditValue = bosrowyazisi;
            }
            Array list = Enum.GetValues(enumtipi);

            for (int i = 0; i < list.Length; i++)
            {
                combobox.Properties.Items.Add(Enum.GetName(enumtipi, list.GetValue(i)));
            }

           
        }

        private static Entity entity;

        /// <summary>
        /// Sadece Griddeki temel alanları saklamak için kullanılacak
        /// </summary>
        /// <returns></returns>
        public static Entity GetEntity()
        {
            if (entity == null)
                return new Entity();
            else
                return entity;


        }

        public static void FillDataSource(string EntityName, string whereClause, string DisplayField, System.Windows.Forms.ComboBox combo, bool EmptyRow, string EmptyMessage)
        {
            string SelectString = whereClause == null ? string.Format("Select  {0},Id  from {1} ", DisplayField, EntityName) : string.Format("Select  {0}  from {1} where {2}", DisplayField, EntityName, whereClause);
            DataTable dt = Transaction.Instance.ExecuteSql(SelectString);
            if (EmptyRow)
            {
                DataRow dr = dt.NewRow();
                dr[DisplayField] = EmptyMessage;
                dr["Id"] = 0;
                dt.Rows.InsertAt(dr, 0);
            }
            combo.DataSource = dt;
            combo.DisplayMember = DisplayField;

        }

        public static void FillEnum(Type tip, System.Windows.Forms.ComboBox combo, bool EmptyRow, string EmptyMessage)
        {
            Array arrayValues = Enum.GetValues(tip);
            Array arrayNames = Enum.GetNames(tip);
            DataTable dt = new DataTable();
            dt.Columns.Add("Ad");
            dt.Columns.Add("Id", typeof(int));
            if (EmptyRow)
            {
                DataRow row = dt.NewRow();
                row["Ad"] = EmptyMessage;
                row["Id"] = 0;
                dt.Rows.Add(row);
            }
            for (int i = 0; i < arrayNames.Length; i++)
            {
                DataRow row = dt.NewRow();
                row["Ad"] = arrayNames.GetValue(i);
                row["Id"] = arrayValues.GetValue(i);
                dt.Rows.Add(row);
            }
            combo.DataSource = dt;
            combo.DisplayMember = "Ad";
            combo.ValueMember = "Id";
        }

        public static DataTable getDataFromXLS(string strFilePath, string excelTabName)
        {
            try
            {
                string strConnectionString = string.Empty;
                strConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFilePath + @";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""";
                OleDbConnection cnCSV = new OleDbConnection(strConnectionString);
                cnCSV.Open();
                OleDbCommand cmdSelect = new OleDbCommand(@"SELECT * FROM [" + excelTabName + "$]", cnCSV);
                OleDbDataAdapter daCSV = new OleDbDataAdapter(); daCSV.SelectCommand = cmdSelect;
                DataTable dtCSV = new DataTable();
                daCSV.Fill(dtCSV);
                cnCSV.Close();
                daCSV = null;
                return dtCSV;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Excel Sayfasının Tab Adı Yanlış");
                return null;
            }
            finally { }
        }


        public static bool IsEmail(string Email)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(strRegex);
            if (re.IsMatch(Email))
                return (true);
            else
                return (false);
        }

        public static bool CheckTcId(string tc, ref string mesaj)
        {
            Int64 tmp, tek_toplam, cift_toplam, deger1, deger2, toplam;

            tmp = Convert.ToInt64(tc.Substring(0, 9));//son 2 basamak hariç
            tek_toplam = Convert.ToInt64(tc.Substring(0, 1)) + Convert.ToInt64(tc.Substring(2, 1)) +
                Convert.ToInt64(tc.Substring(4, 1)) + Convert.ToInt64(tc.Substring(6, 1)) + Convert.ToInt64(tc.Substring(8, 1));
            cift_toplam = Convert.ToInt64(tc.Substring(1, 1)) + Convert.ToInt64(tc.Substring(3, 1)) +
                Convert.ToInt64(tc.Substring(5, 1)) + Convert.ToInt64(tc.Substring(7, 1));
            toplam = (tek_toplam * 3) + cift_toplam;
            deger1 = (10 - (toplam % 10)) % 10;
            tek_toplam = deger1 + Convert.ToInt64(tc.Substring(1, 1)) + Convert.ToInt64(tc.Substring(3, 1)) +
                Convert.ToInt64(tc.Substring(5, 1)) + Convert.ToInt64(tc.Substring(7, 1));
            cift_toplam = Convert.ToInt64(tc.Substring(0, 1)) + Convert.ToInt64(tc.Substring(2, 1)) +
                Convert.ToInt64(tc.Substring(4, 1)) + Convert.ToInt64(tc.Substring(6, 1)) + Convert.ToInt64(tc.Substring(8, 1));
            toplam = (tek_toplam * 3) + cift_toplam;
            deger2 = (10 - (toplam % 10)) % 10;
            tmp = (tmp * 100) + (deger1 * 10) + deger2;
            if (tmp != Convert.ToInt64(tc))
            {
                mesaj = "Tc Kimlik Numarası Tanımsız.! Lütfen Kontrol edin!";
                return false;
            }
            else
            {
                mesaj = "";
                return true;
            }
        }

        //private bool CheckVergiNo(Hasta musteri, ref string mesaj)//-------Vergi/TcNo Bilgileri Kontrol func.
        //{
        //    mesaj = string.Empty;
        //    if (musteri.KayitKimlikDurumu ==mymodel.myenum.KayitKimlikDurumu.TckNoVar)
        //    {
        //        //if (musteri.TckNo.to Length == 11) { }
        //        if (musteri.TckNo.ToString().Length == 0)
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            mesaj = "TC Kimlik No 11 hane girilmelidir!Lütfen Kontrol edin!";
        //            return true;
        //        }
        //        if (IsNumeric(musteri.TckNo.ToString()) == false)
        //        {
        //            mesaj = "TC Kimlik No sadece sayılardan oluşmalıdır!Lütfen kontrol edin!";
        //            return true;

        //        }

        //        return CheckTcId(musteri.TckNo.ToString(), ref mesaj);
        //    }
           
            
            

        //    return false;
        //}

        public static bool IsNumeric(string str)
        {
            try
            {
                str = str.Trim();
                Int64 foo = Int64.Parse(str);
                return (true);
            }
            catch (FormatException e)
            {
                return (false);
            }
        }

        public static string FormName(Type EntityType)
        {
            string formname = "";
            if (EntityType != null)
                formname = EntityType.Name.ToString() + " Listesi ";
            return formname;
        }


        /// <summary>
        /// Verilen celldeki "id" nin değerini dönderir. 
        /// id alanı yoksa ya da hata alırsa 0 dönderir.
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static int ReadGridCell(DataGridViewCell Cell)
        {
            int id = 0;

            if (Cell.Value != null && Cell.Value != System.DBNull.Value)
                id = Convert.ToInt32(Cell.Value);
            else
                return 0;

            return id;

        }

        /// <summary>
        /// Verilen string sayıya çeviriliyorsa çevirir çeviremiyorsa 0 dönderir
        /// isnull ve dbnull contrellerini yapar.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ConvertToInt(string str)
        {

            if (str == "")
                return 0;

            try
            {
                return Convert.ToInt32(str);
            }
            catch (Exception)
            {

                return 0;
            }
            return 0;

        }

        public static Type GetEnumType(string EnumTuru)
        {
            System.Reflection.Assembly a = System.Reflection.Assembly.Load("mymodel");
            Type tip = a.GetType("mymodel.myenum+" + EnumTuru);

            return tip;
        }

        public static decimal ConvertToDecimal(string str)
        {

            if (str == "")
                return Decimal.MinValue;

            try
            {
                return Convert.ToDecimal(str);
            }
            catch (Exception)
            {

                return Decimal.MinValue;
            }
            return Decimal.MinValue;

        }

        #region EditButtonlar için
        private static Dictionary<string, EditbuttonCommand> editbuttoncommands = new Dictionary<string, EditbuttonCommand>();

        public static Dictionary<string, EditbuttonCommand> EditButtonCommands
        {
            get { return editbuttoncommands; }
            set { editbuttoncommands = value; }
        }

        public static void AddEditbuttonCommands()
        {
            long id = 0;
            string deger = string.Empty;
            Utility.EditButtonCommands.Add("EditButtonCihazTuru",
            new EditbuttonCommand(
            delegate(object nesne)
            {
                id = SimpleTreeLookup.Select(Transaction.Instance.ExecuteSql("Select Id,Adi,Kodu,Tip from LookupTable where Aktif=1 and (isnull(Adi,'')) like @prm0", ((EditButton)nesne).Text.Trim() + "%"), "Adi", ref deger);
                if (id > 0)
                {
                    ((EditButton)nesne).Id = id;
                    ((EditButton)nesne).Text = deger;
                }
                else
                {
                    ((EditButton)nesne).Id = 0;
                    ((EditButton)nesne).Text = "";
                }
            }));

            Utility.EditButtonCommands.Add("EditButtonUlkeSec",
            new EditbuttonCommand(
            delegate(object nesne)
            {
                id = SimpleLookup.Select(Transaction.Instance.ExecuteSql("select Id,Adi as [Adı],Kodu from Ulke where aktif=1 and (isnull(Adi,'')) like @prm0", ((EditButton)nesne).Text.Trim() + "%"), "Adı", ref deger);
                if (id > 0)
                {
                    ((EditButton)nesne).Id = id;
                    ((EditButton)nesne).Text = deger;
                }
                else
                {
                    ((EditButton)nesne).Id = 0;
                    ((EditButton)nesne).Text = "";


                }
            }));

            Utility.EditButtonCommands.Add("EditButtonAsiSec",
           new EditbuttonCommand(
           delegate(object nesne)
           {
               id = SimpleLookup.Select(Transaction.Instance.ExecuteSql("select Id,Adi as [Adı],Kodu from AsiTanim where aktif=1 and (isnull(Adi,'')) like @prm0", ((EditButton)nesne).Text.Trim() + "%"), "Adı", ref deger);
               if (id > 0)
               {
                   ((EditButton)nesne).Id = id;
                   ((EditButton)nesne).Text = deger;
               }
               else
               {
                   ((EditButton)nesne).Id = 0;
                   ((EditButton)nesne).Text = "";


               }
           }));

            Utility.EditButtonCommands.Add("EditButtonHasta",
           new EditbuttonCommand(
           delegate(object nesne)
           {
               id = SimpleLookup.Select(Transaction.Instance.ExecuteSql("Select Id,Adi+' '+Soyadi as [AdiSoyadi] from Hasta where (isnull(Adi+' '+Soyadi,'')) like @prm0", ((EditButton)nesne).Text.Trim() + "%"), "AdiSoyadi", ref deger);
               if (id > 0)
               {
                   ((EditButton)nesne).Id = id;
                   ((EditButton)nesne).Text = deger;
               }
               else
               {
                   ((EditButton)nesne).Id = 0;
                   ((EditButton)nesne).Text = "";
               }
           }));

            Utility.EditButtonCommands.Add("EditButtonDoktorSec",
            new EditbuttonCommand(
            delegate(object nesne)
            {
                id = SimpleLookup.Select(Transaction.Instance.ExecuteSql("select Id,Adi+' '+Soyadi as [AdiSoyadi]  from Doktor where (isnull(Adi+' '+Soyadi,'')) like @prm0", ((EditButton)nesne).Text.Trim() + "%"), "AdiSoyadi", ref deger);
                if (id > 0)
                {
                    ((EditButton)nesne).Id = id;
                    ((EditButton)nesne).Text = deger;
                }
                else
                {
                    ((EditButton)nesne).Id = 0;
                    ((EditButton)nesne).Text = "";


                }
            }));


            Utility.EditButtonCommands.Add("EditButtonIlSec",
          new EditbuttonCommand(
          delegate(object nesne)
          {
              id = SimpleLookup.Select(Transaction.Instance.ExecuteSql("select Id,Adi as [Adi]  from il where Adi like @prm0", ((EditButton)nesne).Text.Trim() + "%"), "Adi", ref deger);
              if (id > 0)
              {
                  ((EditButton)nesne).Id = id;
                  ((EditButton)nesne).Text = deger;
              }
              else
              {
                  ((EditButton)nesne).Id = 0;
                  ((EditButton)nesne).Text = "";


              }
          }));

            Utility.EditButtonCommands.Add("EditButtonHasta2",
         new EditbuttonCommand(
         delegate(object nesne)
         {
             SimpleLookup.CommandName = "EditButtonHasta2";
             id = SimpleLookup.Select(Transaction.Instance.ExecuteSql("Select Id,Adi+' '+Soyadi as [AdiSoyadi],CONVERT(NVarChar,TckNo) as TckNo,Adi,Soyadi from Hasta where (isnull(Adi+' '+Soyadi,'')) like @prm0", ((EditButton)nesne).Text.Trim() + "%"), "AdiSoyadi", ref deger);
             if (id > 0)
             {
                 ((EditButton)nesne).Id = id;
                 ((EditButton)nesne).Text = deger;
             }
             else
             {
                 ((EditButton)nesne).Id = 0;
                 ((EditButton)nesne).Text = "";
             }
         }));
        }

        #endregion

        #region GridStyle

        public static void SetGridStyle(DevExpress.XtraGrid.Views.Grid.GridView view)
        {

            for (int i = 0; i < view.VisibleColumns.Count; i++)
            {
                if (view.VisibleColumns[i].FieldName==("Aktif") ||
                    view.VisibleColumns[i].FieldName==("EklemeTarihi") ||
                    view.VisibleColumns[i].FieldName==("DegistirmeTarihi") ||
                    view.VisibleColumns[i].FieldName==("EkleyenMakAdres") ||
                    view.VisibleColumns[i].FieldName==("EkleyenKullanici") ||
                    view.VisibleColumns[i].FieldName==("DegistirenKullanici") ||
                    view.VisibleColumns[i].FieldName==("DegistirenMakAdres") ||
                    view.VisibleColumns[i].FieldName==("Id") ||
                    view.VisibleColumns[i].FieldName==("RowVersion") ||
                    view.VisibleColumns[i].FieldName==("TransferDurumu") ||
                    view.VisibleColumns[i].FieldName==("TransferMesaj") ||
                    view.VisibleColumns[i].FieldName==("TransferSonuc") ||
                    view.VisibleColumns[i].FieldName==("TransferTarihi") ||
                    view.VisibleColumns[i].FieldName==("_"))
                {
                    view.VisibleColumns[i].Visible = false;
                }


            }
        }

        public static void SetGridStyle(DevExpress.XtraGrid.Views.Card.CardView view)
        {
           
            for (int i = 0; i < view.VisibleColumns.Count; i++)
            {
                if (view.VisibleColumns[i].Name.Contains("Aktif") ||
                    view.VisibleColumns[i].Name.Contains("EklemeTarihi") ||
                    view.VisibleColumns[i].Name.Contains("DegistirmeTarihi") ||
                    view.VisibleColumns[i].Name.Contains("EkleyenMakAdres") ||
                    view.VisibleColumns[i].Name.Contains("EkleyenKullanici") ||
                    view.VisibleColumns[i].Name.Contains("DegistirenKullanici") ||
                    view.VisibleColumns[i].Name.Contains("DegistirenMakAdres") ||
                    view.VisibleColumns[i].Name.Contains("Id") ||
                    view.VisibleColumns[i].Name.Contains("RowVersion") ||
                    view.VisibleColumns[i].Name.Contains("TransferDurumu") ||
                    view.VisibleColumns[i].Name.Contains("TransferMesaj") ||
                    view.VisibleColumns[i].Name.Contains("TransferSonuc") ||
                    view.VisibleColumns[i].Name.Contains("TransferTarihi") ||
                    view.VisibleColumns[i].Name.Contains("_"))
                {
                    view.VisibleColumns[i].Visible = false;
                }

             
        }
        }

        public static void SetGridStyle(DevExpress.XtraTreeList.TreeList treelist)
        {


            for (int i = 0; i < treelist.Columns.Count; i++)
            {
                if (treelist.Columns[i].Name.Contains("Aktif") ||
                    treelist.Columns[i].Name.Contains("EklemeTarihi") ||
                    treelist.Columns[i].Name.Contains("DegistirmeTarihi") ||
                    treelist.Columns[i].Name.Contains("EkleyenMakAdres") ||
                    treelist.Columns[i].Name.Contains("EkleyenKullanici") ||
                    treelist.Columns[i].Name.Contains("DegistirenKullanici") ||
                    treelist.Columns[i].Name.Contains("DegistirenMakAdres") ||
                    treelist.Columns[i].Name.Contains("Id") ||
                    treelist.Columns[i].Name.Contains("RowVersion") ||
                    treelist.Columns[i].Name.Contains("TransferDurumu") ||
                    treelist.Columns[i].Name.Contains("TransferMesaj") ||
                    treelist.Columns[i].Name.Contains("TransferSonuc") ||
                    treelist.Columns[i].Name.Contains("TransferTarihi") ||
                    treelist.Columns[i].Name.Contains("_"))
                {
                    treelist.Columns[i].Visible = false;
                }
            }
        }

        #endregion



        public static string GetProtokolNo()
        {
           
            string protokolno=DateTime.Now.ToString();

            protokolno = System.DateTime.Now.ToString().Substring(8, 2).ToString() +

                System.DateTime.Now.ToString().Substring(3, 2).ToString() +
                System.DateTime.Now.ToString().Substring(0, 2).ToString() +
                  System.DateTime.Now.ToString().Substring(11, 2).ToString() +
                   System.DateTime.Now.ToString().Substring(14, 2).ToString() +
                   System.DateTime.Now.ToString().Substring(17, 2).ToString();
       
         

          
            
            
            
           
            
            
            return protokolno;
        }

        public static short GetSiraNo()
        {
            int sirano = 0;

            //TODO:Burada gün bazında ve doktora göre veriliyor sıra numarası bugün hangi doktor kaç tane muayene yapmış sorusunu cevabı için
            //Tarih,Doktor,Ve Sıra numarasına göre index eklenebilir uniq böylece sıra numarasının unigliği sağlanabilir.
            //sirano=Transaction.Instance.ExecuteScalarI(
            //    "select Max(isnull(SiraNo,0)) from Muayene Where Aktif=1 and MuayeneTarihi=@prm0 and Doktor_Id=@prm1", new object[] {DateTime.Today,Current.AktifDoktorId });
            //sirano ++;

            

            return (short)sirano;
        }

//        //TODO:Muayeden Bağımsız aşı olabilir. Peki O zaman biz muayeneden bağımsız aşıları nerde göstereceğiz.
//        //Var olan sekmeden geöstersek yanlış olmazmı sanki o muayeneden yapılmış gibi görünün sakıncalı gibi...

//        //TODO:aynı aşıdan aynı günde aynı yaş aralıklarına iki tane aşı vurulabiliyor. bunu engellemelimiyiz.
//        //Sıra numaraarı 1,2 verilerek vurulabiliyor. Aynı gün aynı aşıdan iki taneye izin vermemelimiyiz.
//        //Alanlara göre unigindex ekelyerek bunu önleyebiliriz.
//        public static Sonuc AsiTakvimiolustur(long hastaid)
//        {
//            int kayitliasivarmi = Transaction.Instance.ExecuteScalarI("Select count(Id) from TakvimSatiri where TakvimSatiri.Hasta_Id=@prm0 and TakvimSatiri.Asi_Id is not null and TakvimSatiri.IslemTuru=@prm1 and TakvimSatiri.IzlemTuru=@prm1 and TakvimSatiri.Aktif=1 ", new object[] { Current.AktifHastaId, myenum.IslemTuru.Asi.ToString(), myenum.IzlemTuru.Asi.ToString() });
//               if (kayitliasivarmi == 0)
//               {

//                   AsiOzellikTanim[] asiozelliktanim = Persistence.ReadList<AsiOzellikTanim>("Select AsiOzellikTanim.* from AsiOzellikTanim inner join Asitanim at on at.Id=AsiOzellikTanim.Asitanim_Id  where AsiOzellikTanim.Zorunlumu=1 and AsiOzellikTanim.Aktif=1 order by at.TakvimSirasi,AsiTanim_Id,AsiSira");
//                   Hasta hasta = Hasta.HastaOku(hastaid);
//                   if (hasta == null || hasta.Id == 0)
//                   {
//                       return new Sonuc(true, "Sistemde kayıtlı hasta kaydı yok");
//                   }
//                   else
//                   {
//                       if (hasta.KayitDurumu == myenum.KayitDurumu.Misafir)
//                       {
                           
//                           return new Sonuc(true, "Misafir Hastalar içinAşı takvimi oluşturulamaz.");
//                       }
//                   }

//                   DateTime dogumtarihi = hasta.DogumTarihi;
//                   if (hasta.BeyanDogumTarihi != System.DateTime.MinValue)
//                       dogumtarihi = hasta.BeyanDogumTarihi;

//                   int yas = (DateTime.Today.Subtract(dogumtarihi)).Days / 365;
//                   DateTime planlananTarih;
//                   int girilenkayitsayisi = 0;
//                   foreach (AsiOzellikTanim ozelliktanim in asiozelliktanim)
//                   {
//                           bool yasikontroletme = false;
//                           if (ozelliktanim.MinimumYas == 0 && ozelliktanim.MaximumYas == 0)
//                               yasikontroletme = true;

//                           if(!yasikontroletme)
//                             if (yas < ozelliktanim.MinimumYas || yas > ozelliktanim.MaximumYas)
//                                continue;
                               
                     
//                           planlananTarih = hasta.DogumTarihi.AddDays(ozelliktanim.DogumdanItibarenSure);

//                           if (planlananTarih < DateTime.Today)
//                               continue;

//                           Takvim takvim =Utility.RandevuOlustur(hasta, planlananTarih,ozelliktanim,myenum.IslemTuru.Asi,myenum.IzlemTuru.Asi,"");
//                           if (takvim.Id > 0)
//                           {
//                               foreach (TakvimSatiri satir in takvim.TakvimSatirlari)
//                               {
//                                   satir.Insert();
//                               }
//                           }
//                           else
//                           {
//                               takvim.Insert();
//                               foreach (TakvimSatiri satir in takvim.TakvimSatirlari)
//                               {
//                                   satir.Takvim.Id = takvim.Id;
//                                   satir.Insert();
//                               }
//                               girilenkayitsayisi += 1;
//                           }
                               
                       

//                   }
//                   if(girilenkayitsayisi>0)
//                        return new Sonuc(false, "Hastaya ait aşı takvimi oluşturuldu.");        
//                   else
//                       return new Sonuc(true, "bu hastaya vurulacak uygun aşı bulunamadı.");
//               }
//               else
//                   return new Sonuc(true, "bu hastaya ait aşı takvimi daha önce oluşturulmuş.");
//        }

//        public static Takvim RandevuOlustur(Hasta hasta, DateTime planlananTarih, AsiOzellikTanim ozelliktanim, myenum.IslemTuru islemturu, myenum.IzlemTuru izlemturu,string aciklama)
//        {
//            Takvim takvim;

//            Doktor doktor = Utility.GetVekilDoktor(hasta, planlananTarih);
          
            
//            ///Daha öncede aynı hastaya aynı doktordan bu tarihte randevu var ise sadece satıra ekleyelim bilgiyi aynı hastaya 
//            ///aynı gün için tekrar randevu vermeyelim.
//            Takvim[] randevu = IsPlanlananTarihteHastaninRandevusuVar(hasta, planlananTarih, doktor,0);

//            if (randevu != null && randevu.Length > 0)
//            {
//               takvim=randevu[0];
//               //TakvimSatiri[] satir = Persistence.ReadList<TakvimSatiri>("Select * from TakvimSatiri where Randevu_Id=@prm0 and TakvimSatiri.Aktif=1 ",new object[]{takvim.Id});
//               //takvim.TakvimSatirlari.AddRange(satir);
//               takvim.TakvimSatirlari.Add(RandevuSatiri(hasta, planlananTarih, ozelliktanim, islemturu, izlemturu, takvim, aciklama));
//            }
//            else
//            {
//                takvim = new Takvim();
//                takvim.Hasta = hasta;
//                takvim.Hasta.Id = hasta.Id;
//                takvim.BasTarih = planlananTarih;
//                takvim.Doktor.Id = doktor.Id;
//                takvim.Doktor = doktor;
//                Randevu randevubilgisi = Utility.GetRandevu(takvim.BasTarih, takvim.Doktor, takvim.Hasta.Id, 0);
//                takvim.Saat = randevubilgisi.Saat.ToString().Substring(0, 5);
//                takvim.SiraNo = randevubilgisi.SiraNo;
//                takvim.RandevuDurumu = myenum.RandevuDurumu.Verildi;

//                takvim.TakvimSatirlari.Add(RandevuSatiri(hasta, planlananTarih, ozelliktanim, islemturu, izlemturu, takvim, aciklama));
//                //takvim.Konu = "Hastanın " + ozelliktanim.AsiTanim.Adi + " Adlı Aşısı Yapılacaktır.";
//                //takvim.Aciklama = "Hastaya Kodu:" + ozelliktanim.AsiTanim.Kodu + " ve Adi:" + ozelliktanim.AsiTanim.Adi + " olan aşı yapılacaktır."; 
                       
//            }

//            return takvim;
//        }

//        public static Takvim[] IsPlanlananTarihteHastaninRandevusuVar(Hasta hasta, DateTime planlananTarih, Doktor doktor,long randevuId)
//        {
//            Takvim[] randevu = Persistence.ReadList<Takvim>("Select * from Takvim Where Hasta_Id=@prm3 and Doktor_Id=@prm0 and BasTarih=@prm1  and  RandevuDurumu=@prm2 and Aktif=1 and Id!=@prm4 order by sirano asc",
//                new object[] { doktor.Id, planlananTarih.ToString("yyyyMMdd"), myenum.RandevuDurumu.Verildi.ToString(), hasta.Id, randevuId });
//            return randevu;
//        }

//        public static TakvimSatiri RandevuSatiri(Hasta hasta, DateTime planlananTarih, AsiOzellikTanim ozelliktanim, myenum.IslemTuru islemturu, myenum.IzlemTuru izlemturu, Takvim takvim,string aciklama)
//        {
//            TakvimSatiri satir = new TakvimSatiri();
//            satir.Takvim.Id = takvim.Id;
//            satir.Doktor.Id = takvim.Doktor.Id;
//            satir.Doktor = takvim.Doktor;
//            satir.Durum = myenum.TakvimSatirDurumu.Yapılmadı;
//            satir.Hasta = hasta;
//            satir.Hasta.Id = hasta.Id;
//            satir.IslemTuru = islemturu;
//            satir.Izlemturu = izlemturu;
//            satir.PlanlananTarih = planlananTarih;
//            if (satir.IslemTuru == myenum.IslemTuru.Asi)
//            {
//                ozelliktanim.AsiTanim = Persistence.Read<AsiTanim>(ozelliktanim.AsiTanim.Id);
//                satir.Asi = ozelliktanim.AsiTanim;
//                satir.Asi.Id = ozelliktanim.AsiTanim.Id;
//                satir.AsiOzellikTanimId = ozelliktanim.Id;
//                satir.Aciklama = ozelliktanim.AsiTanim.Kodu+" kodlu ve " + ozelliktanim.AsiTanim.Adi + " adlı aşı yapılacaktır."; 
//            }
//            else
//                satir.Aciklama = aciklama;
//            return satir;
//        }
//        /// Varsa vekili vekil yoksa hastanın kendi dokturunu dönderiyor
//        /// </summary>
//        /// <param name="hasta"></param>
//        /// <param name="tarih"></param>
//        /// <param name="hicbiriseyaramıyor"></param>
//        /// <returns></returns>
//        public static Doktor GetVekilDoktor(Hasta hasta, DateTime tarih)
//        {
//            DoktorVekalet[] vekalet = Persistence.ReadList<DoktorVekalet>(@"Select top 1 * from DoktorVekalet where VerenDoktor_Id=@prm0
//             and BaslangicTarihi<=@prm1 and BitisTarihi>=@prm1 and DoktorVekalet.Aktif=1 order by Id desc", new object[] { hasta.Doktor.Id, tarih });

//            if (vekalet != null && vekalet.Length > 0)
//                vekalet[0].AlanDoktor.Read();
//            else
//            {

//                hasta.Doktor.Read();
//                return hasta.Doktor;
//            }

//            return vekalet[0].AlanDoktor;

//        }

//        public static Randevu GetRandevu(DateTime tarih, Doktor doktor,long hastaid, long randevuid = 0)
//        {
//            TimeSpan mtimeSpan = new System.TimeSpan();
//            System.TimeSpan.TryParse(Current.PrgAyar.MesaiBaslangicSaati, out mtimeSpan);
//            TimeSpan rtimeSpan = new System.TimeSpan();
//            System.TimeSpan.TryParse(Current.PrgAyar.RandevuAraligi, out rtimeSpan);
//            Randevu randevu = new Randevu();
//            Takvim[] randevular;
//            if (randevuid == 0)
//                randevular = Persistence.ReadList<Takvim>("Select * from Takvim Where Doktor_Id=@prm0 and BasTarih=@prm1  and  RandevuDurumu!=@prm2 and Aktif=1 order by sirano asc", new object[] { doktor.Id, tarih.ToString("yyyyMMdd"), myenum.RandevuDurumu.İptalEdildi.ToString() });
//            else
//                randevular = Persistence.ReadList<Takvim>("Select * from Takvim Where Doktor_Id=@prm0 and BasTarih=@prm1  and  RandevuDurumu!=@prm2 and Aktif=1 and Id!=@prm3 order by sirano asc", new object[] { doktor.Id, tarih.ToString("yyyyMMdd"), myenum.RandevuDurumu.İptalEdildi.ToString(), randevuid });


//            if (randevular != null && randevular.Length > 0)
//            {
//                TimeSpan ilkdongurandevusaaat;
//                TimeSpan birsonrakidongurandevusaaati;
//                int randevuno = 0;
//                string[] zaman;
//                for (int i = 0; i < randevular.Length; i++)
//                {
//                    if (hastaid == randevular[i].Hasta.Id)
//                    {
//                        randevu.BasTarih = randevular[i].BasTarih;
//                        randevu.Doktor.Id = randevular[i].Doktor.Id;
//                        randevu.RandevuDurumu = myenum.RandevuDurumu.Verildi;
//                        TimeSpan tmspn;
//                        string[] saat = randevular[i].Saat.Split(':');
//                        if (saat != null && saat.Length > 0)
//                        {
//                            tmspn = new TimeSpan(Convert.ToInt16(saat[0]), Convert.ToInt16(saat[1]), 0);
//                        }
//                        else
//                            throw new Exception("Saat formatı yanlış kaydedilmiş.");
//                        randevu.Saat = tmspn;
//                        randevu.SiraNo = randevular[i].SiraNo;
//                        break;
//                    }

//                    zaman = randevular[i].Saat.Split(':');
//                    if (zaman != null && zaman.Length > 0)
//                    {
//                        ilkdongurandevusaaat = new TimeSpan(Convert.ToInt16(zaman[0]), Convert.ToInt16(zaman[1]), 0);
//                    }
//                    else
//                        throw new Exception("Saat formatı yanlış kaydedilmiş.");

//                    if (i == 0)
//                    {

//                        if (ilkdongurandevusaaat != mtimeSpan)
//                        {
//                            randevu.BasTarih = tarih;
//                            randevu.Doktor.Id = doktor.Id;
//                            randevu.RandevuDurumu = myenum.RandevuDurumu.Verildi;
//                            randevu.Saat = mtimeSpan;
//                            randevu.SiraNo = 1;
//                            break;
//                        }
//                    }
//                    randevuno = i + 1;

//                    zaman = null;
//                    if (randevular.Length > randevuno)
//                    {
//                        zaman = randevular[randevuno].Saat.Split(':');
//                        if (zaman != null && zaman.Length > 0)
//                        {
//                            birsonrakidongurandevusaaati = new TimeSpan(Convert.ToInt16(zaman[0]), Convert.ToInt16(zaman[1]), 0);
//                        }
//                        else
//                            throw new Exception("Saat formatı yanlış kaydedilmiş.");

                        
//                        if (birsonrakidongurandevusaaati.Subtract(ilkdongurandevusaaat).Minutes != rtimeSpan.Minutes)
//                        {
//                            randevu.BasTarih = tarih;
//                            randevu.Doktor.Id = doktor.Id;
//                            randevu.RandevuDurumu = myenum.RandevuDurumu.Verildi;
//                            randevu.Saat = (ilkdongurandevusaaat + rtimeSpan);
//                            randevu.SiraNo = (short)(Convert.ToInt16(randevular[i].SiraNo) + 1);
//                            break;
//                        }

//                    }
//                    else
//                    {

//                        randevu.BasTarih = tarih;
//                        randevu.Doktor.Id = doktor.Id;
//                        randevu.RandevuDurumu = myenum.RandevuDurumu.Verildi;
//                        randevu.Saat = (ilkdongurandevusaaat + rtimeSpan);
//                        randevu.SiraNo = (short)(randevular[i].SiraNo + 1);
//                        break;
//                    }

//                    ilkdongurandevusaaat = new TimeSpan();
//                    birsonrakidongurandevusaaati = new TimeSpan();
//                    zaman = null;
//                }


//            }
//            else
//            {
//                randevu.BasTarih = tarih;
//                randevu.Doktor.Id = doktor.Id;
//                randevu.RandevuDurumu = myenum.RandevuDurumu.Verildi;
//                randevu.Saat = mtimeSpan;
//                randevu.SiraNo = 1;


//            }





//            return randevu;
//        }



     
    }


   

}
