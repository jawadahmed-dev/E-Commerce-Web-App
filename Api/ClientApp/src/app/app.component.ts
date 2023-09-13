import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';
import { IBasket } from './shared/models/basket';
import { IResponse } from './shared/models/response';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'ClientApp';

 constructor(private basketService: BasketService) {}

  ngOnInit(): void
  {
    var basketId = localStorage.getItem('basket_id');

    this.basketService.getBasket(basketId).subscribe(() =>
    {
      console.log('Basket Initialized');
    },
    error =>
    {
      console.log(error);
    });
  }
}
