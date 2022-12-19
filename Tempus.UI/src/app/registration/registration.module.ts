import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RegistrationRoutingModule} from "./registration-routing.module";
import {HttpClientModule} from "@angular/common/http";
import {ReactiveFormsModule} from "@angular/forms";
import {RouterModule} from "@angular/router";
import { RegistrationsOverviewComponent } from './registrations-overview/registrations-overview.component';
import {SharedModule} from "../shared/shared.module";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {MatDialogModule} from "@angular/material/dialog";
import {MatSelectModule} from "@angular/material/select";
import { DetailedRegistrationComponent } from './detailed-registration/detailed-registration.component';
import { CreateOrEditRegistrationComponent } from './create-or-edit-registration/create-or-edit-registration.component';
import {PickCategoryDialogComponent} from "./pick-category-dialog/pick-category-dialog.component";


@NgModule({
  declarations: [
    RegistrationsOverviewComponent,
    PickCategoryDialogComponent,
    DetailedRegistrationComponent,
    CreateOrEditRegistrationComponent
  ],
  imports: [
    RegistrationRoutingModule,
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule,
    SharedModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatSelectModule
  ],
    exports: [
    ]
})
export class RegistrationModule {
}
