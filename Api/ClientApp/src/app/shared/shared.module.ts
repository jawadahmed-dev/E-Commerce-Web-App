import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule,NgbCarouselModule } from '@ng-bootstrap/ng-bootstrap';
import { PagerComponent } from './components/pager/pager.component';
import { OrderTotalsComponent } from './components/order-totals/order-totals.component';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    PagerComponent,
    OrderTotalsComponent
  ],
  imports: [
    CommonModule,
    NgbModule,
    NgbCarouselModule,
    RouterModule,
    ReactiveFormsModule
  ],
  exports: [
    NgbModule,
    PagerComponent,
    NgbCarouselModule,
    OrderTotalsComponent,
    ReactiveFormsModule
  ]
})
export class SharedModule { }
