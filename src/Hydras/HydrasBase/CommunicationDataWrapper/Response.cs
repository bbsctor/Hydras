using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.CommunicationDataWrapper;
using ConfigFrame.CommunicationDataWrapper.Complex;
using HydrasProtocol;
using HydrasProtocol.ExceptionDefine;

namespace HydrasBase.CommunicationDataWrapper
{
    public class Response : IComplexResponse
    {
        public CommonResponseFrame responseFrame;

        private IRequest request;

        public IRequest Request
        {
            get { return request; }
            set { request = value; }
        }

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

        public void parse(byte[] data)
        {
            
            responseFrame.Data = data;
            if (validate() == false)
            {
                throw new AbnormalResponseException();
            }
            responseFrame.parse();
        }

        public void parseData()
        {
            this.data =  responseFrame.data.Data;
        }

        public bool validate()
        {
            try
            {
                if (((Request)request).requestFrame.Data[1] == responseFrame.Data[1])
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new AbnormalResponseException();
            }
        }
    }
}
