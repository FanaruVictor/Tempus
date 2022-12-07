import {Component} from '@angular/core';
import {SlideInOutAnimation} from "../../../commons/Animations";

@Component({
  selector: 'app-edit-slider',
  templateUrl: './edit-slider.component.html',
  styleUrls: ['./edit-slider.component.scss'],
  animations: [SlideInOutAnimation]
})
export class EditSliderComponent {
  visible = false;
  animationState = 'in';

  toggleEditPanel() {
    this.animationState = this.animationState === 'out' ? 'in' : 'out';
    this.visible = !this.visible;
  }
}



