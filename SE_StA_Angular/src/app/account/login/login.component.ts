import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

import { AccountInformationService } from '../../account-information.service';
import { AccountService } from '../account.service';
import { LoginRequest } from '../models/login-request.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {
  loginRequest: LoginRequest = { email: '', password: '', factorCode: '' };
  form!: FormGroup;
  private subs: Subscription[] = [];
  constructor(private accountService: AccountService,
    private accountInformationService: AccountInformationService,
    private formBuilder: FormBuilder,
    private router: Router) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      factorCode: ['']
    });
  }

  onSubmit(): void {
    this.form.markAllAsTouched();
    if (this.form.valid) {
      this.loginRequest = this.form.value as LoginRequest;
      this.login(this.loginRequest);
      this.form.reset();
    }
  }

  ngOnDestroy() {
    this.subs.forEach((sub) => {
      sub.unsubscribe();
    });
  }

  login(loginRequest: LoginRequest) {
    this.subs.push(this.accountService.login(loginRequest).subscribe(
      (response) => {
        this.accountInformationService.setLoggedIn(true);
        this.accountInformationService.setToken(response.token);
        this.accountInformationService.setUsername(response.username);
        this.accountInformationService.setEmail(response.email);

        this.router.navigate(["account", "dashboard"]);
        alert("You successfully logged in");
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
