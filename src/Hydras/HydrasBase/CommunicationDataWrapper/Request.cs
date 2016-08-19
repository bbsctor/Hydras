using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.CommunicationDataWrapper;
using ConfigFrame.CommunicationDataWrapper.Complex;
using HydrasProtocol;

namespace HydrasBase.CommunicationDataWrapper
{
    public class Request : IComplexRequest
    {
        public CommonRequestFrame requestFrame;

        private byte[] data;
        public byte[] Data
        {
            get { return data; }
            set { this.data = value; }
        }

        private bool isNull;

        public bool IsNull
        {
            get { return this.isNull; }
            set { this.isNull = value; }
        }

        public byte[] genBytes()
        {
            return requestFrame.Data;
        }

        public IResponse genResponse()
        {
            Response resp = new Response();
            //resp.responseFrame = new Response_40Frame();
            resp.responseFrame = (CommonResponseFrame)Activator.CreateInstance(HydrasFrameMapper.getInstance().
                getResponseFrame(requestFrame.GetType()));
            return resp;
        }
    }
}
