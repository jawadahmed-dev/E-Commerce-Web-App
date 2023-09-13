import { Component } from '@angular/core';
import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent {
  product : IProduct;
  quantity = 1;

  constructor(private shopService:ShopService, private basketService:BasketService, private activateRoute: ActivatedRoute, private breadcrumbService: BreadcrumbService)
  {
    this.loadProduct();

  }
  addItemToBasket()
  {
    this.basketService.addItemToBasket(this.product, this.quantity);
  }

  incrementQuantity()
  {
    this.quantity++;
  }

  decrementQuantity()
  {
    if(this.quantity > 1 ) this.quantity--;
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
