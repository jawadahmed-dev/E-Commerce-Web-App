import { Component, OnInit } from '@angular/core';
import { IProduct } from '../shared/models/product';
import { ShopService } from './shop.service';
import { IProductType } from '../shared/models/product-type';
import { IProductBrand } from '../shared/models/product-brand';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})
export class ShopComponent implements OnInit {

  products : IProduct[] = [];
  productTypes : IProductType[] = [];
  productBrands : IProductBrand[] = [];
  productBrandId = 0;
  productTypeId = 0;
  sortSelected = '';
  sortOptions =
  [
    { name:"Alphabetical", value : 'name'},
    { name:"Price : Low to High", value : 'priceAsc'},
    { name:"Price : High to Low", value : 'priceDesc'}
  ];

  constructor(private shopService: ShopService) {}

  ngOnInit()
  {
    this.getProducts();
    this.getProductTypes();
    this.getProductBrands();
  }

  getProducts()
  {
    this.shopService.getProducts(this.productBrandId, this.productTypeId, this.sortSelected).subscribe(resp =>
      {
        this.products = resp.data;
      },
      error =>
      {
        console.log(error);
      });
  }

  getProductTypes()
  {
    this.shopService.getProductTypes().subscribe(resp =>
      {
        this.productTypes = [{id:0, name:'All'}, ...resp];
      },
      error =>
      {
        console.log(error);
      });
  }

  getProductBrands()
  {
    this.shopService.getProductBrands().subscribe(resp =>
      {
        this.productBrands = [{id:0, name:'All'}, ...resp];
      },
      error =>
      {
        console.log(error);
      });
  }

  onBrandSelected(brandId : number)
  {
    this.productBrandId = brandId;
    this.getProducts();
  }
  onTypeSelected(typeId : number)
  {
    this.productTypeId = typeId;
    this.getProducts();
  }
  onSortOptionSelected(sortOption : string)
  {
    this.sortSelected = sortOption;
    this.getProducts();
  }
}
