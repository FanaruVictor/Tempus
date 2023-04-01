import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GroupOverviewComponent } from './group-overview/group-overview.component';
import { CreateOrEditComponent } from './create/create-or-edit.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'overview' },
  { path: 'create', component: CreateOrEditComponent },
  { path: 'overview', component: GroupOverviewComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class GroupRoutingModule {}
