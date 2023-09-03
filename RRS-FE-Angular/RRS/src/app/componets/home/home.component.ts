import { Component, Renderer2 } from '@angular/core';
import { StorageService } from 'src/app/services/storage-service.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  token: string;
  isLoggedIn: boolean;

  constructor(private storageService: StorageService,private renderer: Renderer2) {
    this.token = this.storageService.getUser();
    this.isLoggedIn = this.storageService.isLoggedIn();
   }
   
  ngOnInit() {
   ;
    const styleEl = this.renderer.createElement('style');
    //styleEl.appendChild(this.renderer.createText(styles));
    this.renderer.appendChild(document.head, styleEl);
  }
}
