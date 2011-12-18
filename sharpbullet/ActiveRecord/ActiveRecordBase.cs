using System;
using System.Collections.Generic;
using System.Text;
using SharpBullet.OAL;
using SharpBullet.OAL.Metadata;
using System.Reflection;

namespace SharpBullet.ActiveRecord
{
    [Serializable]
    [FieldDefinition(TypeName = "Int64", IsFiltered = false)] //For 'Id' 
    [EntityDefinition(IdMethod = IdMethod.UserSubmitted, OptimisticLockField="RowVersion")]
    public class ActiveRecordBase
    {
        private bool isRead = false;

        private long id;
        public  long Id
        {
            get { return id; }
            set
            {
                //--if (value == System.DBNull.Value) value = null;
                id = value;
                PropertyChanged("Id");
            }
        }

        private byte rowVersion;
        public byte RowVersion
        {
            get { return rowVersion; }
            set { rowVersion = value; }
        }

        protected void PropertyChanged(string propertyName)
        {
            // Keep track of changed properties.
            // Fire an event for binding, with propertyName included in event parameter.
            if (OnPropertyChanged != null)
                OnPropertyChanged(this, new PropertyChangedArgs(propertyName));
        }
        public bool IsAutoImport { get; set; }

        public event PropertyChangedEventHandler OnPropertyChanged;

        public virtual void Validate()
        {           
            //Loop each field
            //  if field has definition and validations in it
            //  then call validate method
            Type type = GetType();
            PropertyInfo[] properties = PersistenceStrategyProvider.FindStrategyFor(type).GetPersistentProperties(type);

            if (properties != null)
            {
                foreach (PropertyInfo property in properties)
                {
                    FieldDefinitionAttribute definition = DataDictionary.Instance.GetFieldDefinition(type.Name + "." + property.Name);
                    if(definition==null)
                        throw new Exception("Definition okunamadý: " + property.Name);
                    object value = GetValue(property.Name);

                    //Length validation for string fields
                    if (definition.TypeName == typeof(string).Name)
                    {
                        if (value != null && definition.Length < value.ToString().Length) //! value can be enumeration so call toString
                        {
                            throw new Exception("Ýzin verilenden daha uzun bilgi! " + property.Name);
                        }
                    }

                    //Required validation
                    if (definition.IsRequired)
                        ValidateRequiredField(value, definition.Text);
                }
            }
        }

        protected void ValidateRequiredField(object value, string fieldName)
        {
            if (IsAutoImport)
                return;

            if (value==null)
                throw new Exception("Lütfen '" + fieldName + "' Alanýný Boþ Býrakmayýnýz.");
            
            Type type = value.GetType();
            if (type.IsSubclassOf(typeof(ActiveRecordBase))
                && !((ActiveRecordBase)value).Exist())
            {
                throw new Exception("Lütfen '" + fieldName + "' Alanýný Boþ Býrakmayýnýz.");
            }
            else if (type == typeof(string) && string.IsNullOrEmpty((string)value))
            {
                throw new Exception("Lütfen '" + fieldName + "' Alanýný Boþ Býrakmayýnýz.");
            }
            else if (string.IsNullOrEmpty(value.ToString()))
            {
                throw new Exception("Lütfen '" + fieldName + "' Alanýný Boþ Býrakmayýnýz.");
            }
        }

        public virtual void Insert()
        { 
            Validate();            
            Transaction.Instance.Join(delegate()
            {
                Id = Convert.ToInt64(Persistence.Insert(this));
                InsertAllChilds();
            });
        }

        public virtual int Update()
        {
            Validate();
            int i = -1;
            Transaction.Instance.Join(delegate()
            {
                i = Persistence.Update(this);
                if (i == 1)
                {
                    DeleteAllChilds();
                    InsertAllChilds();
                }
                else
                    throw new ApplicationException("Bu kayýt sizden önce deðiþtirilmiþ veya silinmiþ.");
            });
            return i;
        }

        public void Save()
        {
            if (Exist())
                Update();
            else
                Insert();
        }

        public virtual void Delete()
        {
            Type typeOfInstance = this.GetType();
            object keyValue = Id;
            bool throwException = true;

            Transaction.Instance.Join(delegate()
            {
                DeleteAllChilds();
                Persistence.DeleteByKey(typeOfInstance, keyValue, throwException);
            });
        }

        public virtual Type GetChildType()
        {
            return null;
        }

        public virtual string GetFKName()
        {
            return null;
        }

        public virtual void DeleteAllChilds()
        {
            if (GetChildType() == null || string.IsNullOrEmpty(GetFKName())) return;
            string sql = "delete from " + GetChildType().Name + " where " + GetFKName() + "='" + Id + "'";

            Transaction.Instance.ExecuteNonQuery(sql);
        }

        public virtual void InsertAllChilds()
        {
            if (GetChildType() == null || string.IsNullOrEmpty(GetFKName())) return;

            string parentProperty = GetFKName().Substring(0, GetFKName().IndexOf("_"));
            foreach (ActiveRecordBase child in Details)
            {
                child.SetValue(parentProperty, this);
                child.Insert();
            }
        }

        public string GetName()
        {
            return this.GetType().Name;
        }

        public object GetValue(string fieldName)
        {
            return ReflectionHelper.GetValue(this, fieldName);
        }

        public object GetValueString(string fieldName)
        {
            object value;

            value = ReflectionHelper.GetValue(this, fieldName);
            if (value is DateTime)
                value = ((DateTime)value).ToString("dd.MM.yyyy");
            
            return value;
        }

        public void SetValue(string fieldName, object value)
        {
            ReflectionHelper.SetValue(this, fieldName, value);
        }

        public virtual bool Exist()
        {
            return id != 0;

            /*if (Id == null) return false;

            if (Id is string && string.IsNullOrEmpty((string)Id)) return false;
            if (Id is Int32 && ((Int32)Id) <= 0) return false;
            if (Id is Int64 && ((Int64)Id) <= 0) return false;
            
            return true;*/
        }

        private List<ActiveRecordBase> Details = new List<ActiveRecordBase>();

        public void SetChilds(List<ActiveRecordBase> Details)
        {
            this.Details = Details;
        }

        public void Read()
        {
            Read(false);
        }

        public void Read(bool forceRead)
        {
            if (!forceRead)
            {
                if (isRead) return;
                isRead = true;
            }
            Persistence.Read(this, Id);
        }

       
    }

    public delegate void PropertyChangedEventHandler(object sender, PropertyChangedArgs e);

    public class PropertyChangedArgs : EventArgs
    {
        public PropertyChangedArgs(string propertyName)
        {
            this.propertyName = propertyName;
        }

        private string propertyName;

        public string PropertyName
        {
            get { return propertyName; }
            set { propertyName = value; }
        }
    }
}