import { Component, Inject, OnInit } from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {HttpClient} from '@angular/common/http';
import {AuthService} from '../../auth/auth.service';
import {ReportGenerateService} from '../report-generate.service';
import {ProjectTypeModel} from '../../../models/projectType.model';
import {ProjectsModel} from '../../../models/projects.model';
import {NgForm} from '@angular/forms';
import {RoleMenagerModel} from '../../../models/role-menager.model';
import * as moment from 'moment';

@Component({
  selector: 'app-dialog-edit-report',
  templateUrl: './dialog-edit-report.component.html',
  styleUrls: ['./dialog-edit-report.component.css']
})
export class DialogEditReportComponent implements OnInit {

  activityId: number;
  roleId: number;
  projectId: number;
  reportId: number;
  beginning: string;
  description: string;
  howManyHours: number;
  approvedByManager: boolean;
  thisReportUserId: string;
  allProjectRoleTypes: RoleMenagerModel[];
  allProjects: ProjectsModel;
  allActivityType: any[];
  Hours: any[];

  constructor(private dialogRef: MatDialogRef<DialogEditReportComponent>,
              private http: HttpClient,
              private authService: AuthService,
              private reportGenerateService: ReportGenerateService,
              @Inject(MAT_DIALOG_DATA) public data: any) {
    this.Hours = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 ];
  }

  ngOnInit() {
    if (this.data) {
      this.reportGenerateService.loadSingleReport(this.data.reportId)
        .subscribe((res: any) => {
          this.activityId = res.activityType.activityId;
          this.roleId = res.currentProjectRoleType.roleId;
          this.projectId = res.project.projectId;
          this.reportId = res.id;
          this.beginning = res.begining;
          this.description = res.description;
          this.howManyHours = res.howManyHours;
          this.approvedByManager = res.approvedByManager;
          this.thisReportUserId = res.userId;
        });
    }

    this.reportGenerateService.loadAllProject()
      .subscribe((res: any) => {
        this.allProjects = res;
      });

    this.reportGenerateService.loadAllProjectRole()
      .subscribe((res: any) => {
        this.allProjectRoleTypes = res;
      });

    this.reportGenerateService.loadAllActivityType()
      .subscribe((res: any) => {
        this.allActivityType = res;
      });
  }


  onSave(form: NgForm) {
   if (this.data) {
      const body: any = {};
      body.id = this.reportId;
      body.begining = moment(form.value.beginning.toString()).format("YYYY-M-D");
      body.description = form.value.description;
      body.howManyHours = form.value.howManyHours;
      body.approvedByManager = form.value.approvedByManager;
      body.activityType = { activityId: form.value.activityId};
      body.currentProjectRoleType = { roleId: form.value.roleId};
      body.project = { projectId: form.value.projectId};
      body.userId = this.thisReportUserId;
      this.reportGenerateService.editReport(body)
        .subscribe((data) => {
          this.dialogRef.close(data);
        });
    }
  }

  onDelete() {
    this.reportGenerateService.removeReport(this.reportId)
      .subscribe((data) => {

        this.dialogRef.close("remove");
      });
  }

  onCancel() {
  }


}
