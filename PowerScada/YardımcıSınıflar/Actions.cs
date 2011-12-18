using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PowerScada
{
    public class ActionCihazListesi : ActionBase
    {
        public ActionCihazListesi()
        {
            this.multiselect = multiselect;
        }
        public ActionCihazListesi(bool multiselect)
        {
            this.multiselect = multiselect;
        }

        public override string PrimaryKeyName
        {
            get
            {
                return "Id";
            }
        }
        public override string EntityName
        {
            get
            {
                return "mymodel.Cihaz";
            }
        }
        public override ISelectionForm SelectionForm
        {
            get
            {
                return new frmCihazTanimListesi();
            }
        }
    }

    public class ActionAdresListesi : ActionBase
    {
        public ActionAdresListesi()
        {
            this.multiselect = multiselect;
        }
        public ActionAdresListesi(bool multiselect)
        {
            this.multiselect = multiselect;
        }

        public override string PrimaryKeyName
        {
            get
            {
                return "Id";
            }
        }
        public override string EntityName
        {
            get
            {
                return "mymodel.Adres";
            }
        }
        public override ISelectionForm SelectionForm
        {
            get
            {
                return new frmAdresListesi();
            }
        }
    }


    public class ActionLookupListesi : ActionBase
    {
        int Tipi = 0;

        public ActionLookupListesi()
        {
            this.multiselect = multiselect;
        }
        public ActionLookupListesi(bool multiselect)
        {
            this.multiselect = multiselect;
        }

        public ActionLookupListesi(int tipi)
        {
            this.multiselect = multiselect;
            this.Tipi = tipi;
        }

        public override string PrimaryKeyName
        {
            get
            {
                return "Id";
            }
        }
        public override string EntityName
        {
            get
            {
                return "mymodel.LookupTable";
            }
        }
        public override ISelectionForm SelectionForm
        {
            get
            {
                if (Tipi == 0)
                    return new frmLookupTableListesi();
                else
                    return new frmLookupTableListesi(Tipi);
            }
        }
    }
}
