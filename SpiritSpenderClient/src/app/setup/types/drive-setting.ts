import { Speed, Length, Acceleration } from 'unitsnet-js'

export interface DriveSetting {
    driveName: string;
    stepsPerRevolution: number;
    spindelPitch: Length;
    maxSpeed: Speed;
    acceleration: Acceleration;
    reverseDirection: boolean;

}