import {Component, OnInit} from '@angular/core';
import {UntypedFormControl, UntypedFormGroup, Validators} from "@angular/forms";
import {AuthService} from "../../_services/auth/auth.service";
import {ActivatedRoute, Router} from "@angular/router";
import {first} from "rxjs";
import {environment} from "../../../environments/environment";
import {CredentialResponse} from "google-one-tap";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm = new UntypedFormGroup({
    email: new UntypedFormControl('', [Validators.required]),
    password: new UntypedFormControl('', [Validators.required])
  });
  returnUrl!: string;
  submitted = false;

  constructor(private authService: AuthService, private router: Router, private route: ActivatedRoute) {
    if (localStorage.getItem("authorizationToken")) {
      this.router.navigate(['/registrations']);
    }
  }

  ngOnInit(): void {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    this.createGoogleButton()
  }

  createGoogleButton(){
    //@ts-ignore
    window.onGoogleLibraryLoad = () => {
      //@ts-ignore
      google.accounts.id.initialize({
        client_id: environment.googleClientId,
        callback: this.handleCredentialResponse.bind(this),
        auto_select: false,
        cancel_on_tap_outside: true
      });
      //@ts-ignore
      google.accounts.id.renderButton(
        //@ts-ignore
        document.getElementById("buttonDiv"),
        {theme: 'outline', size: 'large', width: '100%'}
      );

      //@ts-ignore
      google.accounts.id.prompt((notification: PromptMomentNotification) => {});
    }
  }

  async handleCredentialResponse(response: CredentialResponse) {
    await this.authService.loginWithGoogle(response.credential)
      .subscribe(x => {
        this.router.navigate([this.returnUrl]).then(() => location.reload())
      })
  }

  submit() {
    this.submitted = true;
    this.authService.login(this.loginForm.controls['email'].value, this.loginForm.controls['password'].value)
      .pipe(first())
      .subscribe(() => {
          this.router.navigate([this.returnUrl]).then(() => location.reload());
        }
      );
  }

}
