using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medius.Core.Entities;
using Medius.Core.Platform.BusinessRuleEngine.Perspectives;

namespace Medius.Integration.Platform.BusinessRulePerspectives
{
    public class IntegrationMasterDataImportPerspective: CorePerspective
    {
        public MasterData MappedMasterDataRecord { get; set; }
        public DataRow DataRow { get; set; }
        public bool Skip { get; set; }
        public string SkipReason { get; set; }

        public void SkipThisRecord(string reason)
        {
            SkipReason = reason;
            Skip = true;
        }
    }
}
