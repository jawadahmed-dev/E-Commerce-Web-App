import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';
import { IBasket } from './shared/models/basket';
import { IResponse } from './shared/models/response';
import { AccountService } from './account/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'ClientApp';

 constructor(private basketService: BasketService, private accountService:AccountService) {}

  ngOnInit(): void
  {
    this.loadBasket();
    this.loadUser();
  }

  loadBasket()
  {
    var basketId = localStorage.getItem('basket_id');

    return this.basketService.getBasket(basketId).subscribe(() =>
    {
      console.log('Basket Initialized');
    },
    error =>
    {
      console.log(error);
    });
  }

  loadUser()
  {
    let token = localStorage.getItem('token');
    if(token)
    {
      this.accountService.loadUser(token).subscribe(()=>
      {
        console.log('current user loaded.');
      },
      error =>
      {
        console.log(error);
      });
    }
  }
}
