import {Component, OnInit} from '@angular/core';
import {BaseRegistration} from "../../commons/models/registrations/baseRegistration";
import {ActivatedRoute, Router} from "@angular/router";
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
  id: string = '';
  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private httpClient: HttpClient,
    private registrationApiService: RegistrationApiService) {
  }

  ngOnInit(){
    this.id = this.activatedRoute.snapshot.params['id'];
    this.getRegistration(this.id);
  }

  getRegistration(id: string){
      this.httpClient.get<GenericResponse<BaseRegistration>>(`https://localhost:7077/api/v1/registrations/${id}`)
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

  download(){

  }
}
