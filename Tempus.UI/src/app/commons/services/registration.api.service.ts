import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {BaseRegistration} from "../../commons/models/registrations/baseRegistration";
import {GenericResponse} from "../../commons/models/genericResponse";
import {CreateRegistration} from "../models/registrations/createRegistration";

@Injectable({
  providedIn: 'root'
})
export class RegistrationApiService {

  constructor(private httpClient: HttpClient) { }

  delete(id: string){
    return this.httpClient.delete<GenericResponse<string>>(`https://localhost:7077/api/registrations/${id}`);
  }

  create(registration: CreateRegistration){
    return this.httpClient.post<GenericResponse<BaseRegistration>>('https://localhost:7077/api/registrations', registration);
  }
}
