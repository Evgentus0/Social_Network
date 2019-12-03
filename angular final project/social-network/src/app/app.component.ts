import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {switchMap, debounceTime, tap, finalize} from 'rxjs/operators';
import {Observable} from 'rxjs'
import { UserSearch } from './shared/models/search-user.model';
import { UserSerchService } from './shared/services/search-user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'social-network';
  email:string="Email";
  userName=sessionStorage.getItem("userName");

  filteredUsers: UserSearch[] = [];
  usersForm: FormGroup;
  isLoading = false;

  constructor(private fb: FormBuilder, private appService: UserSerchService) {}

  ngOnInit() {
    this.isLoading=false;
    this.usersForm = this.fb.group({
      userInput: null
    })

      this.usersForm
      .get("userInput")
      .valueChanges
      .pipe(
        debounceTime(300),
        tap(() => this.isLoading = false),
        switchMap(value => this.appService.search({name: value}, 1)
        .pipe(
          finalize(() => this.isLoading = false),
          )
        )
      )
      .subscribe(users => {this.filteredUsers = users.results; console.log(this.filteredUsers)});
  }

  displayFn(user: UserSearch) {
    if (user) { return user.name; }
  }
}
