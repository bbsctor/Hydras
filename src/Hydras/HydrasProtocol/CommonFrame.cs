using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtocolFrame.ElementDef.BasicImpl;

namespace HydrasProtocol
{
    public class CommonFrame : BasicFrame, ICommonFrame
    {
        //        帧头	              数据域	        帧尾
        //地址	功能码	数据长度	
        //                           信息数据	CRC校验和	CRC校验和
        //1字节	1字节	1字节		            低字节	    高字节
        public BasicElement addr;
        public BasicElement funcNo;
        public DataLengthElement dataLength;
        public CompositeElement data;
        public CRCElement crc;

        public CommonFrame()
        {
            addr = new BasicElement();
            addr.Name = "addr";
            addr.Size = 1;

            funcNo = new BasicElement();
            funcNo.Name = "funcNo";
            funcNo.Size = 1;

            data = new CompositeElement();
            data.Name = "data";

            dataLength = new DataLengthElement();
            dataLength.Name = "dataLength";
            dataLength.RelateElements = new BasicElement[] { data };
            dataLength.Size = 1;

            crc = new CRCElement();
            crc.Name = "crc";
            crc.RelateElements = new BasicElement[] {
                addr, funcNo, dataLength, data
            };
            crc.Size = 2;

            this.RelateElements = new BasicElement[]{
                addr, funcNo, dataLength, data, crc
            };
        }
    }
}
