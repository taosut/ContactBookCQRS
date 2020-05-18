
import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RestService } from 'app/core/services/http/rest.service';
import { Contact } from 'app/core/models/Contact';

@Injectable({
  providedIn: 'root'
})
export class ContactService extends RestService {

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl);
  }

  public createContact(category: Contact){
    return this.post("contact", category);
  }

  public deleteContact(contactId: string){
    return this.delete("contact/" + contactId);
  }

  public updateContact(contact: Contact){
    return this.put("contact/" + contact.id, contact);
  }
}
