import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {NotFoundComponent} from "./_commons/not-found/not-found.component";
import {AuthGuard} from "./_commons/guards/AuthGuard";

const routes: Routes = [
  {path: '', pathMatch: 'full', redirectTo: '/registrations'},
  {
    path: 'registrations',
    loadChildren: () => import('./registration/registration.module').then(m => m.RegistrationModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'categories',
    loadChildren: () => import('./category/category-routing.module').then(m => m.CategoryRoutingModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'auth',
    loadChildren: () => import('./auth/auth.module').then(m => m.AuthModule)
  },
  {
    path: 'account',
    loadChildren: () => import('./account/account.module').then(m => m.AccountModule),
    canActivate: [AuthGuard]
  },
  {
    path: '**',
    component: NotFoundComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
