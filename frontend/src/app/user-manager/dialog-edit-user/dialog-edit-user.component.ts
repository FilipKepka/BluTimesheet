import {Component, Inject, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {UserManagerService} from '../user-manager.service';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {DialogNewUserComponent} from '../dialog-new-user/dialog-new-user.component';
import {AuthService} from '../../auth/auth.service';
import {UserInfoModel} from '../../../models/userInfo.model';
import {NgForm} from '@angular/forms';

@Component({
  selector: 'app-dialog-edit-user',
  templateUrl: './dialog-edit-user.component.html',
  styleUrls: ['./dialog-edit-user.component.css']
})
export class DialogEditUserComponent implements OnInit {

  email: string;
  firstName: string;
  lastName: string;
  superiorId: string;
  role: string;
  dataSourceSuperior;
  dataSource;

  constructor( private dialogRef: MatDialogRef<DialogNewUserComponent>, private http: HttpClient, private authService: AuthService,
               private userManagerService: UserManagerService, @Inject(MAT_DIALOG_DATA) public data: any) { }


  ngOnInit() {
    this.userManagerService.loadAllUsers()
      .subscribe((res: UserInfoModel) => {
        this.dataSourceSuperior = res;
      });

    this.userManagerService.loadSingleUser(this.data.userId)
      .subscribe((res: UserInfoModel) => {
        this.dataSource = res;
        this.email = res.email;
        this.firstName = res.fistName;
        this.lastName = res.lastName;
        this.superiorId = res.superiorId;
        this.role = res.role[0];
        console.log(this.superiorId);
      });
  }

  onSave(form: NgForm) {

    const body: any = {};
    body.role = [form.value.role];
    body.userId = this.dataSource.userId;
    body.email = form.value.email;
    body.firstName = form.value.firstName;
    body.lastName = form.value.lastName;
    body.superiorId = form.value.superiorId;
    this.userManagerService.editUser(body)
      .subscribe((data) => {
        this.dialogRef.close(data);
      });

  }

  onCancel() {
  }

}
