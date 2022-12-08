import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { CustomValidatorsService } from '../services/custom-validators.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup | any = null;
  constructor(private formBuilder: FormBuilder,private toast: ToastrService,private httpClient: HttpClient,
    private router: Router, private customValidatorService : CustomValidatorsService) { }

  ngOnInit(): void {
    this.createForm();
  }

  register() {
    //this.registerForm["submitted"] = true;
    console.log(this.registerForm.value);
    this.httpClient.post(environment.serverUrl+'account/RegisterUser', this.registerForm.value).subscribe( (res:any)  => {
      console.log(res);
      this.toast.success('Registered Successfully!!!');
      this.router.navigate(['/login'], { replaceUrl: true });
    });
  }


  private createForm() {
      this.registerForm = this.formBuilder.group(
        {
          empid:[null,[Validators.required]],
          firstname: [null, [Validators.required]],
          lastname: [null, [Validators.required]],
          userType: [null, [Validators.required]],
          email: [null, [Validators.required]],
          password: [null, [Validators.required]],
          confirmPassword: [null, [Validators.required]],
          phoneNumber: [null, [Validators.required]],
          empAddress: [null, [Validators.required]],
        },
        {
          validators:[this.customValidatorService.compareValidator("confirmPassword","password")]
        }
      )
  }

}
