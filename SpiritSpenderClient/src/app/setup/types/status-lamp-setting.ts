import { UnitsType } from '../../shared/types/units-type';

export interface StatusLampSetting {
    name: string;
    blinkTimeOn: UnitsType;
    blinkTimeOff: UnitsType;
}

export class StatusLampTexts {
    static readonly blinkTimeOn = "Blink time on";
    static readonly blinkTimeOff = "Blink time off";
}