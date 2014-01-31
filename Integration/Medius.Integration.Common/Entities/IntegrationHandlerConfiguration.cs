using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medius.Data;
using System.Runtime.Serialization;

namespace Medius.Integration.Entities
{
    public class IntegrationHandlerConfiguration : DataDefinition, Medius.Core.Entities.ICompanyRelated
    {
        [DataMember]
        public string HandlerFullName { get; set; }

        [DataMember]
        public bool Active { get; set; }

        [DataMember]
        public int Prio { get; set; }

        [DataMember]
        public string EntityTypeToProcess { get; set; }

        [DataMember]
        public Core.Entities.Company Company { get; set; }

        public Entity GetRelatedEntity()
        {
            return Company;
        }
    }
}
