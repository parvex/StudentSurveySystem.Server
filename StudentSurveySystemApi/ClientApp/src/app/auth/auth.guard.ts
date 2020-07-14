import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router,
  UrlTree
} from '@angular/router';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map, tap, take } from 'rxjs/operators';

import { AuthService } from './auth.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}


  canActivate(route: ActivatedRouteSnapshot): boolean {
    // on the data property
    const expectedRole = route.data.expectedRole;

    if (!this.authService.isAuthenticated() || (!!expectedRole && this.authService.getUser().userRole != expectedRole)) {
      this.router.navigate(['Auth']);
      return false;
    }
    return true;
  }
}
