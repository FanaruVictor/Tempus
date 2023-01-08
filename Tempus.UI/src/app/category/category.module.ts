import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoriesOverviewComponent } from './categories-overview/categories-overview.component';
import {RouterModule} from "@angular/router";
import {SharedModule} from "../shared/shared.module";
import {CategoryRoutingModule} from "./category-routing.module";
import { ReactiveFormsModule} from "@angular/forms";
import {MatButtonModule} from "@angular/material/button";
import {MatDialogModule} from "@angular/material/dialog";
import { CreateOrEditCategoryDialogComponent } from './create-or-edit-category-dialog/create-or-edit-category-dialog.component';
import {MatInputModule} from "@angular/material/input";
import {
  MAT_COLOR_FORMATS,
  NGX_MAT_COLOR_FORMATS,
  NgxMatColorPickerModule
} from "@angular-material-components/color-picker";



@NgModule({
  declarations: [
    CategoriesOverviewComponent,
    CreateOrEditCategoryDialogComponent,
  ],
  imports: [
    CategoryRoutingModule,
    CommonModule,
    RouterModule,
    SharedModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatDialogModule,
    MatInputModule,
    NgxMatColorPickerModule
  ],
  providers: [
    { provide: MAT_COLOR_FORMATS, useValue: NGX_MAT_COLOR_FORMATS }
  ]
})
export class CategoryModule { }
