import {
  Component, OnInit,
} from '@angular/core';
import {ActivatedRoute, NavigationEnd, Router} from "@angular/router";
import {GroupApiService} from "../../_services/group.api.service";
import {take} from "rxjs";

@Component({
  selector: 'app-group-overview',
  templateUrl: './group-overview.component.html',
  styleUrls: ['./group-overview.component.scss'],
})
export class GroupOverviewComponent implements OnInit {
  groupId: string = '';

  constructor(private activatedRoute: ActivatedRoute, private router: Router, private groupApiService: GroupApiService) {
  }

  ngOnInit() {
    this.activatedRoute.params.subscribe(params => {
      this.groupId = params['id'];
    });

    this.groupApiService.getAll().pipe(take(1)).subscribe((response) => {
      this.router.navigate([`groups/${response.resource[0].id}/registrations`]);
    });
  }

  redirectToRegistrations() {
    this.router.navigate([`groups/${this.groupId}/registrations`]);
  }

}
