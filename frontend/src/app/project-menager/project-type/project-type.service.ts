import { Injectable } from '@angular/core';
import { AuthService} from '../../auth/auth.service';
import { Observable } from 'rxjs/Observable';
import {BehaviorSubject} from 'rxjs/BehaviorSubject';
import { ProjectTypeModel} from '../../../models/projectType.model';
import {HttpClient} from '@angular/common/http';
import {ProjectsModel} from '../../../models/projects.model';


@Injectable()
export class ProjectTypeService {

  private baseUrl: string;

  constructor( private http: HttpClient, private authService: AuthService ) {
    this.baseUrl = this.authService.url;
  }

  loadAllPeojectsType() {
    return this.http.get(`${this.baseUrl}/api/ProjectType`);
  }

}
