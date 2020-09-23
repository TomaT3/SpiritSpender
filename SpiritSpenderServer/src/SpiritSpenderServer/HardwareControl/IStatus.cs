using System;

namespace SpiritSpenderServer.HardwareControl
{
    public interface IStatus
    {
        IObservable<Status> GetStatusObservable();
    }
}
