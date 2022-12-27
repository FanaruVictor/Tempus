import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoriesOverviewComponent } from './categories-overview/categories-overview.component';
import {RouterModule} from "@angular/router";
import {SharedModule} from "../shared/shared.module";
import {CategoryRoutingModule} from "./category-routing.module";
import { ReactiveFormsModule} from "@angular/forms";
import {DetailedCategoryComponent} from "./detailed-category/detailed-category.component";
import {MatButtonModule} from "@angular/material/button";



@NgModule({
  declarations: [
    DetailedCategoryComponent,
    CategoriesOverviewComponent,
  ],
  imports: [
    CategoryRoutingModule,
    CommonModule,
    RouterModule,
    SharedModule,
    ReactiveFormsModule,
    MatButtonModule
  ]
})
export class CategoryModule { }
