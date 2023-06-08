import { Component, OnInit } from '@angular/core';
import { GroupService } from 'src/app/_services/group/group.service';
import { AngularFireAuth } from '@angular/fire/compat/auth';
import { AuthService } from 'src/app/_services/auth/auth.service';
import { LoaderService } from '../_services/loader/loader.service';
import { UserDetails } from '../_commons/models/user/userDetails';
import { UserApiService } from '../_services/user.api.service';

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
    public loaderService: LoaderService,
    private userService: UserApiService,
    private groupService: GroupService,
    public fbAuth: AngularFireAuth,
    private authSerice: AuthService
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
  }

  ngOnInit() {
    this.userService.user.subscribe((user) => {
      this.user = user;
      this.tabs[3].label = this.user?.userName || 'Account';
    });
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
    this.authSerice.logout();
  }

  redirectTo(link: string) {
    this.groupService.setGroupId(undefined);
  }
}
