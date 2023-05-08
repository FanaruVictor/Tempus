import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  constructor(private toastr: ToastrService) {}

  succes(message: string, title: string) {
    this.toastr.success(message, title);
  }

  error(messages: object, title: string) {
    let message = '';
    for (let prop in messages) {
      if (Object.prototype.hasOwnProperty.call(messages, prop)) {
        message += messages[prop] + ' ';
      }
    }
    this.toastr.error(message, title);
  }

  warn(message: string, title: string) {
    this.toastr.warning(message, title, {
      enableHtml: true,
    });
  }

  showRegistrationUpdatedMessage(message: string, title: string) {
    this.toastr.warning(message, title, {
      closeButton: true,
      disableTimeOut: true,
    });
  }
}
