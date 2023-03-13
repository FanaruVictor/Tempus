import { Component } from '@angular/core';
import {Router} from "@angular/router";
import {AuthService} from "../../_services/auth/auth.service";

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.scss']
})
export class LogoutComponent {
  constructor(private router: Router, private authService: AuthService) {
  }
  logout(){
    this.authService.logout();
    this.router.navigate(['/']).then(() => window.location.reload());
  }
}
