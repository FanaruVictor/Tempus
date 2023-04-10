import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {GroupOverviewComponent} from './group-overview/group-overview.component';
import {CreateOrEditGroupComponent} from './create/create-or-edit-group.component';
import {RegistrationsComponent} from "../registration/registrations/registrations.component";
import {
  CreateOrEditRegistrationComponent
} from "../registration/create-or-edit-registration/create-or-edit-registration.component";

const routes: Routes = [
  {path: '', component: GroupOverviewComponent},
  {path: 'create', component: CreateOrEditGroupComponent},
  {
    path: ':id', component: GroupOverviewComponent, children: [
      {path: 'registrations', component: RegistrationsComponent},
      {path: 'registrations/create', component: CreateOrEditRegistrationComponent},
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class GroupRoutingModule {
}
