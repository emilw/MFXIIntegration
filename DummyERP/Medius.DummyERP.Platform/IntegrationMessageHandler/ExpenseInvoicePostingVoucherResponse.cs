using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medius.DummyERP.Platform.IntegrationMessageHandler
{
    public abstract class ExpenseInvoicePostingVoucherResponse : SupplierInvoicePostingVoucherResponse
    {
        public override Type ContentType
        {
            get
            {
                return typeof(Medius.ExpenseInvoice.Entities.ExpenseInvoice);
            }
        }
    }
}
