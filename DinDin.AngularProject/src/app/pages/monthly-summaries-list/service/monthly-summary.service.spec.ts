import { TestBed } from '@angular/core/testing';

import { MonthlySummaryService } from './monthly-summary.service';

describe('MonthlySummaryService', () => {
  let service: MonthlySummaryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MonthlySummaryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
