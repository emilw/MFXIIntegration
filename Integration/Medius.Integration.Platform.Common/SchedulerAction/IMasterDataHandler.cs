using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medius.Core.Entities;
using Medius.Core.Platform.BusinessRuleEngine;
using Medius.Data;
using Medius.Logging;

namespace Medius.Integration.Platform.SchedulerAction
{
    public interface IMasterDataHandler
    {
        void Init(IBusinessRepository businessRepository, ILog log, IBusinessRuleEngineFactory businessRuleFactory);
        MasterData[] Run();
        Type MasterDataType { get; }        

        int Prio { get; set; }
    }
}
