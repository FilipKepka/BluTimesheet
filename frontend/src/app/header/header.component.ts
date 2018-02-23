import { Component, OnInit } from '@angular/core';
import { AuthService} from '../auth/auth.service';
import { Router } from '@angular/router';
import {NgForm} from '@angular/forms';
import {DialogChangePasswordComponent} from '../user-manager/dialog-change-password/dialog-change-password.component';
import {MatDialog} from '@angular/material';
import {DialogSingleUserPaswordChangeComponent} from '../user-manager/dialog-single-user-pasword-change/dialog-single-user-pasword-change.component';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor( private authService: AuthService, private router: Router, private dialog: MatDialog) {
  }
  title = 'BluTimesheet';


  onLogout() {
    console.log('Log Out from system');
    this.authService.logout();
  }
  ngOnInit() {
  }

  changePassword() {
    const dialogRef = this.dialog.open(DialogSingleUserPaswordChangeComponent, {
      width: '500px',
    });
    dialogRef.afterClosed()
      .subscribe(data => {

      });
  }

}
