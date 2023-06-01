import { NgModule } from '@angular/core';
import { ServiceDetailsComponent } from './service-details/service-details.component';
import { BookComponent } from './book.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {path: '', component: BookComponent},
  {path: ':id', component: ServiceDetailsComponent},
]


@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]

})
export class BookRoutingModule { }
