import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, ReplaySubject, map, of } from 'rxjs';
import { environment } from 'src/environment/environment';
import { IResponse } from '../shared/models/response';
import { IUser } from '../shared/models/user';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl;
  private userSource = new ReplaySubject<IUser>(1);
  user$ = this.userSource.asObservable();
  constructor(private http:HttpClient, private router: Router) { }

  login(values : any)
  {
    return this.http.post(this.baseUrl + 'account/login', values).pipe(
      map((resp : IResponse<IUser>)=>
      {
        if(resp.statusCode === 200)
        {
          localStorage.setItem('token', resp.result.token);
          this.userSource.next(resp.result);
        }
      })
    );
  }

  register(values : any)
  {
    return this.http.post(this.baseUrl + 'account/register', values).pipe(
      map((resp : IResponse<IUser>)=>
      {
        if(resp.statusCode === 200)
        {
          localStorage.setItem('token', resp.result.token);
          this.userSource.next(resp.result);
        }
      })
    );
  }

  loadUser(token : string)
  {
    if(token === null)
    {
      this.userSource.next(null);
      return of(null);
    }
    let httpHeaders = new HttpHeaders();
    httpHeaders = httpHeaders.set('Authorization', `Bearer ${token}`);
    return this.http.get(this.baseUrl + 'account/load-user', {headers: httpHeaders}).pipe(
      map((resp : IResponse<IUser>) =>
      {
        if(resp.statusCode === 200)
        {
          this.userSource.next(resp.result);
        }
      })
    );
  }

  checkEmailExists(email : string)
  {
    return this.http.get(this.baseUrl + 'account/check-email-exists?email=' + email);
  }

  logout()
  {
    localStorage.removeItem('token');
    this.userSource.next(null);
    this.router.navigateByUrl('/');
  }
}
