import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AutomaticMainComponent } from './automatic-main/automatic-main.component';


const routes: Routes = [
  {path: '', component: AutomaticMainComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AutomaticRoutingModule { }
