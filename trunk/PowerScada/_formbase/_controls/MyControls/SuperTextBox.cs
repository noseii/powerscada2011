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
    public partial class SuperTextBox : UserControl
    {
        public SuperTextBox()
        {
            InitializeComponent();
        }
        private Color getfocuscolor;

        public Color GetFocusColor
        {
            get
            {
                return getfocuscolor;
            }
            set
            {
                getfocuscolor = value;
            }
        }

        private Color gotfocuscolor;

        public Color GotFocusColor
        {
            get
            {
                return gotfocuscolor;
            }
            set
            {
                gotfocuscolor = value;
            }
        }

        private Formati format;

        public Formati Format
        {
            get
            {
                return format;
            }

            set
            {
                format = value;
            }
        }

        protected void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Format == Formati.Sayisal)
            {
                if ((int)e.KeyChar >= 47 && (int)e.KeyChar <= 58)
                {
                    e.Handled = false;
                }
                else if ((int)e.KeyChar == 8)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            else
                if (Format == Formati.Text)
                {

                    if ((int)e.KeyChar < 47 || (int)e.KeyChar > 58)
                    {
                        e.Handled = false;
                    }
                    else if ((int)e.KeyChar == 8)
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
        }
    }

    public enum Formati
    {
        Text = 1,
        Sayisal = 2

    }
}
