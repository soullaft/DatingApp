import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'The Dating app';
  users: any;

  constructor(private http: HttpClient) { }
  
  ngOnInit(): void {
    this.http.get("https://localhost:7116/api/users").subscribe({
      next: response => this.users = response,
      error: e => console.log(e)
    })
  }

  getUsers(): void {
    this.http.get("https://localhost:7116/api/users").subscribe({
      next: response => console.log(response),
      error: e => console.log(e)
    })
  }
}
