import { Component, OnInit } from '@angular/core';
import { UserDetails } from '../../_commons/models/user/userDetails';
import { UserApiService } from '../../_services/user.api.service';
import { filter } from 'rxjs';
import { UpdateUserData } from '../../_commons/models/user/updateUserData';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NotificationService } from 'src/app/_services/notification.service';
import { getAuth, updateEmail, updateProfile } from 'firebase/auth';

@Component({
  selector: 'app-edit-account',
  templateUrl: './edit-account.component.html',
  styleUrls: ['./edit-account.component.scss'],
})
export class EditAccountProfile implements OnInit {
  user!: UserDetails;
  file?: File;
  editForm: FormGroup;
  imageURL?: string;
  isCurrentPhotoChanged = false;

  constructor(
    private router: Router,
    private userApiService: UserApiService,
    private fb: FormBuilder,
    private notificationService: NotificationService
  ) {
    this.editForm = this.fb.group({
      photo: [null],
      userName: ['', Validators.required],
      phoneNumber: [''],
      email: ['', [Validators.email, Validators.required]],
    });
  }

  ngOnInit(): void {
    this.userApiService
      .getDetails()
      .pipe(filter((x) => !!x))
      .subscribe((response) => {
        this.user = response.resource;
        this.editForm.patchValue(this.user);
        this.imageURL = this.user.photo?.url;
      });
  }

  submit() {
    if (!this.isValidForm()) {
      return;
    }

    let userData: UpdateUserData = {
      userName: this.editForm.get('userName')?.value,
      phoneNumber: this.editForm.get('phoneNumber')?.value,
      email: this.editForm.get('email')?.value,
      isCurrentPhotoChanged: this.isCurrentPhotoChanged,
      newPhoto: this.editForm.get('photo')?.value,
    };

    if (!this.isValidData(userData)) {
      this.notificationService.warn(
        'No changes were made.',
        'Request completed'
      );
      return;
    }
    this.userApiService.update(userData).subscribe((response) => {
      this.userApiService.setUser(response.resource);
      const auth = getAuth();
      if (auth.currentUser) {
        if (response.resource.email !== auth.currentUser.email) {
          updateEmail(auth.currentUser, response.resource.email).then(() => {
            console.log('email updated!');
          });
        }

        updateProfile(auth.currentUser, {
          displayName: response.resource.userName,
          photoURL: response.resource.photo?.url ?? null,
        });
      }

      this.router.navigate(['/account']);
    });
  }

  isValidData(user: UpdateUserData): boolean {
    const result =
      this.user.userName !== user.userName ||
      this.user.email !== user.email ||
      this.user.phoneNumber !== user.phoneNumber ||
      user.newPhoto instanceof File ||
      this.isCurrentPhotoChanged;

    return result;
  }

  isValidForm() {
    let messages: string[] = [];
    if (!this.editForm.valid) {
      if (this.editForm.get('userName')?.errors?.['required']) {
        messages.push('User name is required.');
      }
      if (this.editForm.get('email')?.errors?.['required']) {
        messages.push('Email is required.');
      } else if (
        !this.editForm.get('email')?.valid &&
        !this.editForm.get('email')?.errors?.['required']
      ) {
        messages.push('Email is invalid.');
      }
      this.notificationService.error(messages, 'Error');
      return false;
    }

    return true;
  }

  deletePicture() {
    this.editForm.get('photo')?.setValue(null);
    this.imageURL = undefined;
    this.isCurrentPhotoChanged = true;
  }

  showPreview(event: any) {
    this.isCurrentPhotoChanged = true;
    const file = <File>event.target.files[0];

    this.editForm.patchValue({
      photo: file,
    });
    this.editForm.get('photo')?.updateValueAndValidity();
    // File Preview
    const reader = new FileReader();
    reader.onload = () => {
      this.imageURL = reader.result as string;
    };
    reader.readAsDataURL(file);
  }
}
