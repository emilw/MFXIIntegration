using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medius.Core.Entities;
using Medius.Core.Platform.BusinessRuleEngine;
using Medius.Data;
using Medius.Integration.Platform.BusinessRulePerspectives;
using Medius.Integration.Platform.SchedulerAction;
using Medius.Logging;
using MediusTransformer.ConnectionType.Source;
using MediusTransformer.ConnectionType.Source.StaticColumn;
using MediusTransformer.Parameters;

namespace Medius.Integration.Platform.MasterDataHandler
{
    public abstract class BaseMasterDataHandler : IMasterDataHandler
    {
        protected IBusinessRepository BusinessRepository;
        protected ILog Log;
        protected IBusinessRuleEngineFactory BusinessRuleFactory;

        public virtual MasterData CreateNewInstance()
        {
            throw new NotImplementedException();
        }

        public virtual Type MasterDataType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Init(Data.IBusinessRepository businessRepository, Logging.ILog log, IBusinessRuleEngineFactory businessRuleFactory)
        {
            LogThis("Initialization of the message handler {0}", this.GetType().FullName);
            this.BusinessRepository = businessRepository;
            this.Log = log;
            this.BusinessRuleFactory = businessRuleFactory;
        }

        public MasterData[] Run()
        {
            LogThis("Preparing source");
            var source = PrepareSource();
            LogThis("Getting data from source");
            var result = source.getDataTable();
            LogThis("The DataTable size is: {0}", result.Rows.Count);
            if (result.Rows.Count != 0)
            {
                var dataSet = new DataSet();
                dataSet.Tables.Add(result);

                return Map(dataSet).ToArray();
            }

            return new List<MasterData>().ToArray();

        }

        protected IEnumerable<MasterData> Map(DataSet dataSet)
        {
            var rules = BusinessRuleFactory.Create<IntegrationMasterDataImportPerspective>(CreateNewInstance().GetType(), null, null);
            var mappedMasterData = new List<MasterData>();
            foreach (DataRow tableRows in dataSet.Tables[0].Rows)
            {
                var masterDataRecord = CreateNewInstance();
                bool Skip = false;

                foreach (var rule in rules)
                {
                    rule.Skip = Skip;
                    rule.MappedMasterDataRecord = masterDataRecord;
                    rule.DataRow = tableRows;
                    rule.Prepare();
                    rule.Execute();
                    Skip = rule.Skip;

                    if (Skip)
                    {
                        LogThis(rule.SkipReason);
                        break;
                    }
                }

                if(!Skip)
                    mappedMasterData.Add(masterDataRecord);
            }
            return mappedMasterData;
        }

        private void LogThis(string message, params object[] args)
        {
            Console.WriteLine(message, args);
        }

        protected virtual Source PrepareSource()
        {
            throw new NotImplementedException();
        }


        public int Prio { get; set; }
    }
}
