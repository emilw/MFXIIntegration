using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medius.Integration.Entities;
using Medius.Schedulers;
using System.ComponentModel.Composition;
using Medius.Core.Platform.EntityRepository;
using Medius.Core.Integrations.Types;

namespace Medius.Integration.Platform.ScheduledJobs
{
    public class IntegrationQueueReaderJob : ScheduledJobBase
    {
        [Import]
        IIntegrationMessageRepository IntegrationRepository;

        public IntegrationQueueReaderJob(){}

        public override void RunJob(ScheduledJobConfiguration configuration)
        {
            if (IntegrationRepository != null)
            {
                var message = IntegrationRepository.GetNextMessageFromQue("Medius.ExpenseInvoice.Entities.ExpenseInvoice", "PRELIMINARY", null, "Medius.Core.Entities.Integration.Messages.RequestMessage", IntegrationMessageStatus.Posted, false);

                var entity = new DummyEntity() { TestProperty = "No message found" };

                if (message != null)
                {
                    entity.TestProperty = message.Id.ToString();
                }

                Repository.Save(ref entity);
            }

        }
    }
}
