using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBullet.OAL.Metadata
{
    public class RequiredValidation : Validation
    {
        public override void Validate(string field, object value)
        {
            if (value == null ||
                (value is string && ((string)value).Trim() == ""))
                throw new Exception("Bu alana bilgi girmeniz gereklidir: " + field);
        }
    }
}
