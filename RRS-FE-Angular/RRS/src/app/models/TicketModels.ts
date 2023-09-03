export class TicketModel {
  ticketno!:number;
    ticketclass: string;
    passengerName: string;
    trainNo: number;
    berthno: number;
    coachno: number;
    arrivaldate: string;
    bookingdate: string;
    bookingstatus: string;
  
    constructor(
      //ticketno:number,
      ticketClass: string,
      passengerName: string,
      trainNo: number,
      berthNo: number,
      coachNo: number,
      arrivalDate: string,
      bookingDate: string,
      bookingStatus: string
    ) {
      //this.ticketno=ticketno;
      this.ticketclass = ticketClass;
      this.passengerName = passengerName;
      this.trainNo = trainNo;
      this.berthno = berthNo;
      this.coachno = coachNo;
      this.arrivaldate = arrivalDate;
      this.bookingdate = bookingDate;
      this.bookingstatus = bookingStatus;
    }
  }
  

  export class TicketInputModel {
    ticketclass: string;
    passengerName: string;
    trainNo: number;
  
    constructor(ticketClass: string, passengerName: string, trainNo: number) {
      this.ticketclass = ticketClass;
      this.passengerName = passengerName;
      this.trainNo = trainNo;
    }
  }
 
  