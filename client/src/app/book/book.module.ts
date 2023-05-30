import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookComponent } from './book.component';
import { ServiceItemComponent } from './service-item/service-item.component';



@NgModule({
  declarations: [
    BookComponent,
    ServiceItemComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    BookComponent
  ]
})
export class BookModule { }
