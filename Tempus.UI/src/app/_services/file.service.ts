import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {BaseRegistration} from "../_commons/models/registrations/baseRegistration";
import {GenericResponse} from "../_commons/models/genericResponse";

@Injectable({
  providedIn: 'root'
})
export class FileService {

  private apiUrl: string = 'https://localhost:7077/api/v1/files';
  constructor(private httpClient: HttpClient) { }

  download(id: string){
    return this.httpClient.get<GenericResponse<string>>(`${this.apiUrl}/download/${id}` );
  }
}
