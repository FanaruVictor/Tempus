import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {BehaviorSubject, Observable} from "rxjs";
import {User} from "../../_commons/models/user/User";
import {map} from "rxjs/operators";

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
    return this.httpClient.post<any>('https://localhost:7077/api/v1.0/Auth/login', {username, password})
      .pipe(map(result => {
        localStorage.setItem('authorizationToken', JSON.stringify(result.resource));
        this.authorizationTokenSubject.next(result.resource);
        return result.resource;
      }));
  }

  register(user: User){
    return this.httpClient.post<any>('https://localhost:7077/api/v1.0/Auth/register', user)
  }

  logout(){
    localStorage.removeItem('authorizationToken');
    this.authorizationTokenSubject.next('');
  }
}
