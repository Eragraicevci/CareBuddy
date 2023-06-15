import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-checkout-info',
  templateUrl: './checkout-info.component.html',
  styleUrls: ['./checkout-info.component.css']
})
export class CheckoutInfoComponent {
  @Input() checkoutForm?: FormGroup;

}
