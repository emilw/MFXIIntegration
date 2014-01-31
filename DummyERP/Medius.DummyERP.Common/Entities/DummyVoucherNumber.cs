using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Medius.Data;

namespace Medius.DummyERP.Entities
{
    public class DummyVoucherNumber : DataDefinition
    {
        [DataMember]
        public int DocumentId { get; set; }
    }
}
