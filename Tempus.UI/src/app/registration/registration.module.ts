import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RegistrationRoutingModule} from "./registration-routing.module";
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {RouterModule} from "@angular/router";
import {RegistrationsOverviewComponent} from './registrations-overview/registrations-overview.component';
import {SharedModule} from "../shared/shared.module";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {MatDialogModule} from "@angular/material/dialog";
import {MatSelectModule} from "@angular/material/select";
import {DetailedRegistrationComponent} from './detailed-registration/detailed-registration.component';
import {CreateOrEditRegistrationComponent} from './create-or-edit-registration/create-or-edit-registration.component';
import {PickCategoryDialogComponent} from "./pick-category-dialog/pick-category-dialog.component";
import {ErrorStateMatcher, ShowOnDirtyErrorStateMatcher} from "@angular/material/core";
import {MatInputModule} from "@angular/material/input";
import {JwtInterceptor} from "../_commons/interceptors/JwtInterceptor";
import {MatTooltipModule} from "@angular/material/tooltip";
import {QuillModule} from "ngx-quill";
import {FilterPipe} from "../_commons/pipes/filter.pipe";
@NgModule({
  declarations: [

    RegistrationsOverviewComponent,
    PickCategoryDialogComponent,
    DetailedRegistrationComponent,
    CreateOrEditRegistrationComponent,
    FilterPipe
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
    MatSelectModule,
    MatInputModule,
    MatTooltipModule,
    QuillModule.forRoot(),
    FormsModule,
  ],
  exports: [],
  providers: [
    {provide: ErrorStateMatcher, useClass: ShowOnDirtyErrorStateMatcher},
    {
      provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true
    }
  ]
})
export class RegistrationModule {
}
