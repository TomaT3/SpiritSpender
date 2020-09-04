import { Component, OnInit } from '@angular/core';
import { SpiritDispenserApiService } from 'src/app/setup/services/spirit-dispenser-api.service';

@Component({
  selector: 'app-spirit-dispenser-actions',
  styleUrls: ['./spirit-dispenser-actions.component.scss'],
  templateUrl: './spirit-dispenser-actions.component.html',
})
export class SpiritDispenserActionsComponent {

  constructor(private spiritDispenserApi: SpiritDispenserApiService) { }

  public async referenceDrive(): Promise<void> {
    await this.spiritDispenserApi.referenceDrive();
  }

  public async goToBottleChangePosition(): Promise<void> {
    await this.spiritDispenserApi.goToBottleChangePosition();
  }

  public async goToHomePosition(): Promise<void> {
    await this.spiritDispenserApi.goToHomeposition();
  }

  public async goToReleasePosition(): Promise<void> {
    await this.spiritDispenserApi.goToReleasePosition();
  }

  public async fillGlas(): Promise<void> {
    await this.spiritDispenserApi.fillGlas();
  }
}
