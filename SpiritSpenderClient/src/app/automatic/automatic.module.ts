import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AutomaticRoutingModule } from './automatic-routing.module';
import { AutomaticMainComponent } from './automatic-main/automatic-main.component';
import { AutomaticPositionsComponent } from './automatic-main/automatic-positions/automatic-positions.component';
import { PositionQuantityComponent } from './automatic-main/automatic-positions/position-quantity/position-quantity.component';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from "@angular/material/icon";
import { HttpClientModule } from '@angular/common/http';
import { AutomaticActionsComponent } from './automatic-main/automatic-actions/automatic-actions.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [AutomaticMainComponent, AutomaticPositionsComponent, PositionQuantityComponent, AutomaticActionsComponent],
  imports: [
    CommonModule,
    AutomaticRoutingModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    HttpClientModule,
    SharedModule
  ]
})
export class AutomaticModule { }
