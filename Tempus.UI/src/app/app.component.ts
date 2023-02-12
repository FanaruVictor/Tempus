import {Component, OnInit} from '@angular/core';
import {UserApiService} from "./_services/user.api.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  constructor(private userService: UserApiService) {
  }

  ngOnInit() {
    if (localStorage.getItem("authorizationToken")) {
      let isDarkMode = localStorage.getItem("isDarkMode");


      this.userService.getTheme()
        .subscribe(response => {
          localStorage.setItem('isDarkMode', response.resource.toString());
          if (!isDarkMode && response.resource) {
            document.body.classList.toggle('dark-theme');
          }
        });
    }
  }
}

