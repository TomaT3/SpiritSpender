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

        public StatusObserver(IEnumerable<IStatus> components, IHardwareConfiguration hardwareConfiguration)
        {
            _statusLamp = hardwareConfiguration.StatusLamp;
            var statusObservables = components.Select(x => x.GetStatusObservable()).ToList();

            // CombineLatest is used to get an observable that orchestrates multiple other observables.
            // It fires when any of the underlying observables fire (and the predicate is satisfied as well).

            // DistinctUntilChanged filters out observed values that are equal to their predecessors.

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
        }

        private void GreenStatusLamp(LampStatus lampStatus)
        {
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
