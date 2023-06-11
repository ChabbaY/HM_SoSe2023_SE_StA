import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { NotFoundComponent } from './not-found/not-found.component';

import { HotelComponent } from './hotel/hotel.component';
import { RoomComponent } from './hotel/room/room.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'info', loadChildren: () => import('./info/info.module').then(mod => mod.InfoModule) },
  { path: 'hotels', component: HotelComponent },
  { path: 'hotels/rooms', component: RoomComponent },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
