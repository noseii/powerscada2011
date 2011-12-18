using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata;
using System.Data;
using SharpBullet.OAL;


namespace mymodel
{
    public class OPCServer : Entity
    {
        [FieldDefinition(Length =100,IsRequired=true)]
        public string OpcServerName { get; set; }

        [FieldDefinition(Length = 100)]
        public string OPCNodeName { get; set; }

        //[FieldDefinition(Length = 100, IsRequired = true)]
        //public string OPCGroupName { get; set; }

       
        //public int GroupUpdateRate { get; set; }
       
        //public decimal GroupDeadBand { get; set; }
  
        //public bool GroupActiveState { get; set; }

        private List<OPCServerGroup> groups;

        [FieldDefinition(MappingType = FieldMappingType.No)]
        public List<OPCServerGroup> Groups
        {
            get
            {
                if (groups == null)
                    groups = new List<OPCServerGroup>();

                return groups;
            }
            set
            {
                groups = value;
            }
        }


        public OPCServer()
        {
           
        }

        #region static Methots
        public static OPCServerGroup[] ReadGroups(long serverId)
        {
            Condition[] conditionss = new Condition[1];
            conditionss[0].Field = "OPCServer_Id";
            conditionss[0].Value = serverId;
            conditionss[0].Operator = System.Operator.Equal;
            OPCServerGroup[] groups = Persistence.ReadList<OPCServerGroup>(new string[] { "*" }, conditionss, null, 100);
            //if (groups.Length > 0)
            //{
            //    foreach (OPCServerGroup group in groups)
            //    {
            //        if (group.adres.Id > 0)
            //            chzadres.Adres = Persistence.Read<OPCServerGroup>(groups.adres.Id);
            //    }
            //}
            return groups;
        }

        public static OPCServerGroup ReadGroups(long serverId,string lokasyonadi)
        {
            Condition[] conditionss = new Condition[2];
            conditionss[0].Field = "OPCServer_Id";
            conditionss[0].Value = serverId;
            conditionss[0].Operator = System.Operator.Equal;

            conditionss[1].Field = "OPCGroupName";
            conditionss[1].Value = lokasyonadi;
            conditionss[1].Operator = System.Operator.Equal;


            OPCServerGroup[] groups = Persistence.ReadList<OPCServerGroup>(new string[] { "*" }, conditionss, null, 100);
            if (groups.Length > 0)
            {
              return groups[0];
            }
            return null;
        }

        #endregion Methots

    }
}
