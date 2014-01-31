using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medius.Integration.Platform.SchedulerAction;
using Medius.ExpenseInvoice.Entities;
using Medius.Data;
using System.ComponentModel.Composition;
using Medius.Enterprise.Entities;
using Medius.DummyERP.Entities;
using Medius.Logging;
using Medius.Integration.Platform.MessageHandler;

namespace Medius.DummyERP.Platform.IntegrationMessageHandler
{
    public abstract class SupplierInvoicePostingVoucherResponse : BaseIntegrationMessageHandler, IIntegrationMessageHandlerSync
    {
        protected virtual string VoucherTag
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override Type ContentType
        {
            get { throw new NotImplementedException(); }
        }

        public Entity Handle(Data.Entity entity)
        {
            var expenseInvoice = (Medius.PurchaseToPay.Entities.SupplierInvoice)entity;

            Log.Info("Expense: " + expenseInvoice.InvoiceNumber);
            expenseInvoice.VoucherObject = getVoucher(expenseInvoice.Id);
            Log.Info("Voucher created for document " + expenseInvoice.Id.ToString());
            return expenseInvoice;
        }

        private VoucherObject getVoucher(int documentId)
        {
            var voucherRecord = Repository.Query<DummyVoucherNumber>(x => x.DocumentId == documentId).FirstOrDefault();
            if (voucherRecord == null)
            {
                voucherRecord = new DummyERP.Entities.DummyVoucherNumber() { DocumentId = documentId };
                Repository.Save(ref voucherRecord);
            }

            var voucherObject = new Enterprise.Entities.VoucherObject();
            voucherObject.AddVoucher(new Enterprise.Entities.Voucher() { Value = voucherRecord.Id.ToString(), Date = DateTime.Now, Tag = this.VoucherTag });

            return voucherObject;
        }

        public virtual string MessageTagResponse
        {
            get { throw new NotImplementedException(); }
        }
    }
}
