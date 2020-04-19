import { UnitsType } from './units-type';

export interface SpiritDispenserSetting {
    name: string;
    driveTimeToReleaseTheSpirit: UnitsType;
    driveTimeToCloseTheSpiritSpender: UnitsType;
    waitTimeUntilSpiritIsReleased: UnitsType;
}

export class SpiritDispenserTexts {
    static readonly driveTimeToReleaseTheSpirit = "Drive down timespan";
    static readonly driveTimeToCloseTheSpiritSpender = "Drive up timespan";
    static readonly waitTimeUntilSpiritIsReleased = "Timespan to empty spirit dispenser";
}