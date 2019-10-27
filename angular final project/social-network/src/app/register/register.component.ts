import { Component, OnInit } from '@angular/core';
import { AdditionalService } from '../shared/services/additional.service';
import { UserRegisterModel } from '../shared/models/user-register.model';
import { Country } from '../shared/models/country.model';
import { CityModel } from '../shared/models/city.model';
import { RelationshipModel } from '../shared/models/relationship.model';
import { UserService } from '../shared/services/user.service';
import { UserLoginModel } from '../shared/Models/userLoginModel';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private service:AdditionalService, private userService:UserService, private route:Router) { }
  register:UserRegisterModel=new UserRegisterModel();
  countries:Country[];
  cities:CityModel[];
  relationship:RelationshipModel[];

  ngOnInit() {
    this.cities=[];
    this.countries=[];
    this.relationship=[];
    this.service.getCities().subscribe((data)=>{
      data.forEach(element=>{
        this.cities.push({
          id:element["Id"],
          name:element["Name"],
          country:{
            id:element["Country"].Id,
            name:element["Country"].Name
          }
        })
      })
    });
    this.service.getCountries().subscribe((data)=>{
      data.forEach(element=>{
        this.countries.push({
          id:element["Id"],
          name:element["Name"]
        })
      })
    });
    this.service.getRelationship().subscribe((data)=>{
      data.forEach(element=>{
        this.relationship.push({
          id:element["Id"],
          name:element["Name"],
        })
      })
    });
  }

    userRegister(){
    if(this.register.name==undefined||this.register.name==""
      ||this.register.surname==undefined||this.register.surname==""
      ||this.register.phoneNumber==undefined||this.register.phoneNumber==""
      ||this.register.gender==undefined
      ||this.register.dateBirth==undefined
      ||this.register.personalInfo==undefined||this.register.personalInfo==""
      ||this.register.relationshipId==undefined
      ||this.register.countryId==undefined
      ||this.register.cityId==undefined
      ||this.register.email==undefined||this.register.email==""
      ||this.register.password==undefined||this.register.password==""
      ||this.register.confirmPassword==undefined||this.register.confirmPassword==""){
        let message:string="Please, fill your ";
        if(this.register.name==undefined||this.register.name==""){message+="name, "}
        if(this.register.surname==undefined||this.register.surname==""){message+="surname, "}
        if(this.register.phoneNumber==undefined||this.register.phoneNumber==""){message+="phone number, "}
        if(this.register.gender==undefined){message+="gender, "}
        if(this.register.dateBirth==undefined){message+="date of birth, "}
        if(this.register.personalInfo==undefined||this.register.personalInfo==""){message+="personal info, "}
        if(this.register.relationshipId==undefined){message+="relationship, "}
        if(this.register.countryId==undefined){message+="country, "}
        if(this.register.cityId==undefined){message+="city, "}
        if(this.register.email==undefined||this.register.email==""){message+="email, "}
        if(this.register.password==undefined||this.register.password==""){message+="password, "}
        if(this.register.confirmPassword==undefined||this.register.confirmPassword==""){message+="confirm password, "}
        message=message.slice(0,message.length - 2)
        message+=".";
        alert(message);
        console.log(this.register);
      }
      else{
        if(this.register.password.length<6){
          alert("Password must have minimal 6 symbols");
        }
          else{
            if(this.register.password!=this.register.confirmPassword){
              alert("Password and Confirm Password are not equal");
            }
            else{
              let regexp = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);
              if(!regexp.test(this.register.email)){
                alert("Incorrect email adress!");
              }
              else{
                  this.userService.register(this.register).subscribe(()=>{
                    let login:UserLoginModel={
                      email:this.register.email,
                      password:this.register.password,
                    };
                    this.userService.loginUser(login).subscribe((data:any)=>{
                        sessionStorage.setItem("tokenInfo", data.access_token);
                        this.route.navigate(["profile"]);
                    }, error=>{
                      this.route.navigate(["error", {message:error["error"].Message, status:error["status"]}])
                    });
                  }, error=>{
                    this.route.navigate(["error", {message:error["error"].Message, status:error["status"]}])
                  });
              }
            }
          }
      }
  }

}
