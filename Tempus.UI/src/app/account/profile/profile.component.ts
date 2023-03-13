import {Component, OnInit} from '@angular/core';
import {UserDetails} from "../../_commons/models/user/userDetails";
import {UserApiService} from "../../_services/user.api.service";
import {AuthService} from "../../_services/auth/auth.service";
import {MatDialog} from "@angular/material/dialog";
import {DeleteDialogComponent} from "../../_commons/components/delete-user-dialog/delete-dialog.component";
import {filter} from "rxjs";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  user!: UserDetails;

  constructor(private userService: UserApiService, private authService: AuthService, private dialog: MatDialog) {
  }

  ngOnInit() {
    this.userService.user
      .subscribe(user => {
        if (!user) {
          return;
        }
        this.user = user;
        if (this.user.isDarkTheme) {
          if (!document.body.classList.contains('dark-theme')) {
            document.body.classList.toggle('dark-theme');
          }
        } else {
          document.body.classList.remove('dark-theme');
        }
      });
    console.log(this.user.photo);
  }

  edit() {

  }

  delete() {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {data: 'account'});

    dialogRef.afterClosed()
      .pipe(filter(x => !!x))
      .subscribe(result => {
        this.userService.delete()
          .subscribe(reult => {
            if(!!result){
              localStorage.removeItem('authorizationToken');
              localStorage.removeItem('currentUser');
              localStorage.removeItem('isDarkTheme');
              window.location.reload();
            }
          })
    });
  }

  imageInputChange(fileInputEvent: any) {
    let file = <File>fileInputEvent.target.files[0];
    if (this.user.photo) {
      this.userService.updatePhoto(this.user.photo.id, file).subscribe(response => {
        this.user.photo = response.resource;
        this.userService.setUser(this.user);
      });
      return;
    }

    this.userService.addPhoto(file).subscribe(response => {
      this.user.photo = response.resource;
      this.userService.setUser(this.user);
    });
  }


}
