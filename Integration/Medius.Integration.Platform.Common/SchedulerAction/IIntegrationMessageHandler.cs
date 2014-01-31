using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medius.Core.Platform.BusinessRuleEngine;
using Medius.Data;
using Medius.Logging;

namespace Medius.Integration.Platform.SchedulerAction
{
    [InheritedExport]
    public interface IIntegrationMessageHandler
    {
        Type ContentType { get;}
        string MessageTagRequest { get; }
        void Init(ILog log, IRepository repository, IBusinessRuleEngineFactory businessRuleFactory);
    }
}
