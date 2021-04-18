using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace WCFFaults
{
    class GlobalErrorHandler : IErrorHandler
    {
        public bool HandleError(Exception error)
        {
            //logging here, call async

            return true;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            if (error is FaultException)
                return;                     //already processd

            FaultException fe = new FaultException("Service Error", new FaultCode("GeneralException"));
            MessageFault mf = fe.CreateMessageFault();
            fault = Message.CreateMessage(version, mf, null);
        }
    }
}
