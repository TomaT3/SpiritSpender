using SpiritSpenderServer.Interface.HardwareControl;
using SpiritSpenderServer.Simulation.HardwareControlMock.GpioMocks;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiritSpenderServer.Simulation.HardwareControlMock
{
    public class GpioPinFactoryMock : IGpioPinFactory
    {
        public IGpioPin CreateGpioPin(int pinNumber, PinMode pinMode) => (pinNumber, pinMode) switch
        {
            (_, PinMode.Output) => new GpioPinOutputModeMock(),
            (12, _) => new GpioPinInputModeMock(PinValue.High),  // E-Stop,
            (20, _) => new GpioPinInputModeMock(PinValue.High),  // Reference switch X-Axis,
            (21, _) => new GpioPinInputModeMock(PinValue.High),  // Reference switch Y-Axis
            (_, PinMode.Input) => new GpioPinInputModeMock(PinValue.Low),
            _ => null
        };
        
    }
}
