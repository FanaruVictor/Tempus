import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GroupService } from 'src/app/_services/group/group.service';

@Component({
  selector: 'app-group-overview',
  templateUrl: './group-overview.component.html',
  styleUrls: ['./group-overview.component.scss'],
})
export class GroupOverviewComponent implements OnInit {
  groupId: string | undefined;
  isActive = true;

  constructor(private groupService: GroupService, private router: Router) {}

  ngOnInit() {
    this.groupService.currentGroupId.subscribe((x) => {
      this.groupId = x;
    });
  }

  redirectToRegistrations() {
    this.router.navigate([`groups/${this.groupId}/notes`]);
    this.isActive = true;
  }

  redirectToCategories() {
    this.router.navigate([`groups/${this.groupId}/categories`]);
    this.isActive = false;
  }
}
