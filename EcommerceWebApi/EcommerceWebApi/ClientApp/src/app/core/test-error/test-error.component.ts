import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { environment } from 'src/environment/environment';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.css']
})
export class TestErrorComponent {
  baseUrl = environment.apiUrl;
  constructor(private http:HttpClient) {}

  get500Error()
  {
    this.http.get(this.baseUrl + 'buggy/servererror').subscribe(resp =>
    {
      console.log(resp);
    },
    error =>
    {
      console.log(error);
    });
  }

  get404Error()
  {
    this.http.get(this.baseUrl + 'buggy/notfound').subscribe(resp =>
    {
      console.log(resp);
    },
    error =>
    {
      console.log(error);
    });
  }

  get400Error()
  {
    this.http.get(this.baseUrl + 'buggy/badrequest').subscribe(resp =>
    {
      console.log(resp);
    },
    error =>
    {
      console.log(error);
    });
  }

  get400ValidationError()
  {
    this.http.get(this.baseUrl + 'buggy/badrequest/fortyfive').subscribe(resp =>
    {
      console.log(resp);
    },
    error =>
    {
      console.log(error);
    });
  }
}
