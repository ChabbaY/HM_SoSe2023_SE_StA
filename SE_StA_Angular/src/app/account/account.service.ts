import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

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
    return this.http.post('https://localhost:50001/auth/register', request);
  }

  login(request: LoginRequest) {
    return this.http.post<LoginResponse>('https://localhost:50001/auth/login', request);
  }

  validate(request: ValidationRequest) {
    return this.http.get(`https://localhost:50001/auth/validate?token=${request.token}&email=${request.email}`);
  }
}
