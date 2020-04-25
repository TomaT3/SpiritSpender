import { Component, OnInit, Input } from '@angular/core';
import { Quantity } from 'src/app/shared/types/position-settings';
import { ShotGlassPositionsApiService } from 'src/app/shared/services/shot-glass-positions-api.service';

@Component({
  selector: 'app-position-quantity',
  templateUrl: './position-quantity.component.html',
  styleUrls: ['./position-quantity.component.scss']
})
export class PositionQuantityComponent implements OnInit {
  @Input() positionNumber: number;
  @Input() quantity: Quantity;

  constructor(private positionsApiService: ShotGlassPositionsApiService) { }

  ngOnInit(): void {
  }

  public getQuantity() : string{
    switch(this.quantity)
    {
      case Quantity.empty:
        return "empty";
      case Quantity.oneShot:
        return "one";
      case Quantity.doubleShot:
        return "two";
    }
  }

  public async changeQuantity(): Promise<void>{
    this.toggleQuantity();
    await this.positionsApiService.updateQuantity(this.positionNumber, this.quantity);
    this.quantity = await this.positionsApiService.getQuantity(this.positionNumber);
  }

  private toggleQuantity() : void{
    switch(this.quantity)
    {
      case Quantity.empty:
        this.quantity = Quantity.oneShot;
        break;
      case Quantity.oneShot:
        this.quantity = Quantity.doubleShot;
        break;
      case Quantity.doubleShot:
        this.quantity = Quantity.empty;
        break;
    }
  }

}