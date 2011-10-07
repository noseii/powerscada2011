using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata;
using SharpBullet.OAL; 

namespace mymodel
{
    public class TakvimSablonu : Entity
    {
        [FieldDefinition(Length = 100, UniqueIndexGroup = "Adi")]
       
        public string Adi { get; set; }

        [FieldDefinition(UniqueIndexGroup = "SablonTuru", IsRequired = true)]
        public myenum.IzlemTuru SablonTuru { get; set; }

        private BindingList<TakvimSablonSatiri> sablonsatiri;

        [FieldDefinition(MappingType = FieldMappingType.No)]
        public BindingList<TakvimSablonSatiri> SablonSatiri
        {
            get
            {
                if (sablonsatiri == null)
                    sablonsatiri = new BindingList<TakvimSablonSatiri>();

                return sablonsatiri;
            }
            set
            {
                sablonsatiri = value;
            }
        }

        public override Type GetChildType()
        {
            return typeof(TakvimSablonSatiri);
        }

        public override string GetFKName()
        {
            return "TakvimSablonu_Id";
        }

        public override string ToString() { return this.Adi ?? ""; }

        public override void Validate()
        {
            base.Validate();
        }


        #region static Methots
        public static TakvimSablonSatiri[] ReadSablonSatiri(long sablonid)
        {
            Condition[] conditionss = new Condition[1];
            conditionss[0].Field = "TakvimSablonu_Id";
            conditionss[0].Value = sablonid;
            conditionss[0].Operator = System.Operator.Equal;
            return Persistence.ReadList<TakvimSablonSatiri>(new string[] { "*" }, conditionss, null, 100);
        }


        #endregion Methots

    }
}
