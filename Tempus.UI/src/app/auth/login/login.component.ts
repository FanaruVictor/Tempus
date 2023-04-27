import { Component, OnInit } from '@angular/core';
import {
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { AuthService } from '../../_services/auth/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs';
import { environment } from '../../../environments/environment';
import { CredentialResponse } from 'google-one-tap';
import {
  FacebookLoginProvider,
  SocialAuthService,
} from '@abacritt/angularx-social-login';
import { faFacebookF } from '@fortawesome/free-brands-svg-icons';
import { FacebookLoginInfo } from 'src/app/_commons/models/auth/facebookLoginInfo';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  loginForm = new UntypedFormGroup({
    email: new UntypedFormControl('', [Validators.required]),
    password: new UntypedFormControl('', [Validators.required]),
  });
  returnUrl!: string;
  submitted = false;

  constructor(
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    private socialAuthService: SocialAuthService
  ) {
    if (localStorage.getItem('authorizationToken')) {
      this.router.navigate(['/registrations']);
    }
  }

  ngOnInit(): void {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    this.createGoogleButton();
  }

  createGoogleButton() {
    //@ts-ignore
    window.onGoogleLibraryLoad = () => {
      //@ts-ignore
      google.accounts.id.initialize({
        client_id: environment.googleClientId,
        callback: this.handleCredentialResponse.bind(this),
        auto_select: false,
        cancel_on_tap_outside: true,
      });
      //@ts-ignore
      google.accounts.id.renderButton(
        //@ts-ignore
        document.getElementById('google-login'),
        { theme: 'outline', size: 'large', type: 'standard', width: '300' }
      );

      //@ts-ignore
      google.accounts.id.prompt(() => {});
    };
  }

  async handleCredentialResponse(response: CredentialResponse) {
    await this.authService
      .loginWithGoogle(response.credential)
      .subscribe((x) => {
        this.router.navigate([this.returnUrl]).then(() => location.reload());
      });
  }

  submit() {
    this.submitted = true;
    this.authService
      .login(
        this.loginForm.controls['email'].value,
        this.loginForm.controls['password'].value
      )
      .pipe(first())
      .subscribe({
        next: () => {
          this.router.navigate([this.returnUrl]).then(() => location.reload());
        },
        error: () => (this.submitted = false),
      });
  }

  loginWithFacebook() {
    this.socialAuthService
      .signIn(FacebookLoginProvider.PROVIDER_ID)
      .then((x) => {
        const facebookLoginInfo: FacebookLoginInfo = {
          email: x.email,
          externalId: x.id,
          username: x.name,
          photoUrl: x.response.picture.data.url,
        };
        debugger;
        this.authService
          .loginWithFacebook(facebookLoginInfo)
          .pipe(first())
          .subscribe({
            next: () => {
              debugger;
              this.router
                .navigate([this.returnUrl])
                .then(() => location.reload());
            },
            error: () => {
              this.submitted = false;
              debugger;
            },
          });
      });
  }

  faFacebookF = faFacebookF;
}
