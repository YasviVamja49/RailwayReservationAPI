import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './componets/login/login.component';
import { SignupComponent } from './componets/signup/signup.component';
import { HomeComponent } from './componets/home/home.component';
import { PassengerComponent } from './componets/passenger/passenger.component';
import { TrainViewComponent } from './componets/train-view/train-view.component';
import { TrainNewComponent } from './componets/train-new/train-new.component';
import { TrainEditComponent } from './componets/train-edit/train-edit.component';
import { TicketComponent } from './componets/ticket/ticket.component';
import { ErrorComponent } from './componets/error/error.component';
import { AboutComponent } from './componets/about/about.component';
import { ContactComponent } from './componets/contact/contact.component';

const routes: Routes = [
  {path:'login',component:LoginComponent},
  {path:'signup',component:SignupComponent},
  {path:'home',component:HomeComponent},
  { path:'passengers', component: PassengerComponent },
  { path:'passengers/new', component: PassengerComponent },
  {path:'',component:HomeComponent},
  {path:'train',component:TrainViewComponent},
  {path:'train/new',component:TrainNewComponent},
  {path:'train/edit/:id',component:TrainEditComponent},
  {path:'book/:id',component:TicketComponent},
  {path:'ticket',component:TicketComponent},
  {path:'error',component:ErrorComponent},
  
  {path:'about',component:AboutComponent},
  {path:'contact',component:ContactComponent},
  { path: '**', redirectTo: 'error' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }