import { Component, OnInit, ViewChild } from '@angular/core';
import { AutomaticApiService } from '../services/automatic-api.service';
import { AutomaticPositionsComponent } from './automatic-positions/automatic-positions.component';

@Component({
  selector: 'app-automatic-main',
  templateUrl: './automatic-main.component.html',
  styleUrls: ['./automatic-main.component.scss']
})
export class AutomaticMainComponent implements OnInit {

  @ViewChild(AutomaticPositionsComponent)
  private automaticPositions: AutomaticPositionsComponent;

  constructor(private automaticApiService: AutomaticApiService) { }

  ngOnInit(): void {
  }

  public async releaseTheSpirit(): Promise<void> {
    await this.automaticApiService.releaseTheSpirit();
  }

  public async clearSpiritPositions(): Promise<void> {
    return this.automaticPositions.clearPositions();
  }
}
