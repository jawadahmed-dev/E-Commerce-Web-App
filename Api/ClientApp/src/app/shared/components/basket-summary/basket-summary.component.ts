import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IBasket } from '../../models/basket';
import { IBasketItem } from '../../models/basket-item';
import { Observable } from 'rxjs';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-basket-summary',
  templateUrl: './basket-summary.component.html',
  styleUrls: ['./basket-summary.component.css']
})
export class BasketSummaryComponent implements OnInit {
  basket$ : Observable<IBasket>;
  @Output() decrement = new EventEmitter<IBasketItem>();
  @Output() increment = new EventEmitter<IBasketItem>();
  @Output() remove = new EventEmitter<IBasketItem>();

  constructor(private basketService : BasketService) {}

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
  }

  incrementItemQuantity(basket:IBasketItem)
  {
    this.increment.emit(basket);
  }
  decrementItemQuantity(basket:IBasketItem)
  {
    this.decrement.emit(basket);
  }
  removeBasketItem(basket:IBasketItem)
  {
    this.remove.emit(basket);
  }
}
