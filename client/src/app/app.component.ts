import { Component, OnInit } from '@angular/core';
import { AccountService } from './_services/account.service';
import { User } from './models/user';
import { HttpClient } from '@angular/common/http';
import { Service } from './models/service';
import { Pagination } from './models/pagination';
import { ServicePagination } from './models/servicePagination';
import { PatientAppointment } from './models/patientAppointment';
import { PatientAppointmentService } from './patientAppointment/patient-appointment.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'CareBuddy';

  constructor(private accountService: AccountService, private patientAppointmentService: PatientAppointmentService) { }

  ngOnInit(): void {
    this.setCurrentUser();
    const patientAppointmentId = localStorage.getItem('patientAppointment_id');
    if (patientAppointmentId) this.patientAppointmentService.getPatientAppointment(patientAppointmentId);
  }



  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user: User = JSON.parse(userString);
    this.accountService.setCurrentUser(user);
  }
}
