import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({ //this is a decorator in typescript language(i mean @component)-decorator means it gives extra power to 
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{ //we are using oninit so we can use life cycle event after constructor
  title = 'The Dating App'; //this is a property
  users: any; //this means user can be string int etc(we turn off type safety in typescript with this)

  constructor(private http: HttpClient) {}
  ngOnInit() {
    this.getUsers()
  }

  getUsers(){
    this.http.get('https://localhost:5001/api/users').subscribe(response => {
      this.users = response;
    }, error =>{
      console.log(error);
    })
  }
}

//subscribe is made because observebales are lazy on itself,
