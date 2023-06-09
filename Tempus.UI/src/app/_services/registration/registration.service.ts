import { Injectable } from '@angular/core';
import { Subject, Observable, BehaviorSubject } from 'rxjs';
import { RegistrationOverview } from '../../_commons/models/registrations/registrationOverview';

@Injectable({
  providedIn: 'root',
})
export class RegistrationService {
  registrationsSubject: BehaviorSubject<RegistrationOverview[] | undefined>;
  registrations: Observable<RegistrationOverview[] | undefined>;
  constructor() {
    this.registrationsSubject = new BehaviorSubject<
      RegistrationOverview[] | undefined
    >(undefined);
    this.registrations = this.registrationsSubject.asObservable();
  }

  setRegistrations(registrations: RegistrationOverview[] | undefined) {
    this.registrationsSubject.next(registrations);
  }

  addRegistration(newRegistration: RegistrationOverview) {
    ;
    let registrations = [newRegistration];
    const currentRegistrations = this.registrationsSubject.getValue();
    if (!!currentRegistrations) {
      registrations = registrations.concat([...currentRegistrations]);
    }
    this.setRegistrations(registrations);
  }

  removeRegistration(registrationId: string) {
    let currentRegistrations = this.registrationsSubject.getValue();
    if (!!currentRegistrations) {
      currentRegistrations = currentRegistrations.filter(
        (x) => x.id !== registrationId
      );
      this.setRegistrations(currentRegistrations);
    }
  }

  removeAllWithColor(color: string) {
    let currentRegistrations = this.registrationsSubject.getValue();
    if (!!currentRegistrations) {
      currentRegistrations = currentRegistrations.filter(
        (x) => x.categoryColor !== color
      );
      this.setRegistrations(currentRegistrations);
    }
  }

  updateAllWithOldColor(oldColor: string, newColor: string) {
    debugger;
    let currentRegistrations = this.registrationsSubject.getValue();
    if (!!currentRegistrations) {
      currentRegistrations = currentRegistrations.map((x) => {
        if (x.categoryColor === oldColor) {
          x.categoryColor = newColor;
        }
        return x;
      });
      this.setRegistrations(currentRegistrations);
    }
  }
}
