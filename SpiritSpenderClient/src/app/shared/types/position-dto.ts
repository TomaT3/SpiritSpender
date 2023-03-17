import { Position } from "./position-settings";
import { LengthUnit } from "./units-type";

export interface PositionDto {
    X: LengthUnit;
    Y: LengthUnit;
}

export class EmptyPositionDto implements PositionDto {
    
    constructor() {
        this.X = new LengthUnit();
        this.Y = new LengthUnit();
        this.X.Value = 0;
        this.Y.Value = 0;
    }

    Y: LengthUnit;
    X: LengthUnit;
}