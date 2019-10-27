import { Component, OnInit } from '@angular/core';
import { PublicationService } from '../shared/services/publication.service';
import { Router, ActivatedRoute } from '@angular/router';
import { PublicationModel } from '../shared/Models/publication.model';

@Component({
  selector: 'app-publication-edit',
  templateUrl: './publication-edit.component.html',
  styleUrls: ['./publication-edit.component.css']
})
export class PublicationEditComponent implements OnInit {

  constructor(private service:PublicationService, private route:Router, private currentRoute:ActivatedRoute) { }

  publication:PublicationModel=new PublicationModel();

  ngOnInit() {

  }

  add(){
    if(this.publication.header==undefined||this.publication.header==""
      ||this.publication.content==undefined||this.publication.content==""){
        alert("fill all gaps");
      }
    else{
    this.publication.id=+this.currentRoute.snapshot.paramMap.get("id");
    this.service.editPublication(this.publication).subscribe(()=>{})
    this.route.navigate(["profile"]);
    }
  }
  cancel(){
  this.route.navigate(["profile"]);
  }
}
