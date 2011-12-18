using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PowerScada
{
    class ParaTextBox : TextBox
    {
        public ParaTextBox()
        {
            base.KeyPress += new KeyPressEventHandler(ParaTextBox_KeyPress);
            base.Leave += new EventHandler(ParaTextBox_Leave);
        }

        private int precision = 2;

        public int Precision
        {
            get { return precision; }
            set { precision = value; }
        }

        void ParaTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((this.Text.Contains(",") && e.KeyChar == ',') || (this.Text.Contains("-") && e.KeyChar == '-'))
                {
                    e.Handled = true;
                    return;
                }
                if (e.KeyChar != '-'
                    && e.KeyChar != ','
                    // && e.KeyChar != '.' 
                    && e.KeyChar != (char)Keys.Back
                    && e.KeyChar != (char)Keys.Left
                    && e.KeyChar != (char)Keys.Right
                    && e.KeyChar != (char)Keys.Delete)
                    Decimal.Parse(e.KeyChar.ToString());
            }
            catch
            {
                e.Handled = true;
            }
        }

        void ParaTextBox_Leave(object sender, EventArgs e)
        {
            if (Precision == 0)
            {
                base.Leave -= new EventHandler(ParaTextBox_Leave);
                return;
            }
            try
            {
                this.Text = Decimal.Parse(this.Text).ToString(".00");
            }
            catch
            {

            }
        }
    }
}
