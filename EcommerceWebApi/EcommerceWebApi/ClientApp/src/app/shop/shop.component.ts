import { Component, OnInit } from '@angular/core';
import { IProduct } from '../shared/models/product';
import { ShopService } from './shop.service';
import { IProductType } from '../shared/models/product-type';
import { IProductBrand } from '../shared/models/product-brand';
import { ProductParams } from '../shared/models/product-params';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})
export class ShopComponent implements OnInit {

  products : IProduct[] = [];
  productTypes : IProductType[] = [];
  productBrands : IProductBrand[] = [];
  productParams = new ProductParams();
  totalCount : number;
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
    this.shopService.getProducts(this.productParams).subscribe(resp =>
      {
        this.productParams.pageIndex = resp.pageIndex;
        this.productParams.pageSize = resp.pageSize;
        this.totalCount = resp.count;
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
    this.productParams.pageIndex =1;
    this.productParams.brandId = brandId;
    this.getProducts();
  }
  onTypeSelected(typeId : number)
  {
    this.productParams.pageIndex =1;
    this.productParams.typeId = typeId;
    this.getProducts();
  }
  onSortOptionSelected(sortOption : string)
  {
    this.productParams.pageIndex =1;
    this.productParams.sort = sortOption;
    this.getProducts();
  }
  onPageChange(pageIndex : any)
  {
    if(this.productParams.pageIndex == pageIndex) return;
    this.productParams.pageIndex = pageIndex;
    this.getProducts();
  }
  onSearch()
  {
    this.productParams.pageIndex =1;
    this.getProducts();
  }
  onReset()
  {
    this.productParams = new ProductParams();
    this.getProducts();
  }
}
