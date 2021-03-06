import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AuthService } from 'app/core/services/auth.service';
import { AccountService } from '../../account.service';
import { UserRegistration } from 'app/core/models/UserRegistration';
import { NotificationService } from 'app/core/services/notification.service';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.scss']

})
export class AccountComponent implements OnInit {

  accountForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService,
    private notificationService: NotificationService
  ) {
  }

  ngOnInit() {
    this.accountForm = this.formBuilder.group({
        email: ['', Validators.required],
        password: ['', Validators.required],
        passwordConfirm: ['', Validators.required],
    });

    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  // getter for easy access to form fields
  get f() {
    return this.accountForm.controls;
  }

  onSubmit() {

    this.submitted = true;

    // stop here if form is invalid
    if (this.accountForm.invalid) {
        return;
    }

    this.loading = true;
    const userRegistration = new UserRegistration(this.f.email.value,
      this.f.password.value,
      this.f.passwordConfirm.value);

    this.accountService.registerAccount(userRegistration)
    .pipe(first())
    .subscribe(
      data => {
        this.notificationService.showSuccess("Account successfully created!");
        this.router.navigate([this.returnUrl]);
      },
      error => {
          this.loading = false;
      });
  }
}
