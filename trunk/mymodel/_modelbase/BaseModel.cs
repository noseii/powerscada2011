using System;
using mycommon;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class basemodel<T> where T :new()
    {
        public Int64 Id { get; set; }
        public Boolean Aktif { get; set; }
        public DateTime EklemeTarihi { get; set; }
        public DateTime DegistirmeTarihi { get; set; }
        public Kullanici Ekleyen_Kullanici { get; set; }
        public Kullanici Degistiren_Kullanici { get; set; }
        public mymodel.myenum.Islemturu Islemturu { get; set; }
        public String EkleyenMakAdres { get; set; }
        public String DegistirenMakAdres { get; set; }

        public void insert()
        {
            myUtil.Insert(this);
        }
        public void update()
        {
            myUtil.Update(this);
        }

        public void delete()
        {
            myUtil.Delete(this);
        }

        public void DoScript()
        {
            myUtil.DoScript(this);
        }

        public static List<T> Read(string wherestr,bool withdesc) 
        {
            T myObj = new T();
            if (wherestr.Length == 0)
                wherestr =myObj.GetType().Name.ToString()+".aktif=1 ";
            List<T> list = myUtil.Select<T>(myObj, "\nwhere\n" + wherestr, withdesc); 
            return list;
        }

      



       
    }

    
}
