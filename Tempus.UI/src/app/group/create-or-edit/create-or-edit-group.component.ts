import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { Subscription, filter } from 'rxjs';
import { UserEmail } from '../../_commons/models/user/userEmail';
import { UserApiService } from '../../_services/user.api.service';
import { AddGroupData } from '../../_commons/models/groups/addGroupData';
import { GroupApiService } from '../../_services/group/group.api.service';
import { NotificationService } from '../../_services/notification.service';
import { UpdateGroupData } from '../../_commons/models/groups/udpateGroupData';

@Component({
  selector: 'app-create-or-edit-group',
  templateUrl: './create-or-edit-group.component.html',
  styleUrls: ['./create-or-edit-group.component.scss'],
})
export class CreateOrEditGroupComponent implements OnInit {
  createOrEditForm: FormGroup;
  mode: string = 'create';
  id: string = '';
  imageURL?: string;
  isCurrentImageChanged = false;
  showSearchUser = false;
  users: UserEmail[] = [];

  constructor(
    private fb: FormBuilder,
    private userApiService: UserApiService,
    private groupApiService: GroupApiService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private notificationiService: NotificationService
  ) {
    this.createOrEditForm = this.fb.group({
      name: ['', Validators.required],
      members: [[], Validators.required],
      image: ['', Validators.required],
    });
  }

  members: UserEmail[] = [];

  ngOnInit(): void {
    this.userApiService
      .getUserEmails()
      .pipe(filter((x) => !!x.resource))
      .subscribe((response) => {
        this.users = response.resource;
      });

    this.activatedRoute.url.subscribe((x) => {
      this.mode = x[x.length - 1].path;
      if (this.mode === 'edit') {
        this.id = this.activatedRoute.snapshot.params['id'];
        this.groupApiService.getById(this.id).subscribe((response) => {
          this.createOrEditForm.patchValue({
            name: response.resource.name,
            members: [...response.resource.members.map((x) => x.id)],
          });
          this.members = response.resource.members;
          this.imageURL = response.resource.image;
        });
      }
    });
  }

  submit() {
    if (this.mode === 'create') {
      this.create();
      return;
    }
    this.edit();
  }

  create() {
    const group: AddGroupData = {
      name: this.createOrEditForm.get('name')?.value,
      members: this.createOrEditForm.get('members')?.value,
      image: this.createOrEditForm.get('image')?.value,
    };

    if (!this.isValid(group)) {
      return;
    }

    this.groupApiService.add(group).subscribe(() => {
      this.router.navigate(['/groups']);
    });
  }

  private edit() {
    const group: UpdateGroupData = {
      id: this.id,
      name: this.createOrEditForm.get('name')?.value,
      members: this.createOrEditForm.get('members')?.value,
      image: this.createOrEditForm.get('image')?.value,
      isCurrentImageChanged: this.isCurrentImageChanged,
    };

    if (!this.isValid(group)) {
      return;
    }

    this.groupApiService.edit(group, this.id).subscribe(() => {
      this.router.navigate(['/groups']);
    });
  }

  private isValid(group: AddGroupData) {
    let isValid = true;
    if (group.members.length === 0) {
      this.notificationiService.error(
        ['You must add at least one member to your group'],
        'Request not completed'
      );
      isValid = false;
    }
    if (group.name.length === 0) {
      this.notificationiService.error(
        ['You must add a name to your group'],
        'Request not completed'
      );
      isValid = false;
    }

    return isValid;
  }

  showPreview(event: any) {
    this.isCurrentImageChanged = true;
    const file = <File>event.target.files[0];

    this.createOrEditForm.patchValue({
      image: file,
    });
    this.createOrEditForm.get('photo')?.updateValueAndValidity();
    // File Preview
    const reader = new FileReader();
    reader.onload = () => {
      this.imageURL = reader.result as string;
    };
    reader.readAsDataURL(file);
  }

  deletePicture() {
    this.createOrEditForm.get('image')?.setValue(null);
    this.imageURL = undefined;
    this.isCurrentImageChanged = true;
  }
}
