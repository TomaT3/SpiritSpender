import { Component, OnInit, ViewChild } from '@angular/core';
import { AxisSignalRService } from 'src/app/shared/services/signal-r/axis-signal-r.service';
import { PositionDto, PositionDtoClass } from 'src/app/shared/types/position-dto';
import { LengthUnit } from 'src/app/shared/types/units-type';
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

  constructor(private automaticApiService: AutomaticApiService, private axisSignalRservice: AxisSignalRService) { }

  public currentPosition: PositionDto = new PositionDtoClass(23.4566, 11.9968)

  ngOnInit(): void {
    this.axisSignalRservice.positionChanged.subscribe((position: PositionDto) => this.positionChangedHandler(position));
  }

  ngOnDestroy() {
    this.axisSignalRservice?.positionChanged.unsubscribe();
  }

  private positionChangedHandler(position: PositionDto){
      this.currentPosition = position
  }

  public async releaseTheSpirit(): Promise<void> {
    await this.automaticApiService.releaseTheSpirit();
  }

  public async clearSpiritPositions(): Promise<void> {
    return this.automaticPositions.clearPositions();
  }

}
