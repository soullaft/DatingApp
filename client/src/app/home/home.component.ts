import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode: boolean = false;
  users: any;

  constructor(private httpClient: HttpClient) { }

  ngOnInit(): void {
    this.getUsers()
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  getUsers() {
    this.httpClient.get('https://localhost:7116/api/users').subscribe({
      next: response => this.users = response,
      error: e => console.log(e)
    });
  }
  cancelRegisterMode(event: boolean) {
    this.registerMode = event; 
  }
}
