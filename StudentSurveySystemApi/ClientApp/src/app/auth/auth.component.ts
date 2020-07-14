import { Component, OnInit } from '@angular/core';
import { UsersService, UsosAuthDto } from '../generated-api-client';
import { AuthService } from './auth.service';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent implements OnInit {


  setUsosAuth(usosAuth: UsosAuthDto){
    localStorage.setItem("usosAuth", JSON.stringify(usosAuth));
  }

  getUsosAuth(){
    return JSON.parse(localStorage.getItem("usosAuth")) as UsosAuthDto;
  }

  clearUsosAuth(){
    return localStorage.removeItem("usosAuth");
  }

  constructor(private authService: AuthService, private route: ActivatedRoute, private usersService: UsersService, private spinner: NgxSpinnerService) {
    this.route.queryParams.subscribe(params => {
      const oauthToken = params['oauth_token'];
      const oauthVerifier = params['oauth_verifier'];
      if(!!oauthVerifier){
        const usosAuth = this.getUsosAuth();
        usosAuth.oAuthVerifier = oauthVerifier;
        this.clearUsosAuth();
        this.usersService.usersUsosPinAuthPost(usosAuth).subscribe(data =>{
          this.authService.handleAuthentication(data);
        });
      }
    });
  }

  ngOnInit(): void {

  }

  onUsosAuthClick() {
    this.spinner.show();
    this.usersService.usersUsosAuthDataGet().subscribe(data => {
    this.setUsosAuth(data as UsosAuthDto);
      window.location.href = data.usosAuthUrl;
    })
  }

}
