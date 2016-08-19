using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrasProtocol.SpecificFrame
{
    public class Request_51DownloadLogFrame : CommonRequestFrame
    {
        public Request_51DownloadLogFrame(byte logNum, byte sn)
        {

            //01 51 06 00 02 00 00 00 80 dd f0  下载第二个文件，全范围
            //01 51 06 00 02 01 00 00 80 dc 0c  下载第二个文件，选定一定范围
            //01 51 06 00 03 00 00 00 80 e0 30  下载第 三个文件，全范围
            //01 51 06 00 03 01 00 00 80 e1 cc  下载第 三个文件，全范围,第1帧
            //01 51 06 00 03 02 00 00 80 e1 88  下载第 三个文件，全范围,第2帧
            //01 51 06 00 03 03 00 00 80 80 74  下载第 三个文件，全范围,第3帧

            preBuild(FrameContext.slaveAddr, 0x51, new byte[]{0x00, logNum, sn, 0x00, 0x00, 0x80});
            build();
        }
    }
}