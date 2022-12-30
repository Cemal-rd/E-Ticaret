import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageLevel } from 'src/app/Enums/message-level';
import { SignupUser } from 'src/app/Models/signup-user';
import { AuthService } from 'src/app/Services/auth.service';
import { RegisterService } from 'src/app/Services/register.service';
import { StateService } from 'src/app/Services/State/state.service';
import { UtilsService } from 'src/app/Services/Utils/utils.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  loginForm: FormGroup;
  hide=true;

  constructor(
    private fb: FormBuilder,
    private registerService: RegisterService,
    private router : Router,
    private utilsService: UtilsService,
    private stateService: StateService
  ) { }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
      confirmPassword:['',[Validators.required]]
    })
  }

  async register(form: FormGroup) {
    if(form.valid) {
      this.stateService.showLoadingScreen();

      await this.registerService.register(form.value as SignupUser).then((resp) => {
        if(resp) {
          
          this.stateService.hideLoadingScreen();
          this.router.navigate(['/login'])
         
        } 
        else {
          this.stateService.hideLoadingScreen();
          this.stateService.showAppMessage(MessageLevel.error, 'Wrong credentials. Please try again.');
          this.router.navigate(['/login'])
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
  get confirmPassword(): any {
    return this.loginForm.get('confirmPassword');
  }

}
