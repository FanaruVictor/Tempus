import { Component, OnInit } from '@angular/core';
import { UserApiService } from './_services/user.api.service';
import { SignalRService } from './_services/signal-r.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  constructor(
    private userService: UserApiService,
    private signalRService: SignalRService
  ) {}

  ngOnInit() {
    let authToken = localStorage.getItem('authorizationToken');
    if (authToken != null) {
      authToken = authToken.substring(1, authToken.length - 1);

      this.signalRService.initializeSignalRConnection(authToken);
      let isDarkTheme = localStorage.getItem('isDarkTheme');

      this.userService.getDetails().subscribe((response) => {
        let user = response.resource;
        localStorage.setItem('isDarkTheme', user.isDarkTheme.toString());
        if (!isDarkTheme && user.isDarkTheme) {
          document.body.classList.toggle('dark-theme');
          localStorage.setItem('isDarkTheme', user.isDarkTheme.toString());
        }
        this.userService.setUser(user);
      });
    }
  }
}
