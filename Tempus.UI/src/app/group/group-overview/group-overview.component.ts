import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { GroupService } from 'src/app/_services/group/group.service';

@Component({
  selector: 'app-group-overview',
  templateUrl: './group-overview.component.html',
  styleUrls: ['./group-overview.component.scss'],
})
export class GroupOverviewComponent implements OnInit {
  groupId: string | undefined;

  constructor(private groupService: GroupService, private router: Router) {}

  ngOnInit() {
    this.groupService.currentGroupId.subscribe((x) => {
      this.groupId = x;
    });
  }

  redirectToRegistrations() {
    this.router.navigate([`groups/${this.groupId}/registrations`]);
  }

  redirectToCategories() {
    this.router.navigate([`groups/${this.groupId}/categories`]);
  }
}
