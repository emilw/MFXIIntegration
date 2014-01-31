using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medius.Integration.Platform.SchedulerAction;

namespace Medius.DummyERP.Platform.MasterDataImportHandler
{
    [Export(typeof(IMasterDataHandler))]
    public class SupplierImport : SPCSImportHandlerBase, IMasterDataHandler
    {
        public override Core.Entities.MasterData CreateNewInstance()
        {
            return new Medius.Enterprise.Entities.Supplier();
        }

        protected override MediusTransformer.ConnectionType.Source.Source PrepareSource()
        {
            var source = base.PrepareSource();
            source.SelectionStatement = "ADK_DB_SUPPLIER";
            return source;
        }

        public override System.Type MasterDataType
        {
            get
            {
                return typeof(Medius.Enterprise.Entities.Supplier);
            }
        }
    }
}
