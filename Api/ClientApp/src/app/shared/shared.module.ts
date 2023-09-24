import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule,NgbCarouselModule } from '@ng-bootstrap/ng-bootstrap';
import { PagerComponent } from './components/pager/pager.component';
import { OrderTotalsComponent } from './components/order-totals/order-totals.component';
import { Router, RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import {CdkStepperModule} from '@angular/cdk/stepper';
import { StepperComponent } from './components/stepper/stepper.component';
import { BasketSummaryComponent } from './components/basket-summary/basket-summary.component'

@NgModule({
  declarations: [
    PagerComponent,
    OrderTotalsComponent,
    StepperComponent,
    BasketSummaryComponent
  ],
  imports: [
    RouterModule,
    CommonModule,
    NgbModule,
    NgbCarouselModule,
    RouterModule,
    ReactiveFormsModule,
    CdkStepperModule
  ],
  exports: [
    NgbModule,
    RouterModule,
    PagerComponent,
    NgbCarouselModule,
    OrderTotalsComponent,
    ReactiveFormsModule,
    CdkStepperModule,
    StepperComponent,
    BasketSummaryComponent
  ]
})
export class SharedModule { }
