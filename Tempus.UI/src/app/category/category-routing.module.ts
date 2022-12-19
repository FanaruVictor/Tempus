import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {CategoryComponent} from "./category/category.component";

const routes: Routes = [
  {path: '', pathMatch:'full', redirectTo: 'create'},
  {path: 'create', component: CategoryComponent},
  {path: ':id', component: CategoryComponent},
  {path: 'edit/:id', component: CategoryComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CategoryRoutingModule {
}
