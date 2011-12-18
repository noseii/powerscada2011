using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpBullet.OAL;
using mymodel;
using System.Collections;

namespace PowerScada
{
    public partial class frmCihazIzlem : Form
    {
        public static OpcManager mngr = null;

        mymodel.Lokasyon[] lokasyons;
        List<Lokasyon> dt = new List<Lokasyon>();
        public frmCihazIzlem()
        {
            
            InitializeComponent();

            OPCServer opcserver = Persistence.Read<OPCServer>(new Condition("Aktif", Operator.Equal, 1));
            if (opcserver != null && opcserver.Id > 0)
            {
//                mymodel.Adres[] adresler = Persistence.ReadList<Adres>(@"
//                Select 
//                    Adres.* 
//                From Adres 
//                Inner Join CihazAdres on CihazAdres.Adres_Id=Adres.Id and CihazAdres.Aktif=1
//                Where
//                CihazAdres.AdresTipi in ('OkunacakAdres','AlarmAdresi')
//                and Adres.Aktif=@prm0 ",new object[]{true});

                lokasyons = Persistence.ReadList<Lokasyon>("Select * From Lokasyon Where Aktif=@prm0", new object[] { 1 }); ;
                treeListLokasyon.DataSource = lokasyons;
                Hashtable adreslist = new Hashtable();
                Utility.SetGridStyle(treeListLokasyon);
                //foreach (Adres ent in adresler)
                //{
                //    adreslist.Add(ent.TagAdresi);
                //    listBox1.Items.Add(ent.tagadresi);
                //}
                foreach (Lokasyon lksyn in lokasyons)
                {
                    if (lksyn.Adres.Id > 0)
                    {
                        lksyn.Adres = Persistence.Read<Adres>(lksyn.Adres.Id);
                        adreslist.Add(lksyn.Kodu, lksyn);
                        listBox1.Items.Add(lksyn.Adres.TagAdresi);
                    }
                }

                mngr = null;
                //mngr = new OpcManager(adreslist);
                mngr = new OpcManager();
                mngr.OPCServerConnect(opcserver.OpcServerName,opcserver.OPCNodeName);
                OPCServerGroup[] groups=OPCServer.ReadGroups(opcserver.Id);
                if(groups!=null && groups.Length>0)
                {
                    opcserver.Groups.AddRange(groups);
                    
                    foreach (OPCServerGroup group in opcserver.Groups)
                    {
                        mngr.AddOpcGroupServer(group.OPCGroupName, group.GroupUpdateRate, group.GroupActiveState, (float)group.GroupDeadBand);
                      

                        List<string> list = new List<string>();
                        Lokasyon lks = (Lokasyon)adreslist[group.OPCGroupName];
                        if (lks != null)
                        {
                            list.Add(lks.Adres.TagAdresi);
                            mngr.SetOpcItems("lks" + lks.Kodu, list);
                            mngr.OPCAddItems(lks.Kodu);

                            List<OpcItems> items = mngr.GetOPCItem.FindAll(p => p != null && p.OPCItemName == lks.Adres.TagAdresi && (p.OpcGroupName == group.OPCGroupName || p.OpcGroupName == "lks" + group.OPCGroupName));
                            foreach (OpcItems item in items)
                            {
                                item.OPCItemValueChange += new OPCItemValueChangeEventHandler(Opcitemchange);
                            }

                        }
                    }
                    foreach (OPCAutomation.OPCGroupClass group in mngr.ConnectedOPCServer.OPCGroups)
                    {
                        group.DataChange += mngr.ConnectedGroup_DataChange;
                    }
                    mngr.ConnectedOPCServer.OPCGroups.GlobalDataChange += new OPCAutomation.DIOPCGroupsEvent_GlobalDataChangeEventHandler(mngr.OPCGroups_GlobalDataChange);

                    //foreach (Lokasyon lsyn in lokasyons)
                    //{
                    //    List<OpcItems> items = mngr.GetOPCItem.FindAll(p => p != null && p.OPCItemName == lsyn.Adres.TagAdresi);
                    //    foreach (OpcItems item in items)
                    //    {
                    //        item.OPCItemValueChange += new OPCItemValueChangeEventHandler(Opcitemchange);
                    //    }
                    //}

                   
                   
                    //mngr.AddOpcGroupServer(opcserver.OPCGroupName, (opcserver.GroupUpdateRate), (opcserver.GroupActiveState), (float)opcserver.GroupDeadBand);
                    //mngr.OPCAddItems();
                    //SetDataControlsReadOnlyRecursive(this);
                }
            }
            else
            {
                MessageBox.Show("Bağlanılacak OPC Server Bulunamadı.\nOPC Server Tanım Ekranında gerekli tanımlamayı yapınız");
                return;
            }

     }
       
        private  void Opcitemchange(OpcItems sender, OPCItemEventArg e)
        {

           
            string value = e.GuncelDeger;

            foreach (Lokasyon lksyn in lokasyons)
            {
                if (lksyn.Adres.TagAdresi == sender.OPCItemName)
                {
                    //if (chzadres.IsLogTutulsun)
                    //{

                        //if (chzadres.Formul.Length > 0)
                        //{
                        //    value = Current.ConvertToBinary(value);
                        //    if (value.Length >= Convert.ToInt32(chzadres.Formul))
                        //        value = value.Substring(0, Convert.ToInt32(chzadres.Formul));
                        //}
                        //Tarihce = new CihazTarihce();
                        //Tarihce.EskiDegeri = this.textBox1.Text;
                        //Tarihce.YeniDegeri = value;
                        //Tarihce.AdresTipi = chzadres.AdresTipi;
                        //Tarihce.Insert();
                    ShowEntityData(lksyn);
                    //}
                }
            }
        }

        public void ShowEntityData(Lokasyon lk)
        {
            //dt.Add(lk);
            //int id = 0;
            //listBox2.Items.Clear();
            //foreach (Lokasyon lks in dt)
            //{
            //    id++;
            //    listBox2.Items.Add(id + " " + lks);
            //}
            dt.Add(lk);
            listBoxhatalilokasyonlar.Items.Add(lk);
            //foreach (Lokasyon item in dt)
            //{
            //    listBoxhatalilokasyonlar.Items.Add(item);
            //}

            
          
            
        }

        private void SetDataControlsReadOnlyRecursive(Control container)
        {
            foreach (Control c in container.Controls)
            {
                if (c.Controls.Count > 0)
                    SetDataControlsReadOnlyRecursive(c);

                DinlemeyiBaslat(c);
            }
           

        }

        private void DinlemeyiBaslat(Control cntrl)
        {
            //foreach (Control cntrl in this.Controls)
            //{
                if (cntrl is UCBaseControl)
                {
                    UCBaseControl termokupul = ((UCBaseControl)cntrl);
                    termokupul.Opcmanager = mngr;
                    termokupul.CihaziOku();
                }
                
            //}
        }

       

        private void timer1_Tick(object sender, EventArgs e)
        {
            GetDataTarihce();
            GetDataAlarm();
        }

        private void GetDataTarihce()
        {
            string sql = @" Select
                                Tarihce.Id,
                                Lokasyon.Kodu,
                                Lokasyon.Adi,
                                Cihaz.Kodu,
                                Cihaz.Adi,
                                Cihaz.CihazTuru,
                                LookupTable.Adi as CihazModeli,
                                Tarihce.EskiDegeri,
                                Tarihce.YeniDegeri,
                                Tarihce.EklemeTarihi,
                                Tarihce.EkleyenKullanici,
                                Tarihce.AdresTipi
                            From       dbo.CihazTarihce Tarihce
                            Inner Join dbo.Cihaz on Cihaz.Id=Tarihce.Cihaz_Id
                            Inner Join Lokasyon on Lokasyon.Id=Cihaz.Lokasyon_Id
                            Inner Join LookupTable on LookupTable.Id=Cihaz.CihazModeli_Id Where Tarihce.Aktif=@prm0 order by Tarihce.Id desc";

            gridTarihce.DataSource = Transaction.Instance.ExecuteSql(sql, new object[] {true});
        }

        private void GetDataAlarm()
        {
            string sql= @"Select 
            At.Id,
            Cihaz.Adi,
            Alarm.AlarmTipi,alarm.AlarmMesaji
            from AlarmTarihce At
            inner join dbo.CihazAlarmTanimi alarm on alarm.Id=At.Alarm_Id
            inner join dbo.Cihaz on Cihaz.Id=At.Cihaz_Id Where At.Aktif=@prm0 order by At.Id desc";

            gridAlarm.DataSource = Transaction.Instance.ExecuteSql(sql, new object[] { true });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null && listBoxhatalilokasyonlar.SelectedItem != null)
            MessageBox.Show(mngr.GetOPCItemSyncRead(listBoxhatalilokasyonlar.SelectedItem.ToString(), listBox1.SelectedItem.ToString()));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null && textBox1.Text != string.Empty && listBoxhatalilokasyonlar.SelectedItem != null)
                mngr.OPCItemWrite(listBoxhatalilokasyonlar.SelectedItem.ToString(),listBox1.SelectedItem.ToString(), textBox1.Text);
        }

        private void frmCihazIzlem_FormClosing(object sender, FormClosingEventArgs e)
        {
            //mngr.ConnectedOPCServer.OPCGroups.GlobalDataChange -= mngr.OPCGroups_GlobalDataChange;
            mngr.ConnectGroupDataChangeKapat = true;
            mngr.GlobalDataChangeKapat = true;

            mngr.ConnectedOPCServer.OPCGroups.RemoveAll();
            foreach (OPCAutomation.OPCGroupClass grp in mngr.ConnectedOPCServer.OPCGroups)
            {

                mngr.OPCRemoveItems(grp.Name);
                mngr.RemoveGroupServer(grp.Name);

            }
            mngr.ConnectGroupDataChangeKapat = false;
            mngr.GlobalDataChangeKapat = false;
            mngr.ConnectedOPCServer.Disconnect();
            mngr = null;
            System.GC.Collect(); 
            Utility.OpenForms.Remove(this.Name);
        }

        private void treeListLokasyon_DoubleClick(object sender, EventArgs e)
        {
          string lokasyon=  treeListLokasyon.FocusedNode.GetValue(1).ToString();
          string adres = treeListLokasyon.FocusedNode.GetValue(6).ToString();

          if (lokasyon != string.Empty && adres != string.Empty)
          {
              //mngr.ConnectedOPCServer.OPCGroups.GlobalDataChange -= mngr.OPCGroups_GlobalDataChange;
              LokasyonuGetir(lokasyon);
              //mngr.ConnectedOPCServer.OPCGroups.GlobalDataChange += mngr.OPCGroups_GlobalDataChange;

          }
          else
          {
              panelCihazlar.Visible = false;
              MessageBox.Show("bu lokasyon için tanımlı hata adresi yok..");
          }
        }

        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           if(listBoxhatalilokasyonlar.SelectedItem!=null && listBoxhatalilokasyonlar.SelectedItem.ToString()!=string.Empty)
           {
                LokasyonuGetir(listBoxhatalilokasyonlar.SelectedItem.ToString());
           }
           else
           {
               panelCihazlar.Visible = false;
               MessageBox.Show("bu lokasyon için tanımlı hata adresi yok..");
           }
        }

        private void LokasyonuGetir(string lokasyon)
        {
                mngr.GlobalDataChangeKapat = true;
                mngr.ConnectGroupDataChangeKapat = true;
                string eskilokasyonadi = mngr.ConnectedGroup.Name;
                mngr.ConnectedGroup = mngr.ConnectedOPCServer.OPCGroups.GetOPCGroup(lokasyon);
                panelCihazlar.Visible = true;
                List<OpcItems> silinecektaglar = mngr.GetOPCItem.FindAll(p => p != null && p.OpcGroupName == eskilokasyonadi);
                if (silinecektaglar.Count>0)
                    mngr.DeleteOPCItem(silinecektaglar, eskilokasyonadi);


                mymodel.Adres[] adresler = Persistence.ReadList<Adres>(@"
                                Select 
                                    Adres.* 
                                From Cihaz 
                                Inner Join CihazAdres on CihazAdres.Cihaz_Id=Cihaz.Id and CihazAdres.Aktif=1
                                Inner Join Adres on CihazAdres.Adres_Id=Adres.Id and Adres.Aktif=1 
                                Inner Join Lokasyon on   Lokasyon.Id=Cihaz.Lokasyon_Id and Lokasyon.Aktif=1
                                Where
                                CihazAdres.AdresTipi in ('OkunacakAdres','AlarmAdresi')
                                and Adres.Aktif=@prm0 and Lokasyon.Adi=@prm1", new object[] { true, lokasyon });
                
                 
               
                List<string> adreslistesi = new List<string>();
                listBox1.Items.Clear();
                foreach (Adres adres in adresler)
                {
                    adreslistesi.Add(adres.TagAdresi);
                    
                    listBox1.Items.Add(adres.TagAdresi);
                }

                mngr.SetOpcItems(mngr.ConnectedGroup.Name, adreslistesi);
                mngr.OPCAddItems(mngr.ConnectedGroup.Name);
                  //foreach (Adres adres in adresler)
                //{
                //    List<OpcItems> items = mngr.GetOPCItem.FindAll(p => p != null && p.OPCItemName == adres.TagAdresi);
                //    foreach (OpcItems item in items)
                //    {
                //        item.OPCItemValueChange += new OPCItemValueChangeEventHandler(Opcitemchange);
                //    }
                //}


                SetDataControlsReadOnlyRecursive(this);
                mngr.GlobalDataChangeKapat = false;
                mngr.ConnectGroupDataChangeKapat = false;
            }

        private void panelRight_Paint(object sender, PaintEventArgs e)
        {

        }
        }
      
    
    }

