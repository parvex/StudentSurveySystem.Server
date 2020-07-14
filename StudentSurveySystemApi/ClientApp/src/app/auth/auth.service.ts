import { Injectable } from '@angular/core';
import { CurrentUserDto } from '../generated-api-client';
import { JwtHelperService } from '@auth0/angular-jwt';
import { throwError, BehaviorSubject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';




@Injectable({providedIn: 'root'})
export class AuthService {
  user = new BehaviorSubject<CurrentUserDto>(null);
  private tokenExpirationTimer: any;

  constructor(private jwtHelper: JwtHelperService, private http: HttpClient, private router: Router, private spinner: NgxSpinnerService) {}

  public getUser(): CurrentUserDto{
    return JSON.parse(localStorage.getItem("user")) as CurrentUserDto;
  }

  public setUser(user: CurrentUserDto) {
    localStorage.setItem("user", JSON.stringify(user));
  }

  public isAuthenticated(): boolean{
    const user = this.getUser();
    return(user != null)
  }

  public getAuthToken(): string{
    return this.getUser()?.token;
  }

  autoLogin() {
    if(!this.isAuthenticated())
      return;
    const userData : CurrentUserDto = this.getUser();

    this.user.next(userData);
    const expirationDuration = new Date(userData.tokenExpirationDate).getTime() - new Date().getTime();
    this.autoLogout(expirationDuration);
  }

  logout() {
    localStorage.removeItem('user');
    this.user.next(null);
    if (this.tokenExpirationTimer) {
      clearTimeout(this.tokenExpirationTimer);
    }
    this.tokenExpirationTimer = null;
    this.router.navigate(['/Auth']);
  }

  autoLogout(expirationDuration: number) {
    this.tokenExpirationTimer = setTimeout(() => {
      this.logout();
    }, expirationDuration);
  }

  handleAuthentication(user: CurrentUserDto) {
    const expirationDuration = new Date(user.tokenExpirationDate).getTime() - new Date().getTime();
    this.setUser(user);
    this.user.next(user);
    this.autoLogout(expirationDuration);
    localStorage.setItem('user', JSON.stringify(user));
    this.router.navigate(['SurveysResults']);
    this.spinner.hide();
  }
}
