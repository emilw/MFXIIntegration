using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medius.Core.Platform.BusinessRuleEngine;
using Medius.Core.Services;
using Medius.Data;
using Medius.Integration.Entities;
using Medius.Integration.Platform.MasterDataHandler;
using Medius.Integration.Platform.SchedulerAction;
using Medius.Schedulers;
using MediusTransformer.ConnectionType.Source.StaticColumn;
using MediusTransformer.Parameters;

namespace Medius.Integration.Platform.ScheduledJobs
{
    public class MasterDataImportJob : ScheduledJobBase
    {

        [Import]
        IMasterDataImportService masterDataService;

        [ImportMany]
        IEnumerable<Medius.Integration.Platform.SchedulerAction.IMasterDataHandler> CustomMasterDataHandlers;

        List<IMasterDataHandler> ActiveMasterDataHandlers;

        [Import]
        IBusinessRepository BusinessRepository;

        [Import]
        IBusinessRuleEngineFactory BusinessRuleEngineFactory;

        [Import]
        Medius.DataObjectRepository.IDataObjectRepository DataObjectRepository;

        private void AddGenericHandlers(IEnumerable<IntegrationHandlerConfiguration> configuration)
        {
            var masterDataHandlersToHandleGeneric = configuration.Where(x => x.HandlerFullName == typeof(GenericMasterDataHandler).FullName
                                                                        && x.EntityTypeToProcess != null
                                                                        && x.Active);

            foreach(var config in masterDataHandlersToHandleGeneric)
            {
                var dataObjectType = DataObjectRepository.GetDataObjectType(config.EntityTypeToProcess);
                var systemType = DataObjectRepository.GetTypeFromDataObjectType(dataObjectType);

                if (ActiveMasterDataHandlers.ToList().Exists(x => x.MasterDataType.GetType() == systemType))
                {
                    LogThis("Adding generic master data handler for entity type {0}", systemType.GetType().FullName);
                    ActiveMasterDataHandlers.Add(new GenericMasterDataHandler(systemType, config.Company, BusinessRuleEngineFactory) {Prio = config.Prio });
                }
            }
        }

        private void AddCustomHandlers(IEnumerable<IntegrationHandlerConfiguration> configuration)
        {
            var activeHandlers = CustomMasterDataHandlers.Join(configuration, x => x.GetType().FullName,
                                                                                    y => y.HandlerFullName,
                                                                                    (x, y) => { x.Prio = y.Prio; return x; })
                                                                                    .OrderBy(x => x.Prio).ToList();

            var toBeAdded = ActiveMasterDataHandlers.Where(x => x.GetType() != activeHandlers.GetType());

            if (toBeAdded.Count() > 0)
            {
                LogThis("Added {0} custom handlers", toBeAdded.Count());
                ActiveMasterDataHandlers.AddRange(toBeAdded);
            }
        }

        public override void RunJob(ScheduledJobConfiguration configuration)
        {
            LogThis("In the loop");
            if (masterDataService == null)
                LogThis("The master data service is not initialize");

            var configuredActiveHandlers = Repository.Query<IntegrationHandlerConfiguration>(x => x.Active).ToList();

            if (configuredActiveHandlers.Count() > 0)
            {
                LogThis("Number of configuration records {0}", configuredActiveHandlers.Count());
                AddGenericHandlers(configuredActiveHandlers);
                AddCustomHandlers(configuredActiveHandlers);

                LogThis("Number of master data handlers handlers loaded {0}", ActiveMasterDataHandlers.Count());



                foreach (var handler in ActiveMasterDataHandlers)
                {
                    LogThis("Starting master data handler {0} with prio {1}", handler.GetType().FullName, handler.Prio);
                    LogThis("Number 2");
                    handler.Init(this.BusinessRepository, this.Log, this.BusinessRuleEngineFactory);
                    var masterData = handler.Run();

                    if (masterData.Count() > 0)
                    {
                        LogThis("Master data count {0}", masterData.Count());
                        masterDataService.Import(masterData);
                    }
                }
            }
        }

        public void LogThis(string message, params object[] args)
        {
            Console.WriteLine(message, args);
        }
    }
}


