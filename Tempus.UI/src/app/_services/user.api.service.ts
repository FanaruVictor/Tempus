import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {UserDetails} from "../_commons/models/user/userDetails";
import {GenericResponse} from "../_commons/models/genericResponse";
import {environment} from "../../environments/environment";
import {BehaviorSubject, Observable} from "rxjs";
import {Photo} from "../_commons/models/photo/photo";

@Injectable({
  providedIn: 'root'
})
export class UserApiService {
  apiUrl = `${environment.apiUrl}/v1/users`;
  userSubject: BehaviorSubject<UserDetails>;
  user: Observable<UserDetails>;

  constructor(private httpClient: HttpClient) {
    this.userSubject = new BehaviorSubject<UserDetails>({
      id: '',
      userName: '',
      photo: undefined,
      isDarkTheme: false,
      email: '',
      phoneNumber: '',
      externalId: ''
    });
    this.user = this.userSubject.asObservable();
  }

  setUser(user: UserDetails) {
    this.userSubject.next(user);
  }

  getDetails() {
    return this.httpClient.get<GenericResponse<UserDetails>>(`${this.apiUrl}/details`);
  }

  changeTheme(isDarkTheme: boolean) {
    return this.httpClient.put<GenericResponse<UserDetails>>(`${this.apiUrl}/changeTheme`, {isDarkTheme: isDarkTheme});
  }

  delete() {
    return this.httpClient.delete<GenericResponse<any>>(`${this.apiUrl}`);
  }
}
