import { Component, OnDestroy, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { PickCategoryDialogComponent } from '../pick-category-dialog/pick-category-dialog.component';
import { RegistrationApiService } from '../../_services/registration/registration.api.service';
import { CategoryApiService } from '../../_services/category.api.service';
import { RegistrationOverview } from '../../_commons/models/registrations/registrationOverview';
import { BaseCategory } from '../../_commons/models/categories/baseCategory';
import { FormControl, FormGroup } from '@angular/forms';
import { filter, Subscription } from 'rxjs';
import { NotificationService } from '../../_services/notification.service';
import { GroupService } from 'src/app/_services/group/group.service';

import { jsPDF } from 'jspdf';
import { GroupApiService } from 'src/app/_services/group/group.api.service';
import { RegistrationService } from 'src/app/_services/registration/registration.service';
import { ViewportScroller } from '@angular/common';

@Component({
  selector: 'app-notes',
  templateUrl: './notes.component.html',
  styleUrls: ['./notes.component.scss'],
})
export class NotesComponent implements OnInit, OnDestroy {
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
  showNoRegistrationSelectedMessage = true;

  constructor(
    private router: Router,
    private dialog: MatDialog,
    private registrationApiService: RegistrationApiService,
    private categoryApiService: CategoryApiService,
    private notificationService: NotificationService,
    private groupService: GroupService,
    private groupApiService: GroupApiService,
    private regisrationService: RegistrationService
  ) {
    const currentYear = new Date().getFullYear();
    this.minDate = new Date(currentYear - 30, 0, 1);
    this.maxDate = new Date();
  }

  router$?: Subscription;
  currentGroupId$?: Subscription;
  groupsRegistrations$?: Subscription;

  ngOnInit(): void {
    this.currentGroupId$ = this.groupService.currentGroupId.subscribe((x) => {
      this.groupId = x;

      if (!!this.groupId) {
        this.getAllForGroup();
        this.groupsRegistrations$ =
          this.groupService.groupRegistrations.subscribe((x) => {
            this.registrations = x;
            this.setRegistrationsColors();
          });
        return;
      }

      this.regisrationService.registrations.subscribe((x) => {
        this.registrations = x;
        if (!this.registrations) {
          this.getAllForUser();
        } else {
          this.setRegistrationsColors();
        }
      });
    });

    this.updateFocusedRegistration();

    if (!this.router.url.endsWith('notes')) {
      this.showNoRegistrationSelectedMessage = false;
    }

    this.router$ = this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        if (event.url.endsWith('notes')) {
          this.showNoRegistrationSelectedMessage = true;
        } else {
          this.showNoRegistrationSelectedMessage = false;
        }
        if (event.url.startsWith(`/groups/${this.groupId}/notes`)) {
          this.categoryApiService.getAll(this.groupId).subscribe((response) => {
            this.categories = response.resource;
          });
        }
      }
    });

    this.categoryApiService.getAll(this.groupId).subscribe((response) => {
      this.categories = response.resource;
    });
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

  private getAllForUser() {
    this.registrationApiService.getAll().subscribe((response) => {
      this.handleGetRegistrationResponse(response.resource);
    });
  }

  private getAllForGroup() {
    if (!this.groupId) {
      return;
    }
    this.groupApiService
      .getAllRegistrations(this.groupId)
      .subscribe((response) => {
        this.handleGetRegistrationResponse(response.resource);
      });
  }

  private handleGetRegistrationResponse(registrations: RegistrationOverview[]) {
    this.registrations = registrations;

    this.setRegistrationsColors();

    this.sortRegistrationByDate();

    this.modifyRegistrations();
  }

  setRegistrationsColors() {
    this.registrationsColor = [];
    this.registrations?.forEach((x) => {
      if (!this.registrationsColor.includes(x.categoryColor)) {
        this.registrationsColor.push(x.categoryColor);
      }
    });
  }

  modifyRegistrations() {
    !!this.registrations
      ? !!this.groupId
        ? this.groupService.setRegistrations(this.registrations)
        : this.regisrationService.setRegistrations(this.registrations)
      : null;
  }

  addRegistration(): void {
    const dialogRef = this.dialog.open(PickCategoryDialogComponent, {
      data: { data: this.categories, groupId: this.groupId },
    });
    dialogRef
      .afterClosed()
      .pipe(filter((x) => !!x))
      .subscribe((result) => {
        if (!!this.groupId) {
          this.router.navigate([`groups/${this.groupId}/notes/create`], {
            queryParams: { categoryId: result.id },
          });
          return;
        }
        this.router.navigate(['notes/create'], {
          queryParams: { categoryId: result.id },
        });
      });
  }

  delete(id: string) {
    this.registrationApiService.delete(id, this.groupId).subscribe((result) => {
      this.registrationsColor = [];
      this.colors.setValue([]);
      this.registrations?.forEach((x) => {
        if (!this.registrationsColor.includes(x.categoryColor)) {
          this.registrationsColor.push(x.categoryColor);
        }
      });

      if (!this.groupId && !!this.registrations) {
        this.regisrationService.removeRegistration(result.resource);
      }

      this.notificationService.succes(
        'Registration deleted successfully',
        'Request completed'
      );

      this.showNoRegistrationSelectedMessage = true;
      if (!!this.groupId) {
        this.router.navigate([`groups/${this.groupId}/notes`]);
        return;
      }
      this.router.navigate(['/notes']);
    });
  }

  download(registration: RegistrationOverview): void {
    this.prepareDocument(registration);
  }

  private prepareDocument(registration: RegistrationOverview): any {
    if (!registration) return undefined;

    registration.content = this.changeToDoList(registration.content);
    const doc = new jsPDF();

    const elementHtml = document.createElement('div');
    elementHtml.innerHTML = registration.content;
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
        'notes',
        id,
        'edit-full-view',
      ]);
      return;
    }
    this.router.navigate(['/notes', id, 'edit-full-view']);
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
        'notes',
        id,
        'edit-partial-view',
      ]);
      return;
    }
    this.router.navigate(['/notes', id, 'edit-partial-view']);
  }

  excludeClick(event: MouseEvent) {
    event.stopPropagation();
  }

  ngOnDestroy(): void {
    this.router$?.unsubscribe();
    this.currentGroupId$?.unsubscribe();
    this.groupsRegistrations$?.unsubscribe();
  }
}
