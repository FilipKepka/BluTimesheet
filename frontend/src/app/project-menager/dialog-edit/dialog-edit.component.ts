import {Component, Inject, OnInit} from '@angular/core';
import {NgForm} from '@angular/forms';
import {ProjectTypeModel} from '../../../models/projectType.model';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {HttpClient} from '@angular/common/http';
import {AuthService} from '../../auth/auth.service';
import {ProjectMenagerService} from '../project-menager.service';

@Component({
  selector: 'app-dialog-edit',
  templateUrl: './dialog-edit.component.html',
  styleUrls: ['./dialog-edit.component.css']
})
export class DialogEditComponent implements OnInit {

  name: string;
  projectType: ProjectTypeModel;
  projectTypeName: string;
  projectnumber: string;
  projectId: number;
  projectTypeId: number;
  client: string;
  allProjectTypes: ProjectTypeModel[];
  constructor(private dialogRef: MatDialogRef<DialogEditComponent>,
              private http: HttpClient,
              private authService: AuthService,
              private projectMenagerService: ProjectMenagerService,
              @Inject(MAT_DIALOG_DATA) public data: any) {
  }

  ngOnInit() {
    if (this.data) {
      this.projectMenagerService.loadSingleProject(this.data.projectId)
        .subscribe((res: any) => {
          this.name = res.name;
          this.projectType = res.projectType;
          this.projectnumber = res.projectnumber;
          this.projectId = res.projectId;
          this.projectTypeId = res.projectTypeId;
          this.projectTypeName = res.projectType.projectTypeId;
          this.client = res.client;
        });
    }

    this.projectMenagerService.loadAllProjectTypes()
      .subscribe((res: any) => {
        this.allProjectTypes = res;
      });
  }

  onSave(form: NgForm) {
    if (this.data) {
      const body: any = {};
      body.name = form.value.name;
      body.projectnumber = form.value.projectnumber;
      body.projectId = this.data.projectId;
      body.ProjectType = { projectTypeId: form.value.projectTypeName};
      body.client = form.value.client;
      this.projectMenagerService.editColumn(body)
        .subscribe((data) => {
          this.dialogRef.close(data);
        });
    }
  }

  onCancel() {
  }
}
