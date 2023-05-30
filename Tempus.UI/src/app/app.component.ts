import { Component, OnDestroy, OnInit } from '@angular/core';
import { UserApiService } from './_services/user.api.service';
import { ClientEventsService } from './_services/client-events.service';
import { AuthService } from './_services/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit, OnDestroy {
  constructor(
    private userService: UserApiService,
    private clientEventsService: ClientEventsService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit() {
    let authToken = localStorage.getItem('authorizationToken');

    if (authToken != null) {
      let isDarkTheme = localStorage.getItem('isDarkTheme');

      this.userService.getDetails().subscribe((response) => {
        let user = response.resource;

        localStorage.setItem('isDarkTheme', user.isDarkTheme.toString());

        if (!isDarkTheme && user.isDarkTheme) {
          document.body.classList.toggle('dark-theme');
          localStorage.setItem('isDarkTheme', user.isDarkTheme.toString());
        }

        this.clientEventsService.startConnection(authToken);
      });
    }
  }

  ngOnDestroy(): void {
    this.clientEventsService.stopConnection();
  }
}
