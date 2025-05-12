import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MonthlySummaryDetailsToolbarComponent } from './monthly-summary-details-toolbar.component';

describe('MonthlySummaryDetailsToolbarComponent', () => {
  let component: MonthlySummaryDetailsToolbarComponent;
  let fixture: ComponentFixture<MonthlySummaryDetailsToolbarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MonthlySummaryDetailsToolbarComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MonthlySummaryDetailsToolbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
