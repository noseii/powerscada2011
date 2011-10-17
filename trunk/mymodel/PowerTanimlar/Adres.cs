using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata;
using SharpBullet.OAL;


namespace mymodel
{
    public class Adres : Entity
    {

        [FieldDefinition(Length = 100)]
        public string tagadresi;

        public string TagAdresi
        {
            get
            {
                return tagadresi;
            }
            set
            {
                tagadresi = value;
            }

        }

        public override string ToString() { return this.TagAdresi ?? ""; }

       

        //public override Type GetChildType()
        //{
        //    return typeof(Cihaz);
        //}

        //public override string GetFKName()
        //{
        //    return "Adres_Id";
        //}


        #region static Methots
        //public static Cihaz[] ReadCihazlar(long adres)
        //{
        //    Condition[] conditionss = new Condition[1];
        //    conditionss[0].Field = "Adres_Id";
        //    conditionss[0].Value = adres;
        //    conditionss[0].Operator = System.Operator.Equal;
        //    return Persistence.ReadList<Cihaz>(new string[] { "*" }, conditionss, null, 100);
        //}


        #endregion Methots
    }
}
