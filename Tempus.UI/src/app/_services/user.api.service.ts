import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {UserDetails} from "../_commons/models/user/userDetails";
import {GenericResponse} from "../_commons/models/genericResponse";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class UserApiService {
  apiUrl = `${environment.apiUrl}/v1/users`;

  constructor(private httpClient: HttpClient) {
  }

  getDetails() {
    return this.httpClient.get<GenericResponse<UserDetails>>(`${this.apiUrl}/details`);
  }

  changeTheme(isDarkTheme: boolean) {
    return this.httpClient.put<GenericResponse<UserDetails>>(`${this.apiUrl}/changeTheme`, {isDarkTheme: isDarkTheme});
  }

  getTheme() {
    return this.httpClient.get<GenericResponse<boolean>>(`${this.apiUrl}/theme`);
  }

  addPhoto(file: File) {
    let formData = new FormData();
    formData.append('image', file);

    return this.httpClient.post<any>(`${this.apiUrl}/profilePhoto`, formData);
  }

  updatePhoto(currentPhotoId: string, file: File) {
    let formData = new FormData();
    formData.append('id', currentPhotoId);
    formData.append('image', file);

    return this.httpClient.put<any>(`${this.apiUrl}/profilePhoto`, formData);
  }
}
