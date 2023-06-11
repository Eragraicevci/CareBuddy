import { Component, OnInit } from '@angular/core';
import { Appointment, PatientAppointment } from '../models/patientAppointment';
import { PatientAppointmentService } from './patient-appointment.service';

@Component({
  selector: 'app-patient-appointment',
  templateUrl: './patient-appointment.component.html',
  styleUrls: ['./patient-appointment.component.css']
})
export class PatientAppointmentComponent {

  constructor(public patientAppointmentService: PatientAppointmentService) {}
//  /* incrementQuantity(item: Appointment) {
//     this.patientAppointmentService.addItemToPatientAppointment(item);
//   }*/

//   removeItem(id: number) {
//     this.patientAppointmentService.removeAppointmentFromPatientAppointment(id);
//   }
}
