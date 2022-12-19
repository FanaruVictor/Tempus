import {Component, OnInit} from '@angular/core';
import {BaseRegistration} from "../../commons/models/registrations/baseRegistration";
import {Router} from "@angular/router";
import {GenericResponse} from "../../commons/models/genericResponse";
import {HttpClient} from "@angular/common/http";
import {RegistrationApiService} from "../../commons/services/registration.api.service";

@Component({
  selector: 'app-detailed-registration',
  templateUrl: './detailed-registration.component.html',
  styleUrls: ['./detailed-registration.component.scss']
})
export class DetailedRegistrationComponent implements OnInit{
  registration?: BaseRegistration;

  constructor(private router: Router, private httpClient: HttpClient, private registrationApiService: RegistrationApiService) {
  }

  ngOnInit(){
    let url = this.router.url;
    let id = url.substring(url.lastIndexOf('/') + 1);
    this.getRegistration(id);
  }

  getRegistration(id: string){
    this.httpClient.get<GenericResponse<BaseRegistration>>(`https://localhost:7077/api/registrations/${id}`)
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

  delete(){
    let id = this.registration?.id ?? '';
    this.registrationApiService.delete(id).subscribe(response => {
      this.router.navigate(['/registrations/overview'])
    })
  }
}
