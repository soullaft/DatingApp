import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, AbstractControlOptions, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};
  registerForm: FormGroup;

  constructor(private accountService: AccountService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.initializeForm()
  }

  initializeForm() {
    this.registerForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', [Validators.minLength(4), Validators.maxLength(12), Validators.required]),
      confirmPassword: new FormControl('', [Validators.required, this.matchValues('password')])
    })
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
        return control?.value === control?.parent?.controls[matchTo].value ? null : {isMatching: true}
    }
}

  register() {
    console.log(this.registerForm.valid);
    // // this.accountService.register(this.model).subscribe({
    // //   next: resp => {
    // //     console.log(resp);
    // //     this.cancel()
    // //   },
    // //   error: e => {
    // //     console.log(e);
    // //     this.toastr.error(e); 
    // //   }
    // // });
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
