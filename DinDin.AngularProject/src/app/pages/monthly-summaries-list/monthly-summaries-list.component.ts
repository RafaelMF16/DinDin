import { Component } from '@angular/core';

@Component({
  selector: 'app-monthly-summaries-list',
  standalone: false,
  templateUrl: './monthly-summaries-list.component.html',
  styleUrl: './monthly-summaries-list.component.css'
})
export class MonthlySummariesListComponent {
  monthlySummaries =
  [
    {
      titulo: "Janeiro",
      totalGasto: 2000,
      rendaTotal: 1500,
      saldo: -500
    },
    {
      titulo: "Fevereiro",
      totalGasto: 300,
      rendaTotal: 1500,
      saldo: 1200
    },
    {
      titulo: "Março",
      totalGasto: 300,
      rendaTotal: 1500,
      saldo: 120
    },
    {
      titulo: "Abril",
      totalGasto: 200,
      rendaTotal: 1500,
      saldo: 1300
    },
    {
      titulo: "Maio",
      totalGasto: 700,
      rendaTotal: 1500,
      saldo: 800
    },
    {
      titulo: "Junho",
      totalGasto: 300,
      rendaTotal: 1500,
      saldo: 1200
    },
    {
      titulo: "Julho",
      totalGasto: 500,
      rendaTotal: 1500,
      saldo: 1000
    },
    {
      titulo: "Agosto",
      totalGasto: 50,
      rendaTotal: 1500,
      saldo: 1450
    },
  ]
}
