import { animate, keyframes, state, style, transition, trigger } from "@angular/animations";

export const hoverCardTrigger = trigger('hoverCard', [
    state('neutral', style({
        boxShadow: 'none'
    })),
    state('hover', style({
        transform: 'scale(1.03)',
        boxShadow: '0 8px 16px rgba(0, 0, 0, 0.2)',
        borderColor: 'black'
    })),
    transition('neutral <=> hover', [
        animate('0.3s ease-in-out')
    ])
]);

export const slideContentTrigger = trigger('backgroundSlide', [
    state('login', style({
        left: '60%',
    })),
    state('register', style({
        left: '0',
    })),
    transition('login => register', [
        animate('0.9s', keyframes([
            style({
                zIndex: 2,
                width: '40%',
                left: '60%',
                offset: 0
            }),
            style({
                width: '45%',
                left: '54%',
                offset: 0.1
            }),
            style({
                width: '50%',
                left: '48%',
                offset: 0.2
            }),
            style({
                width: '55%',
                left: '42%',
                offset: 0.3
            }),
            style({
                width: '60%',
                left: '36%',
                offset: 0.4
            }),
            style({
                width: '65%',
                left: '30%',
                offset: 0.5
            }),
            style({
                width: '60%',
                left: '24%',
                offset: 0.6
            }),
            style({
                width: '55%',
                left: '18%',
                offset: 0.7
            }),
            style({
                width: '50%',
                left: '12%',
                offset: 0.8
            }),
            style({
                width: '45%',
                left: '6%',
                offset: 0.9
            }),
            style({
                zIndex: 1,
                width: '40%',
                left: '0',
                offset: 1
            })
        ]))
    ]),
    transition('register => login', [
        animate('0.9s', keyframes([
            style({
                zIndex: 2,
                width: '40%',
                left: '0',
                offset: 0
            }),
            style({
                width: '45%',
                left: '6%',
                offset: 0.1
            }),
            style({
                width: '50%',
                left: '12%',
                offset: 0.2
            }),
            style({
                width: '55%',
                left: '18%',
                offset: 0.3
            }),
            style({
                width: '60%',
                left: '24%',
                offset: 0.4
            }),
            style({
                width: '65%',
                left: '30%',
                offset: 0.5
            }),
            style({
                width: '60%',
                left: '36%',
                offset: 0.6
            }),
            style({
                width: '55%',
                left: '42%',
                offset: 0.7
            }),
            style({
                width: '50%',
                left: '48%',
                offset: 0.8
            }),
            style({
                width: '45%',
                left: '54%',
                offset: 0.9
            }),
            style({
                zIndex: 1,
                width: '40%',
                left: '60%',
                offset: 1
            })
        ]))
    ]),
])