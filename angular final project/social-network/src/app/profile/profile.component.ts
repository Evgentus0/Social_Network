import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { UserService } from '../shared/services/user.service';
import { Router } from '@angular/router';
import { User } from '../shared/models/user.model';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  constructor(private service:UserService, private route:Router){}
  
  private user:User;
  error:any;
  @Output() "activate"
  activeEvent:EventEmitter<User>=new EventEmitter<User>();
  role:string;


 hasPicture:boolean=true;
 ngOnInit() {
   this.service.getUser().subscribe((data:User)=>{this.user={
       name:data["Name"],
       city:data["City"].Name,
       id:data["Id"],
       country:data["Country"].Name,
       email:data["Email"],
       personalInfo:data["PersonalInfo"],
       phone:data["PhoneNumber"],
       picturePath:"https://localhost:44348/Photo/"+data["PictureProfilePath"],
       relationship:data["Relationship"].Name,
       surname:data["Surname"],
       gender:data["Gender"]
     }
     this.role=data["Role"];
     sessionStorage.setItem("userName",data["Id"]);
     if(data["PictureProfilePath"]==""){
       this.hasPicture=false;
     }
   },
   error=>{
    console.log(error);
    this.route.navigate(["error", {message:error["error"].Message, status:error["status"]}])
  });
  
 }

 logout() {
   console.log("logout");
   if(confirm("Do you really want to log out?")) {
     sessionStorage.removeItem("tokenInfo");
     this.route.navigate(["sign-in"]);
   }
 }

 isModerator():boolean{
   if(this.role=="Moderator"){
     return true;
   }
   else{
     return false;
   }
 }

 addPublication(){
   console.log("clucl");
   this.route.navigate(["add-publication",]);
 }

 deleteUsers(){
   this.route.navigate(["all-users-delete"]);
 }
}
