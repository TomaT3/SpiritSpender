using SpiritSpenderServer.Helper;
using System;
using System.Device.Gpio;
using System.Threading;
using System.Threading.Tasks;
using UnitsNet;

namespace SpiritSpenderServer.HardwareControl.StatusLamp
{
    public class Light : ILight
    {
        private GpioPin _gpio;
        private CancellationTokenSource _blinkingTokensource;
        private Task _blinkingTask;

        public Light(int gpioPin, IGpioControllerFacade gpioControllerFacade)
        {
            _blinkingTokensource = new CancellationTokenSource();
            _gpio = new GpioPin(gpioControllerFacade, gpioPin, PinMode.Output);
            TurnLightOff();
        }

        public void TurnOn()
        {
            CheckAndStopBlinkingTask();
            TurnLightOn();
        }

        public void TurnOff()
        {
            CheckAndStopBlinkingTask();
            TurnLightOff();
        }

        public void Blink(Duration durationOn, Duration durationOff)
        {
            CheckAndStopBlinkingTask();
            _blinkingTask = Task.Run(() => Blinking(Convert.ToInt32(durationOn.Milliseconds), Convert.ToInt32(durationOff.Milliseconds), _blinkingTokensource.Token), _blinkingTokensource.Token);
        }

        private void CheckAndStopBlinkingTask()
        {
            if (_blinkingTask != null)
            {
                _blinkingTokensource.Cancel();
                _blinkingTokensource = new CancellationTokenSource();
            }
        }

        private async Task Blinking(int durationOn, int durationOff, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (cancellationToken.IsCancellationRequested) return;
                TurnLightOn();
                await durationOn.DelayExceptionFree(cancellationToken);

                if (cancellationToken.IsCancellationRequested) return;
                TurnLightOff();
                await durationOff.DelayExceptionFree(cancellationToken);
            }
        }

        private void TurnLightOn()
        {
            _gpio.Write(PinValue.High);
        }

        private void TurnLightOff()
        {
            _gpio.Write(PinValue.Low);
        }
    }
}
