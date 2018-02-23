import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService} from '../auth/auth.service';
import {NgForm} from '@angular/forms';
import {NewActivityService} from './new-activity.service';
import {ProjectsModel} from '../../models/projects.model';
import {RoleMenagerService} from '../role-menager/role-menager.service';
import {RoleMenagerModel} from '../../models/role-menager.model';
import {MatTableDataSource} from '@angular/material';
import {ActivityModel} from '../../models/activity.model';
import * as moment from 'moment';

@Component({
  selector: 'app-new-activity',
  templateUrl: './new-activity.component.html',
  styleUrls: ['./new-activity.component.css']
})
export class NewActivityComponent implements OnInit {
  Hours: any[];
  projects: ProjectsModel[];
  ActivityTypes: any[];
  projectRole: RoleMenagerModel[];
  private dataStore: {};
  public headers;

  constructor(private newActivityService: NewActivityService, private authService: AuthService) {
    this.Hours = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 ];
  }


  ngOnInit() {
    this.newActivityService.loadAllProjects()
      .subscribe((data: any) => {
        this.projects = data;
      });

    this.newActivityService.loadAllActivityType()
      .subscribe((data: any) => {
        this.ActivityTypes = data;
      });

    this.newActivityService.loadAllProjectRole()
      .subscribe((data: any) => {
        this.projectRole = data;
      });
  }


  sendNewActivity(form: NgForm) {

    const body: any = {};
    body.description = form.value.description;
    body.HowManyHours = form.value.howManyHours;
    body.ActivityType = { ActivityId: form.value.activityType };
    body.Project = { ProjectId: form.value.projects};
    body.CurrentProjectRoleType = { RoleId: form.value.currentProjectRoleType};
    body.Begining = moment(form.value.begining.toString()).format("YYYY-M-D");
    body.UserId = this.authService.getUserInfo().UserId;
    this.newActivityService.addNewActivity(body)
      .subscribe((res: ActivityModel) => {
        this.newActivityService.sendEmail(body)
          .subscribe((resEmail: ActivityModel) => {

          });
        console.log('email-send');
      });
  }

}
/*const body: any = {};
body.Name = form.value.newProject;
body.ProjectType = { Name: form.value.projectType } ;
body.Projectnumber = form.value.projectNumber;


this.projectManagerService.addProject(body)
  .subscribe((res: ProjectsModel) => {
    this.data.push({id: res.id, name: res.name, projectType: res.projectType, projectnumber: res.projectnumber });
    console.log(this.data);
    this.dataSource = new MatTableDataSource<ProjectsModel>(this.data);
  });
form.reset();*/

