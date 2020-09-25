using SpiritSpenderServer.Automatic;
using SpiritSpenderServer.Config.HardwareConfiguration;
using SpiritSpenderServer.HardwareControl.EmergencyStop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace SpiritSpenderServer.HardwareControl
{
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

        public StatusObserver(IHardwareConfiguration hardwareConfiguration, IAutomaticMode automatic)
        {
            var statusObservables = new List<IObservable<Status>>();
            statusObservables.Add(hardwareConfiguration.SpiritDispenserControl.GetStatusObservable());
            statusObservables.Add(hardwareConfiguration.StepperDrives["X"].GetStatusObservable());
            statusObservables.Add(hardwareConfiguration.StepperDrives["Y"].GetStatusObservable());
            statusObservables.Add(automatic.GetStatusObservable());
            
            _statusLamp = hardwareConfiguration.StatusLamp;

            statusObservables.CombineLatest(
                (lastStates) =>
                    lastStates switch
                    {
                        _ when lastStates.All(s => s == Status.Ready) => LampStatus.On,
                        _ when lastStates.Any(s => s == Status.Running) => LampStatus.Blinking,
                        _ => LampStatus.Off
                    }
                ).DistinctUntilChanged()
                .Subscribe(lampState => GreenStatusLamp(lampState));

            statusObservables.CombineLatest(
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
            if(enabledStatus)
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
}
