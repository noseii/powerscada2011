using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBullet.OAL.Metadata
{
    /*public class EntityFieldDefinition : DefinitionBase
    {
        private int entity_id;

        /// <summary>
        /// Foreign key of entity.
        /// </summary>
        public int Entity_Id
        {
            get { return entity_id; }
            set { entity_id = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        private string shortText;

        public string ShortText
        {
            get { return shortText; }
            set { shortText = value; }
        }

        private string typeName;

        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }

        private Validation[] validations;

        public Validation[] Validations
        {
            get { return validations; }
            set { validations = value; }
        }

        public bool IsRequired()
        {
            if(validations!=null)
                foreach (Validation v in validations)
                {
                    if (v is RequiredValidation) return true;
                }

            return false;
        }

        public string GetInputClass()
        {
            return TypeName;
        }
    }*/
}
