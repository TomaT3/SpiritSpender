import { Component, OnInit } from '@angular/core';
import { SpiritDispenserApiService } from 'src/app/setup/services/spirit-dispenser-api.service';

@Component({
  selector: 'app-spirit-dispenser-actions',
  styleUrls: ['./spirit-dispenser-actions.component.scss'],
  templateUrl: './spirit-dispenser-actions.component.html',
})
export class SpiritDispenserActionsComponent {

  public isBottleChangeModeActive = false;
  public isDispenserActive = false;

  constructor(private spiritDispenserApi: SpiritDispenserApiService) { }

  public async toggleBottleChangeMode(): Promise<void> {
    this.isDispenserActive = true;
    if (this.isBottleChangeModeActive) {
      await this.spiritDispenserApi.closeSpiritSpender();
    } else {
      await this.spiritDispenserApi.openSpiritSpender();
    }

    this.isBottleChangeModeActive = !this.isBottleChangeModeActive;
    this.isDispenserActive = false;
  }

  public async closeSpiritSpender(): Promise<void> {
    await this.spiritDispenserApi.closeSpiritSpender();
  }

  public async openSpiritSpender(): Promise<void> {
    await this.spiritDispenserApi.openSpiritSpender();
  }

  public async fillGlas(): Promise<void> {
    this.isDispenserActive = true;
    await this.spiritDispenserApi.fillGlas();
    this.isDispenserActive = false;
  }
}
