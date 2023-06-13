import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AccountInformationService {
  private token = "";
  private username = "";
  private email = "";
  private loggedIn = false;

  isLoggedIn(): boolean {
    return this.loggedIn;
  }
  setLoggedIn(loggedIn: boolean): void {
    this.loggedIn = loggedIn;
  }

  getToken(): string {
    return this.token;
  }
  setToken(token: string): void {
    this.token = token;
  }

  getUsername(): string {
    return this.username;
  }
  setUsername(username: string): void {
    this.username = username;
  }

  getEmail(): string {
    return this.email;
  }
  setEmail(email: string): void {
    this.email = email;
  }
}
