import {Component} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {GenericResponse} from "../../commons/models/genericResponse";
import {BaseCategory} from "../../commons/models/categories/baseCategory";
import {MatDialog} from "@angular/material/dialog";
import {DetailedRegistration} from "../../commons/models/registrations/detailedRegistration";
import {PickCategoryDialogComponent} from "../pick-category-dialog/pick-category-dialog.component";

@Component({
  selector: 'app-registrations-overview',
  templateUrl: './registrations-overview.component.html',
  styleUrls: ['./registrations-overview.component.scss']
})
export class RegistrationsOverviewComponent {
  registrations?: DetailedRegistration[];
  categories?: BaseCategory[];

  constructor(private httpClient: HttpClient, private router: Router, private dialog: MatDialog) {
  }

  ngOnInit(): void {
    this.httpClient.get<GenericResponse<DetailedRegistration[]>>(`https://localhost:7077/api/registrations`)
      .subscribe({
        next: response => {
          console.log(response)
          this.registrations = response.resource;
        }
      });
  }

  openDialog(): void {
    this.httpClient.get<GenericResponse<BaseCategory[]>>(`https://localhost:7077/api/categories`)
      .subscribe({
        next: response => {
          this.categories = response.resource;

          const dialogRef = this.dialog.open(PickCategoryDialogComponent, {
            data: this.categories,
          });
          dialogRef.afterClosed().subscribe(result => {
            if(result)
              this.router.navigate(['/create', {categoryId: result.id}])
          });
        }
      });
  }
}
