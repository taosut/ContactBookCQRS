import { Injectable, Injector } from '@angular/core';
import {
  HttpEvent, HttpRequest, HttpHandler,
  HttpInterceptor, HttpErrorResponse, HTTP_INTERCEPTORS, HttpResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { NotificationService } from '../services/notification.service';
import { AuthService } from '../services/auth.service';

@Injectable()
export class ServerErrorInterceptor implements HttpInterceptor {

  constructor(
    private injector: Injector,
    private authenticationService: AuthService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    let errorMessage: string;
    const notifier = this.injector.get(NotificationService);

    return next.handle(request).pipe(
      //retry(1),
      catchError((error: any) => {
        if (error instanceof HttpErrorResponse) {
          if (error.status === 401) {
            errorMessage = 'Invalid access authentication provided';
            notifier.showError(errorMessage);
            this.authenticationService.logout();
          } else {
            errorMessage = this.sumarizeErrors(error);
            notifier.showError(errorMessage);
            return throwError(error);
          }
        }
      }));
  }

  sumarizeErrors(errorResponse: any): string {
    let errorList: string[];
    let errorMessage: string = ' ';
    if(errorResponse && errorResponse.error) {
      errorList = errorResponse.error.errors;
      if(errorList.length > 0 ){
        errorList.forEach(message => {
          errorMessage = errorMessage.concat(message);
        });
      }
    }

    return errorMessage;
  }
}

export const ErrorInterceptorProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: ServerErrorInterceptor,
    multi: true
};

