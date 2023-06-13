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
    { name: "Hotels", target: "hotels" }
  ];
  subitems = undefined;

  hasSubitems = false;
}
