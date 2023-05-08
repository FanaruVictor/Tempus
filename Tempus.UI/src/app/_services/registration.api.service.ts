import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GenericResponse } from '../_commons/models/genericResponse';
import { RegistrationOverview } from '../_commons/models/registrations/registrationOverview';
import { UpdateRegistrationCommandData } from '../_commons/models/registrations/updateRegistrationCommandData';
import { CreateRegistrationCommandData } from '../_commons/models/registrations/createRegistrationCommandData';
import { environment } from '../../environments/environment';
import { BehaviorSubject, Observable } from 'rxjs';
import { RegistrationDetails } from '../_commons/models/registrations/registrationDetails';

@Injectable({
  providedIn: 'root',
})
export class RegistrationApiService {
  apiUrl = `${environment.apiUrl}/v1/registrations`;
  registrationSubject: BehaviorSubject<RegistrationOverview>;
  registration: Observable<RegistrationOverview>;

  constructor(private httpClient: HttpClient) {
    this.registrationSubject = new BehaviorSubject<RegistrationOverview>({
      id: '',
      description: '',
      content: '',
      categoryColor: '',
      lastUpdatedAt: '',
    });
    this.registration = this.registrationSubject.asObservable();
  }

  setRegistration(registration: RegistrationOverview) {
    this.registrationSubject.next(registration);
  }

  getAll() {
    return this.httpClient.get<GenericResponse<RegistrationOverview[]>>(
      this.apiUrl
    );
  }

  getById(id: string, groupId: string | undefined) {
    return this.httpClient.get<GenericResponse<RegistrationDetails>>(
      `${this.apiUrl}/${id}`,
      !!groupId ? { params: { groupId: groupId } } : undefined
    );
  }

  update(registration: UpdateRegistrationCommandData) {
    return this.httpClient.put<GenericResponse<RegistrationDetails>>(
      this.apiUrl,
      registration
    );
  }

  create(registration: CreateRegistrationCommandData) {
    return this.httpClient.post<GenericResponse<RegistrationOverview>>(
      this.apiUrl,
      registration
    );
  }

  delete(id: string, groupId: string | undefined) {
    return this.httpClient.delete<GenericResponse<string>>(
      `${this.apiUrl}/${id}`,
      !!groupId ? { params: { groupId: groupId } } : undefined
    );
  }
}
