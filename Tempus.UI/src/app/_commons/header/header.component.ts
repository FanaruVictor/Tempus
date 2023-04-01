import {
  Component,
  OnInit,
} from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../_services/auth/auth.service';
import { LoaderService } from '../../_services/loader/loader.service';
import { UserApiService } from '../../_services/user.api.service';
import { UserDetails } from '../models/user/userDetails';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
  tabs: { link: string; label: string; index: number }[];
  authorizationToken?: string;
  user?: UserDetails;

  constructor(
    public authService: AuthService,
    public loaderService: LoaderService,
    private userService: UserApiService
  ) {
    this.tabs = [
      {
        label: 'Registrations',
        link: '/registrations',
        index: 0,
      },
      {
        label: 'Categories',
        link: '/categories',
        index: 1,
      },
      {
        label: 'Groups',
        link: '/groups',
        index: 2,
      },
      {
        label: 'Account',
        link: '/account',
        index: 3,
      },
    ];

    this.authService.authorizationToken.subscribe(
      (x) => (this.authorizationToken = x)
    );
  }

  ngOnInit() {
    if (localStorage.getItem('authorizationToken')) {
      this.userService.user.subscribe((user) => {
        this.user = user;
        this.tabs[3].label = this.user?.userName || 'Account';
      });
    }
  }

  async toggleDarkTheme() {
    document.body.classList.toggle('dark-theme');

    this.userService
      .changeTheme(!this.user?.isDarkTheme)
      .subscribe((response) => {
        this.user = response.resource;
        localStorage.setItem('isDarkTheme', this.user.isDarkTheme.toString());
        this.userService.setUser(this.user);
      });
  }

  logout() {
    localStorage.clear();
    window.location.reload();
  }
}
