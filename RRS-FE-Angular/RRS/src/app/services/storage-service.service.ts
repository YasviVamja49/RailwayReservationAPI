import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

const USER_KEY = 'auth-user';

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  constructor(private router: Router) { }

  clean(): void {
    window.localStorage.clear();
    this.router.navigate(['/home']);
    window.location.reload();
  }

  public saveUser(user: any): void {
    window.localStorage.removeItem(USER_KEY);
    window.localStorage.setItem(USER_KEY, JSON.stringify(user));
    console.log(this.getUser());
  }

  public getUser(): any {
    const user = window.localStorage.getItem(USER_KEY);
    if (user) {
      const { token ,expiration} = JSON.parse(user);
      console.log("exper+" + expiration);
      const currentTime = Date.now() / 1000; // Convert to seconds

      if (expiration && currentTime > expiration) {
        this.logout();
        return null;
      }

    return JSON.parse(user).token;
    }

    return null;
  }

  public isLoggedIn(): boolean {
    const user = window.localStorage.getItem(USER_KEY);
  if (user) {
    const { expiration } = JSON.parse(user);
    const currentTime = Date.now() / 1000; // Convert to seconds
    const currentDate = Date.now();
    const date=new Date(expiration);
    if (date.getTime() < currentDate) {
      console.log("The date is in the past.");
      this.logout();
      return false;
    } 
    

    return true;
  }

  return false;
  }

  public logout(): void {
    this.clean();
   // window.localStorage.removeItem(USER_KEY);
    
  }

}
