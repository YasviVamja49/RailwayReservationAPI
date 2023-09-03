import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TicketModel,TicketInputModel } from 'src/app/models/TicketModels';
import { TrainModel } from 'src/app/models/TrainModel';
import { AdminService } from 'src/app/services/admin.service';
import { UserService } from 'src/app/services/user-service.service';

@Component({
  selector: 'app-ticket',
  templateUrl: './ticket.component.html',
  styleUrls: ['./ticket.component.css']
})
export class TicketComponent implements OnInit {
  errorMessage!: string;
  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.GetDetailsForBook(id);
    }
    else {
      this.loadDate();
    }
  }
  isAddForm: boolean = false;
  str: string = "View";
  ticketForm!: FormGroup;
  tickets!: TicketModel[];
  train!:TrainModel;
  passenger!:string[];
  ticket!:TicketModel;
  IsBooked:Boolean;

  constructor(
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private userService: UserService,
    private adminService:AdminService,
    private router: Router
  ) {
    this.IsBooked=false;
    
   }


   loadDate():void{
    this.tickets=[];
    this.userService.getTickets().subscribe(data => {
      console.log(data);
      this.tickets = data;
      this.str="View"
    },
    error=>{
      if (error.status === 401) {
        this.router.navigate(['/login']);
      }
      else if(error.status===0){
        alert("Server Is Down");
        return;

      }
      else if(error.status===403){
        alert("Access Denied");
      }
      //console.clear();
      this.router.navigate(['/home']);
    }
    );
   }
  GetDetailsForBook(id:string){
    this.isAddForm = true;
      //console.log(id);
      this.str = "Add Ticket";
      this.ticketForm = this.formBuilder.group({
        passengerName: ['', Validators.required],
        ticketclass: ['', Validators.required]
      });
      this.adminService.getTrain(Number(id)).subscribe(
        Response=>{
          this.train = {
            trainNo:Response.trainNo,
            trainName: Response.trainName,
            startloc: Response.startloc,
            endloc: Response.endloc,
            arrivaltime: Response.arrivaltime,
            departuretime: Response.departuretime,
            arrivaldate: Response.arrivaldate,
            ac1tier: Response.ac1tier,
            ac2tier: Response.ac2tier,
            ac3tier: Response.ac3tier,
            sleeper: Response.sleeper,
            tatkal: Response.tatkal,
            ladies: Response.ladies,
            baseFare:Response.baseFare
          };
          console.log(this.train);
        },
        error=>{
          if (error.status === 401) {
            this.router.navigate(['/login']);
          }
          else if(error.status===0){
            alert("Server Is Down");
            return;
  
          }
          else if(error.status===403){
            alert("Access Denied");
          }
          //console.clear();
          this.router.navigate(['/home']);
        }
        
      );
      this.userService.GetPassenger().subscribe(data=>{
        // for(let p of data){
        //   console.log(p);
        //   this.passenger.map
        // }
        this.passenger=data.map(item => item.passengerName);
      console.log(this.passenger);
      for(let p of this.passenger){
        console.log(p.toString());
      }
    },
    error=>{
      if (error.status === 401) {
        this.router.navigate(['/login']);
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

  

  onSubmit(){
    if(this.ticketForm.invalid){
      alert("Please fill all data");
      return;
    }
    const passengerSelect=this.ticketForm.value.passengerName;
    const tclass=this.ticketForm.value.ticketclass;
    console.log(tclass);
    this.tickets=[];
    
      var ticket:TicketInputModel=new TicketInputModel(tclass,passengerSelect,this.train.trainNo);
      console.log(ticket);
      this.userService.BookTicket(ticket).subscribe(response=>{
        console.log(response);
        this.ticket=new TicketModel(
          response.ticketclass,
          response.passengerName,
          response.trainNo,
          response.berthno, 
          response.coachno,
          response.arrivaldate,
          response.bookingdate,
          response.bookingstatus
        );
    
      console.log(this.ticket);
          this.IsBooked=true;
          //this.router.navigate(['/ticket']);
        },
        error=>{
          console.log(error);
          if (error.status === 401) {
            this.errorMessage = 'Details';
          }
          else if(error.status===0){
            alert("Server Is Down");
            return;
            
          }else if(error.status===403){
            alert("Access Denied");
          }
          //this.isLoginFailed = true;
          //console.clear();
          this.router.navigate(['/error']);
        });
  }
  cancelTicket(ticketNo:number):void{
    console.log(ticketNo);
    const confirmed = confirm('Are you sure you want to delete this train?');
    if (confirmed) {
      
      this.userService.CancelTicket(ticketNo).subscribe(
        (response) => {

          console.log(response);
          this.loadDate();
        },
        (error) => {
          if (error.status === 401) {
            alert('Invalid Request');
          }
          else if(error.status===0){
            alert("Server Is Down");
            return;
            
          }
          else if(error.status===403){
            alert("Access Denied");
          }
          //console.log(error);
          //this.router.navigate(['/error']);
        }
      );
      
    } else {
      // User canceled the deletion, do nothing
    }
    window.location.reload();
    this.loadDate();
  }

}
