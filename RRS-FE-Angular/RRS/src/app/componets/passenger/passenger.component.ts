import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PassengerModel } from 'src/app/models/PassengerModel';
import { UserService } from 'src/app/services/user-service.service';


@Component({
  selector: 'app-passenger',
  templateUrl: './passenger.component.html',
  styleUrls: ['./passenger.component.css']
})
export class PassengerComponent {
  isAddForm: Boolean = new Boolean();
  str: String = "View";
  passengerForm!: FormGroup;
  passengers!: PassengerModel[];
  errorMessage!: string;
  constructor(private route: ActivatedRoute, private formBuilder: FormBuilder,private userService:UserService,private router:Router) { }
  ngOnInit(): void {

    this.isAddForm = this.route.snapshot.routeConfig?.path === 'passengers/new';
    if (this.isAddForm) {
      this.str = "Add Passenger";
    }

    this.passengerForm = this.formBuilder.group({
      passengerName: ['', Validators.required],
      gender: ['', Validators.required]
    });

    this.loadPassenger();
  
    
  }
  loadPassenger(){
    this.userService.GetPassenger().subscribe(data=>{
      console.log(data);
      
      this.passengers=data;
    },
      error => {
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
        this.router.navigate(['/login']);
      }
    );
  }
  onSubmit() {

    if (this.passengerForm.valid) {
      const name=this.passengerForm.value.passengerName;
      console.log(name);
      const gender=this.passengerForm.value.gender;
      const pasModel:PassengerModel=new PassengerModel(name,gender);
      console.log(pasModel);
      this.userService.AddPassenger(pasModel).subscribe(data=>{
        console.log(data);
      });
    }
    this.loadPassenger();
    this.router.navigate(['/passengers']);
  }

}
