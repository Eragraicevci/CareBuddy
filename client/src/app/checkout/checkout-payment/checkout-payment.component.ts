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
import { firstValueFrom } from 'rxjs';
import { BookingToCreate } from 'src/app/models/booking';

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
  cardNumberComplete = false;
  cardExpiryComplete = false;
  cardCvcComplete = false;
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
          this.cardNumberComplete = event.complete;
          if (event.error) this.cardErrors = event.error.message;
          else this.cardErrors = null;
        })


        this.cardExpiry = elements.create('cardExpiry');
        this.cardExpiry.mount(this.cardExpiryElement?.nativeElement);
        this.cardExpiry.on('change', event => {
          this.cardExpiryComplete = event.complete;
          if (event.error) this.cardErrors = event.error.message;
          else this.cardErrors = null;
        })

        this.cardCvc = elements.create('cardCvc');
        this.cardCvc.mount(this.cardCvcElement?.nativeElement);
        this.cardCvc.on('change', event => {
          this.cardCvcComplete = event.complete;
          if (event.error) this.cardErrors = event.error.message;
          else this.cardErrors = null;
        })
      }
    })
  }

  get paymentFormComplete() {
    return this.checkoutForm?.get('paymentForm')?.valid
      && this.cardNumberComplete
      && this.cardExpiryComplete
      && this.cardCvcComplete
  }


  async submitBooking() {
    this.loading = true;
    const patientAppointment = this.patientAppointmentService.getCurrentPatientAppointmentValue();
    if (!patientAppointment) throw new Error('cannot get patient appointment');
    try {
      const createdBooking = await this.createBooking(patientAppointment);
      const paymentResult = await this.confirmPaymentWithStripe(patientAppointment);
      if (paymentResult.paymentIntent) {
        const navigationExtras: NavigationExtras = { state: createdBooking };
        this.router.navigate(['checkout/success'], navigationExtras);
      } else {
        this.toastr.error(paymentResult.error.message);
      }
    } catch (error: any) {
      console.log(error);
      this.toastr.error(error.message)
    } finally {
      this.loading = false;
    }

  }


  private async confirmPaymentWithStripe(patientAppointment: PatientAppointment | null) {
    if (!patientAppointment) throw new Error('Patient appointment is null');
    const result = this.stripe?.confirmCardPayment(patientAppointment.clientSecret!, {
      payment_method: {
        card: this.cardNumber!,
        billing_details: {
          name: this.checkoutForm?.get('paymentForm')?.get('nameOnCard')?.value
        }
      }
    });
    if (!result) throw new Error('Problem attempting payment with stripe');
    return result;
  }


  private async createBooking(patientAppointment: PatientAppointment | null) {
    if (!patientAppointment) throw new Error('Patient appointment is null');
    const bookingToCreate = this.getBookingToCreate(patientAppointment);

    return firstValueFrom(this.checkoutService.createBooking(bookingToCreate));
  }

  getBookingToCreate(patientAppointment: PatientAppointment): BookingToCreate {
    const appointmentTypeId = this.checkoutForm?.get('appointmentTypeForm')?.get('appointmentType')?.value;
    const info = this.checkoutForm?.get('infoForm')?.value as PatientInfo;
    if (!appointmentTypeId || !info) throw new Error("Problem with appointment");
    return {
      patientAppointmentId: patientAppointment.id,
      appointmentTypeId: appointmentTypeId,
      info: info
    }

  }
}
