namespace SpiritSpenderServer.HardwareControl.Axis.StepperDrive;

using System;
using UnitsNet;

public interface IStepperDriveControl
{
    event Action<Length> PositionChanged;
    Length CurrentPosition { get; }
    void SetPosition(Length position);
    void SetOutput(Duration[] waitTimeBetweenSteps, bool direction, Length distanceToAddForOneStep, CancellationToken token);
    void DriveToReferenceSwitch(Duration waitTimeBetweenSteps, bool direction, CancellationToken token);
}
