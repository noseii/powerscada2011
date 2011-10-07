using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;

namespace PowerScada
{
    public partial class MyCombo : UserControl
    {
        public event EventHandler SelectedValueChanged;

        public MyCombo()
        {
            InitializeComponent();
        }
        public enum Binding
        {
            Entity = 1,
            Enum
        }

        private Binding bindingTürü = Binding.Entity;

        public Binding BindingTürü
        {
            get { return bindingTürü; }
            set { bindingTürü = value; }
        }

        //----------------------Entity İçin---------------------------------
        private string entityName = "";

        public string EntityName
        {
            get { return entityName; }
            set { entityName = value; }
        }

        private string whereClause;

        public string WhereClause
        {
            get { return whereClause; }
            set
            {
                whereClause = value;
                ComboDoldur();//Bazen MyCombo_ParentChanged Event inden önce çalıştığı için buraya da kondu
            }
        }

        private string displayField = "";

        public string DisplayField
        {
            get { return displayField; }
            set { displayField = value; }
        }

        //------------------------Enum İçin-----------------------------------
        private string enumTipi = "";

        public string EnumTipi
        {
            get { return enumTipi; }
            set { enumTipi = value; }
        }

        //-----------------------------------------------------------

        private bool emptyRow;

        public bool EmptyRow
        {
            get { return emptyRow; }
            set { emptyRow = value; }
        }

        private string emptyMessage = "";

        public string EmptyMessage
        {
            get { return emptyMessage; }
            set { emptyMessage = value; }
        }

        public int Id
        {
            get
            {
                return Convert.ToInt32(comboBox1.SelectedValue);
                //return Convert.ToInt32(((DataTable)comboBox1.DataSource).Rows[comboBox1.SelectedIndex]["Id"]);
            }
            set
            {
                comboBox1.SelectedValue = value;
            }

        }

        public int SelectedIndex
        {
            get
            {
                return comboBox1.SelectedIndex;
            }
            set
            {
                //if ((int)value > -1)
                //    comboBox1.SelectedIndex = ((DataTable)comboBox1.DataSource).Select("Id<=" + value).Length - 1;
            }
        }

        public string comboText(int i)
        {
            return ((DataRowView)comboBox1.Items[i]).Row[comboBox1.DisplayMember].ToString();
        }


        private void MyCombo_ParentChanged(object sender, EventArgs e)
        {
            if (this.DesignMode == false)
                ComboDoldur();
        }

        private void ComboDoldur()
        {
            try
            {
                if (BindingTürü == Binding.Entity)
                {
                    PowerScada.Utility.FillDataSource(EntityName, WhereClause, DisplayField, comboBox1, EmptyRow, EmptyMessage);
                }
                else
                {


                    Type tip = Type.GetType("mymodel.myenum+"+EnumTipi+", mymodel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
                    PowerScada.Utility.FillEnum(tip, comboBox1, EmptyRow, EmptyMessage);

                }


            }
            catch { }
        }

        public override string Text
        {
            get
            {
                return comboBox1.Text;
            }
            set
            {
                comboBox1.Text = value;
            }
        }

        private void OnSelectedValueChanged()
        {
            if (SelectedValueChanged != null)
                SelectedValueChanged(this, EventArgs.Empty);
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            OnSelectedValueChanged();
        }




    }
}
