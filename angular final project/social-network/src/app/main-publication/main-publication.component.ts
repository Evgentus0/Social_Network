import { Component, OnInit, Input } from '@angular/core';
import { PublicationModel } from '../shared/Models/publication.model';
import { PublicationService } from '../shared/services/publication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-main-publication',
  templateUrl: './main-publication.component.html',
  styleUrls: ['./main-publication.component.css']
})
export class MainPublicationComponent implements OnInit {

  constructor(private service:PublicationService, private route:Router) { }
  @Input() userId;
  id:string;
  publications:PublicationModel[];
  ngOnInit() {
    this.publications=[];
    this.id=this.userId;
    this.service.getMainPublications().subscribe((data:PublicationModel[])=>{
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
      console.log(error);
      this.route.navigate(["error", {message:error["error"].Message, status:error["status"]}])
    });
  }
  isLiked(publication:PublicationModel){
    return publication.userWhoLike.filter(val=>val==sessionStorage.getItem("userName")).length>0;
  }

  like(id:number){
    this.service.likePublication(id).subscribe(()=>{});
  }

  delete(id:number, author:string){
    if(sessionStorage.getItem("userName")!=author){
      alert("You are not author");
    }
    else{
    this.service.deletePublication(id).subscribe(()=>{});
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
