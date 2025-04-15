import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MonthlySummariesListComponent } from './monthly-summaries-list.component';

describe('MonthlySummariesListComponent', () => {
  let component: MonthlySummariesListComponent;
  let fixture: ComponentFixture<MonthlySummariesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MonthlySummariesListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MonthlySummariesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
