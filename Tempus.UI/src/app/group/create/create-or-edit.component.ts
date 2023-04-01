import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { log } from 'console';
import { filter } from 'rxjs';
import { AddGroupData } from 'src/app/_commons/models/groups/addGroupData';
import { UserEmail } from 'src/app/_commons/models/user/userEmail';
import { GroupApiService } from 'src/app/_services/group.api.service';
import { UserApiService } from 'src/app/_services/user.api.service';

@Component({
  selector: 'app-create',
  templateUrl: './create-or-edit.component.html',
  styleUrls: ['./create-or-edit.component.scss'],
})
export class CreateOrEditComponent implements OnInit {
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
    private groupApiService: GroupApiService
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
    console.log(this.createOrEditForm);

    const group: AddGroupData = {
      name: this.createOrEditForm.get('name')?.value,
      members: this.createOrEditForm.get('members')?.value,
      image: this.createOrEditForm.get('image')?.value,
    };

    this.groupApiService.add(group).subscribe();
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
