import { Injectable } from '@angular/core';
import {AuthService} from '../auth/auth.service';
import {ProjectsModel} from '../../models/projects.model';
import {HttpClient} from '@angular/common/http';
import {ActivityModel} from '../../models/activity.model';


@Injectable()
export class NewActivityService {

  public headers;
  private baseUrl: string;
  private dataStore: {
    projectManager: ProjectsModel[];
  };


  constructor( private http: HttpClient, private authService: AuthService) {
    this.headers = this.authService.getAuthorizationHeaders();
    this.dataStore = {projectManager: []};
    this.baseUrl = 'http://localhost:51107/api';
  }

  loadAllProjects() {
    return this.http.get(`${this.baseUrl}/Project`, {headers: this.headers});
  }

  loadAllProjectRole() {
    return this.http.get(`${this.baseUrl}/ProjectRoleType`, {headers: this.headers});
  }

  loadAllActivityType() {
    return this.http.get(`${this.baseUrl}/ActivityType`, {headers: this.headers});
  }

  addNewActivity(activity) {
    console.log(activity);
    return this.http.post(`http://localhost:51107/api/Activity`, JSON.stringify(activity), {headers: this.headers});
  }
}
