import {Injectable} from '@angular/core';
import {ToastrService} from "ngx-toastr";

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private toastr: ToastrService) {
  }

  succes(message: string, title: string) {
    this.toastr.success(message, title);
  }

  error(messages: string[], title: string) {
    let totalMessage = '';
    messages.forEach(x => totalMessage += x + '\n\n');
    this.toastr.error(totalMessage, title);
  }

  warn(message: string, title: string) {
    this.toastr.warning(message, title);
  }
}
