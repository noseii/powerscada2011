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
using PowerScada.DataGridViewAutoFilter;
using PowerScada.DataGridViewAutoFilter;
using DevExpress.XtraEditors.Repository;


namespace PowerScada
{
    public partial class frmCihazTanim : frmInfoForm
    {
        //BindingList<CihazAdres> Adresler = new BindingList<CihazAdres>();
        DataTable Adresler = new DataTable();
        private GridEditButtonManager gridadres;
        
      
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
                Adresler = Transaction.Instance.ExecuteSql("Select *,(Select TagAdresi from Adres Where Adres.Id=CihazAdres.Adres_Id and Adres.Aktif=1) as TagAdresi from CihazAdres where Aktif=1 and Cihaz_Id=@prm0", new object[] { infoformentity.Id });
                //if(adresler!=null)
                //{
                //    Adresler.Clear();
                //    foreach (CihazAdres item in adresler)
                //    {
                //        if (item.Adres.Id > 0)
                //            item.Adres = Persistence.Read<Adres>(item.Adres.Id);
                       
                //        Adresler.Add(item);
                //    }
                    
                //}
            }
            return ((Cihaz)infoformentity);
        }

     

        protected  void InitdataControl()
        {
            //GridAdresler.DataError += new DataGridViewDataErrorEventHandler(GridAdresler_DataError);
          
          
        
                            //<Column Name='Davranis'  HeaderText='Davranis'  Width='150' DisplayIndex='6'   Visible='true' Type ='ComboBox' />                           
                    //<Column Name='AdresTipi'  HeaderText='Adres Tipi'  Width='150' DisplayIndex='5'   Visible='true' Type ='ComboBox' />

            GridAdresler.SetGridStyle(
             @"<Style>
                    <Column Name='Id'        HeaderText='Id'             Width='0'  DisplayIndex='0'   Visible='false'  />
                    <Column Name='Adres_Id'  HeaderText='Adres_Id'       Width='0' DisplayIndex='1'    Visible='false' />                    
                    <Column Name='TagAdresi'  HeaderText='TagAdresi'     Width='100' Text='Adres Seç' DisplayIndex='2'   Visible='true' Type ='Button' />
                    <Column Name='Formul'    HeaderText='Formül'         Width='100' DisplayIndex='3'   Visible='true'  />                 
                   <Column Name='AdresTipi'    HeaderText='Adres Tipi'         Width='100' DisplayIndex='3'   Visible='true'  Type ='ComboBox'/>                 
                   <Column Name='Davranis'    HeaderText='Davranış'         Width='100' DisplayIndex='3'   Visible='true'  Type ='ComboBox'/>                 
              
            </Style>");

 //<Column Name='TagAdresi' HeaderText='Tag Adresi'     Width='150'  DisplayIndex='2'   Visible='true'  />

            //DataGridViewComboBoxColumn combo = new DataGridViewComboBoxColumn();
            //combo.DataSource = Enum.GetValues(typeof(myenum.Davranis));
            //combo.DataPropertyName = "Davranis";
            //combo.Name = "Davranis";
            //GridAdresler.Columns.Add(combo);
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
            //DataGridViewComboBoxColumn AdresColumn = new DataGridViewComboBoxColumn();
            //AdresColumn.Name = "AdresTipi";
            //AdresColumn.HeaderText = "AdresTipi";
            //AdresColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            //AdresColumn.DataPropertyName = "AdresTipi";
            //AdresColumn.DataSource=Utility.EnumToDataTable(typeof(myenum.AdresTipi));
            //AdresColumn.DisplayMember = "Ad";
            //AdresColumn.ValueMember = "Id";
            //AdresColumn.DisplayIndex = 7;
             
            //GridAdresler.Columns.Add(AdresColumn);
            gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;

            gridadres = new GridEditButtonManager(GridAdresler, new ActionAdresListesi(),
            new string[] { "Adres_Id", "TagAdresi" }, new string[] { "Id", "TagAdresi" }, true);
            gridadres.BeforeExecute += new BeforeExecuteEventHandler(gridadres_BeforeExecute);
            gridadres.AfterExecute += new EventHandler(gridadres_AfterExecute);
        }

        bool gridadres_BeforeExecute(object sender, EventArgs e)
        {
            //Adresler.Add(new CihazAdres());
            return true;
        }

        void GridAdresler_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
         
        }

        void gridadres_AfterExecute(object sender, EventArgs e)
        {
            
            
                 
              
            //adres.Adres.Id = (long)gridadres.ManagerGrid.CurrentRow.Cells["Adres_Id"].Value;
            //adres.Adres.TagAdresi = (string)gridadres.ManagerGrid.CurrentRow.Cells["TagAdresi"].Value;
         
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
                if (cihaz.CihazTuru.Id > 0)
                {
                    LookupTable cihazturu = Persistence.Read<LookupTable>(cihaz.cihazturu.Id);
                    editButtonCihazTuru.Id = cihazturu.Id;
                    editButtonCihazTuru.Text = cihazturu.Adi;
                }
                textEditAdi.Text = cihaz.Adi;
                textEditkodu.Text = cihaz.Kodu;
                memoEditAciklama.Text = cihaz.Aciklama;
                myComboDavranis.Id = (int)cihaz.Davranis;
                ShowEntityDataGrid();
            }
            
        }

        private void ShowEntityDataGrid()
        {
            GridAdresler.DataSource = Adresler; ;
        }

       

        protected override void UpdateEntityData()
        {
            Cihaz cihaz = ((Cihaz)infoformentity);
            cihaz.Adi = textEditAdi.Text;
            cihaz.Kodu = textEditkodu.Text;
            cihaz.Aciklama = memoEditAciklama.Text;
            cihaz.Davranis=(myenum.Davranis)myComboDavranis.Id;
            cihaz.CihazTuru.Id = editButtonCihazTuru.Id;
            KisiNotlariUpdate();    
        }

        private void KisiNotlariUpdate()
        {
            Adresler.AcceptChanges();

            GridAdresler.DataSource = Adresler;

        }

        protected override void Save()
        {
            UpdateEntityData();
            Transaction.Instance.Join(delegate()
            {
                base.Save();
                try
                {
                    int i = Transaction.Instance.ExecuteNonQuery(" delete from CihazAdres where Cihaz_Id=@prm0", infoformentity.Id);
                }
                catch (Exception)
                {
                    throw new Exception("Cihaz ait adresler silinemdi");
                }
                 //bindingsource.DataSource
                //foreach (CihazAdres ent in Adresler)
                //{
                    
                //    ent.Cihaz.Id = infoformentity.Id;
                //    //chzadres.Adres.Id =Convert.ToInt64(rw["Adres_Id"]);
                //    //chzadres.Davranis = (mymodel.myenum.Davranis) Enum.Parse(typeof(mymodel.myenum.Davranis),rw["Davranis"].ToString());
                //    //chzadres.AdresTipi = (mymodel.myenum.AdresTipi)Enum.Parse(typeof(mymodel.myenum.AdresTipi), rw["AdresTipi"].ToString());
                //    //chzadres.Formul = rw["Formul"] == null ? "" : rw["Formul"].ToString();
                //    ent.Insert();
                //}

                foreach (DataRow rw in Adresler.Rows)
                {
                    CihazAdres chz = new CihazAdres();
                    chz.Cihaz.Id=infoformentity.Id;
                    chz.Adres.Id=Convert.ToInt64(rw["Adres_Id"]);
                    chz.AdresTipi = (mymodel.myenum.AdresTipi)Enum.Parse(typeof(mymodel.myenum.AdresTipi),rw["AdresTipi"].ToString());
                    chz.Davranis = (mymodel.myenum.Davranis)Enum.Parse(typeof(mymodel.myenum.Davranis), rw["Davranis"].ToString());
                    chz.Formul = rw["Formul"].ToString();
                    chz.Insert();
                }

            });

            
        }

        private void GridAdresler_Validating(object sender, CancelEventArgs e)
        {

        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if(DialogResult.Yes==MessageBox.Show("Seçili satırı silmek istediğinizden eminmisiniz ?","Uyarı",MessageBoxButtons.YesNo))
                {
                    gridView1.DeleteRow(gridView1.FocusedRowHandle);
                }
                else
                    return;
            }
        }

      

      

      

     


       
        
    }
}

