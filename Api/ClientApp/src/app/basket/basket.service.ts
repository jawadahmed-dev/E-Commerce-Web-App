import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { environment } from 'src/environment/environment';
import { Basket, IBasket } from '../shared/models/basket';
import { IResponse } from '../shared/models/response';
import { IProduct } from '../shared/models/product';
import { IBasketItem } from '../shared/models/basket-item';
import { IBasketTotal } from '../shared/models/basket-total';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  baseUrl = environment.apiUrl;
  private basketSource = new BehaviorSubject<IBasket>(null);
  basket$ = this.basketSource.asObservable();
  private basketTotalSource = new BehaviorSubject<IBasketTotal>(null);
  basketTotal$ = this.basketTotalSource.asObservable();

  constructor(private http: HttpClient) { }

  getBasket(id:string)
  {
    return this.http.get(this.baseUrl + 'basket?id=' + id)
    .pipe(
      map((resp:IResponse<IBasket>) =>
      {
        this.basketSource.next(resp.result);
        this.calculateBasketTotal();
      })
    );
  }

  setBasket(basket:IBasket)
  {
    this.http.post(this.baseUrl + 'basket', basket)
    .subscribe((resp : IResponse<IBasket>) =>
    {
      this.basketSource.next(resp.result);
      this.calculateBasketTotal();
    }, error =>
    {
      console.log(error)
    });
  }

  getCurrentBasketValue()
  {
    return this.basketSource.value;
  }

  addItemToBasket(product:IProduct, quantity = 1)
  {
    const basketItem : IBasketItem = this.mapProductToBasetItem(product, quantity);
    const basket : IBasket = this.getCurrentBasketValue() ?? this.createBasket();
    basket.basketItems = this.addOrUpdateItem(basket.basketItems, basketItem, quantity);
    this.setBasket(basket);
  }
  addOrUpdateItem(basketItems: IBasketItem[], basketItem: IBasketItem, quantity: number): IBasketItem[] {
    var index = basketItems.findIndex(e => e.id === basketItem.id);
    if(index === -1)
    {
      basketItems.push(basketItem);
    }
    else
    {
      basketItems[index].quantity += quantity
    }
    return basketItems;
  }

  createBasket(): IBasket {
    var basket = new Basket();
    localStorage.setItem('basket_id', basket.id);
    return basket;
  }

  calculateBasketTotal()
  {
    let basket = this.getCurrentBasketValue();
    let shipping = 0;
    let subtotal = basket.basketItems.reduce((a,b) => (b.price * b.quantity) + a, 0);
    let total = subtotal + shipping;
    this.basketTotalSource.next({total, shipping, subtotal})
  }

  incerementItemQuantity(basketItem : IBasketItem)
  {
    let basket = this.getCurrentBasketValue();
    let index = basket.basketItems.findIndex(e => e.id === basketItem.id);

    if(index === -1) return;

    basket.basketItems[index].quantity ++;
    this.setBasket(basket);
  }

  decrementItemQuantity(basketItem: IBasketItem)
  {
    let basket = this.getCurrentBasketValue();
    let index = basket.basketItems.findIndex(e => e.id === basketItem.id);

    if(index === -1) return;

    if(basket.basketItems[index].quantity > 1)
    {
      basket.basketItems[index].quantity --;
      this.setBasket(basket);
    }
    else
    {
      this.removeBasketItem(basketItem);
    }
  }
  removeBasketItem(basketItem: IBasketItem) {
    let basket = this.getCurrentBasketValue();
    basket.basketItems = basket.basketItems.filter(e => e.id != basketItem.id);
    if(basket.basketItems.length === 0)
    {
      this.http.delete(this.baseUrl + 'basket?id=' + basketItem.id)
      .subscribe((resp :IResponse<Boolean>) =>
      {
        localStorage.removeItem('basket_id');
        this.basketSource.next(null);
        this.basketTotalSource.next(null);
      });
    }
    else
    {
      this.setBasket(basket);
    }
  }

  mapProductToBasetItem(product: IProduct, quantity: number) : IBasketItem  {
    return {
      id : product.id,
      name : product.name,
      brand : product.productBrandName,
      price : product.price,
      pictureUrl : product.pictureUrl,
      description : product.description,
      type : product.productTypeName,
      quantity
    };
  }
}
