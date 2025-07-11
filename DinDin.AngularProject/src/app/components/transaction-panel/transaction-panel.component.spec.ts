import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransactionPanelComponent } from './transaction-panel.component';

describe('TransactionPanelComponent', () => {
  let component: TransactionPanelComponent;
  let fixture: ComponentFixture<TransactionPanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
    imports: [TransactionPanelComponent]
})
    .compileComponents();

    fixture = TestBed.createComponent(TransactionPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
