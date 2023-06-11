import { Component, OnInit } from '@angular/core';
import { BookService } from '../book.service';
import { Service } from 'src/app/models/service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { PatientAppointmentService } from 'src/app/patientAppointment/patient-appointment.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-service-details',
  templateUrl: './service-details.component.html',
  styleUrls: ['./service-details.component.css']
})
export class ServiceDetailsComponent implements OnInit {
  service?: Service;
  capacity = 1;
  capacityInPatientAppointment = 0;

  constructor(private bookService: BookService, private activatedRoute: ActivatedRoute, 
    private bcService: BreadcrumbService, private patientAppointmentService: PatientAppointmentService) {
    this.bcService.set('@serviceDetails', ' ')
  }


  addItemToPatientAppointment() {
    this.service && this.patientAppointmentService.addItemToPatientAppointment(this.service);
  }
  ngOnInit(): void {
    this.loadService();
  }

  loadService() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (id) this.bookService.getService(+id).subscribe({
      next: service => this.service = service,
      error: error => console.log(error)
    })
  }
}
