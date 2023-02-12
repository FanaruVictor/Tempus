import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {GenericResponse} from "../_commons/models/genericResponse";
import {DetailedRegistration} from "../_commons/models/registrations/detailedRegistration";
import {BaseRegistration} from "../_commons/models/registrations/baseRegistration";
import {UpdateRegistrationCommandData} from "../_commons/models/registrations/updateRegistrationCommandData";
import {CreateRegistrationCommandData} from "../_commons/models/registrations/createRegistrationCommandData";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class RegistrationApiService {
  apiUrl = `${environment.apiUrl}/v1/registrations`

  constructor(private httpClient: HttpClient) {
  }

  getAll() {
    return this.httpClient.get<GenericResponse<DetailedRegistration[]>>(this.apiUrl);
  }

  getById(id: string) {
    return this.httpClient.get<GenericResponse<BaseRegistration>>(`${this.apiUrl}/${id}`);
  }

  update(data: UpdateRegistrationCommandData) {
    return this.httpClient.put<GenericResponse<BaseRegistration>>(this.apiUrl, data);
  }

  create(registration: CreateRegistrationCommandData) {
    return this.httpClient.post<GenericResponse<BaseRegistration>>(this.apiUrl, registration);
  }

  delete(id: string) {
    return this.httpClient.delete<GenericResponse<string>>(`${this.apiUrl}/${id}`);
  }
}
