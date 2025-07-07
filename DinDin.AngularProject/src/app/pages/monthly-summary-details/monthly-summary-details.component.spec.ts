import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MonthlySummaryDetailsComponent } from './monthly-summary-details.component';

describe('MonthlySummaryDetailsComponent', () => {
  let component: MonthlySummaryDetailsComponent;
  let fixture: ComponentFixture<MonthlySummaryDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
    imports: [MonthlySummaryDetailsComponent]
})
    .compileComponents();

    fixture = TestBed.createComponent(MonthlySummaryDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
