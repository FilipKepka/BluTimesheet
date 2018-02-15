import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {AuthService} from '../auth/auth.service';

@Injectable()
export class ReportGenerateService {

  public headers;
  private baseUrl: string;

  constructor(private http: HttpClient, private authService: AuthService) {
  this.headers = this.authService.getAuthorizationHeaders();
  this.baseUrl = 'http://localhost:51107/api';
  }


  loadAllReportForUse(report) {
    return this.http.get(`${this.baseUrl}/activitiesbyuser/${report.TimeFrom}/${report.TimeTo}/${report.id}`, {headers: this.headers});
  }

  loadAllUsers() {
    return this.http.get(`${this.baseUrl}/UserInfo/UserInfo`, {headers: this.headers});
  }

  loadAllProject() {
    return this.http.get(`${this.baseUrl}/Project`, {headers: this.headers});
  }

  loadAllReportForProject(report) {
    return this.http.get(`${this.baseUrl}/activitiesbyproject/${report.TimeFrom}/${report.TimeTo}/${report.id}`, {headers: this.headers});
  }

}
