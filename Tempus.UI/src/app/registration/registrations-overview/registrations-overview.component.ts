import {Component} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {BaseCategory} from "../../commons/models/categories/baseCategory";
import {MatDialog} from "@angular/material/dialog";
import {DetailedRegistration} from "../../commons/models/registrations/detailedRegistration";
import {PickCategoryDialogComponent} from "../pick-category-dialog/pick-category-dialog.component";
import {RegistrationApiService} from "../../commons/services/registration.api.service";
import {CategoryApiService} from "../../commons/services/category.api.service";

@Component({
  selector: 'app-registrations-overview',
  templateUrl: './registrations-overview.component.html',
  styleUrls: ['./registrations-overview.component.scss']
})
export class RegistrationsOverviewComponent {
  registrations?: DetailedRegistration[];
  categories?: BaseCategory[];
  defaultColor = '#d6efef';

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

  getAll(){
    this.registrationApiService.getAll()
      .subscribe({
        next: response => {
          this.registrations = response.resource;
          this.registrations = this.registrations.sort(
            (objA, objB) => new Date(objB.lastUpdatedAt).getTime() - new Date(objA.lastUpdatedAt).getTime(),
          );
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
            if(result)
              this.router.navigate(['/registrations/create', {categoryId: result.id}])
          });
        }
      });
  }
}
