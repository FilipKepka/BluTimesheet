import { Component, Inject, OnInit } from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {HttpClient} from '@angular/common/http';
import {AuthService} from '../../auth/auth.service';
import {RoleMenagerService} from '../role-menager.service';
import {NgForm} from '@angular/forms';

@Component({
  selector: 'app-dialog-role-edit',
  templateUrl: './dialog-role-edit.component.html',
  styleUrls: ['./dialog-role-edit.component.css']
})
export class DialogRoleEditComponent implements OnInit {

  roleName: string;
  roleId: number;

  constructor( private dialogRef: MatDialogRef<DialogRoleEditComponent> , private http: HttpClient, private authService: AuthService,
    private roleManagerService: RoleMenagerService, @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
    if (this.data) {
      this.roleManagerService.loadSingleRole(this.data.roleId)
        .subscribe((res: any) => {
          this.roleId = res.roleId;
          this.roleName = res.roleName;
        });
    }
  }
  onSave(form: NgForm) {
    if (this.data){
      form.value.roleId = this.data.roleId;
      this.roleManagerService.editColumn(form.value)
        .subscribe((data) => {
          this.dialogRef.close(data);
        });
    }
  }

  onCancel() {
  }

}
