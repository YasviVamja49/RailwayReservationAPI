import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms'
import { Router } from '@angular/router';
import ValidateForm from 'src/app/helpers/ValidateForm';
import { LoginModel } from 'src/app/models/LoginModel';
import { AuthService } from 'src/app/services/auth-service.service';
import { StorageService } from 'src/app/services/storage-service.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements  OnInit{
  type:string="password";
  isText:boolean=false;
  eyeIcon="fa-eye-slash";
  loginForm! :FormGroup;
  isLoggedIn = false;
  isLoginFailed = false;
  errorMessage = '';
  roles: string[] = [];
  
  constructor(private fb:FormBuilder,private authService: AuthService, private storageService: StorageService,private router: Router){
    this.loginForm=this.fb.group({
      email:['',Validators.compose([
        Validators.required,
        Validators.email])],
      password:['',Validators.required]
    })
  }

  ngOnInit():void{
    if (this.storageService.isLoggedIn()) {
      this.isLoggedIn = true;
      this.roles = this.storageService.getUser().roles;
      console.log(this.roles);
    }
  }
  hodeShow(){
    this.isText=!this.isText;
    this.isText ? this.eyeIcon="fa-eye" :this.eyeIcon="fa-eye-slash";
    this.isText ? this.type="text": this.type="password";
  }
  

  onSubmit(){
    if(this.loginForm.valid){
      const email = this.loginForm.value.email;
      const password = this.loginForm.value.password;
      const loginModel = new LoginModel(email, password);
      console.log(loginModel);
      this.authService.login(loginModel).subscribe( 
        response => {
        this.storageService.saveUser(response);
        
        this.isLoginFailed = false;
        this.isLoggedIn = true;
        this.roles = this.storageService.getUser().roles;
        this.reloadPage();
        this.router.navigate(['/home']);
      },
      error => {
        if (error.status === 401) {
          this.errorMessage = 'Invalid email or password';
        }
        else if(error.status===0){
          alert("Server Is Down");
          return;

        }
        this.isLoginFailed = true;
        //console.clear();
        //this.router.navigate(['/error']);
      }
    );
    }
    else{
      //console.log("form invalid");
      ValidateForm.validateFormField(this.loginForm)
    }
  }
  reloadPage(): void {
    window.location.reload();
  }
}
