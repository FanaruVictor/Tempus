import {Component, OnInit} from '@angular/core';
import {SlideUpDownAnimation} from '../../commons/Animations';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  animations: [SlideUpDownAnimation]
})
export class HeaderComponent implements OnInit {
  animationState = 'up';

  constructor() {
  }

  ngOnInit(): void {
  }


  toggleMenu() {
    this.animationState = this.animationState === 'up' ? 'down' : 'up';
  }
}
