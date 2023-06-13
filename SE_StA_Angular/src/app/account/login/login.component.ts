import { Component, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

import { AccountInformationService } from '../../account-information.service';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnDestroy {
  private subs: Subscription[] = [];
  constructor(private accountService: AccountService, private accountInformationService: AccountInformationService) { }

  ngOnDestroy() {
    this.subs.forEach((sub) => {
      sub.unsubscribe();
    });
  }

  login(email: string, password: string, factorCode: string) {
    this.subs.push(this.accountService.login({ email: email, password: password, factorCode: factorCode }).subscribe(
      (response) => {
        this.accountInformationService.setLoggedIn(true);
        this.accountInformationService.setToken(response.token);
        this.accountInformationService.setUsername(response.username);
        this.accountInformationService.setEmail(response.email);
      },
      (error) => { console.log(error); }
    ));
  }
}
