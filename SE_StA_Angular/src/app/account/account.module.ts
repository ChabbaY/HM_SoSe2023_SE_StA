import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';

import { AccountComponent } from './account.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ValidateComponent } from './validate/validate.component';

import { AccountService } from './account.service';

const routes: Routes = [
  {
    path: '', component: AccountComponent,
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'validate', component: ValidateComponent }
    ]
  }
];

@NgModule({
  declarations: [
    AccountComponent,
    DashboardComponent,
    LoginComponent,
    RegisterComponent,
    ValidateComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule
  ],
  providers: [
    AccountService
  ]
})
export class AccountModule { }
