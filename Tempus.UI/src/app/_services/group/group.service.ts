import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { RegistrationOverview } from '../../_commons/models/registrations/registrationOverview';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class GroupService {
  groupId: BehaviorSubject<string | undefined>;
  currentGroupId: Observable<string | undefined>;
  groupRegistrationsSubject: BehaviorSubject<
    RegistrationOverview[] | undefined
  >;
  groupRegistrations: Observable<RegistrationOverview[] | undefined>;

  constructor() {
    this.groupId = new BehaviorSubject<string | undefined>(undefined);
    this.currentGroupId = this.groupId.asObservable();
    this.groupRegistrationsSubject = new BehaviorSubject<
      RegistrationOverview[] | undefined
    >(undefined);
    this.groupRegistrations = this.groupRegistrationsSubject.asObservable();
  }

  setGroupId(groupId: string | undefined) {
    this.groupId.next(groupId);
  }

  setRegistrations(registrations: RegistrationOverview[] | undefined) {
    this.groupRegistrationsSubject.next(registrations);
  }

  addRegistration(newRegistration: RegistrationOverview) {
    ;
    let registrations = [newRegistration];
    const currentRegistrations = this.groupRegistrationsSubject.getValue();
    if (!!currentRegistrations) {
      registrations = registrations.concat([...currentRegistrations]);
    }
    this.setRegistrations(registrations);
  }

  deleteRegistration(registrationId: string) {
    let currentRegistrations = this.groupRegistrationsSubject.getValue();
    if (!!currentRegistrations) {
      currentRegistrations = currentRegistrations.filter(
        (x) => x.id !== registrationId
      );
      this.setRegistrations(currentRegistrations);
    }
  }

  removeAll() {
    this.setRegistrations(undefined);
  }
}
