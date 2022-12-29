import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MessageLevel } from './Enums/message-level';
import { AuthService } from './Services/auth.service';
import { StateService } from './Services/State/state.service';
import { UtilsService } from './Services/Utils/utils.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  
  title = 'client';
  showLogout: boolean = false;
  loadingScreen: any = {display: false, message: 'Please wait...'};
  appMessage: any = {display: false, level: MessageLevel.error, message: ""};
  constructor(
    private auth: AuthService,
    private stateService: StateService,
    private utils: UtilsService,
    private router: Router
  ){
    
    this.stateService.showLogout.subscribe(value => this.showLogout = value);
    this.stateService.loadingScreen.subscribe(value => this.loadingScreen = value);
    this.stateService.appMessage.subscribe(value => this.appMessage = value);

    if(this.utils.isLoggedIn){
      this.showLogout = this.utils.isLoggedIn;
    }
  }

  logout(): void {
    this.auth.logout();
  }

}
