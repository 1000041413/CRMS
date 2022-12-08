import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from '../core/authentication/authentication.service';
import { environment } from '../../environments/environment';
import { finalize } from 'rxjs/operators';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})


export class LoginComponent implements OnInit {
  loginForm: FormGroup | any = null;
  isLoading = false;
  error: string = '';
  version: string = environment.version;

  constructor(private router: Router, private formBuilder: FormBuilder, private authenticationService: AuthenticationService) {
    this.createForm();
  }

  ngOnInit(): void {
  }

  login() {
    this.isLoading = true;  
    this.authenticationService.login(this.loginForm.value)
      .pipe(finalize(() => {
        this.loginForm.markAsPristine();
        this.isLoading = false;
      }))
      .subscribe((credentials: any) => {
        // log.debug(`${credentials.username} successfully logged in`);
        this.router.navigate(['/dashboard'], { replaceUrl: true });
      }, (error: any) => {
        // log.debug(`Login error: ${error}`);
        this.error = error;
      });
  }

  private createForm() {
    this.loginForm = this.formBuilder.group({
      username: ['', [Validators.required]],
      password: [''],
      remember: false
    });
  }

}
