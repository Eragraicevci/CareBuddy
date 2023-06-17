import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Appointment } from 'src/app/models/patientAppointment';
import { PatientAppointmentService } from 'src/app/patientAppointment/patient-appointment.service';

@Component({
  selector: 'app-patient-appointment-summary',
  templateUrl: './patient-appointment-summary.component.html',
  styleUrls: ['./patient-appointment-summary.component.css']
})
export class PatientAppointmentSummaryComponent{
  @Output() addItem = new EventEmitter<Appointment>();
  @Output() removeItem = new EventEmitter<{id: number, capacity: number}>();
  @Input() isPatientAppointment = true;


  constructor(public patientAppointmentService: PatientAppointmentService) {}

  addAppointment(item: Appointment) {
    this.addItem.emit(item)
  }

  removeAppointment(id: number, capacity = 1) {
    this.removeItem.emit({id, capacity})
  }

}
