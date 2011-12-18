using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace SharpBullet.DAL
{
    public class ParameterHandle : IParameterHandle
    {
        public ParameterHandle(DbParameter parameter)
        {
            this.parameter = parameter;
        }

        private DbParameter parameter;

        public DbParameter Parameter
        {
            get { return parameter; }
        }

        public object Value
        {
            get { return parameter.Value; }
            set { parameter.Value = value; }
        }
    }
}
