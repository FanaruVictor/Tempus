import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {UserDetails} from "../../_commons/models/user/userDetails";
import {UserApiService} from "../../_services/user.api.service";
import {filter} from "rxjs";
import {UntypedFormControl, UntypedFormGroup, Validators} from "@angular/forms";
import {ProfilePhotoApiService} from "../../_services/profile-photo.api.service";

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.scss']
})
export class EditProfileComponent implements OnInit {
  user!: UserDetails;
  file?: File;

  editForm = new UntypedFormGroup({
    photo: new UntypedFormControl('', [Validators.required]),
    username: new UntypedFormControl('', [Validators.required]),
    phoneNumber: new UntypedFormControl('', [Validators.required]),
    email: new UntypedFormControl('', [Validators.required]),
  });
   imageURL: string = '';


  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private profilePhotoApiService: ProfilePhotoApiService,
    private userApiService: UserApiService
  ) {
  }

  ngOnInit(): void {
    this.userApiService.getDetails()
      .pipe(filter(x => !!x))
      .subscribe(response => {
        this.user = response.resource;
        this.editForm.patchValue(this.user);
      });
  }

  submit(){

  }

  imageInputChange(fileInputEvent: any) {
    let file = <File>fileInputEvent.target.files[0];
    this.file = file;
    this.profilePhotoApiService.ChangeProfilePicture(file, this.user.photo)
      .subscribe(response => {
        this.user.photo = response.resource;
        this.userApiService.setUser(this.user);
      });
  }

  deletePicture() {
    if (this.user.photo) {
      this.profilePhotoApiService.deleteProfilePicture(this.user.photo?.id)
        .subscribe(response => {
          if (!response.resource) {
            console.log('nasoll')
          }
          this.user.photo = undefined;
          this.userApiService.setUser(this.user);
        });
    }
  }

}
