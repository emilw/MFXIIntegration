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
using Medius.Core.Services;
using Medius.Core.Security;
using Medius.Integration.Platform.SchedulerAction;
using Medius.Data;
using Medius.Core.Entities.Integration.Messages;
using Medius.Core.Platform.BusinessRuleEngine;

namespace Medius.Integration.Platform.ScheduledJobs
{
    public class IntegrationQueueReaderJob : ScheduledJobBase
    {
        [Import]
        IIntegrationMessageRepository IntegrationRepository;

        [Import]
        IBusinessRuleEngineFactory BusinessRuleFactory;

        [ImportMany]
        List<IIntegrationMessageHandler> IntegrationMessageHandlers;


        public IntegrationQueueReaderJob(){
        }

        public override void RunJob(ScheduledJobConfiguration configuration)
        {

            if (IntegrationRepository == null)
            {
                Log.Error("The integration job can not resolve the IntegrationRepository");
            }
            else
            {
                foreach (var messageHandler in IntegrationMessageHandlers)
                {
                    messageHandler.Init(this.Log, this.Repository, this.BusinessRuleFactory);
                    var messageTypeName = typeof(Medius.Core.Entities.Integration.Messages.RequestMessage).Name;
                    Log.InfoFormat("Checking for message for message handler {0} .....", messageHandler.GetType().FullName);
                    try
                    {
                        var message = IntegrationRepository.GetNextMessageFromQue(messageHandler.ContentType.FullName, messageHandler.MessageTagRequest, null, messageTypeName, IntegrationMessageStatus.Posted, true);

                        if (message != null)
                        {
                            Log.InfoFormat("Handling message with correalation key {0} and message id {1}",message.CorrelationKeys, message.Id);
                            var content = message.MessageContent.Entity;

                            if (typeof(IIntegrationMessageHandlerAsync).IsAssignableFrom(messageHandler.GetType()))
                            {
                                Log.Info("It's an async integration message");
                                var assyncHandler = (IIntegrationMessageHandlerAsync)messageHandler;
                                assyncHandler.Handle(content);
                            }
                            else
                            {
                                Log.Info("It's an sync integration message");
                                var syncHandler = (IIntegrationMessageHandlerSync)messageHandler;
                                var result = syncHandler.Handle(content);
                                Log.Info("Request processed, handling response message");
                                createResponseMessage(result, message, syncHandler.MessageTagResponse);
                                Log.Info("Response message was posted");
                            }

                            Log.Info("The message chain was processed");

                            /*if (IntegrationMessageHandlers != null)
                            {
                                Log.Info("Number of IntegrationMessageHandlers found " + IntegrationMessageHandlers.Count());

                                var content = message.MessageContent.Entity;

                                var integrationHandlersForContent = IntegrationMessageHandlers.Where(x => x.ContentType == content.GetType()).ToList();

                                var assyncHandlers = integrationHandlersForContent.Where(x => typeof(IIntegrationMessageHandlerAsync).IsAssignableFrom(x.GetType())).Cast<IIntegrationMessageHandlerAsync>().ToList();
                                var syncHandlers = integrationHandlersForContent.Where(x => typeof(IIntegrationMessageHandlerSync).IsAssignableFrom(x.GetType())).Cast<IIntegrationMessageHandlerSync>().ToList();

                                printNumberOfInstances(assyncHandlers);
                                printNumberOfInstances(syncHandlers);

                                assyncHandlers.ForEach(x => x.Handle(content));

                                syncHandlers.ForEach(x =>
                                {
                                    Log.Info("Handling message with correalation key " + message.CorrelationKeys);
                                    var result = x.Handle(content);
                                    Log.Info("Posting response");
                                    createResponseMessage(result, message, x.MessageTagResponse);
                                    Log.Info("Message was posted");
                                });
                            }*/
                        }

                        //Repository.Save(ref entity);
                    }
                    catch (Exception ex)
                    {
                        Log.Error("Error when reading messages", ex);
                    }
                }
            }

        }
        private void createResponseMessage(Entity responseEntity, IntegrationMessage requestMessage, string responseMessageTag)
        {
            var response = new ResponseMessage(){
                MessageTag = responseMessageTag,
                CorrelationKeys = requestMessage.CorrelationKeys,
                MessageContent = new Core.Entities.Integration.ExternalEntity(responseEntity)
            };

            Log.InfoFormat("Posting response with corrId {0} and response tag {1}", response.CorrelationKeys, response.MessageTag);

            IntegrationRepository.PostMessage(response, false);
        }

        private void printNumberOfInstances(IEnumerable<object> objects)
        {
            if (objects != null)
            {
                Log.InfoFormat("Number of objects in {0} is {1}", objects.GetType().FullName, objects.Count());
            }
        }
    }
}
