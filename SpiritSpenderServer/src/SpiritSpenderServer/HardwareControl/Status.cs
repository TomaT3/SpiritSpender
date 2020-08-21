using Microsoft.AspNetCore.Diagnostics;

namespace SpiritSpenderServer.HardwareControl
{
    public enum Status
    {
        Error,
        NotReady,
        Ready,
        Running
    }
}