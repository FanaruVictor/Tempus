import {
  AfterViewChecked,
  AfterViewInit,
  Component,
  OnInit,
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { log } from 'console';
import { GroupOverview } from 'src/app/_commons/models/groups/groupOverview';
import {GroupApiService} from "../../../_services/group.api.service";

@Component({
  selector: 'app-group-menu',
  templateUrl: './group-menu.component.html',
  styleUrls: ['./group-menu.component.scss'],
})
export class GroupMenuComponent implements OnInit, AfterViewChecked {
  activeGroupId: string = '';
  searchText: string = '';
  groups: GroupOverview[] = [

  ];

  constructor(private activatedRoute: ActivatedRoute, private groupApiService: GroupApiService) {}


  ngOnInit(): void {

    this.groupApiService.getAll().subscribe((response) => {

      this.groups = response.resource;
      this.groups.forEach(x => x.userPhotos = x.userPhotos.filter(x => x != null));
    });
  }
  ngAfterViewChecked(): void {
    const id = this.activatedRoute.snapshot.params['id'];
    this.activeGroupId = id;
    this.setActiveItem(this.activeGroupId);
  }

  delete(id: string) {}

  activeItem: HTMLElement | null = null;

  setActiveItem(id: string) {
    if (id === '' || id == undefined) return;

    this.activeGroupId = id;
    const item = document.getElementById(id);

    if (item === null) return;

    if (this.activeItem) {
      this.activeItem.classList.remove('group-active');
    }

    this.activeItem = item;

    this.activeItem.classList.toggle('group-active');
  }

}
