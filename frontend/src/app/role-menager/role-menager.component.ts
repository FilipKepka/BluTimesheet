import { Component, OnInit } from '@angular/core';
import { MatDialog, MatTableDataSource, MatIcon, MatIconModule, MatDialogRef } from '@angular/material';
import { RoleMenagerModel } from '../../models/role-menager.model';
import { Observable } from 'rxjs/Observable';
import { RoleMenagerService } from './role-menager.service';
import {NgForm} from '@angular/forms';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';

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

  constructor( private roleMenagerService: RoleMenagerService, private dialog: MatDialog ) { }

  ngOnInit() {
    this.roleMenager$ = this.roleMenagerService.roleMenager$;
    this.roleMenagerService.loadAllRole();
    this.roleMenager$
      .subscribe((data: RoleMenagerModel[]) => {
        this.data = data;
        this.dataSource = new MatTableDataSource<RoleMenagerModel>(data);
      });
  }

  addNewRole(form: NgForm) {
    const body: any = {};
    body.RoleName = form.value.addNewRoleManager;
    this.roleMenagerService.addRole(body)
      .subscribe((res: RoleMenagerModel) => {
        console.log("log",res);
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
        this.roleMenagerService.deleteColumn(id);
      }
      this.dialogRef = null;
    });
  }



  }


