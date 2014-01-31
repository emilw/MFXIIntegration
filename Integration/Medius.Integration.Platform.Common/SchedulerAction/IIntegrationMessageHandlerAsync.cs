using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medius.Data;

namespace Medius.Integration.Platform.SchedulerAction
{
    public interface IIntegrationMessageHandlerAsync : IIntegrationMessageHandler
    {
        void Handle(Entity entity);
    }
}
