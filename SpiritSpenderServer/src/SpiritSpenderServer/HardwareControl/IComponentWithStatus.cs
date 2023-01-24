namespace SpiritSpenderServer.HardwareControl;

public interface IComponentWithStatus
{
    IObservable<Status> GetStatusObservable();
}
