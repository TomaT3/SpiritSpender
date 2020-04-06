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
import { MatCheckboxModule } from '@angular/material/checkbox';
import { UnitsComponent } from './components/drive/drive-settings/propertyComponents/units/units.component';
import { SharedModule } from '../shared/shared.module';
import { NumberComponent } from './components/drive/drive-settings/propertyComponents/number/number.component';


@NgModule({
  declarations: [SetupMainComponent, DriveComponent, DriveSettingsComponent, UnitsComponent, NumberComponent],
  imports: [
    CommonModule,
    SetupRoutingModule,
    MatTabsModule,
    MatFormFieldModule,
    MatInputModule,
    MatCheckboxModule,
    HttpClientModule,
    SharedModule
  ]
})
export class SetupModule { }
