import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { urlConstant } from 'src/constants/url-constant';

import { RegistrationRequest } from './models/registration-request.model';
import { LoginRequest } from './models/login-request.model';
import { LoginResponse } from './models/login-response.model';
import { ValidationRequest } from './models/validation-request.model';
import { TwoFactorResponse } from './models/two-factor-response.model';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  constructor(private http: HttpClient) { }

  register(request: RegistrationRequest) {
    return this.http.post(`${urlConstant.apiPath}/auth/register`, request);
  }

  login(request: LoginRequest) {
    return this.http.post<LoginResponse>(`${urlConstant.apiPath}/auth/login`, request);
  }

  validate(request: ValidationRequest) {
    return this.http.get(`${urlConstant.apiPath}/auth/validate?token=${encodeURIComponent(request.token)}&email=${encodeURIComponent(request.email)}`);
  }

  get2faSetup() {
    return this.http.get<TwoFactorResponse>(`${urlConstant.apiPath}/auth/2fa/enable`);
  }
  enable2fa(activationCode: number) {
    return this.http.post(`${urlConstant.apiPath}/auth/2fa/enable?activationCode=${activationCode}`, null);
  }
  disable2fa(code: number) {
    return this.http.post(`${urlConstant.apiPath}/auth/2fa/disable?code=${code}`, null);
  }
  get2faEnabled() {
    return this.http.get<boolean>(`${urlConstant.apiPath}/auth/2fa/enabled`);
  }
}
