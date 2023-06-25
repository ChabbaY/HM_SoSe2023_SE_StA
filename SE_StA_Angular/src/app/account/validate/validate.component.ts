import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { catchError, Subscription, throwError } from 'rxjs';

import { AccountService } from '../account.service';
import { ValidationRequest } from '../models/validation-request.model';

@Component({
  selector: 'app-validate',
  templateUrl: './validate.component.html',
  styleUrls: ['./validate.component.scss']
})
export class ValidateComponent implements OnInit, OnDestroy {
  validationRequest: ValidationRequest = { email: '', token: '' };
  form!: FormGroup;
  private subs: Subscription[] = [];
  feedback = '';
  constructor(private accountService: AccountService,
    private formBuilder: FormBuilder,
    private router: Router) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      token: ['', Validators.required]
    });
  }

  onSubmit(): void {
    this.form.markAllAsTouched();
    if (this.form.valid) {
      this.validationRequest = this.form.value as ValidationRequest;
      this.validate(this.validationRequest);
      this.form.reset();
    }
  }

  ngOnDestroy() {
    this.subs.forEach((sub) => {
      sub.unsubscribe();
    });
  }

  validate(validationRequest: ValidationRequest) {
    this.subs.push(this.accountService.validate(validationRequest).pipe(
      catchError((error) => {
        let errorMsg = '';
        errorMsg = "Fehler " + error.status + " - " + error.statusText + " " + JSON.stringify(error.error);
        this.feedback = errorMsg;
        return throwError(() => new Error(errorMsg));
      })
    ).subscribe((response) => {
      this.feedback = JSON.stringify(response);
      setTimeout(() => {
        this.router.navigate(["account", "login"]);
      }, 1000);
    }));
  }

  //helper methods

  isFieldInvalid(field: string) {
    return (!this.form.get(field)?.valid && this.form.get(field)?.touched);
  }
}
