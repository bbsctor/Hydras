using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProtocolFrame.Util
{
    public class FrameMapper
    {
        private Dictionary<Type, Type> mapper;

        public FrameMapper()
        {
            mapper = new Dictionary<Type, Type>();
        }

        public void add(Type reqType, Type respType)
        {
            mapper.Add(reqType, respType);
        }

        public Type getResponseFrame(Type reqFrame)
        {
            Type respFrame = null;
            mapper.TryGetValue(reqFrame, out respFrame);

            return respFrame;
        }
    }
}
