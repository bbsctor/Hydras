using System;
namespace ConfigFrame.UITask
{
    public interface IBasicTaskGroup
    {
        void finish();
        //void resumeLoop();
        //void runOnce();
        //void runTimes(int times);
        void startLoop();
        void stopLoop();
        //void suspendLoop();
    }
}
