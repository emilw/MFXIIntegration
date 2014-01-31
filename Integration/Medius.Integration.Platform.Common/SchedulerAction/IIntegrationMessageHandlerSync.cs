using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medius.Data;

namespace Medius.Integration.Platform.SchedulerAction
{
    public interface IIntegrationMessageHandlerSync : IIntegrationMessageHandler
    {
        Entity Handle(Entity entity);
        string MessageTagResponse { get; }
    }
}
