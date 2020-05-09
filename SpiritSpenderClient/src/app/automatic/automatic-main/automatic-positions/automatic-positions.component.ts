import { Component, OnInit } from '@angular/core';
import { ShotGlassPositionsApiService } from 'src/app/shared/services/shot-glass-positions-api.service';
import { PositionSettting } from 'src/app/shared/types/position-settings';

@Component({
  selector: 'app-automatic-positions',
  templateUrl: './automatic-positions.component.html',
  styleUrls: ['./automatic-positions.component.scss']
})
export class AutomaticPositionsComponent implements OnInit {

  public positionSettings: PositionSettting[]
  
  constructor(private positionsApiService: ShotGlassPositionsApiService) { }
  
  async ngOnInit(): Promise<void> {
    try{
      await this.getPositionSettings();
    } catch (error) {
      console.error(error);
    }
  }

  public isPositionLocked(positionNumber: number): boolean {
    return false;
  }
  
  public async clearPositions(): Promise<void> {
    this.positionSettings = await this.positionsApiService.clearPositions();
  }

  private async getPositionSettings(): Promise<void> {
    this.positionSettings = await this.positionsApiService.getPositionSettings();
  } 
}
