import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {ButtonComponent} from "./button/button.component";
import {MatIconModule} from "@angular/material/icon";
import {MatTooltipModule} from "@angular/material/tooltip";

@NgModule({
  declarations: [ButtonComponent],
  imports: [
    CommonModule,
    MatIconModule,
    MatTooltipModule
  ],
  exports: [ButtonComponent]
})
export class SharedModule { }
