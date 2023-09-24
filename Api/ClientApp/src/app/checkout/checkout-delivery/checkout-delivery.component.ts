import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { IDeliveryMethod } from 'src/app/shared/models/delivery-method';
import { CheckoutService } from '../checkout.service';
import { IResponse } from 'src/app/shared/models/response';

@Component({
  selector: 'app-checkout-delivery',
  templateUrl: './checkout-delivery.component.html',
  styleUrls: ['./checkout-delivery.component.css']
})
export class CheckoutDeliveryComponent implements OnInit{

  @Input() checkoutForm:FormGroup;
  deliveryMethods : IDeliveryMethod[];

  constructor(private checkoutService:CheckoutService) {}

  ngOnInit(): void {
    this.loadDeliveryMethods();
  }

  loadDeliveryMethods()
  {
    this.checkoutService.getDeliveryMethods().subscribe((resp: IResponse<IDeliveryMethod[]>)=>
    {
      if(resp.result)
      {
        this.deliveryMethods = resp.result;
      }
    },
    error =>
    {

    });
  }
}
