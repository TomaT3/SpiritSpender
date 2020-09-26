using System.Threading;
using UnitsNet;

namespace SpiritSpenderServer.HardwareControl.Axis.StepperDrive
{
    public interface IStepperDriveControl
    {
        Length CurrentPosition { get; }
        void SetPosition(Length position);
        void SetOutput(Duration[] waitTimeBetweenSteps, bool direction, Length distanceToAddForOneStep, CancellationToken token);
        void DriveToReferenceSwitch(Duration waitTimeBetweenSteps, bool direction, CancellationToken token);
    }
}
