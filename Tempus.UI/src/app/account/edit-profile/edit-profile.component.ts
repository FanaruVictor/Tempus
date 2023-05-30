import { Component, OnInit } from '@angular/core';
import { UserApiService } from '../../_services/user.api.service';
import { filter } from 'rxjs';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NotificationService } from 'src/app/_services/notification.service';
import { AuthService } from 'src/app/_services/auth/auth.service';
import { User } from 'src/app/_commons/models/user/user';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.scss'],
})
export class EditProfileComponent implements OnInit {
  user!: User;
  file?: File;
  editForm: FormGroup;
  imageURL?: string;
  isCurrentPhotoChanged = false;

  constructor(
    private router: Router,
    private userApiService: UserApiService,
    private fb: FormBuilder,
    private notificationService: NotificationService,
    private authService: AuthService
  ) {
    this.editForm = this.fb.group({
      photo: [null],
      displayName: ['', Validators.required],
      email: ['', [Validators.email, Validators.required]],
    });
  }

  ngOnInit(): void {
    this.authService.user.subscribe((x) => {
      this.user = x;
    });
    this.authService.user.subscribe((response) => {
      this.editForm.patchValue(response);
      this.imageURL = this.user.photoURL;
    });
  }

  submit() {
    if (!this.isValidForm()) {
      return;
    }

    let userData = {
      username: this.editForm.get('username')?.value,
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
      this.authService.setUser(response.resource);
      this.router.navigate(['/account']);
    });
  }

  isValidData(user): boolean {
    const result =
      this.user.displayName !== user.displayName ||
      this.user.email !== user.email ||
      user.newPhoto instanceof File ||
      this.isCurrentPhotoChanged;

    return result;
  }

  isValidForm() {
    let messages: string[] = [];
    if (!this.editForm.valid) {
      if (this.editForm.get('displayName')?.errors?.['required']) {
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
