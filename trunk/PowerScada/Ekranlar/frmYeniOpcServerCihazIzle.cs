


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Collections.Generic;

namespace PowerScada
{
    public partial class frmYeniOpcServerCihazIzle : Form
    {

         int CihazAdedi = 0;
      
        //' Also note what the actual array size is.( CihazAdedi SPECIFIES Upper Bound of Array )
        //' To summarize then:
        //'	1) Array size = 11
        //'	2) We use indexes 1 thru 10
        //'	3) Index 0 is not used at all
        int ACTUAL_ARRAY_SIZE = 0;

        OPCAutomation.OPCServer AnOPCServer;
        OPCAutomation.OPCServer ConnectedOPCServer;
        OPCAutomation.OPCGroup ConnectedGroup;

        //' OPC Item related data
        List<string> OPCItemIDs = new System.Collections.Generic.List<string>();
        List<int> ClientHandles = new System.Collections.Generic.List<int>();
        System.Array ItemServerHandles;
        //string[] OPCItemIDs = null;
        //int[] ClientHandles = null;
        //System.Array ItemServerHandles;



        List<TextBox> OPCItemName = new System.Collections.Generic.List<TextBox>();
        List<TextBox> OPCItemValue = new System.Collections.Generic.List<TextBox>();
        List<TextBox> OPCItemValueToWrite = new System.Collections.Generic.List<TextBox>();
        List<Button> OPCItemWriteButton = new System.Collections.Generic.List<Button>();
        List<CheckBox> OPCItemActiveState = new System.Collections.Generic.List<CheckBox>();
        List<Button> OPCItemSyncReadButton = new System.Collections.Generic.List<Button>();
        List<TextBox> OPCItemQuality = new System.Collections.Generic.List<TextBox>();
        List<int> OPCItemIsArray = new System.Collections.Generic.List<int>();

        ////' Arrays are used to provide iterative access to sets of controls
        //TextBox[] OPCItemName =null;
        //TextBox[] OPCItemValue = null;
        //TextBox[] OPCItemValueToWrite = null;
        //Button[] OPCItemWriteButton = null;
        //CheckBox[] OPCItemActiveState =null;
        //Button[] OPCItemSyncReadButton = null;
        //TextBox[] OPCItemQuality = null;
        //int[] OPCItemIsArray = null;






        public frmYeniOpcServerCihazIzle()
        {
            InitializeComponent();
            AvailableOPCServerList.Items.Add("Click on 'List OPC Servers' to start");

            mymodel.Cihaz[] cihazlar = SharpBullet.OAL.Persistence.ReadList<mymodel.Cihaz>("Select * from Cihaz Where Aktif=@prm0",new object[]{true});
            CihazAdedi = cihazlar.Length;
            if (cihazlar.Length > 0)
            {
                int cihazno = 1;
               
                foreach (mymodel.Cihaz chz in cihazlar)
                {
                    UserControlCihazTanim ucchz = new UserControlCihazTanim();
                    ucchz.Cihaz = chz;
                    ucchz.CihazNo = cihazno;
                    cihazno++;
                    flowLayoutPanel1.Controls.Add(ucchz);
                }

              
               
            }





           
            //ACTUAL_ARRAY_SIZE = CihazAdedi + 1;
            //OPCItemIDs = new string[ACTUAL_ARRAY_SIZE];
            //ClientHandles = new int[ACTUAL_ARRAY_SIZE];
            //OPCItemName = new TextBox[ACTUAL_ARRAY_SIZE];
            //OPCItemValue = new TextBox[ACTUAL_ARRAY_SIZE];
            //OPCItemValueToWrite = new TextBox[ACTUAL_ARRAY_SIZE];
            //OPCItemWriteButton = new Button[ACTUAL_ARRAY_SIZE];
            //OPCItemActiveState = new CheckBox[ACTUAL_ARRAY_SIZE];
            //OPCItemSyncReadButton = new Button[ACTUAL_ARRAY_SIZE];
            //OPCItemQuality = new TextBox[ACTUAL_ARRAY_SIZE];
            //OPCItemIsArray = new int[ACTUAL_ARRAY_SIZE];
            OPCItemWriteButton.Add(null);
            OPCItemValue.Add(null);
            OPCItemIDs.Add(null);
            ClientHandles.Add(0);
        
            for (int i = 1; i < flowLayoutPanel1.Controls.Count+1; i++)
            {
                UserControlCihazTanim chztanim = (UserControlCihazTanim)flowLayoutPanel1.Controls[i-1];

                chztanim.buttonYaz.Click += new EventHandler(_OPCItemWriteButton_0_Click);
                OPCItemWriteButton.Add(chztanim.buttonYaz);
                OPCItemValue.Add(chztanim.textBoxOkunanDeger);
                //if (chztanim.Cihaz.IsSigortasiVar)
                //    OPCItemValue.Add(chztanim.textBoxSigortadegeri);

                //OPCItemWriteButton[i] = chztanim.buttonYaz;
                //OPCItemValue[i] = chztanim.textBoxOkunanDeger;
            }

            CihazAdedi = OPCItemValue.Count;
          

        }

        private void ListOPCServers_Click(object sender, EventArgs e)
        {
            try
            {
                // Create a temporary OPCServer object and use it to get the list of
                // available OPC Servers
                AnOPCServer = new OPCAutomation.OPCServer();

                // Clear the list control used to display them
                AvailableOPCServerList.Items.Clear();

                // Obtain the list of available OPC servers
                object AllOPCServers = null;
                AllOPCServers = AnOPCServer.GetOPCServers();

                // Load the list returned into the List box for user selection
                int i = 0;

                for (i = Microsoft.VisualBasic.Information.LBound((System.Array)AllOPCServers, 1); i <= Information.UBound((System.Array)AllOPCServers, 1); i++)
                {
                    AvailableOPCServerList.Items.Add(((System.Array)AllOPCServers).GetValue(i));
                }

                // Release the temporary OPCServer object now that we're done with it
                AnOPCServer = null;

            }
            catch (Exception ex)
            {
                // Error handling
                MessageBox.Show("List OPC servers failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);
            }
        }

        private void AvailableOPCServerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            OPCServerName.Text = AvailableOPCServerList.Text;
        }

        private void OPCServerConnect_Click(object sender, EventArgs e)
        {
            //If InStr(OPCServerName.Text, "Click") = 0 Then
            try
            {


                //'Create a new OPC Server object
                ConnectedOPCServer = new OPCAutomation.OPCServer();

                //'Attempt to connect with the server
                ConnectedOPCServer.Connect(OPCServerName.Text, OPCNodeName.Text);

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
                //' Remove group isn't enable until a group has been added

            }
            catch (Exception ex)
            {

                DisconnectFromServer.Enabled = false;
                ConnectedOPCServer = null;
                MessageBox.Show("OPC server connect failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);

            };
            //        ' Error handling
            //            End Try

            //Else
            //    ' A server name has not been selected yet post an error to the user
            //    MessageBox.Show("You must first select an OPC Server, Click on the 'List OPC Servers' button and select a server", "OPC Server Connect", MessageBoxButtons.OK)
            //End If
        }

        private void DisconnectFromServer_Click(object sender, EventArgs e)
        {
            if (ConnectedOPCServer != null)
            {
                try
                {
                    ConnectedOPCServer.Disconnect();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("OPC server disconnect failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);
                }
                finally
                {
                    ConnectedOPCServer = null;

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
        }

        private void AddOPCGroup_Click(object sender, EventArgs e)
        {
            try
            {


                //' Set the desire active state for the group
                ConnectedOPCServer.OPCGroups.DefaultGroupIsActive = Convert.ToBoolean(GroupActiveState.CheckState);

                //'Set the desired percent deadband
                ConnectedOPCServer.OPCGroups.DefaultGroupDeadband = (float)Convert.ToDecimal(GroupDeadBand.Text);

                //' Add the group and set its update rate
                ConnectedGroup = ConnectedOPCServer.OPCGroups.Add(OPCGroupName.Text);

                //' Set the update rate for the group
                ConnectedGroup.UpdateRate = Convert.ToInt32(GroupUpdateRate.Text);

                //' ****************************************************************
                //' Mark this group to receive asynchronous updates via the DataChange event.
                //' This setting is IMPORTANT. Without setting '.IsSubcribed' to True your
                //' VB application will not receive DataChange notifications.  This will
                //' make it appear that you have not properly connected to the server.
                ConnectedGroup.IsSubscribed = true; ;

                //'*****************************************************************
                //' Now that a group has been added disable the Add group Button and enable
                //' the Remove group Button.  This demo application adds only a single group
                OPCGroupName.Enabled = false;
                AddOPCGroup.Enabled = false;
                RemoveOPCGroup.Enabled = true;

                //' Enable the OPC item controls now that a group has been added
                OPCAddItems.Enabled = true;

                //for (int i = 1; i <= CihazAdedi; i++)
                //{
                //    OPCItemName[i].Enabled = true;
                //}
                //For i As Short = 1 To CihazAdedi
                //    OPCItemName(i).Enabled = true;
                //Next

                //' Disable the Disconnect Server button since we now have a group that must be removed first
                DisconnectFromServer.Enabled = false;
                ConnectedGroup.DataChange += new OPCAutomation.DIOPCGroupEvent_DataChangeEventHandler(ConnectedGroup_DataChange);
            }
            catch (Exception ex)
            {
                MessageBox.Show("OPC server add group failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);


            }

        }


        private void OPCRemoveItems_Click(object sender, System.EventArgs e)
        {
            if ((ConnectedGroup != null))
            {
                if ((ConnectedGroup.OPCItems.Count != 0))
                {
                    try
                    {
                        //  Provide an array to contain the ItemServerHandles of the item
                        //  we intend to remove
                        int[] RemoveItemServerHandles = new int[CihazAdedi];
                        
                        //  Array for potential error returns.  This example doesn't
                        //  check them but yours should ultimately.
                        System.Array RemoveItemServerErrors;
                        //  Get the Servers handle for the desired items.  The server handles
                        //  were returned in add item subroutine.  In this case we need to get
                        //  only the handles for item that are valid.
                        int ItemCount = 0;
                        for (int i = 1; (i <= CihazAdedi); i++)
                        {
                            //  In this example if the ItemServerHandle is non zero it is valid
                            if (((int)ItemServerHandles.GetValue(i) != 0))
                            {
                                
                                RemoveItemServerHandles.SetValue(ItemServerHandles.GetValue(i), ItemCount);
                                ItemCount = (ItemCount + 1);
                               
                            }
                        }
                        //  Invoke the Remove Item operation.  Remember this call will
                        //  wait until completion
                        System.Array removeitemserverhandles = RemoveItemServerHandles.ToArray<int>();
                        ConnectedGroup.OPCItems.Remove(ItemCount-1, ref removeitemserverhandles, out RemoveItemServerErrors);
                        for (short i = 1; (i <= ItemCount-1); i++)
                        {
                            if (((int)RemoveItemServerErrors.GetValue(i) != 0))
                            {
                                MessageBox.Show(("OPC server remove item failed with error: " + RemoveItemServerErrors.GetValue(i)), "OPC remove item", MessageBoxButtons.OK);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //  Error handling
                        MessageBox.Show(("OPC server remove items failed with exception: " + ex.Message), "SimpleOPCInterface Exception", MessageBoxButtons.OK);
                    }
                    finally
                    {
                        //  Clear the ItemServerHandles and turn off the controls for interacting
                        //  with the OPC items on the form.
                        for (short i = 1; (i <= CihazAdedi); i++)
                        {
                            ItemServerHandles.SetValue(0, i);
                            // Mark the handle as empty
                            //OPCItemValueToWrite[i].Enabled = false;
                            //OPCItemWriteButton[i].Enabled = false;
                            //OPCItemActiveState[i].Enabled = false;
                            //OPCItemSyncReadButton[i].Enabled = false;
                        }
                        //  Enable the Add OPC item button and Remove Group button now that the
                        //  items are released
                        //OPCAddItems.Enabled = true;
                        //RemoveOPCGroup.Enabled = true;
                        //OPCRemoveItems.Enabled = false;
                        for (short i = 1; (i <= CihazAdedi); i++)
                        {
                            //OPCItemName[i].Enabled = true;
                            OPCItemIsArray[i] = 0;
                        }
                    }
                }
            }
        }


        private void _OPCItemWriteButton_0_Click(object sender, EventArgs e)
        {
            if (ConnectedGroup != null)
            {
                //' Get control index from name
                int index = -1;

                if (((System.Windows.Forms.Button)sender).Parent.Parent.Parent.Parent is UserControlCihazTanim)
                    index = ((UserControlCihazTanim)((Button)sender).Parent.Parent.Parent.Parent).CihazNo;
                //if (((Button)sender).Name == "_OPCItemWriteButton_0")
                //    index = 1;
                //else if (((Button)sender).Name == "_OPCItemWriteButton_1")
                //    index = 2;
                //else if (((Button)sender).Name == "_OPCItemWriteButton_2")
                //    index = 3;
                //else if (((Button)sender).Name == "_OPCItemWriteButton_3")
                //    index = 4;
                //else if (((Button)sender).Name == "_OPCItemWriteButton_4")
                //    index = 5;
                //else if (((Button)sender).Name == "_OPCItemWriteButton_5")
                //    index = 6;
                //else if (((Button)sender).Name == "_OPCItemWriteButton_6")
                //    index = 7;
                //else if (((Button)sender).Name == "_OPCItemWriteButton_7")
                //    index = 8;
                //else if (((Button)sender).Name == "_OPCItemWriteButton_8")
                //    index = 9;
                //else
                //    index = 10;

                try
                {
                    short ItemCount = 1;
                    int[] SyncItemServerHandles = new int[2];
                    object[] SyncItemValues = new object[2];
                    System.Array SyncItemServerErrors;
                    OPCAutomation.OPCItem AnOpcItem;
                    SyncItemServerHandles[1] = Convert.ToInt32(ItemServerHandles.GetValue(index));
                    AnOpcItem = ((OPCAutomation.OPCItem)ConnectedGroup.OPCItems.GetOPCItem((int)ItemServerHandles.GetValue(index)));


                    Array ItsAnArray;
                    short CanonDT;
                    CanonDT = AnOpcItem.CanonicalDataType;

                    //        ' If it is an array, figure out the base type
                    //if( CanonDT > vbArray Then
                    //    CanonDT -= vbArray
                    //End If
                    UserControlCihazTanim chztnm = (UserControlCihazTanim)flowLayoutPanel1.Controls[index - 1];
                    switch (CanonDT)
                    {
                      
                       

                        case (short)CanonicalDataTypes.CanonDtByte:
                            if (OPCItemIsArray[index] > 0)
                            {
                                string str = chztnm.textBoxYazilacakDeger.Text;
                                ItsAnArray = Array.CreateInstance(Type.GetType("Byte"), OPCItemIsArray[index]);
                                if (!LoadArray(ref ItsAnArray, CanonDT, ref str))
                                    return;
                                else
                                    SyncItemValues[1] = (ItsAnArray); //SyncItemValues[1] = CObj(ItsAnArray);
                            }
                            else
                                SyncItemValues[1] = Convert.ToByte(chztnm.textBoxYazilacakDeger.Text);
                            break;
                        case (short)CanonicalDataTypes.CanonDtChar:
                            if (OPCItemIsArray[index] > 0)
                            {
                                string str = chztnm.textBoxYazilacakDeger.Text;
                                ItsAnArray = Array.CreateInstance(Type.GetType("SByte"), OPCItemIsArray[index]);
                                if (!LoadArray(ref ItsAnArray, CanonDT, ref str))
                                    return;
                                else
                                    SyncItemValues[1] = (ItsAnArray); //SyncItemValues[1] = CObj(ItsAnArray);
                            }
                            else
                                SyncItemValues[1] = Convert.ToByte(chztnm.textBoxYazilacakDeger.Text);
                            break;
                        case (short)CanonicalDataTypes.CanonDtWord:
                            if (OPCItemIsArray[index] > 0)
                            {
                                string str = chztnm.textBoxYazilacakDeger.Text;
                                ItsAnArray = Array.CreateInstance(Type.GetType("UInt16"), OPCItemIsArray[index]);
                                if (!LoadArray(ref ItsAnArray, CanonDT, ref str))
                                    return;
                                else
                                    SyncItemValues[1] = (ItsAnArray); //SyncItemValues[1] = CObj(ItsAnArray);
                            }
                            else
                                SyncItemValues[1] = Convert.ToByte(chztnm.textBoxYazilacakDeger.Text);
                            break;
                        case (short)CanonicalDataTypes.CanonDtShort:
                            if (OPCItemIsArray[index] > 0)
                            {
                                string str = chztnm.textBoxYazilacakDeger.Text;
                                ItsAnArray = Array.CreateInstance(Type.GetType("Int16"), OPCItemIsArray[index]);
                                if (!LoadArray(ref ItsAnArray, CanonDT, ref str))
                                    return;
                                else
                                    SyncItemValues[1] = (ItsAnArray); //SyncItemValues[1] = CObj(ItsAnArray);
                            }
                            else
                                SyncItemValues[1] = Convert.ToInt16(chztnm.textBoxYazilacakDeger.Text);
                            break;


                        case (short)CanonicalDataTypes.CanonDtDWord:
                            if (OPCItemIsArray[index] > 0)
                            {
                                string str = chztnm.textBoxYazilacakDeger.Text;
                                ItsAnArray = Array.CreateInstance(Type.GetType("UInt32"), OPCItemIsArray[index]);
                                if (!LoadArray(ref ItsAnArray, CanonDT, ref str))
                                    return;
                                else
                                    SyncItemValues[1] = (ItsAnArray); //SyncItemValues[1] = CObj(ItsAnArray);
                            }
                            else
                                SyncItemValues[1] = Convert.ToByte(chztnm.textBoxYazilacakDeger.Text);
                            break;
                        case (short)CanonicalDataTypes.CanonDtLong:
                            if (OPCItemIsArray[index] > 0)
                            {
                                string str = chztnm.textBoxYazilacakDeger.Text;
                                ItsAnArray = Array.CreateInstance(Type.GetType("Int32"), OPCItemIsArray[index]);
                                if (!LoadArray(ref ItsAnArray, CanonDT, ref str))
                                    return;
                                else
                                    SyncItemValues[1] = (ItsAnArray); //SyncItemValues[1] = CObj(ItsAnArray);
                            }
                            else
                                SyncItemValues[1] = Convert.ToByte(chztnm.textBoxYazilacakDeger.Text);
                            break;
                        case (short)CanonicalDataTypes.CanonDtFloat:
                            if (OPCItemIsArray[index] > 0)
                            {
                                string str = chztnm.textBoxYazilacakDeger.Text;
                                ItsAnArray = Array.CreateInstance(Type.GetType("Single"), OPCItemIsArray[index]);
                                if (!LoadArray(ref ItsAnArray, CanonDT, ref str))
                                    return;
                                else
                                    SyncItemValues[1] = (ItsAnArray); //SyncItemValues[1] = CObj(ItsAnArray);
                            }
                            else
                                SyncItemValues[1] = Convert.ToByte(chztnm.textBoxYazilacakDeger.Text);
                            break;
                        case (short)CanonicalDataTypes.CanonDtDouble:
                            if (OPCItemIsArray[index] > 0)
                            {
                                string str = chztnm.textBoxYazilacakDeger.Text;
                                ItsAnArray = Array.CreateInstance(Type.GetType("Double"), OPCItemIsArray[index]);
                                if (!LoadArray(ref ItsAnArray, CanonDT, ref str))
                                    return;
                                else
                                    SyncItemValues[1] = (ItsAnArray); //SyncItemValues[1] = CObj(ItsAnArray);
                            }
                            else
                                SyncItemValues[1] = Convert.ToByte(chztnm.textBoxYazilacakDeger.Text);
                            break;
                        case (short)CanonicalDataTypes.CanonDtBool:
                            if (OPCItemIsArray[index] > 0)
                            {
                                string str = chztnm.textBoxYazilacakDeger.Text;
                                ItsAnArray = Array.CreateInstance(Type.GetType("Boolean"), OPCItemIsArray[index]);
                                if (!LoadArray(ref ItsAnArray, CanonDT, ref str))
                                    return;
                                else
                                    SyncItemValues[1] = (ItsAnArray); //SyncItemValues[1] = CObj(ItsAnArray);
                            }
                            else
                                SyncItemValues[1] = Convert.ToByte(chztnm.textBoxYazilacakDeger.Text);
                            break;
                        case (short)CanonicalDataTypes.CanonDtString:
                            if (OPCItemIsArray[index] > 0)
                            {
                                string str = chztnm.textBoxYazilacakDeger.Text;
                                ItsAnArray = Array.CreateInstance(Type.GetType("String"), OPCItemIsArray[index]);
                                if (!LoadArray(ref ItsAnArray, CanonDT, ref str))
                                    return;
                                else
                                    SyncItemValues[1] = (ItsAnArray); //SyncItemValues[1] = CObj(ItsAnArray);
                            }
                            else
                                SyncItemValues[1] = Convert.ToByte(chztnm.textBoxYazilacakDeger.Text);
                            break;

                        default:
                            MessageBox.Show("OPCItemWriteButton Unknown data type", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                            break;
                    }
                    Array syncitemserverhandles = SyncItemServerHandles.ToArray<int>();
                    Array syncitemvalues = SyncItemValues.ToArray<object>();
                    //' Invoke the SyncWrite operation.  Remember this call will wait until completion
                    ConnectedGroup.SyncWrite(ItemCount, ref syncitemserverhandles, ref syncitemvalues, out SyncItemServerErrors);

                    if ((int)SyncItemServerErrors.GetValue(1) != 0)
                        MessageBox.Show("SyncItemServerError: " + SyncItemServerErrors.GetValue(1));



                }
                catch (Exception ex)
                {

                    MessageBox.Show("OPC server write item failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);
                }
            }

        }

      

        private bool LoadArray(ref System.Array AnArray, short CanonDT, ref string wrTxt)
        {
            int ii;
            int loc;
            int Wlen;
            int start;
            try
            {
                start = 1;
                Wlen = wrTxt.Length;
                for (ii = AnArray.GetLowerBound(0); (ii <= AnArray.GetUpperBound(0)); ii++)
                {
                    loc = (wrTxt.IndexOf(",", (start - 1)) + 1);
                    if ((ii < AnArray.GetUpperBound(0)))
                    {
                        if ((loc == 0))
                        {
                            MessageBox.Show("Write Value: Incorrect Number of Items for Array Size?", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return false;
                        }
                    }
                    else
                    {
                        loc = (Wlen + 1);
                    }
                    switch ((CanonicalDataTypes)CanonDT)
                    {
                        case CanonicalDataTypes.CanonDtByte:
                            AnArray.SetValue(Convert.ToByte(wrTxt.Substring((start - 1), (loc - start))), ii);
                            //  End case
                            break;
                        case CanonicalDataTypes.CanonDtChar:
                            AnArray.SetValue(Convert.ToSByte(wrTxt.Substring((start - 1), (loc - start))), ii);
                            //  End case
                            break;
                        case CanonicalDataTypes.CanonDtWord:
                            AnArray.SetValue(Convert.ToUInt16(wrTxt.Substring((start - 1), (loc - start))), ii);
                            //  End case
                            break;
                        case CanonicalDataTypes.CanonDtShort:
                            AnArray.SetValue(Convert.ToInt16(wrTxt.Substring((start - 1), (loc - start))), ii);
                            //  End case
                            break;
                        case CanonicalDataTypes.CanonDtDWord:
                            AnArray.SetValue(Convert.ToUInt32(wrTxt.Substring((start - 1), (loc - start))), ii);
                            //  End case
                            break;
                        case CanonicalDataTypes.CanonDtLong:
                            AnArray.SetValue(Convert.ToInt32(wrTxt.Substring((start - 1), (loc - start))), ii);
                            //  End case
                            break;
                        case CanonicalDataTypes.CanonDtFloat:
                            AnArray.SetValue(Convert.ToSingle(wrTxt.Substring((start - 1), (loc - start))), ii);
                            //  End case
                            break;
                        case CanonicalDataTypes.CanonDtDouble:
                            AnArray.SetValue(Convert.ToDouble(wrTxt.Substring((start - 1), (loc - start))), ii);
                            //  End case
                            break;
                        case CanonicalDataTypes.CanonDtBool:
                            AnArray.SetValue(Convert.ToBoolean(wrTxt.Substring((start - 1), (loc - start))), ii);
                            //  End case
                            break;
                        case CanonicalDataTypes.CanonDtString:
                            AnArray.SetValue(Convert.ToString(wrTxt.Substring((start - 1), (loc - start))), ii);
                            //  End case
                            break;
                        default:
                            MessageBox.Show("Write Value Unknown data type", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return false;
                            break;
                    }
                    start = (loc + 1);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(("Write Value generated Exception: " + ex.Message), "SimpleOPCInterface Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        private void _OPCItemSyncReadButton_0_Click(object sender, EventArgs e)
        {
            if ((ConnectedGroup != null))
            {
                //    ' Get control index from name
               
                int index = -1;
                UserControlCihazTanim chztanim = ((UserControlCihazTanim)((Button)sender).Parent.Parent.Parent.Parent);
                if (((System.Windows.Forms.Button)sender).Parent.Parent.Parent.Parent is UserControlCihazTanim)
                    index = ((UserControlCihazTanim)((Button)sender).Parent.Parent.Parent.Parent).CihazNo;
                //if (((Button)sender).Name == "_OPCItemSyncReadButton_0")
                //    index = 1;
                //else if (((Button)sender).Name == "_OPCItemSyncReadButton_1")
                //    index = 2;
                //else if (((Button)sender).Name == "_OPCItemSyncReadButton_2")
                //    index = 3;
                //else if (((Button)sender).Name == "_OPCItemSyncReadButton_3")
                //    index = 4;
                //else if (((Button)sender).Name == "_OPCItemSyncReadButton_4")
                //    index = 5;
                //else if (((Button)sender).Name == "_OPCItemSyncReadButton_5")
                //    index = 6;
                //else if (((Button)sender).Name == "_OPCItemSyncReadButton_6")
                //    index = 7;
                //else if (((Button)sender).Name == "_OPCItemSyncReadButton_7")
                //    index = 8;
                //else if (((Button)sender).Name == "_OPCItemSyncReadButton_8")
                //    index = 9;
                //else
                //    index = 10;


                try
                {


                    //        ' Read only 1 item
                    short ItemCount = 1;

                    //        ' Provide storage the arrays returned by the 'SyncRead' method
                    int[] SyncItemServerHandles = new int[2];
                    System.Array SyncItemValues;
                    System.Array SyncItemServerErrors;

                    //        ' Get the Servers handle for the desired item.  The server handles were
                    //        ' returned in add item subroutine.
                    SyncItemServerHandles[1] = (int)ItemServerHandles.GetValue(index);

                    //        ' Invoke the SyncRead operation.  Remember this call will wait until
                    //        ' completion. The source flag in this case, 'OPCDevice' , is set to
                    //        ' read from device which may take some time.
                    object timestamp = null;
                    object qualities = null;
                    System.Array syncitemserverhandles = SyncItemServerHandles.ToArray<int>();
                    ConnectedGroup.SyncRead((short)OPCAutomation.OPCDataSource.OPCDevice, ItemCount, ref syncitemserverhandles, out SyncItemValues, out SyncItemServerErrors, out qualities, out timestamp);

                    //        ' Save off the value returned after checking for error
                    if (((int)SyncItemServerErrors.GetValue(1) == 0))
                    {
                        if (IsArray(SyncItemValues.GetValue(1).GetType()))
                        {
                            Array ItsAnArray;
                            int x;
                            string Suffix;
                            ItsAnArray = (System.Array)SyncItemValues.GetValue(1);
                            chztanim.textBoxOkunanDeger.Text = "";
                            //((TextBox)OPCItemValue.GetValue(index)).Text = "";

                            for (x = ItsAnArray.GetLowerBound(0); (x <= ItsAnArray.GetUpperBound(0)); x++)
                            {
                                if ((x == ItsAnArray.GetUpperBound(0)))
                                {
                                    Suffix = "";
                                }
                                else
                                {
                                    Suffix = ", ";
                                }
                                //((TextBox)(OPCItemValue.GetValue(index))).Text = (((TextBox)OPCItemValue.GetValue(index)).Text + (ItsAnArray.GetValue(x) + Suffix));
                                chztanim.textBoxOkunanDeger.Text=(chztanim.textBoxOkunanDeger.Text+(ItsAnArray.GetValue(x) + Suffix));
                            }
                        }
                        else
                        {
                            //((TextBox)OPCItemValue.GetValue(index)).Text = SyncItemValues.GetValue(1).ToString();
                             chztanim.textBoxOkunanDeger.Text= SyncItemValues.GetValue(1).ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show(("SyncItemServerError: " + SyncItemServerErrors.GetValue(1)));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("OPC server read item failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);
                }

            }
        }

        private void OPCAddItems_Click(object sender, EventArgs e)
        {
            if ((ConnectedGroup != null))
            {
                try
                {


                    int ItemCount = CihazAdedi;
               
                    //' Array for potential error returns.  This example doesn't
                    //' check them but yours should ultimately.
                    System.Array AddItemServerErrors;
                    int sayac = 1;
                    //' Load the request OPC Item names and build the ClientHandles list
                    for (int i = 1; i <= CihazAdedi; i++)
                    {
                        //' Load the name of then item to be added to this group.  You can add
                        //' as many items as you want to the group in a single call by building these
                        //' arrays as needed.
                        UserControlCihazTanim chz = (UserControlCihazTanim)flowLayoutPanel1.Controls[i - 1];
                        //OPCItemIDs[i] = chz.Cihaz.OkumaAdresi;// OPCItemName[i].Text;
                        //OPCItemIDs.Add(chz.Cihaz.OkumaAdresiFormulu);
                        //ClientHandles.Add(sayac);
                        //if (chz.Cihaz.IsSigortasiVar)
                        //{
                        //    sayac++;
                        //    OPCItemIDs.Add(chz.Cihaz.SigortaAdresiFormulu);
                        //    ClientHandles.Add(sayac);
                        //}
                        //' ASSume all aren't an array. If it is, this holds size and is set in
                        //' Data change event.
                        //OPCItemIsArray[i] = 0;

                        //' The client handles are given to the OPC Server for each item you intend
                        //' to add to the group.  The OPC Server will uses these client handles
                        //' by returning them to you in the 'DataChange' event.  You can use the
                        //' client handles as a key to linking each valued returned from the Server
                        //' back to some element in your application.  In this example we are simply
                        //' placing the Index number of each control that will be used to display
                        //' data for the item.  In your application the ClientHandle value you use
                        //' can by whatever you need to best fit your program.  You will see how
                        //' these client handles are used in the 'DataChange' event handler.
                        ClientHandles[i] = i;

                        //' Make the Items active start control Active, for the demo I want all items to start active
                        //' Your application may need to start the items as inactive.
                        //OPCItemActiveState[i].CheckState = System.Windows.Forms.CheckState.Checked;
                    }


                    //' Establish a connection to the OPC item interface of the connected group
                    //'                OPCItemCollection = ConnectedGroup.OPCItems

                    //' Setting the '.DefaultIsActive' property forces all items we are about to
                    //' add to the group to be added in an active state.  If you want to add them
                    //' all as inactive simply set this property false, you can always make the
                    //' items active later as needed using each item's own active state property.
                    //' One key distinction to note, the active state of an item is independent
                    //' from the group active state.  If a group is active but the item is
                    //' inactive no data will be received for the item.  Also changing the
                    //' state of the group will not change the state of an item.
                    ConnectedGroup.OPCItems.DefaultIsActive = true;

                    //' Atempt to add the items,  some may fail so the ItemServerErrors will need
                    //' to be check on completion of the call.  We are adding all item using the
                    //' default data type of VT_EMPTY and letting the server pick the appropriate
                    //' data type.  The ItemServerHandles is an array that the OPC Server will
                    //' return to your application.  This array like your own ClientHandles array
                    //' is used by the server to allow you to reference individual items in an OPC
                    //' group.  When you need to perform an action on a single OPC item you will
                    //' need to use the ItemServerHandles for that item.  With this said you need to
                    //' maintain the ItemServerHandles array for use throughout your application.
                    //' Use of the ItemServerHandles will be demonstrated in other subroutines in
                    //' this example program.
                    System.Array clienthandles = ClientHandles.ToArray<int>();
                    System.Array oPCitemIDs = OPCItemIDs.ToArray<string>();
                    ConnectedGroup.OPCItems.AddItems(ItemCount, ref oPCitemIDs, ref clienthandles, out ItemServerHandles, out AddItemServerErrors);

                    //' This next step checks the error return on each item we attempted to
                    //' register.  If an item is in error it's associated controls will be
                    //' disabled.  If all items are in error then the Add Item button will
                    //' remain active.
                    bool AnItemIsGood;
                    AnItemIsGood = false;

                    for (int i = 1; i <= CihazAdedi; i++)
                    {
                        if ((int)AddItemServerErrors.GetValue(i) == 0) 					  //'If the item was added successfully then allow it to be used.
                        {
                            //OPCItemValueToWrite[i].Enabled = true;
                            //OPCItemWriteButton[i].Enabled = true;
                            //OPCItemActiveState[i].Enabled = true;
                            //OPCItemSyncReadButton[i].Enabled = true;

                            AnItemIsGood = true;
                            //OPCItemValue[i].Enabled = true;
                        }
                        else
                        {
                            ItemServerHandles.SetValue(0, i);						// ' If the handle was bad mark it as empty
                            //OPCItemValueToWrite[i].Enabled = false;
                            //OPCItemWriteButton[i].Enabled = false;
                            //OPCItemActiveState[i].Enabled = false;
                            //OPCItemSyncReadButton[i].Enabled = false;

                            //OPCItemValue[i].Enabled = false;
                            //OPCItemValue[i].Text = "OPC Add Item Fail";
                            //cihaztanim[i].textBoxOkunanDeger.Text = "OPC Add Item Fail";
                            UserControlCihazTanim chz = (UserControlCihazTanim)flowLayoutPanel1.Controls[i];
                            chz.textBoxOkunanDeger.Text = "OPC Add Item Fail";
                        }
                    }
                    for (int i = 1; i <= CihazAdedi; i++)
                    {
                        if ((int)AddItemServerErrors.GetValue(i) == 0)					  //'If the item was added successfully then allow it to be used.
                        {
                            //OPCItemValueToWrite[i].Enabled = true;
                            //OPCItemWriteButton[i].Enabled = true;
                            //OPCItemActiveState[i].Enabled = true;
                            //OPCItemSyncReadButton[i].Enabled = true;

                            AnItemIsGood = true;
                            //OPCItemValue[i].Enabled = true;
                        }
                        else
                        {
                            ItemServerHandles.SetValue(0, i);						// ' If the handle was bad mark it as empty
                            //OPCItemValueToWrite[i].Enabled = false;
                            //OPCItemWriteButton[i].Enabled = false;
                            //OPCItemActiveState[i].Enabled = false;
                            //OPCItemSyncReadButton[i].Enabled = false;

                            //OPCItemValue[i].Enabled = false;
                            //OPCItemValue[i].Text = "OPC Add Item Fail";
                            //cihaztanim[i].textBoxOkunanDeger.Text = "OPC Add Item Fail";
                            UserControlCihazTanim chz = (UserControlCihazTanim)flowLayoutPanel1.Controls[i];
                            chz.textBoxOkunanDeger.Text = "OPC Add Item Fail";
                        }
                    }
                    //' Disable the Add OPC item button if any item in the list was good
                    Object Response;
                    if (AnItemIsGood)
                    {
                        //OPCAddItems.Enabled = false;
                        //for (int i = 1; i <= CihazAdedi; i++)
                        //{
                        //    //OPCItemName[i].Enabled = false;	 // ' Disable the Item Name cotnrols while now that they have been added to the group.
                        //}


                        RemoveOPCGroup.Enabled = false;					// ' If an item has been added don't allow the group to be removed until the item is removed
                        OPCRemoveItems.Enabled = true;
                    }
                    else
                        //' The OPC Server did not accept any of the items we attempted to enter, let the user know to try again.
                        MessageBox.Show("The OPC Server has not accepted any of the item you have entered, check your item names and try again.", "OPC Add Item", MessageBoxButtons.OK);

                }
                catch (Exception ex)
                {

                    MessageBox.Show("OPC server add items failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);
                }

            }
        }

        private void RemoveOPCGroup_Click(object sender, System.EventArgs e)
        {
            //  Test to see if the OPC Group object is currently available
            if (!(ConnectedGroup == null))
            {
                try
                {
                    //  Remove the group from the server
                    ConnectedOPCServer.OPCGroups.Remove(OPCGroupName.Text);
                }
                catch (Exception ex)
                {
                    //  Error handling
                    MessageBox.Show(("OPC server remove group failed with exception: " + ex.Message), "SimpleOPCInterface Exception", MessageBoxButtons.OK);
                }
                finally
                {
                    ConnectedGroup = null;
                    OPCGroupName.Enabled = true;
                    AddOPCGroup.Enabled = true;
                    RemoveOPCGroup.Enabled = false;
                    OPCAddItems.Enabled = false;
                    OPCRemoveItems.Enabled = false;
                    //for (short i = 1; (i <= CihazAdedi-1); i++)
                    //{
                    //    OPCItemName[i].Enabled = false;
                    //}
                    //  Enable the Disconnect Server button since we have removed the group and can disconnect from the server properly
                    DisconnectFromServer.Enabled = true;
                }
            }
        }

        public static bool IsArray(Type tip)
        {
            if (tip.BaseType.Name == "Array")
                return true;
            else
                return false;
        }


        private void GroupUpdateRate_TextChanged(object sender, System.EventArgs e)
        {
            //  If the group has been added and exist then change its update rate
            if (!(ConnectedGroup == null))
            {
                try
                {
                    ConnectedGroup.UpdateRate = int.Parse(GroupUpdateRate.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(("OPC server group update rate change failed with exception: " + ex.Message), "SimpleOPCInterface Exception", MessageBoxButtons.OK);
                }
            }
        }

        private void GroupDeadBand_TextChanged(object sender, System.EventArgs e)
        {
            //  If the group has been added and exist then change its dead band
            if (!(ConnectedGroup == null))
            {
                try
                {
                    ConnectedGroup.DeadBand = float.Parse(GroupDeadBand.Text);
                }
                catch (Exception ex)
                {
                    //  Error handling
                    MessageBox.Show(("OPC server group deadband change failed with exception: " + ex.Message), "SimpleOPCInterface Exception", MessageBoxButtons.OK);
                }
            }
        }

        private void GroupActiveState_CheckedChanged(object sender, System.EventArgs e)
        {
            //  If the group has been added and exist then change its active state
            if (!(ConnectedGroup == null))
            {
                try
                {
                    ConnectedGroup.IsActive = GroupActiveState.Checked;
                }
                catch (Exception ex)
                {
                    //  Error handling
                    MessageBox.Show(("OPC server group active state change failed with exception: " + ex.Message), "SimpleOPCInterface Exception", MessageBoxButtons.OK);
                }
            }
        }


        private void OPCItemActiveState_CheckedChanged(object sender, System.EventArgs e)
        {
            if (!(ConnectedGroup == null))
            {
                //  Get control index from name
                short index = -1;
                if ((((CheckBox)sender).Name == "_OPCItemActiveState_0"))
                {
                    index = 1;
                }
                else if ((((CheckBox)sender).Name == "_OPCItemActiveState_1"))
                {
                    index = 2;
                }
                else if ((((CheckBox)sender).Name == "_OPCItemActiveState_2"))
                {
                    index = 3;
                }
                else if ((((CheckBox)sender).Name == "_OPCItemActiveState_3"))
                {
                    index = 4;
                }
                else if ((((CheckBox)sender).Name == "_OPCItemActiveState_4"))
                {
                    index = 5;
                }
                else if ((((CheckBox)sender).Name == "_OPCItemActiveState_5"))
                {
                    index = 6;
                }
                else if ((((CheckBox)sender).Name == "_OPCItemActiveState_6"))
                {
                    index = 7;
                }
                else if ((((CheckBox)sender).Name == "_OPCItemActiveState_7"))
                {
                    index = 8;
                }
                else if ((((CheckBox)sender).Name == "_OPCItemActiveState_8"))
                {
                    index = 9;
                }
                else
                {
                    index = 10;
                }
                try
                {
                    //  Change only 1 item
                    short ItemCount = 1;
                    //  Dim local arrays to pass desired item for state change
                    int[] ActiveItemServerHandles = new int[2];
                    bool ActiveState;
                    System.Array ActiveItemErrors;
                    //  Get the desired state from the control.
                    ActiveState = OPCItemActiveState[index].Checked;
                    //  Get the Servers handle for the desired item.  The server handles
                    //  were returned in add item subroutine.
                    ActiveItemServerHandles.SetValue(ItemServerHandles.GetValue(index), 1);
                    System.Array activeitemserverhandles = ActiveItemServerHandles.ToArray<int>();

                    //  Invoke the SetActive operation on the OPC item collection interface
                    ConnectedGroup.OPCItems.SetActive(ItemCount, ref activeitemserverhandles, ActiveState, out ActiveItemErrors);
                }
                catch (Exception ex)
                {
                    //  Error handling
                    MessageBox.Show(("OPC server set active state failed with exception: " + ex.Message), "SimpleOPCInterface Exception", MessageBoxButtons.OK);
                }
            }
        }

        private void ConnectedGroup_DataChange(int TransactionID, int NumItems, ref System.Array ClientHandles, ref System.Array ItemValues, ref System.Array Qualities, ref System.Array TimeStamps)
        {
            //  We don't have error handling here since this is an event called from the OPC interface
            try
            {
                short i;
                for (i = 1; (i <= NumItems); i++)
                {
                    //  Use the 'Clienthandles' array returned by the server to pull out the
                    //  index number of the control to update and load the value.
                    if (IsArray(ItemValues.GetValue(i).GetType()))
                    {
                        Array ItsAnArray;
                        int x;
                        string Suffix;
                        ItsAnArray = (System.Array)ItemValues.GetValue(i);
                        //  Store the size of array for use by sync write
                        OPCItemIsArray[(int)ClientHandles.GetValue(i)] = (ItsAnArray.GetUpperBound(0) + 1);
                        OPCItemValue[(int)ClientHandles.GetValue(i)].Text = "";
                        for (x = ItsAnArray.GetLowerBound(0); (x <= ItsAnArray.GetUpperBound(0)); x++)
                        {
                            if ((x == ItsAnArray.GetUpperBound(0)))
                            {
                                Suffix = "";
                            }
                            else
                            {
                                Suffix = ", ";
                            }
                            OPCItemValue[(int)ClientHandles.GetValue(i)].Text = (OPCItemValue[(int)ClientHandles.GetValue(i)].Text + (ItsAnArray.GetValue(x) + Suffix));
                        }
                    }
                    else
                    {
                        OPCItemValue[(int)ClientHandles.GetValue(i)].Text = ItemValues.GetValue(i).ToString();
                    }
                    //  Check the Qualties for each item retured here.  The actual contents of the
                    //  quality field can contain bit field data which can provide specific
                    //  error conditions.  Normally if everything is OK then the quality will
                    //  contain the 0xC0
                    //if (((Qualities.GetValue(i) && OPCAutomation.OPCQuality.OPCQualityGood)
                    //            == OPCAutomation.OPCQuality.OPCQualityGood))
                    //{
                    //    OPCItemQuality[(int)ClientHandles.GetValue(i)].Text = "Good";
                    //}
                    //else if (((Qualities.GetValue(i) && OPCAutomation.OPCQuality.OPCQualityUncertain)
                    //            == OPCAutomation.OPCQuality.OPCQualityUncertain))
                    //{
                    //    OPCItemQuality[(int)ClientHandles.GetValue(i)].Text = "Uncertain";
                    //}
                    //else
                    //{
                    //    OPCItemQuality[(int)ClientHandles.GetValue(i)].Text = "Bad";
                    //}
                    //if (((OPCAutomation.OPCQuality)Qualities.GetValue(i)) == OPCAutomation.OPCQuality.OPCQualityGood)
                    //{
                    //    OPCItemQuality[(int)ClientHandles.GetValue(i)].Text = "Good";
                    //}
                    //else if ((((OPCAutomation.OPCQuality)Qualities.GetValue(i)) == OPCAutomation.OPCQuality.OPCQualityUncertain))
                    //{
                    //    OPCItemQuality[(int)ClientHandles.GetValue(i)].Text = "Uncertain";
                    //}
                    //else
                    //{
                    //    OPCItemQuality[(int)ClientHandles.GetValue(i)].Text = "Bad";
                    //}
                }
            }
            catch (Exception ex)
            {
                //  Error handling
                MessageBox.Show(("OPC DataChange failed with exception: " + ex.Message), "SimpleOPCInterface Exception", MessageBoxButtons.OK);
            }
        }


        private void ExitExample_Click(object sender, System.EventArgs e)
        {
            //  These calls will remove the OPC items, Group, and Disconnect in the proper order
            OPCRemoveItems_Click(OPCRemoveItems, new System.EventArgs());
            RemoveOPCGroup_Click(RemoveOPCGroup, new System.EventArgs());
            DisconnectFromServer_Click(DisconnectFromServer, new System.EventArgs());
        }

        private void _OPCItemValue_0_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Name == "_OPCItemValue_0")
            {
                   
            }
        }

    

        private void userControl11_ValueChange(object sender, EventArgs e)
        {
            //DataLog dtlog = new DataLog();
            //dtlog.User = Program.User;
            //dtlog.YeniDeger = userControl11.YeniDeger;
            //dtlog.EskiDeger = userControl11.EskiDeger;
            //dtlog.Tarih = System.DateTime.Now;

            //dtlog.Insert();
        }
    }

   




}

