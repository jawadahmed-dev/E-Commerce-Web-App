import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable, map } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';
import { IUser } from 'src/app/shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private accountService:AccountService, private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> {
    return this.accountService.user$.pipe(
      map(auth =>
      {
        if(auth)
        {
          return true;
        }

        this.router.navigate(['account/login'], {queryParams : {returnUrl: state.url}});
        return false;
      })
    );
  }

}
