import { Component, Input, OnInit } from '@angular/core';
import { Service } from 'src/app/models/service';
import { PatientAppointmentService } from 'src/app/patientAppointment/patient-appointment.service';

@Component({
  selector: 'app-service-item',
  templateUrl: './service-item.component.html',
  styleUrls: ['./service-item.component.css']
})
export class ServiceItemComponent {
[x: string]: any;
  @Input() service?: Service;

  constructor(private patientAppointmentService: PatientAppointmentService) { }

  addItemToPatientAppointment() {
    this.service && this.patientAppointmentService.addItemToPatientAppointment(this.service);
  }
}