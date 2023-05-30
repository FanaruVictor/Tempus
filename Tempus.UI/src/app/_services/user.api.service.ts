import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GenericResponse } from '../_commons/models/genericResponse';
import { environment } from '../../environments/environment';
import { UserEmail } from '../_commons/models/user/userEmail';

@Injectable({
  providedIn: 'root',
})
export class UserApiService {
  apiUrl = `${environment.apiUrl}/v1/users`;

  constructor(private httpClient: HttpClient) {
  }

  getDetails() {
    return this.httpClient.get<GenericResponse<any>>(
      `${this.apiUrl}/details`
    );
  }

  changeTheme(isDarkTheme: boolean) {
    return this.httpClient.put<GenericResponse<any>>(
      `${this.apiUrl}/changeTheme`,
      { isDarkTheme: isDarkTheme }
    );
  }

  delete() {
    return this.httpClient.delete<GenericResponse<boolean>>(this.apiUrl);
  }

  update(data) {
    let formData = new FormData();
    if (!!data.newPhoto) formData.append('newPhoto', data.newPhoto);
    formData.append(
      'isPhotoChanged',
      data.isCurrentPhotoChanged ? 'true' : 'false'
    );
    formData.append('email', data.email);

    return this.httpClient.put<GenericResponse<any>>(
      this.apiUrl,
      formData
    );
  }

  getUserEmails() {
    return this.httpClient.get<GenericResponse<UserEmail[]>>(
      `${this.apiUrl}/emails`
    );
  }
}
