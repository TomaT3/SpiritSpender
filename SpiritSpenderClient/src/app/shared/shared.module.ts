import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { UnitToStringPipe } from './pipes/unit-to-string.pipe';
import { EnumToArrayPipe } from './pipes/enum-to-array.pipe';
import { ShotGlassPositionsApiService } from './services/shot-glass-positions-api.service';



@NgModule({
  declarations: [PageNotFoundComponent, UnitToStringPipe, EnumToArrayPipe],
  imports: [
    CommonModule
  ],
  exports: [
    UnitToStringPipe,
    EnumToArrayPipe,
  ]
})
export class SharedModule { }
