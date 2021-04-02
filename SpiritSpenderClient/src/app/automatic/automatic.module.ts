import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AutomaticRoutingModule } from './automatic-routing.module';
import { AutomaticMainComponent } from './automatic-main/automatic-main.component';
import { AutomaticPositionsComponent } from './automatic-main/automatic-positions/automatic-positions.component';
import { PositionQuantityComponent } from './automatic-main/automatic-positions/position-quantity/position-quantity.component';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from "@angular/material/icon";
import { HttpClientModule } from '@angular/common/http';
import { AutomaticActionsComponent } from './automatic-main/automatic-actions/automatic-actions.component';


@NgModule({
  declarations: [AutomaticMainComponent, AutomaticPositionsComponent, PositionQuantityComponent, AutomaticActionsComponent],
  imports: [
    CommonModule,
    AutomaticRoutingModule,
    MatButtonModule,
    MatIconModule,
    HttpClientModule
  ]
})
export class AutomaticModule { }
