import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {BaseRegistration} from "../../commons/models/registrations/baseRegistration";
import {GenericResponse} from "../../commons/models/genericResponse";
import {CreateRegistrationCommandData} from "../models/registrations/createRegistrationCommandData";
import {UpdateRegistrationCommandData} from "../models/registrations/updateRegistrationCommandData";
import {DetailedRegistration} from "../models/registrations/detailedRegistration";

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
