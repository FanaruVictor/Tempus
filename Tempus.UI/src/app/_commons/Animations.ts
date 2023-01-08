import {animate, group, state, style, transition, trigger} from "@angular/animations";

export const SlideInOutAnimation = [
  trigger('slideInOut', [
    transition(':enter', [
      style({
        transform: 'translateX(-100%)'
      }),
      animate('200ms ease-in', style({transform: 'translateX(0%)'}))
    ]),
    transition(':leave', [
      animate('200ms ease-in', style({transform: 'translateX(-100%)'}))
    ])
  ])
];

export const SlideUpDownAnimation = [
  trigger('slideUpDown', [
    state('down', style({
      transform: 'translateX(0)'
    })),
    state('up', style({
      transform: 'translateX(-100vw)'
    })),
    transition('down => up', [group([
        animate('200ms ease-in-out', style({
          transform: 'translateX(-100vw)'
        })),
        animate('400ms ease-in-out', style({})),
        animate('400ms ease-in-out', style({}))
      ]
    )]),
    transition('up => down', [group([
        animate('200ms ease-in-out', style({
          transform: 'translateX(0)'
        })),
        animate('700ms ease-in-out', style({})),
        animate('800ms ease-in-out', style({}))
      ]
    )])
  ]),
]

