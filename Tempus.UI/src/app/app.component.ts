import { Component, OnDestroy, OnInit } from '@angular/core';
import { UserApiService } from './_services/user.api.service';
import { ClientEventsService } from './_services/client-events.service';
import { AngularFireAuth } from '@angular/fire/compat/auth';
import {
  FirebaseUISignInFailure,
  FirebaseUISignInSuccessWithAuthResult,
} from 'firebaseui-angular';
import { AuthService } from './_services/auth/auth.service';
import { Router } from '@angular/router';
import { NotificationService } from './_services/notification.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit, OnDestroy {
  constructor(
    private userService: UserApiService,
    private clientEventsService: ClientEventsService,
    public fbAuth: AngularFireAuth,
    public authService: AuthService,
    public router: Router
  ) {}

  ngOnInit() {
    this.fbAuth.user.subscribe((user) => {
      if (user) {
        let authToken = localStorage.getItem('authorizationToken');

        if (authToken == null) {
          this.authService
            .login({
              email: user.email || '',
              externalId: user.uid,
              userName: user.displayName || '',
              phoneNumber: user.phoneNumber || '',
              photoURL: user.photoURL || '',
            })
            .subscribe({
              next: (response) => {
                localStorage.setItem(
                  'authorizationToken',
                  response.authorizationToken
                );

                localStorage.setItem(
                  'isDarkTheme',
                  response.user.isDarkTheme.toString()
                );

                if (response.user.isDarkTheme) {
                  if (!document.body.classList.contains('dark-theme'))
                    document.body.classList.toggle('dark-theme');
                  localStorage.setItem('isDarkTheme', true.toString());
                }

                this.userService.setUser(response.user);
                this.router.navigate(['/notes']);
              },
              error: (error) => {
                this.authService.logout();
                this.router.navigate(['/login']);
              },
            });
        }

        if (authToken != null) {
          let isDarkTheme = localStorage.getItem('isDarkTheme');

          this.userService.getDetails().subscribe((response) => {
            let user = response.resource;

            localStorage.setItem('isDarkTheme', user.isDarkTheme.toString());

            if (!isDarkTheme && user.isDarkTheme) {
              document.body.classList.toggle('dark-theme');
              localStorage.setItem('isDarkTheme', user.isDarkTheme.toString());
            }

            this.userService.setUser(user);

            this.clientEventsService.startConnection(authToken);
          });
        }
      }
    });
  }

  ngOnDestroy(): void {
    this.clientEventsService.stopConnection();
  }
}
