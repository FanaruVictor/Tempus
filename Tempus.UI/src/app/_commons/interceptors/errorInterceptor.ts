import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from '../../_services/auth/auth.service';
import { catchError, Observable, throwError } from 'rxjs';
import { NotificationService } from '../../_services/notification.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(
    private authService: AuthService,
    private notificationService: NotificationService
  ) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((err) => {
        if (err.status === 401) {
          this.authService.signOut();
          location.reload();
        }
        console.log(err);
        const errors = err.error.errors || [err.statusText];

        this.notificationService.error(
          errors,
          'Something went bad, plese try again'
        );

        return throwError(err);
      })
    );
  }
}
