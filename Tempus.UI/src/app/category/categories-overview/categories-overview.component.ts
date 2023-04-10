import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { CreateOrEditCategoryDialogComponent } from '../create-or-edit-category-dialog/create-or-edit-category-dialog.component';
import { filter } from 'rxjs';
import { CategoryApiService } from '../../_services/category.api.service';
import { GenericResponse } from 'src/app/_commons/models/genericResponse';
import { BaseCategory } from '../../_commons/models/categories/baseCategory';
import { CreateCategoryCommandData } from '../../_commons/models/categories/createCategoryCommandData';
import { UpdateCategoryCommandData } from '../../_commons/models/categories/updateCategoryCommandData';
import { NotificationService } from '../../_services/notification.service';

@Component({
  selector: 'app-categories-overview',
  templateUrl: './categories-overview.component.html',
  styleUrls: ['./categories-overview.component.scss'],
})
export class CategoriesOverviewComponent {
  categories: BaseCategory[] = [];
  groupId: string | undefined;

  constructor(
    private dialog: MatDialog,
    private categoryApiService: CategoryApiService,
    private notificationService: NotificationService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const urlSections = this.router.url.split('/');
    const page = urlSections[1];
    if (page === 'groups') {
      this.groupId = this.router.url.split('/')[2] || '';
    }

    this.getAll();
  }

  getAll() {
    this.categoryApiService.getAll(this.groupId).subscribe({
      next: (response) => {
        this.categories = response.resource;
        this.categories = this.categories.sort(
          (objA, objB) =>
            new Date(objB.lastUpdatedAt).getTime() -
            new Date(objA.lastUpdatedAt).getTime()
        );
      },
    });
  }

  delete(id: string) {
    this.categoryApiService
      .delete(id)
      .pipe(filter((x) => !!x))
      .subscribe({
        next: (response) => {
          let id = response.resource;
          this.categories = this.categories?.filter((x) => x.id !== id);
          this.notificationService.succes(
            'Category deleted successfully',
            'Request completed'
          );
        },
      });
  }

  createCategory() {
    this.openDialog()
      .pipe(filter((x) => !!x))
      .subscribe({
        next: (result) => {
          let createCategoryCommandData: CreateCategoryCommandData = {
            name: result.name,
            color: result.color,
          };

          this.create(createCategoryCommandData);

          this.notificationService.succes(
            'Category added successfully',
            'Request completed'
          );
        },
      });
  }

  updateCategory(category: BaseCategory) {
    this.openDialog(category)
      .pipe(filter((x) => !!x))
      .subscribe({
        next: (result) => {
          if (result.name === category.name && result.color === category.color)
            return;

          let updateCategoryCommandData: UpdateCategoryCommandData = {
            id: category.id,
            name: result.name,
            color: result.color,
          };
          this.update(updateCategoryCommandData);
          this.notificationService.succes(
            'Category added successfully',
            'Request completed'
          );
        },
      });
  }

  openDialog(data?: { name: string; color: string }) {
    const dialogRef = this.dialog.open(CreateOrEditCategoryDialogComponent, {
      data: data,
    });
    return dialogRef.afterClosed();
  }

  update(category: UpdateCategoryCommandData) {
    this.categoryApiService
      .update(category, this.groupId)
      .pipe(filter((x) => !!x))
      .subscribe((result) => {
        this.categories = this.categories?.filter((x) => {
          return x.id !== result.resource.id;
        });
        this.addToCategories(result.resource);
      });
  }

  create(category: CreateCategoryCommandData) {
    this.categoryApiService
      .create(category, this.groupId)
      .pipe(filter((x) => !!x))
      .subscribe((result) => {
        this.addToCategories(result.resource);
      });
  }

  addToCategories(category: BaseCategory) {
    this.categories?.push(category);
    this.categories = this.categories?.sort(
      (objA, objB) =>
        new Date(objB.lastUpdatedAt).getTime() -
        new Date(objA.lastUpdatedAt).getTime()
    );
  }
}
