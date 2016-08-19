using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.UITask;
using ConfigFrame.AppInterface;
using ConfigFrame.CommunicationService.SerialPortSupport;

namespace HydrasUI_WPF.UIServiceImpl
{
    public class UIBasicService : IUIService
    {
        protected IController controller;
        protected SerialPortModel port;
    }
}
