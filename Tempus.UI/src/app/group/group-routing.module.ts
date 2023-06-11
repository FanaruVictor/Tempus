import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GroupsComponent } from './groups/groups.component';
import { CreateOrEditGroupComponent } from './create-or-edit/create-or-edit-group.component';

const routes: Routes = [
  {
    path: '',
    component: GroupsComponent,
    children: [
      {
        path: ':id/notes',
        loadChildren: () =>
          import('../note/note.module').then((m) => m.NoteModule),
      },
      {
        path: ':id/categories',
        loadChildren: () =>
          import('../category/category-routing.module').then(
            (m) => m.CategoryRoutingModule
          ),
      },
    ],
  },
  { path: 'create', component: CreateOrEditGroupComponent },
  { path: ':id/edit', component: CreateOrEditGroupComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class GroupRoutingModule {}
