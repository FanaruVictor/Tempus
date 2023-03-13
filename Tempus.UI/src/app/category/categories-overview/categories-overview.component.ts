import {Component} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {MatDialog} from "@angular/material/dialog";
import {
  CreateOrEditCategoryDialogComponent
} from "../create-or-edit-category-dialog/create-or-edit-category-dialog.component";
import {filter} from "rxjs";
import {CategoryApiService} from "../../_services/category.api.service";
import {GenericResponse} from 'src/app/_commons/models/genericResponse';
import {BaseCategory} from "../../_commons/models/categories/baseCategory";
import {CreateCategoryCommandData} from "../../_commons/models/categories/createCategoryCommandData";
import {UpdateCategoryCommandData} from "../../_commons/models/categories/updateCategoryCommandData";
import {NotificationService} from "../../_services/notification.service";

@Component({
  selector: 'app-categories-overview',
  templateUrl: './categories-overview.component.html',
  styleUrls: ['./categories-overview.component.scss']
})
export class CategoriesOverviewComponent {
  categories?: BaseCategory[];

  constructor(
    private httpClient: HttpClient,
    private router: Router,
    private dialog: MatDialog,
    private categoryApiService: CategoryApiService,
    private notificationService: NotificationService
  ) {
  }

  ngOnInit(): void {
    this.httpClient.get<GenericResponse<BaseCategory[]>>(`https://localhost:7077/api/v1/categories`)
      .subscribe({
        next: response => {
          this.categories = response.resource;
          this.categories = this.categories.sort(
            (objA, objB) => new Date(objB.lastUpdatedAt).getTime() - new Date(objA.lastUpdatedAt).getTime(),
          );
        }
      });
  }

  delete(id: string) {
    this.httpClient.delete<GenericResponse<string>>(`https://localhost:7077/api/v1/categories/${id}`)
      .pipe(filter(x => !!x))
      .subscribe({
        next: response => {
          let id = response.resource;
          this.categories = this.categories?.filter(x => x.id !== id);
          let last = this.categories?.slice(-1)[0];
          this.router.navigate([`/categories/overview`]);
          this.notificationService.succes('Category deleted successfully', 'Request completed');
        }
      });
  }

  addCategory() {
    this.openDialog()
      .pipe(filter(x => !!x))
      .subscribe({
        next: result => {
          let createCategoryCommandData: CreateCategoryCommandData = {
            userId: 'FA2C9EFA-E576-44A9-A6E5-08DACD729E8D',
            name: result.name,
            color: result.color
          }
          this.create(createCategoryCommandData);
          this.notificationService.succes('Category added successfully', 'Request completed');

        }
      });

  }

  updateCategory(category: BaseCategory) {
    this.openDialog(category)
      .pipe(filter(x => !!x))
      .subscribe({
        next: result => {
          if(result.name === category.name && result.color === category.color)
            return;

          let updateCategoryCommandData: UpdateCategoryCommandData = {
            id: category.id,
            name: result.name,
            color: result.color
          }
          this.update(updateCategoryCommandData);
          this.notificationService.succes('Category added successfully', 'Request completed');
        }
      })
  }

  openDialog(data?: { name: string, color: string }) {
    const dialogRef = this.dialog.open(CreateOrEditCategoryDialogComponent, {
      data: data,
    });
    return dialogRef.afterClosed();
  }

  update(category: UpdateCategoryCommandData) {
    this.categoryApiService.update(category)
      .pipe(filter(x => !!x))
      .subscribe(result => {
        this.categories = this.categories?.filter(x => {
          return x.id !== result.resource.id;
        });
        this.addToCategories(result.resource);
      })
  }

  create(category: CreateCategoryCommandData) {
    this.categoryApiService.create(category)
      .pipe(filter(x => !!x))
      .subscribe(result => {
        this.addToCategories(result.resource);
      })
  }

  addToCategories(category: BaseCategory) {
    this.categories?.push(category);
    this.categories = this.categories?.sort(
      (objA, objB) => new Date(objB.lastUpdatedAt).getTime() - new Date(objA.lastUpdatedAt).getTime(),
    );
  }
}
