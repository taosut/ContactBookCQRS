import { Injectable, Injector } from '@angular/core';
import {
  HttpEvent, HttpRequest, HttpHandler,
  HttpInterceptor, HttpErrorResponse, HTTP_INTERCEPTORS, HttpResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { ErrorService } from './../services/error-handling/error.service';
import { NotificationService } from '../services/notification.service';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable()
export class ServerErrorInterceptor implements HttpInterceptor {

  constructor(
    private injector: Injector,
    private authenticationService: AuthService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    let message;
    const errorService = this.injector.get(ErrorService);
    const notifier = this.injector.get(NotificationService);

    return next.handle(request).pipe(
      retry(1),
      catchError((error: any) => {
        if (error instanceof HttpErrorResponse) {
          if (error.status === 401) {
            message = 'Invalid access authentication provided';
            notifier.showError(message);
            this.authenticationService.logout();
          } else {
            message = errorService.getServerErrorMessage(error);
            notifier.showError(message);
            return throwError(error);
          }
        }
      }));
  }
}

export const ErrorInterceptorProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: ServerErrorInterceptor,
    multi: true
};
