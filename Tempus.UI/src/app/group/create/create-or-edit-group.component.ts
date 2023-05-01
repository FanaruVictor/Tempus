import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { debug, log } from 'console';
import { filter } from 'rxjs';
import { UserEmail } from '../../_commons/models/user/userEmail';
import { UserApiService } from '../../_services/user.api.service';
import { GroupApiService } from '../../_services/group.api.service';
import { AddGroupData } from '../../_commons/models/groups/addGroupData';

@Component({
  selector: 'app-create-or-edit-group',
  templateUrl: './create-or-edit-group.component.html',
  styleUrls: ['./create-or-edit-group.component.scss'],
})
export class CreateOrEditGroupComponent implements OnInit {
  createOrEditForm: FormGroup;
  isCreateMode = false;
  id: string = '';
  imageURL?: string;
  isCurrentPhotoChanged = false;
  showSearchUser = false;
  users: UserEmail[] = [];

  constructor(
    private fb: FormBuilder,
    private userApiService: UserApiService,
    private groupApiService: GroupApiService,
    private router: Router
  ) {
    this.createOrEditForm = this.fb.group({
      name: ['', Validators.required],
      members: [[], Validators.required],
      image: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.userApiService
      .getUserEmails()
      .pipe(filter((x) => !!x.resource))
      .subscribe((response) => {
        this.users = response.resource;
      });
  }

  submit() {
    const group: AddGroupData = {
      name: this.createOrEditForm.get('name')?.value,
      members: this.createOrEditForm.get('members')?.value,
      image: this.createOrEditForm.get('image')?.value,
    };

    this.groupApiService.add(group).subscribe(() => {
      this.router.navigate(['/groups']);
    });
  }

  showPreview(event: any) {
    this.isCurrentPhotoChanged = true;
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
    this.isCurrentPhotoChanged = true;
  }

  addPhotos() {
    let element = document.querySelectorAll('.ng-option');
    element.forEach((x) => {
      if (x.innerHTML.includes('img') || x.innerHTML.includes('div')) {
        return;
      }
      let emailElement = x.querySelector('span');
      const email = emailElement?.textContent ?? '';
      if (email !== '') {
        const user = this.users.filter((y) => {
          return y.email == email;
        })[0];
        const innerHtml = x.innerHTML;
        x.innerHTML = '';
        if (!!user.photoUrl) {
          x.innerHTML = `<img style="width: 50px; height:  50px; border-radius: 50%;" src='${user.photoUrl}'>`;
        } else {
          x.innerHTML = `<div style="
          background-color: #7e57c2;
           width: 50px; height: 50px;
            border-radius: 50%;
            color: white;
            font-size: 30px;
            display: flex;
            justify-content: center;
            align-items: center;">${user.email[0].toUpperCase()}</div>`;
        }
        x.innerHTML += innerHtml.toString();
      }
    });
  }
}
