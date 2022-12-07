import {Component, OnInit} from '@angular/core';
import {SlideUpDownAnimation} from "../../commons/Animations";
import {HttpClient} from "@angular/common/http";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {GenericResponse} from "../../commons/models/genericResponse";
import {DetailedRegistration} from "../../commons/models/detailedRegistration";
import {CreateRegistration} from "../../commons/models/createRegistration";

@Component({
  selector: 'app-create-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss'],
  animations: [SlideUpDownAnimation]
})
export class RegistrationComponent implements OnInit{
  registration: DetailedRegistration | undefined;
  isCreateForm = true;
  isEditForm = true;
  isDelete = false;

  registrationForm = new FormGroup({
    title: new FormControl('Title',[Validators.required]),
    content: new FormControl('', [Validators.required])
  });

  constructor(private httpClient: HttpClient, private router: Router) {
  }

  ngOnInit(): void {
    let url = this.router.url;
    if(this.isCreate(url)) {
      this.isEditForm = false;
    }
    else if(this.isEdit(url)){
      this.isCreateForm = false;
      let id = url.substring(url.lastIndexOf('/') + 1);
      this.getRegistration(id);
    }
    else{
      let id = url.substring(url.lastIndexOf('/') + 1);
      if(id && id != 'registrations'){
        this.getRegistration(id);
      }
      else{
        this.getLastUpdated();
        this.router.navigate([])
      }
      this.isCreateForm = false;
      this.isEditForm = false;
    }

  }

  isCreate(url: string): boolean{
    return url.includes('/create');
  }

  isEdit(url: string): boolean{
    return url.includes('/edit');
  }

  getRegistration(id: string){
    this.httpClient.get<GenericResponse<DetailedRegistration>>(`https://localhost:7077/api/registrations/${id}`)
      .subscribe({
        next: response => {
          this.registration = response.resource
          if (this.registration == undefined) {
            console.log("error")
          }
          else{
            this.registrationForm.controls['title'].setValue(this.registration.title);
            this.registrationForm.controls['content'].setValue(this.registration.content);
          }
        },
        error: errors => {
          console.log(errors.errors)
        }
      });
  }

  getLastUpdated(){
    this.httpClient.get<GenericResponse<DetailedRegistration>>(`https://localhost:7077/api/registrations/lastUpdated`)
      .subscribe({
        next: response => {
          console.log(response)
          this.registration = response.resource
          if (this.registration == undefined) {
            console.log("error")
          }
          else{
            this.registrationForm.controls['title'].setValue(this.registration.title);
            this.registrationForm.controls['content'].setValue(this.registration.content);
          }
        },
        error: errors => {
          console.log(errors.errors)
        }
      });
  }

  onSubmit() {
    if(this.isDelete) return;
    if(!this.isEditForm && !this.isCreateForm){
      this.router.navigate([`/registrations/edit/${this.registration?.id}`])
      return;
    }

    if(this.registration){
      this.registration.title = this.registrationForm.controls['title'].value;
      this.registration.content = this.registrationForm.controls['content'].value;

      this.httpClient.put<GenericResponse<DetailedRegistration>>(`https://localhost:7077/api/registrations`, this.registration)
        .subscribe({
          next: result => {
            this.registration = result.resource;
            if (this.registration == undefined) {
              console.log("error")
            }
            else{
              this.router.navigate([`/registrations/${this.registration?.id}`])
            }
          }
        });
    }
    else {
      let registration: CreateRegistration = {
        title: this.registrationForm.controls['title'].value,
        content: this.registrationForm.controls['content'].value,
        categoryId: 'cb2a53a4-02f7-4057-c94c-08dad3e5d366'
      };

      this.httpClient.post<GenericResponse<DetailedRegistration>>(`https://localhost:7077/api/registrations`, registration)
        .subscribe({
          next: result => {
            this.registration = result.resource;
            if (this.registration == undefined) {
              console.log("error")
            }
            else{
              this.router.navigate([`/registrations/${this.registration?.id}`])
            }
          }
        });
    }
  }

  cancel(){
    console.log(1)
    this.router.navigate([`/registrations/${this.registration?.id}`])
  }

  delete(){
    if(this.registration){
      this.isDelete = true;
      this.httpClient.delete<GenericResponse<string>>(`https://localhost:7077/api/registrations/${this.registration.id}`)
        .subscribe(response => {
          this.router.navigate([`/registrations`])
        })
    }
  }
}
