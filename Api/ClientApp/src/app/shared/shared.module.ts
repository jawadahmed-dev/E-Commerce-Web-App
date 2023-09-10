import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule,NgbCarouselModule } from '@ng-bootstrap/ng-bootstrap';
import { PagerComponent } from './components/pager/pager.component';


@NgModule({
  declarations: [
    PagerComponent
  ],
  imports: [
    CommonModule,
    NgbModule,
    NgbCarouselModule
  ],
  exports: [
    NgbModule,
    PagerComponent,
    NgbCarouselModule
  ]
})
export class SharedModule { }
