import {Component, OnInit} from '@angular/core';
import {UserDetails} from "../../_commons/models/user/userDetails";
import {UserApiService} from "../../_services/user.api.service";
import {MatDialog} from "@angular/material/dialog";
import {AddOrUpdateProfilePhotoComponent} from "./add-or-update-profile-photo/add-or-update-profile-photo.component";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  isChecked = false;
  user?: UserDetails;

  constructor(private userService: UserApiService, private dialog: MatDialog,) {
    this.userService.getDetails()
      .subscribe({
        next: response => {
          if(!!response.resource){
            this.user = response.resource;

            this.isChecked = this.user.isDarkTheme;
            if(this.isChecked){
              if(!document.body.classList.contains('dark-theme')) {
                document.body.classList.toggle('dark-theme');
              }
            }
            else {
              document.body.classList.remove('dark-theme');
            }
          }
        }
      })
  }

  ngOnInit() {

  }

  edit() {

  }

  delete(){

  }

  toggleDarkTheme(): void {
    document.body.classList.toggle('dark-theme');

    this.userService.changeTheme(!this.isChecked)
      .subscribe({
        next: response => {
          this.user = response.resource;
          this.isChecked = this.user.isDarkTheme;
        }
      })

  }

  addPhoto(){
    const dialogRef = this.dialog.open(AddOrUpdateProfilePhotoComponent, {
      data: this.user?.photoDetails,
    });
    dialogRef.afterClosed().subscribe(result => {
      if(result){
      this.userService.uploadPicture(result)
        .subscribe((response : any) => {
          console.log(response);
        })
      }
    });
  }


}
