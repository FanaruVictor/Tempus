import { Injectable } from '@angular/core';
import { Subject, Observable, BehaviorSubject } from 'rxjs';
import { RegistrationOverview } from 'src/app/_commons/models/registrations/registrationOverview';

@Injectable({
  providedIn: 'root',
})
export class RegistrationService {
  registrationsSubject: BehaviorSubject<RegistrationOverview[]>;
  registrations: Observable<RegistrationOverview[]>;
  constructor() {
    this.registrationsSubject = new BehaviorSubject<RegistrationOverview[]>([]);
    this.registrations = this.registrationsSubject.asObservable();
  }

  setRegistrations(registrations: RegistrationOverview[]) {
    this.registrationsSubject.next(registrations);
  }

  addRegistration(newRegistration: RegistrationOverview) {
    const currentRegistrations = this.registrationsSubject.getValue();
    currentRegistrations.push(newRegistration);
    this.setRegistrations(currentRegistrations);
  }

  removeRegistration(registrationId: string) {
    let currentRegistrations = this.registrationsSubject.getValue();
    currentRegistrations = currentRegistrations.filter(
      (x) => x.id !== registrationId
    );

    this.setRegistrations(currentRegistrations);
  }
}
