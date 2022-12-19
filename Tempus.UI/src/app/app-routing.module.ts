import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {CategoryRoutingModule} from "./category/category-routing.module";

const routes: Routes = [
  {path: '', pathMatch: 'full', redirectTo: '/registrations'},
  {
    path: 'registrations',
    loadChildren: () => import('./registration/registration.module').then(m => m.RegistrationModule)
  },
  {
    path: 'categories',
    loadChildren: () => import('./category/category-routing.module').then(m => m.CategoryRoutingModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
