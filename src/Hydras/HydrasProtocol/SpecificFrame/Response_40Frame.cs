using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtocolFrame.ElementDef.BasicImpl;

namespace HydrasProtocol.SpecificFrame
{
    public class Response_40Frame : CommonResponseFrame
    {
        //public enum TYPE { INIT, NORMAL };
        //private TYPE type;

        //public TYPE Type
        //{
        //    get { return type; }
        //    set { type = value; }
        //}

        //17 Byte	9 Byte	17 Byte	        6 Byte	6 Byte
        //Hydrolab	66256	HYDROLAB DS5X	5.44	1.16

        public BasicElement manufacturer;
        public BasicElement manufactureDate;
        //public BasicElement unknown;
        public BasicElement serialNum;
        public BasicElement model;
        public BasicElement softwareVersion;
        public BasicElement modbusVersion;

        public Response_40Frame()
        {
            manufacturer = new BasicElement();
            manufacturer.Name = "manufacture";
            manufacturer.Size = 13;

            manufactureDate = new BasicElement();
            manufactureDate.Name = "manufactureDate";
            manufactureDate.Size = 4;

            //unknown = new BasicElement();
            //unknown.Name = "unknown";
            //unknown.Size = 1;

            serialNum = new BasicElement();
            serialNum.Name = "serialNum";
            serialNum.Size = 9;

            model = new BasicElement();
            model.Name = "model";
            model.Size = 17;

            softwareVersion = new BasicElement();
            softwareVersion.Name = "softwareVersion";
            softwareVersion.Size = 6;

            modbusVersion = new BasicElement();
            modbusVersion.Name = "modbusVersion";
            modbusVersion.Size = 6;
            
        }

        public override bool parse()
        {
            if (this.Data.Length == 2)
            {
                this.type = TYPE.ABNORMAL;
                base.data.RelateElements = null;
                return true;
            }
            else if (this.Data.Length == 4)
            {
                this.type = TYPE.INIT;
                base.data.RelateElements = null;
                return true;
            }
            else
            {
                this.type = TYPE.NORMAL;
                base.data.RelateElements = new BasicElement[]{
                manufacturer,
                manufactureDate,
                //unknown,
                serialNum,
                model,
                softwareVersion,
                modbusVersion
                };
            }
            return base.parse();
        }
    }
}
