import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {HttpClient, HttpEvent, HttpEventType, HttpResponse} from "@angular/common/http";
import {RegistrationApiService} from "../../_services/registration.api.service";
import {BaseRegistration} from "../../_commons/models/registrations/baseRegistration";
import {GenericResponse} from "../../_commons/models/genericResponse";
import {FileService} from "../../_services/file.service";
import {log} from "util";
@Component({
  selector: 'app-detailed-registration',
  templateUrl: './detailed-registration.component.html',
  styleUrls: ['./detailed-registration.component.scss']
})
export class DetailedRegistrationComponent implements OnInit{
  registration?: BaseRegistration;
  id: string = '';
  message: string = '';
  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private httpClient: HttpClient,
    private registrationApiService: RegistrationApiService,
    private fileService: FileService) {
  }

  ngOnInit(){
    this.id = this.activatedRoute.snapshot.params['id'];
    this.getRegistration();
  }

  getRegistration(){
    console.log(this.id)
      this.httpClient.get<GenericResponse<BaseRegistration>>(`https://localhost:7077/api/v1/registrations/${this.id}`)
      .subscribe({
        next: response => {
          this.registration = response.resource
          if (this.registration == undefined) {
            console.log("error")
          }
        },
        error: errors => {
          console.log(errors.errors)
        }
      });
  }

  edit(){
    this.router.navigate(['/registrations/edit', this.id]);
  }

  delete(){
    this.registrationApiService.delete(this.id).subscribe(response => {
      this.router.navigate(['/registrations/overview'])
    })
  }

  download(): void {
    if(!!this.registration)
      this.fileService.download(this.registration.id).subscribe(data => {
        let FileSaver = require('file-saver');
        const byteCharacters = atob(data.resource);
        const byteNumbers = new Array(byteCharacters.length);
        for (let i = 0; i < byteCharacters.length; i++) {
          byteNumbers[i] = byteCharacters.charCodeAt(i);
        }
        const byteArray = new Uint8Array(byteNumbers);
        const blob = new Blob([byteArray], {type: "application/pdf"});
        FileSaver.saveAs(blob, `${this.registration?.title}.pdf`);
      });
  }

}
