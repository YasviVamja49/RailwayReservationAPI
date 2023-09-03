import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms'
import { Router } from '@angular/router';
import ValidateForm from 'src/app/helpers/ValidateForm';
import { RegistrationModel } from 'src/app/models/RegistrationModel';
import { AuthService } from 'src/app/services/auth-service.service';
import { StorageService } from 'src/app/services/storage-service.service';
import { UserService } from 'src/app/services/user-service.service';


@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
  type: string = "password";
  isText: boolean = false;
  eyeIcon = "fa-eye-slash";
  password: string = "";
  confirmPassword: string = "";
  signUp!: FormGroup;
  isSuccessful = false;
  isSignUpFailed = false;
  errorMessage = '';
  IsUserAdmin: Boolean = false;
  isLoggedIn: boolean = false;

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router, private storageService: StorageService, private userService: UserService) {

    this.signUp = this.fb.group({
      name: ['', [Validators.required]],
      gender: ['', Validators.required],
      dob: ['', Validators.required],
      email: ['', Validators.compose([
        Validators.required,
        Validators.email])],
      city: ['', Validators.required],
      password: ['', Validators.compose([
        Validators.required,
        Validators.minLength(8),
        Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]).{8,16}$/)])],
      cpassword: ['', Validators.required]
    }, { validator: this.matchingPasswords('password', 'cpassword') });


  }
  matchingPasswords(passwordKey: string, confirmPasswordKey: string) {
    return (group: FormGroup) => {
      const password = group.controls[passwordKey];
      const confirmPassword = group.controls[confirmPasswordKey];
      if (password.value !== confirmPassword.value) {
        return confirmPassword.setErrors({ mismatchedPasswords: true });
      }
    };

  }
  ngOnInit(): void {
    console.log(this.isLoggedIn)
    if (this.storageService.isLoggedIn()) {
      this.isLoggedIn = true;
      this.userService.isUserAdmin().subscribe((isAdmin: boolean) => {
        console.log("isadmin " + isAdmin);
        if (isAdmin) {
          this.IsUserAdmin = true;
        }
        else {
          this.IsUserAdmin = false;
        }
        console.log("isadmin" + this.IsUserAdmin);
      });
    }

  }
  hodeShow() {
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = "fa-eye" : this.eyeIcon = "fa-eye-slash";
    this.isText ? this.type = "text" : this.type = "password";
  }
  OnFocusDate(target: EventTarget | null) {
    if (target instanceof HTMLInputElement) {
      target.type = "date";
      const date = new Date(new Date().getFullYear() - 18, new Date().getMonth(), new Date().getDate()).toISOString().split("T")[0];
      target.setAttribute("max", date);
    }
  }
  passwordsMatch() {
    return this.password === this.confirmPassword;
  }

  onSubmit() {     

    if (this.signUp.valid) {
      const name = this.signUp.value.name;
      const gender = this.signUp.value.gender;
      const dob = this.signUp.value.dob;
      const email = this.signUp.value.email;
      const city = this.signUp.value.city;
      const password = this.signUp.value.password;
      const registrationModel: RegistrationModel = new RegistrationModel(name, gender, dob, email, city, password);
      console.log(registrationModel);
      if (this.isLoggedIn && this.IsUserAdmin) {
        console.log(this.IsUserAdmin);
        this.authService.registerAdmin(registrationModel).subscribe(
          response => {
            console.log(response);
            this.isSignUpFailed = false;
            this.isSuccessful = true;
            this.router.navigate(['/home']);
          },
          error => {
            if (error.status === 401) {
              this.errorMessage = 'Invalid Details';
            }
            else if(error.status===0){
              alert("Server Is Down");
              return;
    
            }
            else if(error.status===403){
              alert("Access Denied");
            }
            //console.clear();
            this.router.navigate(['/error']);
          }
        );
      }
      else {
        this.authService.register(registrationModel).subscribe(
          response => {
            console.log(response);
            this.isSignUpFailed = false;
            this.isSuccessful = true;
            this.router.navigate(['/login']);
          },
          error => {
            if (error.status === 401) {
              this.errorMessage = 'Invalid Details';
            }
            else if(error.status===0){
              alert("Server Is Down");
              return;
    
            }
            //console.clear();
            this.router.navigate(['/error']);
          }
        );
      }
    }
    else {
      console.log("form invalid");
      ValidateForm.validateFormField(this.signUp)
    }
  }
}
