import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ButtonComponent} from "./button/button.component";
import {MatIconModule} from "@angular/material/icon";
import {MatTooltipModule} from "@angular/material/tooltip";
import {MatDialogModule} from "@angular/material/dialog";
import {MatButtonModule} from "@angular/material/button";
import {DeleteDialogComponent} from "./delete-user-dialog/delete-dialog.component";
import {NotFoundComponent} from "./not-found/not-found.component";

@NgModule({
  declarations: [ButtonComponent, DeleteDialogComponent, NotFoundComponent],
  imports: [
    CommonModule,
    MatIconModule,
    MatTooltipModule,
    MatButtonModule,
    MatDialogModule,
  ],
  exports: [ButtonComponent, DeleteDialogComponent, NotFoundComponent]
})
export class SharedModule {
}
