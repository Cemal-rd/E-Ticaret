import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { LoginUser } from '../Models/login-user';
import { LoginService } from './login.service';
import { StateService } from './State/state.service';
import { UtilsService } from './Utils/utils.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  user: any
  isLoggedIn = false

  constructor(private router: Router,
    private utils: UtilsService,
    private loginService: LoginService,
    private stateService: StateService) { }
    async login(user: LoginUser): Promise<boolean> {
      let result = false;
      await this.loginService.login(user).then((resp: any) => {
        this.stateService.showLogout.next(true);
        this.utils.isLoggedIn = true;
        result = true;
      }).catch((err: any) => {
         console.log(err);
      });
  
      return result;
    }
  
    logout() {
      this.utils.isLoggedIn = false;  
      this.stateService.resetState();
      this.router.navigate(['/login']);
    }

}
