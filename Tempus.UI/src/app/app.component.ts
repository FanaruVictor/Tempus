import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {AuthService} from "./_services/auth/auth.service";
import {LoaderService} from "./_services/loader/loader.service";
import {UserApiService} from "./_services/user.api.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
    constructor(private userService: UserApiService) {
    }

    ngOnInit(){
      if(localStorage.getItem("authorizationToken")){
        this.userService.getTheme()
          .subscribe(response => {
            if(response.resource){
              document.body.classList.toggle('dark-theme');
            }
          })
      }
    }
}

