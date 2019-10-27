import { Component, OnInit } from '@angular/core';
import { User } from '../shared/Models/user.model';
import { UserService } from '../shared/services/user.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-start-page',
  templateUrl: './start-page.component.html',
  styleUrls: ['./start-page.component.css']
})
export class StartPageComponent implements OnInit {

  constructor(private service:UserService, private currentRoute:ActivatedRoute, private route:Router) { }
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
  goToUser(id:string){
    console.log("press");
    this.route.navigate(["user-profile", id])
  }
}
