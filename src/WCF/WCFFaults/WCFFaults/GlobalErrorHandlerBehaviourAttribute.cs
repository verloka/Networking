using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace WCFFaults
{
    class GlobalErrorHandlerBehaviourAttribute : Attribute, IServiceBehavior
    {
        public Type ErrorHandlerType { get; private set; }

        public GlobalErrorHandlerBehaviourAttribute(Type ErrorHandlerType)
        {
            this.ErrorHandlerType = ErrorHandlerType;
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters) { }
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase) { }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase) 
        {
            IErrorHandler hundler = (IErrorHandler)Activator.CreateInstance(ErrorHandlerType);
            foreach (var item in serviceHostBase.ChannelDispatchers)
                if (item is ChannelDispatcher cb)
                    cb.ErrorHandlers.Add(hundler);
        }
    }
}
