import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AccountComponent} from "./account/account.component";
import {EditAccountProfile} from "./edit-account/edit-account.component";

const routes: Routes = [
  {path: '', pathMatch: 'prefix', component: AccountComponent},
  {path: 'edit', component: EditAccountProfile},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountRoutingModule {
}
