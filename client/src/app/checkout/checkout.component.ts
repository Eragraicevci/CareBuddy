import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { PatientAppointmentService } from '../patientAppointment/patient-appointment.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.css']
})
export class CheckoutComponent implements OnInit {

  ngOnInit(): void {
   /* this.getPatientInfoFormValues();*/
   this.getAppointmentTypeValues();
  }


  constructor(private fb: FormBuilder, private accountService:AccountService, private appointmentService:PatientAppointmentService) { }

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

  /*getPatientInfoFormValues() {
    this.accountService.getUserAddress().subscribe({
      next: address => {
        address && this.checkoutForm.get('addressForm')?.patchValue(address)
@@ -40,4 +43,12 @@
    })
  }*/

  getAppointmentTypeValues() {
    const appointment = this.appointmentService.getCurrentPatientAppointmentValue();
    if (appointment && appointment.appointmentTypeId) {
      this.checkoutForm.get('appointmentTypeForm')?.get('appointmentType')
        ?.patchValue(appointment.appointmentTypeId.toString());
    }
  }


}
