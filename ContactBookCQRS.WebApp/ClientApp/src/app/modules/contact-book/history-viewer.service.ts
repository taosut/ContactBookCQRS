import { Injectable, Inject } from '@angular/core';
import { CategoryHistoryData } from 'app/core/models/CategoryHistoryData';
import { RestService } from 'app/core/services/http/rest.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ContactHistoryData } from 'app/core/models/ContactHistoryData';

@Injectable({
  providedIn: 'root'
})
export class HistoryViewerService extends RestService {

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl);
  }

  public getCategoryEventHistory(aggegateId: string): Observable<CategoryHistoryData[]>{
    return this.get("category/history/" + aggegateId);
  }

  public getContactEventHistory(aggegateId: string): Observable<ContactHistoryData[]>{
    return this.get("contact/history/" + aggegateId);
  }
}
