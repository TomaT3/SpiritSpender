import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AutomaticRoutingModule } from './automatic-routing.module';
import { AutomaticMainComponent } from './automatic-main/automatic-main.component';


@NgModule({
  declarations: [AutomaticMainComponent],
  imports: [
    CommonModule,
    AutomaticRoutingModule
  ]
})
export class AutomaticModule { }
