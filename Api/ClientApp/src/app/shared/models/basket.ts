import { IBasketItem } from "./basket-item";
import { v4 as uuidv4 } from 'uuid';

export interface IBasket {
  id: string;
  basketItems: IBasketItem[];
}

export class Basket implements IBasket
{
  id = uuidv4();
  basketItems: IBasketItem[] = [];

}
