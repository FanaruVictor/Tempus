import { Component, OnInit } from '@angular/core';
import { GroupOverview } from 'src/app/_commons/models/groups/groupOverview';

@Component({
  selector: 'app-group-overview',
  templateUrl: './group-overview.component.html',
  styleUrls: ['./group-overview.component.scss'],
})
export class GroupOverviewComponent implements OnInit {
  groups: GroupOverview[] = [];
  ngOnInit(): void {}
}
