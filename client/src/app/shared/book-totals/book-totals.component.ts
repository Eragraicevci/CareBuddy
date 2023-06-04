import { Component, OnInit } from '@angular/core';
import { PatientAppointmentService } from 'src/app/patientAppointment/patient-appointment.service';

@Component({
  selector: 'app-book-totals',
  templateUrl: './book-totals.component.html',
  styleUrls: ['./book-totals.component.css']
})
export class BookTotalsComponent {

  
    constructor(public patientAppointmentService: PatientAppointmentService){}
  
  }


