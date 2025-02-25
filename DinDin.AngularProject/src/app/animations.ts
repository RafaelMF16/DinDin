import { animate, keyframes, state, style, transition, trigger } from "@angular/animations";

export const slideContentTrigger = trigger('backgroundSlide', [
    state('login', style({
        position: 'absolute',
        backgroundColor: '#58af9b',
        width: '40%',
        height: '100%',
        borderTopRightRadius: '15px',
        borderBottomRightRadius: '15px',
        left: '60%',
        zIndex: '1'
    })),
    state('register', style({
        position: 'absolute',
        backgroundColor: '#58af9b',
        width: '40%',
        height: '100%',
        borderTopRightRadius: '0',
        borderBottomRightRadius: '0',
        left: '0',
        zIndex: '1',
        borderTopLeftRadius: '15px',
        borderBottomLeftRadius: '15px'
    })),
    transition('login => register', [
        animate('0.9s', keyframes([
            style({
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
                width: '40%',
                left: '0',
                offset: 1
            })
        ]))
    ]),
    transition('register => login', [
        animate('0.9s', keyframes([
            style({
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
                width: '40%',
                left: '60%',
                offset: 1
            })
        ]))
    ]),
])