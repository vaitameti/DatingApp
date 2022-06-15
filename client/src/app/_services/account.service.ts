import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import {map} from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';

@Injectable({ //injectable is a decorator which means this service can be injected into other components or other services in our app
  providedIn: 'root' //services are singleton(it will run always from start to finish of app)
})
export class AccountService {
  baseUrl = environment.apiUrl; 

  private currentUserSource = new ReplaySubject<User>(1) //we are interested with only one value because we need to store wither null or the current user object,replay subject is also a kind of an observable
  currentUser$ = this.currentUserSource.asObservable(); //by convention we give dollar sign at the end because it is an observable

  constructor(private http: HttpClient) { } //injecting httpclient

  login(model:User){
    return this.http.post(this.baseUrl + 'account/login', model).pipe( //anything inside pipe method will be rxJS operator
      map((response: any)=>{
        const user = response;
        if(user){
          localStorage.setItem('user', JSON.stringify(user)); 
          this.currentUserSource.next(user);
        }
      })
    )
  }

  register(model: any){
    return this.http.post(this.baseUrl + 'account/register', model).pipe(
      map((user: User)=>{
        if(user){
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })

    )
  }

  setCurrentUser(user: User){
    this.currentUserSource.next(user);
  }

  logout(){
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}
