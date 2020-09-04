import { Component, OnInit } from '@angular/core';
import { StatusLampSetting, StatusLampTexts } from 'src/app/setup/types/status-lamp-setting';
import { StatusLampApiService } from 'src/app/setup/services/status-lamp-api.service';
import { CopyHelper } from 'src/app/shared/Helpers/CopyHelper';

@Component({
  selector: 'app-status-lamp-settings',
  templateUrl: './status-lamp-settings.component.html',
  styleUrls: ['./status-lamp-settings.component.scss']
})
export class StatusLampSettingsComponent implements OnInit {
  serverSettingValue: StatusLampSetting;
  newSettingValue: StatusLampSetting;

  constructor(private statusLampApi: StatusLampApiService) { }

  public readonly blinkTimeOff = StatusLampTexts.blinkTimeOff;
  public readonly blinkTimeOn = StatusLampTexts.blinkTimeOn;

  async ngOnInit(): Promise<void> {
    try{
      await this.getServerValues();
    } catch (error) {
      console.error(error);
    }
  }

  public async updateSettingValues(): Promise<void> {
    await this.statusLampApi.updateSettings(this.newSettingValue);
    await this.getServerValues();
  }

  private async getServerValues(): Promise<void> {
    this.serverSettingValue = await this.statusLampApi.getSetting();
    this.newSettingValue = <StatusLampSetting>CopyHelper.deepCopy(this.serverSettingValue);
  }
}
