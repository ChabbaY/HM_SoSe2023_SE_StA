import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { RegistrationRequest } from './register/registration-request.model';
import { LoginRequest } from './login/login-request.model';
import { LoginResponse } from './login/login-response.model';

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
}
