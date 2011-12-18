using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;
using SharpBullet.ActiveRecord;
using mymodel;

namespace PowerScada
{
    public class BindingMap
    {
        private Hashtable controlsField = new Hashtable();

        public Hashtable ControlsField
        {
            get { return controlsField; }
            set { controlsField = value; }
        }

        private object dataObject;

        public object DataObject
        {
            get { return dataObject; }
            set { dataObject = value; }
        }

        public ActiveRecordBase Entity
        {
            get { return (ActiveRecordBase)dataObject; }
        }

        public void AddBinding(Control control, string field)
        {
            // if ControlsField.Contains(control)... remove control

            ControlsField[control] = field;
            if (control is TextBox)
            {
                TextBox tbox = (TextBox)control;
                tbox.TextChanged += new EventHandler(tbox_TextChanged);
            }
            else if (control is DateTimePicker)
            {
                DateTimePicker picker = (DateTimePicker)control;
                picker.ValueChanged += new EventHandler(dtime_ValueChanged);
            }
        }
        //Sadece Pointerlar için kullanılır. editbutton set etmek için kullanılır.
        public void AddBinding(Control control, string field, object value)
        {
            ControlsField[control] = field;
            SetObjectField(null, field, value);
        }

        void dtime_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker picker = (DateTimePicker)sender;
            object value = picker.Value;
            string fieldName = (string)ControlsField[sender];

            SetObjectField(sender, fieldName, value);
        }

        void tbox_TextChanged(object sender, EventArgs e)
        {
            TextBox tbox = (TextBox)sender;
            object value = tbox.Text;
            string fieldName = (string)ControlsField[sender];

            SetObjectField(sender, fieldName, value);
        }

        public void SetObjectField(object sender, string fieldName, object value)
        {
            if (sender != null && sender == updatingControl) return;

            PropertyInfo prop = dataObject.GetType().GetProperty(fieldName);
            value = Convert.ChangeType(value, prop.PropertyType);
            prop.SetValue(dataObject, value, null);

            //Fire event or trigger some method for the controls other than SENDER
            foreach (Control control in controlsField.Keys)
            {
                if (control == sender) continue;

                string afield = (string)controlsField[control];
                if (afield.StartsWith(fieldName))
                {
                    ShowDataControl(control, fieldName, getValueOfObject(dataObject, afield));
                }
            }
        }

        private Control updatingControl;

        public void ShowData()
        {
            foreach (Control control in ControlsField.Keys)
            {
                string fieldName = (string)ControlsField[control];
                object value = getValueOfObject(dataObject, fieldName);

                ShowDataControl(control, fieldName, value);
            }
        }

        private void ShowDataControl(Control control, string fieldName, object value)
        {
            value = value ?? "";

            updatingControl = control;
            if (!(control is DateTimePicker))
                control.Text = value.ToString();
            else
            {
                if (control is DateTimePicker)
                {
                    DateTime dt = (DateTime)value;
                    if (((DateTimePicker)control).MinDate > dt)
                        SetObjectField(null, fieldName, ((DateTimePicker)control).MinDate);
                    else
                        control.Text = value.ToString();
                }

            }
            updatingControl = null;
        }

        private object getValueOfObject(object dataObject, string fieldName)
        {
            if (!fieldName.Contains("."))
                return dataObject.GetType().GetProperty(fieldName).GetValue(dataObject, null);
            else
            {
                string[] subfields = fieldName.Split('.');
                object value = dataObject;
                MemberInfo mi = null;
                Type type = dataObject.GetType();

                for (int i = 0; i < subfields.Length; i++)
                {
                    mi = type.GetMember(subfields[i])[0];
                    if (mi.MemberType == MemberTypes.Property)
                    {
                        type = ((PropertyInfo)mi).PropertyType;
                        value = ((PropertyInfo)mi).GetValue(value, null);
                    }
                    else
                    {
                        type = ((FieldInfo)mi).FieldType;
                        value = ((FieldInfo)mi).GetValue(value);
                    }
                }

                return value;
            }
        }
    }
}
