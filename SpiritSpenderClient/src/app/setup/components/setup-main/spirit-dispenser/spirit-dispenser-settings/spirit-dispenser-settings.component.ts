import { Component, OnInit } from '@angular/core';
import { SpiritDispenserApiService } from 'src/app/setup/services/spirit-dispenser-api.service';
import { SpiritDispenserSetting, SpiritDispenserTexts } from 'src/app/setup/types/spirit-dispenser-setting';
import { CopyHelper } from 'src/app/shared/Helpers/CopyHelper';

@Component({
  selector: 'app-spirit-dispenser-settings',
  templateUrl: './spirit-dispenser-settings.component.html',
  styleUrls: ['./spirit-dispenser-settings.component.scss']
})
export class SpiritDispenserSettingsComponent implements OnInit {
  serverSettingValue: SpiritDispenserSetting;
  newSettingValue: SpiritDispenserSetting;

  public readonly driveTimeToCloseTheSpiritSpender = SpiritDispenserTexts.driveTimeToCloseTheSpiritSpender;
  public readonly driveTimeToReleaseTheSpirit = SpiritDispenserTexts.driveTimeToReleaseTheSpirit;
  public readonly waitTimeUntilSpiritIsReleased = SpiritDispenserTexts.waitTimeUntilSpiritIsReleased;

  constructor(private spiritDispenserApi: SpiritDispenserApiService) { }

  async ngOnInit(): Promise<void> {
    try{
      await this.getServerValues();
    } catch (error) {
      console.error(error);
    }
  }

  public async updateSettingValues(): Promise<void> {
    await this.spiritDispenserApi.updateSettings(this.newSettingValue);
    await this.getServerValues();
  }

  private async getServerValues(): Promise<void> {
    this.serverSettingValue = await this.spiritDispenserApi.getSetting();
    this.newSettingValue = <SpiritDispenserSetting>CopyHelper.deepCopy(this.serverSettingValue);
  }
}
