import {
  AfterViewChecked,
  AfterViewInit,
  Component,
  OnInit,
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { log } from 'console';
import { GroupOverview } from 'src/app/_commons/models/groups/groupOverview';

@Component({
  selector: 'app-group-menu',
  templateUrl: './group-menu.component.html',
  styleUrls: ['./group-menu.component.scss'],
})
export class GroupMenuComponent implements AfterViewChecked {
  activeGroupId: string = '';
  searchText: string = '';
  groups: GroupOverview[] = [
    {
      id: '1',
      name: 'Group 1',
      image: 'https://picsum.photos/200',
      usersPhotos: [
        'https://picsum.photos/200',
        'https://picsum.photos/200',
        'https://picsum.photos/200',
        'https://picsum.photos/200',
        'https://picsum.photos/200',
        'https://picsum.photos/200',
      ],
      createdAt: new Date(),
    },
    {
      id: '2',
      name: 'Group 2',
      image: 'https://picsum.photos/200',
      usersPhotos: ['https://picsum.photos/200'],
      createdAt: new Date(),
    },
    {
      id: '3',
      name: 'Group 3',
      image: 'https://picsum.photos/200',
      usersPhotos: undefined,
      createdAt: new Date(),
    },
  ];

  constructor(private activatedRoute: ActivatedRoute) {}

  ngAfterViewChecked(): void {
    const id = this.activatedRoute.snapshot.params['id'];
    this.activeGroupId = id;
    this.setActiveItem(this.activeGroupId);
    console.log(this.activeItem);
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
