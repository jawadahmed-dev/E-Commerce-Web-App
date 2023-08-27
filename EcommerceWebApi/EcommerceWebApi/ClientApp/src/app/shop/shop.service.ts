import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http'
import { IProduct } from '../shared/models/product';
import { IPagination } from '../shared/models/pagination';
import { IProductType } from '../shared/models/product-type';
import { IProductBrand } from '../shared/models/product-brand';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl = 'https://localhost:5001/api/';

  constructor(private http:HttpClient) { }

  getProducts(brandId? : number, typeId? : number, sortOption? : string)
  {
    let params = new HttpParams();

    if(brandId) params = params.append('brandId', brandId);
    if(typeId) params = params.append('brandId', brandId);
    if(sortOption) params = params.append('sort', sortOption);
    return this.http.get<IPagination<IProduct>>(this.baseUrl + 'products', {params : params});
  }
  getProductTypes()
  {
    return this.http.get<IProductType[]>(this.baseUrl + 'products/types');
  }
  getProductBrands()
  {
    return this.http.get<IProductBrand[]>(this.baseUrl + 'products/brands');
  }
}
