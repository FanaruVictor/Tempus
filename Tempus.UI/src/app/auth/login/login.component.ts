import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {F} from "@angular/cdk/keycodes";
import {AuthService} from "../../_services/auth/auth.service";
import {ActivatedRoute, Router} from "@angular/router";
import {first} from "rxjs";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit{
  loginForm = new FormGroup({
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required])
  });
  returnUrl!: string;
  submitted = false;

  constructor(private authService: AuthService, private router: Router, private route: ActivatedRoute) {
    if(this.authService.authorizationTokenValue){
      this.router.navigate(['/']);
    }
  }

  ngOnInit(): void {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  submit(){
    this.submitted = true;
    this.authService.login(this.loginForm.controls['username'].value, this.loginForm.controls['password'].value)
      .pipe(first())
      .subscribe( response => {
          this.router.navigate([this.returnUrl]);
        }
      );
  }

}
