import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {GroupOverviewComponent} from './group-overview/group-overview.component';
import {CreateOrEditGroupComponent} from './create/create-or-edit-group.component';
import {RegistrationsComponent} from "../registration/registrations/registrations.component";
import {
  CreateOrEditRegistrationComponent
} from "../registration/create-or-edit-registration/create-or-edit-registration.component";

const routes: Routes = [
  {
    path: '', component: GroupOverviewComponent, children: [
      {path: ':id/registrations', component: RegistrationsComponent},
      {path: ':id/registrations/create', component: CreateOrEditRegistrationComponent},
    ]
  },
  {path: 'create', component: CreateOrEditGroupComponent},

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class GroupRoutingModule {
}
