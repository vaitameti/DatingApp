import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({ //this is a decorator in typescript language(i mean @component)-decorator means it gives extra power to 
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{ //we are using oninit so we can use life cycle event after constructor
  title = 'The Dating App'; //this is a property
  users: any; //this means user can be string int etc(we turn off type safety in typescript with this)

  constructor(private accountService: AccountService) {}
  ngOnInit() {
    
    this.setCurrentUser();
  }

  setCurrentUser(){
    const user: User = JSON.parse(localStorage.getItem('user'));
    this.accountService.setCurrentUser(user);
  }

 
}

//subscribe is made because observebales are lazy on itself,
