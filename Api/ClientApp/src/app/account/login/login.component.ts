import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{
  loginForm:FormGroup;

  constructor(private fb: FormBuilder, private accountService:AccountService, private router:Router) {}

  ngOnInit(): void {
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
      this.router.navigateByUrl('/shop')
      console.log("login successfully.")
    });
  }
}

