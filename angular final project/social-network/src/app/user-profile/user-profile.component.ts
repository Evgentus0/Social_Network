import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { UserService } from '../shared/services/user.service';
import { Router, ActivatedRoute } from '@angular/router';
import { User } from '../shared/Models/user.model';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  constructor(private service:UserService, private route:Router, private activateRoute: ActivatedRoute){
    this.id=activateRoute.snapshot.params["id"];  
  }
  
  private user:User;
  error:any;
  id:string;  

 hasPicture:boolean=true;
 ngOnInit() {
   this.service.getUserById(this.id).subscribe((data:User)=>{this.user={
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
       gender:data["Gener"]
     }
     if(data["PictureProfilePath"]==""){
       this.hasPicture=false;
     }
   },
   error => {this.error = error.message; console.log(error);});
 }


}
