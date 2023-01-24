namespace SpiritSpenderServer.HardwareControl.StatusLamp;

using SpiritSpenderServer.Helper;
using SpiritSpenderServer.Interface.HardwareControl;
using System.Device.Gpio;
using UnitsNet;

public class Light : ILight
{
    private IGpioPin _gpio;
    private CancellationTokenSource _blinkingTokensource;
    private Task? _blinkingTask;
    private object _lockObject = new object();

    public Light(int gpioPin, IGpioPinFactory gpioPinFactory)
    {
        _blinkingTokensource = new CancellationTokenSource();
        _gpio = gpioPinFactory.CreateGpioPin(gpioPin, PinMode.Output);
        TurnLightOff();
    }

    public void TurnOn()
    {
        lock (_lockObject)
        {
            CheckAndStopBlinkingTask();
            TurnLightOn();
        }
    }

    public void TurnOff()
    {
        lock (_lockObject)
        {
            CheckAndStopBlinkingTask();
            TurnLightOff();
        }
    }

    public void Blink(Duration durationOn, Duration durationOff)
    {
        lock (_lockObject)
        {
            CheckAndStopBlinkingTask();
            _blinkingTask = Task.Run(() => Blinking(Convert.ToInt32(durationOn.Milliseconds), Convert.ToInt32(durationOff.Milliseconds), _blinkingTokensource.Token), _blinkingTokensource.Token);
        }
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
