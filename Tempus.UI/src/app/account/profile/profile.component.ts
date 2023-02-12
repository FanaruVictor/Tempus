import {Component, OnInit} from '@angular/core';
import {UserDetails} from "../../_commons/models/user/userDetails";
import {UserApiService} from "../../_services/user.api.service";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  user!: UserDetails;

  constructor(private userService: UserApiService) {
  }

  ngOnInit() {
    this.userService.getDetails()
      .subscribe(response => {
        if (!response.resource) {
          return;
        }
        this.user = response.resource;
        console.log(this.user);
        if (this.user.isDarkTheme) {
          if (!document.body.classList.contains('dark-theme')) {
            document.body.classList.toggle('dark-theme');
          }
        } else {
          document.body.classList.remove('dark-theme');
        }
      });
  }

  edit() {

  }

  delete() {

  }

  async toggleDarkTheme() {
    document.body.classList.toggle('dark-theme');

    this.userService.changeTheme(!this.user.isDarkTheme).subscribe(response => {
      this.user = response.resource;
      localStorage.setItem("isDarkMode", (this.user.isDarkTheme).toString());
    });
  }

  imageInputChange(fileInputEvent: any) {
    let file = <File>fileInputEvent.target.files[0];
    if (this.user.photo) {
      this.userService.updatePhoto(this.user.photo.id, file).subscribe(response => {
        this.user.photo = response.resource;
      });
      return;
    }

    this.userService.addPhoto(file).subscribe(response => {
      this.user.photo = response.resource;
    });
  }


}
