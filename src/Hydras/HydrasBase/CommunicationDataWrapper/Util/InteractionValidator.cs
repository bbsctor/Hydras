using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.AppInterface;
using HydrasProtocol.ExceptionDefine;
using HydrasProtocol;

namespace HydrasBase.CommunicationDataWrapper.Util
{
    public class InteractionValidator : IInteractionValidator
    {
        public bool validate(Object request, Object response)
        {
            CommonRequestFrame req = ((Request)request).requestFrame;
            CommonResponseFrame resp = ((Response)response).responseFrame;
            if (resp.data.RelateElements != null && req.funcNo.Data[0] != resp.funcNo.Data[0])
            {
                throw new AbnormalResponseException();
            }
            return true;
        }
    }
}
