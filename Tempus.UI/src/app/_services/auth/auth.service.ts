import { Injectable, NgZone } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/compat/auth';
import { Router } from '@angular/router';
import { NotificationService } from '../notification.service';
import * as auth from 'firebase/auth';
import { User } from 'src/app/_commons/models/user/user';
import { BehaviorSubject, Observable, filter } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { GenericResponse } from 'src/app/_commons/models/genericResponse';
import { environment } from 'src/environments/environment';
import {
  AngularFirestore,
  AngularFirestoreDocument,
} from '@angular/fire/compat/firestore';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  apiUrl = `${environment.apiUrl}/v1/auth`;

  userSubject: BehaviorSubject<User>;
  authorizationToken: Observable<string>;
  private authorizationTokenSubject: BehaviorSubject<string>;

  userData: any;

  user: Observable<User>;
  defaultUser: User = {
    id: '',
    displayName: '',
    photoURL: '',
    isDarkTheme: false,
    email: '',
    externalId: '',
    emailVerified: false,
  };

  constructor(
    private afAuth: AngularFireAuth,
    private router: Router,
    private notificationService: NotificationService,
    private httpClient: HttpClient,
    private afs: AngularFirestore
  ) {
    const user = JSON.parse(localStorage.getItem('user') || '{}');
    this.userSubject = new BehaviorSubject<User>(user as User);
    this.user = this.userSubject.asObservable();

    const authorizationToken = localStorage.getItem('authorizationToken') || '';
    this.authorizationTokenSubject = new BehaviorSubject<string>(
      authorizationToken
    );

    this.authorizationToken = this.authorizationTokenSubject.asObservable();

    this.afAuth.authState.subscribe((user) => {
      if (user) {
        this.userData = user;
      }
    });
  }

  setUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user as User));
    this.userSubject.next(user);
  }

  setAuthorizationToken(authorizationToken: string) {
    localStorage.setItem('authorizationToken', authorizationToken);
    this.authorizationTokenSubject.next(authorizationToken);
  }

  // Sign in with email/password
  signIn(email: any, password: any) {
    return this.afAuth
      .signInWithEmailAndPassword(email, password)
      .then((result) => {
        this.setUserData(result.user);
        this.login({
          email: result.user?.email || '',
          externalId: undefined,
        }).subscribe((response) => {
          if (result.user?.emailVerified === false) {
            this.sendVerificationMail();
            this.notificationService.warn(
              'Please verify your email address',
              'Sign in failed'
            );
            return;
          }

          this.setUser({
            id: response.resource.userId,
            displayName: result.user?.displayName || '',
            photoURL: response.resource.photoURL,
            isDarkTheme: response.resource.isDarkTheme,
            email: result.user?.email || '',
            externalId: undefined,
            emailVerified: result.user?.emailVerified || false,
          });
          localStorage.setItem('isDarkTheme', response.resource.isDarkTheme);

          this.setAuthorizationToken(response.resource.authorizationToken);

          this.router.navigate(['/registrations']);
        });
      })
      .catch((error) => {
        this.notificationService.error(error.message, 'Sign in failed');
      });
  }

  // Sign up with email/password
  signUp(email: any, password: any, displayName: string) {
    return this.afAuth
      .createUserWithEmailAndPassword(email, password)
      .then((result) => {
        this.setUserData(result.user);
        const user = result.user;
        this.register({
          email: result.user?.email || '',
          externalId: '',
          photoURL: result.user?.photoURL || '',
        }).subscribe(() => {
          user?.updateProfile({
            displayName: displayName,
          });
          this.sendVerificationMail();
        });
      })
      .catch((error) => {
        this.notificationService.error(error.message, 'Sign up failed');
      });
  }

  // Send email verfificaiton when new user sign up
  sendVerificationMail() {
    return this.afAuth.currentUser
      .then((u) => u?.sendEmailVerification())
      .then(() => {
        this.router.navigate(['/auth/verifyEmail']);
      });
  }

  // Reset Forggot password
  forgotPassword(passwordResetEmail: any) {
    return this.afAuth
      .sendPasswordResetEmail(passwordResetEmail)
      .then(() => {
        this.notificationService.warn(
          'Password reset email sent, check your inbox.',
          'Request completed'
        );
      })
      .catch((error) => {
        this.notificationService.error(error.message, 'Reset password failed');
      });
  }

  // Returns true when user is looged in and email is verified
  get isLoggedIn(): boolean {
    const user = JSON.parse(localStorage.getItem('user') || '{}');
    return user !== null && user.emailVerified !== false ? true : false;
  }

  // Sign in with Google
  googleAuth() {
    return this.authLogin(new auth.GoogleAuthProvider());
  }

  // Auth logic to run auth providers
  authLogin(provider: any) {
    return this.afAuth
      .signInWithPopup(provider)
      .then((result) => {
        this.setUserData(result.user);
        if (result.additionalUserInfo?.isNewUser) {
          this.register({
            email: result.user?.email || '',
            externalId: result.user?.uid,
            photoURL: result.user?.photoURL || '',
          }).subscribe(() => {
            this.login({
              email: result.user?.email || '',
              externalId: result.user?.uid,
            }).subscribe((response) => {
              debugger;
              this.setUser({
                id: response.resource.userId,
                displayName: result.user?.displayName || '',
                photoURL: response.resource.photoURL,
                isDarkTheme: response.resource.isDarkTheme,
                email: result.user?.email || '',
                externalId: result.user?.uid,
                emailVerified: result.user?.emailVerified || false,
              });
              localStorage.setItem(
                'isDarkTheme',
                response.resource.isDarkTheme
              );

              this.setAuthorizationToken(response.resource.authorizationToken);

              this.router.navigate(['/registrations']);
            });
          });
        } else {
          this.login({
            email: result.user?.email || '',
            externalId: result.user?.uid,
          }).subscribe((response) => {
            debugger;
            this.setUser({
              id: response.resource.userId,
              displayName: result.user?.displayName || '',
              photoURL: response.resource.photoURL,
              isDarkTheme: response.resource.isDarkTheme,
              email: result.user?.email || '',
              externalId: result.user?.uid,
              emailVerified: result.user?.emailVerified || false,
            });
            localStorage.setItem('isDarkTheme', response.resource.isDarkTheme);
            this.setAuthorizationToken(response.resource.authorizationToken);
            this.router.navigate(['/registrations']);
          });
        }
      })
      .catch((error) => {
        this.notificationService.error(error.message, 'Sign in failed');
      });
  }

  // Sign out
  signOut() {
    return this.afAuth
      .signOut()
      .then(() => {
        localStorage.clear();
        this.router.navigate(['/auth/signIn']);
      })
      .catch((error) => {
        this.notificationService.error(error.message, 'Logout failed');
      });
  }

  private login(user: LoginInfo) {
    return this.httpClient.post<GenericResponse<any>>(
      `${this.apiUrl}/login`,
      user
    );
  }

  private register(user: RegisterInfo) {
    return this.httpClient.post(`${this.apiUrl}/register`, user);
  }

  setUserData(user: any) {
    const userRef: AngularFirestoreDocument<any> = this.afs.doc(
      `users/${user.uid}`
    );
    const userData: User = {
      id: '',
      externalId: user.uid,
      email: user.email,
      displayName: user.displayName,
      photoURL: user.photoURL,
      emailVerified: user.emailVerified,
      isDarkTheme: false,
    };
    return userRef.set(userData, {
      merge: true,
    });
  }
}

export interface LoginInfo {
  externalId?: string;
  email: string;
}

export interface RegisterInfo {
  externalId?: string;
  email: string;
  photoURL?: string;
}
