import {Component, OnInit} from '@angular/core';
import {UserDetails} from "../../_commons/models/user/userDetails";
import {UserApiService} from "../../_services/user.api.service";
import {AuthService} from "../../_services/auth/auth.service";
import {MatDialog} from "@angular/material/dialog";
import {filter} from "rxjs";
import {DeleteDialogComponent} from "../../shared/delete-user-dialog/delete-dialog.component";

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
  }

  delete() {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {data: 'account'});

    dialogRef.afterClosed()
      .pipe(filter(x => !!x))
      .subscribe(result => {
        this.userService.delete()
          .pipe(filter(x => !!x))
          .subscribe(reult => {
            localStorage.removeItem('authorizationToken');
            localStorage.removeItem('currentUser');
            localStorage.removeItem('isDarkTheme');
            window.location.reload();
          })
      });
  }
}
