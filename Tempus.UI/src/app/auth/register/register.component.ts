import {Component} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {AuthService} from "../../_services/auth/auth.service";
import {User} from "../../_commons/models/user/User";
import {Router} from "@angular/router";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  registerForm = new FormGroup({
    username: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required])
  });
  submitted = false;
  constructor(private router: Router, private authService: AuthService) {

  }

  submit() {
    let user: User = {
      username: this.registerForm.controls['username'].value,
      email: this.registerForm.controls['email'].value,
      password: this.registerForm.controls['password'].value,
    }
    this.authService.register(user)
      .subscribe(result => this.router.navigate(['/auth/login']));
  }
}
