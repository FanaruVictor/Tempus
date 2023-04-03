import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GroupOverviewComponent } from './group-overview/group-overview.component';
import { CreateOrEditGroupComponent } from './create/create-or-edit-group.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'overview' },
  { path: 'create', component: CreateOrEditGroupComponent },
  { path: 'overview/:id/registrations', component: GroupOverviewComponent },
  { path: 'overview', component: GroupOverviewComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class GroupRoutingModule {}
