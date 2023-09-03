import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TrainModel } from 'src/app/models/TrainModel';
import { AdminService } from 'src/app/services/admin.service';
import { DatePipe } from '@angular/common';
import { StorageService } from 'src/app/services/storage-service.service';
import { UserService } from 'src/app/services/user-service.service';


@Component({
  selector: 'app-train-edit',
  templateUrl: './train-edit.component.html',
  styleUrls: ['./train-edit.component.css']
})
export class TrainEditComponent implements OnInit {
  trainForm: FormGroup;
  minDate: string;
  trainId!: number; // The ID of the train to edit
  Islogin():void{
    if (this.storageService.isLoggedIn()) {

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
  constructor(
    private formBuilder: FormBuilder,
    private adminService: AdminService,
    private router: Router,
    private route:ActivatedRoute,
    private datePipe: DatePipe,private storageService:StorageService,private userService:UserService
    
  ) {
    this.minDate = new Date().toISOString().split('T')[0];
    this.trainForm = this.formBuilder.group({
      trainName: ['', Validators.required],
      startloc: ['', Validators.required],
      endloc: ['', Validators.required],
      arrivaltime: ['', Validators.required],
      departuretime: ['', Validators.required],
      arrivaldate: ['', Validators.required],
      ac1tier: [0, [Validators.required, Validators.min(0)]],
      ac2tier: [0, [Validators.required, Validators.min(0)]],
      ac3tier: [0, [Validators.required, Validators.min(0)]],
      sleeper: [0, [Validators.required, Validators.min(0)]],
      tatkal: [0, [Validators.required, Validators.min(0)]],
      ladies: [0, [Validators.required, Validators.min(0)]],
      baseFare:[200,Validators.min(0)]
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.Islogin();
      this.trainId = +params['id']; // Retrieve the train ID from the route parameter
      console.log(this.trainId);
      // Fetch the train data for editing
      if (this.trainId) {
        this.adminService.getTrain(this.trainId).subscribe(
          (train: TrainModel) => {
            // Populate the edit form with the retrieved train data
            console.log(train);
            train.arrivaltime = this.convertToTime(train.arrivaltime);
            train.departuretime = this.convertToTime(train.departuretime);
            train.ac1tier = Number(train.ac1tier);
            train.ac2tier = Number(train.ac2tier);
            train.ac3tier = Number(train.ac3tier);
            train.sleeper = Number(train.sleeper);
            train.tatkal = Number(train.tatkal);
            train.ladies = Number(train.ladies);
            train.baseFare=Number(train.baseFare);


            this.trainForm.patchValue(train);
            

          },
          (error) => {
            // Handle error if necessary
            console.error(error);
          }
        );
      }
    });
  }
  
  convertToTime(timeString: string): string {
    // Split the time string into hours and minutes
    const [time, period] = timeString.split(' ');

    let [hours, minutes] = time.split(':').map(Number);
    if (period === 'PM' && hours < 12) {
      hours += 12;
    } else if (period === 'AM' && hours === 12) {
      hours = 0;
    }

    const formattedHours = hours.toString().padStart(2, '0');
  const formattedMinutes = minutes.toString().padStart(2, '0');

    return `${formattedHours}:${formattedMinutes}`;

  }

  onSubmit(): void {
    if (this.trainForm.invalid) {
      return;
    }

    const trainData = this.trainForm.value as TrainModel;

    if (this.trainId) {
      // Update an existing train
      this.adminService.updateTrain(this.trainId, trainData).subscribe(
        data => {
          // Redirect to the list of trains on success
          console.log(data);
          this.router.navigate(['/train']);

        },
        (error) => {
          // Handle error if necessary
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
          this.router.navigate(['/error']);
        }
      );
    } 
    
  }
  cancel(){
    this.router.navigate(['/train']);
  }
}