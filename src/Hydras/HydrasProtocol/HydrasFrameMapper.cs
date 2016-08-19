using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtocolFrame.Util;
using HydrasProtocol.SpecificFrame;

namespace HydrasProtocol
{
    public class HydrasFrameMapper:FrameMapper
    {
        private static HydrasFrameMapper instance = null;

        public static HydrasFrameMapper getInstance()
        {
            if (instance == null)
            {
                instance = new HydrasFrameMapper();
            }
            return instance;
        }

        private HydrasFrameMapper()
        {
            base.add(typeof(Request_40Frame), typeof(Response_40Frame));
            base.add(typeof(Request_41Frame), typeof(Response_41Frame));
            base.add(typeof(Request_42Frame), typeof(Response_42Frame));
            base.add(typeof(Request_43Frame), typeof(Response_43Frame));
            base.add(typeof(Request_44Frame), typeof(Response_44Frame));
            base.add(typeof(Request_46Frame), typeof(Response_46Frame));
            base.add(typeof(Request_47Frame), typeof(Response_47Frame));
            base.add(typeof(Request_48Frame), typeof(Response_48Frame));
            base.add(typeof(Request_49BaseFrame), typeof(Response_49BaseFrame));
            base.add(typeof(Request_49DetailFrame), typeof(Response_49DetailFrame));
            base.add(typeof(Request_4AFrame), typeof(Response_4AFrame));
            base.add(typeof(Request_4BAllFrame), typeof(Response_4BAllFrame));
            base.add(typeof(Request_4BDetailFrame), typeof(Response_4BDetailFrame));
            base.add(typeof(Request_4CFrame), typeof(Response_4CFrame));
            base.add(typeof(Request_4DFrame), typeof(Response_4DFrame));
            base.add(typeof(Request_4EQueryDeviceStorageFrame), typeof(Response_4EQueryDeviceStorageFrame));
            base.add(typeof(Request_4EQueryLogInfoFrame), typeof(Response_4EQueryLogInfoFrame));
            base.add(typeof(Request_4FSettingFieldFrame), typeof(Response_4FSettingFieldFrame));
            base.add(typeof(Request_4FSelectedParaFrame), typeof(Response_4FSelectedParaFrame));
            base.add(typeof(Request_50AbleFrame), typeof(Response_50AbleFrame));
            base.add(typeof(Request_50FileNameFrame), typeof(Response_50FileNameFrame));
            base.add(typeof(Request_50ParameterFrame), typeof(Response_50ParameterFrame));
            base.add(typeof(Request_50SettingFieldFrame), typeof(Response_50SettingFieldFrame));

            base.add(typeof(Request_51DownloadLogFrame), typeof(Response_51DownloadLogFrame));
            base.add(typeof(Request_51QueryParaFrame), typeof(Response_51QueryParaFrame));
            base.add(typeof(Request_52Frame), typeof(Response_52Frame));
            base.add(typeof(Request_67QueryingFrame), typeof(Response_67QueryingFrame));
            base.add(typeof(Request_67SettingFrame), typeof(Response_67SettingFrame));
            base.add(typeof(Request_68Frame), typeof(Response_68Frame));
            base.add(typeof(Request_69Frame), typeof(Response_69Frame));
            base.add(typeof(Request_6BFrame), typeof(Response_6BFrame));
        }
    }
}
