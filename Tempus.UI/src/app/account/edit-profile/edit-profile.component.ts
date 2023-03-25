import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {UserDetails} from "../../_commons/models/user/userDetails";
import {UserApiService} from "../../_services/user.api.service";
import {filter} from "rxjs";
import {FormBuilder, FormGroup, UntypedFormControl, UntypedFormGroup, Validators} from "@angular/forms";
import {ProfilePhotoApiService} from "../../_services/profile-photo.api.service";
import {UpdateUserData} from "../../_commons/models/user/updateUserData";

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.scss']
})
export class EditProfileComponent implements OnInit {
  user!: UserDetails;
  file?: File;
  editForm: FormGroup;
  imageURL?: string;
  private isCurrentPhotoChanged = false;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private profilePhotoApiService: ProfilePhotoApiService,
    private userApiService: UserApiService,
    private fb: FormBuilder
  ) {
    this.editForm = this.fb.group({
      photo: [null],
      userName: ['', Validators.required],
      phoneNumber: [''],
      email: ['', Validators.email]
    })
  }

  ngOnInit(): void {
    this.userApiService.getDetails()
      .pipe(filter(x => !!x))
      .subscribe(response => {
        this.user = response.resource;
        this.editForm.patchValue(this.user);
        console.log(this.editForm);
        this.imageURL = this.user.photo?.url;
      });
  }

  submit(){
    let userData: UpdateUserData = {
      userName: this.editForm.get('userName')?.value,
      phoneNumber: this.editForm.get('phoneNumber')?.value,
      email: this.editForm.get('email')?.value,
      isCurrentPhotoChanged: this.isCurrentPhotoChanged,
      newPhoto: this.editForm.get('photo')?.value
    }

    this.userApiService.update(userData)
      .subscribe(response => {
        this.userApiService.setUser(response.resource);
        this.router.navigate(['/account']);
      })
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
      photo: file
    });
    this.editForm.get('photo')?.updateValueAndValidity()
    // File Preview
    const reader = new FileReader();
    reader.onload = () => {
      this.imageURL = reader.result as string;
    }
    reader.readAsDataURL(file)
  }

}
