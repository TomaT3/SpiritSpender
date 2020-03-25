import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SetupMainComponent } from './components/setup-main/setup-main.component';


const routes: Routes = [
  {path: '', component: SetupMainComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SetupRoutingModule { }
