import { Component, OnInit } from '@angular/core';
import { AccountInformationService } from '../../account-information.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  public username = "";
  constructor(private accountInformationService: AccountInformationService) { }

  ngOnInit() {
    if (this.accountInformationService.isLoggedIn()) {
      this.username = this.accountInformationService.getUsername();
    }
  }
}
