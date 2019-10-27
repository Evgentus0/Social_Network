import { Component, OnInit } from '@angular/core';
import { User } from '../shared/Models/user.model';
import { MessageHeaderModel } from '../shared/Models/messageHeader.model';
import { UserService } from '../shared/services/user.service';
import { MessageHeaderService } from '../shared/services/message-header.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-messanger',
  templateUrl: './add-messanger.component.html',
  styleUrls: ['./add-messanger.component.css']
})
export class AddMessangerComponent implements OnInit {

  constructor(private userService:UserService, private messageHeaderService:MessageHeaderService, private route:Router) { }

  users:User[];
  user:string;
  messageHeader:MessageHeaderModel=new MessageHeaderModel();
  usersId:string[];


  ngOnInit() {
    this.users=[];
    this.usersId=[];
    this.userService.getFollowingById(sessionStorage.getItem("userName")).subscribe((data:User[])=>{
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
  }

  addUser(){
  if(this.user==undefined||this.user==""){
    alert("Choose user");
  }
    else{
      if(this.usersId.filter(val=>val==this.user).length>0){
        alert("this user is already added");
      }
      else{
      this.usersId.push(this.user);
      }
    }
  }

  add(){
    if(this.messageHeader.header==undefined||this.messageHeader.header==""){
      alert("Name a header");
    }
    else{
      if(this.usersId.length==0){
        alert("Add users");
      }
      else{
        this.usersId.push(sessionStorage.getItem("userName"));
        if(this.usersId.length>2){
          this.messageHeader.typeId=2;
        }
        else{
          this.messageHeader.typeId=1;
        }
        this.messageHeaderService.createMessageHeader(this.messageHeader, this.usersId).subscribe(()=>{
          this.route.navigate(["messanger"]);
        });
      }
    }
  }

  cancel(){
    this.route.navigate(["messanger"]);
  }
}
