import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from  '@angular/common/http';

import { SetupRoutingModule } from './setup-routing.module';
import { SetupMainComponent } from './components/setup-main/setup-main.component';
import { MatTabsModule } from '@angular/material/tabs';
import { DriveComponent } from './components/drive/drive.component';
import { DriveSettingsComponent } from './components/drive/drive-settings/drive-settings.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MaxSpeedComponent } from './components/drive/drive-settings/propertyComponents/max-speed/max-speed.component';
import { UnitToStringPipe } from '../shared/pipes/unit-to-string.pipe';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [SetupMainComponent, DriveComponent, DriveSettingsComponent, MaxSpeedComponent],
  imports: [
    CommonModule,
    SetupRoutingModule,
    MatTabsModule,
    MatFormFieldModule,
    MatInputModule,
    HttpClientModule,
    SharedModule
  ]
})
export class SetupModule { }
