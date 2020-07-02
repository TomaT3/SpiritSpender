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

        public Light(int gpioPin, IGpioControllerFacade gpioControllerFacade)
        {
            _blinkingTokensource = new CancellationTokenSource();
            _gpio = new GpioPin(gpioControllerFacade, gpioPin, PinMode.Output);
            TurnLightOff();
        }

        public void TurnOn()
        {
            _blinkingTokensource.Cancel();
            TurnLightOn();
        }

        public void TurnOff()
        {
            _blinkingTokensource.Cancel();
            TurnLightOff();
        }

        public void Blink(Duration durationOn, Duration durationOff)
        {
            Task.Run(() => Blinking(Convert.ToInt32(durationOn), Convert.ToInt32(durationOff), _blinkingTokensource.Token), _blinkingTokensource.Token);
        }

        private async Task Blinking(int durationOn, int durationOff, CancellationToken cancellationToken)
        {
            while (true)
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
