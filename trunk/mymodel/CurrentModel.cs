using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mymodel
{
    public class CurrentModel
    {
        public static Kullanici User { get; set; }

        public static string GetMakAdres()
        {
            //string makadres = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            //makadres = makadres.Replace(@"\", "_");
            //makadres = makadres.Insert(0, "_");
            //makadres = makadres.Insert(0, System.Environment.MachineName);

            //return makadres;
            return string.Empty;
        }
    }
}
