import { UnitsType } from '../../shared/types/units-type';

export interface SpiritDispenserSetting {
    name: string;
    driveTimeToReleaseTheSpirit: UnitsType;
    driveTimeToCloseTheSpiritSpender: UnitsType;
    waitTimeUntilSpiritIsReleased: UnitsType;
}

export class SpiritDispenserTexts {
    static readonly driveTimeToReleaseTheSpirit = "Drive time to open spirit dispenser";
    static readonly driveTimeToCloseTheSpiritSpender = "Drive time to close spirit dispenser";
    static readonly waitTimeUntilSpiritIsReleased = "Timespan to empty spirit dispenser";
}