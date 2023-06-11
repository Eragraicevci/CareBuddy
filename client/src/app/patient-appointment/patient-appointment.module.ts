import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PatientAppointmentComponent } from '../patientAppointment/patient-appointment.component';
import { PatientAppointmentRoutingModule } from '../patientAppointment/patient-appointment-routing.module';
import { SharedModule } from '../_modules/shared.module';
import { RouterModule, Routes } from '@angular/router';



@NgModule({
  declarations: [
    PatientAppointmentComponent
  ],
  imports: [
    CommonModule,
    PatientAppointmentRoutingModule,
    SharedModule

  ]
})
export class PatientAppointmentModule { }
