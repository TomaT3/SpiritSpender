import { UnitsType } from './units-type';

export interface PositionSettting {
    number: number;
    position: Position;
    quantity: Quantity;
}

export interface Position {
    x: UnitsType;
    y: UnitsType;
}

export enum Quantity{
    empty = 0,
    oneShot = 1,
    doubleShot = 2,
}

