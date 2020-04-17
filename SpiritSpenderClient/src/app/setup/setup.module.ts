import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from  '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { SetupRoutingModule } from './setup-routing.module';
import { SetupMainComponent } from './components/setup-main/setup-main.component';
import { MatTabsModule } from '@angular/material/tabs';
import { DriveComponent } from './components/drive/drive.component';
import { DriveSettingsComponent } from './components/drive/drive-settings/drive-settings.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { UnitsComponent } from './components/drive/drive-settings/propertyComponents/units/units.component';
import { SharedModule } from '../shared/shared.module';
import { NumberComponent } from './components/drive/drive-settings/propertyComponents/number/number.component';
import { EnumComponent } from './components/drive/drive-settings/propertyComponents/enum/enum.component';


@NgModule({
  declarations: [SetupMainComponent, DriveComponent, DriveSettingsComponent, UnitsComponent, NumberComponent, EnumComponent],
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
    SharedModule
  ]
})
export class SetupModule { }
