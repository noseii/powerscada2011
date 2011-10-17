using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace PowerScada
{
    public class OpcManager
    {
        OPCAutomation.OPCServer AnOPCServer;
        OPCAutomation.OPCServer ConnectedOPCServer;
        OPCAutomation.OPCGroup ConnectedGroup;
        System.Array ItemServerHandles;
        List<String> OPCItemIDs = null;
        List<int> ClientHandles = null;
        List<OpcItems> OPCItem= null;
        List<int> OPCItemIsArray = null;

        public string GroupName { get; set; }

        public string OPCServerName { get; set; }

        public string OPCNodeName { get; set; } 


        public OpcManager(List<string> adresler)
        {

            OPCItemIDs = new List<string>(adresler.Count+1);
            OPCItem=new List<OpcItems>();
            OPCItem.Add(null);
            for (int i = 0; i < adresler.Count; i++)
            {
                OpcItems item = new OpcItems(i + 1, adresler[i], "", true);
                item.OPCItemValueChange += new OPCItemValueChangeEventHandler(item_OPCItemValueChange);
                OPCItem.Add(item);
                
            }
        
            
            ClientHandles = new List<int>(adresler.Count + 1);
            OPCItemIsArray = new List<int>(adresler.Count + 1);
        }

        /// <summary>
        /// Adresin degeri değiştiğinde çalışır..
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void item_OPCItemValueChange(OpcItems sender, OPCItemEventArg e)
        {
            MessageBox.Show(sender.OPCItemName + " " + e.GuncelDeger);
        }

        public List<string> GetOpcServerList()
        {
            List<string> serverlist = new List<string>();
            try
            {
                
                // Create a temporary OPCServer object and use it to get the list of
                // available OPC Servers
                AnOPCServer = new OPCAutomation.OPCServer();

                // Obtain the list of available OPC servers
                object AllOPCServers = null;
                AllOPCServers = AnOPCServer.GetOPCServers();

                // Load the list returned into the List box for user selection
                int i = 0;

                for (i = Microsoft.VisualBasic.Information.LBound((System.Array)AllOPCServers, 1); i <= Information.UBound((System.Array)AllOPCServers, 1); i++)
                {
                    serverlist.Add(((System.Array)AllOPCServers).GetValue(i).ToString());
                }

                // Release the temporary OPCServer object now that we're done with it
                AnOPCServer = null;


            }
            catch (Exception ex)
            {
               
                MessageBox.Show("List OPC servers failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);
            }
           
           return serverlist;
            
        }

        public void OPCServerConnect(string servername,string nodename)
        {
            //If InStr(OPCServerName, "Click") = 0 Then
            try
            {

                ConnectedOPCServer = new OPCAutomation.OPCServer();
                ConnectedOPCServer.Connect(servername, nodename);
                this.OPCServerName=servername;
                this.OPCNodeName = nodename; 
            }
            catch (Exception ex)
            {
                ConnectedOPCServer = null;
                MessageBox.Show("OPC server connect failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);
            };
        }

        public void DisConnectServer()
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
                }
            }
        }

        public void AddOpcGroupServer(string opcgroupname,int groupupdaterate,bool groupactive, float groupdeadband)
        {
            try
            {
                this.GroupName = opcgroupname;
                
                ConnectedOPCServer.OPCGroups.DefaultGroupIsActive = groupactive;
                ConnectedOPCServer.OPCGroups.DefaultGroupDeadband = groupdeadband;
                ConnectedGroup = ConnectedOPCServer.OPCGroups.Add(opcgroupname);
                ConnectedGroup.UpdateRate = groupupdaterate;
                ConnectedGroup.IsSubscribed = true; ;
                ConnectedGroup.DataChange += new OPCAutomation.DIOPCGroupEvent_DataChangeEventHandler(ConnectedGroup_DataChange);
            }
            catch (Exception ex)
            {
                MessageBox.Show("OPC server add group failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);


            }
        }

        public void RemoveGroupServer(string opcgroupname)
        {
            //  Test to see if the OPC Group object is currently available
            if (!(ConnectedGroup == null))
            {
                try
                {
                    //  Remove the group from the server
                    ConnectedOPCServer.OPCGroups.Remove(opcgroupname);
                }
                catch (Exception ex)
                {
                    //  Error handling
                    MessageBox.Show(("OPC server remove group failed with exception: " + ex.Message), "SimpleOPCInterface Exception", MessageBoxButtons.OK);
                }
                finally
                {
                    ConnectedGroup = null;
                }
            }

        }

        public void ConnectedGroup_DataChange(int TransactionID, int NumItems, ref System.Array ClientHandles, ref System.Array ItemValues, ref System.Array Qualities, ref System.Array TimeStamps)
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
                        OPCItem[(int)ClientHandles.GetValue(i)].OPCItemValue = "";
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
                            OPCItem[(int)ClientHandles.GetValue(i)].OPCItemValue = (OPCItem[(int)ClientHandles.GetValue(i)].OPCItemValue + (ItsAnArray.GetValue(x) + Suffix));
                        }
                    }
                    else
                    {
                        OPCItem[(int)ClientHandles.GetValue(i)].OPCItemValue = ItemValues.GetValue(i).ToString();
                    }

                    
                    if (((OPCAutomation.OPCQuality)Qualities.GetValue(i)) == OPCAutomation.OPCQuality.OPCQualityGood)
                    {
                        OPCItem[(int)ClientHandles.GetValue(i)].OPCItemQuality = "Good";
                    }
                    else if ((((OPCAutomation.OPCQuality)Qualities.GetValue(i)) == OPCAutomation.OPCQuality.OPCQualityUncertain))
                    {
                        OPCItem[(int)ClientHandles.GetValue(i)].OPCItemQuality = "Uncertain";
                    }
                    else
                    {
                        OPCItem[(int)ClientHandles.GetValue(i)].OPCItemQuality = "Bad";
                    }
                   
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(("OPC DataChange failed with exception: " + ex.Message), "SimpleOPCInterface Exception", MessageBoxButtons.OK);
            }
        }

        public void OPCItemWrite(string adres,string yazilacakdeger)
        {
            if (ConnectedGroup != null)
            {
                List<OpcItems> items=OPCItem.FindAll(p =>  p!=null && p.OPCItemName == adres);
                items[0].OPCItemValue = yazilacakdeger;
                try
                {
                    short ItemCount = 1;
                    int[] SyncItemServerHandles = new int[2];
                    object[] SyncItemValues = new object[2];
                    System.Array SyncItemServerErrors;
                    OPCAutomation.OPCItem AnOpcItem;
                    SyncItemServerHandles[1] = Convert.ToInt32(ItemServerHandles.GetValue(items[0].AdresNo));
                    AnOpcItem = ((OPCAutomation.OPCItem)ConnectedGroup.OPCItems.GetOPCItem((int)ItemServerHandles.GetValue(items[0].AdresNo)));


                    Array ItsAnArray;
                    short CanonDT;
                    CanonDT = AnOpcItem.CanonicalDataType;
           
                    switch (CanonDT)
                    {

                        //items[0].OPCItemValue;

                        case (short)CanonicalDataTypes.CanonDtByte:
                            if (OPCItemIsArray[items[0].AdresNo] > 0)
                            {
                                string str = items[0].OPCItemValue;
                                ItsAnArray = Array.CreateInstance(Type.GetType("Byte"), OPCItemIsArray[items[0].AdresNo]);
                                if (!LoadArray(ref ItsAnArray, CanonDT, ref str))
                                    return;
                                else
                                    SyncItemValues[1] = (ItsAnArray); //SyncItemValues[1] = CObj(ItsAnArray);
                            }
                            else
                                SyncItemValues[1] = Convert.ToByte(items[0].OPCItemValue);
                            break;
                        case (short)CanonicalDataTypes.CanonDtChar:
                            if (OPCItemIsArray[items[0].AdresNo] > 0)
                            {
                                string str = items[0].OPCItemValue;
                                ItsAnArray = Array.CreateInstance(Type.GetType("SByte"), OPCItemIsArray[items[0].AdresNo]);
                                if (!LoadArray(ref ItsAnArray, CanonDT, ref str))
                                    return;
                                else
                                    SyncItemValues[1] = (ItsAnArray); //SyncItemValues[1] = CObj(ItsAnArray);
                            }
                            else
                                SyncItemValues[1] = Convert.ToByte(items[0].OPCItemValue);
                            break;
                        case (short)CanonicalDataTypes.CanonDtWord:
                            if (OPCItemIsArray[items[0].AdresNo] > 0)
                            {
                                string str = items[0].OPCItemValue;
                                ItsAnArray = Array.CreateInstance(Type.GetType("UInt16"), OPCItemIsArray[items[0].AdresNo]);
                                if (!LoadArray(ref ItsAnArray, CanonDT, ref str))
                                    return;
                                else
                                    SyncItemValues[1] = (ItsAnArray); //SyncItemValues[1] = CObj(ItsAnArray);
                            }
                            else
                                SyncItemValues[1] = Convert.ToByte(items[0].OPCItemValue);
                            break;
                        case (short)CanonicalDataTypes.CanonDtShort:
                            if (OPCItemIsArray[items[0].AdresNo] > 0)
                            {
                                string str = items[0].OPCItemValue;
                                ItsAnArray = Array.CreateInstance(Type.GetType("Int16"), OPCItemIsArray[items[0].AdresNo]);
                                if (!LoadArray(ref ItsAnArray, CanonDT, ref str))
                                    return;
                                else
                                    SyncItemValues[1] = (ItsAnArray); //SyncItemValues[1] = CObj(ItsAnArray);
                            }
                            else
                                SyncItemValues[1] = Convert.ToInt16(items[0].OPCItemValue);
                            break;


                        case (short)CanonicalDataTypes.CanonDtDWord:
                            if (OPCItemIsArray[items[0].AdresNo] > 0)
                            {
                                string str = items[0].OPCItemValue;
                                ItsAnArray = Array.CreateInstance(Type.GetType("UInt32"), OPCItemIsArray[items[0].AdresNo]);
                                if (!LoadArray(ref ItsAnArray, CanonDT, ref str))
                                    return;
                                else
                                    SyncItemValues[1] = (ItsAnArray); //SyncItemValues[1] = CObj(ItsAnArray);
                            }
                            else
                                SyncItemValues[1] = Convert.ToByte(items[0].OPCItemValue);
                            break;
                        case (short)CanonicalDataTypes.CanonDtLong:
                            if (OPCItemIsArray[items[0].AdresNo] > 0)
                            {
                                string str = items[0].OPCItemValue;
                                ItsAnArray = Array.CreateInstance(Type.GetType("Int32"), OPCItemIsArray[items[0].AdresNo]);
                                if (!LoadArray(ref ItsAnArray, CanonDT, ref str))
                                    return;
                                else
                                    SyncItemValues[1] = (ItsAnArray); //SyncItemValues[1] = CObj(ItsAnArray);
                            }
                            else
                                SyncItemValues[1] = Convert.ToByte(items[0].OPCItemValue);
                            break;
                        case (short)CanonicalDataTypes.CanonDtFloat:
                            if (OPCItemIsArray[items[0].AdresNo] > 0)
                            {
                                string str = items[0].OPCItemValue;
                                ItsAnArray = Array.CreateInstance(Type.GetType("Single"), OPCItemIsArray[items[0].AdresNo]);
                                if (!LoadArray(ref ItsAnArray, CanonDT, ref str))
                                    return;
                                else
                                    SyncItemValues[1] = (ItsAnArray); //SyncItemValues[1] = CObj(ItsAnArray);
                            }
                            else
                                SyncItemValues[1] = Convert.ToByte(items[0].OPCItemValue);
                            break;
                        case (short)CanonicalDataTypes.CanonDtDouble:
                            if (OPCItemIsArray[items[0].AdresNo] > 0)
                            {
                                string str = items[0].OPCItemValue;
                                ItsAnArray = Array.CreateInstance(Type.GetType("Double"), OPCItemIsArray[items[0].AdresNo]);
                                if (!LoadArray(ref ItsAnArray, CanonDT, ref str))
                                    return;
                                else
                                    SyncItemValues[1] = (ItsAnArray); //SyncItemValues[1] = CObj(ItsAnArray);
                            }
                            else
                                SyncItemValues[1] = Convert.ToByte(items[0].OPCItemValue);
                            break;
                        case (short)CanonicalDataTypes.CanonDtBool:
                            if (OPCItemIsArray[items[0].AdresNo] > 0)
                            {
                                string str = items[0].OPCItemValue;
                                ItsAnArray = Array.CreateInstance(Type.GetType("Boolean"), OPCItemIsArray[items[0].AdresNo]);
                                if (!LoadArray(ref ItsAnArray, CanonDT, ref str))
                                    return;
                                else
                                    SyncItemValues[1] = (ItsAnArray); //SyncItemValues[1] = CObj(ItsAnArray);
                            }
                            else
                                SyncItemValues[1] = Convert.ToByte(items[0].OPCItemValue);
                            break;
                        case (short)CanonicalDataTypes.CanonDtString:
                            if (OPCItemIsArray[items[0].AdresNo] > 0)
                            {
                                string str = items[0].OPCItemValue;
                                ItsAnArray = Array.CreateInstance(Type.GetType("String"), OPCItemIsArray[items[0].AdresNo]);
                                if (!LoadArray(ref ItsAnArray, CanonDT, ref str))
                                    return;
                                else
                                    SyncItemValues[1] = (ItsAnArray); //SyncItemValues[1] = CObj(ItsAnArray);
                            }
                            else
                                SyncItemValues[1] = Convert.ToByte(items[0].OPCItemValue);
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

        public void  OpcServerKapat()
        {
            OPCRemoveItems();
            RemoveGroupServer(GroupName);
            DisConnectServer();
        }

        public void  OPCRemoveItems()
        {
            if ((ConnectedGroup != null))
            {
                if ((ConnectedGroup.OPCItems.Count != 0))
                {
                    try
                    {
                        //  Provide an array to contain the ItemServerHandles of the item
                        //  we intend to remove
                        int[] RemoveItemServerHandles = new int[OPCItem.Count];

                        //  Array for potential error returns.  This example doesn't
                        //  check them but yours should ultimately.
                        System.Array RemoveItemServerErrors;
                        //  Get the Servers handle for the desired items.  The server handles
                        //  were returned in add item subroutine.  In this case we need to get
                        //  only the handles for item that are valid.
                        int ItemCount = 0;
                        for (int i = 1; (i <= OPCItem.Count); i++)
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
                        ItemCount = ItemCount - 1;
                        ConnectedGroup.OPCItems.Remove(ItemCount, ref removeitemserverhandles, out RemoveItemServerErrors);
                        for (short i = 1; (i <= ItemCount); i++)
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
                       
                        for (short i = 1; (i <= OPCItem.Count); i++)
                        {
                            ItemServerHandles.SetValue(0, i);
                           
                        }
                       
                        for (short i = 1; (i <= OPCItem.Count); i++)
                        {
                            //OPCItemName[i].Enabled = true;
                            OPCItemIsArray[i] = 0;
                        }
                    }
                }
            }
        }

        public string  GetOPCItemSyncRead(string adres)
        {
            if ((ConnectedGroup != null))
            {

                List<OpcItems> items = OPCItem.FindAll(p => p!=null && p!=null && p.OPCItemName == adres);
               
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
                    SyncItemServerHandles[1] = (int)ItemServerHandles.GetValue(items[0].AdresNo);

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
                            //((TextBox)OPCItemValue.GetValue(index)).Text = "";
                            items[0].OPCItemValue = "";
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
                                items[0].OPCItemValue = (items[0].OPCItemValue + (ItsAnArray.GetValue(x) + Suffix));
                                return items[0].OPCItemValue; 
                            }
                        }
                        else
                        {
                            items[0].OPCItemValue = SyncItemValues.GetValue(1).ToString();
                            return items[0].OPCItemValue;
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
            return string.Empty;
        }

        public void SetOPCItemActiveStateChecked(string adres, bool check)
        {
            if (!(ConnectedGroup == null))
            {

                List<OpcItems> items = OPCItem.FindAll(p => p!=null && p.OPCItemName == adres);
               
                try
                {
                    //  Change only 1 item
                    short ItemCount = 1;
                    //  Dim local arrays to pass desired item for state change
                    int[] ActiveItemServerHandles = new int[2];
                    bool ActiveState;
                    System.Array ActiveItemErrors;
                    //  Get the desired state from the control.
                    ActiveState = check;
                    //  Get the Servers handle for the desired item.  The server handles
                    //  were returned in add item subroutine.
                    ActiveItemServerHandles.SetValue(ItemServerHandles.GetValue(items[0].AdresNo), 1);
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

        public void  SetGroupActiveStateChecked(bool check)
        {
            //  If the group has been added and exist then change its active state
            if (!(ConnectedGroup == null))
            {
                try
                {
                    ConnectedGroup.IsActive = check;
                }
                catch (Exception ex)
                {
                    //  Error handling
                    MessageBox.Show(("OPC server group active state change failed with exception: " + ex.Message), "SimpleOPCInterface Exception", MessageBoxButtons.OK);
                }
            }
        }

        public void SetGroupDeadBand(float groupdeadband)
        {
            //  If the group has been added and exist then change its dead band
            if (!(ConnectedGroup == null))
            {
                try
                {
                    ConnectedGroup.DeadBand =groupdeadband;
                }
                catch (Exception ex)
                {
                    //  Error handling
                    MessageBox.Show(("OPC server group deadband change failed with exception: " + ex.Message), "SimpleOPCInterface Exception", MessageBoxButtons.OK);
                }
            }
        }

        public void SetGroupUpdateRate(int groupupdaterate)
        {
            //  If the group has been added and exist then change its update rate
            if (!(ConnectedGroup == null))
            {
                try
                {
                    ConnectedGroup.UpdateRate = groupupdaterate;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(("OPC server group update rate change failed with exception: " + ex.Message), "SimpleOPCInterface Exception", MessageBoxButtons.OK);
                }
            }
        }


        /// <summary>
        /// Adresleri Ekler...
        /// </summary>
        public void OPCAddItems()
        {
            if ((ConnectedGroup != null))
            {
                try
                {
                    int ItemCount = OPCItem.Count-1;

                    //' Array for potential error returns.  This example doesn't
                    //' check them but yours should ultimately.
                    System.Array AddItemServerErrors;
                    OPCItemIDs.Add(null);
                    OPCItemIsArray.Add(0);
                    ClientHandles.Add(0);
                    //' Load the request OPC Item names and build the ClientHandles list
                    for (int i = 1; i < OPCItem.Count; i++)
                    {
                        //' Load the name of then item to be added to this group.  You can add
                        //' as many items as you want to the group in a single call by building these
                        //' arrays as needed.
                        List<OpcItems> item = OPCItem.FindAll(p => p!=null && p.AdresNo==i);
                           
                        OPCItemIDs.Add(item[0].OPCItemName);

                        //' ASSume all aren't an array. If it is, this holds size and is set in
                        //' Data change event.
                        OPCItemIsArray.Add(0);

                        //' The client handles are given to the OPC Server for each item you intend
                        //' to add to the group.  The OPC Server will uses these client handles
                        //' by returning them to you in the 'DataChange' event.  You can use the
                        //' client handles as a key to linking each valued returned from the Server
                        //' back to some element in your application.  In this example we are simply
                        //' placing the Index number of each control that will be used to display
                        //' data for the item.  In your application the ClientHandle value you use
                        //' can by whatever you need to best fit your program.  You will see how
                        //' these client handles are used in the 'DataChange' event handler.
                        ClientHandles.Add(i);

                        //' Make the Items active start control Active, for the demo I want all items to start active
                        //' Your application may need to start the items as inactive.
                        item[0].OPCItemActiveState = true;
                    }


                    ConnectedGroup.OPCItems.DefaultIsActive = true;
                    System.Array clienthandles = ClientHandles.ToArray<int>();
                    System.Array oPCitemIDs = OPCItemIDs.ToArray<string>();
                    ConnectedGroup.OPCItems.AddItems(ItemCount, ref oPCitemIDs, ref clienthandles, out ItemServerHandles, out AddItemServerErrors);
                    bool AnItemIsGood;
                    AnItemIsGood = false;

                    for (int i = 1; i < OPCItem.Count; i++)
                    {
                        List<OpcItems> item = OPCItem.FindAll(p => p != null && p.AdresNo == i);
                        if ((int)AddItemServerErrors.GetValue(i) == 0) 					  //'If the item was added successfully then allow it to be used.
                        {
                            AnItemIsGood = true;
                        }
                        else
                        {
                            ItemServerHandles.SetValue(0, i);						// ' If the handle was bad mark it as empty

                            item[0].OPCItemValue = "OPC Add Item Fail";
                        }
                    }
                    for (int i = 1; i < OPCItem.Count; i++)
                    {
                        List<OpcItems> item = OPCItem.FindAll(p => p!=null && p.AdresNo == i);
                        if ((int)AddItemServerErrors.GetValue(i) == 0)					  //'If the item was added successfully then allow it to be used.
                        {
                            AnItemIsGood = true;
                        }
                        else
                        {
                            ItemServerHandles.SetValue(0, i);						// ' If the handle was bad mark it as empty
                            item[0].OPCItemValue = "OPC Add Item Fail";
                        }
                    }
                    //' Disable the Add OPC item button if any item in the list was good
                    Object Response;
                    if (!AnItemIsGood)
                        MessageBox.Show("The OPC Server has not accepted any of the item you have entered, check your item names and try again.", "OPC Add Item", MessageBoxButtons.OK);

                }
                catch (Exception ex)
                {

                    MessageBox.Show("OPC server add items failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);
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
    }

   

    public enum CanonicalDataTypes
    {
        CanonDtByte = 17,
        CanonDtChar = 16,
        CanonDtWord = 18,
        CanonDtShort = 2,
        CanonDtDWord = 19,
        CanonDtLong = 3,
        CanonDtFloat = 4,
        CanonDtDouble = 5,
        CanonDtBool = 11,
        CanonDtString = 8
    }

    
}
