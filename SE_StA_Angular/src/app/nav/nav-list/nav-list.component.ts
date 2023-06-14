import { Component } from '@angular/core';

@Component({
  selector: 'app-nav-list',
  templateUrl: './nav-list.component.html',
  styleUrls: ['./nav-list.component.scss']
})
export class NavListComponent {
  home = { name: "Home", target: "home" };
  items = [
    { name: "Account", target: "account" },
    { name: "Hotels", target: "hotels" },
    { name: "Services", target: "services" },
    { name: "Customers", target: "customers" }
  ];
  subitems = undefined;

  hasSubitems = false;
}
