import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { AppointmentType } from 'src/app/models/appointmentType';
import { CheckoutService } from '../checkout.service';
import { PatientAppointmentService } from 'src/app/patientAppointment/patient-appointment.service';

@Component({
  selector: 'app-checkout-appointment-type',
  templateUrl: './checkout-appointment-type.component.html',
  styleUrls: ['./checkout-appointment-type.component.css']
})
export class CheckoutAppointmentTypeComponent  implements OnInit {
  @Input() checkoutForm?: FormGroup;
  appointmentTypes: AppointmentType[] = [];

  constructor(private checkoutService: CheckoutService) {}

  ngOnInit(): void {
    this.checkoutService.getAppointmentTypes().subscribe({
      next: dm => this.appointmentTypes = dm
    })
  }

}
