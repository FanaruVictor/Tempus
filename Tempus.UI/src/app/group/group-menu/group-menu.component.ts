import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GroupOverview } from 'src/app/_commons/models/groups/groupOverview';
import { GroupService } from 'src/app/_services/group/group.service';
import { filter } from 'rxjs';
import { GroupApiService } from 'src/app/_services/group/group.api.service';
import { UserApiService } from '../../_services/user.api.service';
import { UserDetails } from '../../_commons/models/user/userDetails';

@Component({
  selector: 'app-group-menu',
  templateUrl: './group-menu.component.html',
  styleUrls: ['./group-menu.component.scss'],
})
export class GroupMenuComponent implements OnInit {
  activeGroupId: string | undefined;
  searchText: string = '';
  groups: GroupOverview[] = [];
  currentUser: UserDetails | undefined;

  constructor(
    private router: Router,
    private groupApiService: GroupApiService,
    private groupService: GroupService,
    private userService: UserApiService
  ) {}

  ngOnInit(): void {
    this.userService.user.subscribe((x) => {
      this.currentUser = x;
    });
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
      this.groups = this.groups.sort(this.compare);
    });
  }

  private compare(a, b) {
    if (a.name < b.name) {
      return -1;
    }
    if (a.name > b.name) {
      return 1;
    }
    return 0;
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

  edit(id: string) {
    this.router.navigate(['/groups', id, 'edit']);
  }

  setActiveItem(id: string) {
    this.groupService.setGroupId(id);
    this.activeGroupId = id;

    this.groupService.removeAll();

    this.router.navigate(['/groups', id, 'notes']);
    return;
  }
}
