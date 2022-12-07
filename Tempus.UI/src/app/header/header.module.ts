import {NgModule} from '@angular/core';
import {HeaderComponent} from "./header/header.component";
import {EditSliderComponent} from "./header/edit-slider/edit-slider.component";
import {ButtonComponent} from "./button/button.component";
import {MatIconModule} from "@angular/material/icon";
import {BrowserModule} from "@angular/platform-browser";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {MenuComponent} from './header/menu/menu.component';
import {CommonModule} from "@angular/common";
import {HttpClientModule} from "@angular/common/http";
import {RouterModule} from "@angular/router";

@NgModule({
  declarations: [
    HeaderComponent,
    EditSliderComponent,
    ButtonComponent,
    MenuComponent,
  ],
  imports: [
    MatIconModule,
    BrowserModule,
    BrowserAnimationsModule,
    CommonModule,
    HttpClientModule,
    RouterModule
  ],
  providers: [],

  exports: [
    HeaderComponent
  ]
})
export class HeaderModule {
}
