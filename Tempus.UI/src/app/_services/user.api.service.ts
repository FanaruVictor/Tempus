import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {UserDetails} from "../_commons/models/user/userDetails";
import {GenericResponse} from "../_commons/models/genericResponse";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class UserApiService {
  apiUrl =  `${environment.apiUrl}/v1/users`;

  constructor(private httpClient: HttpClient) { }

  getDetails(){
    return this.httpClient.get<GenericResponse<UserDetails>>(`${this.apiUrl}/details`);
  }

  changeTheme(theme: boolean) {
    return this.httpClient.put<GenericResponse<UserDetails>>(`${this.apiUrl}/changeTheme`, {theme: theme});
  }

  getTheme(){
    return this.httpClient.get<GenericResponse<boolean>>(`${this.apiUrl}/theme`);
  }

  uploadPicture(file: File){
    return this.httpClient.post<any>(`${environment.apiUrl}/v1/profilePhoto`, file);
  }
}
