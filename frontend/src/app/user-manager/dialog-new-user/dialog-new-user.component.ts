import { Component, Inject, OnInit } from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {HttpClient} from '@angular/common/http';
import {AuthService} from '../../auth/auth.service';
import {UserManagerService} from '../user-manager.service';
import {NgForm} from '@angular/forms';
import {UserInfoModel} from '../../../models/userInfo.model';

@Component({
  selector: 'app-dialog-new-user',
  templateUrl: './dialog-new-user.component.html',
  styleUrls: ['./dialog-new-user.component.css']
})
export class DialogNewUserComponent implements OnInit {

  email: string;
  password: string;
  confirmPassword: string;
  firstName: string;
  lastName: string;
  superiorId: string;
  role: string;
  dataSource;
  constructor( private dialogRef: MatDialogRef<DialogNewUserComponent>, private http: HttpClient, private authService: AuthService,
               private userManagerService: UserManagerService, @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
    this.userManagerService.loadAllUsers()
      .subscribe((res: UserInfoModel) => {
        this.dataSource = res;
      });
  }


  onSave(form: NgForm) {
      form.value.role = [form.value.role];
      this.userManagerService.addNewUser(form.value)
        .subscribe((data) => {
          this.dialogRef.close(data);
        });

  }

  newUserValidationPassword(form: NgForm) {
    console.log('formvalid', form.valid);
    if (!(form.value.password === form.value.confirmPassword && form.valid)) {
      return true;
    }else {
      return false;
    }
  }

  onCancel() {
  }

}
