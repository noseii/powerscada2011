using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PowerScada
{

    public class OpcItems
    {

        public event OPCItemValueChangeEventHandler OPCItemValueChange;

        public string OpcGroupName { get; set; }
        
        public int AdresNo { get; set; }
        
        public string OPCItemName { get; set; }

        private string opcitemvalue;

        public string OPCItemValue
        {
            get
            {
                return opcitemvalue;
            }
            set
            {
                string eskideger = OPCItemValue;
                if (eskideger != value)
                {
                    opcitemvalue = value;
                    if (OPCItemValueChange != null)
                        this.OPCItemValueChange(this, new OPCItemEventArg(value));
                }
            }
        }

        public bool OPCItemActiveState { get; set; }

        public string OPCItemQuality { get; set; }

        public int GrupAdresNo { get; set; }

        public OpcItems(string opcgroupname, int adresno, string opcitemname, string opcitemvalue, bool opcitemactivestate, int grupadresno)
        {
            this.OpcGroupName = opcgroupname;
            this.AdresNo = adresno;
            this.OPCItemName = opcitemname;
            this.OPCItemValue = opcitemvalue;
            this.OPCItemActiveState = opcitemactivestate;
            this.GrupAdresNo = grupadresno;
        }

        public override string ToString()
        {
            return OPCItemName;
        }
    }

    public class OPCItemEventArg : EventArgs
    {
        public string GuncelDeger { get; set; }

        public OPCItemEventArg(string gunceldeger)
        {
            this.GuncelDeger = gunceldeger;
        }

    }

    public delegate void OPCItemValueChangeEventHandler(OpcItems sender, OPCItemEventArg e);

   
}
