import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SetupRoutingModule } from './setup-routing.module';
import { MainComponent } from './main/main.component';
import {MatTabsModule} from '@angular/material/tabs';


@NgModule({
  declarations: [MainComponent],
  imports: [
    CommonModule,
    SetupRoutingModule,
    MatTabsModule
  ]
})
export class SetupModule { }
