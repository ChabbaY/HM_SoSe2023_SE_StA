import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { catchError, Subscription, throwError } from 'rxjs';

import { AccountInformationService } from '../../account-information.service';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-two-factor',
  templateUrl: './two-factor.component.html',
  styleUrls: ['./two-factor.component.scss']
})
export class TwoFactorComponent implements OnInit, OnDestroy {
  public username = "";
  private subs: Subscription[] = [];
  enabled = false;

  inSetup = false;
  key = '';
  qr_url = '';

  enableForm!: FormGroup;
  disableForm!: FormGroup;

  feedback = '';
  constructor(private accountService: AccountService,
    private accountInformationService: AccountInformationService,
    private formBuilder: FormBuilder,
    private router: Router) { }

  ngOnInit() {
    this.enableForm = this.formBuilder.group({
      factorCode: ['', Validators.required]
    });
    this.disableForm = this.formBuilder.group({
      factorCode: ['', Validators.required]
    });
    if (this.accountInformationService.isLoggedIn()) {
      this.username = this.accountInformationService.getUsername();
      this.isEnabled();
    }
  }

  onSubmitEnable(): void {
    this.enableForm.markAllAsTouched();
    if (this.enableForm.valid) {
      let factorCode = this.enableForm.value as FactorCode;
      this.enable(factorCode.factorCode);
      this.enableForm.reset();
    }
  }
  onSubmitDisable(): void {
    this.disableForm.markAllAsTouched();
    if (this.disableForm.valid) {
      let factorCode = this.disableForm.value as FactorCode;
      this.disable(factorCode.factorCode);
      this.disableForm.reset();
    }
  }

  ngOnDestroy() {
    this.subs.forEach((sub) => {
      sub.unsubscribe();
    });
  }

  isEnabled() {
    this.subs.push(this.accountService.get2faEnabled().pipe(
      catchError((error) => {
        let errorMsg = "Error " + error.status + " - " + error.statusText + " " + JSON.stringify(error.error);
        this.feedback = errorMsg;
        return throwError(() => new Error(errorMsg));
      })
    ).subscribe((response) => {
      this.enabled = response;
    }));
  }

  setup() {
    this.subs.push(this.accountService.get2faSetup().pipe(
      catchError((error) => {
        let errorMsg = "Error " + error.status + " - " + error.statusText + " " + JSON.stringify(error.error);
        this.feedback = errorMsg;
        return throwError(() => new Error(errorMsg));
      })
    ).subscribe((response) => {
      this.inSetup = true;
      this.key = response.key;
      this.qr_url = response.qr;
    }));
  }

  enable(activationCode: number) {
    this.subs.push(this.accountService.enable2fa(activationCode).pipe(
      catchError((error) => {
        let errorMsg = "Error " + error.status + " - " + error.statusText + " " + JSON.stringify(error.error);
        this.feedback = errorMsg;
        return throwError(() => new Error(errorMsg));
      })
    ).subscribe((response) => {
      this.feedback = "Successfully enabled 2FA";
      setTimeout(() => {
        this.router.navigate(["account", "twofactor"]);
      }, 1000);
    }));
  }

  disable(factorCode: number) {
    this.subs.push(this.accountService.disable2fa(factorCode).pipe(
      catchError((error) => {
        let errorMsg = "Error " + error.status + " - " + error.statusText + " " + JSON.stringify(error.error);
        this.feedback = errorMsg;
        return throwError(() => new Error(errorMsg));
      })
    ).subscribe((response) => {
      this.feedback = "Successfully disabled 2FA";
      setTimeout(() => {
        this.router.navigate(["account", "twofactor"]);
      }, 1000);
    }))
  }

  //helper methods

  isFieldInvalidEnable(field: string) {
    return (!this.enableForm.get(field)?.valid && this.enableForm.get(field)?.touched);
  }
  isFieldInvalidDisable(field: string) {
    return (!this.disableForm.get(field)?.valid && this.disableForm.get(field)?.touched);
  }
}

export interface FactorCode {
  factorCode: number;
}
