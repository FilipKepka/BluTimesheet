import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {AuthService} from '../auth/auth.service';

@Injectable()
export class ReportGenerateService {

  private baseUrl: string;

  constructor(private http: HttpClient, private authService: AuthService) {
    this.baseUrl = this.authService.url;
  }


  loadAllReportForUse(report) {
    return this.http.get(`${this.baseUrl}/api/activitiesbyuser/${report.TimeFrom}/${report.TimeTo}/${report.id}`);
  }

  loadAllUsers() {
    return this.http.get(`${this.baseUrl}/api/UserInfo/UserInfo`);
  }

  loadAllProject() {
    return this.http.get(`${this.baseUrl}/api/Project`);
  }

  loadAllReportForProject(report) {
    return this.http.get(`${this.baseUrl}/api/activitiesbyproject/${report.TimeFrom}/${report.TimeTo}/${report.id}`);
  }

  editReport(reportAfterEdit) {
    return this.http.put(`${this.baseUrl}/api/Activity`, JSON.stringify(reportAfterEdit));
  }

  loadSingleReport(reportId)
  {
    return this.http.get(`${this.baseUrl}/api/Activity/${reportId}`);
  }

  loadAllActivityType() {
    return this.http.get(`${this.baseUrl}/api/ActivityType`);
  }

  loadAllProjectRole() {
    return this.http.get(`${this.baseUrl}/api/ProjectRoleType`);
  }

  removeReport(reportToRemove) {
    return this.http.delete(`${this.baseUrl}/api/Activity/${reportToRemove}`);
  }

}
