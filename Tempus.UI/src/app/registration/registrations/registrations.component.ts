import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { PickCategoryDialogComponent } from '../pick-category-dialog/pick-category-dialog.component';
import { RegistrationApiService } from '../../_services/registration.api.service';
import { CategoryApiService } from '../../_services/category.api.service';
import { RegistrationOverview } from '../../_commons/models/registrations/registrationOverview';
import { BaseCategory } from '../../_commons/models/categories/baseCategory';
import { FormControl, FormGroup } from '@angular/forms';
import { filter } from 'rxjs';
import { NotificationService } from '../../_services/notification.service';
import * as pdfMake from 'pdfmake/build/pdfmake';
import * as pdfFonts from 'pdfmake/build/vfs_fonts';
import { GroupService } from 'src/app/_services/group/group.service';

const htmlToPdfmake = require('html-to-pdfmake');
import { jsPDF } from 'jspdf';
(pdfMake as any).vfs = pdfFonts.pdfMake.vfs;

@Component({
  selector: 'app-registrations',
  templateUrl: './registrations.component.html',
  styleUrls: ['./registrations.component.scss'],
})
export class RegistrationsComponent {
  groupId: string | undefined;
  registrations?: RegistrationOverview[];
  categories?: BaseCategory[];
  searchText = '';
  maxDate: Date;
  minDate: Date;
  registrationsColor: string[] = [];

  dateRange = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });

  colors = new FormControl([]);
  showNoRegistrationSelectedMessage = false;

  constructor(
    private router: Router,
    private dialog: MatDialog,
    private registrationApiService: RegistrationApiService,
    private categoryApiService: CategoryApiService,
    private notificationService: NotificationService,
    private groupService: GroupService
  ) {
    const currentYear = new Date().getFullYear();
    this.minDate = new Date(currentYear - 30, 0, 1);
    this.maxDate = new Date();
  }

  ngOnInit(): void {
    const urlSections = this.router.url.split('/');
    if (urlSections.length == 2) {
      this.showNoRegistrationSelectedMessage = true;
    }

    this.groupService.currentGroupId.subscribe((x) => {
      this.groupId = x;
      this.getAll();
    });

    this.categoryApiService.getAll(this.groupId).subscribe((response) => {
      this.categories = response.resource;
    });

    this.updateFocusedRegistration();
  }

  updateFocusedRegistration() {
    this.registrationApiService.registration.subscribe((x) => {
      this.registrations = this.registrations?.map((registration) => {
        if (registration.id === x.id) {
          registration.description = x.description;
          registration.lastUpdatedAt = x.lastUpdatedAt;
          registration.content = x.content;
          registration.categoryColor = x.categoryColor;
        }
        return registration;
      });

      this.sortRegistrationByDate();
    });
  }

  private sortRegistrationByDate() {
    this.registrations = this.registrations?.sort(
      (objA, objB) =>
        new Date(objB.lastUpdatedAt).getTime() -
        new Date(objA.lastUpdatedAt).getTime()
    );
  }

  private getAll() {
    this.registrationApiService.getAll(this.groupId).subscribe({
      next: (response) => {
        this.registrations = response.resource;
        this.registrations = this.registrations?.sort(
          (objA, objB) =>
            new Date(objB.lastUpdatedAt).getTime() -
            new Date(objA.lastUpdatedAt).getTime()
        );

        this.sortRegistrationByDate();
      },
    });
  }

  addRegistration(): void {
    const dialogRef = this.dialog.open(PickCategoryDialogComponent, {
      data: this.categories,
    });
    dialogRef
      .afterClosed()
      .pipe(filter((x) => !!x))
      .subscribe((result) => {
        console.log(this.groupId);
        if (!!this.groupId) {
          this.router.navigate(
            [`groups/${this.groupId}/registrations/create`],
            { queryParams: { categoryId: result.id } }
          );
          return;
        }
        this.router.navigate(['registrations/create'], {
          queryParams: { categoryId: result.id },
        });
      });
  }

  delete(id: string) {
    this.registrationApiService.delete(id, this.groupId).subscribe((result) => {
      this.registrations = this.registrations?.filter(
        (x) => x.id !== result.resource
      );
      this.registrationsColor = [];
      this.registrations?.forEach((x) => {
        if (!this.registrationsColor.includes(x.categoryColor)) {
          this.registrationsColor.push(x.categoryColor);
        }
      });
      this.notificationService.succes(
        'Registration deleted successfully',
        'Request completed'
      );
    });
  }

  download(registration: RegistrationOverview): void {
    this.prepareDocument(registration);
    // if (documentDefinition == undefined) {
    //   return;
    // }

    // pdfMake.createPdf(documentDefinition).download();
  }

  print(registration: RegistrationOverview) {
    const documentDefinition = this.prepareDocument(registration);
    if (documentDefinition == undefined) {
      return;
    }

    pdfMake.createPdf(documentDefinition).print();
  }

  private prepareDocument(registration: RegistrationOverview): any {
    if (!registration) return undefined;

    registration.content = this.changeToDoList(registration.content);
    const doc = new jsPDF();

    const elementHtml = document.createElement('div');
    elementHtml.innerHTML = registration.content;
    debugger;
    doc.text(registration.description, 100, 10, {
      align: 'center',
      maxWidth: 100,
    });

    doc.html(elementHtml.innerHTML, {
      callback: function (doc) {
        doc.canvas.height = 72 * 11;
        doc.save('file.pdf');
      },
      margin: [10, 10, 10, 10],
      autoPaging: 'text',
      x: 10,
      y: 15,
      width: 190, //target width in the PDF document
      windowWidth: 675,
      //window width in CSS pixels
    });
  }

  private changeToDoList(content: string): string {
    const regexToDoChecked = /<ul data-checked="true">(.*?)<\/ul>/g;
    const regexToDoUnchecked = /<ul data-checked="false">(.*?)<\/ul>/g;
    debugger;
    const checkedItems = content.match(regexToDoChecked);
    const uncheckedItems = content.match(regexToDoUnchecked);
    if (!checkedItems && !uncheckedItems) {
      return content;
    }
    checkedItems?.forEach((x) => {
      var itemText = x.replace('<ul data-checked="true">', '');
      itemText = itemText.replace('</ul>', '');
      let checks = itemText.split('</li><li>');

      checks = checks.map((x) => {
        x = x.replace('<li>', '');
        x = x.replace('</li>', '');
        return x;
      });

      let replace = '';
      checks.forEach((x) => {
        replace = replace.concat(
          `<input type="checkbox" checked> <label>${x}</label><br>`
        );
      });

      content = content.replace(x, replace);
    });

    uncheckedItems?.forEach((x) => {
      var itemText = x.replace('<ul data-checked="false">', '');
      itemText = itemText.replace('</ul>', '');
      let checks = itemText.split('</li><li>');

      checks = checks.map((x) => {
        x = x.replace('<li>', '');
        x = x.replace('</li>', '');
        return x;
      });

      let replace = '';
      checks.forEach((x) => {
        replace = replace.concat(
          `<input type="checkbox"> <label>${x}</label><br>`
        );
      });

      content = content.replace(x, replace);
    });
    return content;
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

  resetDate() {
    this.dateRange.reset();
  }

  redirectToEditPage(id: string) {
    const registration = this.registrations?.find((x) => x.id === id);
    if (!registration) {
      return;
    }

    this.registrationApiService.setRegistration(registration);
    if (!!this.groupId) {
      this.router.navigate([
        '/groups',
        this.groupId,
        'registrations',
        id,
        'edit-full-view',
      ]);
      return;
    }
    this.router.navigate(['/registrations', id, 'edit-full-view']);
  }

  openEditContainer(id: string) {
    this.showNoRegistrationSelectedMessage = false;
    const registration = this.registrations?.find((x) => x.id === id);
    if (!registration) {
      return;
    }

    this.registrationApiService.setRegistration(registration);
    if (!!this.groupId) {
      this.router.navigate([
        '/groups',
        this.groupId,
        'registrations',
        id,
        'edit-registrations-view',
      ]);
      return;
    }
    this.router.navigate(['/registrations', id, 'edit-registrations-view']);
  }

  excludeClick(event: MouseEvent) {
    event.stopPropagation();
  }
}
