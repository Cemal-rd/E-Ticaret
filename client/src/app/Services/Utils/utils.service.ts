import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UtilsService {
  private IS_LOGGED_IN_KEY: string = 'isLoggedIn';

  constructor() { }
  set isLoggedIn(value: boolean) {
    localStorage.setItem(this.IS_LOGGED_IN_KEY, JSON.stringify(value));
  }

  get isLoggedIn(): boolean {
    const result = JSON.parse(localStorage.getItem(this.IS_LOGGED_IN_KEY));

    if(result != null) return result;
    return false;
  }

  getEntityNames(entities: any[]): string[] {
    let result: string[] = [];

    entities.forEach(entity => {
      result.push(entity.name);
    });

    return result;
  }
}
