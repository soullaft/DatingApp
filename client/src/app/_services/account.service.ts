import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from 'src/app/_models/user';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  //base url to our server
  baseUrl : string = environment.apiUrl
  //collection of current user. 1 max
  private currentUserSource = new ReplaySubject<User>(1);
  //observable currentUserSource
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http:HttpClient) { }

  //login user
  login(model: any) {
    return this.http.post(this.baseUrl + 'account/login', model).pipe(
      map((response: any) => {
        const user = response;
        if(user) {
          this.setCurrentUser(user);
        }
      })
    )
  }

  //register new user
  register(model: any) {
    return this.http.post(this.baseUrl + 'account/register', model).pipe(
      map((user: any) => {
        if (user) {
          this.setCurrentUser(user);
        }
      })
    )
  }

  //set user to currentUser observable
  setCurrentUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  //logout user
  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}
