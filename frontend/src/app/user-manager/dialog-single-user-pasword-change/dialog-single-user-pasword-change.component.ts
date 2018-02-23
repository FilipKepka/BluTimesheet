import {Component, Inject, OnInit} from '@angular/core';
import {NgForm} from '@angular/forms';
import {HttpClient} from '@angular/common/http';
import {DialogChangePasswordComponent} from '../dialog-change-password/dialog-change-password.component';
import {AuthService} from '../../auth/auth.service';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {UserManagerService} from '../user-manager.service';

@Component({
  selector: 'app-dialog-single-user-pasword-change',
  templateUrl: './dialog-single-user-pasword-change.component.html',
  styleUrls: ['./dialog-single-user-pasword-change.component.css']
})
export class DialogSingleUserPaswordChangeComponent implements OnInit {



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
    body.OldPassword = form.value.oldPassword;
    body.user = sessionStorage.getItem('UserId');
    this.userManagerService.changePassword(body)
      .subscribe((data) => {
        console.log(data);
        this.dialogRef.close(data);
      });

  }

  onCancel() {
  }

}
