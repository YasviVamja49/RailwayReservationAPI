import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { StorageService } from './storage-service.service';
import { PassengerModel } from '../models/PassengerModel';
import { TicketInputModel, TicketModel } from '../models/TicketModels';
import { TrainModel } from '../models/TrainModel';
const USER_KEY = 'auth-user';
@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient,private storageService:StorageService) {}
  private baseUrl = 'https://localhost:44383/';
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${this.storageService.getUser()}`
    })
  };
  public getUserRole(): Observable<any> {
    return this.http.get(`${this.baseUrl}Authentication/role`, this.httpOptions);
  }
  public isUserAdmin(): Observable<boolean> {
    return this.getUserRole().pipe(
      map(
        (response: any) =>response.role === 'Admin')
    );
  }
  public AddPassenger(passenger:PassengerModel):Observable<any>{
    return this.http.post(`${this.baseUrl}Passenger/AddPassenger`,passenger,this.httpOptions);
  }
  
  public GetPassenger():Observable<PassengerModel[]>{
    return this.http.get<PassengerModel[]>(`${this.baseUrl}Passenger/ViewPassengers`,this.httpOptions);
  }
  public getTickets():Observable<TicketModel[]>{
    return this.http.get<TicketModel[]>(`${this.baseUrl}Ticket`,this.httpOptions);
  }
  public BookTicket(ticket:TicketInputModel):Observable<TicketModel>{
    return this.http.post<TicketModel>(`${this.baseUrl}Ticket`,ticket,this.httpOptions);
  }
  public SearchTrain(start:string,end:string,date:string):Observable<TrainModel[]>{
    const params = new HttpParams()
    .set('startloc', start)
    .set('endloc', end)
    .set('date', date);
    console.log(encodeURIComponent(date));
    return this.http.get<TrainModel[]>(`${this.baseUrl}Train/Search`, { params });
}

public CancelTicket(ticketno:number):Observable<any>{
  return this.http.delete<any>(`${this.baseUrl}Ticket/${ticketno}`,this.httpOptions)

}
}
