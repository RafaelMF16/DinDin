import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MonthlySummariesContainerComponent } from './monthly-summaries-container.component';

describe('MonthlySummariesContainerComponent', () => {
  let component: MonthlySummariesContainerComponent;
  let fixture: ComponentFixture<MonthlySummariesContainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MonthlySummariesContainerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MonthlySummariesContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
