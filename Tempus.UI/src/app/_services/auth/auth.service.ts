import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { GenericResponse } from '../../_commons/models/genericResponse';
import { LoginResult } from '../../_commons/models/auth/loginResult';
import { environment } from 'src/environments/environment';
import { AngularFireAuth } from '@angular/fire/compat/auth';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  authorizationToken: Observable<string>;
  private authorizationTokenSubject: BehaviorSubject<string>;

  apiUrl = environment.apiUrl;

  constructor(
    private httpClient: HttpClient,
    private fbAtuth: AngularFireAuth,
    private router: Router
  ) {
    const token = localStorage.getItem('authorizationToken');
    this.authorizationTokenSubject = new BehaviorSubject<string>(token ?? '');

    this.authorizationToken = this.authorizationTokenSubject.asObservable();
  }

  login(loginData: LoginData) {
    return this.httpClient
      .post<GenericResponse<LoginResult>>(
        `${this.apiUrl}/v1.0/auth/login`,
        loginData
      )
      .pipe(
        map((result) => {
          localStorage.setItem(
            'authorizationToken',
            JSON.stringify(result.resource.authorizationToken)
          );
          this.authorizationTokenSubject.next(
            result.resource.authorizationToken
          );
          return result.resource;
        })
      );
  }

  logout() {
    localStorage.clear();
    this.fbAtuth.signOut();
    this.authorizationTokenSubject.next('');
    
    this.router.navigate(['/login']);
  }
}

export interface LoginData {
  email: string;
  externalId: string;
  userName: string;
  photoURL: string;
  phoneNumber: string;
}
