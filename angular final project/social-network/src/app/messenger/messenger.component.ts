import { Component, OnInit } from '@angular/core';
import { MessageHeaderService } from '../shared/services/message-header.service';
import { Router } from '@angular/router';
import { MessageHeaderModel } from '../shared/models/messageHeader.model';

@Component({
  selector: 'app-messenger',
  templateUrl: './messenger.component.html',
  styleUrls: ['./messenger.component.css']
})
export class MessengerComponent implements OnInit {

  constructor(private service:MessageHeaderService, private route:Router) { }

  messageHeaders:MessageHeaderModel[];

  ngOnInit() {
    this.messageHeaders=[];
    this.service.getMessageHeaders().subscribe((data:MessageHeaderModel[])=>{
      data.forEach(element=>{
        this.messageHeaders.push({
          id:element["Id"],
          createDate:element["CreateDate"],
          header:element["Header"],
          isRead:element["IsRead"],
          type: element["Type"].Name,
          typeId:element["Type"].Id,
        })
      });
      console.log(data);
    },
    error=>{
      console.log(error);
      this.route.navigate(["error", {message:error["error"].Message, status:error["status"]}])
    });
  }

  toCorrespondence(id:number){
    this.route.navigate(["correspondence", id]);
  }

  addMessanger(){
    this.route.navigate(["add-messanger"]);
  }

  deleteForOne(id:number){
    if(confirm("Do you really want to delete this messsanger?")) {
    this.service.deleteForOne(id).subscribe(()=>{});
    }
  }

  delete(id:number){
    if(confirm("Do you really want to delete this messanger for all?")) {
    this.service.deleteMessageHeader(id).subscribe(()=>{});
    }
  }
}
