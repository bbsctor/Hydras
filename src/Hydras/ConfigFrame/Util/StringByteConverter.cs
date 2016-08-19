using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace ConfigFrame.Util
{
    public class StringByteConverter 
    {
        public static byte[] stringToByteArray(string str)
        {
            if (str != null)
            {
                char[] chars = str.ToCharArray();
                byte[] bytes = new byte[chars.Length];
                for (int i = 0; i < chars.Length; i++)
                {
                    bytes[i] = System.Convert.ToByte(chars[i]);
                }
                return bytes;
            }
            return null;
        }

        public static string byteArrayToString(byte[] bytes)
        {
            if (bytes != null)
            {
                List<char> chars = new List<char>(); ;
                for (int i = 0; i < bytes.Length; i++)
                {
                    if (bytes[i] == '\0')
                    {
                        break;
                    }
                    if (bytes[i] < 0x80)
                    {
                        chars.Add(BitConverter.ToChar(new byte[] { bytes[i], 0x00 }, 0));
                    }
                    
                }
                for (int i = chars.Count; i > 1; i--)
                {
                    if (chars[i - 1] == 0x00)
                    {
                        chars.RemoveAt(i - 1);
                    }
                    else
                    {
                        break;
                    }
                }

                    return new string(chars.ToArray());
            }
            return null;
        }

        public static byte[] parseByteArrayString(string byteStr)
        {
            List<byte> result = new List<byte>();
            string[] tempStrArray = byteStr.Split('-');
            byte temp;
            for (int i = 0; i < tempStrArray.Length; i++)
            {
                byte.TryParse(tempStrArray[i],NumberStyles.HexNumber, null, out temp);
                result.Add(temp);
            }

            return result.ToArray();
        }
    }
}
