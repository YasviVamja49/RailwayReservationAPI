export class RegistrationModel {
    name: string;
    gender: string;
    dob: string;
    email: string;
    city: string;
    password: string;
  
    constructor(name: string, gender: string, dob: string, email: string, city: string, password: string) {
      this.name = name;
      this.gender = gender;
      this.dob = dob;
      this.email = email;
      this.city = city;
      this.password = password;
    }
  }
  