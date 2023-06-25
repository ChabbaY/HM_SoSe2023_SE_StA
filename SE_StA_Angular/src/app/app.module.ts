import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';

import { FooterModule } from './footer/footer.module';
import { NavModule } from './nav/nav.module';
import { NotFoundComponent } from './not-found/not-found.component';

import { HomeComponent } from './home/home.component';
import { InfoModule } from './info/info.module';
import { HotelModule } from './hotel/hotel.module';
import { AccountModule } from './account/account.module';
import { CustomerModule } from './customer/customer.module';

import { AccountInformationService } from './account-information.service';
import { AuthInterceptor } from './auth-interceptor';

@NgModule({
  declarations: [
    AppComponent,
    NotFoundComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    CommonModule,
    FooterModule,
    NavModule,
    InfoModule,
    HotelModule,
    AccountModule,
    CustomerModule
  ],
  providers: [
    AccountInformationService,
    {
      provide : HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi   : true,
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
