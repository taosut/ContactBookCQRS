import { Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor() { }

  showSuccess(message: string): void {
    alert(message);
  }

  showError(message: string): void {
    alert(message);
  }
}
