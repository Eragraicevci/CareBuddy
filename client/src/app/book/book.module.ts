import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookComponent } from './book.component';
import { ServiceItemComponent } from './service-item/service-item.component';
import { SharedModule } from '../_modules/shared.module';
import { ServiceDetailsComponent } from './service-details/service-details.component';



@NgModule({
  declarations: [
    BookComponent,
    ServiceItemComponent,
    ServiceDetailsComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports: [
    BookComponent
  ]
})
export class BookModule { }
