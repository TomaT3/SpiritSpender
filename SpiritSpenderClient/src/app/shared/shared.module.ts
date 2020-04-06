import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { UnitToStringPipe } from './pipes/unit-to-string.pipe';



@NgModule({
  declarations: [PageNotFoundComponent, UnitToStringPipe],
  imports: [
    CommonModule
  ],
  exports: [
    UnitToStringPipe
  ]
})
export class SharedModule { }
