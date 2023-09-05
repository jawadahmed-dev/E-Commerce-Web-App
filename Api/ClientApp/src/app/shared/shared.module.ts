import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {PaginationModule} from 'ngx-bootstrap/pagination'
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { PagerComponent } from './components/pager/pager.component';


@NgModule({
  declarations: [
    PagerComponent
  ],
  imports: [
    CommonModule,
    NgbModule
  ],
  exports: [
    NgbModule,
    PagerComponent
  ]
})
export class SharedModule { }
