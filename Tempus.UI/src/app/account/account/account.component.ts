import { Component, OnInit } from '@angular/core';
import { UserDetails } from '../../_commons/models/user/userDetails';
import { UserApiService } from '../../_services/user.api.service';
import { MatDialog } from '@angular/material/dialog';
import { filter } from 'rxjs';
import { DeleteDialogComponent } from './delete-user-dialog/delete-dialog.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.scss'],
})
export class AccountComponent implements OnInit {
  user!: UserDetails;

  constructor(
    private userService: UserApiService,
    private dialog: MatDialog,
    private router: Router
  ) {}

  ngOnInit() {
    this.userService.user.subscribe((user) => {
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
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: 'account',
    });

    dialogRef
      .afterClosed()
      .pipe(filter((x) => !!x))
      .subscribe((result) => {
        this.userService
          .delete()
          .pipe(filter((x) => !!x))
          .subscribe(() => {
            localStorage.removeItem('authorizationToken');
            localStorage.removeItem('currentUser');
            localStorage.removeItem('isDarkTheme');
            this.router.navigate(['/login']);
          });
      });
  }
}
