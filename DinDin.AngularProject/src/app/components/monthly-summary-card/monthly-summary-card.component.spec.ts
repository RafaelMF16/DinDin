import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MonthlySummaryCardComponent } from './monthly-summary-card.component';

describe('MonthlySummaryCardComponent', () => {
  let component: MonthlySummaryCardComponent;
  let fixture: ComponentFixture<MonthlySummaryCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
    imports: [MonthlySummaryCardComponent]
})
    .compileComponents();

    fixture = TestBed.createComponent(MonthlySummaryCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
