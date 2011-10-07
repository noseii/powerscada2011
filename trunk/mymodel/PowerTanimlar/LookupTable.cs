using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata;


namespace mymodel
{
   

        public class LookupTable : BaseEntity
        {

            /// <summary>
            /// Enum sayısal değerini tutar.
            /// </summary>
            private mymodel.myenum.ParametreTipi tip;
            public mymodel.myenum.ParametreTipi Tip
            {
                get
                {
                    return tip;
                }
                set
                {
                    tip = value;
                }
            }

            /// <summary>
            /// İsterse açıklama yazabilsin
            /// </summary>
            [FieldDefinition(Length = 250)]
            private string aciklama;
            public string Aciklama
            {
                get
                {
                    return aciklama;
                }
                set
                {
                    aciklama = value;
                }
            }


        }
       
    
}
