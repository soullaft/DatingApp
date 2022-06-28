import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { AccountService } from 'src/_services/account.service';
import { User } from '../_models/user';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(public accountService : AccountService,
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit(): void {
  }
  
  login() {
    this.accountService.login(this.model).subscribe({
      next: response => {
        this.router.navigateByUrl('/members');
      },
      error: error => {
        console.log(error);
        this.toastr.error(error.error);
      }
    });
  }

  logout() {
    this.model.username = '';
    this.model.password = '';
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
