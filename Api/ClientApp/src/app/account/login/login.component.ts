import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{
  loginForm:FormGroup;
  returnUrl:string;

  constructor(private activatedRoute:ActivatedRoute, private fb: FormBuilder, private accountService:AccountService, private router:Router) {}

  ngOnInit(): void {
    this.returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '/shop';
    this.createLoginForm();
  }

  createLoginForm() {
    this.loginForm = this.fb.group(
    {
      email : ['', Validators.required],
      password : ['', Validators.required],
    });
  }

  submitForm()
  {
    this.accountService.login(this.loginForm.value).subscribe(()=>
    {
      this.router.navigateByUrl(this.returnUrl);
      console.log("login successfully.")
    });
  }
}

