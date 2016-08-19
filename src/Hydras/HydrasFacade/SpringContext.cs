using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Context;
using Spring.Context.Support;
using ConfigFrame.AppInterface;
using ConfigFrame.CommunicationService.SerialPortSupport;
using ConfigFrame.ExceptionHandle;
using ConfigFrame.CommunicationService;

namespace HydrasFacade
{
    public class SpringContext
    {
        public static void initContext()
        {
            IApplicationContext context = ContextRegistry.GetContext();
            Console.WriteLine("OK!");
        }

        public static void setLocalServerExceptionHandler(ExceptionHandler handler)
        {
            IApplicationContext context = ContextRegistry.GetContext();
            LocalRunningServer server = (LocalRunningServer)context["localRunningServer"];
            server.exceptionHandler = handler;
        }

        public static IController getControllerByName(string name)
        {
            IApplicationContext context = ContextRegistry.GetContext();
            return (IController)context[name];
        }

        public static ICommunicationService getCommunicationService()
        {
            IApplicationContext context = ContextRegistry.GetContext();
            return (ICommunicationService)context["communicationService"];
        }

        //public static ModelRepository getModelRepository()
        //{
        //    IApplicationContext context = ContextRegistry.GetContext();
        //    return (ModelRepository)context["modelRepository"];
        //}
    }
}
