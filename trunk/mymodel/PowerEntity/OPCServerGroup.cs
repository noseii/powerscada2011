using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata;
using System.Data;
using SharpBullet.OAL;


namespace mymodel
{
    public class OPCServerGroup : Entity
    {

        public OPCServer opcserver;

        public OPCServer OPCServer
        {
            get
            {
                return opcserver == null ? opcserver = new OPCServer() : opcserver;
            }
            set
            {
                opcserver = value;
            }
        }

        [FieldDefinition(Length = 100, IsRequired = true)]
        public string OPCGroupName { get; set; }

        public int GroupUpdateRate { get; set; }
       
        public decimal GroupDeadBand { get; set; }
  
        public bool GroupActiveState { get; set; }
    
        public OPCServerGroup()
        {
           
        }

        public override string ToString()
        {
            return OPCGroupName;
        }

    }
}
