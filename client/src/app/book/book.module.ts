import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookComponent } from './book.component';
import { ServiceItemComponent } from './service-item/service-item.component';
import { SharedModule } from '../_modules/shared.module';
import { ServiceDetailsComponent } from './service-details/service-details.component';
import { RouterModule } from '@angular/router';
import { BookRoutingModule } from './book-routing.module';



@NgModule({
  declarations: [
    BookComponent,
    ServiceItemComponent,
    ServiceDetailsComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    BookRoutingModule
  ]
})
export class BookModule { }
