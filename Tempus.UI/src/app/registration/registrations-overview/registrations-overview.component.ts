import {Component} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {MatDialog} from "@angular/material/dialog";
import {PickCategoryDialogComponent} from "../pick-category-dialog/pick-category-dialog.component";
import {RegistrationApiService} from "../../_services/registration.api.service";
import {CategoryApiService} from "../../_services/category.api.service";
import {RegistrationOverview} from "../../_commons/models/registrations/registrationOverview";
import {BaseCategory} from "../../_commons/models/categories/baseCategory";
import {FormControl, FormGroup} from "@angular/forms";
import {filter} from "rxjs";
import {FileService} from "../../_services/file.service";
import {NotificationService} from "../../_services/notification.service";

@Component({
  selector: 'app-registrations-overview',
  templateUrl: './registrations-overview.component.html',
  styleUrls: ['./registrations-overview.component.scss']
})
export class RegistrationsOverviewComponent {
  registrations?: RegistrationOverview[];
  categories?: BaseCategory[];
  defaultColor = '#d6efef';
  searchText = '';
  maxDate: Date;
  minDate: Date;

  dateRange = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });

  colors = new FormControl('');

  constructor(
    private httpClient: HttpClient,
    private router: Router,
    private dialog: MatDialog,
    private registrationApiService: RegistrationApiService,
    private categoryApiService: CategoryApiService,
    private fileService: FileService,
    private notificationService: NotificationService
  ) {
    const currentYear = new Date().getFullYear();
    this.minDate = new Date(currentYear - 30, 0, 1);
    this.maxDate = new Date();
  }

  ngOnInit(): void {
    this.getAll();
    this.categoryApiService.getAll()
      .subscribe(response => {
        this.categories = response.resource;
      })

  }

  getAll() {
    this.registrationApiService.getAll()
      .subscribe({
        next: response => {
          this.registrations = response.resource;
          this.registrations = this.registrations?.sort(
            (objA, objB) => new Date(objB.createdAt).getTime() - new Date(objA.createdAt).getTime(),
          );
          console.log(!!this.registrations)
        }
      });
  }

  addRegistration(): void {
    const dialogRef = this.dialog.open(PickCategoryDialogComponent, {
      data: this.categories,
    });
    dialogRef.afterClosed()
      .pipe(filter(x => !!x))
      .subscribe(result => {
        this.router.navigate(['/registrations/create', {categoryId: result.id}])
      });
  }

  delete(id: string) {
    this.registrationApiService.delete(id).subscribe(result => {
        this.registrations = this.registrations?.filter(x => x.id !== result.resource)
        this.notificationService.succes('Registration deleted successfully', 'Request completed');
    });
  }

  download(registration: RegistrationOverview): void {
    if (!!registration)
      this.fileService.download(registration.id).subscribe({
        next: data => {
          let FileSaver = require('file-saver');
          const byteCharacters = atob(data.resource);
          const byteNumbers = new Array(byteCharacters.length);
          for (let i = 0; i < byteCharacters.length; i++) {
            byteNumbers[i] = byteCharacters.charCodeAt(i);
          }
          const byteArray = new Uint8Array(byteNumbers);
          const blob = new Blob([byteArray], {type: "application/pdf"});
          FileSaver.saveAs(blob, `${registration.title}.pdf`);
          this.notificationService.succes('Registration downloaded successfully', 'Request completed');
        }
      });
  }
}
