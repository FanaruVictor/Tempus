import {
  AfterViewChecked,
  AfterViewInit,
  Component,
  OnInit,
} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {log} from 'console';
import {GroupOverview} from 'src/app/_commons/models/groups/groupOverview';
import {GroupApiService} from "../../../_services/group.api.service";

@Component({
  selector: 'app-group-menu',
  templateUrl: './group-menu.component.html',
  styleUrls: ['./group-menu.component.scss'],
})
export class GroupMenuComponent implements OnInit, AfterViewInit {
  activeGroupId: string = '';
  searchText: string = '';
  groups: GroupOverview[] = [];

  constructor(private activatedRoute: ActivatedRoute, private groupApiService: GroupApiService) {
  }

  ngOnInit(): void {

    this.groupApiService.getAll().subscribe((response) => {
      this.groups = response.resource;
      this.groups.forEach(x => x.userPhotos = x.userPhotos.filter(x => x != null));
    });
  }

  ngAfterViewInit(): void {
    this.activeGroupId = this.activatedRoute.snapshot.params['id'];

  }

  delete(id: string) {
  }

  setActiveItem(id: string) {
    this.activeGroupId = id;
    return;

  }

}
