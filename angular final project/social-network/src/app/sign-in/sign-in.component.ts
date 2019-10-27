import { Component, OnInit } from '@angular/core';
import { UserService } from '../shared/services/user.service';
import { Router } from '@angular/router';
import { UserLoginModel } from '../shared/models/userLoginModel';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {

  constructor(private service:UserService, private route:Router) { }
  login:UserLoginModel=new UserLoginModel();
  isValid:boolean=true
  error:any;  
  ngOnInit() {
  }


  loginUser(){

    this.service.loginUser(this.login).subscribe((data:any)=>{
      sessionStorage.setItem("tokenInfo", data.access_token);
      this.route.navigate(["profile"]);
    }
    , error=>{
      console.log(error);
      this.route.navigate(["error", {message:error["error"].Message, status:error["status"]}])
    });
  }
}
