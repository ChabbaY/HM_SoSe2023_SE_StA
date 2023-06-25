import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { urlConstant } from 'src/constants/url-constant';

import { RegistrationRequest } from './models/registration-request.model';
import { LoginRequest } from './models/login-request.model';
import { LoginResponse } from './models/login-response.model';
import { ValidationRequest } from './models/validation-request.model';

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
}
