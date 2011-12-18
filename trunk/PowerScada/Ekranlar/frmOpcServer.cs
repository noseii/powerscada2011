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
    public partial class frmOpcServer : Form
    {
        public static OpcManager mngr = null;
        OPCServer server;
        bool opcserverlisteyebasildi = false;

        public frmOpcServer()
        {
            InitializeComponent();
            mngr = new OpcManager();
         
            server=Persistence.Read<OPCServer>(new Condition[]{new Condition("Aktif",Operator.Equal,true)});
            if (server != null)
            {
                OPCServerGroup[] groups = OPCServer.ReadGroups(server.Id);
                server.Groups.AddRange(groups);
                dataGridView1.DataSource = server.Groups;
                textBoxServerIsmi.Text = server.OpcServerName;
                textBoxOPCNodeIsmi.Text = server.OPCNodeName;
            }
            else
                server = new OPCServer();


            dataGridView1.SetGridStyle(
            @"<Style>
                    <Column Name='Id'                       HeaderText='Id'                                 Width='100'   DisplayIndex='0'    Visible='false'                                         />
                    <Column Name='OPCGroupName'             HeaderText='OPCGroupName'                       Width='100'   DisplayIndex='1'    Visible='true'                                         />                    
                    <Column Name='GroupUpdateRate'          HeaderText='GroupUpdateRate'                    Width='100' DisplayIndex='2'    Visible='true'                                          />
                    <Column Name='GroupDeadBand'            HeaderText='GroupDeadBand'                      Width='100' DisplayIndex='3'    Visible='true'                                          />                 
                    <Column Name='GroupActiveState'         HeaderText='GroupActiveState'                   Width='100' DisplayIndex='4'    Visible='true'    Type ='CheckBox'                      />                 
            </Style>");
        }

        private void ListOPCServers_Click(object sender, EventArgs e)
        {
            opcserverlisteyebasildi = true;
            AvailableOPCServerList.Items.Clear();
            foreach (string str in mngr.GetOpcServerList())
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
                //OPCServerConnect.Enabled = false;
                //AvailableOPCServerList.Enabled = false;
                //OPCServerName.Enabled = false;

                //' Enable the group controls now that we have a server connection
                //OPCGroupName.Enabled = true;
                //GroupUpdateRate.Enabled = true;
                //GroupDeadBand.Enabled = true;
                //GroupActiveState.Enabled = true;
                //AddOPCGroup.Enabled = true;
            }
            catch (Exception ex)
            {

                //DisconnectFromServer.Enabled = false;

            };

        }

        private void AvailableOPCServerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            OPCServerName.Text = AvailableOPCServerList.Text;
            textBoxServerIsmi.Text = AvailableOPCServerList.Text;

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
                //OPCServerConnect.Enabled = true;
                //AvailableOPCServerList.Enabled = true;
                //OPCServerName.Enabled = true;

                //' Don't alllow the Disconnect to be issued now that the connection is closed
                //DisconnectFromServer.Enabled = true;

                //' Disable the group controls now that we no longer have a server connection
                //OPCGroupName.Enabled = true;
                //GroupUpdateRate.Enabled = true;
                //GroupDeadBand.Enabled = true;
                //GroupActiveState.Enabled = true;
                //AddOPCGroup.Enabled = true;
            }


        }

        private void AddOPCGroup_Click(object sender, EventArgs e)
        {
            try
            {
                mngr.AddOpcGroupServer(OPCGroupName.Text, Convert.ToInt32(GroupUpdateRate.Text), Convert.ToBoolean(GroupActiveState.Checked), (float)Convert.ToDecimal(GroupDeadBand.Text));
               

                //'*****************************************************************
                //' Now that a group has been added disable the Add group Button and enable
                //' the Remove group Button.  This demo application adds only a single group
                //OPCGroupName.Enabled = false;
                //AddOPCGroup.Enabled = false;
                //RemoveOPCGroup.Enabled = true;

                ////' Enable the OPC item controls now that a group has been added


                ////' Disable the Disconnect Server button since we now have a group that must be removed first
                //DisconnectFromServer.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("OPC server add group failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);


            }



        }

        private void RemoveOPCGroup_Click(object sender, EventArgs e)
        {
            mngr.RemoveGroupServer(mngr.GroupName);
            //OPCGroupName.Enabled = true;
            //AddOPCGroup.Enabled = true;
            //RemoveOPCGroup.Enabled = false;
            //DisconnectFromServer.Enabled = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Transaction.Instance.Join(delegate()
            {
               
                try
                {
                    int i = Transaction.Instance.ExecuteNonQuery(" delete from OPCServer where aktif=@prm0;delete from OPCServerGroup where aktif=@prm0", 1);
                }
                catch (Exception)
                {
                    throw new Exception("OPC Server silinemedi");
                }

                

                if (opcserverlisteyebasildi)
                {
                    server.OPCNodeName = OPCNodeName.Text;
                    server.OpcServerName = OPCServerName.Text;
                }
                else
                {
                    server.OPCNodeName =server.OPCNodeName;
                    server.OpcServerName = server.OpcServerName;
                }
                server.Id = 0;
                server.Insert();

               List<OPCServerGroup> groups = (dataGridView1.DataSource as List<OPCServerGroup>);
               foreach (OPCServerGroup grp in groups)
               {
                   OPCServerGroup grup = new OPCServerGroup();
                   grup.OPCServer.Id = server.Id;
                   grup.OPCGroupName = grp.OPCGroupName;
                   grup.GroupActiveState = grp.GroupActiveState;
                   grup.GroupDeadBand = grp.GroupDeadBand;
                   grup.GroupUpdateRate = grp.GroupUpdateRate;
                   grup.Insert();
               }

               MessageBox.Show("Kayıt Yapıldı.");
            

            });
            
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OPCServerGroup group =new OPCServerGroup();
            
            group.OPCGroupName = OPCGroupName.Text;
            group.GroupActiveState = GroupActiveState.Checked;
            group.GroupDeadBand =Convert.ToDecimal(GroupDeadBand.Text);
            group.GroupUpdateRate =Convert.ToInt32(GroupUpdateRate.Text);

            server.Groups.Add(group);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = server.Groups;
           

        }

        private void buttonSil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null )
            {
                mymodel.OPCServerGroup group = ((mymodel.OPCServerGroup)dataGridView1.CurrentRow.DataBoundItem);
                server.Groups.Remove(group);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = server.Groups;
            }
            else
                MessageBox.Show("Group Seçmelisiniz.","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

        private void OPCNodeName_Validated(object sender, EventArgs e)
        {
            textBoxOPCNodeIsmi.Text = OPCNodeName.Text;
        }

        


       


    
    }

}