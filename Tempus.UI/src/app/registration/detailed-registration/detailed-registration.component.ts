import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {RegistrationApiService} from "../../_services/registration.api.service";
import {BaseRegistration} from "../../_commons/models/registrations/baseRegistration";
import {FileService} from "../../_services/file.service";
import {NotificationService} from "../../_services/notification.service";

@Component({
  selector: 'app-detailed-registration',
  templateUrl: './detailed-registration.component.html',
  styleUrls: ['./detailed-registration.component.scss']
})
export class DetailedRegistrationComponent implements OnInit {
  registration?: BaseRegistration;
  id: string = '';
  message: string = '';

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private registrationApiService: RegistrationApiService,
    private fileService: FileService,
    private notificationService: NotificationService
  ) {
  }

  ngOnInit() {
    this.id = this.activatedRoute.snapshot.params['id'];
    this.getRegistration();
  }

  getRegistration() {
    this.registrationApiService.getById(this.id)
      .subscribe({
        next: response => {
          this.registration = response.resource
        }
      });
  }

  edit() {
    this.router.navigate(['/registrations/edit', this.id]);
  }

  delete() {
    this.registrationApiService.delete(this.id).subscribe({
      next: () => {
        this.router.navigate(['/registrations/overview']);
        this.notificationService.succes('Registration deleted successfully', 'Request completed');
      }
    });
  }

  download(): void {
    if (!!this.registration)
      this.fileService.download(this.registration.id).subscribe({
        next: data => {
          let FileSaver = require('file-saver');
          const byteCharacters = atob(data.resource);
          const byteNumbers = new Array(byteCharacters.length);
          for (let i = 0; i < byteCharacters.length; i++) {
            byteNumbers[i] = byteCharacters.charCodeAt(i);
          }
          const byteArray = new Uint8Array(byteNumbers);
          const blob = new Blob([byteArray], {type: "application/pdf"});
          FileSaver.saveAs(blob, `${this.registration?.title}.pdf`);
          this.notificationService.succes('Registration downloaded successfully', 'Request completed');
        }
      });
  }

  isOverflow(element: HTMLElement): boolean {

    const isOverflowing = element.clientWidth < element.scrollWidth || element.clientHeight < element.scrollHeight;

    return isOverflowing;
  }
}
