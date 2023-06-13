import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';

import { AccountService } from '../account.service';
import { RegistrationRequest } from '../models/registration-request.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit, OnDestroy {
  registrationRequest: RegistrationRequest = { email: '', username: '', password: '' };
  form!: FormGroup;
  private subs: Subscription[] = [];
  constructor(private accountService: AccountService,
    private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onSubmit(): void {
    this.form.markAllAsTouched();
    if (this.form.valid) {
      console.log('form submitted');
      this.register(this.registrationRequest);
      this.form.reset();
    }
  }

  ngOnDestroy() {
    this.subs.forEach((sub) => {
      sub.unsubscribe();
    });
  }

  register(registrationRequest: RegistrationRequest) {
    this.subs.push(this.accountService.register(registrationRequest).subscribe(
      (response) => {
        console.log('success', response);
      },
      (error) => { console.log(error); }
    ));
  }

  //helper methods

  isFieldInvalid(field: string) {
    return (!this.form.get(field)?.valid && this.form.get(field)?.touched);
  }
}
