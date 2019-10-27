import { Component, OnInit } from '@angular/core';
import { UserService } from '../shared/services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../shared/Models/user.model';

@Component({
  selector: 'app-add-following',
  templateUrl: './add-following.component.html',
  styleUrls: ['./add-following.component.css']
})
export class AddFollowingComponent implements OnInit {

  constructor(private service:UserService, private currentRoute:ActivatedRoute, private route:Router) { 
  }

  users:User[];
  following:User[];
  id:string;

  ngOnInit() {
    this.id=sessionStorage.getItem("userName");
    this.users=[];
    this.following=[];
    this.service.getAll().subscribe((data:User[])=>{
      data.forEach(element=>{
        this.users.push({
          name:element["Name"],
          city:"",
          id:element["Id"],
          country:"",
          email:element["Email"],
          personalInfo:element["PersonalInfo"],
          phone:element["PhoneNumber"],
          picturePath:"https://localhost:44348/Photo/"+element["PictureProfilePath"],
          relationship:"",
          surname:element["Surname"],
          gender:element["Gender"]
        })
      })
    });
    this.service.getFollowingById(this.id).subscribe((data:User[])=>{
      data.forEach(element=>{
        this.following.push({
          name:element["Name"],
          city:"",
          id:element["Id"],
          country:"",
          email:element["Email"],
          personalInfo:element["PersonalInfo"],
          phone:element["PhoneNumber"],
          picturePath:"https://localhost:44348/Photo/"+element["PictureProfilePath"],
          relationship:"",
          surname:element["Surname"],
          gender:element["Gener"]
        })
      })
    },
    error=>{
      console.log(error);
      this.route.navigate(["error", {message:error["error"].Message, status:error["status"]}])
    })
  }

  isFollowing(user:User):boolean{
    return (this.following.filter(val=>val.id==user.id).length>0);
  }

  follow(id:string){
    this.service.subscribe(id).subscribe(()=>{
      window.location.reload();
    }
    ,error=>{
      console.log(error);
      this.route.navigate(["error", {message:error["error"].Message, status:error["status"]}])
    });
  }

  unfollow(id:string){
    this.service.unsubscribe(id).subscribe(()=>{
      window.location.reload();
    }
    ,error=>{
      console.log(error);
      this.route.navigate(["error", {message:error["error"].Message, status:error["status"]}])
    });
  }

  goToUser(id:string){
    console.log("press");
    this.route.navigate(["user-profile", id])
  }

  isUser(id:string){
    if(id==this.id){
      return true;
    }
    else{
      return false;
    }
  }

}
