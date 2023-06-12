import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './_commons/guards/AuthGuard';
import { RegisterOrLoginComponent } from './register-or-login/register-or-login.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/notes' },
  {
    path: 'notes',
    loadChildren: () => import('./note/note.module').then((m) => m.NoteModule),
    canActivate: [AuthGuard],
  },
  {
    path: 'categories',
    loadChildren: () =>
      import('./category/category-routing.module').then(
        (m) => m.CategoryRoutingModule
      ),
    canActivate: [AuthGuard],
  },
  {
    path: 'account',
    loadChildren: () =>
      import('./account/account.module').then((m) => m.AccountModule),
    canActivate: [AuthGuard],
  },
  {
    path: 'groups',
    loadChildren: () =>
      import('./group/group.module').then((m) => m.GroupModule),
    canActivate: [AuthGuard],
  },
  {
    path: 'login',
    component: RegisterOrLoginComponent,
  },
  {
    path: '**',
    redirectTo: '/notes',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
