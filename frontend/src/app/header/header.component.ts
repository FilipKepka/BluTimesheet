import { Component, OnInit } from '@angular/core';
import { AuthService} from '../auth/auth.service';
import { Router } from '@angular/router';
import {NgForm} from '@angular/forms';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor( private authService: AuthService, private router: Router) {
  }
  title = 'BluTimesheet';


  onLogout() {
    this.authService.logout();
  }
  ngOnInit() {
  }

}
