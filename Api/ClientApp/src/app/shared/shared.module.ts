import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule,NgbCarouselModule } from '@ng-bootstrap/ng-bootstrap';
import { PagerComponent } from './components/pager/pager.component';
import { OrderTotalsComponent } from './components/order-totals/order-totals.component';
import { RouterModule } from '@angular/router';


@NgModule({
  declarations: [
    PagerComponent,
    OrderTotalsComponent
  ],
  imports: [
    CommonModule,
    NgbModule,
    NgbCarouselModule,
    RouterModule
  ],
  exports: [
    NgbModule,
    PagerComponent,
    NgbCarouselModule,
    OrderTotalsComponent
  ]
})
export class SharedModule { }
