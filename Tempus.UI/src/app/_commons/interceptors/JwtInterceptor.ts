import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/_services/auth/auth.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    let authorizationToken = '';

    this.authService.authorizationToken.subscribe((token) => {
      authorizationToken = token;
    });
    //de setat tokenul din local storage
    if (!!authorizationToken) {
      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${authorizationToken}`,
        },
      });
    }

    return next.handle(req);
  }
}
