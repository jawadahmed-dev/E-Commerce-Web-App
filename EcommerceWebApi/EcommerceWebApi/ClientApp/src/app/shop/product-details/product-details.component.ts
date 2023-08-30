import { Component } from '@angular/core';
import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent {
  product : IProduct;

  constructor(private shopService:ShopService, private activateRoute: ActivatedRoute)
  {
    this.loadProduct();

  }
  loadProduct() {
    this.shopService.getProduct(parseInt(this.activateRoute.snapshot.paramMap.get('id')))
      .subscribe(resp =>
        {
          this.product = resp;
        },
        error =>
        {
          console.log(error);
        }
      );
  }
}
