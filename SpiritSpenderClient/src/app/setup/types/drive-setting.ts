import { UnitsType } from './units-type';

export interface DriveSetting {
    driveName: string;
    stepsPerRevolution: number;
    spindelPitch: UnitsType;
    maxSpeed: UnitsType;
    acceleration: UnitsType;
    reverseDirection: boolean;
}

export class DriveSettingTexts {
    static readonly driveName = "Drive name";
    static readonly stepsPerRevolution = "Steps per Revolution";
    static readonly spindlePitch = "Spindle pitch";
    static readonly maxSpeed = "Max. speed";
    static readonly acceleration = "Acceleration";
    static readonly reverseDirection = "Reverse direction";
}