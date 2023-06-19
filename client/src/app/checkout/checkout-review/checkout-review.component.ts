import { CdkStepper } from '@angular/cdk/stepper';
import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { PatientAppointmentService } from 'src/app/patientAppointment/patient-appointment.service';

@Component({
  selector: 'app-checkout-review',
  templateUrl: './checkout-review.component.html',
  styleUrls: ['./checkout-review.component.css']
})
export class CheckoutReviewComponent {
  

  constructor(private patientAppointmentService: PatientAppointmentService, private toastr: ToastrService) { }

  createPaymentIntent() {
    this.patientAppointmentService.createPaymentIntent().subscribe({
      next: () => this.toastr.success('Payment intent created'),
      error: error => this.toastr.error(error.message)
    })
  }


}
