import { Component, OnInit } from '@angular/core';
import { SpiritDispenserApiService } from 'src/app/setup/services/spirit-dispenser-api.service';

@Component({
  selector: 'app-spirit-dispenser-actions',
  templateUrl: './spirit-dispenser-actions.component.html',
  styleUrls: ['./spirit-dispenser-actions.component.scss']
})
export class SpiritDispenserActionsComponent implements OnInit {

  constructor(private spiritDispenserApi: SpiritDispenserApiService) { }

  ngOnInit(): void {
  }

  public async closeSpiritSpender() : Promise<void> {
    await this.spiritDispenserApi.closeSpiritSpender();
  }

  public async openSpiritSpender() : Promise<void> {
    await this.spiritDispenserApi.openSpiritSpender();
  }

  public async fillGlas() : Promise<void> {
    await this.spiritDispenserApi.fillGlas();
  }
}
