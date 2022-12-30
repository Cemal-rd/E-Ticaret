import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SignupUser } from '../Models/signup-user';
const httpOptions = {
  headers: new HttpHeaders({'Content-Type': 'application/json'})
}


@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  private url: string = 'https://localhost:5249/api/User/register';
  private headers = new HttpHeaders({
    'content-type': "application/json"
  });

  constructor(private http: HttpClient) { }
  register(credentials: SignupUser): any {
    return new Promise((resolve, reject) => {
      const data: string = JSON.stringify(credentials);
      this.http.post<any>(this.url, data, httpOptions
      ).subscribe({
        next: (resp) => resolve(resp),
        error: (err) => reject(err),
        complete: () => console.log('register completed')
      })
    });
  }


  
}
