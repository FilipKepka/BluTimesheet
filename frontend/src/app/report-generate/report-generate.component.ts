import {Component, OnInit, ViewChild, AfterViewInit, ViewChildren} from '@angular/core';
import {MatTableDataSource, MatSort, MatPaginator, MatDialog} from '@angular/material';
import {ReportGenerateService} from './report-generate.service';
import {AuthService} from '../auth/auth.service';
import {log} from 'util';
import {ActivityModel} from '../../models/activity.model';
import {RoleMenagerModel} from '../../models/role-menager.model';
import {Data} from '@angular/router';
import {NgForm} from '@angular/forms';
import * as moment from 'moment';
import {UserInfoModel} from '../../models/userInfo.model';
import {forEach} from '@angular/router/src/utils/collection';
import {ProjectsModel} from '../../models/projects.model';
import {UserHoursModel} from '../../models/userHours.model';
import {DialogEditReportComponent} from './dialog-edit-report/dialog-edit-report.component';
import {isLineBreak} from 'codelyzer/angular/sourceMappingVisitor';
import {Breakpoint, BreakResponse} from '_debugger';


@Component({
  selector: 'app-report-generate',
  templateUrl: './report-generate.component.html',
  styleUrls: ['./report-generate.component.css']
})
export class ReportGenerateComponent implements OnInit, AfterViewInit {
  displayedColumns = ['User', 'Data', 'Hours', 'ActivityType', 'Project', 'ProjectRole', 'ApprovedByManager'];
  displayedColumnsHours = ['Working', 'Sickness', 'Holidays', 'BankHolidays', 'Event', 'Other'];
  dataSource = new MatTableDataSource<ActivityModel>();
  userDataSource: UserInfoModel[];
  projectDataSource: ProjectsModel;
  dataSourcePerProject = new MatTableDataSource<ActivityModel>();
  dataReportSource: any;
  userHourData: number;
  timeFrom: any;

  userList: any[];
  dataProject;
  data;

  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('paginator') paginator: MatPaginator;
  @ViewChild('otherPaginator') otherPaginator: MatPaginator;

  constructor(private reportGenerateService: ReportGenerateService, private authService: AuthService,
              private dialog: MatDialog ) {
  }

  ngOnInit() {

    const body = {
      id: this.authService.getUserInfo().UserId,
      TimeTo: moment().format('YYYY-M-D'),
      TimeFrom: moment().day(-30).format('YYYY-M-D'),
    };
    this.reportGenerateService.loadAllReportForUse(body)
      .subscribe((res: ActivityModel) => {
        this.data = res;
        this.dataSource = new MatTableDataSource<ActivityModel>(this.data);
        this.ngAfterViewInit();
      });
    this.reportGenerateService.loadAllUsers()
      .subscribe((userRes: any) => {
        this.userDataSource = userRes;
      });

    this.reportGenerateService.loadAllProject()
      .subscribe((project: any) => {
        this.projectDataSource = project;
      });

  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.dataSourcePerProject.paginator = this.otherPaginator;
  //  this.dataSourcePerProject.sort = this.sort;
  }


  searchReportByDataAndUser(form: NgForm) {
    const body: any = {};
    body.TimeFrom = moment(form.value.timeFrom.toString()).format('YYYY-M-D');
    body.TimeTo = moment(form.value.timeTo.toString()).format('YYYY-M-D');
    body.id = form.value.user.userId;
    console.log(body);
    this.reportGenerateService.loadAllReportForUse(body)
      .subscribe((res: ActivityModel) => {
        this.data = res;
        this.dataSource = new MatTableDataSource<ActivityModel>(this.data);
        this.ngAfterViewInit();
      });
  }

  searchRaportByDataAndProject(form: NgForm) {
    const body: any = {};
    body.TimeFrom = moment(form.value.timeFromProject.toString()).format('YYYY-M-D');
    body.TimeTo = moment(form.value.timeToProject.toString()).format('YYYY-M-D');
    body.id = form.value.project.projectId;
    this.reportGenerateService.loadAllReportForProject(body)
      .subscribe((res: ActivityModel) => {
        this.dataProject = res;
        this.dataSourcePerProject = new MatTableDataSource<ActivityModel>(this.dataProject);
        this.ngAfterViewInit();
      });
  }

  userHoursPerPerson(ActivityType) {
    this.userHourData = 0;
    if (this.data === undefined) {
      return 0;
    }
    this.data.forEach((e, i) => {
      if (e.activityType.name === ActivityType) {
        this.userHourData = this.userHourData + e.howManyHours;
      }
    });
    return this.userHourData;
  }

  userHoursPerProject(ActivityType) {
    this.userHourData = 0;
    if (this.dataProject === undefined) {
      return 0;
    }
    this.dataProject.forEach((e, i) => {
        this.userHourData = this.userHourData + e.howManyHours;
    });
    return this.userHourData;
  }



  userIdToRealName(Id) {
    {
      let returnValue = '';
      this.userDataSource.forEach((e, i) => {
        if (e.userId === Id) {
          returnValue = e.lastName + ' ' + e.fistName;
        }
      });
      return returnValue;

    }
  }

  editReport(reportId) {
    let onlyOneToDeleteData = 0;
    let onlyOneToDeleteDataProject = 0;
    let dialogRef = this.dialog.open(DialogEditReportComponent, {
      width: '500px',
      data: {
        reportId,
      }
    });
    dialogRef.afterClosed()
      .subscribe((res: any) => {
        if (this.data) {
          this.data.forEach((e, i) => {
            if (res === "remove") {
              if (e.id === reportId) {
                this.data.splice(i, 1);
                onlyOneToDeleteData++;
              }
            } else if (e.id === res.id) {
              this.data[i] = res;
            }
          });
        }
        if (this.dataProject) {
          this.dataProject.forEach((e, i) => {
            if (res === "remove") {
              if (e.id === reportId) {
                this.dataProject.splice(i, 1);
                onlyOneToDeleteDataProject++;
              }
            } else if (e.id === res.id) {
              this.dataProject[i] = res;
            }
          });
        }
        this.dataSource = new MatTableDataSource<ActivityModel>(this.data);
        this.dataSourcePerProject = new MatTableDataSource<ActivityModel>(this.dataProject);
        this.ngAfterViewInit();
      });
  }

}
