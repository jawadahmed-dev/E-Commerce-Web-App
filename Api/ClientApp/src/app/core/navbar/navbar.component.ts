import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasket } from 'src/app/shared/models/basket';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  public basket$ : Observable<IBasket>;
  constructor(private basketService:BasketService) {}

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;

    this.basket$.subscribe(data =>
      {
        console.log("data received : " + data);
      })
  }
}
