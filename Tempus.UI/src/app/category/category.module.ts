import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoryComponent } from './category/category.component';
import { CategoriesOverviewComponent } from './categories-overview/categories-overview.component';
import {RouterModule} from "@angular/router";
import {SharedModule} from "../shared/shared.module";
import {CategoryRoutingModule} from "./category-routing.module";
import { ReactiveFormsModule} from "@angular/forms";



@NgModule({
  declarations: [
    CategoryComponent,
    CategoriesOverviewComponent,
  ],
  imports: [
    CategoryRoutingModule,
    CommonModule,
    RouterModule,
    SharedModule,
    ReactiveFormsModule
  ],
    exports: [
        CategoriesOverviewComponent,
        CategoryComponent
    ]
})
export class CategoryModule { }
