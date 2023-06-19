import { Component, OnInit } from '@angular/core';
import {
  FirebaseUISignInFailure,
  FirebaseUISignInSuccessWithAuthResult,
} from 'firebaseui-angular';

@Component({
  selector: 'app-register-or-login',
  templateUrl: './register-or-login.component.html',
  styleUrls: ['./register-or-login.component.scss'],
})
export class RegisterOrLoginComponent implements OnInit {
  constructor() {}

  ngOnInit(): void {}

  successLoginCallback(event: FirebaseUISignInSuccessWithAuthResult) {
    console.log('login success');
  }

  errorLoginCallback(event: FirebaseUISignInFailure) {
    console.log('login failed');
  }
}
