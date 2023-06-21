import { TestBed } from '@angular/core/testing';

import { AccountInformationService } from './account-information.service';

describe('AccountInformationService', () => {
  let service: AccountInformationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AccountInformationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
