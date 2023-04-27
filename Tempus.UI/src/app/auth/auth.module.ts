import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AuthRoutingModule } from './auth-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { LogoutComponent } from './logout/logout.component';
import { MatCardModule } from '@angular/material/card';
import { CoolSocialLoginButtonsModule } from '@angular-cool/social-login-buttons';

@NgModule({
  declarations: [LoginComponent, RegisterComponent, LogoutComponent],
  imports: [
    CommonModule,
    AuthRoutingModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatInputModule,
    MatIconModule,
    MatCardModule,
    CoolSocialLoginButtonsModule
  ],
})
export class AuthModule {}
