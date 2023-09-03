import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './componets/login/login.component';
import { SignupComponent } from './componets/signup/signup.component';
import { FormsModule ,NgSelectOption,ReactiveFormsModule} from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './componets/home/home.component';
import { PassengerComponent } from './componets/passenger/passenger.component';
import { TrainNewComponent } from './componets/train-new/train-new.component';
import { TrainEditComponent } from './componets/train-edit/train-edit.component';
import { TrainViewComponent } from './componets/train-view/train-view.component';
import { DatePipe } from '@angular/common';
import { TicketComponent } from './componets/ticket/ticket.component';
import { ErrorComponent } from './componets/error/error.component';
import { ContactComponent } from './componets/contact/contact.component';
import { AboutComponent } from './componets/about/about.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    SignupComponent,
    HomeComponent,
    PassengerComponent,
    TrainNewComponent,
    TrainEditComponent,
    TrainViewComponent,
    TicketComponent,
    ErrorComponent,
    ContactComponent,
    AboutComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule
    
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
