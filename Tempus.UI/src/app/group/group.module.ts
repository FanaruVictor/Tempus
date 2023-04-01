import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GroupRoutingModule } from './group-routing.module';
import { GroupOverviewComponent } from './group-overview/group-overview.component';
import { MatButtonModule } from '@angular/material/button';
import { CreateOrEditComponent } from './create/create-or-edit.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { SharedModule } from '../shared/shared.module';
import { NgSelectModule } from '@ng-select/ng-select';

@NgModule({
  declarations: [GroupOverviewComponent, CreateOrEditComponent],
  imports: [
    CommonModule,
    GroupRoutingModule,
    MatButtonModule,
    FormsModule,
    ReactiveFormsModule,
    MatIconModule,
    SharedModule,
    NgSelectModule,
  ],
})
export class GroupModule {}
