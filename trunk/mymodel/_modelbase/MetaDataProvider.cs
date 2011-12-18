using System;
using mycommon;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

using SharpBullet.OAL.Metadata; namespace mymodel
{
    /*
         * Sahalara özellik atamaları için aşağıdakiler kullanılabilir ama istenirse daha bir çok özellik takip edilebilir
         * 
         * Sınırlama özelliği................................................
         *  Range(0,30,ErrorMessage="Girilen değer 0 ile 30 arasında olmalıdır.")
         * Ekran etiketi.....................................................
         *  DisplayName("Değiştirme Tarihi")
         * Ekranda kullanılacak komponent....................................
         *  UIHint("TextBox")
         * Veri girilebilir mi...............................................
         *  Editable(false)
         * Ekranda görünsün mü?..............................................
         *  ScaffoldColumn(false)
         * İçerdiği değerle ekranda görünsün mü?.............................
         *  Browsable(true)
         * Tablolar arası ilişkili saha ise, ilişkinin tanımı için
         *  Association("FK_KullaniciEkleyenKullanici","Ekleyen_Kullanici","Kullanici")
         * SQL datatype için kullaılabilir...................................
         *  DataType("varchar(20)")
         * Varsayılan değer atamak için türüne göre bilgi girilir............
         *  DefaultValue()
         * Açıklama girmek için..............................................
         *  Description("Kaydı hangi kullanıcının girdiği bilgisi")
         * Herhangi bir enum ile otomatik map edilecek ise...................
         *  EnumDataType(typeof(myenum.MuayeneTuru))
         * Lookup list için kullanılacak ise................................
         *  LookupBindingProperties()
         * Şifre bilgisi için kullanılıyor ise..............................  
         *  PasswordPropertyText(true)
         * Değiştirilemez ise...............................................
         *  ReadOnly(true)
         * Zorunlu saha yönetimi için.......................................
         *  Required(AllowEmptyStrings=true,ErrorMessage="",ErrorMessageResourceName="",ErrorMessageResourceType=typeof(WarningException))
         * Maxsimum uzunluk için............................................
         *  FieldDefinition(Length =36),

         */

    //  metasaha kullanım örneği
    //  MetaSaha  saha =  tablekullanici.GetMetaField(u=>u.Ekleyen_Kullanici);
    //  string str =saha.Etiket;
    public static class MetaDataProvider
    {
        public static MetaSaha GetMetaField<TSource, TProperty>(this TSource source, Expression<Func<TSource, TProperty>> propertyLambda)
        {

            PropertyInfo propInfo = (propertyLambda.Body as MemberExpression).Member as PropertyInfo;
            MetaSaha saha = new MetaSaha();
            
            try
            {
                StringLengthAttribute StringLength = propInfo.GetCustomAttributes(typeof(StringLengthAttribute), true)[0] as StringLengthAttribute;
                saha.Uzunluk = StringLength.MaximumLength;
            }
            catch { }
            try
            {
                DisplayNameAttribute displayName = propInfo.GetCustomAttributes(typeof(DisplayNameAttribute), true)[0] as DisplayNameAttribute;
                saha.Etiket = displayName.DisplayName;
            }
            catch { }
            try
            {
                ReadOnlyAttribute readOnly = propInfo.GetCustomAttributes(typeof(ReadOnlyAttribute), true)[0] as ReadOnlyAttribute;
                saha.IsReadOnly = readOnly.IsReadOnly;
            }
            catch { }

            return saha;
        }
    }

    public class MetaSaha
    {
        /// <summary>
        /// Bağlı Olduğu Entity Adı
        /// </summary>
        public string EntityAd { get; set; }
        /// <summary>
        /// Sahanın Adı
        /// </summary>
        public string Ad { get; set; }
        /// <summary>
        /// Ekranda, Listelerde görünen adı
        /// </summary>
        public string Etiket { get; set; }
        /// <summary>
        /// Database'de Indexli mi olsun
        /// </summary>
        public bool Indexli { get; set; }
        /// <summary>
        /// Veri Türü
        /// </summary>
        public TypeCode Tur { get; set; }
        /// <summary>
        /// Ekranda hangi kolonda çıksın
        /// </summary>
        public byte Kolon { get; set; }
        /// <summary>
        /// Kolonda hangi sırada çıksın
        /// </summary>
        public Int16 No { get; set; }
        /// <summary>
        /// Formda giriş kısmında görünsünmü
        /// </summary>
        public bool Form { get; set; }
        /// <summary>
        /// Listede görünsünmü
        /// </summary>
        public bool List { get; set; }
        /// <summary>
        /// Giriş Yapılabilsinmi
        /// </summary>
        public bool Gir { get; set; }
        /// <summary>
        /// Veri girişi zorunlumu
        /// </summary>
        public bool Zorunlu { get; set; }
        /// <summary>
        /// Enummu
        /// </summary>
        public bool Enum { get; set; }
        /// <summary>
        /// String bilgilerde text uzunluğu
        /// </summary>
        public int Uzunluk { get; set; }
        /// <summary>
        /// evet/hayır karşılığı Bay/Bayan gibi
        /// </summary>
        public string[] Secenek { get; set; }

        public bool IsReadOnly { get; set; }

    }
}
