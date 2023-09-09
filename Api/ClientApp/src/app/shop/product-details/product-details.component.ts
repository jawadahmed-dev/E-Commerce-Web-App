import { Component } from '@angular/core';
import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent {
  product : IProduct;

  constructor(private shopService:ShopService, private activateRoute: ActivatedRoute, private breadcrumbService: BreadcrumbService)
  {
    this.loadProduct();

  }
  loadProduct() {
    this.shopService.getProduct(parseInt(this.activateRoute.snapshot.paramMap.get('id')))
      .subscribe(resp =>
        {
          this.product = resp.result;
          this.breadcrumbService.set('@product-title', this.product.name);
        },
        error =>
        {
          console.log(error);
        }
      );
  }
}
