import { Component, Inject } from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogRef,
} from '@angular/material/dialog';
import { FormControl, Validators } from '@angular/forms';
import { BaseCategory } from '../../_commons/models/categories/baseCategory';
import { CreateOrEditCategoryDialogComponent } from 'src/app/category/create-or-edit-category-dialog/create-or-edit-category-dialog.component';
import { CreateCategoryCommandData } from 'src/app/_commons/models/categories/createCategoryCommandData';
import { filter } from 'rxjs';
import { NotificationService } from 'src/app/_services/notification.service';
import { CategoryApiService } from 'src/app/_services/category.api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-pick-category-dialog',
  templateUrl: './pick-category-dialog.component.html',
  styleUrls: ['./pick-category-dialog.component.scss'],
})
export class PickCategoryDialogComponent {
  categoryControl = new FormControl<BaseCategory | null>(
    null,
    Validators.required
  );

  constructor(
    public dialogRef: MatDialogRef<PickCategoryDialogComponent>,
    @Inject(MAT_DIALOG_DATA)
    public categories: { data: BaseCategory[]; groupId: string },
    private notificationService: NotificationService,
    private dialog: MatDialog,
    private categoryApiService: CategoryApiService,
    private router: Router
  ) {}

  onNoClick(): void {
    this.dialogRef.close();
  }

  addCategory() {
    this.openDialog()
      .pipe(filter((x) => !!x))
      .subscribe({
        next: (result) => {
          let createCategoryCommandData: CreateCategoryCommandData = {
            name: result.name,
            color: result.color,
          };

          this.create(createCategoryCommandData);
        },
      });
  }

  openDialog(data?: { name: string; color: string }) {
    const dialogRef = this.dialog.open(CreateOrEditCategoryDialogComponent, {
      data: data,
    });
    return dialogRef.afterClosed();
  }

  create(category: CreateCategoryCommandData) {
    this.categoryApiService
      .create(category, this.categories.groupId)
      .subscribe((result) => {
        this.notificationService.succes(
          'Category added successfully',
          'Request completed'
        );
        this.dialogRef.close();
        if (!!this.categories.groupId) {
          this.router.navigate(
            [`groups/${this.categories.groupId}/notes/create`],
            { queryParams: { categoryId: result.resource.id } }
          );
          return;
        }
        this.router.navigate(['notes/create'], {
          queryParams: { categoryId: result.resource.id },
        });
      });
  }
}
