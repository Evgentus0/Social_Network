import { Component, OnInit, ɵɵcontainerRefreshEnd } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from '../shared/services/message.service';
import { UserService } from '../shared/services/user.service';
import { MessageModel } from '../shared/models/message.model';

@Component({
  selector: 'app-correspondence',
  templateUrl: './correspondence.component.html',
  styleUrls: ['./correspondence.component.css']
})
export class CorrespondenceComponent implements OnInit {

  constructor(private activateRoute: ActivatedRoute, private service:MessageService, private userService:UserService, private route:Router) { 
  }
  userId:string="";
  id:number;
  messages:MessageModel[];
  header:string="";
  headerId:number;
  inputValue:string;
  content:string;
  path:string="correspondence/";

  ngOnInit() {
    this.messages=[];
    this.headerId=this.activateRoute.snapshot.params["id"];
    this.path+=this.headerId;
    console.log(this.path);
    this.service.getMessageByMessageHeaderId(this.headerId).subscribe((data:MessageModel[])=>{
      data.forEach(element=>{
        this.messages.push({
          id:element["Id"],
          content:element["Content"],
          date:element["DateTimeSend"],
          isRead:element["IsRead"],
          messageHeaderId:element["MessageHeader"].Id,
          senderId:element["Sender"].Id
        })
        this.header=element["MessageHeader"].Header
      })
    })
    this.userId=sessionStorage.getItem("userName");

  }

  isAuthor(id:string):boolean{
    if(id==this.userId){
      return true;
    }
    else{
      return false;
    }
  }

  sendMessage(message:string){
    if(message==undefined||message==""){
      alert("Enter the message");
    }
    else{
    let model:MessageModel={
      content:message,
      date:null,
      id:null,
      isRead:null,
      messageHeaderId:this.headerId,
      senderId:this.userId
    };
    this.inputValue="";
    this.service.sendMessage(model).subscribe(()=>{

      window.location.reload();
    },
    error=>{
      console.log(error);
      this.route.navigate(["error", {message:error["error"].Message, status:error["status"]}])
    }); 
    }
  }


}
