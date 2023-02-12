import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {AuthService} from "../../_services/auth/auth.service";
import {LoaderService} from "../../_services/loader/loader.service";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  tabs: { link: string, label: string, index: number }[];
  activeLinkIndex = 0;
  authorizationToken?: string;
  contentIsLoading = this.loaderService.isLoading;

  constructor(private router: Router, private authService: AuthService, public loaderService: LoaderService, private ref: ChangeDetectorRef) {
    this.tabs = [
      {
        label: 'Registrations',
        link: '/registrations',
        index: 0
      },
      {
        label: 'Categories',
        link: '/categories',
        index: 1
      },
      {
        label: 'Account',
        link: '/account',
        index: 2
      }
    ];

    this.authService.authorizationToken.subscribe(x => this.authorizationToken = x);
  }

  ngOnInit(): void {
    this.router.events.subscribe((res) => {
      let element = this.tabs.find(tab => tab.link === this.router.url);
      if (element)
        this.activeLinkIndex = this.tabs.indexOf(element);
      else
        this.activeLinkIndex = 0;
    });
  }
}
