import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private url: string = 'http://localhost:5249/api/Categories';

  constructor(
    private http: HttpClient
  ) { }


  getAllCategories(): any {
    return this.http.get<any>(this.url)
  }

  getCategory(id: string) {
    const data: string = JSON.stringify(id);
    return this.http.get<any>(this.url + '/' + id,
      {
        params: new HttpParams()
          .set('data', data)
      }
    )
  }

  createCategory(category: any) {
    const data: string = JSON.stringify(category);
    return this.http.post<any>(this.url, data);
  }
}
