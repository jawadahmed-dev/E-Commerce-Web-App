import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.css']
})
export class PagerComponent {

  @Input() totalCount : number;
  @Input() pageIndex : number;
  @Input() pageSize : number;
  @Output() pageChange = new EventEmitter<number>();

  onPageChange(pageIndex : any)
  {

    this.pageChange.emit(pageIndex);
  }
}
