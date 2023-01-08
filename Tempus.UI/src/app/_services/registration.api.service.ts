import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {GenericResponse} from "../_commons/models/genericResponse";
import {DetailedRegistration} from "../_commons/models/registrations/detailedRegistration";
import {BaseRegistration} from "../_commons/models/registrations/baseRegistration";
import {UpdateRegistrationCommandData} from "../_commons/models/registrations/updateRegistrationCommandData";
import {CreateRegistrationCommandData} from "../_commons/models/registrations/createRegistrationCommandData";

@Injectable({
  providedIn: 'root'
})
export class RegistrationApiService {

  constructor(private httpClient: HttpClient) { }

  getAll(){
    return this.httpClient.get<GenericResponse<DetailedRegistration[]>>(`https://localhost:7077/api/v1/registrations`);
  }

  getById(id: string){
    return this.httpClient.get<GenericResponse<BaseRegistration>>(`https://localhost:7077/api/v1/registrations/${id}`);
  }

  update(data: UpdateRegistrationCommandData){
    return this.httpClient.put<GenericResponse<BaseRegistration>>(`https://localhost:7077/api/v1/registrations`, data);
  }

  create(registration: CreateRegistrationCommandData){
    return this.httpClient.post<GenericResponse<BaseRegistration>>('https://localhost:7077/api/v1/registrations', registration);
  }

  delete(id: string){
    return this.httpClient.delete<GenericResponse<string>>(`https://localhost:7077/api/v1/registrations/${id}`);
  }
}
