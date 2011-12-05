using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace SharpBullet.UI
{
    public class CommandBinding
    {
        Command command;
        object control;

        private CommandBinding(Command command)
        {
            this.command = command;

            Application.Idle += new EventHandler(Application_Idle);
        }

        void Application_Idle(object sender, EventArgs e)
        {
            if (command == null
                || control == null
                || command.IsEnabledMethod == null) return; //son satır bir optimizasyon, bu method yoksa disabled olma şansı da yok zaten

            PropertyInfo info = control.GetType().GetProperty("Enabled");
            if (info != null)
            {
                bool enabled = command.IsEnabled(sender);
                info.SetValue(control, enabled, null);
            }

            PropertyInfo textProp = control.GetType().GetProperty("Text");
            if (textProp != null)
            {
                string text = (string)textProp.GetValue(control, null);
                if (text != command.Name)
                {
                    textProp.SetValue(control, command.Name, null);
                }
            }
        }

        public CommandBinding(Command command, Button control)
            : this(command)
        {
            this.control = control;
            control.Click += new EventHandler(control_Click);
        }

        public CommandBinding(Command command, ToolStripMenuItem control)
            : this(command)
        {
            this.control = control;
            control.Click += new EventHandler(control_Click);
        }

        public CommandBinding(Command command, ToolStripButton control)
            : this(command)
        {
            this.control = control;
            control.Click += new EventHandler(control_Click);
        }

        public CommandBinding(Command command, LinkLabel control)
            : this(command)
        {
            this.control = control;
            control.Click += new EventHandler(control_Click);
        }

        void control_Click(object sender, EventArgs e)
        {
            command.Execute(sender);
        }

        public void Clear()
        {
            if (control is Control)
                ((Control)control).Click -= new EventHandler(control_Click);
            else if (control is LinkLabel)
                ((LinkLabel)control).Click -= new EventHandler(control_Click);
            else if (control is ToolStripButton)
                ((ToolStripButton)control).Click -= new EventHandler(control_Click);
            else if (control is ToolStripMenuItem)
                ((ToolStripMenuItem)control).Click -= new EventHandler(control_Click);
            else
                throw new ApplicationException("Sistem Hatası: Bilinmeyen kontrol türü, CommandBinding");
        }
    }
}
