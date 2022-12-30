import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../auth.service';
import { UtilsService } from '../Utils/utils.service';

@Injectable({
  providedIn: 'root'
})
export class AuthguardGuard implements CanActivate {
  constructor(
    private router: Router, 
    private auth: AuthService,
    private utils: UtilsService
    ) {}

    canActivate(
      route: ActivatedRouteSnapshot,
      state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
        const isLoggedIn: boolean = this.utils.isLoggedIn;

        return this.checkLoginAndPermissions(route);
  }

  checkLoginAndPermissions(route: ActivatedRouteSnapshot): boolean {
    const isLoggedIn: boolean = this.utils.isLoggedIn;
    if(isLoggedIn) {
     

      return true;
    } 

    this.router.navigate(['/login']);
    return false;
  }
  
}
