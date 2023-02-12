import {Component, OnInit} from '@angular/core';
import {UserDetails} from "../../_commons/models/user/userDetails";
import {UserApiService} from "../../_services/user.api.service";
import {AuthService} from "../../_services/auth/auth.service";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  user!: UserDetails;

  constructor(private userService: UserApiService, private authService: AuthService) {
  }

  ngOnInit() {
    this.authService.user
      .subscribe(user => {
        if (!user) {
          return;
        }
        this.user = user;
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

  imageInputChange(fileInputEvent: any) {
    let file = <File>fileInputEvent.target.files[0];
    if (this.user.photo) {
      this.userService.updatePhoto(this.user.photo.id, file).subscribe(response => {
        this.user.photo = response.resource;
        this.authService.setUser(this.user);
      });
      return;
    }

    this.userService.addPhoto(file).subscribe(response => {
      this.user.photo = response.resource;
      this.authService.setUser(this.user);
    });
  }


}
