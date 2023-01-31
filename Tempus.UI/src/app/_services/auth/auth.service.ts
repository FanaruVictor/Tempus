import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {BehaviorSubject, Observable} from "rxjs";
import {BaseUser} from "../../_commons/models/user/baseUser";
import {map} from "rxjs/operators";
import {GenericResponse} from "../../_commons/models/genericResponse";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private authorizationTokenSubject: BehaviorSubject<string>;
    authorizationToken: Observable<string>;

  constructor(private httpClient: HttpClient) {
    this.authorizationTokenSubject = new BehaviorSubject<string>(JSON.parse(localStorage.getItem('authorizationToken')!));
    this.authorizationToken = this.authorizationTokenSubject.asObservable();
  }

  get authorizationTokenValue(): string{
    return this.authorizationTokenSubject.value;
  }

  login(username: string, password: string){
    return this.httpClient.post<GenericResponse<string>>('https://localhost:7077/api/v1.0/auth/login', {username, password})
      .pipe(map(result => {
        localStorage.setItem('authorizationToken', JSON.stringify(result.resource));
        this.authorizationTokenSubject.next(result.resource);
        return result.resource;
      }));
  }

  register(user: BaseUser){
    return this.httpClient.post<GenericResponse<BaseUser>>('https://localhost:7077/api/v1.0/auth/register', user)
  }

  logout(){
    localStorage.removeItem('authorizationToken');
    this.authorizationTokenSubject.next('');
  }
}
