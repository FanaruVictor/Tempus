import {
  Component, OnInit,
} from '@angular/core';
import { GroupOverview } from 'src/app/_commons/models/groups/groupOverview';
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-group-overview',
  templateUrl: './group-overview.component.html',
  styleUrls: ['./group-overview.component.scss'],
})
export class GroupOverviewComponent implements OnInit{
  groupId: string = '';
  routePattern: string | undefined;
  constructor(private activatedRoute: ActivatedRoute, private router: Router) {}

  ngOnInit(){
    this.routePattern = this.activatedRoute.snapshot.routeConfig?.path;
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if(!!id){
      this.groupId = id;
    }
  }

  redirectToRegistrations() {
    this.router.navigate([`groups/${this.groupId}/registrations`]);
  }

}
