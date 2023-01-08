import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {CategoriesOverviewComponent} from "./categories-overview/categories-overview.component";

const routes: Routes = [
  {path: '', pathMatch: 'prefix', redirectTo: 'overview'},
  {path: 'overview', component: CategoriesOverviewComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CategoryRoutingModule {
}
