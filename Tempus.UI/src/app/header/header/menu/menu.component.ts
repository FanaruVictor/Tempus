import {AfterViewChecked, AfterViewInit, Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {GenericResponse} from "../../../commons/models/genericResponse";
import {RegistrationInfo} from "../../../commons/models/registrationInfo";
import {Subscription} from "rxjs";
import {Router} from "@angular/router";

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit{
  registrations?: RegistrationInfo[] ;
  constructor(private httpClient: HttpClient, private router: Router) {
  }

  ngOnInit(): void {
    this.httpClient.get<GenericResponse<RegistrationInfo[]>>(`https://localhost:7077/api/registrations`)
      .subscribe({
        next: response => {
          this.registrations = response.resource;
        }
      });
  }

  select(id: string){
    this.router.navigate([`/registrations/${id}`]);
  }

  createRegistration(){
    this.router.navigate([`/registrations/create`]);
  }
}
