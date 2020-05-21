import { Injectable, Inject } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { RestService } from './http/rest.service';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { LocalStorageService } from './local-storage.service';
import { User } from '../models/User';
import { map } from 'rxjs/operators';
import { TokenStorageService } from '../token-storage.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends RestService {
    private currentUserSubject: BehaviorSubject<User>;
    public currentUser: Observable<User>;
    private isLogged = new Subject<boolean>();

    // Observable string streams
    isLoggedAnnounced$ = this.isLogged.asObservable();

    constructor (http: HttpClient,
      private tokenStorageToken: TokenStorageService,
      private localStorageService: LocalStorageService,
      private router: Router,
      @Inject('BASE_URL') baseUrl: string) {
        super(http, baseUrl);

        var storedUser = JSON.parse(this.localStorageService.getValueByKey('currentUser'));
        this.currentUserSubject = new BehaviorSubject<User>(storedUser);
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): User {
      return this.currentUserSubject.value;
    }

    login(email: string, password: string) {
      return this.post("account/login", { email, password })
          .pipe(map(user => {
            // login successful if there's a jwt token in the response
            if (user && user.data.token) {
              // store user details and jwt token in local storage to keep user logged in between page refreshes
              this.localStorageService.setValue('currentUser', JSON.stringify(user.data));
              this.tokenStorageToken.saveToken(user.data.token);
              this.currentUserSubject.next(user.data);
              this.isLogged.next(true);
            }

            return user;
        }));
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        this.tokenStorageToken.clearToken();
        this.currentUserSubject.next(null);

        //broadcasting to listeners
        this.isLogged.next(false);

        // redirects
        this.router.navigate(["/login"]);
    }
}
