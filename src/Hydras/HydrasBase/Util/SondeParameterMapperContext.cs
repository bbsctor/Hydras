using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigFrame.CommunicationService.SerialPortSupport;
using HydrasProtocol.StringMap;

namespace HydrasBase.Util
{
    public class SondeParameterMapperContext
    {
        public static Dictionary<SerialPortModel, SondeParameterMapper> mapperDict;

        static SondeParameterMapperContext()
        {
            mapperDict = new Dictionary<SerialPortModel, SondeParameterMapper>();
        }

        public static void addParameterMapper(SerialPortModel port, SondeParameterMapper mapper)
        {
            mapperDict.Add(port, mapper);
        }

        public static SondeParameterMapper getParameterMapper(SerialPortModel port)
        {
            SondeParameterMapper mapper;
            mapperDict.TryGetValue(port, out mapper);
            if (mapper == null)
            {
                mapper = new SondeParameterMapper();
                mapperDict.Add(port, mapper);
            }
            return mapper;
        }
    }
}
