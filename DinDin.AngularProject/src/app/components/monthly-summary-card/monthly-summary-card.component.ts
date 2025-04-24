import { Component, Input } from '@angular/core';
import { hoverCardTrigger } from '../../animations';

@Component({
  selector: 'app-monthly-summary-card',
  standalone: false,
  templateUrl: './monthly-summary-card.component.html',
  styleUrl: './monthly-summary-card.component.css',
  animations: [
    hoverCardTrigger,
    
  ]
})
export class MonthlySummaryCardComponent {
  @Input() titulo!: string;
  @Input() rendaTotal!: number;
  @Input() totalGasto!: number;
  @Input() saldo!: number;

  hoverState: string = 'neutral'

  onClickInCard() {
    console.log("clicou")
  }
}
