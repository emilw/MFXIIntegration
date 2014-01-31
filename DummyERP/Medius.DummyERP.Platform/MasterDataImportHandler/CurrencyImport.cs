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
    public class CurrencyImport : SPCSImportHandlerBase, IMasterDataHandler
    {
        public override Core.Entities.MasterData CreateNewInstance()
        {
            return new Medius.Core.Entities.Currency();
        }

        protected override MediusTransformer.ConnectionType.Source.Source PrepareSource()
        {
            var source = base.PrepareSource();
            source.SelectionStatement = "ADK_DB_CODE_OF_CURRENCY";
            return source;
        }

        public override System.Type MasterDataType
        {
            get
            {
                return typeof(Medius.Core.Entities.Currency);
            }
        }
    }
}
