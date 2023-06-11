import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateOrEditNoteComponent } from './create-or-edit-note/create-or-edit-note.component';
import { NotesComponent } from './notes/notes.component';

const routes: Routes = [
  {
    path: '',
    component: NotesComponent,
    children: [
      {
        path: ':id/edit-notes-view',
        component: CreateOrEditNoteComponent,
      },
    ],
  },
  { path: ':id/edit-full-view', component: CreateOrEditNoteComponent },
  { path: 'create', component: CreateOrEditNoteComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class NotesRoutingModule {}
