import { Component, OnInit } from '@angular/core';
import { User } from '../shared/Models/user.model';
import { UserService } from '../shared/services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-all-users-delete',
  templateUrl: './all-users-delete.component.html',
  styleUrls: ['./all-users-delete.component.css']
})
export class AllUsersDeleteComponent implements OnInit {

  constructor(private service:UserService, private route:Router) { }

  users:User[];
  ngOnInit() {
    this.users=[];
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
  }

  delete(id:string){
    this.service.delete(id).subscribe(()=>{
      window.location.reload();
    },
    error=>{
      console.log(error);
      this.route.navigate(["error", {message:error["error"].Message, status:error["status"]}])
    });
  }
}
