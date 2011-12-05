using System;
using mycommon;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class Model : basemodel<Model>
    {



        public static T ReadBy<T>(string wherestr, bool withdesc) where T : mymodel.Model
        {

            Type type = typeof(T);


            mymodel.Model entity = (mymodel.Model)Activator.CreateInstance(type);

            if (wherestr.Length == 0)
                wherestr = entity.GetType().Name.ToString() + ".aktif=1 ";
            List<mymodel.Model> list = mymodel.Metadata.Select<mymodel.Model>(entity, "\nwhere\n" + wherestr, withdesc);

            entity = list[0];
            return entity as T;

        }
    }

}
