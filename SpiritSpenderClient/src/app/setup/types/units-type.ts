export interface UnitsType {
    Unit: string;
    Value: number;
}

export class LengthUnit implements UnitsType{
    Unit: string = "LengthUnit.Millimeter";
    Value: number = 0;
}