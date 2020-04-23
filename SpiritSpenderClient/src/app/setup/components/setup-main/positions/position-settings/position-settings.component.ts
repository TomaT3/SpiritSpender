import { Component, OnInit } from '@angular/core';
import { ShotGlassPositionsApiService } from 'src/app/shared/services/shot-glass-positions-api.service';
import { PositionSettting } from 'src/app/shared/types/position-settings';

@Component({
  selector: 'app-position-settings',
  templateUrl: './position-settings.component.html',
  styleUrls: ['./position-settings.component.scss']
})
export class PositionSettingsComponent implements OnInit {

  numberOfPositions: number;
  positionSettings: PositionSettting[];
  constructor(private positionsApiService: ShotGlassPositionsApiService) { }
  
  async ngOnInit(): Promise<void> {
    try{
      await this.getPositionSettings();
    } catch (error) {
      console.error(error);
    }
  }

  // public async updateValues(): Promise<void> {
  //   await this.drivesApiService.updateDriveSettings(this.driveName, this.newSettingValue);
  //   await this.getServerValues();
  // }

  private async getNumberOfPositions(): Promise<void> {
    this.numberOfPositions = await this.positionsApiService.getNumberOfPositions();
  }

  private async getPositionSettings(): Promise<void> {
    this.positionSettings = await this.positionsApiService.getPositionSettings();
  }
}
