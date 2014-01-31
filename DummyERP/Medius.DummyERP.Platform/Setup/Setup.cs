using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medius.Data;

namespace Medius.DummyERP.Platform.Setup
{
    public class Setup : SetupBase
    {
        public override void Run()
        {
            RegisterIntegrationHandlers(typeof(MasterDataImportHandler.CurrencyImport).FullName, typeof(Medius.Core.Entities.Currency).FullName,true, 1);
            RegisterIntegrationHandlers(typeof(MasterDataImportHandler.CurrencyRateImport).FullName, typeof(Medius.Core.Entities.CurrencyRate).FullName, true, 2);
            RegisterIntegrationHandlers(typeof(MasterDataImportHandler.PaymentTermImport).FullName, typeof(Medius.Enterprise.Entities.PaymentTerm).FullName, true, 3);
            RegisterIntegrationHandlers(typeof(MasterDataImportHandler.AccountImport).FullName, typeof(Medius.Enterprise.Entities.Accounting.Dimensions.DimensionValue).FullName, true, 4);
            RegisterIntegrationHandlers(typeof(MasterDataImportHandler.SupplierImport).FullName, typeof(Medius.Enterprise.Entities.Supplier).FullName, true, 5);
        }

        private void RegisterIntegrationHandlers(string handlerName, string entityName, bool active, int prioOrder)
        {
            var integrationHandlerConfig = Repository.Query<Integration.Entities.IntegrationHandlerConfiguration>(c => c.HandlerFullName == handlerName &&
                                                                                                                    c.EntityTypeToProcess == entityName).SingleOrDefault();
            if (integrationHandlerConfig == null)
            {
                integrationHandlerConfig = new Integration.Entities.IntegrationHandlerConfiguration { HandlerFullName = handlerName, EntityTypeToProcess = entityName, Active = active, Prio = prioOrder};
                Repository.DirtySave(ref integrationHandlerConfig, refresh: false);
            }
        }
    }
}
