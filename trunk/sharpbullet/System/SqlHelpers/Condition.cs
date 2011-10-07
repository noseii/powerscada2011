using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public struct Condition
    {
        private string field;
        private Operator op;
        private object value;

        public string Field
        {
            get { return field; }
            set { field = value; }
        }

        public Operator Operator
        {
            get { return op; }
            set { op = value; }
        }

        public object Value
        {
            get { return value; }
            set { this.value = value; }
        }


        public Condition(string field, Operator op, object value) 
        {
            this.field = field;
            this.op = op;
            this.value = value;
        }

        public static Condition Aktif()
        {
            return new Condition("Aktif", Operator.Equal, true);
        }

        public static Condition[] AktifArray()
        {
            return new Condition[] { new Condition("Aktif", Operator.Equal, true) };
        }

        public static Condition[] Tek(string field, Operator op, object value)
        {
            return new Condition[] { new Condition(field, op, value) };
        }
    }
}
