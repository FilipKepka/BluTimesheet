import {Component, OnInit} from '@angular/core';
import {UserManagerService} from './user-manager.service';
import {MatDialog, MatDialogRef, MatTableDataSource} from '@angular/material';
import {UserInfoModel} from '../../models/userInfo.model';
import {DialogNewUserComponent} from './dialog-new-user/dialog-new-user.component';
import {forEach} from '@angular/router/src/utils/collection';
import {ConfirmationDialogComponent} from '../confirmation-dialog/confirmation-dialog.component';
import {Observable} from 'rxjs/Observable';
import {DialogEditUserComponent} from './dialog-edit-user/dialog-edit-user.component';
import {AuthService} from '../auth/auth.service';
import {DialogChangePasswordComponent} from './dialog-change-password/dialog-change-password.component';

@Component({
  selector: 'app-user-manager',
  templateUrl: './user-manager.component.html',
  styleUrls: ['./user-manager.component.css']
})
export class UserManagerComponent implements OnInit {

  private dataSource: MatTableDataSource<UserInfoModel>;
  private displayedColumns: Array<string> = ['FirstName', 'Surname', 'E-mail', 'Role', 'Superior', 'settings'];
  data;
  dialogRef: MatDialogRef<ConfirmationDialogComponent>;

  constructor(private userManagerService: UserManagerService, private dialog: MatDialog,
              private authService: AuthService) {
  }

  ngOnInit() {
    this.userManagerService.loadAllUsers()
      .subscribe((res: UserInfoModel) => {
        this.data = res;
        this.dataSource = new MatTableDataSource<UserInfoModel>(this.data);
      });
  }

  addNewUser() {
    const dialogRef = this.dialog.open(DialogNewUserComponent, {
      width: '500px',
    });
    dialogRef.afterClosed()
      .subscribe((data: UserInfoModel) => {
        this.refreshTableData();
      });
  }

  findUserBySuperior(superiorIdToSurname) {
    let returnValue = '';
    this.data.forEach((e, i) => {
      if (e.userId === superiorIdToSurname) {
        returnValue = e.lastName;
      }
    });
    return returnValue;
  }

  removeUser(userId) {
    this.dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      disableClose: false
    });
    this.dialogRef.componentInstance.confirmMessage = 'Are you sure you want to delete?';

    this.dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.userManagerService.removeUser(userId)
          .subscribe((res: any) => {
            this.refreshTableData();
          });
      }
      this.dialogRef = null;
    });
  }

  editUser(userId) {
    const dialogRef = this.dialog.open(DialogEditUserComponent, {
      width: '500px',

      data: {
        userId
      }
    });
    dialogRef.afterClosed()
      .subscribe((data: UserInfoModel) => {
        this.refreshTableData();
      });
  }

  refreshTableData() {
    this.userManagerService.loadAllUsers()
      .subscribe((res: UserInfoModel) => {
        this.data = res;
        this.dataSource = new MatTableDataSource<UserInfoModel>(this.data);
      });
  }

  changePassword(userId) {
  const dialogRef = this.dialog.open(DialogChangePasswordComponent, {
    width: '500px',
    data: {
      userId
    }
  });
  dialogRef.afterClosed()
    .subscribe(data => {

    });
  }
}
