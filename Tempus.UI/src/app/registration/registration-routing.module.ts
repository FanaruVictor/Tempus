import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {RegistrationsOverviewComponent} from "./registrations-overview/registrations-overview.component";
import {CreateOrEditRegistrationComponent} from "./create-or-edit-registration/create-or-edit-registration.component";

const routes: Routes = [
  {path: '', pathMatch: 'full', redirectTo: 'overview'},
  {path: 'create', component: CreateOrEditRegistrationComponent},
  {path: 'edit/:id', component: CreateOrEditRegistrationComponent},
  {path: 'overview', component: RegistrationsOverviewComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RegistrationRoutingModule {
}
