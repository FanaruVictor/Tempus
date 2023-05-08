import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { RegistrationOverview } from 'src/app/_commons/models/registrations/registrationOverview';

@Injectable({
  providedIn: 'root',
})
export class GroupService {
  groupId: BehaviorSubject<string | undefined>;
  currentGroupId: Observable<string | undefined>;
  groupRegistrationsSubject: BehaviorSubject<RegistrationOverview[]>;
  groupRegistrations: Observable<RegistrationOverview[]>;

  constructor() {
    this.groupId = new BehaviorSubject<string | undefined>(undefined);
    this.currentGroupId = this.groupId.asObservable();
    this.groupRegistrationsSubject = new BehaviorSubject<
      RegistrationOverview[]
    >([]);
    this.groupRegistrations = this.groupRegistrationsSubject.asObservable();
  }

  setGroupId(groupId: string | undefined) {
    this.groupId.next(groupId);
  }

  setRegistrations(registrations: RegistrationOverview[]) {
    this.groupRegistrationsSubject.next(registrations);
  }

  addRegistration(newRegistration: RegistrationOverview) {
    const currentRegistrations = this.groupRegistrationsSubject.getValue();
    currentRegistrations.push(newRegistration);
    this.setRegistrations(currentRegistrations);
  }

  deleteRegistration(registrationId: string) {
    let currentRegistrations = this.groupRegistrationsSubject.getValue();
    currentRegistrations = currentRegistrations.filter(
      (x) => x.id !== registrationId
    );

    this.setRegistrations(currentRegistrations);
  }
}
