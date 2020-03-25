import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SetupRoutingModule } from './setup-routing.module';
import { SetupMainComponent } from './setup-main/setup-main.component';
import {MatTabsModule} from '@angular/material/tabs';


@NgModule({
  declarations: [SetupMainComponent],
  imports: [
    CommonModule,
    SetupRoutingModule,
    MatTabsModule
  ]
})
export class SetupModule { }
