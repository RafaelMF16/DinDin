import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MonthlySummaryDetailsCardComponent } from './monthly-summary-details-card.component';

describe('MonthlySummaryDetailsCardComponent', () => {
  let component: MonthlySummaryDetailsCardComponent;
  let fixture: ComponentFixture<MonthlySummaryDetailsCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MonthlySummaryDetailsCardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MonthlySummaryDetailsCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
