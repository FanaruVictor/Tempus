import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GroupOverview } from 'src/app/_commons/models/groups/groupOverview';
import { GroupService } from 'src/app/_services/group/group.service';
import { filter } from 'rxjs';
import { GroupApiService } from 'src/app/_services/group/group.api.service';

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
    this.getAll();

    const groupId = this.router.url.split('/')[2];
    if (!!groupId) {
      this.setActiveItem(groupId);
    }

    this.groupService.currentGroupId.subscribe((x) => {
      this.activeGroupId = x;
    });
  }

  getAll() {
    this.groupApiService.getAll().subscribe((response) => {
      this.groups = response.resource;
      this.groups.forEach(
        (x) => (x.userPhotos = x.userPhotos.filter((x) => x != null))
      );
    });
  }

  delete(id: string) {
    this.groupApiService
      .delete(id)
      .pipe(filter((x) => !!x))
      .subscribe((response) => {
        const id = response.resource;
        this.groups = this.groups.filter((x) => x.id != id);
        this.router.navigate(['/groups']);
      });
  }

  setActiveItem(id: string) {
    this.groupService.setGroupId(id);
    this.activeGroupId = id;
    return;
  }
}
