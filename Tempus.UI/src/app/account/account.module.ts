import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ProfileComponent} from './profile/profile.component';
import {AccountRoutingModule} from "./account-routing.module";
import {SharedModule} from "../shared/shared.module";
import {MatSlideToggleModule} from "@angular/material/slide-toggle";
import {FormsModule} from "@angular/forms";
import {MatButtonModule} from "@angular/material/button";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatSelectModule} from "@angular/material/select";
import {MatDialogModule} from "@angular/material/dialog";
import {FileUploadModule} from "ng2-file-upload";
import {MatIconModule} from "@angular/material/icon";
import {DeleteDialogComponent} from "../_commons/components/delete-user-dialog/delete-dialog.component";

@NgModule({
  declarations: [
    ProfileComponent,
    DeleteDialogComponent,
  ],
  imports: [
    CommonModule,
    AccountRoutingModule,
    SharedModule,
    MatSlideToggleModule,
    FormsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatSelectModule,
    MatDialogModule,
    FileUploadModule,
    MatIconModule
  ]
})
export class AccountModule {
}
