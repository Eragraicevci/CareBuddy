import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.css']
})
export class CheckoutComponent {

  constructor(private fb: FormBuilder) { }

  checkoutForm = this.fb.group({
    infoForm: this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      contactInfo: ['', Validators.required],
      address: ['', Validators.required],
      medicalHistory: ['', Validators.required],
      currentMedication: ['', Validators.required],
      symptoms: ['', Validators.required],
    }),
    appointmentTypeForm: this.fb.group({
      appointmentType: ['', Validators.required]
    }),
    paymentForm: this.fb.group({
      nameOnCard: ['', Validators.required]
    })
  })


}
