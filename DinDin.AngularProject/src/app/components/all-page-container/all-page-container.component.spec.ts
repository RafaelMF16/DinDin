import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllPageContainerComponent } from './all-page-container.component';

describe('AllPageContainerComponent', () => {
  let component: AllPageContainerComponent;
  let fixture: ComponentFixture<AllPageContainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AllPageContainerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AllPageContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
