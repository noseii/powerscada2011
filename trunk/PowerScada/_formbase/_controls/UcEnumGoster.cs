using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PowerScada
{
   

    public partial class UcEnumGoster : UserControl
    {
        public event EventHandler ValueChanged=null;

        private string enumturu=string.Empty;

        public string EnumTuru
        {
            get
            {
                return enumturu;
            }
            set
            {
                enumturu = value;
            }
        }

        private void InitializeUserControl()
        {
            this.Load += new EventHandler(UcEnumGoster_Load);
            
        }


        protected virtual void OnValueChanged()
        {
            if (ValueChanged != null)
                ValueChanged(this, EventArgs.Empty);
            
        }
       

        void UcEnumGoster_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
                ComboDoldur();
        }

        private void ComboDoldur()
        {
            if (!this.Enabled)
                edtCombobox.Enabled = true;
            
            if (EnumTuru !=string.Empty )
            {
                ///Cache atılacak tekrar tekrar okunmaması için

                Utility.Combodoldur(edtCombobox,Utility.GetEnumType(EnumTuru), false, "");
               
            }
       }
  
        public UcEnumGoster()
        {
            InitializeComponent();
            InitializeUserControl();
        }

        private object deger;

        public object Deger
        {
          
            get
            {
                if (this.edtCombobox.EditValue == "Seçiniz" || this.edtCombobox.EditValue==null)
                    return 0;
                return Enum.Parse(Utility.GetEnumType(this.EnumTuru), this.edtCombobox.EditValue.ToString(), false);
            }
            set
            {
                if (EnumTuru != "")
                {
                    if (!DesignMode)
                        edtCombobox.EditValue = Enum.GetName(Utility.GetEnumType(this.EnumTuru), value);
                    else
                        edtCombobox.EditValue = value;
                }
            }
        
        }

        private void edtCombobox_SelectedValueChanged(object sender, EventArgs e)
        {
            OnValueChanged();
        }

       
             
    }
}
