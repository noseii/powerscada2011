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

using DevExpress.XtraEditors.Repository;


namespace PowerScada
{
    public partial class frmCihazTanim : frmInfoForm
    {
        //BindingList<CihazAdres> Adresler = new BindingList<CihazAdres>();
        DataTable Adresler = new DataTable();
        DataTable AlarmAdresleri = new DataTable();
        private GridEditButtonManager gridadres;
        private GridEditButtonManager gridalarmadres;
        private GridEditButtonManager gridrskodu;
        OpcManager mngr;
        OPCServer opcserver;
      
        public frmCihazTanim()
        {
           
            InitializeComponent();
           
            EditNew(null);
            InitdataControl();
            //CommandAdd();
      }

        public frmCihazTanim(long id, EkranDurumu ekrandurumu)
        {
            InitializeComponent();
           
            //CommandAdd();
            this.EkranDurumu = ekrandurumu;
            EditById(id);
            InitdataControl();
            
        }

        protected override Entity findEntity(long id)
        {
            infoformentity = Persistence.Read<Cihaz>(id);
            if (infoformentity != null)
            {
                
                //CihazAdres[] adresler=Persistence.ReadList<CihazAdres>("Select * from CihazAdres where Aktif=1 and Cihaz_Id=@prm0", new object[] { infoformentity.Id });
                Adresler = Transaction.Instance.ExecuteSql("Select *,(Select TagAdresi from Adres Where Adres.Id=CihazAdres.Adres_Id and Adres.Aktif=1) as TagAdresi from CihazAdres where Aktif=1 and Cihaz_Id=@prm0 and AdresTipi in (@prm1,@prm2,@prm3)", new object[] { infoformentity.Id, myenum.AdresTipi.OkunacakAdres.ToString(), myenum.AdresTipi.IstenilenDegerAdresi.ToString(), myenum.AdresTipi.LimitDegerAdresi.ToString() });
                AlarmAdresleri = Transaction.Instance.ExecuteSql(@"Select *
                 ,(Select Kodu from LookupTable Where LookupTable.Id=catnm.RsKodu_Id and LookupTable.Aktif=1) as RsKodu
                 ,(Select TagAdresi from Adres Where Adres.Id=CihazAdres.Adres_Id and Adres.Aktif=1) as TagAdresi from CihazAdres 
                 inner join CihazAlarmTanimi catnm on catnm.CihazAdres_Id=CihazAdres.Id and catnm.Aktif=1 
                 where CihazAdres.Aktif=1 and CihazAdres.Cihaz_Id=@prm0 and AdresTipi=@prm1", new object[] { infoformentity.Id, myenum.AdresTipi.AlarmAdresi.ToString() });
            }
            return ((Cihaz)infoformentity);
        
        
        }

     

        protected  void InitdataControl()
        {
            DataColumn OkunanDeger = new DataColumn("OkunanDeger", typeof(string));
            DataColumn YazilacakDeger = new DataColumn("YazilacakDeger", typeof(string));
            DataColumn YazButonu = new DataColumn("YazButonu");

            DataColumn OkunanDeger1 = new DataColumn("OkunanDeger", typeof(string));
            DataColumn YazilacakDeger1 = new DataColumn("YazilacakDeger", typeof(string));
            DataColumn YazButonu1 = new DataColumn("YazButonu");

            Adresler.Columns.Add(OkunanDeger);
            Adresler.Columns.Add(YazilacakDeger);
            Adresler.Columns.Add(YazButonu);
            AlarmAdresleri.Columns.Add(OkunanDeger1);
            AlarmAdresleri.Columns.Add(YazilacakDeger1);
            AlarmAdresleri.Columns.Add(YazButonu1);

            GridAdresler.SetGridStyle(
             @"<Style>
                    <Column Name='Id'             HeaderText='Id'             Width='0'     DisplayIndex='0'   Visible='false' />
                    <Column Name='Adres_Id'       HeaderText='Adres_Id'       Width='0'     DisplayIndex='1'   Visible='false' />                    
                    <Column Name='TagAdresi'      HeaderText='TagAdresi'      Width='100'   DisplayIndex='2'   Visible='true'  Type ='Button' Text='Adres Seç'/>
                    <Column Name='Formul'         HeaderText='Formül'         Width='100'   DisplayIndex='3'   Visible='true'  />                 
                    <Column Name='AdresTipi'      HeaderText='Adres Tipi'     Width='100'   DisplayIndex='4'   Visible='true'  Type ='ComboBox' />                 
                    <Column Name='Davranis'       HeaderText='Davranış'       Width='100'   DisplayIndex='5'   Visible='true'  Type ='ComboBox' />                 
                    <Column Name='IsLogTutulsun'  HeaderText='IsLogTutulsun'  Width='100'   DisplayIndex='6'   Visible='true'  Type ='Checkbox' />
                    <Column Name='OkunanDeger'    HeaderText='OkunanDeger'    Width='100'   DisplayIndex='7'   Visible='true'  />                 
                    <Column Name='YazilacakDeger' HeaderText='YazilacakDeger' Width='100'   DisplayIndex='8'   Visible='true'  />                 
                    <Column Name='YazButonu'      HeaderText='Değeri Set Et'      Width='100'   DisplayIndex='9'   Visible='true'  Type ='Button' Text='Değeri Set Et' ShowButtonMode='ShowAlways'/>
                   <Column Name='TagDeğeriniOku'        HeaderText='Tag Değerini Oku'           Width='100' DisplayIndex='12'   Visible='true'  Type ='Button' Text='Tag Değerini Oku'    ShowButtonMode='ShowAlways' />
                </Style>");


            gridAlarmAdresler.SetGridStyle(
             @"<Style>
                    <Column Name='Id'               HeaderText='Id'                         Width='0'   DisplayIndex='0'    Visible='false'                                         />
                    <Column Name='Adres_Id'         HeaderText='Adres_Id'                   Width='0'   DisplayIndex='1'    Visible='false'                                         />                    
                    <Column Name='TagAdresi'        HeaderText='TagAdresi'                  Width='100' DisplayIndex='2'    Visible='true'    Type ='Button' Text='Adres Seç'       />
                    <Column Name='Formul'           HeaderText='Formül'                     Width='100' DisplayIndex='3'    Visible='true'                                          />                 
                    <Column Name='AlarmTipi'        HeaderText='Alarm Tipi'                 Width='100' DisplayIndex='4'    Visible='true'    Type ='ComboBox'                      />                 
                    <Column Name='DataTipi1'        HeaderText='Data Tipi'                  Width='100' DisplayIndex='5'    Visible='true'    Type ='ComboBox'                      />
                    <Column Name='AlarmMesaji'      HeaderText='Alarm Mesajı'               Width='100' DisplayIndex='6'    Visible='true'                                          />
                    <Column Name='SesAcik'          HeaderText='SesAcik'                    Width='100' DisplayIndex='7'    Visible='true'    Type ='Checkbox'                      />
                    <Column Name='SesDosyasiAdresi' HeaderText='SesDosyasiAdresi'           Width='100' DisplayIndex='8'    Visible='true'                                          />
                    <Column Name='IsLogTutulsun1'   HeaderText='IsLogTutulsun'              Width='100' DisplayIndex='9'    Visible='true'    Type ='Checkbox'                      />
                    <Column Name='OkunanDeger'      HeaderText='OkunanDeger'                Width='100' DisplayIndex='10'   Visible='true'                                          />                 
                    <Column Name='YazilacakDeger'   HeaderText='YazilacakDeger'             Width='100' DisplayIndex='11'   Visible='true'                                          />                 
                    <Column Name='YazButonu'        HeaderText='Değeri Set Et'              Width='100' DisplayIndex='12'   Visible='true'  Type ='Button' Text='Değeri Set Et'       ShowButtonMode='ShowAlways' />
                    <Column Name='TagDeğeriniOku'   HeaderText='Tag Değerini Oku'           Width='100' DisplayIndex='12'   Visible='true'  Type ='Button' Text='Tag Değerini Oku'    ShowButtonMode='ShowAlways' />
                    <Column Name='RsKodu'           HeaderText='Rs Kodu'                    Width='100' DisplayIndex='13'   Visible='true'    Type ='Button' Text='RS Kodu'       />
                    <Column Name='RSKodu_Id'        HeaderText='RSKodu_Id'                  Width='0'   DisplayIndex='14'   Visible='false'                                       />                    
            </Style>");

 
            string[] names = Enum.GetNames(typeof(mymodel.myenum.Davranis));
            foreach (string str in names)
            {
                ((RepositoryItemComboBox)gridView1.Columns["Davranis"].ColumnEdit).Items.Add(str);
            }
            names = null;
            names = Enum.GetNames(typeof(mymodel.myenum.AdresTipi));
            foreach (string str in names)
            {
                ((RepositoryItemComboBox)gridView1.Columns["AdresTipi"].ColumnEdit).Items.Add(str);
            }
    
            gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridadres = new GridEditButtonManager(GridAdresler, new ActionAdresListesi(),
            new string[] { "Adres_Id", "TagAdresi" }, new string[] { "Id", "TagAdresi" }, true);
        
            

            names = Enum.GetNames(typeof(mymodel.myenum.AlarmTipi));
            foreach (string str in names)
            {
                ((RepositoryItemComboBox)gridViewAlarmAdresler.Columns["AlarmTipi"].ColumnEdit).Items.Add(str);
            }
            names = null;
            names = Enum.GetNames(typeof(mymodel.myenum.MappedFieldType));
            foreach (string str in names)
            {
                ((RepositoryItemComboBox)gridViewAlarmAdresler.Columns["DataTipi1"].ColumnEdit).Items.Add(str);
            }

            gridViewAlarmAdresler.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridalarmadres = new GridEditButtonManager(gridAlarmAdresler, new ActionAdresListesi(),
            new string[] { "Adres_Id", "TagAdresi" }, new string[] { "Id", "TagAdresi" }, true);

            gridrskodu = new GridEditButtonManager(gridAlarmAdresler, new ActionLookupListesi(14),
            new string[] { "RSKodu_Id", "RsKodu" }, new string[] { "Id", "Kodu" }, true);
            

            RepositoryItemButtonEdit button = ((RepositoryItemButtonEdit)gridView1.Columns["YazButonu"].ColumnEdit);
            button.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            button.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(frmCihazTanim_ButtonClick);

            RepositoryItemButtonEdit button1 = ((RepositoryItemButtonEdit)gridViewAlarmAdresler.Columns["YazButonu"].ColumnEdit);
            button1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            button1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(frmCihazTanim_ButtonClick1);


            RepositoryItemButtonEdit button2 = ((RepositoryItemButtonEdit)gridView1.Columns["TagDeğeriniOku"].ColumnEdit);
            button2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            button2.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(frmCihazTanim_ButtonClick2);

            RepositoryItemButtonEdit button3 = ((RepositoryItemButtonEdit)gridViewAlarmAdresler.Columns["TagDeğeriniOku"].ColumnEdit);
            button3.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            button3.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(frmCihazTanim_ButtonClick3);
          
            opcserver = Persistence.Read<OPCServer>(new Condition("Aktif", Operator.Equal, 1));
            if (opcserver != null && opcserver.Id > 0)
            {
                OPCServerGroup groups = OPCServer.ReadGroups(opcserver.Id, editButtonLokasyon.Text);
                if (groups != null)
                {
                    opcserver.Groups.Add(groups);
                }
                else
                {
                    MessageBox.Show("Hata:Bu Lokasyona ait OPCServerGroup ismi boş !!!", "Uyarı..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Hata:OPCServer Kaydı Bulunamadı. OPCServer Kayıt Ekranında gerekli tanımlamayı yapabilirsiniz.!!!", "Uyarı..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }

        void frmCihazTanim_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            object value = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "YazilacakDeger");
            object  adres = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagAdresi");
            if (value != null && value != System.DBNull.Value && adres != null && adres!=string.Empty)
            {
                //OpcManager mngr;
                //OPCServer opcserver = Persistence.Read<OPCServer>(new Condition("Aktif", Operator.Equal, 1));
                //if (opcserver != null && opcserver.Id > 0)
                //{
                //    OPCServerGroup groups=OPCServer.ReadGroups(opcserver.Id,editButtonLokasyon.Text);
                //    if (groups != null)
                //    {
                //        opcserver.Groups.Add(groups);
                //    }
                //    else
                //    {
                //        MessageBox.Show("Hata:Bu Lokasyona ait OPCServerGroup ismi boş !!!", "Uyarı..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        return;
                //    }
                
                    mymodel.Adres[] adresler = Persistence.ReadList<Adres>();
                    List<string> adreslist = new List<string>();
                    adreslist.Add(adres.ToString());
                    mngr = new OpcManager(opcserver.Groups[0].OPCGroupName,adreslist);
                    mngr.OPCServerConnect(opcserver.OpcServerName, opcserver.OPCNodeName);
                    mngr.AddOpcGroupServer(opcserver.Groups[0].OPCGroupName, opcserver.Groups[0].GroupUpdateRate, opcserver.Groups[0].GroupActiveState, (float)opcserver.Groups[0].GroupDeadBand);
                    mngr.OPCAddItems(this.editButtonLokasyon.Text);
                    mngr.OPCItemWrite(this.editButtonLokasyon.Text,adres.ToString(), value.ToString());
                    gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "OkunanDeger", mngr.GetOPCItemSyncRead(this.editButtonLokasyon.Text,adres.ToString()));
                    mngr.DisConnectServer();
                    mngr = null;
                }
           //}
           // else
           //     MessageBox.Show("Hata:Tag Degeri boş ya da adres seçmediniz !!!", "Uyarı..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          
        }

        void frmCihazTanim_ButtonClick1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            object value = gridViewAlarmAdresler.GetRowCellValue(gridViewAlarmAdresler.FocusedRowHandle, "YazilacakDeger");
            object adres = gridViewAlarmAdresler.GetRowCellValue(gridViewAlarmAdresler.FocusedRowHandle, "TagAdresi");
            if (value != null && value != System.DBNull.Value && adres != null && adres != string.Empty)
            {
                //OpcManager mngr;
                //OPCServer opcserver = Persistence.Read<OPCServer>(new Condition("Aktif", Operator.Equal, 1));
                //if (opcserver != null && opcserver.Id > 0)
                //{
                    mymodel.Adres[] adresler = Persistence.ReadList<Adres>();
                    List<string> adreslist = new List<string>();
                    adreslist.Add(adres.ToString());
                    mngr = new OpcManager(opcserver.Groups[0].OPCGroupName,adreslist);
                    mngr.OPCServerConnect(opcserver.OpcServerName, opcserver.OPCNodeName);
                    //mngr.AddOpcGroupServer(opcserver.OPCGroupName, (opcserver.GroupUpdateRate), (opcserver.GroupActiveState), (float)opcserver.GroupDeadBand);
                    mngr.AddOpcGroupServer(opcserver.Groups[0].OPCGroupName, opcserver.Groups[0].GroupUpdateRate, opcserver.Groups[0].GroupActiveState, (float)opcserver.Groups[0].GroupDeadBand);
                    mngr.OPCAddItems(this.editButtonLokasyon.Text);
                    mngr.OPCItemWrite(this.editButtonLokasyon.Text,adres.ToString(), value.ToString());
                    gridViewAlarmAdresler.SetRowCellValue(gridViewAlarmAdresler.FocusedRowHandle, "OkunanDeger", mngr.GetOPCItemSyncRead(this.editButtonLokasyon.Text,adres.ToString()));
                    mngr.DisConnectServer();
                    mngr = null;
                //}

            }
            else
                MessageBox.Show("Hata:Tag Degeri boş ya da adres seçmediniz !!!", "Uyarı..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        void frmCihazTanim_ButtonClick2(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            object adres = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagAdresi");
            if (adres != null && adres != string.Empty)
            {
                //OpcManager mngr;
                //OPCServer opcserver = Persistence.Read<OPCServer>(new Condition("Aktif", Operator.Equal, 1));
                //if (opcserver != null && opcserver.Id > 0)
                //{
                    List<string> adreslist = new List<string>();
                    adreslist.Add(adres.ToString());
                    mngr = new OpcManager(opcserver.Groups[0].OPCGroupName,adreslist);
                    mngr.OPCServerConnect(opcserver.OpcServerName, opcserver.OPCNodeName);
                    //mngr.AddOpcGroupServer(opcserver.OPCGroupName, (opcserver.GroupUpdateRate), (opcserver.GroupActiveState), (float)opcserver.GroupDeadBand);
                    mngr.AddOpcGroupServer(opcserver.Groups[0].OPCGroupName, opcserver.Groups[0].GroupUpdateRate, opcserver.Groups[0].GroupActiveState, (float)opcserver.Groups[0].GroupDeadBand);
                    mngr.OPCAddItems(this.editButtonLokasyon.Text);
                    gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "OkunanDeger", mngr.GetOPCItemSyncRead(this.editButtonLokasyon.Text,adres.ToString()));
                    mngr.DisConnectServer();
                    mngr = null;
                //}

            }
            else
                MessageBox.Show("Hata:Tag Degeri boş ya da adres seçmediniz !!!", "Uyarı..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        void frmCihazTanim_ButtonClick3(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            object adres = gridViewAlarmAdresler.GetRowCellValue(gridViewAlarmAdresler.FocusedRowHandle, "TagAdresi");
            if (adres != null && adres != string.Empty)
            {
                //OpcManager mngr;
                //OPCServer opcserver = Persistence.Read<OPCServer>(new Condition("Aktif", Operator.Equal, 1));
                //if (opcserver != null && opcserver.Id > 0)
                //{  
                    List<string> adreslist = new List<string>();
                    adreslist.Add(adres.ToString());
                    mngr = new OpcManager(opcserver.Groups[0].OPCGroupName,adreslist);
                    mngr.OPCServerConnect(opcserver.OpcServerName, opcserver.OPCNodeName);
                    //mngr.AddOpcGroupServer(opcserver.OPCGroupName, (opcserver.GroupUpdateRate), (opcserver.GroupActiveState), (float)opcserver.GroupDeadBand);
                    mngr.AddOpcGroupServer(opcserver.Groups[0].OPCGroupName, opcserver.Groups[0].GroupUpdateRate, opcserver.Groups[0].GroupActiveState, (float)opcserver.Groups[0].GroupDeadBand);
                    mngr.OPCAddItems(this.editButtonLokasyon.Text);
                    gridViewAlarmAdresler.SetRowCellValue(gridViewAlarmAdresler.FocusedRowHandle, "OkunanDeger", mngr.GetOPCItemSyncRead(this.editButtonLokasyon.Text,adres.ToString()));
                    mngr.DisConnectServer();
                    mngr = null;
                //}

            }
            else
                MessageBox.Show("Hata:Tag Degeri boş ya da adres seçmediniz !!!", "Uyarı..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        protected override Entity getNewEntity()
        {
            Cihaz  cihaz=new Cihaz();
            Adresler = Transaction.Instance.ExecuteSql("Select *,(Select TagAdresi from Adres Where Adres.Id=CihazAdres.Adres_Id and Adres.Aktif=1) as TagAdresi from CihazAdres where Aktif=1 and Cihaz_Id=@prm0", new object[] { cihaz.Id });

            //Adresler = new BindingList<CihazAdres>();
            //Adresler.Add(new CihazAdres());
           
            return cihaz;
        }

        protected override void ShowEntityData()
        {
            Cihaz cihaz = ((Cihaz)infoformentity);
            if (cihaz != null)
            {
                if (cihaz.CihazModeli.Id > 0)
                {
                    LookupTable cihazmodeli = Persistence.Read<LookupTable>(cihaz.CihazModeli.Id);
                    editButtonCihazTuru.Id = cihazmodeli.Id;
                    editButtonCihazTuru.Text = cihazmodeli.Adi;
                    cihaz.CihazModeli = cihazmodeli;
                }

                if (cihaz.Lokasyon.Id > 0)
                {
                    Lokasyon lokasyon = Persistence.Read<Lokasyon>(cihaz.Lokasyon.Id);
                    editButtonLokasyon.Id = lokasyon.Id;
                    editButtonLokasyon.Text = lokasyon.Adi;
                    cihaz.Lokasyon = lokasyon;
                }

                textEditAdi.Text = cihaz.Adi;
                textEditkodu.Text = cihaz.Kodu;
                memoEditAciklama.Text = cihaz.Aciklama;
                myComboCihazTuru.Id = (int)cihaz.CihazTuru;
                ShowEntityDataGrid();
            }
            
        }

        private void ShowEntityDataGrid()
        {
            GridAdresler.DataSource = Adresler;
            gridAlarmAdresler.DataSource = AlarmAdresleri;
        }

       

        protected override void UpdateEntityData()
        {
            Cihaz cihaz = ((Cihaz)infoformentity);
            cihaz.Adi = textEditAdi.Text;
            cihaz.Kodu = textEditkodu.Text;
            cihaz.Aciklama = memoEditAciklama.Text;
            cihaz.CihazTuru=(myenum.CihazTuru)myComboCihazTuru.Id;
            cihaz.CihazModeli.Id = editButtonCihazTuru.Id;
            cihaz.Lokasyon.Id = editButtonLokasyon.Id;
            
            KisiNotlariUpdate();    
        }

        private void KisiNotlariUpdate()
        {
            Adresler.AcceptChanges();
            AlarmAdresleri.AcceptChanges();
            gridAlarmAdresler.DataSource = AlarmAdresleri;
            GridAdresler.DataSource = Adresler;

        }

        protected override void Save()
        {
            UpdateEntityData();
            Transaction.Instance.Join(delegate()
            {
                base.Save();
                string cihaztagadresId = string.Empty;
                string alarmtanimId = string.Empty;
              

                long Id = 0; 
                foreach (DataRow rw in Adresler.Rows)
                {
                    
                    if(rw["Id"]!=null && rw["Id"]!=System.DBNull.Value)
                       Id = Convert.ToInt64(rw["Id"]);
                    else
                        Id = 0;
                    CihazAdres chz;
                    if (Id == 0)
                    {
                        chz = new CihazAdres();
                        chz.Cihaz.Id = infoformentity.Id;
                        chz.Adres.Id = Convert.ToInt64(Current.IsNull(rw["Adres_Id"],0));
                        chz.AdresTipi = (mymodel.myenum.AdresTipi)Enum.Parse(typeof(mymodel.myenum.AdresTipi), Current.IsNull(rw["AdresTipi"],"").ToString());
                        chz.Davranis = (mymodel.myenum.Davranis)Enum.Parse(typeof(mymodel.myenum.Davranis),Current.IsNull(rw["Davranis"],"").ToString());
                        chz.Formul = Current.IsNull(rw["Formul"],"").ToString();
                        chz.IsLogTutulsun = bool.Parse(Current.IsNull(rw["IsLogTutulsun"],false).ToString());
                        chz.Insert();
                    }
                    else
                    {
                        chz = Persistence.Read<CihazAdres>(Id);
                        chz.Cihaz.Id = infoformentity.Id;
                        chz.Adres.Id = Convert.ToInt64(Current.IsNull(rw["Adres_Id"],0));
                        chz.AdresTipi = (mymodel.myenum.AdresTipi)Enum.Parse(typeof(mymodel.myenum.AdresTipi), Current.IsNull(rw["AdresTipi"],"").ToString());
                        chz.Davranis = (mymodel.myenum.Davranis)Enum.Parse(typeof(mymodel.myenum.Davranis), Current.IsNull(rw["Davranis"],"").ToString());
                        chz.Formul = Current.IsNull(rw["Formul"],"").ToString();
                        chz.IsLogTutulsun = bool.Parse(Current.IsNull(rw["IsLogTutulsun"],false).ToString());
                        chz.Update();
                    }
                    cihaztagadresId += chz.Id + ",";
                }

                foreach (DataRow rw1 in AlarmAdresleri.Rows)
                {
                    
                    if(rw1["Id"]!=null && rw1["Id"]!=System.DBNull.Value)
                        Id = Convert.ToInt64(rw1["Id"]);
                    else
                        Id = 0;
                    CihazAdres chz = null;
                    if (Id == 0)
                    {
                        chz = new CihazAdres();
                        chz.Cihaz.Id = infoformentity.Id;
                        chz.Adres.Id = Convert.ToInt64(Current.IsNull(rw1["Adres_Id"],0));
                        chz.AdresTipi = myenum.AdresTipi.AlarmAdresi; ;
                        chz.Davranis = myenum.Davranis.Oku;
                        chz.Formul = Current.IsNull(rw1["Formul"],"").ToString();
                        chz.IsLogTutulsun = bool.Parse(Current.IsNull(rw1["IsLogTutulsun"],false).ToString());
                        chz.Insert();
                    }
                    else
                    {
                        chz = Persistence.Read<CihazAdres>(Id);
                        chz.Cihaz.Id = infoformentity.Id;
                        chz.Adres.Id = Convert.ToInt64(Current.IsNull(rw1["Adres_Id"],0));
                        chz.AdresTipi = myenum.AdresTipi.AlarmAdresi; ;
                        chz.Davranis = myenum.Davranis.Oku;
                        chz.Formul = Current.IsNull(rw1["Formul"],"").ToString();
                        chz.IsLogTutulsun = bool.Parse(Current.IsNull(rw1["IsLogTutulsun"],false).ToString());
                        chz.Update();
                    }
                    cihaztagadresId += chz.Id + ",";
                    
                    
                    Id = 0;
                    if (rw1["Id1"] != null && rw1["Id1"] != System.DBNull.Value)
                        Id = Convert.ToInt64(rw1["Id1"]);
                    else
                        Id = 0;
                    CihazAlarmTanimi alarmtanimi;
                    if (Id == 0)
                    {
                        alarmtanimi = new CihazAlarmTanimi();
                        alarmtanimi.Cihaz.Id = infoformentity.Id;
                        alarmtanimi.CihazAdres.Id = chz.Id;
                        alarmtanimi.AlarmTipi = (mymodel.myenum.AlarmTipi)Enum.Parse(typeof(mymodel.myenum.AlarmTipi), Current.IsNull(rw1["AlarmTipi"],false).ToString());
                        alarmtanimi.AlarmMesaji = rw1["AlarmMesaji"].ToString();
                        alarmtanimi.DataTipi = (mymodel.myenum.MappedFieldType)Enum.Parse(typeof(mymodel.myenum.MappedFieldType), Current.IsNull(rw1["DataTipi1"],false).ToString());
                        alarmtanimi.IsLogTutulsun = bool.Parse(Current.IsNull(rw1["IsLogTutulsun1"],false).ToString());
                        alarmtanimi.SesAcik = bool.Parse(Current.IsNull(rw1["SesAcik"], false).ToString());
                        alarmtanimi.SesDosyasiAdresi = rw1["SesDosyasiAdresi"].ToString();
                        alarmtanimi.RSKodu.Id = Convert.ToInt64(Current.IsNull(rw1["RSKodu_Id"], 0));
                        alarmtanimi.Insert();
                    }
                     else
                    {
                        alarmtanimi = Persistence.Read<CihazAlarmTanimi>(Id);
                        alarmtanimi.Cihaz.Id = infoformentity.Id;
                        alarmtanimi.CihazAdres.Id = chz.Id;
                        alarmtanimi.AlarmTipi = (mymodel.myenum.AlarmTipi)Enum.Parse(typeof(mymodel.myenum.AlarmTipi),Current.IsNull(rw1["AlarmTipi"].ToString(),"").ToString());
                        alarmtanimi.AlarmMesaji = rw1["AlarmMesaji"].ToString();
                        alarmtanimi.DataTipi = (mymodel.myenum.MappedFieldType)Enum.Parse(typeof(mymodel.myenum.MappedFieldType), Current.IsNull(rw1["DataTipi1"].ToString(),"").ToString());
                        alarmtanimi.IsLogTutulsun = bool.Parse(Current.IsNull(rw1["IsLogTutulsun1"].ToString(), false).ToString());
                        alarmtanimi.SesAcik = bool.Parse(Current.IsNull(rw1["SesAcik"], false).ToString());
                        alarmtanimi.SesDosyasiAdresi = Current.IsNull(rw1["SesDosyasiAdresi"], "").ToString();
                        alarmtanimi.RSKodu.Id = Convert.ToInt64(Current.IsNull(rw1["RSKodu_Id"], 0));
                        alarmtanimi.Update();
                    }
                    alarmtanimId += alarmtanimi.Id + ",";
                }


                try
                {
                    if (cihaztagadresId.Length > 0)
                    {
                        cihaztagadresId = cihaztagadresId.Remove(cihaztagadresId.Length - 1, 1);
                    }
                    else
                        cihaztagadresId = "0";

                    if (alarmtanimId.Length > 0)
                    {
                        alarmtanimId = alarmtanimId.Remove(alarmtanimId.Length - 1, 1);
                    }
                    else
                        alarmtanimId = "0";



                    int i = Transaction.Instance.ExecuteNonQuery(" delete from CihazAdres where Cihaz_Id=@prm0 and Id Not in ("+cihaztagadresId+")", new object[] { infoformentity.Id });
                    i = Transaction.Instance.ExecuteNonQuery(" delete from CihazAlarmTanimi where Cihaz_Id=@prm0 and Id Not in("+alarmtanimId+")", new object[] { infoformentity.Id });
                }
                catch (Exception ex)
                {
                    throw new Exception("Cihaz ait adresler silinemedi" + ex.Message);
                }
            });

            
        }

     

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if(DialogResult.Yes==MessageBox.Show("Seçili satırı silmek istediğinizden eminmisiniz ?","Uyarı",MessageBoxButtons.YesNo))
                {
                    if (((DevExpress.XtraGrid.Views.Grid.GridView)sender).Name == "gridView1")
                        gridView1.DeleteRow(gridView1.FocusedRowHandle);
                    else
                        gridViewAlarmAdresler.DeleteRow(gridViewAlarmAdresler.FocusedRowHandle);

                }
                else
                    return;
            }
        }

      

      

      

     


       
        
    }
}

