import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {GenericResponse} from "../_commons/models/genericResponse";
import {RegistrationOverview} from "../_commons/models/registrations/registrationOverview";
import {BaseRegistration} from "../_commons/models/registrations/baseRegistration";
import {UpdateRegistrationCommandData} from "../_commons/models/registrations/updateRegistrationCommandData";
import {CreateRegistrationCommandData} from "../_commons/models/registrations/createRegistrationCommandData";
import {environment} from "../../environments/environment";
import {RegistrationDetails} from "../_commons/models/registrations/RegistrationDetails";
import {BehaviorSubject, Observable} from "rxjs";
import {UserDetails} from "../_commons/models/user/userDetails";

@Injectable({
  providedIn: 'root'
})
export class RegistrationApiService {
  apiUrl = `${environment.apiUrl}/v1/registrations`;

  constructor(private httpClient: HttpClient) {
  }

  getAll(groupId: string) {
    return this.httpClient.get<GenericResponse<RegistrationOverview[]>>(this.apiUrl, {params: {groupId: groupId}});
  }

  getById(id: string) {
    return this.httpClient.get<GenericResponse<RegistrationDetails>>(`${this.apiUrl}/${id}`);
  }

  update(data: UpdateRegistrationCommandData) {
    return this.httpClient.put<GenericResponse<RegistrationDetails>>(this.apiUrl, data);
  }

  create(registration: CreateRegistrationCommandData) {
    return this.httpClient.post<GenericResponse<RegistrationDetails>>(this.apiUrl, registration);
  }

  delete(id: string) {
    return this.httpClient.delete<GenericResponse<string>>(`${this.apiUrl}/${id}`);
  }
}
