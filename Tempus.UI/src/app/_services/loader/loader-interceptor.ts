import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {finalize, Observable} from 'rxjs';
import {LoaderService} from "./loader.service";

@Injectable({
  providedIn: 'root'
})
export class LoaderInterceptor implements HttpInterceptor {

  constructor(public loaderService: LoaderService) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let isDone = false;
    setTimeout(() => {
      if (!isDone) {
        this.loaderService.isLoading.next(true);
      }
    }, 400);

    return next.handle(req).pipe(
      finalize(
        () => {
          isDone = true;
          if (this.loaderService.isLoading.value) {
            this.loaderService.isLoading.next(false);
          }
        }
      )
    );
  }
}
