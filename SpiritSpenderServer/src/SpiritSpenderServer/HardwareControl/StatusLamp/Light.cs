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
        private object _lockObject = new Object();

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
            Task.Run(() => Blinking(Convert.ToInt32(durationOn.Milliseconds), Convert.ToInt32(durationOff.Milliseconds), _blinkingTokensource.Token), _blinkingTokensource.Token);
        }

        private void CheckAndStopBlinkingTask()
        {
            lock (_lockObject)
            {
                if (_blinkingTask != null)
                {
                    _blinkingTokensource.Cancel();
                    _blinkingTask.Dispose();
                    _blinkingTokensource = new CancellationTokenSource();
                }
            }
        }

        private async Task Blinking(int durationOn, int durationOff, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (cancellationToken.IsCancellationRequested) return;
                TurnLightOn();
                await Task.Delay(durationOn, cancellationToken);

                if (cancellationToken.IsCancellationRequested) return;
                TurnLightOff();
                await Task.Delay(durationOff, cancellationToken);
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
