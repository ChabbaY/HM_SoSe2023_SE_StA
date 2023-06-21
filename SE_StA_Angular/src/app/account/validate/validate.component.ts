import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

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
    this.subs.push(this.accountService.validate(validationRequest).subscribe(
      () => {
        this.router.navigate(["account", "login"]);
        alert("Your E-Mail has been validated. You can now log in!");
      },
      (error: HttpErrorResponse) => {
        console.log(error);
        alert("Something went wrong: " + JSON.stringify(error.error));
      }
    ));
  }

  //helper methods

  isFieldInvalid(field: string) {
    return (!this.form.get(field)?.valid && this.form.get(field)?.touched);
  }
}
