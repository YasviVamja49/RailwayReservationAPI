export class PassengerModel {
    passengerName: string;
    gender: string;
    constructor(email:string,password:string){
        this.passengerName = email;
        this.gender=password;
    }
  }