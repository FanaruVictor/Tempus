import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateOrEditRegistrationComponent } from './create-or-edit-registration/create-or-edit-registration.component';
import { NotesComponent } from './notes/notes.component';

const routes: Routes = [
  {
    path: '',
    component: NotesComponent,
    children: [
      {
        path: ':id/edit-notes-view',
        component: CreateOrEditRegistrationComponent,
      },
    ],
  },
  { path: ':id/edit-full-view', component: CreateOrEditRegistrationComponent },
  { path: 'create', component: CreateOrEditRegistrationComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class NotesRoutingModule {}
