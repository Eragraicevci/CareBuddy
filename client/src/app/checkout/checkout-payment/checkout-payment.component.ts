import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { PatientAppointmentService } from 'src/app/patientAppointment/patient-appointment.service';
import { CheckoutService } from '../checkout.service';
import { ToastrService } from 'ngx-toastr';
import { NavigationExtras, Router } from '@angular/router';
import { PatientAppointment } from 'src/app/models/patientAppointment';
import { User } from 'src/app/models/user';

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.css']
})
export class CheckoutPaymentComponent {
  @Input() checkoutForm?: FormGroup;

  constructor(private patientAppointmentService: PatientAppointmentService, private checkoutService: CheckoutService,
    private toastr: ToastrService, private router: Router) { }


  submitBooking() {
    const patientAppointment = this.patientAppointmentService.getCurrentPatientAppointmentValue();
    if (!patientAppointment) return;
    const bookingToCreate = this.getBookingToCreate(patientAppointment);
    if (!bookingToCreate) return;
    this.checkoutService.createBooking(bookingToCreate).subscribe({
      next: booking => {
        this.toastr.success('Booking created successfully');
        // this.patientAppointmentService.deletePatientAppointment();
        const navigationExtras: NavigationExtras = {state: booking};
        this.router.navigate(['checkout/success'], navigationExtras);
      }
    })
  }
  getBookingToCreate(patientAppointment: PatientAppointment) {
    const appointmentTypeId = this.checkoutForm?.get('appointmentTypeForm')?.get('appointmentType')?.value;
    const info = this.checkoutForm?.get('infoForm')?.value as User;
    if (!appointmentTypeId || !info) return;
    return {
      patientAppointmentId: patientAppointment.id,
      appointmentTypeId: appointmentTypeId,
      info: info
    }

  }}
