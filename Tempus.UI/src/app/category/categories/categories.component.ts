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
import { FormControl, FormGroup } from '@angular/forms';
import { RegistrationService } from 'src/app/_services/registration/registration.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.scss'],
})
export class CategoriesComponent {
  categories: BaseCategory[] = [];
  groupId: string | undefined;
  searchText = '';
  maxDate: Date;
  minDate: Date;

  dateRange = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });

  constructor(
    private dialog: MatDialog,
    private categoryApiService: CategoryApiService,
    private notificationService: NotificationService,
    private router: Router,
    private registrationsService: RegistrationService
  ) {
    const currentYear = new Date().getFullYear();
    this.minDate = new Date(currentYear - 30, 0, 1);
    this.maxDate = new Date();
  }

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
          const category = this.categories?.find((x) => x.id === id);
          if (category?.color) {
            this.registrationsService.removeAllWithColor(category.color);
          }

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
        const oldColor = this.categories?.find(
          (x) => x.id === result.resource.id
        )?.color;
        const newColor = result.resource.color;
        if (oldColor && newColor && oldColor !== newColor) {
          debugger
          this.registrationsService.updateAllWithOldColor(oldColor, newColor);
        }
        debugger
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

  resetDate() {
    this.dateRange.reset();
  }

  modifyContent(values: string[]) {
    let element = document.getElementsByClassName('mat-select-value')[0];
    element.innerHTML = '';
    values.forEach(
      (x) =>
        (element.innerHTML = element.innerHTML.concat(`<div style="
        background-color: ${x};
        height: 15px;
        width: 15px;
        margin-right: 5px;"></div>`))
    );
  }
}
