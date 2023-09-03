import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { StorageService } from './storage-service.service';
import { Observable } from 'rxjs';
import { TrainModel } from '../models/TrainModel';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http: HttpClient,private storageService:StorageService) {}
  private baseUrl = 'https://localhost:44383/';
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${this.storageService.getUser()}`
    })
  };

  public GetAllTrain():Observable<TrainModel[]>{
    return this.http.get<TrainModel[]>(this.baseUrl + 'Train/GetAll',this.httpOptions);
  }
  public getTrain(trainId: number): Observable<TrainModel> {
    return this.http.get<TrainModel>(`${this.baseUrl}Train/GetTrainById/${trainId}`);
  }

  public updateTrain(trainId: number, trainData: TrainModel): Observable<TrainModel> {
    trainData.arrivaltime = this.formatTime(trainData.arrivaltime);
    trainData.departuretime = this.formatTime(trainData.departuretime);
    return this.http.put<TrainModel>(`${this.baseUrl}Train/EditTrainById/${trainId}`, trainData,this.httpOptions);
  } 
  public createTrain(trainData:TrainModel):Observable<any>{
    trainData.arrivaltime = this.formatTime(trainData.arrivaltime);
    trainData.departuretime = this.formatTime(trainData.departuretime);

    return this.http.post<any>(this.baseUrl+'Train/AddTrain', trainData,this.httpOptions);
  }

  public deleteTrain(id:number):Observable<any>{
    return this.http.delete<any>(`${this.baseUrl}Train/DeleteTrainById/${id}`,this.httpOptions)
  }
  private formatTime(time: string): string {
    const [hour, minute] = time.split(':');
    const date = new Date();
    date.setHours(Number(hour));
    date.setMinutes(Number(minute));

    return date.toLocaleTimeString([], { hour: 'numeric', minute: '2-digit' });
  }
}
