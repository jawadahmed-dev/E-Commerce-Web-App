import { HTTP_INTERCEPTORS, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Observable, catchError, throwError } from "rxjs";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor
{

  constructor(private router : Router) {

  }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req)
    .pipe(
      catchError(errorResp =>
      {
        if(errorResp)
        {
          if(errorResp.status === 404)
          {
            this.router.navigateByUrl('/not-found')
          }
          if(errorResp.status === 500)
          {
            this.router.navigateByUrl('/server-error')
          }

        }
        return throwError(errorResp);
      })
    )
  }

}
