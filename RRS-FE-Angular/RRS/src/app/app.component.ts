import { Component } from '@angular/core';
import { AuthService } from './services/auth-service.service';
import { StorageService } from './services/storage-service.service';
import { Router } from '@angular/router';
import { UserService } from './services/user-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent { 
  title = 'RRS';
  
  isLoggedIn :boolean= false;
  username?: string;
  IsUserAdmin: boolean=false;
  constructor(
    private storageService: StorageService,
    private authService: AuthService,
    private router:Router,
    private userService: UserService,
  ) {}

  ngOnInit(): void {
    this.Islogin();
  }
  Islogin():void{
    if (this.storageService.isLoggedIn()) {
      this.isLoggedIn = true;
      this.userService.isUserAdmin().subscribe((isAdmin: boolean) => {
        //console.log("isadmindt " + isAdmin);
        if (isAdmin) {
          this.IsUserAdmin = true;
        }
        else {
          this.IsUserAdmin = false;
        }
        console.log("isadminvar" + this.IsUserAdmin);
      });
    }
  }
  logout(): void {
    
        this.storageService.logout();
        //this.router.navigate(['/login']);
}
navigateTo(route: string): void {
  this.router.navigate([route]);
}
}

