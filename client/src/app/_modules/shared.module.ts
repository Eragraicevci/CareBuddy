import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { NgxSpinnerModule } from 'ngx-spinner';
import { FileUploadModule } from 'ng2-file-upload';
import {BsDatepickerModule} from 'ngx-bootstrap/datepicker';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { TimeagoModule } from 'ngx-timeago';
import { ModalModule } from 'ngx-bootstrap/modal';
import { PagingHeaderComponent } from '../shared/paging-header/paging-header.component';
import { PagerComponent } from '../shared/pager/pager.component';
import { BookTotalsComponent } from '../shared/book-totals/book-totals.component';
import { StepperComponent } from '../shared/components/stepper/stepper.component';
import {CdkStepperModule} from '@angular/cdk/stepper';
import { TextInputComponent } from '../_forms/text-input/text-input.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PatientAppointmentSummaryComponent } from '../shared/patient-appointment-summary/patient-appointment-summary.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    PagingHeaderComponent,
    PagerComponent,
    BookTotalsComponent,
    StepperComponent,
    TextInputComponent,
        PatientAppointmentSummaryComponent
  ],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    NgxGalleryModule,
    NgxSpinnerModule.forRoot({
      type: 'line-scale-party'
    }),
    FileUploadModule,
    BsDatepickerModule.forRoot(),
    PaginationModule.forRoot(),
    ButtonsModule.forRoot(),
    TimeagoModule.forRoot(),
    ModalModule.forRoot(),
    CdkStepperModule,
    ReactiveFormsModule,
    RouterModule
    
  ],
  exports: [
    BsDropdownModule,
    ToastrModule,
    TabsModule,
    NgxGalleryModule,
    NgxSpinnerModule,
    FileUploadModule,
    BsDatepickerModule,
    PaginationModule,
    ButtonsModule,
    TimeagoModule,
    ModalModule,
    PagingHeaderComponent,
    PagerComponent,
    BookTotalsComponent,
    StepperComponent,
    CdkStepperModule,
    TextInputComponent,
    ReactiveFormsModule,
    PatientAppointmentSummaryComponent

    
  ]
})
export class SharedModule { }
