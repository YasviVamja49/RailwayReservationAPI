import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TrainModel } from 'src/app/models/TrainModel';
import { AdminService } from 'src/app/services/admin.service';
import { StorageService } from 'src/app/services/storage-service.service';
import { UserService } from 'src/app/services/user-service.service';

@Component({
  selector: 'app-train-new',
  templateUrl: './train-new.component.html',
  styleUrls: ['./train-new.component.css']
})
export class TrainNewComponent implements OnInit {
  trainForm: FormGroup;
  arrivalTimeType:string='text';
  departureTimeType:string='text';
  arrivalDateType:string='text';
  ladiesType: string='text';
  tatkalType: string='text';
  sleeperType: string='text';
  ac3TierType: string='text';
  ac2TierType: string='text';
  ac1TierType: string='text';
  baseFareType:string='text';
  isLoggedIn: boolean=false;
  IsUserAdmin: boolean=false;
  Islogin():void{
    if (this.storageService.isLoggedIn()) {
      this.isLoggedIn = true;
      this.userService.isUserAdmin().subscribe((isAdmin: boolean) => {
        //console.log("isadmindt " + isAdmin);
        if (isAdmin) {
          return;
        }
        else {
          alert("Unauthorized! You do not have permission to access this content.");
          this.router.navigate(['/home']);
        }
      });
    }
    else{
      this.router.navigate(['/login']);
    }
  }
  
  constructor(private formBuilder: FormBuilder,private adminService:AdminService,private router: Router,
    private storageService:StorageService,private userService:UserService) {
    this.trainForm = this.formBuilder.group({
      trainName: ['', Validators.required],
      startloc: ['', Validators.required],
      endloc: ['', Validators.required],
      arrivaltime: ['', Validators.required],
      departuretime: ['', Validators.required],
      arrivaldate: ['', Validators.required],
      ac1tier: ['', [Validators.required, Validators.min(0)]],
      ac2tier: ['', [Validators.required, Validators.min(0)]],
      ac3tier: ['', [Validators.required, Validators.min(0)]],
      sleeper: ['', [Validators.required, Validators.min(0)]],
      tatkal: ['', [Validators.required, Validators.min(0)]],
      ladies: ['', [Validators.required, Validators.min(0)]],
      baseFare:[200,Validators.min(0)]
    });
  }

  ngOnInit(): void {
    this.Islogin();
  }
  changeInputType(inputName: string) {
    switch (inputName) {
      case 'arrivaltime':
        this.arrivalTimeType = 'time';
        break;
      case 'departuretime':
        this.departureTimeType = 'time';
        break;
      case 'arrivaldate':
        this.arrivalDateType = 'date';
        break;
        case 'ac1tier':
          
          this.ac1TierType = 'number';
          break;
        case 'ac2tier':

          this.ac2TierType = 'number';
          break;
        case 'ac3tier':
 
          this.ac3TierType = 'number';
          break;
        case 'sleeper':
  
          this.sleeperType = 'number';
          break;
        case 'tatkal':
       
          this.tatkalType = 'number';
          break;
        case 'ladies':
      
          this.ladiesType = 'number';
          break;
          case 'baseFare':
      
          this.baseFareType = 'number';
          break;
      }
    }
    OnFocusDate(target: EventTarget | null) {
      if (target instanceof HTMLInputElement) {
        target.type = "date";
        const date = new Date().toISOString().split("T")[0];
        target.setAttribute("min", date);
      }
    }

  onSubmit(): void {
    if (this.trainForm.invalid) {
      alert("Fill the data Properly");
      return;
    }

    const trainData = this.trainForm.value as TrainModel;
    // Handle the form submission here
    // You can access the trainData object and its properties to send the data to the backend or perform any other actions
    console.log(trainData);
    this.adminService.createTrain(trainData)
      .subscribe(
        response => {
          // Handle the success response from the service
          console.log('Train created successfully:', response);
          this.router.navigate(['/train']);
        },
        error => {
          // Handle the error response from the service
          if (error.status === 401) {
            alert('Invalid Details');
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
}
