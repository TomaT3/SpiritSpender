import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AutomaticComponent } from './automatic/automatic.component';


const routes: Routes = [
  {path: '', component: AutomaticComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AutomaticRoutingModule { }
