import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { BaseUser } from '../../_commons/models/user/baseUser';
import { map } from 'rxjs/operators';
import { GenericResponse } from '../../_commons/models/genericResponse';
import { UserDetails } from '../../_commons/models/user/userDetails';
import { LoginResult } from '../../_commons/models/auth/loginResult';
import { UserRegistration } from '../../_commons/models/user/userRegistration';
import { FacebookLoginInfo } from 'src/app/_commons/models/auth/facebookLoginInfo';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  authorizationToken: Observable<string>;
  private authorizationTokenSubject: BehaviorSubject<string>;
  userSubject: BehaviorSubject<UserDetails>;
  user: Observable<UserDetails>;
  defaultUser = {
    id: '',
    userName: '',
    photo: undefined,
    isDarkTheme: false,
    email: '',
    phoneNumber: '',
    externalId: '',
  };

  constructor(private httpClient: HttpClient) {
    this.authorizationTokenSubject = new BehaviorSubject<string>(
      JSON.parse(localStorage.getItem('authorizationToken')!)
    );
    let user = localStorage.getItem('currentUser');
    if (user) {
      this.userSubject = new BehaviorSubject<UserDetails>(JSON.parse(user));
    } else {
      this.userSubject = new BehaviorSubject<UserDetails>(this.defaultUser);
    }
    this.authorizationToken = this.authorizationTokenSubject.asObservable();
    this.user = this.userSubject.asObservable();
  }
  setUser(user: UserDetails) {
    localStorage.setItem('currentUser', JSON.stringify(user));
    this.userSubject.next(user);
  }
  login(email: string, password: string) {
    return this.httpClient
      .post<GenericResponse<LoginResult>>(
        'https://localhost:7077/api/v1.0/auth/login',
        {
          email,
          password,
        }
      )
      .pipe(
        map((result) => {
          localStorage.setItem(
            'authorizationToken',
            JSON.stringify(result.resource.authorizationToken)
          );
          localStorage.setItem(
            'currentUser',
            JSON.stringify(result.resource.user)
          );
          this.authorizationTokenSubject.next(
            result.resource.authorizationToken
          );
          return result.resource;
        })
      );
  }

  loginWithGoogle(googleToken: string) {
    return this.httpClient
      .post<GenericResponse<LoginResult>>(
        'https://localhost:7077/api/v1.0/auth/loginWithGoogle',
        {
          googleToken,
        }
      )
      .pipe(
        map((result) => {
          localStorage.setItem(
            'authorizationToken',
            JSON.stringify(result.resource.authorizationToken)
          );
          localStorage.setItem(
            'currentUser',
            JSON.stringify(result.resource.user)
          );
          this.authorizationTokenSubject.next(
            result.resource.authorizationToken
          );
          return result.resource;
        })
      );
  }

  loginWithFacebook(facebookLoginInfo: FacebookLoginInfo) {
    return this.httpClient
      .post<GenericResponse<LoginResult>>(
        'https://localhost:7077/api/v1.0/auth/loginWithFacebook',
        {
          email: facebookLoginInfo.email,
          externalId: facebookLoginInfo.externalId,
          photoUrl: facebookLoginInfo.photoUrl,
          username: facebookLoginInfo.username,
        }
      )
      .pipe(
        map((result) => {
          localStorage.setItem(
            'authorizationToken',
            JSON.stringify(result.resource.authorizationToken)
          );
          localStorage.setItem(
            'currentUser',
            JSON.stringify(result.resource.user)
          );
          this.authorizationTokenSubject.next(
            result.resource.authorizationToken
          );
          return result.resource;
        })
      );
  }

  register(user: UserRegistration) {
    return this.httpClient.post<GenericResponse<LoginResult>>(
      'https://localhost:7077/api/v1.0/auth/register',
      user
    );
  }

  logout() {
    localStorage.clear();
    this.authorizationTokenSubject.next('');
  }
}
