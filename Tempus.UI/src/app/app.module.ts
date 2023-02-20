import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatTabsModule} from "@angular/material/tabs";
import {RegistrationModule} from "./registration/registration.module";
import {CategoryModule} from "./category/category.module";
import {MatIconModule} from "@angular/material/icon";
import {MatButtonModule} from "@angular/material/button";
import {NotFoundComponent} from "./_commons/not-found/not-found.component";
import {HTTP_INTERCEPTORS} from "@angular/common/http";
import {JwtInterceptor} from "./_commons/interceptors/JwtInterceptor";
import {AuthModule} from "./auth/auth.module";
import {ToastrModule} from "ngx-toastr";
import {ErrorInterceptor} from "./_commons/interceptors/errorInterceptor";
import {MatProgressBarModule} from "@angular/material/progress-bar";
import {LoaderInterceptor} from "./_services/loader/loader-interceptor";
import {HeaderComponent} from './_commons/header/header.component';
import {MatSlideToggleModule} from "@angular/material/slide-toggle";

@NgModule({
  declarations: [
    AppComponent,
    NotFoundComponent,
    HeaderComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatTabsModule,
    RegistrationModule,
    CategoryModule,
    MatIconModule,
    MatButtonModule,
    MatSlideToggleModule,
    AuthModule,
    MatProgressBarModule,
    ToastrModule.forRoot({
      progressBar: true,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
      countDuplicates: true
    })
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true
    },
    {
      provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true
    },
    {
      provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
