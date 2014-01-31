using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medius.Core.Platform.BusinessRuleEngine;
using Medius.Data;
using Medius.Integration.Platform.SchedulerAction;
using Medius.Logging;

namespace Medius.Integration.Platform.MessageHandler
{
    public abstract class BaseIntegrationMessageHandler : IIntegrationMessageHandler
    {
        protected ILog Log;
        protected IRepository Repository;
        protected IBusinessRuleEngineFactory BusinessRuleFactory;

        public void Init(Logging.ILog log, Data.IRepository repository, IBusinessRuleEngineFactory businessRuleFactory)
        {
            Log = log;
            Repository = repository;
            BusinessRuleFactory = businessRuleFactory;
        }

        public virtual Type ContentType
        {
            get { throw new NotImplementedException(); }
        }

        public virtual string MessageTagRequest
        {
            get { throw new NotImplementedException(); }
        }
    }
}
