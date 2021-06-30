using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Artist.Exceptions;
using Serilog;

namespace Artist.Exceptions
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ExceptionSubstitutorAttribute : Attribute, IServiceBehavior
    {
        class ErrorHandler : IErrorHandler
        {
            public bool HandleError(Exception error)
            {
                Log.Error(error.ToString());
                return true;
            }

            public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
            {
                if (error is FaultException<int>)
                {
                    return;
                }

                var code = (int)Errors.TechnicalError;

                if (error is CodedException)
                {
                    code = (error as CodedException).Code;
                }

                var e = new FaultException<int>(code, error.Message);
                fault = Message.CreateMessage(version, e.CreateMessageFault(), e.Action);
            }
        }

        private IErrorHandler _errorHandler = new ErrorHandler();

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers.OfType<ChannelDispatcher>())
            {
                channelDispatcher.ErrorHandlers.Add(_errorHandler);
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }
}
