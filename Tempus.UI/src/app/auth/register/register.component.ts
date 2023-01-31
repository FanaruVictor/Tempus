import {Component} from '@angular/core';
import {UntypedFormControl, UntypedFormGroup, Validators} from "@angular/forms";
import {AuthService} from "../../_services/auth/auth.service";
import {BaseUser} from "../../_commons/models/user/baseUser";
import {Router} from "@angular/router";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  registerForm = new UntypedFormGroup({
    username: new UntypedFormControl('', [Validators.required]),
    email: new UntypedFormControl('', [Validators.required, Validators.email]),
    phoneNumber: new UntypedFormControl('', [Validators.required, Validators.email]),
    password: new UntypedFormControl('', [Validators.required])
  });
  submitted = false;
  constructor(private router: Router, private authService: AuthService) {

  }

  submit() {
    let user: BaseUser = {
      userName: this.registerForm.controls['username'].value,
      email: this.registerForm.controls['email'].value,
      phoneNumber: this.registerForm.controls['phoneNumber'].value,
      password: this.registerForm.controls['password'].value,
    };

    this.authService.register(user)
      .subscribe(result => this.router.navigate(['/auth/login']));
  }
}
