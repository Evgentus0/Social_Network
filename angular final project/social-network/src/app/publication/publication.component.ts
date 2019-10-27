import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { User } from '../shared/models/user.model';
import { UserService } from '../shared/services/user.service';
import { Router } from '@angular/router';
import { PublicationService } from '../shared/services/publication.service';
import { PublicationModel } from '../shared/Models/publication.model';

@Component({
  selector: 'app-publication',
  templateUrl: './publication.component.html',
  styleUrls: ['./publication.component.css']
})
export class PublicationComponent implements OnInit {

  constructor(private service:PublicationService, private route:Router) { }
  @Input() userId;
  id:string;
  publications:PublicationModel[];
  ngOnInit() {
    this.publications=[];
    this.id=this.userId;
    this.service.getHomePublications(this.id).subscribe((data:PublicationModel[])=>{
      data.forEach(element => {
        let users:string[]=[];
        element["UsersWhoLike"].forEach(el => {
          users.push(el["Id"])
        });
        this.publications.push({
          id:element["Id"],
          authorName:element["Author"].Name,
          authorId:element["Author"].Id,
          content:element["Content"],
          date:element["DateOfCreate"],
          header:element["Header"],
          userWhoLike:users
        })
      });
    },
    error=>{
      
    });
  }
  isLiked(publication:PublicationModel){
    return publication.userWhoLike.filter(val=>val==sessionStorage.getItem("userName")).length>0;
  }

  like(id:number){
    this.service.likePublication(id).subscribe(()=>{
      window.location.reload();
    }
    ,error=>{
      console.log(error);
      this.route.navigate(["error", {message:error["error"].Message, status:error["status"]}])
    });
  }

  delete(id:number, author:string){
    if(sessionStorage.getItem("userName")!=author){
      alert("You are not author");
    }
    else{
    this.service.deletePublication(id).subscribe(()=>{
      window.location.reload();
    }
    ,error=>{
      console.log(error);
      this.route.navigate(["error", {message:error["error"].Message, status:error["status"]}])
    });
    }
  }

  edit(id:number, author:string){
    if(sessionStorage.getItem("userName")!=author){
      alert("You are not author");
    }
    else{
    this.route.navigate(["publication-edit", id]);
    }
  }
}
