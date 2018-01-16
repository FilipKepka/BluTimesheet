import { Component, OnInit } from '@angular/core';
import {NgForm} from '@angular/forms';

import { AuthService } from '../auth.service';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SigninComponent implements OnInit {

  constructor( private authService: AuthService) { }

  onSignIn(form: NgForm) {
    const login = form.value.login;
    const password = form.value.password;
    this.authService.signInUser(login, password);
  }
  logout() {
    this.authService.logout();
  }

  ngOnInit() {
  }

}
