import {
  AfterContentChecked,
  AfterContentInit,
  AfterViewInit,
  Component,
  OnInit,
} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GroupOverview } from 'src/app/_commons/models/groups/groupOverview';
import { GroupApiService } from 'src/app/_services/group.api.service';

@Component({
  selector: 'app-group-overview',
  templateUrl: './group-overview.component.html',
  styleUrls: ['./group-overview.component.scss'],
})
export class GroupOverviewComponent implements OnInit, AfterViewInit {
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

  constructor(
    private activatedRoute: ActivatedRoute,
    private groupApiService: GroupApiService
  ) {}

  ngAfterViewInit(): void {
    const id = this.activatedRoute.snapshot.params['id'];
    this.activeGroupId = id;
    this.setActiveItem(this.activeGroupId);
  }

  ngOnInit(): void {
    // this.groupApiService.getAll().subscribe((response) => {
    //   this.groups = response.resource;
    // });
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
function fliter(
  arg0: (x: any) => boolean
): import('rxjs').OperatorFunction<any, unknown> {
  throw new Error('Function not implemented.');
}
