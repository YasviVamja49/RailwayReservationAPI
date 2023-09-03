import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginModel } from '../models/LoginModel';
import { catchError, map } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { RegistrationModel } from '../models/RegistrationModel';
import { StorageService } from './storage-service.service';

const AUTH_API = 'https://localhost:44383/Authentication/';
const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient,private storageService:StorageService) {}

  login(login:LoginModel): Observable<any> {
    return this.http.post(
      AUTH_API + 'login',
      JSON.stringify(login),
      httpOptions
    );
  }

  register(registrationModel:RegistrationModel): Observable<any> {
    
    return this.http.post(
      AUTH_API + 'register',JSON.stringify(registrationModel),
      httpOptions
    );
  }
  registerAdmin(registrationModel:RegistrationModel): Observable<any> {
    return this.http.post(
      AUTH_API + 'register-admin',JSON.stringify(registrationModel),
      {
        headers: new HttpHeaders({
          'Content-Type': 'application/json',
          Authorization: `Bearer ${this.storageService.getUser()}`
        })
      }
    );
  }
  
  
}
