import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'unitToString'
})
export class UnitToStringPipe implements PipeTransform {

  transform(unit: string): string {
    switch (unit) {
      case "LengthUnit.Millimeter": return "mm";
      case "SpeedUnit.MillimeterPerSecond": return "mm/s";
      case "AccelerationUnit.MillimeterPerSecondSquared": return "mm/s²";
      case "DurationUnit.Second": return "sec";
      default: return "unknown unit";
    }

  }

}
