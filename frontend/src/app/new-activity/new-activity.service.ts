import { Injectable } from '@angular/core';
import {AuthService} from '../auth/auth.service';
import {ProjectsModel} from '../../models/projects.model';
import {HttpClient} from '@angular/common/http';
import {ActivityModel} from '../../models/activity.model';


@Injectable()
export class NewActivityService {

  private baseUrl: string;
  private dataStore: {
    projectManager: ProjectsModel[];
  };


  constructor( private http: HttpClient, private authService: AuthService) {
    this.dataStore = {projectManager: []};
    this.baseUrl = this.authService.url;
  }

  loadAllProjects() {
    const headers = this.authService.getAuthorizationHeaders();
    return this.http.get(`${this.baseUrl}/api/Project`);
  }

  loadAllProjectRole() {
    return this.http.get(`${this.baseUrl}/api/ProjectRoleType`);
  }

  loadAllActivityType() {
    return this.http.get(`${this.baseUrl}/api/ActivityType`);
  }

  addNewActivity(activity) {
    return this.http.post(`${this.baseUrl}/api/Activity`, JSON.stringify(activity));
  }

  sendEmail(activity) {
    console.log(activity);
    return this.http.post(`${this.baseUrl}/api/sendEmailToSuperior/`, JSON.stringify(activity));
  }
}
