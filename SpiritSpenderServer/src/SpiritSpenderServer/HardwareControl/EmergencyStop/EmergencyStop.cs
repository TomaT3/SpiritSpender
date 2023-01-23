using SpiritSpenderServer.Interface.HardwareControl;
using System;
using System.Device.Gpio;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace SpiritSpenderServer.HardwareControl.EmergencyStop
{
    public class EmergencyStop : IEmergencyStop
    {
        private IGpioPin _emergencyStopReleased = null!;
        private BehaviorSubject<PinValue> _emergencyStop = null!;
        private TimeSpan _debounceTime;
        private IDisposable? _emergencyStopSubscription;

        public event Action<bool>? EmergencyStopPressedChanged;

        public bool EmergencyStopPressed { get; private set; }

        public EmergencyStop(IGpioPinFactory gpioPinFactory)
            => InitGpio(emergencyStopGpioPin: 12, gpioPinFactory);

        public void SetEmergencyStop(bool isEmergencyStopPressed)
        {
            EmergencyStopReleased_ValueChanged(isEmergencyStopPressed ? PinValue.Low : PinValue.High);
        }

        public void SetDebounceTime(int debounceTime)
        {
            SubscribeToEmergencyStopChanges(debounceTime);           
        }

        private void InitGpio(int emergencyStopGpioPin, IGpioPinFactory gpioPinFactory)
        {
            _emergencyStopReleased = gpioPinFactory.CreateGpioPin(emergencyStopGpioPin, PinMode.Input);
            _emergencyStop = new BehaviorSubject<PinValue>(_emergencyStopReleased.Read());
            SubscribeToEmergencyStopChanges(200);

            _emergencyStopReleased.ValueChanged += EmergencyStopReleased_ValueChanged;
        }

        private void SubscribeToEmergencyStopChanges(int debounceTime)
        {
            if(_emergencyStopSubscription != null)
            {
                _emergencyStopSubscription.Dispose();
            }

            _debounceTime = TimeSpan.FromMilliseconds(debounceTime);
            _emergencyStopSubscription = _emergencyStop
                .Throttle(_debounceTime)
                .DistinctUntilChanged()
                .Subscribe(p => EmergencyStopReleased_DebouncedValueChanged(p));
        }

        private void EmergencyStopReleased_ValueChanged(PinValue pinValue)
        {
            _emergencyStop.OnNext(pinValue);
        }

        private void EmergencyStopReleased_DebouncedValueChanged(PinValue pinValue)
        {
            EmergencyStopPressed = pinValue == PinValue.Low;
            EmergencyStopPressedChanged?.Invoke(EmergencyStopPressed);
        }
    }
}
