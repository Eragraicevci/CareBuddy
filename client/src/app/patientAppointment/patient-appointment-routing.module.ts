import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { PatientAppointmentComponent } from './patient-appointment.component';


const routes: Routes = [
  {path: '', component: PatientAppointmentComponent}
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class PatientAppointmentRoutingModule { }
