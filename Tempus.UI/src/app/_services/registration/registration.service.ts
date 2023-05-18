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

  setRegistrations(registrations: RegistrationOverview[]) {
    this.registrationsSubject.next(registrations);
  }

  addRegistration(newRegistration: RegistrationOverview) {
    const currentRegistrations = this.registrationsSubject.getValue();
    if (!!currentRegistrations) {
      currentRegistrations.push(newRegistration);
      this.setRegistrations(currentRegistrations);
    }
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
}