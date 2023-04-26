import { Component, ElementRef, ViewChild } from '@angular/core';
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
    const documentDefinition = this.prepareDocument(registration);
    if (documentDefinition == undefined) {
      return;
    }

    pdfMake.createPdf(documentDefinition).download();
  }

  print(registration: RegistrationOverview) {
    const documentDefinition = this.prepareDocument(registration);
    if (documentDefinition == undefined) {
      return;
    }

    pdfMake.createPdf(documentDefinition).print();
  }

  @ViewChild('pdfTable') downloadButton?: ElementRef;
  private prepareDocument(registration: RegistrationOverview): any {
    if (!registration) return undefined;

    registration.content = this.changeToDoList(registration.content);
    const pdfTable = this.downloadButton?.nativeElement || undefined;
    const content = htmlToPdfmake(
      pdfTable ? pdfTable.innerHTML : registration.content
    );
    debugger;
    const documentDefinition = {
      content: [
        {
          text: registration.description,
          style: 'header',
        },
        content,
      ],
      styles: {
        header: {
          fontSize: 17,
          marginBottom: 10,
          bold: true,
          alignment: 'center',
          padding: 10,
        },
      },
      defaultStyle: {
        bold: false,
      },
    };

    return documentDefinition;
  }

  private changeToDoList(content: string): string {
    const regexToDoChecked = /<ul data-checked="true"><li>(.*?)<\/li><\/ul>/g;
    const regexToDoUnchecked =
      /<ul data-checked="false"><li>(.*?)<\/li><\/ul>/g;
    const checkedItems = content.match(regexToDoChecked);
    const uncheckedItems = content.match(regexToDoUnchecked);
    if (!checkedItems && !uncheckedItems) {
      return content;
    }
    checkedItems?.forEach((x) => {
      var itemText = x.replace('<ul data-checked="true"><li>', '');
      itemText = itemText.replace('</li></ul>', '');
      content = content.replace(
        x,
        `<input type="checkbox" checked> <label>${itemText}</label><br>`
      );
    });

    uncheckedItems?.forEach((x) => {
      var itemText = x.replace('<ul data-checked="false"><li>', '');
      itemText = itemText.replace('</li></ul>', '');
      content = content.replace(
        x,
        `<button type="button"> <label>${itemText}</label><br>`
      );
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
