import {Component, OnChanges, SimpleChanges} from '@angular/core';
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
import * as pdfMake from "pdfmake/build/pdfmake";
import * as pdfFonts from "pdfmake/build/vfs_fonts";
const htmlToPdfmake = require("html-to-pdfmake");
(pdfMake as any).vfs = pdfFonts.pdfMake.vfs;

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
  registrationsColor: string[] = [];

  dateRange = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });

  colors = new FormControl([]);

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

  private getAll() {
    this.registrationApiService.getAll()
      .subscribe({
        next: response => {
          this.registrations = response.resource;
          this.registrations = this.registrations?.sort(
            (objA, objB) => new Date(objB.createdAt).getTime() - new Date(objA.createdAt).getTime(),
          );

          this.registrations?.forEach(x => {
            if(!this.registrationsColor.includes(x.categoryColor)){
              this.registrationsColor.push(x.categoryColor);
            }
          });
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
      this.registrationsColor = [];
      this.registrations?.forEach(x => {
        if(!this.registrationsColor.includes(x.categoryColor)){
          this.registrationsColor.push(x.categoryColor);
        }
      });
      this.notificationService.succes('Registration deleted successfully', 'Request completed');
    });
  }

  download(registration: RegistrationOverview): void {
    const documentDefinition = this.prepareDocument(registration);
    if(documentDefinition == undefined){
      return;
    }

    pdfMake.createPdf(documentDefinition).download();
  }

  print(registration: RegistrationOverview){
    const documentDefinition = this.prepareDocument(registration);
    if(documentDefinition == undefined){
      return;
    }

    pdfMake.createPdf(documentDefinition).print();
  }

  private prepareDocument(registration: RegistrationOverview) :any {
    if (!registration)
      return undefined;

    const html = htmlToPdfmake(registration.content);
    const documentDefinition = {
      content: [
        {text: registration.description, style: 'header'},
        html
      ],
      styles: {
        header: {
          fontSize: 17,
          marginBottom: 10,
          bold: true,
          alignment: 'left'
        },
      },
      defaultStyle:{
        bold: false
      }
    };

    return documentDefinition;
  }

  modifyContent(values: string[]){
    let element = document.getElementsByClassName('mat-select-value')[0];
    element.innerHTML = '';
    values.forEach(x =>
      element.innerHTML = element.innerHTML.concat(`<div style="
        background-color: ${x};
        height: 15px;
        width: 15px;
        margin-right: 5px;"></div>`)
    );
  }

  resetDate(){
    this.dateRange.reset();
  }
}
