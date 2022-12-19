import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  tabs: {link: string, label: string, index: number}[];
  activeLinkIndex = 0;

  constructor(private router: Router) {
    this.tabs = [
      {
        label: 'Registrations',
        link: '/registrations' ,
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
    ]
  }

  ngOnInit(): void {
    this.router.events.subscribe((res) => {
      let element = this.tabs.find(tab => tab.link === this.router.url);
      if(element)
        this.activeLinkIndex = this.tabs.indexOf(element);
      else
        this.activeLinkIndex = 0;
    });
  }
}

