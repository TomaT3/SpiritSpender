using System;
using System.Linq;
using UnitsNet;

namespace SpiritSpenderServer.HardwareControl.StepperDrive
{
    public interface IStepperMotorControl
    {
        Length CurrentPosition { get; }
        void SetPosition(Length position);
        void SetOutput(Duration[] waitTimeBetweenSteps, bool direction, Length distanceToAddForOneStep);
        void ReferenceDrive(Duration waitTimeBetweenSteps, bool direction, Length referencePosition);
    }
}
