import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GroupRoutingModule } from './group-routing.module';
import { GroupsComponent } from './groups/groups.component';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { SharedModule } from '../shared/shared.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { CreateOrEditGroupComponent } from './create-or-edit/create-or-edit-group.component';
import { MatMenuModule } from '@angular/material/menu';
import { MatCardModule } from '@angular/material/card';
import { NoteModule } from '../note/note.module';
import { GroupMenuComponent } from './groups/group-menu/group-menu.component';

@NgModule({
  declarations: [
    GroupsComponent,
    CreateOrEditGroupComponent,
    GroupMenuComponent,
  ],
  imports: [
    CommonModule,
    GroupRoutingModule,
    MatButtonModule,
    FormsModule,
    ReactiveFormsModule,
    MatIconModule,
    SharedModule,
    NgSelectModule,
    MatMenuModule,
    MatCardModule,
    NoteModule,
  ],
})
export class GroupModule {}
