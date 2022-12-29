import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseurl='http://localhost:5249/api/'
  loggedIn=false;

  constructor(private http:HttpClient) { }
  login(model:any){
    return this.http.post(this.baseurl+'User/login',model)
  }
}
