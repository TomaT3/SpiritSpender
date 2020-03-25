import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AutomaticRoutingModule } from './automatic-routing.module';
import { AutomaticComponent } from './automatic/automatic.component';


@NgModule({
  declarations: [AutomaticComponent],
  imports: [
    CommonModule,
    AutomaticRoutingModule
  ]
})
export class AutomaticModule { }
