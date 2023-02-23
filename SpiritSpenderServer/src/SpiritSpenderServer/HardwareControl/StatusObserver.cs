namespace SpiritSpenderServer.HardwareControl;

using SpiritSpenderServer.Automatic;
using SpiritSpenderServer.HardwareControl.SpiritSpenderControl;
using SpiritSpenderServer.HardwareControl.StatusLamp;
using System.Reactive.Linq;
using NC_Communication;

public class StatusObserver
{
    private enum LampStatus
    {
        On,
        Blinking,
        Off
    }

    private IStatusLamp _statusLamp;
    private LampStatus _greenLampLastStatus;
    private LampStatus _redLampLastStatus;
    private List<IObservable<Status>> _statusObservables;

    public StatusObserver(
        IAutomaticMode automatic,
        ISpiritDispenserControl spiritDispenserControl,
        INcCommunication ncCommunication,
        IStatusLamp statusLamp)
    {
        _statusObservables = new List<IObservable<Status>>();
        _statusObservables.Add(spiritDispenserControl.GetStatusObservable());
        _statusObservables.Add(ncCommunication.GetStatusObservable());
        _statusObservables.Add(automatic.GetStatusObservable());

        _statusLamp = statusLamp;
    }

    public void Init()
    {
        _statusObservables.CombineLatest(
            (lastStates) =>
                lastStates switch
                {
                    _ when lastStates.All(s => s == Status.Ready) => LampStatus.On,
                    _ when lastStates.Any(s => s == Status.Running) => LampStatus.Blinking,
                    _ => LampStatus.Off
                }
            ).DistinctUntilChanged()
            .Subscribe(lampState => GreenStatusLamp(lampState));

        _statusObservables.CombineLatest(
            (lastStates) =>
                lastStates switch
                {
                    _ when lastStates.Any(s => s == Status.Error) => LampStatus.On,
                    _ when lastStates.Any(s => s == Status.NotReady) => LampStatus.Blinking,
                    _ => LampStatus.Off
                }
            ).DistinctUntilChanged()
            .Subscribe(lampState => RedStatusLamp(lampState));

        _statusLamp.EnabledChanged += StatusLampEnabledChanged;
    }

    private void StatusLampEnabledChanged(bool enabledStatus)
    {
        if (enabledStatus)
        {
            GreenStatusLamp(_greenLampLastStatus);
            RedStatusLamp(_redLampLastStatus);
        }
    }

    private void GreenStatusLamp(LampStatus lampStatus)
    {
        _greenLampLastStatus = lampStatus;
        switch (lampStatus)
        {
            case LampStatus.On:
                _statusLamp.GreenLightOn();
                break;
            case LampStatus.Blinking:
                _statusLamp.GreenLightBlink();
                break;
            case LampStatus.Off:
                _statusLamp.GreenLightOff();
                break;
        }
    }

    private void RedStatusLamp(LampStatus lampStatus)
    {
        _redLampLastStatus = lampStatus;
        switch (lampStatus)
        {
            case LampStatus.On:
                _statusLamp.RedLightOn();
                break;
            case LampStatus.Blinking:
                _statusLamp.RedLightBlink();
                break;
            case LampStatus.Off:
                _statusLamp.RedLightOff();
                break;
        }
    }
}
