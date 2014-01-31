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
    public class PaymentTermImport : SPCSImportHandlerBase, IMasterDataHandler
    {
        public override Core.Entities.MasterData CreateNewInstance()
        {
            return new Medius.Enterprise.Entities.PaymentTerm();
        }

        protected override MediusTransformer.ConnectionType.Source.Source PrepareSource()
        {
            var source = base.PrepareSource();
            source.SelectionStatement = "ADK_DB_CODE_OF_TERMS_OF_PAYMENT";
            source.Filter = "ADK_CODE_OF_TERMS_OF_PAYMENT_PAYMENT_CODE <> 'K' AND ADK_CODE_OF_TERMS_OF_PAYMENT_PAYMENT_CODE <> 'P' AND ADK_CODE_OF_TERMS_OF_PAYMENT_PAYMENT_CODE <> 'E' AND ADK_CODE_OF_TERMS_OF_PAYMENT_CODE <> 'N' AND ADK_CODE_OF_TERMS_OF_PAYMENT_PAYMENT_CODE <> ''  AND ADK_CODE_OF_TERMS_OF_PAYMENT_PAYMENT_CODE <> 'AUT' AND ADK_CODE_OF_TERMS_OF_PAYMENT_PAYMENT_CODE <> 'L/C'";

            return source;
        }

        public override System.Type MasterDataType
        {
            get
            {
                return typeof(Medius.Enterprise.Entities.PaymentTerm);
            }
        }
    }
}
