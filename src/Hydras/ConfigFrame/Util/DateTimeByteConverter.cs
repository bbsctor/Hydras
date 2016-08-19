using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigFrame.Util
{
    public class DateTimeByteConverter
    {
        public static DateTime getDateTime(byte[] secondsBytes)
        {
            uint seconds;
            DateTime dateTime;
            byte[] temp = new byte[4];
            System.Array.Copy(secondsBytes, temp, 4);
            seconds = BitConverter.ToUInt32(temp, 0);
            uint day = seconds / (60 * 60 * 24);
            uint hour = seconds % (60 * 60 * 24) / 3600;
            uint minute = seconds % (60 * 60) / 60;
            uint second = seconds % 60;
            dateTime = new DateTime(1970, 1, 1) + new TimeSpan((int)day, (int)hour, (int)minute, (int)second);
            return dateTime;
        }

        public static byte[] getSecondsByte(DateTime dt)
        {
            TimeSpan tspan = dt - new DateTime(1970, 1, 1);
            uint seconds = (uint)(tspan.Days * (60 * 60 * 24) + tspan.Hours * 3600 + tspan.Minutes * 60 + tspan.Seconds);

            return BitConverter.GetBytes(seconds);
        }
    }
}
