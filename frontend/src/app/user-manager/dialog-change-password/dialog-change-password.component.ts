import { Component,Inject, OnInit } from '@angular/core';
import {NgForm} from '@angular/forms';
import {UserManagerService} from '../user-manager.service';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {HttpClient} from '@angular/common/http';
import {AuthService} from '../../auth/auth.service';

@Component({
  selector: 'app-dialog-change-password',
  templateUrl: './dialog-change-password.component.html',
  styleUrls: ['./dialog-change-password.component.css']
})
export class DialogChangePasswordComponent implements OnInit {

  constructor( private userManagerService: UserManagerService, private dialogRef: MatDialogRef<DialogChangePasswordComponent>,
               private http: HttpClient, private authService: AuthService, @Inject(MAT_DIALOG_DATA) private data: any) { }

  ngOnInit() {
  }

  disabledCheck(form: NgForm) {

    if (!(form.value.newPassword === form.value.confirmPassword && form.value.newPassword != undefined &&
        form.value.newPassword != undefined && form.value.newPassword.length >= 6)) {
      return true;
    }else {
      return false;
    }
}

  onSave(form: NgForm) {

    const body: any = {};
    body.NewPassword = form.value.newPassword;
    body.ConfirmPassword = form.value.confirmPassword;
    body.user = this.data.userId;
    this.userManagerService.changePasswordWithoutOld(body)
      .subscribe((data) => {
        console.log(data);
        this.dialogRef.close(data);
      });

  }

  onCancel() {
  }
}
