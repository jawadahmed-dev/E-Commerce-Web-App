import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environment/environment';
import { IDeliveryMethod } from '../shared/models/delivery-method';
import { IResponse } from '../shared/models/response';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  baseUrl = environment.apiUrl;
  constructor(private http:HttpClient) { }

  getDeliveryMethods()
  {
    return this.http.get(this.baseUrl + 'orders/delivery-methods').pipe(
      map((resp: IResponse<IDeliveryMethod[]>) =>
      {
         resp.result = resp.result.sort((a,b) => b.price-a.price);
         return resp;
      })
    )
  }
}
