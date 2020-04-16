import { UnitsType } from './units-type';

export interface DriveSetting {
    driveName: string;
    stepsPerRevolution: number;
    spindlePitch: UnitsType;
    maxSpeed: UnitsType;
    acceleration: UnitsType;
    softwareLimitMinus: UnitsType;
    softwareLimitPlus: UnitsType;
    reverseDirection: boolean;
    referencePosition: UnitsType;
    referenceDrivingDirection: DrivingDirection;
    referenceDrivingSpeed: UnitsType;
}

export enum DrivingDirection{
    positive = 0,
    negative = 1
}
export class DriveSettingTexts {
    static readonly driveName = "Drive name";
    static readonly stepsPerRevolution = "Steps per Revolution";
    static readonly spindlePitch = "Spindle pitch";
    static readonly maxSpeed = "Max. speed";
    static readonly acceleration = "Acceleration";
    static readonly softwareLimitMinus = "Software limit -"
    static readonly softwareLimitPlus ="Software limit +"
    static readonly reverseDirection = "Reverse direction";
    static readonly referencePosition = "Reference position";
    static readonly referenceDrivingDirection = "Reference driving direction";
    static readonly referenceDrivingSpeed = "Reference driving speed";
}