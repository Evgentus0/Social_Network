import { Component, OnInit } from '@angular/core';
import { User } from '../shared/Models/user.model';
import { UserService } from '../shared/services/user.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-followers',
  templateUrl: './followers.component.html',
  styleUrls: ['./followers.component.css']
})
export class FollowersComponent implements OnInit {

  constructor(private service:UserService, private currentRoute:ActivatedRoute, private route:Router) { 
  }

  users:User[];
  following:User[];
  id:string;

  ngOnInit() {
    this.id=this.currentRoute.snapshot.paramMap.get("id");
    this.users=[];
    this.following=[];
    this.service.getFollowersById(this.id).subscribe((data:User[])=>{
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

}
