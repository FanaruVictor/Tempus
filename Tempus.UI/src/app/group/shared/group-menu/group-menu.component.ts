import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GroupOverview } from 'src/app/_commons/models/groups/groupOverview';
import { GroupApiService } from '../../../_services/group.api.service';
import { GroupService } from 'src/app/_services/group/group.service';

@Component({
  selector: 'app-group-menu',
  templateUrl: './group-menu.component.html',
  styleUrls: ['./group-menu.component.scss'],
})
export class GroupMenuComponent implements OnInit {
  activeGroupId: string | undefined;
  searchText: string = '';
  groups: GroupOverview[] = [];

  constructor(
    private router: Router,
    private groupApiService: GroupApiService,
    private groupService: GroupService
  ) {}

  ngOnInit(): void {
    this.groupApiService.getAll().subscribe((response) => {
      this.groups = response.resource;
      this.groups.forEach(
        (x) => (x.userPhotos = x.userPhotos.filter((x) => x != null))
      );
    });

    this.groupService.currentGroupId.subscribe((x) => {
      this.activeGroupId = x;
    });

    const groupId = this.router.url.split('/')[2];
    if (!!groupId) {
      this.setActiveItem(groupId);
    }
  }

  delete(id: string) {}

  setActiveItem(id: string) {
    this.groupService.setGroupId(id);
    this.activeGroupId = id;
    return;
  }
}
