
import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { RestService } from 'app/core/services/http/rest.service';
import { Category } from 'app/core/models/Category';
import { Contact } from 'app/core/models/Contact';
import { UserRegistration } from 'app/core/models/UserRegistration';

@Injectable({
  providedIn: 'root'
})
export class AccountService extends RestService {

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl);
  }

  public registerAccount(userRegistration: UserRegistration){
    return this.post("account/register", userRegistration);
  }

}
