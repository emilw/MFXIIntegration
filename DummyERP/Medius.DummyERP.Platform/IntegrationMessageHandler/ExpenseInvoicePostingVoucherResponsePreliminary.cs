using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medius.DummyERP.Platform.IntegrationMessageHandler
{
    public class ExpenseInvoicePostingVoucherResponsePreliminary : ExpenseInvoicePostingVoucherResponse
    {
        public override string MessageTagRequest
        {
            get
            {
                return "PRELIMINARY";
            }
        }

        protected override string VoucherTag
        {
            get
            {
                return "PRELIMINARY";
            }
        }

        public override string MessageTagResponse
        {
            get
            {
                return "PRELIMINARY_VOUCHER";
            }
        }
    }
}
