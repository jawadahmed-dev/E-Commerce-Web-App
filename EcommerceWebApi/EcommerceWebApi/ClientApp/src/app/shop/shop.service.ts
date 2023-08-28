import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http'
import { IProduct } from '../shared/models/product';
import { IPagination } from '../shared/models/pagination';
import { IProductType } from '../shared/models/product-type';
import { IProductBrand } from '../shared/models/product-brand';
import { ProductParams } from '../shared/models/product-params';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl = 'https://localhost:5001/api/';

  constructor(private http:HttpClient) { }

  getProducts(productParams : ProductParams)
  {
    let params = new HttpParams();

    if(productParams.brandId) params = params.append('brandId', productParams.brandId);
    if(productParams.typeId) params = params.append('typeId', productParams.typeId);
    if(productParams.sort) params = params.append('sort', productParams.sort);
    if(productParams.search) params = params.append('search', productParams.search);

    params = params
              .append('pageIndex', productParams.pageIndex)
              .append('pageSize', productParams.pageSize);

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
