import { UnitsType } from './units-type';

export interface DriveSetting {
    driveName: string;
    stepsPerRevolution: number;
    spindelPitch: UnitsType;
    maxSpeed: UnitsType;
    acceleration: UnitsType;
    reverseDirection: boolean;

}