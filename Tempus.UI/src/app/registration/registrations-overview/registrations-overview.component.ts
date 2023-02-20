import {Component} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {MatDialog} from "@angular/material/dialog";
import {PickCategoryDialogComponent} from "../pick-category-dialog/pick-category-dialog.component";
import {RegistrationApiService} from "../../_services/registration.api.service";
import {CategoryApiService} from "../../_services/category.api.service";
import {RegistrationOverview} from "../../_commons/models/registrations/registrationOverview";
import {BaseCategory} from "../../_commons/models/categories/baseCategory";

@Component({
  selector: 'app-registrations-overview',
  templateUrl: './registrations-overview.component.html',
  styleUrls: ['./registrations-overview.component.scss']
})
export class RegistrationsOverviewComponent {
  registrations!: RegistrationOverview[];
  categories?: BaseCategory[];
  defaultColor = '#d6efef';
  searchText = '';
  constructor(
    private httpClient: HttpClient,
    private router: Router,
    private dialog: MatDialog,
    private registrationApiService: RegistrationApiService,
    private categoryApiService: CategoryApiService
  ) {
  }

  ngOnInit(): void {
    this.getAll();
  }

  getAll() {
    this.registrationApiService.getAll()
      .subscribe({
        next: response => {
          this.registrations = response.resource;
          this.registrations = this.registrations?.sort(
            (objA, objB) => new Date(objB.createdAt).getTime() - new Date(objA.createdAt).getTime(),
          );
          this.registrations = [...this.registrations, ...this.registrations, ...this.registrations];
          this.registrations = [...this.registrations, ...this.registrations, ...this.registrations];
          this.registrations = [...this.registrations, ...this.registrations, ...this.registrations];
        }
      });
  }

  addRegistration(): void {
    this.categoryApiService.getAll()
      .subscribe({
        next: response => {
          this.categories = response.resource;

          const dialogRef = this.dialog.open(PickCategoryDialogComponent, {
            data: this.categories,
          });
          dialogRef.afterClosed().subscribe(result => {
            if (result)
              this.router.navigate(['/registrations/create', {categoryId: result.id}])
          });
        }
      });
  }

  isOverflow(element: HTMLElement): boolean {
    let currentOverflow = element.style.overflow;

    if (!currentOverflow || currentOverflow == "visible") {
      element.style.overflow = "hidden";
    }

    const isOverflowing = element.clientWidth < element.scrollWidth || element.clientHeight < element.scrollHeight;

    element.style.overflow = currentOverflow;
    return isOverflowing;
  }
}
