import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
    selector: 'app-forms-container',
    templateUrl: './forms-container.component.html',
    styleUrl: './forms-container.component.css',
    standalone: true,
    imports: [
        ReactiveFormsModule
    ]
})
export class FormsContainerComponent {

}
