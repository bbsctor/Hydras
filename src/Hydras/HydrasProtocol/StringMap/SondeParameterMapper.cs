using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.StringMap
{
    public class SondeParameterMapper
    {

        public List<Element> elementList;

        public class Element
        {
            public byte serialNum;
            public byte code;
            public string value;
            public Element(byte sn, byte code, string str)
            {
                this.serialNum = sn;
                this.code = code;
                this.value = str;
            }
        }

        //private static SondeParameterMapper instance = null;

        //public static SondeParameterMapper getInstance()
        //{
        //    if (instance == null)
        //    {
        //        instance = new SondeParameterMapper();
        //    }
        //    return instance;
        //}

        public SondeParameterMapper()
        {
            elementList = new List<Element>();
        }

        public void add(byte sn,byte code, string str)
        {
            elementList.Add(new Element(sn, code, str));
        }

        public string getStringByCode(byte code)
        {
            string temp = null;
            for (int i = 0; i < elementList.Count; i++)
            {
                if (elementList[i].code == code)
                {
                    temp = elementList[i].value;
                    break;
                }
            }
            return temp;
        }

        public byte getCodeByString(string str)
        {
            byte temp = 0x00;
            for (int i = 0; i < elementList.Count; i++)
            {
                if (elementList[i].value.Equals(str))
                {
                    temp = elementList[i].code;
                    break;
                }
            }
            return temp;
        }

        public byte getSerialNumByCode(byte code)
        {
            byte temp = 0x00;
            for (int i = 0; i < elementList.Count; i++)
            {
                if (elementList[i].code.Equals(code))
                {
                    temp = elementList[i].serialNum;
                    break;
                }
            }
            return temp;
        }

        public byte getCodeBySerialNum(byte serialNum)
        {
            byte temp = 0x00;
            for (int i = 0; i < elementList.Count; i++)
            {
                if (elementList[i].serialNum.Equals(serialNum))
                {
                    temp = elementList[i].code;
                    break;
                }
            }
            return temp;
        }

        public byte getSerialNumByString(string str)
        {
            byte temp = 0x00;
            for (int i = 0; i < elementList.Count; i++)
            {
                if (elementList[i].value.Equals(str))
                {
                    temp = elementList[i].serialNum;
                    break;
                }
            }
            return temp;
        }

        public void clear()
        {
            elementList.Clear();
        }
    }
}
