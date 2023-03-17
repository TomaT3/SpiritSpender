import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { UnitToStringPipe } from './pipes/unit-to-string.pipe';
import { EnumToArrayPipe } from './pipes/enum-to-array.pipe';
import { ShotGlassPositionsApiService } from './services/shot-glass-positions-api.service';
import { SafePipe } from './pipes/safe.pipe';
import { UnitsComponent } from './controls/units/units.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { AutoSizeInputModule, AutoSizeInputOptions, AUTO_SIZE_INPUT_OPTIONS } from 'ngx-autosize-input';

const CUSTOM_AUTO_SIZE_INPUT_OPTIONS: AutoSizeInputOptions = {
  extraWidth: 0,
  includeBorders: false,
  includePadding: true,
  includePlaceholder: true,
  maxWidth: -1,
  minWidth: -1,
  setParentWidth: false,
  usePlaceHolderWhenEmpty: false,
}

@NgModule({
  declarations: [PageNotFoundComponent, UnitToStringPipe, EnumToArrayPipe, SafePipe, UnitsComponent],
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    AutoSizeInputModule
  ],
  exports: [
    UnitToStringPipe,
    EnumToArrayPipe,
    SafePipe,
    UnitsComponent
  ],
  providers: [
    { provide: AUTO_SIZE_INPUT_OPTIONS, useValue: CUSTOM_AUTO_SIZE_INPUT_OPTIONS }
  ]
})
export class SharedModule { }
