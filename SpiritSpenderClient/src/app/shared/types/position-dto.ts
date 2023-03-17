import { Position } from "./position-settings";
import { LengthUnit } from "./units-type";

export interface PositionDto {
    X: LengthUnit;
    Y: LengthUnit;
}

export class PositionDtoClass implements PositionDto {

    public X: LengthUnit;
    public Y: LengthUnit;

    constructor(x: number, y: number) {
        this.X = new LengthUnit();
        this.Y = new LengthUnit();
        this.X.Value = x ;
        this.Y.Value = y;
    }
}