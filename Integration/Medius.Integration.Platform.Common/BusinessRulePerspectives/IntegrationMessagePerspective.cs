using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medius.Core.Entities;
using Medius.Core.Platform.BusinessRuleEngine.Perspectives;
using Medius.Data;

namespace Medius.Integration.Platform.BusinessRulePerspectives
{
    public class IntegrationMessagePerspective : CorePerspective
    {
        public Entity MessageEntity { get; set; }
        public DataSet DataRow { get; set; }
        public string MessageTagRequest { get; set; }
        public Type ContentType { get; set; }
        public bool CheckMessageMode { get; set; }
    }
}
