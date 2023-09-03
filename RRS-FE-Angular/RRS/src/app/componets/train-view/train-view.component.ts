import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TrainModel } from 'src/app/models/TrainModel';
import { AdminService } from 'src/app/services/admin.service';
import { StorageService } from 'src/app/services/storage-service.service';
import { UserService } from 'src/app/services/user-service.service';

@Component({
  selector: 'app-train-view',
  templateUrl: './train-view.component.html',
  styleUrls: ['./train-view.component.css']
})
export class TrainViewComponent implements OnInit{
  trains: TrainModel[] = [];
  trainSearchForm!: FormGroup;
  isLoggedIn: boolean=false;
  IsUserAdmin: boolean=false;
  date:string='text';
  constructor(private adminService: AdminService,
    private router:Router,
    private storageService: StorageService, 
    private userService: UserService,
    private formBuilder: FormBuilder
    ) {}

  ngOnInit(): void {
    this.fetchTrainData();
    this.Islogin();
    this.trainSearchForm = this.formBuilder.group({
      startLocation: ['', Validators.required],
      endLocation: ['', Validators.required],
      date: ['', Validators.required]
    });
  }
  Change(){
    this.date='date';
  }
  Islogin():void{
    if (this.storageService.isLoggedIn()) {
      this.isLoggedIn = true;
      this.userService.isUserAdmin().subscribe((isAdmin: boolean) => {
        //console.log("isadmindt " + isAdmin);
        if (isAdmin) {
          this.IsUserAdmin = true;
        }
        else {
          this.IsUserAdmin = false;
        }
        console.log("isadminvar" + this.IsUserAdmin);
      });
    }
  }

  fetchTrainData(): void {
    this.adminService.GetAllTrain().subscribe(
      (trainData: TrainModel[]) => {
        console.log(trainData);
        this.trains = trainData;
      },
      (error: any) => {
        console.error(error);
      }
    );
  }

  editTrain(id: number): void {
    // Redirect to the edit page for the specified train ID
    // Assuming the route for the edit page is '/edit/:id'
    this.router.navigate([`train/edit/${id}`]);
  }
  AddTrain(){
    this.router.navigate(['train/new']);
  }

  deleteTrain(id: number): void {
    // Show a confirmation dialog before deleting the train
    const confirmed = confirm('Are you sure you want to delete this train?');
    if (confirmed) {
      // Call the deleteTrain method from the admin service to delete the train
      this.adminService.deleteTrain(id).subscribe(
        (response) => {
          // Train deleted successfully, remove it from the trains array
          //this.trains = this.trains.filter(train => train.id !== id);
          console.log(response);
          this.fetchTrainData();
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
          //console.clear();
          this.router.navigate(['/error']);
        }
      );
    } else {
      // User canceled the deletion, do nothing
    }
  }

  BookTicket(trainNo:number){
    if(this.isLoggedIn){
      //console.log("booking")
      this.router.navigate([`/book/${trainNo}`]);
    }
    else{
      this.router.navigate(['/login']);
    }
  }

  searchTrains() {
    // this.http.get<any[]>(apiUrl, {
    //   params: {
    //     startLocation: this.startLocation,
    //     endLocation: this.endLocation,
    //     date: this.date
    //   }
    // }).subscribe((response) => {
    //   this.trains = response;
    // // });
    // this.userService.SearchTrain(this.trainSearchForm.value.startLocation,
      // this.trainSearchForm.value.endLocation,
      // this.trainSearchForm.value.date).subscribe(data=>{
      //   console.log(data);

      // })
      
      
      this.userService.SearchTrain(this.trainSearchForm.value.startLocation,
      this.trainSearchForm.value.endLocation,
      this.trainSearchForm.value.date).subscribe(data=>{
        console.log(data);
        this.trains=data;
      },
      (error) => {
        // Handle error if necessary
        if(error.status===0){
          alert("Server Is Down");
          return;
          
        }
        
        this.router.navigate(['/error']);
      })
  }
  OnFocusDate(target: EventTarget | null) {
    if (target instanceof HTMLInputElement) {
      target.type = "date";
      const date = new Date().toISOString().split("T")[0];
      target.setAttribute("min", date);
    }
  }
  
}
