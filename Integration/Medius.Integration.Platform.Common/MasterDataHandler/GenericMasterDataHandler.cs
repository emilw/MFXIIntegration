using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medius.Core.Entities;
using Medius.Core.Platform.BusinessRuleEngine;
using Medius.Integration.Platform.BusinessRulePerspectives;

namespace Medius.Integration.Platform.MasterDataHandler
{
    public class GenericMasterDataHandler : BaseMasterDataHandler
    {

        private Type _masterDataType = null;
        private IBusinessRuleEngineFactory _breFactory;
        private Company _company;
        public GenericMasterDataHandler(Type masterDataType, Company company, Medius.Core.Platform.BusinessRuleEngine.IBusinessRuleEngineFactory businessRuleEngine)
        {
            _masterDataType = masterDataType;
            _breFactory = businessRuleEngine;
            _company = company;
        }

        public override Type MasterDataType
        {
            get
            {
                return _masterDataType;
            }
        }

        protected override MediusTransformer.ConnectionType.Source.Source PrepareSource()
        {
            var source = _breFactory.Create<GenericMasterDataSourcePerspective>(this.MasterDataType, null, _company);

            var sourceToExecute = source.FirstOrDefault();

            if (sourceToExecute != null)
            {
                sourceToExecute.CreateSource();
                sourceToExecute.Prepare();
                sourceToExecute.Execute();

                return sourceToExecute.Source;
            }
            else
            {
                Log.InfoFormat("Could not load source for generic master data handler {0} ", this.MasterDataType);
                return null;
            }
        }
    }
}
