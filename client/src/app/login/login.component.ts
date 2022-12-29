import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageLevel } from '../Enums/message-level';
import { LoginUser } from '../Models/login-user';
import { AuthService } from '../Services/auth.service';
import { StateService } from '../Services/State/state.service';
import { UtilsService } from '../Services/Utils/utils.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  hide=true;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router : Router,
    private utilsService: UtilsService,
    private stateService: StateService
  ) { }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    })
  }

  async login(form: FormGroup) {
    if(form.valid) {
      this.stateService.showLoadingScreen();

      await this.authService.login(form.value as LoginUser).then((resp) => {
        if(resp) {
          
          this.stateService.hideLoadingScreen();
          this.router.navigate(['/product'])
         
        } 
        else {
          this.stateService.hideLoadingScreen();
          this.stateService.showAppMessage(MessageLevel.error, 'Wrong credentials. Please try again.');
          this.router.navigate(['/product'])
        }

      });
    }
  }

  get email(): any {
    return this.loginForm.get('email');
  }

  get password(): any {
    return this.loginForm.get('password');
  }

 

 

}
