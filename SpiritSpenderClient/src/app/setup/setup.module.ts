import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from  '@angular/common/http';

import { SetupRoutingModule } from './setup-routing.module';
import { SetupMainComponent } from './components/setup-main/setup-main.component';
import { MatTabsModule } from '@angular/material/tabs';
import { DriveComponent } from './components/drive/drive.component';
import { DriveSettingsComponent } from './components/drive/drive-settings/drive-settings.component';


@NgModule({
  declarations: [SetupMainComponent, DriveComponent, DriveSettingsComponent],
  imports: [
    CommonModule,
    SetupRoutingModule,
    MatTabsModule,
    HttpClientModule
  ]
})
export class SetupModule { }
