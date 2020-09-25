import { Component, OnInit } from '@angular/core';
import { StatusLampApiService } from 'src/app/setup/services/status-lamp-api.service';

@Component({
  selector: 'app-status-lamp-actions',
  templateUrl: './status-lamp-actions.component.html',
  styleUrls: ['./status-lamp-actions.component.scss']
})
export class StatusLampActionsComponent {

  constructor(private statusLampApi: StatusLampApiService) { }

  public async enableStatusLamp(): Promise<void> {
    await this.statusLampApi.enableStatusLamp();
  }

  public async disableStatusLamp(): Promise<void> {
    await this.statusLampApi.disableStatusLamp();
  }

  public async redLightOff(): Promise<void> {
    await this.statusLampApi.redLightOff();
  }

  public async redLightOn(): Promise<void> {
    await this.statusLampApi.redLightOn();
  }

  public async redLightBlink(): Promise<void> {
    await this.statusLampApi.redLightBlink();
  }

  public async greenLightOff(): Promise<void> {
    await this.statusLampApi.greenLightOff();
  }

  public async greenLightOn(): Promise<void> {
    await this.statusLampApi.greenLightOn();
  }

  public async greenLightBlink(): Promise<void> {
    await this.statusLampApi.greenLightBlink();
  }
}
