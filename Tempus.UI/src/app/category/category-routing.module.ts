import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {CategoriesOverviewComponent} from "./categories-overview/categories-overview.component";
import {DetailedCategoryComponent} from "./detailed-category/detailed-category.component";

const routes: Routes = [
  {path: '', pathMatch: 'prefix', redirectTo: 'overview'},
  {path: 'overview', component: CategoriesOverviewComponent},
  {path: 'create', component: DetailedCategoryComponent},
  {path: ':id', component: DetailedCategoryComponent},
  {path: 'edit/:id', component: DetailedCategoryComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CategoryRoutingModule {
}
