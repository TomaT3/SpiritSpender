import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from  '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { SetupRoutingModule } from './setup-routing.module';
import { SetupMainComponent } from './components/setup-main/setup-main.component';
import { MatTabsModule } from '@angular/material/tabs';
import { DriveComponent } from './components/setup-main/drive/drive.component';
import { DriveSettingsComponent } from './components/setup-main/drive/drive-settings/drive-settings.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { UnitsComponent } from './controls/units/units.component';
import { SharedModule } from '../shared/shared.module';
import { NumberComponent } from './controls/number/number.component';
import { DriveDirectionComponent } from './controls/enums/drive-direction/drive-direction.component';
import { DriveActionsComponent } from './components/setup-main/drive/drive-actions/drive-actions.component';
import { UnitstypeOneActionComponent } from './controls/unitstype-one-action/unitstype-one-action.component';
import { UnitstypeTwoActionsComponent } from './controls/unitstype-two-actions/unitstype-two-actions.component';
import { SpiritDispenserComponent } from './components/setup-main/spirit-dispenser/spirit-dispenser.component';
import { SpiritDispenserSettingsComponent } from './components/setup-main/spirit-dispenser/spirit-dispenser-settings/spirit-dispenser-settings.component';
import { SpiritDispenserActionsComponent } from './components/setup-main/spirit-dispenser/spirit-dispenser-actions/spirit-dispenser-actions.component';
import { PositionsComponent } from './components/setup-main/positions/positions.component';
import { PositionSettingsComponent } from './components/setup-main/positions/position-settings/position-settings.component';
import { PositionSettingComponent } from './components/setup-main/positions/position-settings/position-setting/position-setting.component';
import { AutoSizeInputModule, AutoSizeInputOptions, AUTO_SIZE_INPUT_OPTIONS } from 'ngx-autosize-input';
import { StatusLampComponent } from './components/setup-main/status-lamp/status-lamp.component';
import { StatusLampSettingsComponent } from './components/setup-main/status-lamp/status-lamp-settings/status-lamp-settings.component';
import { StatusLampActionsComponent } from './components/setup-main/status-lamp/status-lamp-actions/status-lamp-actions.component';

const CUSTOM_AUTO_SIZE_INPUT_OPTIONS: AutoSizeInputOptions = {
  extraWidth: 0,
  includeBorders: false,
  includePadding: true,
  includePlaceholder: true,
  maxWidth: -1,
  minWidth: -1,
  setParentWidth: false,
  usePlaceHolderWhenEmpty: false,
}


@NgModule({
  declarations: [SetupMainComponent, DriveComponent, DriveSettingsComponent, UnitsComponent, NumberComponent, DriveDirectionComponent, DriveActionsComponent, UnitstypeOneActionComponent, UnitstypeTwoActionsComponent, SpiritDispenserComponent, SpiritDispenserSettingsComponent, SpiritDispenserActionsComponent, PositionsComponent, PositionSettingsComponent, PositionSettingComponent, StatusLampComponent, StatusLampSettingsComponent, StatusLampActionsComponent],
  imports: [
    CommonModule,
    SetupRoutingModule,
    FormsModule,
    MatTabsModule,
    MatFormFieldModule,
    MatInputModule,
    MatCheckboxModule,
    MatSelectModule,
    MatButtonModule,
    HttpClientModule,
    SharedModule,
    AutoSizeInputModule
  ],
  providers: [
    { provide: AUTO_SIZE_INPUT_OPTIONS, useValue: CUSTOM_AUTO_SIZE_INPUT_OPTIONS }
  ]
})
export class SetupModule { }
