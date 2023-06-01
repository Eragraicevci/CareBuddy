import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SectionHeaderComponent } from './section-header/section-header.component';
import { BreadcrumbModule } from 'xng-breadcrumb';



@NgModule({
  declarations: [
    SectionHeaderComponent
  ],
  imports: [
    CommonModule,
    BreadcrumbModule,
  ],
  exports:
    [
      SectionHeaderComponent
    ]
})
export class CoreModule { }
