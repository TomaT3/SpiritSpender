import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PageNotFoundComponent } from './shared/components/page-not-found/page-not-found.component';


const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'automatic',
  },
  {
   path: 'automatic',
   loadChildren: () => import('./automatic/automatic.module').then(m => m.AutomaticModule),
  },
  {
    path: 'setup',
    loadChildren: () => import('./setup/setup.module').then(m => m.SetupModule),
   },
  {
    path: '404',
    component: PageNotFoundComponent
  },
  {
    path: '**',
    redirectTo: '/404'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
