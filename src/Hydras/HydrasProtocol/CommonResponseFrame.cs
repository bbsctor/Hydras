using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol
{
    public class CommonResponseFrame:CommonFrame
    {
        public enum TYPE { INIT, NORMAL, ABNORMAL };
        public TYPE type;

        public TYPE Type
        {
            get { return type; }
            set { type = value; }
        }

        public override bool parse()
        {
            preParse();
            return base.parse();
        }

        protected virtual void preParse()
        {
            int dataLen = (int)base.Data[2];
            data.Size = dataLen;
        }

        public List<byte[]> parseDataByEndingChar()
        {
            List<byte[]> result = new List<byte[]>();
            List<byte> temp = null;
            for (int i = 0; i < data.Data.Length; i++)
            {
                if (data.Data[i] != '\0')
                {
                    if (temp == null)
                    {
                        temp = new List<byte>();
                    }
                    temp.Add(data.Data[i]);
                }
                else
                {
                    if (temp != null)
                    {
                        result.Add(temp.ToArray());
                        temp = null;
                    }
                }
            }
            return result;
        }
    }
}
