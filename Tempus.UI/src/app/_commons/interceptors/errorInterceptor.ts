import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { NotificationService } from '../../_services/notification.service';
import { AngularFireAuth } from '@angular/fire/compat/auth';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(
    private notificationService: NotificationService,
    private fbAuth: AngularFireAuth
  ) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((err) => {
        if (err.status === 401) {
          localStorage.clear();
          this.fbAuth.signOut();
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
