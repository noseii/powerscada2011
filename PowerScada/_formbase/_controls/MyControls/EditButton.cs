using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SharpBullet.UI;
using PowerScada;
using mymodel;

namespace PowerScada
{
    public partial class EditButton : UserControl
    {
        public EventHandler AfterExecute;

        public EventHandler BeforeExecute;

        public event EventHandler TextLeave;

        public BaseEntity entity;

        public EditButton()
        {
            InitializeComponent();

        }

        public override string Text
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
            }
        }

        private long id;

        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        private string oldvalue;

        public string OldValue
        {
            get
            {
                if (oldvalue == null)
                    oldvalue = string.Empty;
                return oldvalue;
            }
            set
            {
                oldvalue = value;
            }
        }

        private string newvalue;

        public string NewValue
        {
            get
            {
                if (newvalue == null)
                    newvalue = string.Empty;
                return newvalue;
            }
            set
            {
                newvalue = value;
            }
        }

        public bool ReadOnly
        {
            get { return textBox1.ReadOnly; }
            set { textBox1.ReadOnly = value; }
        }

        private string commandName;

        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Execute(Text, null);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Length == 0)
                id = 0;
            OnTextLeave();
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(commandName)) return;

            NewValue = Text.Trim();

            if (OldValue == NewValue)
            {
                return;
            }
            if (string.Compare(OldValue, NewValue, true) == 0)
            {
                OldValue = NewValue;
                return;
            }
            if (NewValue == "")
            {
                id = 0;
                OldValue = "";
                OnAfterExecute();
                return;
            }
            else
            {
                OldValue = NewValue;

                Execute(NewValue, null);

            }

        }

        private void Execute(string parameter, EditButton editbtn)
        {
            OnBeforeExecute();

            if (commandName!=null &&!commandName.StartsWith("EditButton"))
            {
                frmBase form = (frmBase)this.FindForm();
                form.ExecuteCommand(commandName);
            }
            else
                if (Utility.EditButtonCommands != null && Utility.EditButtonCommands.Count>0)
                    Utility.EditButtonCommands[commandName].Execute(this);
            OnAfterExecute();
        }

        private void OnAfterExecute()
        {
            if (this.AfterExecute != null)
                this.AfterExecute(this, EventArgs.Empty);
        }

        private void OnBeforeExecute()
        {
            if (this.BeforeExecute != null)
                this.BeforeExecute(this, EventArgs.Empty);
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            Validate();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            OldValue = textBox1.Text.Trim();
        }

        private void OnTextLeave()
        {
            if (TextLeave != null)
                TextLeave(this, EventArgs.Empty);
        }

    }


}
