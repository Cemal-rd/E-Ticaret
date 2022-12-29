import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginUser } from '../Models/login-user';

const httpOptions = {
  headers: new HttpHeaders({'Content-Type': 'application/json'})
}

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private url: string = 'https://localhost:5249/api/User/login';
  private headers = new HttpHeaders({
    'content-type': "application/json"
  });

  constructor(private http: HttpClient) { }
  login(credentials: LoginUser): any {
    return new Promise((resolve, reject) => {
      const data: string = JSON.stringify(credentials);
      this.http.post<any>(this.url, data, httpOptions
      ).subscribe({
        next: (resp) => resolve(resp),
        error: (err) => reject(err),
        complete: () => console.log('login completed'),
      })
    });
  }

  // login(credentials: LoginUser): any {
  //   return new Promise((resolve, reject) => {
  //     const data: string = JSON.stringify(credentials);

  //     this.http.post(this.url, data
       
  //     ).subscribe({
  //       next: (resp) => resolve(resp),
  //       error: (err) => reject(err),
  //       complete: () => console.log('login completed'),
  //     })
  //   });
  }

 

