using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBullet.UI
{
    public class Command
    {
        public string Name { get; set; }
        public Func<object, bool> IsEnabledMethod { get; set; }
        public Action<object> ExecuteMethod { get; set; }

        public bool IsEnabled(object sender)
        {
                //IsEnabled null ise, yani kullanılmıyorsa, default TRUE kabul edilir.
                if (IsEnabledMethod == null) return true;
                return IsEnabledMethod(sender);
        }

        public void Execute(object sender)
        {
            if (IsEnabled(sender) && ExecuteMethod != null)
            {
                ExecuteMethod(sender);
            }
        }
    }
}