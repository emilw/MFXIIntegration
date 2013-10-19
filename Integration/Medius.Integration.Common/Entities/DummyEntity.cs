using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Medius.Data;

namespace Medius.Integration.Entities
{
    public class DummyEntity : DataDefinition
    {
        [DataMember]
        public string TestProperty { get; set; }
    }
}
