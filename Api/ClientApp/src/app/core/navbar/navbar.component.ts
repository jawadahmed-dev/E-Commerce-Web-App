import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountModule } from 'src/app/account/account.module';
import { AccountService } from 'src/app/account/account.service';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasket } from 'src/app/shared/models/basket';
import { IUser } from 'src/app/shared/models/user';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  public basket$ : Observable<IBasket>;
  public user$ : Observable<IUser>;
  constructor(private basketService:BasketService, private accountService:AccountService) {}

  ngOnInit(): void {

    this.user$ = this.accountService.user$;
    this.basket$ = this.basketService.basket$;

    this.basket$.subscribe(data =>
      {
        console.log("data received : " + data);
      })
  }
}
