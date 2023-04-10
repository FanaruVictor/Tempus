import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GroupOverviewComponent } from './group-overview/group-overview.component';
import { CreateOrEditGroupComponent } from './create/create-or-edit-group.component';
import { RegistrationsComponent } from '../registration/registrations/registrations.component';
import { CreateOrEditRegistrationComponent } from '../registration/create-or-edit-registration/create-or-edit-registration.component';
import { CategoryApiService } from '../_services/category.api.service';
import { CategoriesOverviewComponent } from '../category/categories-overview/categories-overview.component';
import { CreateOrEditCategoryDialogComponent } from '../category/create-or-edit-category-dialog/create-or-edit-category-dialog.component';

const routes: Routes = [
  {
    path: '',
    component: GroupOverviewComponent,
    children: [
      {
        path: ':id/registrations',
        loadChildren: () =>
          import('../registration/registration.module').then(
            (m) => m.RegistrationModule
          ),
      },
      {
        path: ':id/categories',
        loadChildren: () =>
          import('../category/category-routing.module').then(
            (m) => m.CategoryRoutingModule
          ),
      },
    ],
  },
  { path: 'create', component: CreateOrEditGroupComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class GroupRoutingModule {}
