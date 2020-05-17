
import { Observable } from 'rxjs';
import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { RestService } from 'app/core/services/http/rest.service';
import { Category } from 'app/core/models/Category';
import { Contact } from 'app/core/models/Contact';

@Injectable({
  providedIn: 'root'
})
export class CategoryService extends RestService {

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl);
  }

  public createCategory(category: Category){
    return this.post("category", category);
  }

  public deleteCategory(categoryId: string){
    return this.delete("category/" + categoryId);
  }

  public getCategories(): Observable<Category[]>{
    return this.get("category");
  }

  public getContacts(categoryId: string): Observable<Contact[]>{
    return this.get("category/" + categoryId + "/contacts");
  }
}
