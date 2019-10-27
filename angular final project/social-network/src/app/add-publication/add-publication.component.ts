import { Component, OnInit } from '@angular/core';
import { PublicationModel } from '../shared/Models/publication.model';
import { PublicationService } from '../shared/services/publication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-publication',
  templateUrl: './add-publication.component.html',
  styleUrls: ['./add-publication.component.css']
})
export class AddPublicationComponent implements OnInit {

  constructor(private service:PublicationService, private route:Router) { }

  publication:PublicationModel=new PublicationModel();

  ngOnInit() {

  }

  add(){
    if(this.publication.header==undefined||this.publication.header==""
      ||this.publication.content==undefined||this.publication.content==""){
        alert("fill all gaps");
      }
    else{
    this.service.createPublication(this.publication).subscribe(()=>{}, error=>{
      console.log(error);
      this.route.navigate(["error", {message:error["error"].Message, status:error["status"]}])
    });
    this.route.navigate(["profile"]);
    }
  }
  cancel(){
this.route.navigate(["profile"]);
  }
}
