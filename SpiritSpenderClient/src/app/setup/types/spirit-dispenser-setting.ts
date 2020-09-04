import { UnitsType } from '../../shared/types/units-type';

export interface SpiritDispenserSetting {
    name: string;
    driveTimeFromBottleChangeToHomePos: UnitsType;
    driveTimeFromHomePosToBottleChange: UnitsType;
    driveTimeFromHomeToReleasePosition: UnitsType;
    driveTimeFromReleaseToHomePosition: UnitsType;
    waitTimeUntilSpiritIsReleased: UnitsType;
    waitTimeUntilSpiritIsRefilled: UnitsType;
}

export class SpiritDispenserTexts {
    static readonly driveTimeFromBottleChangeToHomePos = "Drive time from bottle change to home position";
    static readonly driveTimeFromHomePosToBottleChange = "Drive time from home to bottle change position";
    static readonly driveTimeFromHomeToReleasePosition = "Drive time from home to release position";
    static readonly driveTimeFromReleaseToHomePosition = "Drive time from release to home position";
    static readonly waitTimeUntilSpiritIsReleased = "Timespan to empty spirit dispenser";
    static readonly waitTimeUntilSpiritIsRefilled = "Timespan to refill spirit dispenser";
}