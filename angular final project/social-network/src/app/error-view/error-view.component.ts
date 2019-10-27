import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-error-view',
  templateUrl: './error-view.component.html',
  styleUrls: ['./error-view.component.css']
})
export class ErrorViewComponent implements OnInit {

  constructor(private route: ActivatedRoute) { }

  message:string="";
  status:string ="";

  ngOnInit() {
    this.message=this.route.snapshot.paramMap.get("message");
    this.status=this.route.snapshot.paramMap.get("status");
  }

}
