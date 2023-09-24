import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-checkout-address',
  templateUrl: './checkout-address.component.html',
  styleUrls: ['./checkout-address.component.css']
})
export class CheckoutAddressComponent {
  @Input() checkoutForm : FormGroup;
}
