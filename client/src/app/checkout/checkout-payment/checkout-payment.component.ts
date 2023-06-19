import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { PatientAppointmentService } from 'src/app/patientAppointment/patient-appointment.service';
import { CheckoutService } from '../checkout.service';
import { ToastrService } from 'ngx-toastr';
import { NavigationExtras, Router } from '@angular/router';
import { PatientAppointment } from 'src/app/models/patientAppointment';
import { User } from 'src/app/models/user';
import { PatientInfo } from 'src/app/models/patientInfo';
import { Stripe, StripeCardCvcElement, StripeCardExpiryElement, StripeCardNumberElement, loadStripe } from '@stripe/stripe-js';

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.css']
})
export class CheckoutPaymentComponent implements OnInit {
  @Input() checkoutForm?: FormGroup;
  @ViewChild('cardNumber') cardNumberElement?: ElementRef;
  @ViewChild('cardExpiry') cardExpiryElement?: ElementRef;
  @ViewChild('cardCvc') cardCvcElement?: ElementRef;
  stripe: Stripe | null = null;
  cardNumber?: StripeCardNumberElement;
  cardExpiry?: StripeCardExpiryElement;
  cardCvc?: StripeCardCvcElement;
  cardErrors: any;
  loading = false;


  constructor(private patientAppointmentService: PatientAppointmentService, private checkoutService: CheckoutService,
    private toastr: ToastrService, private router: Router) { }

  ngOnInit(): void {
    loadStripe("pk_test_51NKVkXG3aBSvF1Y6eFKlp2HMCMZUZ9PR92yaLQVXFjQU3vb7lZD5vwAvNqU5zOBQibgcsG2AXSoe300gkR9rrnsP00LJsC0ifC").then(stripe => {
      this.stripe = stripe;
      const elements = stripe?.elements();
      if (elements) {
        this.cardNumber = elements.create('cardNumber');
        this.cardNumber.mount(this.cardNumberElement?.nativeElement);
        this.cardNumber.on('change', event => {
          // this.cardNumberComplete = event.complete;
          if (event.error) this.cardErrors = event.error.message;
          else this.cardErrors = null;
        })


        this.cardExpiry = elements.create('cardExpiry');
        this.cardExpiry.mount(this.cardExpiryElement?.nativeElement);
        this.cardExpiry.on('change', event => {
          // this.cardExpiryComplete = event.complete;
          if (event.error) this.cardErrors = event.error.message;
          else this.cardErrors = null;
        })

        this.cardCvc = elements.create('cardCvc');
        this.cardCvc.mount(this.cardCvcElement?.nativeElement);
        this.cardCvc.on('change', event => {
          // this.cardCvcComplete = event.complete;
          if (event.error) this.cardErrors = event.error.message;
          else this.cardErrors = null;
        })
      }
    })
  }


  async submitBooking() {
    this.loading = true;
    const patientAppointment = this.patientAppointmentService.getCurrentPatientAppointmentValue();
    if (!patientAppointment) return;
    const bookingToCreate = this.getBookingToCreate(patientAppointment);
    if (!bookingToCreate) return;
    this.checkoutService.createBooking(bookingToCreate).subscribe({
      next: booking => {
        this.stripe?.confirmCardPayment(patientAppointment.clientSecret!, {
          payment_method: {
            card: this.cardNumber!,
            billing_details: {
              name: this.checkoutForm?.get('paymentForm')?.get('nameOnCard')?.value
            }
          }
        }).then(result => {
          if (result.paymentIntent) {
            // this.patientAppointmentService.deletePatientAppointment();
            const navigationExtras: NavigationExtras = { state: booking };
            this.router.navigate(['checkout/success'], navigationExtras);
          } else {
            this.toastr.error(result.error.message);
          }
        })

      }
    })
  }
  getBookingToCreate(patientAppointment: PatientAppointment) {
    const appointmentTypeId = this.checkoutForm?.get('appointmentTypeForm')?.get('appointmentType')?.value;
    const info = this.checkoutForm?.get('infoForm')?.value as PatientInfo;
    if (!appointmentTypeId || !info) return;
    return {
      patientAppointmentId: patientAppointment.id,
      appointmentTypeId: appointmentTypeId,
      info: info
    }

  }
}
