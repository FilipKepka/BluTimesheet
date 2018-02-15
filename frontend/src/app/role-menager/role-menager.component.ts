import { Component, OnInit } from '@angular/core';
import { MatDialog, MatTableDataSource, MatIcon, MatIconModule, MatDialogRef } from '@angular/material';
import { RoleMenagerModel } from '../../models/role-menager.model';
import { Observable } from 'rxjs/Observable';
import { RoleMenagerService } from './role-menager.service';
import {NgForm} from '@angular/forms';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import {DialogEditComponent} from '../project-menager/dialog-edit/dialog-edit.component';
import {DialogRoleEditComponent} from './dialog-role-edit/dialog-role-edit.component';

@Component({
  selector: 'app-role-menager',
  templateUrl: './role-menager.component.html',
  styleUrls: ['./role-menager.component.css']
})
export class RoleMenagerComponent implements OnInit {

  private roleMenager$: Observable<RoleMenagerModel[]>;
  private dataSource: MatTableDataSource<RoleMenagerModel>;
  private displayedColumns: Array<string> = ['id', 'name', 'settings'];
  dialogRef: MatDialogRef<ConfirmationDialogComponent>;
  data;

  constructor( private roleManagerService: RoleMenagerService, private dialog: MatDialog ) { }

  ngOnInit() {
    this.roleMenager$ = this.roleManagerService.roleMenager$;
    this.roleManagerService.loadAllRole();
    this.roleMenager$
      .subscribe((data: RoleMenagerModel[]) => {
        this.data = data;
        this.dataSource = new MatTableDataSource<RoleMenagerModel>(data);
      });
  }

  addNewRole(form: NgForm) {
    const body: any = {};
    body.RoleName = form.value.addNewRoleManager;
    this.roleManagerService.addRole(body)
      .subscribe((res: RoleMenagerModel) => {
        this.data.push({id: res.roleId, roleName: res.roleName});
        this.dataSource = new MatTableDataSource<RoleMenagerModel>(this.data);
      });
    form.reset();
  }


  removeRole(id) {
    this.dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      disableClose: false
    });
    this.dialogRef.componentInstance.confirmMessage = "Are you sure you want to delete?"

    this.dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.roleManagerService.deleteColumn(id);
      }
      this.dialogRef = null;
    });
  }

  editRole(roleId) {
      const dialogRef = this.dialog.open(DialogRoleEditComponent, {
      width: '300px',
      data: {
        roleId,
      }
    });
    dialogRef.afterClosed()
      .subscribe((res: RoleMenagerModel) => {
      this.data.forEach((e, i) => {
        if(e.roleId === res.roleId) {
          this.data[i] = res;
        }
      });
      this.dataSource = new MatTableDataSource<RoleMenagerModel>(this.data);
      });

  }



  }


