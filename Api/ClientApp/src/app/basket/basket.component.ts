import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { BasketService } from './basket.service';
import { IBasket } from '../shared/models/basket';
import { IBasketItem } from '../shared/models/basket-item';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.css']
})
export class BasketComponent {
  public basket$ : Observable<IBasket>;
  constructor(private basketService:BasketService) {}

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
  }

  incrementItemQuantity(basket:IBasketItem)
  {
    this.basketService.incerementItemQuantity(basket);
  }
  decrementItemQuantity(basket:IBasketItem)
  {
    this.basketService.decrementItemQuantity(basket);
  }
  removeBasketItem(basket:IBasketItem)
  {
    this.basketService.removeBasketItem(basket);
  }
}
