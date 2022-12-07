import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RegistrationRoutingModule} from "./registration-routing.module";
import {HttpClientModule} from "@angular/common/http";
import {ReactiveFormsModule} from "@angular/forms";
import {RouterModule} from "@angular/router";
import {RegistrationComponent} from "./registration/registration.component";


@NgModule({
  declarations: [
    RegistrationComponent,
  ],
  imports: [
    RegistrationRoutingModule,
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule
  ]
})
export class RegistrationModule {
}
