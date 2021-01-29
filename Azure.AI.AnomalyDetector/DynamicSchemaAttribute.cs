using System;
using System.Collections.Generic;
using System.Text;

namespace Azure.Core
{
    [AttributeUsage(AttributeTargets.ReturnValue | AttributeTargets.Property | AttributeTargets.Parameter)]
    public class DynamicSchemaAttribute : Attribute
    {
        public string Schema { get; }

        public DynamicSchemaAttribute(string schema)
        {
            Schema = schema;
        }
    }
}
