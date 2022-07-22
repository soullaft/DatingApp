import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-test-errors',
  templateUrl: './test-errors.component.html',
  styleUrls: ['./test-errors.component.css']
})
export class TestErrorsComponent implements OnInit {
  baseUrl = "https://localhost:7116/api/"
  validationErrors: string[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  get404Error() {
    this.http.get(this.baseUrl + "Buggy/not-found").subscribe({
      next: resp => {
        console.log(resp)
      },
      error: e => {
        console.log(e)
      }
    })
  }

  getServerError() {
    this.http.get(this.baseUrl + "Buggy/server-error").subscribe({
      next: resp => {
        console.log(resp)
      },
      error: e => {
        console.log(e)
      }
    })
  }

  getValidationErros() {
    this.http.post(this.baseUrl + "account/register", {}).subscribe({
      next: resp => {
        console.log(resp)
      },
      error: e => {
        console.log(e)
        this.validationErrors = e;
      }
    })
  }
}
