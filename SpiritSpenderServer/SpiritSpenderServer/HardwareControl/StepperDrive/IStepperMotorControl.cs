﻿using System;
using System.Linq;
using UnitsNet;

namespace SpiritSpenderServer.HardwareControl.StepperDrive
{
    public interface IStepperMotorControl
    {
        Length CurrentPosition { get; }
        void SetPosition(Length position);
        void SetOutput(double[] waitTimeBetweenSteps, bool direction, Length distanceToAddForOneStep);
    }
}