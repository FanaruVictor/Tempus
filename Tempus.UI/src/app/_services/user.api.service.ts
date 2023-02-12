import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {UserDetails} from "../_commons/models/user/userDetails";
import {GenericResponse} from "../_commons/models/genericResponse";
import {environment} from "../../environments/environment";
import {BehaviorSubject, Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class UserApiService {
  apiUrl = `${environment.apiUrl}/v1/users`;
  userSubject : BehaviorSubject<UserDetails>;
  user: Observable<UserDetails>;

  constructor(private httpClient: HttpClient) {
    this.userSubject = new BehaviorSubject<UserDetails>({
      userName: '',
      photo: undefined,
      password: '',
      isDarkTheme: false,
      email: '',
      phoneNumber: ''
    });
    this.user = this.userSubject.asObservable();
  }

  setUser(user: UserDetails){
    this.userSubject.next(user);
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
