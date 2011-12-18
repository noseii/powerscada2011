using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Reflection;

namespace SharpBullet.OAL.Metadata
{
    public class DataDictionary
    {
        #region Singleton
        private DataDictionary()
        {
        }

        private static DataDictionary instance;

        public static DataDictionary Instance
        {
            get
            {
                if (instance == null) instance = new DataDictionary();
                return instance;
            }
            set { instance = value; }
        }
        #endregion

        private Hashtable entityTypeHash = new Hashtable();

        public void AddEntityType(string entityName, Type entityType)
        {
            entityTypeHash[entityName] = entityType;
        }

        public void AddEntities(Type[] entities)
        {
            foreach (Type entityType in entities)
            {
                if (!entityType.IsClass
                    || entityType.IsGenericType
                    || entityType.Name.Contains("<")) continue;

                string className = entityType.Name;
                AddEntityType(className, entityType);
            }
        }

        public int EntityCount { get { return entityTypeHash.Count; } }

        public Type GetTypeofEntity(int index)
        {
            int i = 0;
            foreach (object key in entityTypeHash.Keys)
            {
                if (index == i)
                    return (Type)entityTypeHash[key];
                i++;
            }

            throw new IndexOutOfRangeException();
        }

        public Type GetTypeofEntity(string entityName)
        {
            return (Type)entityTypeHash[entityName];
        }

        public EntityDefinition GetEntityDefinition(string entityName)
        {
            EntityDefinition definition = new EntityDefinition();

            Type type = GetTypeofEntity(entityName);
            if (type == null) return null;

            EntityDefinitionAttribute attr = ReflectionHelper.GetAttribute<EntityDefinitionAttribute>(type);
            if (attr != null)
            {
                definition.Name = entityName;
                definition.IdMethod = attr.IdMethod;
                definition.StringField = attr.StringField;
                definition.OptimisticLockField = attr.OptimisticLockField;
            }
            else
                throw new Exception("Entity definition is missing: " + entityName);

            return definition;
        }

        /// <summary>
        /// Finds the first field that matches the given fieldType. If not found, returns "Id".
        /// </summary>
        /// <param name="entityName">Name of the entity</param>
        /// <param name="fieldType">Type of the field</param>
        /// <returns></returns>
        public string GetFirstFieldName(string entityName, Type fieldType)
        {
            EntityDefinition entityDefinition = GetEntityDefinition(entityName);
            string strProp = entityDefinition.StringField;
            if (String.IsNullOrEmpty(strProp))
            {
                PropertyInfo firstStrProp = ReflectionHelper.GetFirstProperty(GetTypeofEntity(entityName), fieldType);
                strProp = firstStrProp == null ? "Id" : firstStrProp.Name;
            }
            return strProp;
        }

        public List<FieldDefinitionAttribute> GetAllFields(string entityName)
        {
            Type type = GetTypeofEntity(entityName);
            if (type == null) return null;

            PropertyInfo[] props = type.GetProperties();

            List<FieldDefinitionAttribute> result = new List<FieldDefinitionAttribute>();
            foreach (PropertyInfo property in props)
            {
                result.Add(
                    GenerateFieldDefinition(type, property));
            }
            return result;
        }

        /// <summary>
        /// Finds the field definition for a field path.
        /// </summary>
        /// <param name="fieldPath">Full path of a field.</param>
        /// <returns>Field definition</returns>
        public FieldDefinitionAttribute GetFieldDefinition(string fieldPath)
        {
            string[] fields = fieldPath.Split(new char[] { '.' });

            string entityName = fields[0];
            Type type = GetTypeofEntity(entityName);
            Type entityType = null;
            PropertyInfo property = null;

            for (int i = 1; i < fields.Length; i++)
            {
                property = type.GetProperty(fields[i]);
                entityType = type;
                type = property.PropertyType;
            }
        
            return GenerateFieldDefinition(entityType, property);
        }

        private FieldDefinitionAttribute GenerateFieldDefinition(Type entityType, PropertyInfo property)
        {
            FieldDefinitionAttribute attr = null;
            //EntityFieldDefinition fieldDefinition = new EntityFieldDefinition();
            //fieldDefinition.Name = property.Name;
                       
            if(property.Name != "Id")
                attr = ReflectionHelper.GetAttribute<FieldDefinitionAttribute>(property);
            else
                attr = ReflectionHelper.GetAttribute<FieldDefinitionAttribute>(entityType);

            if (attr != null)
            {
                attr.InstanceProperty = property;
            }
            else
            {
                attr = new FieldDefinitionAttribute(property);
            }

            return attr;
        }

        public List<FieldDefinitionAttribute> GetFilteringFields(string entityName)
        {
            EntityDefinition definition = new EntityDefinition();

            Type type = GetTypeofEntity(entityName);
            if (type == null) return null;

            List<FieldDefinitionAttribute> list = new List<FieldDefinitionAttribute>();
            PropertyInfo[] props = type.GetProperties();
            foreach (PropertyInfo pi in props)
            {
                FieldDefinitionAttribute f = GenerateFieldDefinition(type, pi);
                if (f.IsFiltered)
                    list.Add(f);
            }

            return list;
        }
    }
}
