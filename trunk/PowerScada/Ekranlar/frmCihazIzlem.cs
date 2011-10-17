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

namespace PowerScada
{
    public partial class frmCihazIzlem : Form
    {
        OpcManager mngr = null;
        
        public frmCihazIzlem()
        {
            InitializeComponent();
            mymodel.Adres[] adresler = Persistence.ReadList<Adres>();
            List<string> adreslist=new List<string>();
            foreach (Adres ent in adresler)
	        {
		        adreslist.Add(ent.TagAdresi);
                listBox1.Items.Add(ent.tagadresi);
	        }

            mngr = new OpcManager(adreslist);

            Cihaz[] cihazlar = Persistence.ReadList<Cihaz>("Select * From Cihaz Where Aktif=@prm0 ",new object[] {true});
            foreach (Cihaz cihaz in cihazlar)
            {
                UserControlCihazTanim uccihaz = new UserControlCihazTanim(cihaz);
                flowLayoutPanel1.Controls.Add(uccihaz);
            }

        }

        private void ListOPCServers_Click(object sender, EventArgs e)
        {
             foreach (string str in  mngr.GetOpcServerList())
            {
                AvailableOPCServerList.Items.Add(str);
            }
        }

        private void OPCServerConnect_Click(object sender, EventArgs e)
        {
            try
            {
                mngr.OPCServerConnect(OPCServerName.Text, OPCNodeName.Text);
                //' Throughout this example you will see a lot of code that simply enables
                //' and disables the various controls on the form.  The purpose of these
                //' actions is to demonstrate and insure the proper sequence of events when
                //' making an OPC connection.
                //' If we successfully connect to a server allow the user to disconnect
                DisconnectFromServer.Enabled = true;

                //' Don't allow a reconnect until the user disconnects
                OPCServerConnect.Enabled = false;
                AvailableOPCServerList.Enabled = false;
                OPCServerName.Enabled = false;

                //' Enable the group controls now that we have a server connection
                OPCGroupName.Enabled = true;
                GroupUpdateRate.Enabled = true;
                GroupDeadBand.Enabled = true;
                GroupActiveState.Enabled = true;
                AddOPCGroup.Enabled = true;
            }
            catch (Exception ex)
            {

                DisconnectFromServer.Enabled = false;
               
            };

        }

        private void AvailableOPCServerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            OPCServerName.Text = AvailableOPCServerList.Text;
        }

        private void DisconnectFromServer_Click(object sender, EventArgs e)
        {
            try
            {
                mngr.DisConnectServer();
            }
            catch (Exception ex)
            {
                MessageBox.Show("OPC server disconnect failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);
            }
            finally
            {
               

                //' Allow a reconnect once the disconnect completes
                OPCServerConnect.Enabled = true;
                AvailableOPCServerList.Enabled = true;
                OPCServerName.Enabled = true;

                //' Don't alllow the Disconnect to be issued now that the connection is closed
                DisconnectFromServer.Enabled = true;

                //' Disable the group controls now that we no longer have a server connection
                OPCGroupName.Enabled = true;
                GroupUpdateRate.Enabled = true;
                GroupDeadBand.Enabled = true;
                GroupActiveState.Enabled = true;
                AddOPCGroup.Enabled = true;
            }

           
        }

        private void AddOPCGroup_Click(object sender, EventArgs e)
        {
            try
            {


                mngr.AddOpcGroupServer(OPCGroupName.Text, Convert.ToInt32(GroupUpdateRate.Text), Convert.ToBoolean(GroupActiveState.Checked), (float)Convert.ToDecimal(GroupDeadBand.Text));
                mngr.OPCAddItems();

                //'*****************************************************************
                //' Now that a group has been added disable the Add group Button and enable
                //' the Remove group Button.  This demo application adds only a single group
                OPCGroupName.Enabled = false;
                AddOPCGroup.Enabled = false;
                RemoveOPCGroup.Enabled = true;

                //' Enable the OPC item controls now that a group has been added
             

                //' Disable the Disconnect Server button since we now have a group that must be removed first
                DisconnectFromServer.Enabled = false;
          }
            catch (Exception ex)
            {
                MessageBox.Show("OPC server add group failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);


            }
        }

        private void RemoveOPCGroup_Click(object sender, EventArgs e)
        {
            mngr.RemoveGroupServer(mngr.GroupName);
            OPCGroupName.Enabled = true;
            AddOPCGroup.Enabled = true;
            RemoveOPCGroup.Enabled = false;
            DisconnectFromServer.Enabled = true;
      
        }

       
    }
}

