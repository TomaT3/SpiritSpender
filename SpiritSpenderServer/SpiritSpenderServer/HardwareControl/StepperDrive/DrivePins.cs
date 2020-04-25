﻿using System;

namespace SpiritSpenderServer.HardwareControl.StepperDrive
{
    public class DrivePins
    {
        public int EnablePin { get; set; }
        public int DirectionPin { get; set; }
        public int StepPin { get; set; }
        public int ReferenceSwitchPin { get; set; }
    }
}