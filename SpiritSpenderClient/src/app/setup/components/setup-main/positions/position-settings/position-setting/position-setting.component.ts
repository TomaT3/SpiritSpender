import { Component, OnInit, Input } from '@angular/core';
import { Quantity, Position, PositionSettting } from 'src/app/shared/types/position-settings';
import { ShotGlassPositionsApiService } from 'src/app/shared/services/shot-glass-positions-api.service';
import { CopyHelper } from 'src/app/shared/Helpers/CopyHelper';

@Component({
  selector: 'app-position-setting',
  templateUrl: './position-setting.component.html',
  styleUrls: ['./position-setting.component.scss']
})
export class PositionSettingComponent implements OnInit {
  @Input() positionNumber: number;
  @Input() position: Position;

  newPosition: Position;

  constructor(private positionsApiService: ShotGlassPositionsApiService) { }

  ngOnInit(): void {
    this.newPosition = <Position>CopyHelper.deepCopy(this.position);
  }

  public async setPositionValue() : Promise<void> {
    await this.positionsApiService.updatePosition(this.positionNumber, this.newPosition);
    await this.getServerValues();
  }

  public async driveToPosition(): Promise<void> {
    await this.positionsApiService.driveToPosition(this.positionNumber);
  }

  private async getServerValues(): Promise<void> {
    this.position = await this.positionsApiService.getPosition(this.positionNumber);
    this.newPosition = <Position>CopyHelper.deepCopy(this.position);
  }
}
